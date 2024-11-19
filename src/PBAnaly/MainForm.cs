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
        
   
        bool isRun = false;
        private Thread thread;
        private void runThread() 
        {
            //while (isRun)
            //{
                
            //    int index = 0;
            //    foreach (var item in ImageToolMannage.imageDataPath)
            //    {
            //        if (index == 0)
            //        {
            //            if (pb_image1.InvokeRequired) 
            //            {
            //                pb_image1.Invoke(new Action(() => pb_image1.Image = item.Value.pictureBox));
            //            }
                       
                     
            //        }
            //        else if (index == 1)
            //        {
            //            if (pb_image2.InvokeRequired)
            //            {
            //                pb_image2.Invoke(new Action(() => pb_image2.Image = item.Value.pictureBox));
            //            }
            //        }
            //        else if (index == 2)
            //        {
            //            if (pb_image3.InvokeRequired)
            //            {
            //                pb_image3.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //            }
            //        }
            //        else if (index == 3)
            //        {
            //            if (pb_image4.InvokeRequired)
            //            {
            //                pb_image4.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //            }
            //        }
            //        else if (index == 4)
            //        {
            //            if (pb_image5.InvokeRequired)
            //            {
            //                pb_image5.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //            }
            //        }
            //        else if (index == 5)
            //        {
            //            if (pb_image6.InvokeRequired)
            //            {
            //                pb_image6.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //            }
            //        }
            //        else if (index == 6)
            //        {
            //            if (pb_image7.InvokeRequired)
            //            {
            //                pb_image7.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //            }
            //        }
            //        else if (index == 7)
            //        {
            //            if (pb_image8.InvokeRequired)
            //            {
            //                pb_image8.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //            }
            //        }
            //        else if (index == 8)
            //        {
            //            if (pb_image9.InvokeRequired)
            //            {
            //                pb_image9.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //            }
            //        }
            //    }
            //    Thread.Sleep(200);
            //}
        }
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
        public void SetPic(Bitmap p) 
        {
          
            int index = 0;

            //foreach (var item in ImageToolMannage.imageDataPath)
            //{
            //    if (index == 0)
            //    {
            //        var camera = item.Value.GetImage();
            //        pb_image1.Image = util.ConvertRgb24ImageToBitmap(camera);
                    
            //        pb_image1.Refresh();

            //    }
            //    else if (index == 1)
            //    {
            //        pb_image2.Invoke(new Action(() => pb_image2.Image = item.Value.pictureBox));
            //    }
            //    else if (index == 2)
            //    {
            //        pb_image3.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //    }
            //    else if (index == 3)
            //    {
            //        pb_image4.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //    }
            //    else if (index == 4)
            //    {
            //        pb_image5.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //    }
            //    else if (index == 5)
            //    {
            //        if (pb_image6.InvokeRequired)
            //        {
            //            pb_image6.Invoke(new Action(() => pb_image6.Image = item.Value.pictureBox));
            //        }
            //    }
            //    else if (index == 6)
            //    {
            //        pb_image7.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //    }
            //    else if (index == 7)
            //    {
            //        pb_image8.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //    }
            //    else if (index == 8)
            //    {
            //        pb_image9.Invoke(new Action(() => pb_image9.Image = item.Value.pictureBox));
            //    }
            //    index++;
            //}
        }
        private void initPanel() 
        {
            //BeginUpdate(tl_right_main_view);
           
            //tl_right_main_view.RowCount = 1;
            //tl_right_main_view.ColumnCount = 1;

            //tl_right_main_view.Controls.Clear();
            
           

            //if (isGridView)
            //{
            //    pl_right.Controls.Clear();
            //    itpf = new COMMImageToolPaletteForm(this);
            //    itpf.TopLevel = false;
            //    itpf.Dock = DockStyle.Fill;
            //    pb_colorbar = new PictureBox();
            //    pb_colorbar.SizeMode = PictureBoxSizeMode.StretchImage;
                
            //    tl_right_main_view.RowCount = 1;
            //    tl_right_main_view.ColumnCount = 2;
            //    tl_right_main_view.RowStyles.Clear();
            //    tl_right_main_view.ColumnStyles.Clear();

            //    tl_right_main_view.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100f));
            //    tl_right_main_view.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100f));
            //    pb_lists.Clear();
            //    tlp_main_images = new TableLayoutPanel();
            //    tl_right_main_view.Controls.Add(tlp_main_images,0,0);
            //    tl_right_main_view.Controls.Add(pb_colorbar, 1,0);
            //    pb_colorbar.Dock = DockStyle.Fill;
            //    pl_right.Controls.Add(itpf);
            //    pb_colorbar.Show();
            //    pb_colorbar.Dock = DockStyle.Fill;
            //    itpf.Show();
            //    tlp_main_images.Dock = DockStyle.Fill;
                
            //    int capCount = ImageToolMannage.imageDataPath.Count;
            //    int rows = (int)Math.Ceiling(Math.Sqrt(capCount));
            //    int cols = (int)Math.Ceiling((double)capCount / rows);
            //    if (capCount == 1) 
            //    {
            //        rows = 1;
            //        cols = 0;
            //    }
            //    else if (capCount == 2)
            //    {
            //        rows = 1;
            //        cols = 2;
            //    }
            //    else if ( capCount == 3)
            //    {
            //        rows = 1;
            //        cols = capCount;
            //    }
            //    else if (capCount == 4)
            //    {
            //        rows = 2;
            //        cols = 2;
            //    }
            //    else if (capCount > 4 && capCount <= 6)
            //    {
            //        rows = 2;
            //        cols = 3;
            //    }
            //    else if (capCount > 6 && capCount <= 9)
            //    {
            //        rows = 3;
            //        cols = 3;
            //    }


            //    tlp_main_images.RowCount = rows;
            //    tlp_main_images.ColumnCount = cols;

            //    tlp_main_images.RowStyles.Clear();
            //    tlp_main_images.ColumnStyles.Clear();
            //    for (int i = 0; i < rows; i++)
            //    {
            //        tlp_main_images.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
            //    }
            //    for (int i = 0; i < cols; i++)
            //    {
            //        tlp_main_images.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));
            //    }

            //    int index = 0;
            //    int r = 0;
            //    int c = 0;
            //    foreach (var item in ImageToolMannage.imageDataPath)
            //    {
            //        if (index == 0)
            //        {
            //            tlp_main_images.Controls.Add(pb_image1, c, r);
            //            pb_image1.Dock = DockStyle.Fill;
            //            pb_image1.Show();
            //        }
            //        else if(index == 1)
            //        {
            //            tlp_main_images.Controls.Add(pb_image2, c, r);
            //            pb_image2.Dock = DockStyle.Fill;
            //            pb_image2.Show();
            //        }
            //        else if (index == 2)
            //        {
            //            tlp_main_images.Controls.Add(pb_image3, c, r);
            //            pb_image3.Dock = DockStyle.Fill;
            //            pb_image3.Show();
            //        }
            //        else if (index == 3)
            //        {
            //            tlp_main_images.Controls.Add(pb_image4, c, r);
            //            pb_image4.Dock = DockStyle.Fill;
            //            pb_image4.Show();
            //        }
            //        else if (index == 4)
            //        {
            //            tlp_main_images.Controls.Add(pb_image5, c, r);
            //            pb_image5.Dock = DockStyle.Fill;
            //            pb_image5.Show();
            //        }
            //        else if (index == 5)
            //        {
            //            tlp_main_images.Controls.Add(pb_image6, c, r);
            //            pb_image6.Dock = DockStyle.Fill;
            //            pb_image6.Show();
            //        }
            //        else if (index == 6)
            //        {
            //            tlp_main_images.Controls.Add(pb_image7, c, r);
            //            pb_image7.Dock = DockStyle.Fill;
            //            pb_image7.Show();
            //        }
            //        else if (index == 7)
            //        {
            //            tlp_main_images.Controls.Add(pb_image8, c, r);
            //            pb_image8.Dock = DockStyle.Fill;
            //            pb_image8.Show();
            //        }
            //        else if (index == 8)
            //        {
            //            tlp_main_images.Controls.Add(pb_image9, c, r);
            //            pb_image9.Dock = DockStyle.Fill;
            //            pb_image9.Show();
            //        }
            //        c++;
            //        if (c >= cols)
            //        {
            //            r++;
            //            c = 0;
            //        }
            //        index++;
            //    }
               
            //    isRun = true;
            //    thread = new Thread(runThread);
            //    thread.IsBackground = true;
            //    thread.Start();
            //}
            //else 
            //{
            //    isRun = false;
            //    tl_right_main_view.Controls.Add(DataProcess_panel);
            //    DataProcess_panel.Dock = DockStyle.Fill;

            //    foreach (var frmEmbed in ImageToolMannage.imageDataPath.Values)
            //    {
            //        if (frmEmbed != null)
            //        {
            //            frmEmbed.TopLevel = false;
            //            DataProcess_panel.Controls.Add(frmEmbed);
            //            //FormGenerate_X = FormGenerate_X + 15;
            //            //FormGenerate_Y = FormGenerate_Y + 15;
            //            frmEmbed.Location = new System.Drawing.Point(FormGenerate_X, FormGenerate_Y);
            //            frmEmbed.Show();      //  显示
            //            frmEmbed.BringToFront();
            //            frmEmbed.initPicturebox();




            //        }
            //    }
            //}
            //EndUpdate(tl_right_main_view);
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
                if (ImageToolMannage.imageDataPath.TryGetValue(selectedFilePath, out var value)) 
                {
                    return;
                }



                ImagePanel frmEmbed = new ImagePanel(selectedFilePath, pl_right);
                ImageToolMannage.imageDataPath[selectedFilePath] = frmEmbed;

                if (frmEmbed != null) 
                {
                    frmEmbed.TopLevel = false;
                    DataProcess_panel.Controls.Add(frmEmbed);
                    FormGenerate_X = FormGenerate_X + 15;
                    FormGenerate_Y = FormGenerate_Y + 15;
                    frmEmbed.Location = new System.Drawing.Point(FormGenerate_X, FormGenerate_Y);
                    frmEmbed.Show();      //  显示
                    frmEmbed.BringToFront();
                    
                    //frmEmbed.RefreshUI();
                    

                }
                initPanel();


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

            //isGridView = isGridView == false ? true : false;
            //initPanel();
#if true
            //var forms = ImageToolMannage.imageDataPath.Values.ToList();
            //int formCount = forms.Count;

            //int rows, columns;

            //// 根据窗体数量确定布局
            //if (formCount == 1)
            //{
            //    rows = 1;
            //    columns = 1;
            //}
            //else if (formCount == 2)
            //{
            //    rows = 1;
            //    columns = 2;
            //}
            //else if (formCount == 3)
            //{
            //    rows = 1;
            //    columns = 3;
            //}
            //else if (formCount == 4)
            //{
            //    rows = 2;
            //    columns = 2;
            //}
            //else if (formCount == 5)
            //{
            //    rows = 2;
            //    columns = 3;
            //}
            //else if (formCount == 6)
            //{
            //    rows = 2;
            //    columns = 3;
            //}
            //else // formCount >= 7
            //{
            //    rows = (int)Math.Ceiling(formCount / 2.0);
            //    columns = 2;
            //}

            //// 计算每个窗体的大小
            //int formWidth = DataProcess_panel.Width / columns;
            //int formHeight = DataProcess_panel.Height / rows;

            //// 清除 DataProcess_panel 中已有的控件
            //DataProcess_panel.Controls.Clear();

            //// 调整每个窗体的位置和大小
            //for (int i = 0; i < formCount; i++)
            //{
            //    var form = forms[i];
            //    PictureBox pictureBox = new PictureBox();
            //    pictureBox.Image = form.GetPseuImage;


            //    int row = i / columns;
            //    int col = i % columns;


            //    pictureBox.Bounds = new System.Drawing.Rectangle(col * formWidth, row * formHeight, formWidth, formHeight);

            //    // 添加窗体到 Panel 中并显示
            //    DataProcess_panel.Controls.Add(pictureBox);
            //    pictureBox.Show();
            //}
            //COMMImageToolPaletteForm cOMMImageToolPaletteForm = new COMMImageToolPaletteForm(this);
            //cOMMImageToolPaletteForm.TopLevel = false;
            //cOMMImageToolPaletteForm.Dock = DockStyle.Fill;
            //pl_right.Controls.Clear();
            //pl_right.Controls.Add(cOMMImageToolPaletteForm);
            //cOMMImageToolPaletteForm.Show();


            int margin = 5;
            int formCount = ImageToolMannage.imageDataPath.Count;
            if (formCount == 0) return;

            int columns = (int)Math.Ceiling(Math.Sqrt(formCount));
            int rows = (int)Math.Ceiling((double)formCount / columns);

            int formWidth = (DataProcess_panel.Width - (columns + 1) * margin) / columns;
            int formHeight = (DataProcess_panel.Height - (rows + 1) * margin) / rows;

            int totalWidth = columns * formWidth + (columns + 1) * margin;
            int totalHeight = rows * formHeight + (rows + 1) * margin;
            if (totalWidth > DataProcess_panel.Width)
            {
                formWidth = (DataProcess_panel.Width - (columns + 1) * margin) / columns;
            }
            if (totalHeight > DataProcess_panel.Height)
            {
                formHeight = (DataProcess_panel.Height - (rows + 1) * margin) / rows;
            }
            int index = 0;
            foreach (var item in ImageToolMannage.imageDataPath)
            {
                int row = index / columns;
                int col = index % columns;

                int x = margin + col * (formWidth + margin);
                int y = margin + row * (formHeight + margin);

                var panel = item.Value;
                panel.Size = new Size(formWidth, formHeight);
                panel.Location = new System.Drawing.Point(x, y);
                panel.TopLevel = false;
                panel.FormBorderStyle = FormBorderStyle.None;
                panel.Visible = true;
                DataProcess_panel.Controls.Add(panel);
                panel.Show();
                panel.BringToFront();
                index++;
            }
#endif

        }
    }
}
