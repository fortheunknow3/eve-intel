using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using EVEAntiCollision.Properties;

namespace EVEAntiCollision
{
	internal static class ScreenCapture
	{
		internal const string InvalidRegionMessage = "区域宽度和高度必须大于零，且整个区域须位于当前桌面范围内。";

		internal static Rectangle ReadSavedRegion()
		{
			Settings settings = Settings.Default;
			return new Rectangle(settings.start_x, settings.start_y, settings.area_x, settings.area_y);
		}

		internal static void SaveWarningSettings(Rectangle region, bool continuousReminder)
		{
			ValidateRegion(region);
			Settings settings = Settings.Default;
			Rectangle previous = ReadSavedRegion();
			bool previousMode = settings.ContinuousReminder;
			try
			{
				WriteSettings(settings, region, continuousReminder);
				settings.Save();
			}
			catch
			{
				WriteSettings(settings, previous, previousMode);
				throw;
			}
		}

		private static void WriteSettings(Settings settings, Rectangle region, bool continuousReminder)
		{
			settings.start_x = region.X;
			settings.start_y = region.Y;
			settings.area_x = region.Width;
			settings.area_y = region.Height;
			settings.ContinuousReminder = continuousReminder;
		}

		internal static bool IsValidRegion(Rectangle region)
		{
			Rectangle desktop = SystemInformation.VirtualScreen;
			return region.Width > 0 && region.Height > 0 &&
				region.Left >= desktop.Left && region.Top >= desktop.Top &&
				(long)region.Left + region.Width <= desktop.Right &&
				(long)region.Top + region.Height <= desktop.Bottom;
		}

		private static void ValidateRegion(Rectangle region)
		{
			if (!IsValidRegion(region))
			{
				throw new ArgumentException(InvalidRegionMessage);
			}
		}

		internal static Bitmap Capture(Rectangle region)
		{
			ValidateRegion(region);
			var image = new Bitmap(region.Width, region.Height, PixelFormat.Format32bppArgb);
			try
			{
				using (Graphics graphics = Graphics.FromImage(image))
				{
					graphics.CopyFromScreen(region.Location, Point.Empty, region.Size);
				}
				return image;
			}
			catch
			{
				image.Dispose();
				throw;
			}
		}

		internal static void ReplacePreview(PictureBox preview, Image image)
		{
			Image previous = preview.Image;
			if (ReferenceEquals(previous, image))
			{
				return;
			}

			try
			{
				preview.Image = image;
			}
			finally
			{
				if (ReferenceEquals(preview.Image, image))
				{
					previous?.Dispose();
				}
				else
				{
					image?.Dispose();
				}
			}
		}
	}
}
