using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;  // text 파일 읽기 위한
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeManagement
{
    public partial class PathSetting : Form
    {
        public event EventHandler Changed = null;
        public event EventHandler ClosePathSetting = null;

        public PathSetting()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(PathSetting_FormClosing);
            comboBox1.SelectedIndex = 0;
        }

        public string RootPath
        {
            get { return tbRootPath.Text; }
        }

        public string SearcherPaht
        {
            get { return comboBox1.Text; }
        }


        // Path.txt 
        private void PathSetting_Load(object sender, EventArgs e)
        {
            try
            {
                string[] strpath = File.ReadAllLines(@"..\..\textFile\Path.txt");  // 파일 로드
                tbRootPath.Text = strpath[0];                                      // 최상단에 출력
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        #region 창 닫힘
        // 확인
        private void btnPathSettingConfirm_Click(object sender, EventArgs e)
        {
            // 확인 메시지 박스를 표시합니다.
            DialogResult result = MessageBox.Show("저장합니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 확인 버튼을 누른 경우에만 저장
            if (result == DialogResult.Yes)
            {
                // Rootpath 저장
                string[] strpath = File.ReadAllLines(@"..\..\textFile\Path.txt");
                strpath[0] = tbRootPath.Text;
                File.WriteAllLines(@"..\..\textFile\Path.txt", strpath);


                if (Changed != null)
                    Changed(this, new EventArgs());

                Close_PathSetting();
            }
        }

        // 취소
        private void btnPathSettingCancel_Click(object sender, EventArgs e)
        {
            Close_PathSetting();
        }

        private void PathSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 확인 메시지 박스를 표시합니다.
            //DialogResult result = MessageBox.Show("정말로 닫으시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 코드 간략화
            if (MessageBox.Show("정말로 닫으시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else 
            {
                Close_PathSetting();
            }
        }

        private void Close_PathSetting()
        {
            if (ClosePathSetting != null)
                ClosePathSetting(this, new EventArgs());
            this.Close();
        }
        #endregion
    }
}
