using Aspose.Pdf;
using MaterialSkin;
using MaterialSkin.Controls;
using OpenCvSharp.Flann;
using OpenTK;
using PBAnaly.Module;
using PBAnaly.Properties;
using PBAnaly.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Resources = PBAnaly.Properties.Resources;

namespace PBAnaly
{
    public partial class MainForm : MaterialForm
    {
        private MaterialSkinManager materialSkinManager;
        private SettingForm settingForm;
        private LoginForm loginForm;
        private LogForm logForm;

        private int FormGenerate_X;
        private int FormGenerate_Y;

        private string InnerUserID = "";

        private string color_bar = "YellowHot";
        System.Windows.Forms.TableLayoutPanel tlp_main_images;

        private Dictionary<string ,BioanalysisMannage> bioanalysisMannages = new Dictionary<string, BioanalysisMannage>();
   
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
                this.btnStartUpToolTip.SetToolTip(btn, "适配窗口");
            }
        }

        private void materialButton_imageChange_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                this.btnStartUpToolTip.SetToolTip(btn, "图像变换");
            }
        }

        private void materialButton_fakeColor_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                this.btnStartUpToolTip.SetToolTip(btn, "伪彩");
            }
        }

        private void materialButton_imageInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                this.btnStartUpToolTip.SetToolTip(btn, "图像信息");
            }
        }

        private void materialButton_resetImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                this.btnStartUpToolTip.SetToolTip(btn, "重置原图");
            }
        }

        private void materialButton_inverse_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                this.btnStartUpToolTip.SetToolTip(btn, "反值");
            }
        }

        private void materialButton_save_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                this.btnStartUpToolTip.SetToolTip(btn, "Ctrl + S 保存");
            }
        }

        private void materialButton_return_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                this.btnStartUpToolTip.SetToolTip(btn, "Ctrl + Z 撤銷");
            }
        }

        private void materialButton_forward_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                this.btnStartUpToolTip.SetToolTip(btn, "Ctrl + Y 重做");
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void materialButton_LoadData_Click(object sender, EventArgs e)
        {
            string selectedFilePath = "";
            // 弹出选择图像的框
            #region 打开图片
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TIF Files (*.tif)|*.tif|All files (*.*)|*.*";  // 设置文件筛选器
            openFileDialog.Title = "Select a TIF File";  // 设置对话框标题

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取选中的文件路径
                selectedFilePath = openFileDialog.FileName;
                
            }

            #endregion
            if (selectedFilePath != "") 
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
                OldLog.Add(new Log() { UserID = InnerUserID, ITEM = "加载数据", Description = "加载数据", Time = dateTime });

                read_Write_Log.WriteCsv(SaveLogFile, OldLog);

                DataProcessForm frmEmbed = new DataProcessForm(materialSkinManager, selectedFilePath);

                if (frmEmbed != null)
                {
                    //frmEmbed.FormBorderStyle = FormBorderStyle.None;  //  无边框
                    frmEmbed.TopLevel = false;  //  不是最顶层窗体
                    DataProcess_panel.Controls.Add(frmEmbed);   //  添加到 Panel中

                    FormGenerate_X = FormGenerate_X + 15;
                    FormGenerate_Y = FormGenerate_Y + 15;

                    frmEmbed.Location = new System.Drawing.Point(FormGenerate_X, FormGenerate_Y);
                    frmEmbed.Show();      //  显示
                    PBAnalyCommMannager.processForm = frmEmbed;
                }
            }
           
        }

        private void materialButton_setting_Click(object sender, EventArgs e)
        {
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
                BioanalysisMannage bioanalysisMannage = new BioanalysisMannage(selectedFilePath, pl_right, bioanalysisMannages);

                DataProcess_panel.Controls.Add(bioanalysisMannage.GetImagePanel);
                bioanalysisMannage.GetImagePanel.BringToFront();


              
                


            }
        }
        private void materialButton_log_Click(object sender, EventArgs e)
        {
            if (logForm != null)
                return;

            logForm = new LogForm(materialSkinManager,InnerUserID);
            logForm.FormClosed += LogForm_FormClosed;
            logForm.TopMost = true;
            logForm.Show();
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
            System.Environment.Exit(0);
        }

        private void materialButton_imageInfo_Click(object sender, EventArgs e)
        {
            
        }
        bool isGridView = false;
        private void materialButton_changeFormSize_Click(object sender, EventArgs e)
        {
            if (bioanalysisMannages.Count == 1) 
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.WindowAdaptive();
                }
            }
        }
    }
}
