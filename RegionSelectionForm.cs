using System;
using System.Drawing;
using System.Windows.Forms;

namespace EVEAntiCollision
{
	internal sealed class RegionSelectionForm : Form
	{
		private Point? dragStart;
		private Rectangle dragBounds;

		public Rectangle SelectedRegion { get; private set; }

		public RegionSelectionForm()
		{
			AutoScaleMode = AutoScaleMode.None;
			FormBorderStyle = FormBorderStyle.None;
			StartPosition = FormStartPosition.Manual;
			Text = "框选预警区域";
			ShowInTaskbar = false;
			TopMost = true;
			BackColor = Color.Black;
			Opacity = 0.4;
			Cursor = Cursors.Cross;
			DoubleBuffered = true;
			Bounds = SystemInformation.VirtualScreen;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (dragBounds.Width > 0 && dragBounds.Height > 0)
			{
				e.Graphics.FillRectangle(Brushes.DeepSkyBlue, dragBounds);
				using (var border = new Pen(Color.White, 2))
				{
					e.Graphics.DrawRectangle(border, dragBounds);
				}
			}

			Point hintLocation = PointToClient(Screen.FromPoint(MousePosition).Bounds.Location);
			hintLocation.Offset(16, 16);
			TextRenderer.DrawText(e.Graphics, "按住鼠标左键拖动框选区域，按 Esc 取消", Font, hintLocation, Color.White);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				dragStart = e.Location;
				Capture = true;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (dragStart.HasValue)
			{
				dragBounds = GetDragBounds(e.Location);
				Invalidate();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (e.Button != MouseButtons.Left || !dragStart.HasValue)
			{
				return;
			}

			Rectangle bounds = GetDragBounds(e.Location);
			dragStart = null;
			dragBounds = Rectangle.Empty;
			Capture = false;
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				Invalidate();
				return;
			}

			SelectedRegion = RectangleToScreen(bounds);
			DialogResult = DialogResult.OK;
		}

		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
			if (!Capture && dragStart.HasValue)
			{
				dragStart = null;
				dragBounds = Rectangle.Empty;
				Invalidate();
			}
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				DialogResult = DialogResult.Cancel;
				return true;
			}

			return base.ProcessDialogKey(keyData);
		}

		private Rectangle GetDragBounds(Point end)
		{
			Point start = dragStart.Value;
			end.X = Math.Max(0, Math.Min(ClientSize.Width, end.X));
			end.Y = Math.Max(0, Math.Min(ClientSize.Height, end.Y));
			return Rectangle.FromLTRB(
				Math.Min(start.X, end.X),
				Math.Min(start.Y, end.Y),
				Math.Max(start.X, end.X),
				Math.Max(start.Y, end.Y));
		}
	}
}
