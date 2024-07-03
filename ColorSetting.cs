using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeManagement
{
    public partial class ColorSetting : Form
    {
        public event EventHandler CloseColorSetting = null;

        public static string character;
        public static Color color = Color.Gray;

        //단어 색상 추가시 strTarget색상 문자열에 추가됨
        //프로그램 시작시 데이터베이스에서 불러오기
        public static string strTargetGray = @"Gray";
        public static string strTargetBlue = @"Blue";
        public static string strTargetlightblue = @"lightblue";
        public static string strTargetGreen = @"Green";
        public static string strTargetRed = @"Red";

        public ColorSetting()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(ColorSetting_FormClosing);
            comboBox2.SelectedIndex = 0;
        }

        #region 검색
        //단어 검색
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (strTargetGray.Contains("|" + textBox1.Text))
            {
                MessageBox.Show("할당된 색상 : Gray\n" + strTargetGray);
                return;
            }
            else if (strTargetBlue.Contains("|" + textBox1.Text))
            {
                MessageBox.Show("할당된 색상 : Blue\n" + strTargetBlue);
                return;
            }
            else if (strTargetlightblue.Contains("|" + textBox1.Text))
            {
                MessageBox.Show("할당된 색상 : blue\n" + strTargetlightblue);
                return;
            }
            else if (strTargetGreen.Contains("|" + textBox1.Text))
            {
                MessageBox.Show("할당된 색상 : Green\n" + strTargetGreen);
                return;
            }
            else if (strTargetRed.Contains("|" + textBox1.Text))
            {
                MessageBox.Show("할당된 색상 : Red\n" + strTargetRed);
                return;
            }
            else
                MessageBox.Show("해당 문자는 없습니다");
        }
        #endregion

        #region 삭제
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (strTargetGray.Contains("|" + textBox1.Text))
            {
                strTargetGray = strTargetGray.Replace("|" + textBox1.Text, "|");
                MessageBox.Show("제거 완료");
                return;
            }
            else if (strTargetBlue.Contains("|" + textBox1.Text))
            {
                strTargetBlue = strTargetBlue.Replace("|" + textBox1.Text, "|");
                MessageBox.Show("제거 완료");
                return;
            }
            else if (strTargetlightblue.Contains("|" + textBox1.Text))
            {
                strTargetlightblue = strTargetlightblue.Replace("|" + textBox1.Text, "|");
                MessageBox.Show("제거 완료");
                return;
            }
            else if (strTargetGreen.Contains("|" + textBox1.Text))
            {
                strTargetGreen = strTargetGreen.Replace("|" + textBox1.Text, "|");
                MessageBox.Show("제거 완료");
                return;
            }
            else if (strTargetRed.Contains("|" + textBox1.Text))
            {
                strTargetRed = strTargetRed.Replace("|" + textBox1.Text, "|");
                MessageBox.Show("제거 완료");
                return;
            }
            else
                MessageBox.Show("해당 문자는 없습니다");

        }
        #endregion

        #region 등록
		private void button5_Click_1(object sender, EventArgs e)
		{
            if (strTargetGray.Contains(textBox1.Text))
            {
                MessageBox.Show(textBox1.Text + " : 해당 문자는 이미 Gray 색상에 등록되어있습니다");
                return;
            }
            else if (strTargetBlue.Contains(textBox1.Text))
            {
                MessageBox.Show(textBox1.Text + " : 해당 문자는 이미 Blue 색상에 등록되어있습니다");
                return;
            }
            else if (strTargetlightblue.Contains(textBox1.Text))
            {
                MessageBox.Show(textBox1.Text + " : 해당 문자는 이미 LightBlue 색상에 등록되어있습니다");
                return;
            }
            else if (strTargetGreen.Contains(textBox1.Text))
            {
                MessageBox.Show(textBox1.Text + " : 해당 문자는 이미 Green 색상에 등록되어있습니다");
                return;
            }
            else if (strTargetRed.Contains(textBox1.Text))
            {
                MessageBox.Show(textBox1.Text + " : 해당 문자는 이미 Red 색상에 등록되어있습니다");
                return;
            }

            character = textBox1.Text;
            switch (comboBox2.SelectedIndex)
            {
                case 0: color = Color.FromName(comboBox2.SelectedItem as string); strTargetGray += "|" + character; break;
                case 1: color = Color.FromName(comboBox2.SelectedItem as string); strTargetBlue += "|" + character; break;
                case 2: color = Color.FromName(comboBox2.SelectedItem as string); strTargetlightblue += "|" + character; break;
                case 3: color = Color.FromName(comboBox2.SelectedItem as string); strTargetGreen += "|" + character; break;
                case 4: color = Color.FromName(comboBox2.SelectedItem as string); strTargetRed += "|" + character; break;
            }
            //데이터베이스에 character(문자), color(색상)전송하기
            MessageBox.Show("등록했습니다");

        }


        #endregion

        #region 창 닫힘
        // 확인
        private void button1_Click_1(object sender, EventArgs e)
        {
            Close_ColorSetting();
        }

        // 취소
        private void button2_Click_1(object sender, EventArgs e)
        {
            Close_ColorSetting();
        }

        private void ColorSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 확인 메시지 박스를 표시합니다.
            DialogResult result = MessageBox.Show("정말로 닫으시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 사용자가 'No'를 선택하면 닫히지 않도록 합니다.
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            if (result == DialogResult.Yes)
            {
                Close_ColorSetting();
            }
        }

        private void Close_ColorSetting()
        {
            if (CloseColorSetting != null)
                CloseColorSetting(this, new EventArgs());
            this.Close();
        }
        #endregion
    }

}
