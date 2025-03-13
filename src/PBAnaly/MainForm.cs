using Aspose.Pdf;
using MaterialSkin;
using MaterialSkin.Controls;
using OpenCvSharp.Flann;
using OpenTK;
using PBAnaly.Assist;
using PBAnaly.LoginCommon;
using PBAnaly.Module;
using PBAnaly.Properties;
using PBAnaly.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using Resources = PBAnaly.Properties.Resources;

namespace PBAnaly
{
    public partial class MainForm : MaterialForm
    {
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        private MaterialSkinManager materialSkinManager;
        private SettingForm settingForm;
        private LoginForm loginForm;
        private LogForm logForm;
        private TableLayoutPanel data_tab = null;
        private TableLayoutPanel data_right_bar = null;
        private bool bioanalyBool = false;
        private int data_col = 2;
        private int data_row = 2;
        private int FormGenerate_X;
        private int FormGenerate_Y;

        private string InnerUserID = "";

        private string color_bar = "YellowHot";
        System.Windows.Forms.TableLayoutPanel tlp_main_images;

        private Dictionary<string ,BioanalysisMannage> bioanalysisMannages = new Dictionary<string, BioanalysisMannage>();
        private Dictionary<string, LanesMannage> lanesMannages = new Dictionary<string, LanesMannage>();
        private Dictionary<string, ColonyMannage> colonysMannages = new Dictionary<string, ColonyMannage>();
        private List<string> bioanalyName = new List<string>();
        private List<string> lanesName = new List<string>();
        bool isRun = false;
        private Thread thread;
       
        public MainForm(LoginForm InnerloginForm, Autholity autholity, string UserID)
        {
            InitializeComponent();
            //InnerloginForm.Close();
            loginForm = InnerloginForm;
            loginForm.Visible = false;
            //loginForm.Close();
            loginForm = null;
            

            UIInit();

            InnerUserID = UserID;
            FormGenerate_X = 0;
            FormGenerate_Y = 0;
           // initPanel();
        }
        public MainForm()
        {
            InitializeComponent();

            LoginCommon.LoginForm loginForm = new LoginCommon.LoginForm();
            loginForm.ShowDialog();
            if (!loginForm.isOK)
            {
                this.FormClosing -= new FormClosingEventHandler(MainForm_FormClosing);
                Close();
            }

            loginForm.Hide();

            GlobalData.PropertyChanged += OnGlobalDataPropertyChanged;
            UserManage.LogionUserChanged += OnLogionUserChanged;
            
            InitAccessControls();
            LoadAccessFile();
            OnLogionUser();

            UIInit();

            FormGenerate_X = 0;
            FormGenerate_Y = 0;

            if (GlobalData.GetProperty("Language") == "English")
            {
                SetLanguage("en-US");
            }
            else
            {
                SetLanguage("zh-CN");
              
            }
            // initPanel();
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

        #region 重新梳理权限控制，控件的权限可通过管理员进行配置
        /// <summary>
        /// 用于权限控制，将所有要管控的控件保存到mControls中
        /// </summary>
        private Control[] mControls;

        /// <summary>
        /// 初始化权限控件集合
        /// </summary>
        private void InitAccessControls()
        {
            mControls = new Control[] 
            {
                materialButton_setting,         //0、系统设置
                materialButton_curveimage,      //1、泳道波形图
                materialButton_analyzedata,     //2、分析数据
                materialButton_outimage,        //3、导出图像
                materialButton_LoadData,        //4、加载数据
                materialButton_imageProcess,    //5、图像处理
                materialButton_acidAnalyze,     //6、泳道分析
                materialButton_roiAnalyze,      //7、ROIs分析
                materialButton_miniAnalyze,     //8、微孔版分析
                mb_colonyCount,       //9、菌落计数
                materialButton_correction      //10、蛋白归一化
            };
        }

        #region LoadAccessFile 加载管理控件访问权限的文件，如果文件不存在，就根据界面的设置的控件创建一个
        /// <summary>
        /// 加载管理控件访问权限的文件，如果文件不存在，就根据界面的设置的控件创建一个
        /// </summary>
        private void LoadAccessFile()
        {
            try
            {
                if (!File.Exists("AccessControl.xml"))
                {
                    CreatAccessControlFlie();
                }
                else
                {
                    FileStream fs = new FileStream("AccessControl.xml", FileMode.Open);
                    XmlSerializer xs = new XmlSerializer(typeof(List<AccessItem>));
                    AccessControl.AccessItems = xs.Deserialize(fs) as List<AccessItem>;
                    fs.Close();

                    if(AccessControl.AccessItems.Count!= mControls.Length)
                    {
                        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string xmlFileName = "AccessControl.xml";
                        // 拼接出完整的文件路径
                        string filePath = Path.Combine(currentDirectory, xmlFileName);
                        // 删除文件
                        File.Delete(filePath);

                        CreatAccessControlFlie();

                    }
                }
            }
            catch (Exception)
            {

            }
            
        }
        #endregion

        #region CreatAccessControlFlie 创建管理控件权限访问的文件夹
        /// <summary>
        /// 创建管理控件权限访问的文件夹
        /// </summary>
        private void CreatAccessControlFlie()
        {
            try
            {
                // 创建XML根节点
                XElement root = new XElement("ArrayOfItem");
                for (int i = 0; i < mControls.Length; i++)
                {


                    XElement item = new XElement("item",
                             new XAttribute("Id", i),
                             new XAttribute("Operator", "false"),
                             new XAttribute("Engineer", "false"),
                             new XAttribute("Administrator", "true"),
                             new XAttribute("SuperAdministrator", "true"),
                             new XAttribute("Disible", mControls[i].Text)
                         );
                    root.Add(item);
                }

                // 保存XML到文件
                string filePath = "AccessControl.xml";
                root.Save(filePath);

                AccessControl.AccessItems = new List<AccessItem>();

                FileStream fs = new FileStream("AccessControl.xml", FileMode.Open);
                XmlSerializer xs = new XmlSerializer(typeof(List<AccessItem>));
                AccessControl.AccessItems = xs.Deserialize(fs) as List<AccessItem>;
                fs.Close();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region OnLogionUserChanged 处理登录用户更改事件
        /// <summary>
        /// 处理登录用户更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLogionUserChanged(object sender, EventArgs e)
        {
            if (UserManage.IsLogined)
            {
                switch (UserManage.LogionUser.Role)
                {
                    case UserRole.Operator:
                        SetOperatorRole();
                        break;
                    case UserRole.Engineer:
                        SetEngineerRole();
                        break;
                    case UserRole.Administrator:
                        SetAdministratorRole();
                        break;
                    case UserRole.SuperAdministrator:
                        SetSuperAdministratorRole();
                        break;
                }
            }
            else
            {
                CloseControlEnabled();
            }
        }

        private void OnLogionUser()
        {
            if (UserManage.IsLogined)
            {
                switch (UserManage.LogionUser.Role)
                {
                    case UserRole.Operator:
                        SetOperatorRole();
                        break;
                    case UserRole.Engineer:
                        SetEngineerRole();

                        break;
                    case UserRole.Administrator:
                        SetAdministratorRole();
                        break;
                    case UserRole.SuperAdministrator:
                        SetSuperAdministratorRole();
                        break;
                }
            }
            else
            {
                CloseControlEnabled();
            }
        }

        #endregion

        #region CloseControlEnabled 关闭控件权限，在未登录时使用
        /// <summary>
        /// 关闭控件权限，在未登录时使用
        /// </summary>
        private void CloseControlEnabled()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(CloseControlEnabled));
            }
            else
            {
                SetControlsEnabled(false);
            }
        }
        #endregion

        #region SetControlsEnabled 设置控件是否可以对用户交互作出响应。
        /// <summary>
        /// 设置控件是否可以对用户交互作出响应。
        /// </summary>
        /// <param name="isEnabled">true-可以对用户交互作出响应；false-不可以对用户交互作出响应。</param>
        public void SetControlsEnabled(bool isEnabled)
        {
            for (int index = 0; index < mControls.Length; index++)
            {
                mControls[index].Enabled = false;
            }
        }
        #endregion

        #region SetOperatorRole 设置操作员权限
        /// <summary>
        /// 设置操作员权限
        /// </summary>
        private void SetOperatorRole()
        {
            for (int index = 0; index < mControls.Length; index++)
            {
                mControls[index].Enabled = AccessControl.AccessItems[index].Operator;
            }
        }
        #endregion

        #region SetEngineerRole 设置工程师权限
        /// <summary>
        /// 设置工程师权限
        /// </summary>
        private void SetEngineerRole()
        {
            for (int index = 0; index < mControls.Length; index++)
            {
                mControls[index].Enabled = AccessControl.AccessItems[index].Engineer;
            }
        }
        #endregion

        #region SetAdministratorRole 设置管理员权限
        /// <summary>
        /// 设置管理员权限
        /// </summary>
        private void SetAdministratorRole()
        {
            for (int index = 0; index < mControls.Length; index++)
            {
                mControls[index].Enabled = AccessControl.AccessItems[index].Administrator;
            }
        }
        #endregion

        #region SetAdministratorRole 设置超级管理员权限
        /// <summary>
        /// 设置超级管理员权限
        /// </summary>
        private void SetSuperAdministratorRole()
        {
            for (int index = 0; index < mControls.Length; index++)
            {
                mControls[index].Enabled = AccessControl.AccessItems[index].SuperAdministrator;
            }
        }
        #endregion

        #endregion


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParm);
        private const int WM_SETREDRAW = 11;
        private void BeginUpdate(Control control) 
        {
            SendMessage(control.Handle,WM_SETREDRAW,false,0);
        }
        private void EndUpdate(Control control) 
        {
            SendMessage(control.Handle, WM_SETREDRAW, true, 0);
            control.Refresh();
        }

        public void UIInit()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            materialSkinManager = MaterialSkinManager.Instance; // 初始化 MaterialSkinManager 实例
            materialSkinManager.AddFormToManage(this);  // 将要应用 Material Design 的窗体添加到管理列表中
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;   // Theme 属性用来设置整体的主题
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Indigo700, TextShade.WHITE);    // ColorScheme 属性来设置配色方案
        }
       
       

        private void materialButton_changeFormSize_MouseMove(object sender, MouseEventArgs e)
        {
           if (sender is Button)
            {
                Button btn = sender as Button;

                if (GlobalData.GetProperty("Language") == "Chinese")
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "多图分析");
                }
                else
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Multi analysis");
                }

               
            }
        }

     

        private void materialButton_fakeColor_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                if (GlobalData.GetProperty("Language") == "Chinese")
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "3D");
                }
                else
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "3D");
                }
                
            }
        }

        private void materialButton_imageInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                if (GlobalData.GetProperty("Language") == "Chinese")
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "图像信息");
                }
                else
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Image information");
                }
               
            }
        }

        private void materialButton_resetImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                if (GlobalData.GetProperty("Language") == "Chinese")
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "重置原图");
                }
                else
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Reset artwork");
                }
                
            }
        }

        private void materialButton_inverse_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                if (GlobalData.GetProperty("Language") == "Chinese")
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "反值");
                }
                else
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Inverse value");
                }
                
            }
        }

        private void materialButton_save_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                if (GlobalData.GetProperty("Language") == "Chinese")
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Ctrl + S 保存");
                }
                else
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Ctrl + S save");
                }
                
            }
        }

        private void materialButton_return_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                if (GlobalData.GetProperty("Language") == "Chinese")
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Ctrl + Z 撤銷");
                }
                else
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Ctrl + Z revocation");
                }
                
            }
        }

        private void materialButton_forward_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                if (GlobalData.GetProperty("Language") == "Chinese")
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Ctrl + Y 重做");
                }
                else
                {
                    this.btnStartUpToolTip.SetToolTip(btn, "Ctrl + Y renewal");
                }
                
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //刷新
            foreach (var item in bioanalysisMannages)
            {
                item.Value.GetImagePanel.CenterPictureBox();
            }
        }

        private void materialButton_LoadData_Click(object sender, EventArgs e)
        {
            // 加载泳道分析的图库
            string selectedFilePath = "";
            // 弹出选择图像的框
            #region 打开图片
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TIF Files (*.tif)|*.tif|TIFF files (*.tiff)|*.tiff";  // 设置文件筛选器
            openFileDialog.Title = "Select a TIF File";  // 设置对话框标题

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取选中的文件路径  只传入目录
                selectedFilePath = openFileDialog.FileName;


            }

            #endregion
            if (selectedFilePath != "")
            {


                if (lanesMannages.TryGetValue(selectedFilePath, out var value))
                {
                    return;
                }
                if (lanesMannages.Count == 0)
                {
                    lanesName.Clear();
                }
                LanesMannage lanesMannage = new LanesMannage(selectedFilePath, pl_right, lanesMannages);

                if (lanesMannage.GetImagePanel == null)
                {
                    lanesMannage = null;
                    return;
                }
                DataProcess_panel.Controls.Add(lanesMannage.GetImagePanel);
                lanesMannage.GetImagePanel.BringToFront();
                lanesName.Add(selectedFilePath);




            }
            //string selectedFilePath = "";
            //// 弹出选择图像的框
            //#region 打开图片
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "TIF Files (*.tif)|*.tif|All files (*.*)|*.*";  // 设置文件筛选器
            //openFileDialog.Title = "Select a TIF File";  // 设置对话框标题

            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    // 获取选中的文件路径
            //    selectedFilePath = openFileDialog.FileName;

            //}

            //#endregion
            //if (selectedFilePath != "")
            //{
            //    // Save Log Information
            //    Read_Write_Log read_Write_Log = new Read_Write_Log();
            //    string SaveLogFile = read_Write_Log.LogFile;

            //    List<Log> OldLog = new List<Log>();
            //    if (File.Exists(SaveLogFile))
            //    {
            //        OldLog = read_Write_Log.ReadCsv(SaveLogFile);
            //    }

            //    string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            //    OldLog.Add(new Log() { UserID = InnerUserID, ITEM = "加载数据", Description = "加载数据", Time = dateTime });

            //    read_Write_Log.WriteCsv(SaveLogFile, OldLog);

            //    DataProcessForm frmEmbed = new DataProcessForm(materialSkinManager, selectedFilePath);

            //    if (frmEmbed != null)
            //    {
            //        //frmEmbed.FormBorderStyle = FormBorderStyle.None;  //  无边框
            //        frmEmbed.TopLevel = false;  //  不是最顶层窗体
            //        DataProcess_panel.Controls.Add(frmEmbed);   //  添加到 Panel中

            //        FormGenerate_X = FormGenerate_X + 15;
            //        FormGenerate_Y = FormGenerate_Y + 15;

            //        frmEmbed.Location = new System.Drawing.Point(FormGenerate_X, FormGenerate_Y);
            //        frmEmbed.Show();      //  显示
            //        PBAnalyCommMannager.processForm = frmEmbed;
            //    }
            //}

        }

        private void materialButton_setting_Click(object sender, EventArgs e)
        {
            OperatingRecord.CreateRecord("系统设置按钮", "被点击了一下");
            SystemSettingForm system = new SystemSettingForm();
            system.ShowDialog();
            //if (settingForm != null)
            //    return;

            //settingForm = new SettingForm(materialSkinManager);
            //settingForm.FormClosed += settingForm_FormClosed;
            //settingForm.TopMost = true;
            //settingForm.Show();
        }

        private void settingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (settingForm == null)
                return;

            settingForm.Dispose();
            settingForm = null;
        }

        private void materialButton_curveimage_Click(object sender, EventArgs e)
        {
            if (PBAnalyCommMannager.processcurveAlg()) 
            {
                // Save Log Information
                Read_Write_Log read_Write_Log = new Read_Write_Log();
                string SaveLogFile = read_Write_Log.LogFile;

                List<Log> OldLog = new List<Log>();
                if (File.Exists(SaveLogFile))
                {
                    OldLog = read_Write_Log.ReadCsv(SaveLogFile);
                }

                string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                OldLog.Add(new Log() { UserID = InnerUserID, ITEM = "泳道波形图", Description = "操作泳道波形图", Time = dateTime });

                read_Write_Log.WriteCsv(SaveLogFile, OldLog);
            }
        }

        private void materialButton_analyzedata_Click(object sender, EventArgs e)
        {
            if (PBAnalyCommMannager.band_info == null) 
                return;

            // Save Log Information
            Read_Write_Log read_Write_Log = new Read_Write_Log();
            string SaveLogFile = read_Write_Log.LogFile;

            List<Log> OldLog = new List<Log>();
            if (File.Exists(SaveLogFile))
            {
                OldLog = read_Write_Log.ReadCsv(SaveLogFile);
            }

            string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            OldLog.Add(new Log() { UserID = InnerUserID, ITEM = "分析数据", Description = "分析数据", Time = dateTime });

            read_Write_Log.WriteCsv(SaveLogFile, OldLog);

            PBAnaly.UI.AnalyzeDataForm analyzeDataForm = new UI.AnalyzeDataForm(PBAnalyCommMannager.band_info);
            analyzeDataForm.TopMost = true;
            analyzeDataForm.Show();
        }


        private void materialButton_imageProcess_Click(object sender, EventArgs e)
        {
            string selectedFilePath = "";
            // 弹出选择图像的框
            #region 打开图片
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TIF Files (*.tif)|*.tif|All files (*.*)|*.*";  // 设置文件筛选器
            openFileDialog.Title = "Select a TIF File";  // 设置对话框标题

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取选中的文件路径  只传入目录
                selectedFilePath = Path.GetDirectoryName(openFileDialog.FileName);

                
            }

            #endregion
            if (selectedFilePath != "")
            {
                //if (ImageToolMannage.imageDataPath.TryGetValue(selectedFilePath, out var value)) 
                //{
                //    return;
                //}

                if (bioanalysisMannages.TryGetValue(selectedFilePath, out var value))
                {
                    return;
                }
                if (bioanalysisMannages.Count == 0)
                {
                    bioanalyName.Clear();
                }
                BioanalysisMannage bioanalysisMannage = new BioanalysisMannage(selectedFilePath, pl_right, bioanalysisMannages);
                if (bioanalysisMannage.GetImagePanel == null) 
                {
                    bioanalysisMannage = null;
                    return;
                }
                DataProcess_panel.Controls.Add(bioanalysisMannage.GetImagePanel);
                bioanalysisMannage.GetImagePanel.BringToFront();


                bioanalyName.Add(selectedFilePath);




            }
        }

        private void mb_colonyCount_Click(object sender, EventArgs e)
        {
            string selectedFilePath = "";
            // 弹出选择图像的框
            #region 打开图片
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TIF Files (*.tif)|*.tif|All files (*.*)|*.*";  // 设置文件筛选器
            openFileDialog.Title = "Select a TIF File";  // 设置对话框标题

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                selectedFilePath = openFileDialog.FileName;


            }
            #endregion
            if (selectedFilePath != "") 
            {
                if (colonysMannages.TryGetValue(selectedFilePath, out var value))
                {
                    return;
                }
                if (colonysMannages.Count == 0)
                {
                    colonysMannages.Clear();
                }

                ColonyMannage colonyMannage = new ColonyMannage(selectedFilePath, pl_right, colonysMannages);
                if (colonyMannage.GetImagePanel == null)
                {
                    colonyMannage = null;
                    return;
                }
                DataProcess_panel.Controls.Add(colonyMannage.GetImagePanel);
                colonyMannage.GetImagePanel.BringToFront();


                colonysMannages.Add(selectedFilePath, colonyMannage);
            }

        }

        private void materialButton_log_Click(object sender, EventArgs e)
        {
            UI.LogForm logForm = new UI.LogForm(materialSkinManager);
            logForm.Show();

            //if (logForm != null)
            //    return;

            //logForm = new LogForm(materialSkinManager,InnerUserID);
            //logForm.FormClosed += LogForm_FormClosed;
            //logForm.TopMost = true;
            //logForm.Show();
        }

        private void LogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (logForm == null)
                return;

            logForm.Dispose();
            logForm = null;
        }

        private void materialButton_outimage_Click(object sender, EventArgs e)
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string dir = string.Format(@"./{0}/", "Report");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            string savePDF = string.Format("{0}{1}_Report.pdf",dir,datetime);

            // 初始化文檔對象
            Document document = new Document();
            // 添加頁面
            Page page = document.Pages.Add();
            // 向新頁面添加文本
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Hello World!"));
            // 保存PDF 
            document.Save(savePDF);
            string ReportMesage = string.Format("Report Save at : {0}", savePDF);
            MessageBox.Show(ReportMesage);

            // Save Log Information
            Read_Write_Log read_Write_Log = new Read_Write_Log();
            string SaveLogFile = read_Write_Log.LogFile;

            List<Log> OldLog = new List<Log>();
            if (File.Exists(SaveLogFile))
            {
                OldLog = read_Write_Log.ReadCsv(SaveLogFile);
            }

            string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            OldLog.Add(new Log() { UserID = InnerUserID, ITEM = "导出图像", Description = "导出图像", Time = dateTime });

            read_Write_Log.WriteCsv(SaveLogFile, OldLog);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (true) 
            {
                Process[] processes = Process.GetProcessesByName("PointCloudDemo");
                foreach (Process process in processes) 
                {
                    try
                    {
                        if (process.MainWindowHandle != IntPtr.Zero) 
                        {
                            SendMessage(process.MainWindowHandle, 0x0010,IntPtr.Zero, IntPtr.Zero);

                            if (process.WaitForExit(3000)) 
                            {

                            }
                            else
                            {
                                process.Kill();
                            }
                        }
                        else
                        {
                            process.Kill();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            
            System.Environment.Exit(0);
        }

        private void materialButton_imageInfo_Click(object sender, EventArgs e)
        {
            
        }
        bool isGridView = false;
        private void materialButton_changeFormSize_Click(object sender, EventArgs e)
        {
            #region 排除已经不存在的
            if (bioanalysisMannages.Count == 0) 
            {
                bioanalyName.Clear();
                return;
            }
            // 如果list 不在字典中 将移除list中的某一个
            List<int> indexF = new List<int>();
            
            foreach (var item in bioanalyName)
            {
                bool ret = false;
                int i = -1;
                foreach (var bio in bioanalysisMannages)
                {
                    i++;
                    if (item == bio.Key) 
                    {
                        ret = true;
                    }
                }
                if(ret == false) 
                {
                    indexF.Add(i);
                }
            }

            for (int i = indexF.Count-1; i >= 0; i--)
            {
                bioanalyName.RemoveAt(indexF[i]);
            }
            #endregion

            if (bioanalysisMannages.Count == 1) 
            {
                #region 如果只有一张图时显示方案
                if (bioanalyBool == false)
                {
                    bioanalyBool = true;
                    foreach (var item in bioanalysisMannages)
                    {
                        item.Value.Arrangement = 0;
                        item.Value.WindowAdaptive();
                    }
                }
                else
                {
                    bioanalyBool = false;
                    foreach (var item in bioanalysisMannages)
                    {
                        item.Value.Arrangement = 0;
                        item.Value.WindowNormalAdaptive();
                    }
                }
                #endregion
            }
            else
            {
                #region 是否合并了  
                if (bioanalyBool == false)
                {
                    bioanalyBool = true;
                    if (bioanalysisMannages.Count == 0) return;
                    SizeForm sizeForm = new SizeForm();

                    if (sizeForm.ShowDialog() == DialogResult.OK)
                    {
                        data_row = sizeForm.row;
                        data_col = sizeForm.col;
                        sizeForm.Dispose();
                        sizeForm = null;
                    }
                    else
                    {
                        sizeForm.Dispose();
                        sizeForm = null;
                        return;
                    }
                    DataProcess_panel.Controls.Clear();
                    pl_right.Controls.Clear();

                    if (data_right_bar != null)
                    {
                        data_right_bar.Controls.Clear();
                        data_right_bar.Dispose();

                    }
                    data_right_bar = new TableLayoutPanel();
                    data_right_bar.SuspendLayout();
                    data_right_bar.RowCount = 2;
                    data_right_bar.ColumnCount = 1;
                    data_right_bar.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 1));
                    data_right_bar.RowStyles.Add(new RowStyle(SizeType.Absolute, 60f));
                    data_right_bar.ResumeLayout();
                    if (data_tab != null)
                    {
                        data_tab.Controls.Clear();
                        data_tab.Dispose();
                    }
                    data_tab = new TableLayoutPanel();
                    data_tab.SuspendLayout();
                    if(data_row >= 1 && data_row <=3)data_row = 3;
                    else if(data_row >=5 && data_row <=9) data_row = 9;
                    data_tab.RowCount = data_row*2;
                    data_tab.ColumnCount = data_col + 1;
                    


                    for (int i = 0; i < data_row; i++)
                    {
                        if(i % 2==0)
                            data_tab.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / data_row));
                        else
                            data_tab.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                    }
                    for (int i = 0; i < data_col; i++)
                    {
                        data_tab.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f / data_col));
                    }
                    data_tab.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150f));
                    data_tab.ResumeLayout();
                    int row = 0;
                    int col = 0;
                    data_tab.Dock = DockStyle.Fill;
                    DataProcess_panel.Controls.Add(data_tab);
                    int index = 0;
                    foreach (var bname in bioanalyName)
                    {
                        
                        var item = bioanalysisMannages[bname];
                        item.rectangles.Clear();
                        item.CircleAndInfoList.Clear();
                        item.Arrangement = 1;
                        if (index == bioanalysisMannages.Count - 1)
                        {
                            pl_right.Controls.Add(item.GetBioanayImagePanel);
                            //item.Value.GetRight.Dock = DockStyle.Fill;
                            //data_tab.Controls.Add(item.Value.GetRight, data_tab.ColumnCount - 1, 0);
                            //data_tab.SetRowSpan(item.Value.GetRight, data_row);
                            data_right_bar.Controls.Add(item.GetBarImage, 0, 0);
                            data_right_bar.Controls.Add(item.GetImagePanel.lb_wh, 0, 1);
                            data_right_bar.Dock = DockStyle.Fill;
                            data_tab.Controls.Add(data_right_bar, data_tab.ColumnCount - 1, 0);
                            data_tab.SetRowSpan(data_right_bar, data_row);
                            item.Arrangement = 2;

                        }
                        index++;
                        
                        data_tab.Controls.Add(item.GetPanel, col, row);
                        item.GetPanel.Dock = DockStyle.Fill;
                        data_tab.Controls.Add(item.GetBottomPanel,col,row+1);
                        item.GetImagePanel.image_pl.SizeMode = PictureBoxSizeMode.Zoom;
                        item.GetImagePanel.ava_auto_Click(null, null);
                        if (col < data_tab.ColumnCount - 2)
                        {
                            col++;
                        }
                        else
                        {
                            row += 2;
                            col = 0;
                        }
                    }
                    

                }
                else
                {
                    bioanalyBool = false;
                    if (data_tab == null) return;
                    data_tab.Controls.Clear();
                    DataProcess_panel.Controls.Clear();
                    pl_right.Controls.Clear();
                    int index = 0;
                    foreach (var item in bioanalysisMannages)
                    {
                       
                        if (index == 0) 
                        {
                            
                            pl_right.Controls.Add(item.Value.GetBioanayImagePanel);
                            index++;
                        }
                        item.Value.Arrangement = 0;
                        DataProcess_panel.Controls.Add(item.Value.GetImagePanel);
                        item.Value.Rifresh();
                        item.Value.GetImagePanel.BringToFront();
                    }
                    
                }
                #endregion

            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        // 暂时用来显示3D图
        private void materialButton_fakeColor_Click(object sender, EventArgs e)
        {
            foreach (var item in bioanalysisMannages)
            {
                if (item.Value.IsActive) 
                {
                    // 获取mark图 存下来
                    item.Value.SaveMark("tmp1.bmp");

                    item.Value.SavePseu("tmp2.bmp");
                    string langur = GlobalData.GetProperty("Language") == "English" ? "0" : "1";
                    // 启动 WPF EXE 并传递参数
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "PointCloudDemo.exe", // WPF EXE 的路径
                        Arguments = $"\"0,{langur},tmp1.bmp,tmp2.bmp\"", // 用双引号包裹参数（防止空格或特殊字符问题）
                        UseShellExecute = false
                    };

                    Process.Start(startInfo);
                }
               
            }

           
        }
    }
}
