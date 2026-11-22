using System.ComponentModel;
using System.Windows.Forms;

namespace EVEAntiCollision
{
	public partial class Form1
	{
		private IContainer components = null;

		private Button button4;

		private PictureBox pictureBox1;

		private RichTextBox richTextBox1;

		private Button clearWarningButton;

		private Button button2;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				monitorCancellation?.Cancel();
				if (pictureBox1 != null && !pictureBox1.IsDisposed)
				{
					ScreenCapture.ReplacePreview(pictureBox1, null);
				}
				components?.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EVEAntiCollision.Form1));
			this.button4 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.clearWarningButton = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.SuspendLayout();
			this.button4.Font = new System.Drawing.Font("幼圆", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.button4.Location = new System.Drawing.Point(56, 139);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(123, 31);
			this.button4.TabIndex = 8;
			this.button4.Text = "开启自动预警";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(button4_Click);
			this.pictureBox1.Location = new System.Drawing.Point(4, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(47, 240);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 10;
			this.pictureBox1.TabStop = false;
			this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
			this.richTextBox1.Font = new System.Drawing.Font("微软雅黑 Light", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
			this.richTextBox1.Location = new System.Drawing.Point(57, 4);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.richTextBox1.Size = new System.Drawing.Size(123, 129);
			this.richTextBox1.TabIndex = 11;
			this.richTextBox1.Text = "";
			this.clearWarningButton.Font = new System.Drawing.Font("幼圆", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.clearWarningButton.Location = new System.Drawing.Point(57, 213);
			this.clearWarningButton.Name = "clearWarningButton";
			this.clearWarningButton.Size = new System.Drawing.Size(122, 31);
			this.clearWarningButton.TabIndex = 12;
			this.clearWarningButton.Text = "清除预警信息";
			this.clearWarningButton.UseVisualStyleBackColor = true;
			this.clearWarningButton.Click += new System.EventHandler(ClearWarningButton_Click);
			this.button2.Font = new System.Drawing.Font("幼圆", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.button2.Location = new System.Drawing.Point(57, 176);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(122, 31);
			this.button2.TabIndex = 13;
			this.button2.Text = "预警设置";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(Button2_Click);
			this.AutoScaleDimensions = new System.Drawing.SizeF(96f, 96f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(187, 255);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.clearWarningButton);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.button4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EVE 自动预警";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.ResumeLayout(false);
		}
	}
}
