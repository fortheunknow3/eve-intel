using System.ComponentModel;
using System.Windows.Forms;

namespace EVEAntiCollision
{
	public partial class Form3
	{
		private IContainer components = null;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private TextBox textBox1;

		private TextBox textBox2;

		private TextBox textBox3;

		private TextBox textBox4;

		private PictureBox pictureBox1;

		private Button selectRegionButton;

		private Button button1;

		private Button button2;

		private GroupBox reminderModeGroup;

		private RadioButton singleReminderRadio;

		private RadioButton continuousReminderRadio;

		private Label reminderModeHint;

		private Label saveStatusLabel;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EVEAntiCollision.Form3));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.selectRegionButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.reminderModeGroup = new System.Windows.Forms.GroupBox();
			this.singleReminderRadio = new System.Windows.Forms.RadioButton();
			this.continuousReminderRadio = new System.Windows.Forms.RadioButton();
			this.reminderModeHint = new System.Windows.Forms.Label();
			this.saveStatusLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.reminderModeGroup.SuspendLayout();
			this.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "起始X：";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "起始Y：";
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "宽度：";
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 138);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 12);
			this.label4.TabIndex = 3;
			this.label4.Text = "高度：";
			this.textBox1.Location = new System.Drawing.Point(58, 12);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(60, 21);
			this.textBox1.TabIndex = 4;
			this.textBox2.Location = new System.Drawing.Point(58, 53);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(60, 21);
			this.textBox2.TabIndex = 5;
			this.textBox3.Location = new System.Drawing.Point(58, 94);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(60, 21);
			this.textBox3.TabIndex = 6;
			this.textBox4.Location = new System.Drawing.Point(58, 135);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(60, 21);
			this.textBox4.TabIndex = 7;
			this.pictureBox1.Location = new System.Drawing.Point(126, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(288, 215);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 12;
			this.pictureBox1.TabStop = false;
			this.selectRegionButton.Location = new System.Drawing.Point(12, 164);
			this.selectRegionButton.Name = "selectRegionButton";
			this.selectRegionButton.Size = new System.Drawing.Size(106, 27);
			this.selectRegionButton.TabIndex = 8;
			this.selectRegionButton.Text = "鼠标框选区域";
			this.selectRegionButton.UseVisualStyleBackColor = true;
			this.selectRegionButton.Click += new System.EventHandler(SelectRegionButton_Click);
			this.button1.Location = new System.Drawing.Point(12, 197);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(106, 27);
			this.button1.TabIndex = 9;
			this.button1.Text = "查看效果";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(Button1_Click);
			this.reminderModeGroup.Controls.Add(this.singleReminderRadio);
			this.reminderModeGroup.Controls.Add(this.continuousReminderRadio);
			this.reminderModeGroup.Controls.Add(this.reminderModeHint);
			this.reminderModeGroup.Location = new System.Drawing.Point(12, 233);
			this.reminderModeGroup.Name = "reminderModeGroup";
			this.reminderModeGroup.Size = new System.Drawing.Size(402, 68);
			this.reminderModeGroup.TabIndex = 10;
			this.reminderModeGroup.TabStop = false;
			this.reminderModeGroup.Text = "提醒模式";
			this.singleReminderRadio.AutoSize = true;
			this.singleReminderRadio.Checked = true;
			this.singleReminderRadio.Location = new System.Drawing.Point(12, 20);
			this.singleReminderRadio.Name = "singleReminderRadio";
			this.singleReminderRadio.TabIndex = 0;
			this.singleReminderRadio.TabStop = true;
			this.singleReminderRadio.Text = "单次提醒";
			this.singleReminderRadio.UseVisualStyleBackColor = true;
			this.continuousReminderRadio.AutoSize = true;
			this.continuousReminderRadio.Location = new System.Drawing.Point(129, 20);
			this.continuousReminderRadio.Name = "continuousReminderRadio";
			this.continuousReminderRadio.TabIndex = 1;
			this.continuousReminderRadio.Text = "持续提醒";
			this.continuousReminderRadio.UseVisualStyleBackColor = true;
			this.reminderModeHint.AutoSize = true;
			this.reminderModeHint.Location = new System.Drawing.Point(12, 45);
			this.reminderModeHint.Text = "持续提醒每 3 秒重复，直到红色消失。";
			this.saveStatusLabel.AutoSize = true;
			this.saveStatusLabel.Location = new System.Drawing.Point(12, 317);
			this.saveStatusLabel.Name = "saveStatusLabel";
			this.saveStatusLabel.Text = "保存后立即应用";
			this.button2.Location = new System.Drawing.Point(308, 310);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(106, 27);
			this.button2.TabIndex = 11;
			this.button2.Text = "保存并应用";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(Button2_Click);
			this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(420, 345);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.saveStatusLabel);
			this.Controls.Add(this.reminderModeGroup);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.selectRegionButton);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form3";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "预警设置";
			this.Load += new System.EventHandler(Form3_Load);
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.reminderModeGroup.ResumeLayout(false);
			this.reminderModeGroup.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
	}
}
