
namespace CodeManagement
{
	partial class ColorSetting
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button6 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(602, 35);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(75, 26);
			this.button6.TabIndex = 23;
			this.button6.Text = "색 검색";
			this.button6.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(511, 94);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 26);
			this.button5.TabIndex = 22;
			this.button5.Text = "등록";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click_1);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(602, 94);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 26);
			this.button4.TabIndex = 21;
			this.button4.Text = "삭제";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click_1);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(511, 35);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 26);
			this.button3.TabIndex = 20;
			this.button3.Text = "단어 검색";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click_1);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(602, 321);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 26);
			this.button2.TabIndex = 19;
			this.button2.Text = "취소";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(46, 52);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(108, 21);
			this.textBox1.TabIndex = 18;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(172, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 12);
			this.label2.TabIndex = 17;
			this.label2.Text = "지정할 색상";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(44, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 16;
			this.label1.Text = "문자";
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Items.AddRange(new object[] {
            "Gray",
            "Blue",
            "lightblue",
            "Green",
            "Red"});
			this.comboBox2.Location = new System.Drawing.Point(174, 53);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(107, 20);
			this.comboBox2.TabIndex = 15;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(511, 321);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 26);
			this.button1.TabIndex = 14;
			this.button1.Text = "확인";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// ColorSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(705, 360);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.button1);
			this.Name = "ColorSetting";
			this.Text = "ColorSetting";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Button button1;
	}
}