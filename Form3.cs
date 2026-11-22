using System;
using System.Drawing;
using System.Windows.Forms;
using EVEAntiCollision.Properties;

namespace EVEAntiCollision
{
	public partial class Form3 : Form
	{
		internal event Action<Rectangle, bool> SettingsSaved;

		public Form3()
		{
			InitializeComponent();
		}

		private void SelectRegionButton_Click(object sender, EventArgs e)
		{
			Form mainWindow = Owner;
			bool restoreMainWindow = mainWindow != null && mainWindow.Visible;
			Hide();
			try
			{
				if (restoreMainWindow)
				{
					mainWindow.Hide();
				}

				using (var selector = new RegionSelectionForm())
				{
					if (selector.ShowDialog(this) != DialogResult.OK)
					{
						return;
					}

					SetRegionFields(selector.SelectedRegion);
					ScreenCapture.ReplacePreview(pictureBox1, null);
				}
			}
			finally
			{
				if (restoreMainWindow)
				{
					mainWindow.Show();
				}

				Show();
				Activate();
			}
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			try
			{
				ScreenCapture.ReplacePreview(pictureBox1, ScreenCapture.Capture(ReadRegionFields()));
			}
			catch (Exception error)
			{
				ShowSettingsError("无法预览选中的区域。" + error.Message);
			}
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			Rectangle region;
			bool repeat = continuousReminderRadio.Checked;
			try
			{
				region = ReadRegionFields();
				ScreenCapture.SaveWarningSettings(region, repeat);
			}
			catch (Exception error)
			{
				ShowSettingsError("保存预警设置失败。" + error.Message);
				return;
			}

			SettingsSaved?.Invoke(region, repeat);
			saveStatusLabel.Text = "上次保存 " + DateTime.Now.ToString("HH:mm:ss");
		}

		private void Form3_Load(object sender, EventArgs e)
		{
			try
			{
				SetRegionFields(ScreenCapture.ReadSavedRegion());
				bool repeat = Settings.Default.ContinuousReminder;
				singleReminderRadio.Checked = !repeat;
				continuousReminderRadio.Checked = repeat;
			}
			catch (Exception error)
			{
				ShowSettingsError("读取预警设置失败。" + error.Message);
			}
		}

		private Rectangle ReadRegionFields()
		{
			if (!int.TryParse(textBox1.Text, out int x) || !int.TryParse(textBox2.Text, out int y) ||
				!int.TryParse(textBox3.Text, out int width) || !int.TryParse(textBox4.Text, out int height))
			{
				throw new ArgumentException("起始坐标、宽度和高度都必须为整数。");
			}

			return new Rectangle(x, y, width, height);
		}

		private void ShowSettingsError(string message)
		{
			MessageBox.Show(this, message, "预警设置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void SetRegionFields(Rectangle region)
		{
			textBox1.Text = region.X.ToString();
			textBox2.Text = region.Y.ToString();
			textBox3.Text = region.Width.ToString();
			textBox4.Text = region.Height.ToString();
		}
	}
}
