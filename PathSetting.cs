using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;  // text 파일 읽기 위한
using System.Windows.Forms;

namespace CodeManagement
{
	public partial class PathSetting : Form
	{
		public event EventHandler Changed = null;
		public event EventHandler ClosepathSetting = null;


		public string RootPath
		{
			get { return tbRootPath.Text; }
		}

		public string SearcherPaht
		{
			get { return comboBox1.Text; }
		}


		public PathSetting()
		{
			InitializeComponent();

			comboBox1.SelectedIndex = 0;
		}

		// Path.txt 
		private void PathSetting_Load(object sender, EventArgs e)
		{
			string[] strpath = File.ReadAllLines(@"..\..\textFile\Path.txt");  // 파일 로드
			tbRootPath.Text = strpath[0];                                      // 최상단에 출력
		}    

		// treeView Root Path Search
		private void btnRootPathSearch_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;

			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				tbRootPath.Text = dialog.FileName;
			}
		}


		// 확인
		private void btnPathSettingConfirm_Click(object sender, EventArgs e)
		{
			// Rootpath 저장
			string[] strpath = File.ReadAllLines(@"..\..\textFile\Path.txt");
			strpath[0] = tbRootPath.Text;
			File.WriteAllLines(@"..\..\textFile\Path.txt", strpath);


			if (Changed != null)
				Changed(this, new EventArgs());

			if (ClosepathSetting != null)
				ClosepathSetting(this, new EventArgs());
			this.Close();

		}


		// 취소
		private void btnPathSettingCancel_Click(object sender, EventArgs e)
		{
			if (ClosepathSetting != null)
				ClosepathSetting(this, new EventArgs());
			this.Close();
		}
    }

}
