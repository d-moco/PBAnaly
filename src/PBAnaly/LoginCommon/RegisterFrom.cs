using PBAnaly.Assist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.LoginCommon
{
    public partial class RegisterFrom : Form
    {
        public RegisterFrom()
        {
            InitializeComponent();


            // 设置窗体的启动位置为屏幕的中心
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                                 (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);

            if (GlobalData.GetProperty("Language") == "Chinese")
            {
                SetLanguage("zh-CN");
            }
            else
            {
                SetLanguage("en-US");
            }
        }

        #region 中英文切换模块
        ResourceManager resourceManager;
        private void SetLanguage(string cultureCode)
        {
            resourceManager = new ResourceManager("PBAnaly.Properties.Resources", typeof(MainForm).Assembly);

            // 设置当前线程的文化信息
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            // 更新所有控件的文本
            UpdateControlsText();
        }

        // 更新所有控件的文本
        private void UpdateControlsText()
        {
            //// 遍历所有控件并更新文本
            foreach (Control control in this.Controls)
            {
                UpdateControlText(control);
            }
        }
        // 更新单个控件的文本
        private void UpdateControlText(Control control)
        {
            //// 直接通过控件的 Name 属性获取资源字符串
            string resourceText = resourceManager.GetString(control.Name);
            if (!string.IsNullOrEmpty(resourceText))
            {
                control.Text = resourceText;
            }

            // 如果控件包含子控件，则递归更新子控件
            foreach (Control subControl in control.Controls)
            {
                UpdateControlText(subControl);
            }
        }
        #endregion

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

        #region btn_register_Click 注册按钮
        private void btn_register_Click(object sender, EventArgs e)
        {
            try
            {
                string UserName         = txt_UserName.Text;
                string Password         = txt_Password.Text;
                string Enter_Password   = txt_enter_password.Text;
                string Broblem          = txt_broblem.Text;
                string Answer           = txt_answer.Text;

                if (UserManage.UsersKeyValuePairs.ContainsKey(UserName)) { MessageBox.Show("The current user already exists!!"); return; }
                if (string.IsNullOrEmpty(UserName)){MessageBox.Show("Please enter your username!!");return;}
                if (string.IsNullOrEmpty(Password)){MessageBox.Show("Please enter your Password!!");return;}
                if (string.IsNullOrEmpty(Enter_Password)){MessageBox.Show("Please enter a second password!!");return;}
                if (string.IsNullOrEmpty(Broblem)){MessageBox.Show("Please enter the security question, in case you forget" +
                    " the password, you can report the question to retrieve!!");return;}
                if (string.IsNullOrEmpty(Answer)){MessageBox.Show("Please enter the security answer!!");return;}
                if(Password!= Enter_Password) { MessageBox.Show("The two passwords are different!!"); return; }
                
                User user = new User();
                user.Name = UserName;
                user.Password= Password;
                user.Role = UserRole.Operator;
                user.PasswordQuestion = Broblem;
                user.QuestionAnswer = Answer;
                user.CreatedDate=DateTime.Now;

                if (UserManage.RegisterUser(user))
                {
                    txt_UserName.Text = "";
                    txt_Password.Text = "";
                    txt_enter_password.Text = "";
                    txt_broblem.Text = "";
                    txt_answer.Text = "";
                    
                    MessageBox.Show("注册成功");
                }
                else
                {
                    MessageBox.Show("注册失败");

                }

            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        private void txt_UserName_Click(object sender, EventArgs e)
        {
            txt_UserName.Text = "";
        }

        private void txt_Password_Click(object sender, EventArgs e)
        {
            txt_Password.Text = "";
        }

        private void txt_enter_password_Click(object sender, EventArgs e)
        {
            txt_enter_password.Text = "";
        }

        private void txt_broblem_Click(object sender, EventArgs e)
        {
            txt_broblem.Text = "";
        }

        private void txt_answer_Click(object sender, EventArgs e)
        {
            txt_answer.Text = "";
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
