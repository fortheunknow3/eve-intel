using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EVEAntiCollision.Properties;
using Microsoft.VisualBasic.Devices;

namespace EVEAntiCollision
{
	public partial class Form1 : Form
	{
		private const int EmLineScroll = 0x00B6;
		private const int EmGetFirstVisibleLine = 0x00CE;
		private const int VerticalScrollBar = 1;
		private const uint ScrollRangePagePosition = 0x0007;
		private const int CaptureDelayMilliseconds = 250;
		private const int RepeatReminderMilliseconds = 3000;
		private const int RedThreshold = 125;
		private const int GreenThreshold = 20;
		private const int BlueThreshold = 30;
		private const int MinimumRedPixels = 3;

		[StructLayout(LayoutKind.Sequential)]
		private struct ScrollInfo
		{
			public uint Size;
			public uint Mask;
			public int Minimum;
			public int Maximum;
			public uint PageSize;
			public int Position;
			public int TrackPosition;
		}

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr SendMessage(IntPtr window, int message, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetScrollInfo(IntPtr window, int bar, ref ScrollInfo info);

		private readonly Func<Rectangle, Bitmap> captureScreen;
		private CancellationTokenSource monitorCancellation;
		private Task monitorTask;
		private bool closePending;
		private Rectangle monitorRegion;
		private int regionRevision;
		private bool continuousReminder;
		private TaskCompletionSource<bool> settingsWake;

		private static char[] constant = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

		public Form1() : this(ScreenCapture.Capture)
		{
		}

		internal Form1(Func<Rectangle, Bitmap> captureScreen)
		{
			this.captureScreen = captureScreen;
			InitializeComponent();
		}

		public static string GenerateRandomString(int length)
		{
			string checkCode = string.Empty;
			Random rd = new Random();
			for (int i = 0; i < length; i++)
			{
				checkCode += constant[rd.Next(10)];
			}
			return checkCode;
		}

		public string GetTimeStamp()
		{
			return Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds - 28800.0).ToString();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (monitorCancellation != null)
			{
				RequestPause();
				return;
			}

			Rectangle region;
			bool repeat;
			try
			{
				region = ScreenCapture.ReadSavedRegion();
				repeat = Settings.Default.ContinuousReminder;
			}
			catch (Exception error)
			{
				button4.Text = "预警未开启";
				button4.ForeColor = Color.Red;
				WriteLog("读取预警设置失败。" + error.Message);
				return;
			}

			if (!ScreenCapture.IsValidRegion(region))
			{
				button4.Text = "预警区域无效";
				button4.ForeColor = Color.Red;
				WriteLog(ScreenCapture.InvalidRegionMessage + " 请重新设置预警区域。");
				return;
			}

			ApplyWarningSettings(region, repeat);
			button4.Text = "预警已开启";
			button4.ForeColor = Color.Green;
			WriteLog("预警开启\n Fly Safe！");
			monitorCancellation = new CancellationTokenSource();
			monitorTask = MonitorAsync(monitorCancellation);
		}

		private void RequestPause()
		{
			button4.Text = "正在暂停…";
			button4.Enabled = false;
			monitorCancellation.Cancel();
		}

		private void ApplyWarningSettings(Rectangle region, bool repeat)
		{
			bool regionChanged = monitorRegion != region;
			if (!regionChanged && continuousReminder == repeat)
			{
				return;
			}

			continuousReminder = repeat;
			if (regionChanged)
			{
				monitorRegion = region;
				regionRevision++;
				ScreenCapture.ReplacePreview(pictureBox1, null);
			}
			settingsWake?.TrySetResult(true);
		}

		private async Task MonitorAsync(CancellationTokenSource cancellation)
		{
			CancellationToken token = cancellation.Token;
			bool redPresent = false;
			int lastRegionRevision = regionRevision;
			var sinceReminder = new Stopwatch();
			bool failed = false;
			try
			{
				while (!token.IsCancellationRequested)
				{
					Rectangle region = monitorRegion;
					int capturedRevision = regionRevision;
					(Bitmap Image, bool HasRed) result;
					try
					{
						result = await Task.Run(() => CaptureFrame(region, token), token);
					}
					catch (Exception) when (!token.IsCancellationRequested && capturedRevision != regionRevision)
					{
						continue;
					}

					if (token.IsCancellationRequested)
					{
						result.Image.Dispose();
						break;
					}

					if (capturedRevision != regionRevision)
					{
						result.Image.Dispose();
						continue;
					}

					if (lastRegionRevision != capturedRevision)
					{
						redPresent = false;
						lastRegionRevision = capturedRevision;
					}

					ScreenCapture.ReplacePreview(pictureBox1, result.Image);
					if (result.HasRed && (!redPresent ||
						(continuousReminder && sinceReminder.ElapsedMilliseconds >= RepeatReminderMilliseconds)))
					{
						ReportRed();
						sinceReminder.Restart();
					}
					redPresent = result.HasRed;
					await WaitForNextFrameAsync(token);
				}
			}
			catch (OperationCanceledException) when (token.IsCancellationRequested)
			{
			}
			catch (Exception error)
			{
				if (!token.IsCancellationRequested)
				{
					failed = true;
					WriteLog("监控失败，预警已停止。" + error.Message);
				}
			}
			finally
			{
				monitorCancellation = null;
				monitorTask = null;
				cancellation.Dispose();
				if (!IsDisposed && !Disposing)
				{
					button4.Text = failed ? "预警已停止" : "预警已暂停";
					button4.ForeColor = Color.Red;
					button4.Enabled = true;
					if (!failed && !closePending)
					{
						WriteLog("预警暂停\n ------");
					}
				}
			}
		}

		private async Task WaitForNextFrameAsync(CancellationToken token)
		{
			var wake = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
			settingsWake = wake;
			try
			{
				await Task.WhenAny(Task.Delay(CaptureDelayMilliseconds, token), wake.Task);
			}
			finally
			{
				settingsWake = null;
			}
		}

		private (Bitmap Image, bool HasRed) CaptureFrame(Rectangle region, CancellationToken token)
		{
			Bitmap image = captureScreen(region);
			try
			{
				token.ThrowIfCancellationRequested();
				int minimum = (int)Math.Min(MinimumRedPixels, (long)image.Width * image.Height);
				bool hasRed = HasMatchingPixels(image, RedThreshold, GreenThreshold, BlueThreshold, minimum, token);
				return (image, hasRed);
			}
			catch
			{
				image.Dispose();
				throw;
			}
		}

		private void ReportRed()
		{
			WriteLog("来红！");
			try
			{
				string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sound.wav");
				new Audio().Play(soundPath);
			}
			catch (Exception error)
			{
				WriteLog("预警提示音播放失败。" + error.Message);
			}
		}

		private void WriteLog(string message)
		{
			AppendWarningInfo(DateTime.Now.ToString("HH:mm:ss") + " " + message + "\n");
		}

		protected override async void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			if (e.Cancel || monitorCancellation == null)
			{
				return;
			}

			if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.TaskManagerClosing)
			{
				monitorCancellation.Cancel();
				return;
			}

			e.Cancel = true;
			if (closePending)
			{
				return;
			}

			closePending = true;
			Task pending = monitorTask;
			RequestPause();
			await pending;
			await Task.Yield();
			closePending = false;
			if (!IsDisposed)
			{
				Close();
			}
		}

		private void AppendWarningInfo(string info)
		{
			RichTextBox log = richTextBox1;
			if (log.TextLength == 0)
			{
				log.ScrollBars = RichTextBoxScrollBars.Vertical;
			}

			IntPtr handle = log.Handle;
			int selectionLength = log.SelectionLength;
			bool followLatest = selectionLength == 0 && !log.Capture && IsLogAtBottom(handle);
			if (followLatest)
			{
				log.AppendText(info);
				log.ScrollToCaret();
				return;
			}

			int selectionStart = log.SelectionStart;
			int firstLine = SendMessage(handle, EmGetFirstVisibleLine, IntPtr.Zero, IntPtr.Zero).ToInt32();
			log.AppendText(info);
			log.Select(selectionStart, selectionLength);
			int currentLine = SendMessage(handle, EmGetFirstVisibleLine, IntPtr.Zero, IntPtr.Zero).ToInt32();
			if (currentLine != firstLine)
			{
				SendMessage(handle, EmLineScroll, IntPtr.Zero, (IntPtr)(firstLine - currentLine));
			}
		}

		private static bool IsLogAtBottom(IntPtr handle)
		{
			var scroll = new ScrollInfo
			{
				Size = (uint)Marshal.SizeOf<ScrollInfo>(),
				Mask = ScrollRangePagePosition
			};
			// RichEdit can stop one position short of the inclusive limit when scrolled manually.
			return !GetScrollInfo(handle, VerticalScrollBar, ref scroll) ||
				scroll.PageSize == 0 ||
				scroll.Position >= (long)scroll.Maximum - scroll.PageSize;
		}

		public bool GetRGB(Bitmap src, int tr, int tg, int tb)
		{
			return HasMatchingPixels(src, tr, tg, tb, 1, CancellationToken.None);
		}

		private static unsafe bool HasMatchingPixels(Bitmap image, int redThreshold, int greenThreshold,
			int blueThreshold, int minimum, CancellationToken token)
		{
			Size size = image.Size;
			BitmapData data = image.LockBits(new Rectangle(Point.Empty, size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			try
			{
				byte* firstRow = (byte*)data.Scan0.ToPointer();
				for (int y = 0; y < size.Height; y++)
				{
					token.ThrowIfCancellationRequested();
					byte* pixel = firstRow + (long)y * data.Stride;
					for (int x = 0; x < size.Width; x++, pixel += 4)
					{
						if (pixel[2] > redThreshold && pixel[1] < greenThreshold && pixel[0] < blueThreshold)
						{
							minimum--;
							if (minimum == 0)
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			finally
			{
				image.UnlockBits(data);
			}
		}

		private void ClearWarningButton_Click(object sender, EventArgs e)
		{
			richTextBox1.Clear();
			richTextBox1.ScrollBars = RichTextBoxScrollBars.None;
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			foreach (Form ownedForm in OwnedForms)
			{
				if (ownedForm is Form3)
				{
					ownedForm.Activate();
					return;
				}
			}

			var settingsForm = new Form3();
			settingsForm.SettingsSaved += ApplyWarningSettings;
			settingsForm.Show(this);
		}
	}
}
