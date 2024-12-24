using MaterialSkin;
using MaterialSkin.Controls;
using PBAnaly.Assist;
using ReaLTaiizor.Manager;
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
    public partial class LoginForm : Form
    {
        private MaterialSkin.MaterialSkinManager materialSkinManager;
        public bool isOK = false;

        public LoginForm()
        {
            InitializeComponent();

            // 设置窗体的启动位置为屏幕的中心
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                                 (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
           

            //如果上一次登录的时候是勾选了记住本次登录，那么就像用户名和密码填充到本文控件
            if (UserManage.LastLoginUser.Count > 0)
            {
                foreach (var item in UserManage.LastLoginUser.Values)
                {
                    if (item.Remember == "1")
                    {
                        txt_UserName.Text= item.UserName;
                        txt_Password.Text = item.Password;
                        cb_Remember.Checked = true;
                    }
                }
            }
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

        #region ProcessCmdKey 是一个用于处理按键命令的方法，在自定义控件中可以重写它来捕获键盘输入（包括 Enter 键）
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 检查是否按下了 Enter 键
            if (keyData == Keys.Enter)
            {
                btn_Login_Click(null, null);
                return true; // 表示该按键已处理
            }
            return base.ProcessCmdKey(ref msg, keyData);
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

        #region btn_Login_Click 登录按钮
        private void btn_Login_Click(object sender, EventArgs e)
        {
            string UserName = txt_UserName.Text;
            string Password = txt_Password.Text;
            string Remember = cb_Remember.Checked ? "1" : "0";
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("User ID or Password is empty ,Please Check!!", "Login Error");
                return;
            }


            if (UserManage.UsersKeyValuePairs.ContainsKey(UserName))
            {
                User user = UserManage.UsersKeyValuePairs[UserName];
                if (Password == UserManage.UsersKeyValuePairs[UserName].Password)
                {
                    UserManage.IsLogined = true;//系统已登录
                    UserManage.SetLogionUser(user);//当前登录的用户
                    //将本次登录更新为上一次登录的用户插入数据库，方便下次登录的时候查看
                    UserManage.UpDateLastUser(UserName, Password, Remember);
                    isOK = true;

                    OperatingRecord.CreateRecord("登录按钮", "用户登录");

                    Close();
                }
                else { MessageBox.Show("Password is incorrect, please re-enter"); return; }
            }
            else { MessageBox.Show("User name is incorrect, please re-enter"); }

        }
        #endregion

        #region btn_Close_Click 关闭按钮
        private void btn_Close_Click(object sender, EventArgs e)
        {
            isOK = false;

            Close();
        }
        #endregion

        private void SIGNIN_materialButton_Click(object sender, EventArgs e)
        {
            RegisterFrom register = new RegisterFrom();
            register.ShowDialog();

        }

        private void lab_forget_pass_MouseEnter(object sender, EventArgs e)
        {
            // 鼠标进入时，样式变为手指
            Cursor = Cursors.Hand;
            lab_forget_pass.Font = new Font("微软雅黑", 10);
            lab_forget_pass.ForeColor = Color.LimeGreen;
        }

        private void lab_forget_pass_MouseLeave(object sender, EventArgs e)
        {
            // 鼠标离开时，样式恢复为默认
            Cursor = Cursors.Default;
            lab_forget_pass.Font = new Font("微软雅黑", 9);
            lab_forget_pass.ForeColor = Color.White;
        }

        private void lab_forget_pass_Click(object sender, EventArgs e)
        {
            BackPassWordForm backPass = new BackPassWordForm();
            backPass.ShowDialog();
        }
    }
}
