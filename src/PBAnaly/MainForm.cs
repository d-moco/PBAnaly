using MaterialSkin;
using MaterialSkin.Controls;
using PBAnaly.Module;
using System;
using System.Windows.Forms;

namespace PBAnaly
{
    public partial class MainForm : MaterialForm
    {
        private MaterialSkinManager materialSkinManager;
        private SettingForm settingForm;

        private int FormGenerate_X;
        private int FormGenerate_Y;

        public MainForm()
        {
            InitializeComponent();

            UIInit();

            FormGenerate_X = 0;
            FormGenerate_Y = 0;
        }

        public void SetLeftButtonSize(int width,int height)
        {
            var startPoint = materialCard_LeftBtn.Location;

            materialButton_imageProcess.Size = new System.Drawing.Size(width, height);
            materialButton_imageProcess.Location = new System.Drawing.Point(startPoint.X, startPoint.Y);

            materialButton_acidAnalyze.Size = new System.Drawing.Size(width, height);
            materialButton_acidAnalyze.Location = new System.Drawing.Point(startPoint.X, startPoint.Y + 1*(height + 5));

            materialButton_roiAnalyze.Size = new System.Drawing.Size(width, height);
            materialButton_roiAnalyze.Location = new System.Drawing.Point(startPoint.X, startPoint.Y + 2 * (height + 5));

            materialButton_miniAnalyze.Size = new System.Drawing.Size(width, height);
            materialButton_miniAnalyze.Location = new System.Drawing.Point(startPoint.X, startPoint.Y + 3 * (height + 5));

            materialButton_dotcounts.Size = new System.Drawing.Size(width, height);
            materialButton_dotcounts.Location = new System.Drawing.Point(startPoint.X, startPoint.Y + 4 * (height + 5));

            materialButton_correction.Size = new System.Drawing.Size(width, height);
            materialButton_correction.Location = new System.Drawing.Point(startPoint.X, startPoint.Y + 5 * (height + 5));
        }

        public void SetRightButtonSize(int width, int height)
        {
            var startPoint = metroPanel_RightTop.Location;

            materialButton_LoadData.Size = new System.Drawing.Size(width, height);
            materialButton_LoadData.Location = new System.Drawing.Point(startPoint.X, startPoint.Y);

            materialButton_outimage.Size = new System.Drawing.Size(width, height);
            materialButton_outimage.Location = new System.Drawing.Point(startPoint.X + 1 * width + 5, startPoint.Y);

            materialButton_analyzedata.Size = new System.Drawing.Size(width, height);
            materialButton_analyzedata.Location = new System.Drawing.Point(startPoint.X + 2 * width + 5, startPoint.Y);

            materialButton_curveimage.Size = new System.Drawing.Size(width, height);
            materialButton_curveimage.Location = new System.Drawing.Point(startPoint.X + 3 * width + 5, startPoint.Y);

            materialButton_setting.Size = new System.Drawing.Size(width, height);
            materialButton_setting.Location = new System.Drawing.Point(startPoint.X + 4 * width + 5, startPoint.Y);

            materialButton_log.Size = new System.Drawing.Size(width, height);
            materialButton_log.Location = new System.Drawing.Point(startPoint.X + 5 * width + 5, startPoint.Y);
        }

        public void UI_MaxMinSizeChange()
        {
            var Formsize_width = this.Width;
            var Formsize_height = this.Height;

            var InitialLocation = CompanyIcon_pictureBox.Location;
        
            if (Formsize_width > 1500)
            {
                materialCard_LeftBtn.Size = new System.Drawing.Size(240, Formsize_height);
                materialCard_LeftBtn.Location = new System.Drawing.Point(3, 130);
                metroPanel_RightTop.Size = new System.Drawing.Size(Formsize_width - 700, 60);
                SetLeftButtonSize(240, 60);
                SetRightButtonSize(200, 60);
                CompanyIcon_pictureBox.Size = new System.Drawing.Size(240, 100);
                metroPanel_RightTop.Location = new System.Drawing.Point(InitialLocation.X + 240 + 3, InitialLocation.Y);
                metroPanel_RightIcon.Location = new System.Drawing.Point(InitialLocation.X + 240 + 3, InitialLocation.Y + metroPanel_RightTop.Size.Height + 3);
                DataProcess_panel.Location = new System.Drawing.Point(metroPanel_RightTop.Location.X, 130);
                DataProcess_panel.Size = new System.Drawing.Size(Convert.ToInt32(Formsize_width * 0.865), Convert.ToInt32(Formsize_height * 0.865));
            }
            else
            {
                materialCard_LeftBtn.Size = new System.Drawing.Size(165, 500);
                materialCard_LeftBtn.Location = new System.Drawing.Point(3,100);
                metroPanel_RightTop.Size = new System.Drawing.Size(Formsize_width - 150, 32);
                SetLeftButtonSize(165, 48);
                SetRightButtonSize(750, 32);
                CompanyIcon_pictureBox.Size = new System.Drawing.Size(165, 70);
                metroPanel_RightTop.Location = new System.Drawing.Point(InitialLocation.X + 165 + 3, InitialLocation.Y);
                metroPanel_RightIcon.Location = new System.Drawing.Point(InitialLocation.X + 165 + 3, InitialLocation.Y + metroPanel_RightTop.Size.Height + 3);
                DataProcess_panel.Location = new System.Drawing.Point(metroPanel_RightTop.Location.X, 100);
                DataProcess_panel.Size = new System.Drawing.Size(Convert.ToInt32(Formsize_width * 0.85), Convert.ToInt32(Formsize_height * 0.80));
            }
                
            this.Refresh();
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
            UI_MaxMinSizeChange();
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
            if (settingForm != null)
                return;

            settingForm = new SettingForm(materialSkinManager);
            settingForm.FormClosed += settingForm_FormClosed;
            settingForm.Show();
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
               
            }
        }

        private void materialButton_analyzedata_Click(object sender, EventArgs e)
        {
            if (PBAnalyCommMannager.band_info == null) return;
            PBAnaly.UI.AnalyzeDataForm analyzeDataForm = new UI.AnalyzeDataForm(PBAnalyCommMannager.band_info);
            analyzeDataForm.TopMost = true;
            analyzeDataForm.Show();
        }
    }
}
