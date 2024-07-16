
namespace CodeManagement
{
	partial class PathSetting
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
            this.btnRootPathSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRootPath = new System.Windows.Forms.TextBox();
            this.btnPathSettingCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPathSettingConfirm = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRootPathSearch
            // 
            this.btnRootPathSearch.Location = new System.Drawing.Point(396, 76);
            this.btnRootPathSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRootPathSearch.Name = "btnRootPathSearch";
            this.btnRootPathSearch.Size = new System.Drawing.Size(86, 29);
            this.btnRootPathSearch.TabIndex = 6;
            this.btnRootPathSearch.Text = "Search";
            this.btnRootPathSearch.UseVisualStyleBackColor = true;
            this.btnRootPathSearch.Click += new System.EventHandler(this.btnRootPathSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Root Path";
            // 
            // tbRootPath
            // 
            this.tbRootPath.Location = new System.Drawing.Point(19, 76);
            this.tbRootPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbRootPath.Name = "tbRootPath";
            this.tbRootPath.Size = new System.Drawing.Size(355, 25);
            this.tbRootPath.TabIndex = 4;
            // 
            // btnPathSettingCancel
            // 
            this.btnPathSettingCancel.Location = new System.Drawing.Point(416, 416);
            this.btnPathSettingCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPathSettingCancel.Name = "btnPathSettingCancel";
            this.btnPathSettingCancel.Size = new System.Drawing.Size(86, 29);
            this.btnPathSettingCancel.TabIndex = 8;
            this.btnPathSettingCancel.Text = "취소";
            this.btnPathSettingCancel.UseVisualStyleBackColor = true;
            this.btnPathSettingCancel.Click += new System.EventHandler(this.btnPathSettingCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbRootPath);
            this.groupBox1.Controls.Add(this.btnRootPathSearch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(490, 395);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Path Setting";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "https://www.google.co.kr/",
            "https://www.naver.com/"});
            this.comboBox1.Location = new System.Drawing.Point(19, 153);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(355, 23);
            this.comboBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Root Path";
            // 
            // btnPathSettingConfirm
            // 
            this.btnPathSettingConfirm.Location = new System.Drawing.Point(313, 416);
            this.btnPathSettingConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPathSettingConfirm.Name = "btnPathSettingConfirm";
            this.btnPathSettingConfirm.Size = new System.Drawing.Size(86, 29);
            this.btnPathSettingConfirm.TabIndex = 7;
            this.btnPathSettingConfirm.Text = "확인";
            this.btnPathSettingConfirm.UseVisualStyleBackColor = true;
            this.btnPathSettingConfirm.Click += new System.EventHandler(this.btnPathSettingConfirm_Click);
            // 
            // PathSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 463);
            this.Controls.Add(this.btnPathSettingCancel);
            this.Controls.Add(this.btnPathSettingConfirm);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PathSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PathSetting";
            this.Load += new System.EventHandler(this.PathSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnRootPathSearch;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbRootPath;
		private System.Windows.Forms.Button btnPathSettingCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnPathSettingConfirm;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label1;
	}
}