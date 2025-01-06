using PBAnaly.Assist;
using PBAnaly.LoginCommon;
using ScottPlot.Colormaps;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.UI
{
    public partial class SystemSettingForm : Form
    {
        public SystemSettingForm()
        {
            InitializeComponent();

            pnl_MainMenu.BringToFront();

            // 设置窗体的启动位置为屏幕的中心
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                                 (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            
            GlobalData.PropertyChanged += OnGlobalDataPropertyChanged;

            if (GlobalData.GetProperty("Language") == "Chinese")
            {
                SetLanguage("zh-CN");
            }
            else
            {
                SetLanguage("en-US");
            }
        }

        UserManageForm UserForm;

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



        private void SystemSettingForm_Load(object sender, EventArgs e)
        {
            //加载系统参数
            LoadSystemParam();

            UserForm = new UserManageForm();
            UserForm.Dock = DockStyle.Fill;
            UserForm.Location = new Point(0, 0);
            UserForm.TopLevel = false;
            tab_UserManage1.Controls.Add(UserForm);
            UserForm.InitUser();
            UserForm.Show();

            if (UserManage.IsLogined)
            {
                if (UserManage.LogionUser.Role == UserRole.SuperAdministrator)
                {
                    tab_UserManage1.Parent = tab_Main;
                }
                else
                {
                    tab_UserManage1.Parent = null;
                }
            }
        }

        #region OnGlobalDataPropertyChanged 处理全局属性更改事件
        /// <summary> 
        /// 处理全局属性更改事件
        /// </summary>
        /// <param name="name">发生变化的属性名</param>
        /// <param name="value">更改的属性值</param>
        private void OnGlobalDataPropertyChanged(string name, string value)
        {
            switch (name)
            {
                case "Language":
                    if (GlobalData.GetProperty("Language") == "Chinese")
                    {
                        SetLanguage("zh-CN");
                    }
                    else
                    {
                        SetLanguage("en-US");
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 中英文切换
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

        #region btn_Close_Click 窗口关闭按钮
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region LoadSystemParam 加载系统参数
        /// <summary>
        /// 加载系统参数
        /// </summary>
        private void LoadSystemParam()
        {
            try
            {
                cbx_System_Language.Text = GlobalData.GetProperty("Language") == "English" ? "English" : "简体中文";
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region btn_SystemSetting_Click 系统设置按钮
        private void btn_SystemSetting_Click(object sender, EventArgs e)
        {
            OperatingRecord.CreateRecord("系统设置按钮", "点击事件");
            this.tab_Main.SelectedIndex = 0;

            SetMainMenuButtonCilkeColor(((Button)sender).Name);
        }
        #endregion

        #region btn_UserManager_Click 用户管理按钮
        private void btn_UserManager_Click(object sender, EventArgs e)
        {
            OperatingRecord.CreateRecord("用户管理按钮", "点击事件");
            this.tab_Main.SelectedIndex = 1;

            SetMainMenuButtonCilkeColor(((Button)sender).Name);
        }
        #endregion

        #region  SetMainMenuButtonCilkeColor 主菜单中按钮点击之后，设置按钮的前景色和背景色
        /// <summary>
        /// 主菜单中按钮点击之后，设置按钮的前景色和背景色
        /// </summary>
        /// <param name="strBtnName">点击的按钮的名称</param>
        private void SetMainMenuButtonCilkeColor(string strBtnName)
        {
            foreach (Control control in pnl_MainMenu.Controls)
            {
                if (control.Name == pnl_MainMenu.Name)
                {
                    continue;
                }
                else if (control.Name == strBtnName)
                {
                    control.BackColor = Color.White;
                    control.ForeColor = Color.Black;
                }
                else
                {
                    control.BackColor = ColorTranslator.FromHtml("30, 56, 83");
                    control.ForeColor = Color.White;
                }
            }
        }
        #endregion

        #region btn_save_ZH_US_Click 系统参数保存按钮
        private void btn_save_ZH_US_Click(object sender, EventArgs e)
        {
            try
            {
                if(GlobalData.GetProperty("Language") != cbx_System_Language.Text)
                {
                    OperatingRecord.CreateRecord("系统参数保存按钮",
                        $"系统语言由{GlobalData.GetProperty("Language")}修改为：{cbx_System_Language.Text}");
                    if (cbx_System_Language.Text == "English")
                    {
                        SetLanguage("en-US");
                        GlobalData.SetProperty("Language", "English");
                    }
                    else
                    {
                        SetLanguage("zh-CN");
                        GlobalData.SetProperty("Language", "Chinese");
                    }
                }


                MessageBox.Show("保存成功");
    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败，原因：{ex.Message.ToString()}");
            }

        }
        #endregion
    }
}
