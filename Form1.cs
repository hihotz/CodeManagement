using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web;
using System.Threading;
using System.Collections.Generic;

namespace CodeManagement
{
    public partial class Form1 : Form
    {
        private PathSetting pathSetting = null;
        private string url = null;
        string m_curPath = "";
        Thread m_thread = null;
        private static string FullPath = null;
        private static string OnlyFileName;
        private static string OnlyFilePath;
        private static string OnlyExtension;
        private ColorSetting colorSetting = null;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            // 자바스크립트 오류 안뜨게하기
            wbMain.ScriptErrorsSuppressed = true;
        }

        #region Form1 로드시 
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] strpath = File.ReadAllLines(@"..\..\textFile\Path.txt");
                textbRootPath.Text = strpath[0];

                treeView2.Nodes.Clear(); // 트리뷰 클리어
                ListDirectory(treeView2, textbRootPath.Text);

                //url = pathSetting.SearcherPaht;  // 초기 구글 검색으로 
                url = "https://www.google.co.kr/";
                wbMain.Navigate(url);

                this.Invalidate();
            }
            catch (Exception)
            {
                MessageBox.Show("디렉토리를 설정해주세요");
            }
        }
        #endregion

        #region Form1 닫힘
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 사용자가 'No'를 선택하면 닫히지 않도록 합니다.
            if (MessageBox.Show("종료하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region TreeView 
        // TreeView2 load
        private void ListDirectory(TreeView treeView, string path)
        {
            try
            {
                treeView2.Nodes.Clear();  // 트리뷰 노드 클리어
                DirectoryInfo rootDirectioryInfo = new DirectoryInfo(path);  // DirectoryInfo는 말그대로 디렉토리의 정보가 들어있음, 생성,삭제등의 메소드 제공   path의 루트 디렉토리 정보를 가져옴     
                treeView2.Nodes.Add(CreateDirectoryNode(rootDirectioryInfo));
            }
            catch
            {
                MessageBox.Show("디렉토리를 설정해주세요");
            }
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo) //CreateDirectoryNode 는 recursive method. 메소드 안에서 자신의 메소드를 호출.
        {
            TreeNode directoryNode = new TreeNode(directoryInfo.Name);
            try
            {
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
                }
                foreach (var file in directoryInfo.GetFiles())
                {
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("디렉토리를 설정해주세요");
            }
            return directoryNode;
        }

        // treeview2 폰트 굵기 및 색상 변경(선택 노드)
        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //treeView2.SelectedNode.NodeFont = new Font(treeView2.Font, FontStyle.Bold);
            treeView2.SelectedNode.ForeColor = Color.Orange;

        }
        private void treeView2_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (treeView2.SelectedNode != null)
            {
                //treeView2.SelectedNode.NodeFont = new Font(treeView2.Font, FontStyle.Regular);
                treeView2.SelectedNode.ForeColor = Color.Black;
            }
        }
        #endregion

        #region Treeview Extend , Collapse
        // Extend
        private void btnTreeviewExtend_Click(object sender, EventArgs e)
        {
            if (treeView2.SelectedNode != null)
            {
                treeView2.SelectedNode.ExpandAll();
            }
        }

        //Collapse
        private void btnTreeviewCollapse_Click(object sender, EventArgs e)
        {
            treeView2.CollapseAll();
        }

        #endregion

        #region Treeview Path 출력
        // path란에 표기
        private void treeView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string TreeViewFullPath = treeView2.SelectedNode.FullPath; // 트리뷰로 부터 경로를 가져옴
                TreeViewPath.Items.Insert(0, TreeViewFullPath);            // 경로를 최상단에 넣어 보여줌 
                CutFilename(TreeViewFullPath);                             //< ------------------------------

                int num1 = TreeViewFullPath.IndexOf('\\');
                string strMade = '\\' + TreeViewFullPath.Remove(0, num1 + 1);  // 경로 만들기


                string textValue = File.ReadAllText(textbRootPath.Text + strMade); // 파일  읽어 오기
                rtbCodeViewer.Text = textValue;

            }
            catch (Exception)
            {
                rtbCodeViewer.Text = " 읽을 수 없는 파일입니다.";
                m_curPath = madeFolderFullPath();
                ViewDirectoryList(m_curPath);
            }


        }
        #endregion

        #region 파일 경로
        private string madeFolderFullPath()
        {
            try
            {
                string madRootpath = textbRootPath.Text;
                string treeviewpath = treeView2.SelectedNode.FullPath;
                int ExtractPath = treeviewpath.IndexOf('\\');

                if (ExtractPath == -1)
                {
                    return madRootpath;
                }
                else
                {
                    treeviewpath = treeviewpath.Substring(ExtractPath);
                    string FolderFileFullPath = madRootpath + treeviewpath;

                    return FolderFileFullPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void CutFilename(string TreeViewFullPath)
        {
            int ExtractPath = TreeViewFullPath.LastIndexOf('\\');
            OnlyFilePath = TreeViewFullPath.Remove(ExtractPath) + '\\';         //파일 경로

            int ExtractExtension = TreeViewFullPath.LastIndexOf('.');
            OnlyExtension = TreeViewFullPath.Substring(ExtractExtension);           //파일 확장자  

            int ExtractName = TreeViewFullPath.LastIndexOf('\\');
            string FileFullName = TreeViewFullPath.Substring(ExtractName + 1);
            int CutExtension = FileFullName.IndexOf('.');               //확장자 제거
            OnlyFileName = FileFullName.Remove(CutExtension);
        }
        #endregion

        #region 입력된 값 저장

        //button name : Save_Button

        private void btnSave_Click(object sender, EventArgs e)
        {
            string str = rtbCodeViewer.Text;        //입력된 코드
            SaveFileDialog(str);        //코드 전달

        }

        //파일 저장
        private void WriteTxtFile(string str, string filename)
        {
            StreamWriter writer_;
            String strFilePath = filename;      //파일경로 + 파일이름
            writer_ = File.CreateText(strFilePath);
            writer_.Write(str);
            writer_.Close();
        }
        //저장 경로 설정
        private void SaveFileDialog(string str)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            string filename;
            if (OnlyFileName != null)
            {
                saveFile.InitialDirectory = OnlyFilePath;
                saveFile.FileName = OnlyFileName;
                saveFile.Title = "저장 위치 지정";
                saveFile.DefaultExt = OnlyExtension;
                saveFile.Filter = "모든파일|*.* | cshop(*.cs)|*.cs | txt(*.txt)|*.txt";
            }

            else
            {
                //filename = "";
                // 다이얼 로그가 Open되었을 때 최초의 경로 설정
                saveFile.InitialDirectory = textbRootPath.Text;     //path 경로를 기본 경로로
                                                                    // 다이얼 로그의 제목
                saveFile.Title = "저장 위치 지정";
                // 기본 확장자
                saveFile.DefaultExt = "cs";
                // 파일 목록 필터링
                saveFile.Filter = "cshop(*.cs)|*.cs | txt(*.txt)|*.txt | 모든파일|*.*";
            }
            // OK버튼을 눌렀을때의 동작
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                // 경로와 파일명을 fileName에 저장
                filename = saveFile.FileName.ToString();
                WriteTxtFile(str, filename);
            }
            else
                return;
        }
        #endregion

        #region 환경 설정 _ path 설정 / Color 설정

        private void settingPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pathSetting == null)
            {
                pathSetting = new PathSetting();
                pathSetting.Owner = this;
                pathSetting.Changed += new EventHandler(pathSettingApply);  // 컨트롤 등의 변경
                pathSetting.ClosePathSetting += new EventHandler(pathSettingClose); // 닫음
                pathSetting.Show();
            }
            else
            {
                pathSetting.Focus();
            }
        }

        private void colorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (colorSetting == null)
            {
                colorSetting = new ColorSetting();
                colorSetting.Owner = this;
                colorSetting.CloseColorSetting += new EventHandler(colorSettingClose); // 닫음
                colorSetting.Show();
            }
            else
            {
                colorSetting.Focus();
            }
        }

        #endregion

        #region (pathSetting)모달리스 호출 이벤트 핸들러
        public void pathSettingApply(object sender, EventArgs e)
        {
            textbRootPath.Text = pathSetting.RootPath;     // pathSetting Form에서  Rootpath 정보를 가져와서 ~

            treeView2.Nodes.Clear();                       // 트리뷰 지우고
            ListDirectory(treeView2, textbRootPath.Text);  // 다시 생성
        }

        public void pathSettingClose(object sender, EventArgs e)
        {
            //모달리스 종료 처리
            pathSetting.Dispose();
            pathSetting = null;
        }

        public void colorSettingClose(object sender, EventArgs e)
        {
            //모달리스 종료 처리
            colorSetting.Dispose();
            colorSetting = null;
        }
        #endregion

        #region 글자 색 변경
        private void rtbCodeViewer_TextChanged_1(object sender, EventArgs e)
        {
            Regex regexgray = new Regex(ColorSetting.strTargetGray); //Regex 사용하여 특정문자 찾기
            MatchCollection mcgray = regexgray.Matches(rtbCodeViewer.Text);

            Regex regexblue = new Regex(ColorSetting.strTargetBlue); //Regex 사용하여 특정문자 찾기
            MatchCollection mcblue = regexblue.Matches(rtbCodeViewer.Text);

            Regex regexlightblue = new Regex(ColorSetting.strTargetlightblue); //Regex 사용하여 특정문자 찾기
            MatchCollection mclightblue = regexlightblue.Matches(rtbCodeViewer.Text);

            Regex regexgreen = new Regex(ColorSetting.strTargetGreen); //Regex 사용하여 특정문자 찾기
            MatchCollection mcgreen = regexgreen.Matches(rtbCodeViewer.Text);

            Regex regexred = new Regex(ColorSetting.strTargetRed); //Regex 사용하여 특정문자 찾기
            MatchCollection mcred = regexred.Matches(rtbCodeViewer.Text);

            int iCursorPosition = rtbCodeViewer.SelectionStart;


            #region 색상 결정
            foreach (Match m in mcgray)
            {
                int iStartIdx = m.Index;
                int iStopIdx = m.Length;
                rtbCodeViewer.Select(iStartIdx, iStopIdx);
                rtbCodeViewer.SelectionColor = Color.Gray;
                rtbCodeViewer.SelectionStart = iCursorPosition;
                rtbCodeViewer.SelectionColor = Color.Black;
            }
            foreach (Match m in mcblue)
            {
                int iStartIdx = m.Index;
                int iStopIdx = m.Length;
                rtbCodeViewer.Select(iStartIdx, iStopIdx);
                rtbCodeViewer.SelectionColor = Color.Blue;
                rtbCodeViewer.SelectionStart = iCursorPosition;
                rtbCodeViewer.SelectionColor = Color.Black;
            }
            foreach (Match m in mclightblue)
            {
                int iStartIdx = m.Index;
                int iStopIdx = m.Length;
                rtbCodeViewer.Select(iStartIdx, iStopIdx);
                rtbCodeViewer.SelectionColor = Color.LightBlue;
                rtbCodeViewer.SelectionStart = iCursorPosition;
                rtbCodeViewer.SelectionColor = Color.Black;
            }
            foreach (Match m in mcgreen)
            {
                int iStartIdx = m.Index;
                int iStopIdx = m.Length;
                rtbCodeViewer.Select(iStartIdx, iStopIdx);
                rtbCodeViewer.SelectionColor = Color.Green;
                rtbCodeViewer.SelectionStart = iCursorPosition;
                rtbCodeViewer.SelectionColor = Color.Black;
            }
            foreach (Match m in mcred)
            {
                int iStartIdx = m.Index;
                int iStopIdx = m.Length;
                rtbCodeViewer.Select(iStartIdx, iStopIdx);
                rtbCodeViewer.SelectionColor = Color.Red;
                rtbCodeViewer.SelectionStart = iCursorPosition;
                rtbCodeViewer.SelectionColor = Color.Black;
            }
            #endregion
        }
        #endregion

        #region URL
        private void btnUrlGo_Click(object sender, EventArgs e)
        {
            string url = combUrl.Text;
            wbMain.Navigate(url);


        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 위에 입력해놓은 함수 불러서 페이지 이동 시키기
                btnUrlGo_Click(sender, e);
            }
        }

        private void btnBefore_Click_1(object sender, EventArgs e)
        {
            // wbMain의 페이지 뒤로가기
            wbMain.GoBack();
        }

        private void btnForward_Click_1(object sender, EventArgs e)
        {
            // wbMain의 페이지 앞으로가기
            wbMain.GoForward();
        }

        private void rtbCodeViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                combUrl.Text = "https://www.google.com/search?q=" + rtbCodeViewer.SelectedText;
                wbMain.Navigate(combUrl.Text);
            }
        }
        private void btnUrlHome_Click_1(object sender, EventArgs e)
        {
            url = "https://www.google.co.kr/";
            wbMain.Navigate(url);
        }
        private void wbMain_Navigated_1(object sender, WebBrowserNavigatedEventArgs e)
        {
            combUrl.Text = wbMain.Url.AbsoluteUri.ToString();
        }

        #endregion

        #region Ref_Document
        private void FindFile_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            String file_path = null;
            openFileDialog1.InitialDirectory = FullPath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_path = openFileDialog1.FileName;       //선택된 파일의 풀 경로를 불러와 저장

                textBox1.Text = "file:\\\\\\" + file_path;
            }

            string url = textBox1.Text;
            wbSub.Navigate(url);
        }
        #endregion

        #region Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(textbRootPath.Text).Parent;
                removeSelectedNodes(treeView2.Nodes, directoryInfo);
                FIleDelete();
            }
        }
        void removeSelectedNodes(TreeNodeCollection nodes, DirectoryInfo directory)
        {
            try
            {
                foreach (TreeNode node in nodes)
                {
                    directory = new DirectoryInfo(directory.FullName + "\\" + node.Text);
                    if (node.Checked)
                    {
                        nodes.Remove(node);
                        Directory.Delete(directory.FullName, true);
                    }
                    else
                    {
                        removeSelectedNodes(node.Nodes, directory);
                    }
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine("ViewDirectoryList : " + ex.Message);
            }
        }
        private void FIleDelete()
        {
            try
            {
                string subPath = madeFolderFullPath();

                if (subPath == null)
                {
                    MessageBox.Show("경로가 없습니다.");
                    return;
                }

                if (subPath.Contains("."))
                {
                    File.Delete(subPath);
                }
                else
                {
                    Directory.Delete(subPath);
                }
                MessageBox.Show("삭제되었습니다");
            }
            catch (Exception ex)
            {
                MessageBox.Show("The process failed: {0}", ex.Message);
            }
        }


        #endregion

        #region FindFolder
        //private void brnFindFolder_Click(object sender, EventArgs e)
        //{
        //	FolderBrowserDialog fbd = new FolderBrowserDialog();

        //	if (fbd.ShowDialog() == DialogResult.OK)
        //	{
        //		m_curPath = fbd.SelectedPath;
        //		//Console.WriteLine(m_curPath);
        //		label1.Text = m_curPath;

        //		ViewDirectoryList(m_curPath);
        //	}
        //}
        //하위폴더&파일 리스트형태로 보여주기
        private void ViewDirectoryList(string path)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();

            string curPath = path;

            Console.WriteLine(path.IndexOf("Root\\"));
            if (path.IndexOf("Root\\") == 0)
            {
                curPath = path.Substring(path.IndexOf("\\") + 1);
                label1.Text = (curPath.Length > 4) ? curPath.Remove(curPath.IndexOf("\\") + 1, 1) : curPath;
                m_curPath = label1.Text;
            }
            else
            {
                label1.Text = path;
                m_curPath = path;
            }

            try
            {
                listView1.Items.Clear();

                string[] directories = Directory.GetDirectories(curPath);

                foreach (string directory in directories)
                {
                    DirectoryInfo info = new DirectoryInfo(directory);
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        info.Name, info.LastWriteTime.ToString(), "파일 폴더", ""
                    });
                    listView1.Items.Add(item);
                }

                string[] files = Directory.GetFiles(curPath);

                foreach (string file in files)
                {
                    FileInfo info = new FileInfo(file);
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        info.Name, info.LastWriteTime.ToString(), info.Extension, ((info.Length/1000)+1).ToString()+"KB"
                    });
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ViewDirectoryList : " + ex.Message);
            }
        }
        #endregion

        #region 이벤트
        //URL_ADD

        ////private void rtbCodeViewer_MouseDown(object sender, MouseEventArgs e)
        //{
        //	if (e.Button == MouseButtons.Right)
        //	{
        //		combUrl.Text = "https://www.google.com/search?q=" + rtbCodeViewer.SelectedText;
        //		wbMain.Navigate(combUrl.Text);
        //	}
        //}

        private void rtbCodeViewer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //db.Open();
            //List<CODE> products = db.KeyWordToURL(rtbCodeViewer.SelectedText);

            //combUrl.Items.Add(products.Count.ToString());

            wbMain.Navigate(combUrl.Text);
            //db.Dispose();
        }

        #endregion
    }

    #region Search
    //private void btnSearch_Click(object sender, EventArgs e)
    //{
    //	if (m_thread != null && m_thread.IsAlive)
    //		m_thread.Abort();

    //	m_curPath = label1.Text;
    //	string searchFiles = textBox2.Text;
    //	DirectoryInfo rootDirInfo = new DirectoryInfo(m_curPath);

    //	m_thread = new Thread(delegate ()
    //	{
    //		WalkDirectoryTree(rootDirInfo, searchFiles);
    //	});
    //	m_thread.Start();
    //}
    //public void WalkDirectoryTree(DirectoryInfo dirInfo, string searchFiles)
    //{
    //	listView1.Items.Find(searchFiles, true);

    //	FileInfo[] files = null;
    //	DirectoryInfo[] subDirs = null;

    //	try
    //	{
    //		files = dirInfo.GetFiles(searchFiles + "*.*");
    //	}
    //	catch (UnauthorizedAccessException e)
    //	{
    //		Console.WriteLine(e.Message);
    //	}
    //	catch (DirectoryNotFoundException e)
    //	{
    //		Console.WriteLine(e.Message);
    //	}

    //	if (files != null)
    //	{
    //		DirectoryInfo tempDirInfo = new DirectoryInfo(m_curPath);

    //		if (dirInfo.ToString() == tempDirInfo.ToString())
    //			listView1.Items.Clear();

    //		foreach (FileInfo fi in files)
    //		{
    //			ListViewItem item = new ListViewItem(new string[]
    //			{
    //				fi.FullName, fi.LastWriteTime.ToString(), fi.Extension, ((fi.Length/1000)+1).ToString()+"KB"
    //			});
    //			listView1.Items.Add(item);
    //		}

    //		subDirs = dirInfo.GetDirectories();
    //		foreach (DirectoryInfo di in subDirs)
    //		{
    //			WalkDirectoryTree(di, searchFiles);
    //		}
    //	}
    //}
    #endregion

    #region 트리뷰 노드 선택 이벤트
    //private void Treeview_MouseDown(object sender, MouseEventArgs e)
    //{
    //	TreeViewHitTestInfo tvHit = treeView2.HitTest(e.Location); //마우스가 클릭된 위치 구하기 
    //	//ListView를 사용할 경우 //ListViewHitTestInfo lvHit = GroupListView.HitTest(e.Location);

    //	if (e.Button == MouseButtons.Right)
    //	{
    //		ContextMenu ctms = new ContextMenu();
    //		Point mousePoint = new Point(e.X+50, e.Y+100);

    //		if (tvHit.Location == TreeViewHitTestLocations.None) //마우스가 클릭된 위치가 빈 공간일때 == 노드가 아닐때
    //			MessageBox.Show(" no Node click");

    //		if (tvHit.Location == TreeViewHitTestLocations.Label) //마우스가 클릭된 위치가 노드
    //			{
    //				MessageBox.Show("Node click");
    //			//ctms.MenuItems.Add("Create URL Table");
    //			//ctms.Show(this, mousePoint);

    //		}


    //	}
    //}
    #endregion

}
