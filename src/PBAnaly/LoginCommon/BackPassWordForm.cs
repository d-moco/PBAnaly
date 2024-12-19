using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.LoginCommon
{
    public partial class BackPassWordForm : Form
    {
        public BackPassWordForm()
        {
            InitializeComponent();

            // 设置窗体的启动位置为屏幕的中心
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                                 (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        #region =====重写WndPoc方法 无边框窗体更改大小及拖动=========
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case 0x0084:
                        base.WndProc(ref m);
                        System.Drawing.Point vPoint = new System.Drawing.Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        if (vPoint.X <= 5)
                        {
                            if (vPoint.Y <= 5)
                            {
                                m.Result = (IntPtr)HTTOPLEFT;
                            }
                            else if (vPoint.Y >= ClientSize.Height - 5)
                            {
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            }
                            else
                            {
                                m.Result = (IntPtr)HTLEFT;
                            }
                        }
                        else if (vPoint.X >= ClientSize.Width - 5)
                        {
                            if (vPoint.Y <= 5)
                            {
                                m.Result = (IntPtr)HTTOPRIGHT;
                            }
                            else if (vPoint.Y >= ClientSize.Height - 5)
                            {
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                            }
                            else
                            {
                                m.Result = (IntPtr)HTRIGHT;
                            }
                        }
                        else if (vPoint.Y <= 5)
                        {
                            m.Result = (IntPtr)HTTOP;
                        }
                        else if (vPoint.Y >= ClientSize.Height - 5)
                        {
                            m.Result = (IntPtr)HTBOTTOM;
                        }
                        break;

                    case 0x0201://鼠标左键按下的消息 用于实现拖动窗口功能
                        m.Msg = 0x00A1;//更改消息为非客户区按下鼠标
                        m.LParam = IntPtr.Zero;//默认值
                        m.WParam = new IntPtr(2);//鼠标放在标题栏内
                        base.WndProc(ref m);
                        break;

                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            catch (Exception ex)
            {
                //showERRORMsg($"重写WndPoc方法 无边框窗体更改大小及拖动方法发生异常，原因：{ex.Message}");
            }

        }
        #endregion

        private void txt_UserName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_broblem.Text = "";

                if (UserManage.UsersKeyValuePairs.ContainsKey(txt_UserName.Text))
                {
                    txt_broblem.Text = UserManage.UsersKeyValuePairs[txt_UserName.Text].PasswordQuestion;
                }
               
            }
            catch (Exception)
            {

            }
        }

        private void btn_Frin_Password_Click(object sender, EventArgs e)
        {
            try
            {
                string UserName = txt_UserName.Text;
                string Password = txt_Password.Text;
                string Broblem = txt_broblem.Text;
                string Answer = "";
                
                if (string.IsNullOrEmpty(txt_UserName.Text)) { MessageBox.Show("Please enter your username!!"); return; }
                if (string.IsNullOrEmpty(txt_answer.Text)) { MessageBox.Show("Please enter the security answer!!"); return; }

                Answer = UserManage.UsersKeyValuePairs[txt_UserName.Text].QuestionAnswer;

                if (Answer != txt_answer.Text) { MessageBox.Show("Incorrect answer!!"); return; }

                txt_Password.Text = UserManage.UsersKeyValuePairs[txt_UserName.Text].Password;

            }
            catch (Exception)
            {

            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_UserName_Click(object sender, EventArgs e)
        {
            txt_UserName.Text = "";
        }
    }
}
