﻿using Aspose.Pdf;
using MaterialSkin;
using MaterialSkin.Controls;
using OpenCvSharp.Flann;
using OpenTK;
using PBAnaly.Module;
using PBAnaly.Properties;
using PBAnaly.UI;
using System;
using System.Collections.Generic;
using System.Data;
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
        private List<string> bioanalyName = new List<string>();
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

            UIInit();

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
    }
}
