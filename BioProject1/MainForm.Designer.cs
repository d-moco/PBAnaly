namespace BioProject1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.materialSwitch_UI = new MaterialSkin.Controls.MaterialSwitch();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.materialButton_log = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_setting = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_curveimage = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_analyzedata = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_outimage = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_LoadData = new MaterialSkin.Controls.MaterialButton();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.materialButton_forward = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_return = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_save = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_inverse = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_resetImage = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_imageInfo = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_fakeColor = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_imageChange = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_changeFormSize = new MaterialSkin.Controls.MaterialButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnStartUpToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.materialButton_miniAnalyze = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_roiAnalyze = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_dotcounts = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_acidAnalyze = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_correction = new MaterialSkin.Controls.MaterialButton();
            this.materialButton_imageProcess = new MaterialSkin.Controls.MaterialButton();
            this.materialDivider3 = new MaterialSkin.Controls.MaterialDivider();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialSwitch_UI
            // 
            this.materialSwitch_UI.AutoSize = true;
            this.materialSwitch_UI.Depth = 0;
            this.materialSwitch_UI.Location = new System.Drawing.Point(853, 97);
            this.materialSwitch_UI.Margin = new System.Windows.Forms.Padding(0);
            this.materialSwitch_UI.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialSwitch_UI.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSwitch_UI.Name = "materialSwitch_UI";
            this.materialSwitch_UI.Ripple = true;
            this.materialSwitch_UI.Size = new System.Drawing.Size(195, 37);
            this.materialSwitch_UI.TabIndex = 11;
            this.materialSwitch_UI.Text = "Dark / Light Theme";
            this.materialSwitch_UI.UseVisualStyleBackColor = true;
            this.materialSwitch_UI.CheckedChanged += new System.EventHandler(this.materialSwitch_UI_CheckedChanged);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.metroPanel1.Controls.Add(this.materialButton_log);
            this.metroPanel1.Controls.Add(this.materialButton_setting);
            this.metroPanel1.Controls.Add(this.materialButton_curveimage);
            this.metroPanel1.Controls.Add(this.materialButton_analyzedata);
            this.metroPanel1.Controls.Add(this.materialButton_outimage);
            this.metroPanel1.Controls.Add(this.materialButton_LoadData);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 7;
            this.metroPanel1.Location = new System.Drawing.Point(172, 24);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(712, 32);
            this.metroPanel1.TabIndex = 12;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel1.UseCustomBackColor = true;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 7;
            // 
            // materialButton_log
            // 
            this.materialButton_log.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_log.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_log.Depth = 0;
            this.materialButton_log.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialButton_log.HighEmphasis = true;
            this.materialButton_log.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_log.Icon")));
            this.materialButton_log.Location = new System.Drawing.Point(581, 0);
            this.materialButton_log.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_log.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_log.Name = "materialButton_log";
            this.materialButton_log.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_log.Size = new System.Drawing.Size(113, 32);
            this.materialButton_log.TabIndex = 19;
            this.materialButton_log.Text = "操作日志";
            this.materialButton_log.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_log.UseAccentColor = false;
            this.materialButton_log.UseVisualStyleBackColor = true;
            // 
            // materialButton_setting
            // 
            this.materialButton_setting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_setting.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_setting.Depth = 0;
            this.materialButton_setting.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialButton_setting.HighEmphasis = true;
            this.materialButton_setting.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_setting.Icon")));
            this.materialButton_setting.Location = new System.Drawing.Point(468, 0);
            this.materialButton_setting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_setting.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_setting.Name = "materialButton_setting";
            this.materialButton_setting.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_setting.Size = new System.Drawing.Size(113, 32);
            this.materialButton_setting.TabIndex = 18;
            this.materialButton_setting.Text = "系统设置";
            this.materialButton_setting.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_setting.UseAccentColor = false;
            this.materialButton_setting.UseVisualStyleBackColor = true;
            // 
            // materialButton_curveimage
            // 
            this.materialButton_curveimage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_curveimage.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_curveimage.Depth = 0;
            this.materialButton_curveimage.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialButton_curveimage.HighEmphasis = true;
            this.materialButton_curveimage.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_curveimage.Icon")));
            this.materialButton_curveimage.Location = new System.Drawing.Point(339, 0);
            this.materialButton_curveimage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_curveimage.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_curveimage.Name = "materialButton_curveimage";
            this.materialButton_curveimage.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_curveimage.Size = new System.Drawing.Size(129, 32);
            this.materialButton_curveimage.TabIndex = 17;
            this.materialButton_curveimage.Text = "泳道波形图";
            this.materialButton_curveimage.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_curveimage.UseAccentColor = false;
            this.materialButton_curveimage.UseVisualStyleBackColor = true;
            // 
            // materialButton_analyzedata
            // 
            this.materialButton_analyzedata.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_analyzedata.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_analyzedata.Depth = 0;
            this.materialButton_analyzedata.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialButton_analyzedata.HighEmphasis = true;
            this.materialButton_analyzedata.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_analyzedata.Icon")));
            this.materialButton_analyzedata.Location = new System.Drawing.Point(226, 0);
            this.materialButton_analyzedata.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_analyzedata.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_analyzedata.Name = "materialButton_analyzedata";
            this.materialButton_analyzedata.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_analyzedata.Size = new System.Drawing.Size(113, 32);
            this.materialButton_analyzedata.TabIndex = 16;
            this.materialButton_analyzedata.Text = "分析数据";
            this.materialButton_analyzedata.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_analyzedata.UseAccentColor = false;
            this.materialButton_analyzedata.UseVisualStyleBackColor = true;
            // 
            // materialButton_outimage
            // 
            this.materialButton_outimage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_outimage.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_outimage.Depth = 0;
            this.materialButton_outimage.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialButton_outimage.HighEmphasis = true;
            this.materialButton_outimage.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_outimage.Icon")));
            this.materialButton_outimage.Location = new System.Drawing.Point(113, 0);
            this.materialButton_outimage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_outimage.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_outimage.Name = "materialButton_outimage";
            this.materialButton_outimage.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_outimage.Size = new System.Drawing.Size(113, 32);
            this.materialButton_outimage.TabIndex = 15;
            this.materialButton_outimage.Text = "导出图像";
            this.materialButton_outimage.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_outimage.UseAccentColor = false;
            this.materialButton_outimage.UseVisualStyleBackColor = true;
            // 
            // materialButton_LoadData
            // 
            this.materialButton_LoadData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_LoadData.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_LoadData.Depth = 0;
            this.materialButton_LoadData.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialButton_LoadData.HighEmphasis = true;
            this.materialButton_LoadData.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_LoadData.Icon")));
            this.materialButton_LoadData.Location = new System.Drawing.Point(0, 0);
            this.materialButton_LoadData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_LoadData.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_LoadData.Name = "materialButton_LoadData";
            this.materialButton_LoadData.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_LoadData.Size = new System.Drawing.Size(113, 32);
            this.materialButton_LoadData.TabIndex = 14;
            this.materialButton_LoadData.Text = "加载数据";
            this.materialButton_LoadData.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_LoadData.UseAccentColor = false;
            this.materialButton_LoadData.UseVisualStyleBackColor = true;
            // 
            // metroPanel2
            // 
            this.metroPanel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.metroPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel2.Controls.Add(this.materialButton_forward);
            this.metroPanel2.Controls.Add(this.materialButton_return);
            this.metroPanel2.Controls.Add(this.materialButton_save);
            this.metroPanel2.Controls.Add(this.materialButton_inverse);
            this.metroPanel2.Controls.Add(this.materialButton_resetImage);
            this.metroPanel2.Controls.Add(this.materialButton_imageInfo);
            this.metroPanel2.Controls.Add(this.materialButton_fakeColor);
            this.metroPanel2.Controls.Add(this.materialButton_imageChange);
            this.metroPanel2.Controls.Add(this.materialButton_changeFormSize);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 7;
            this.metroPanel2.Location = new System.Drawing.Point(172, 58);
            this.metroPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(712, 31);
            this.metroPanel2.Style = MetroFramework.MetroColorStyle.Silver;
            this.metroPanel2.TabIndex = 13;
            this.metroPanel2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroPanel2.UseCustomBackColor = true;
            this.metroPanel2.UseStyleColors = true;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 7;
            // 
            // materialButton_forward
            // 
            this.materialButton_forward.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_forward.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_forward.Depth = 0;
            this.materialButton_forward.HighEmphasis = true;
            this.materialButton_forward.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_forward.Icon")));
            this.materialButton_forward.Location = new System.Drawing.Point(446, -6);
            this.materialButton_forward.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_forward.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_forward.Name = "materialButton_forward";
            this.materialButton_forward.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_forward.Size = new System.Drawing.Size(64, 36);
            this.materialButton_forward.TabIndex = 23;
            this.materialButton_forward.Text = " ";
            this.materialButton_forward.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton_forward.UseAccentColor = false;
            this.materialButton_forward.UseVisualStyleBackColor = true;
            this.materialButton_forward.MouseMove += new System.Windows.Forms.MouseEventHandler(this.materialButton_forward_MouseMove);
            // 
            // materialButton_return
            // 
            this.materialButton_return.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.materialButton_return.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_return.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_return.Depth = 0;
            this.materialButton_return.HighEmphasis = true;
            this.materialButton_return.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_return.Icon")));
            this.materialButton_return.Location = new System.Drawing.Point(510, -6);
            this.materialButton_return.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_return.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_return.Name = "materialButton_return";
            this.materialButton_return.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_return.Size = new System.Drawing.Size(64, 36);
            this.materialButton_return.TabIndex = 22;
            this.materialButton_return.Text = " ";
            this.materialButton_return.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton_return.UseAccentColor = false;
            this.materialButton_return.UseVisualStyleBackColor = true;
            this.materialButton_return.MouseMove += new System.Windows.Forms.MouseEventHandler(this.materialButton_return_MouseMove);
            // 
            // materialButton_save
            // 
            this.materialButton_save.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_save.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_save.Depth = 0;
            this.materialButton_save.HighEmphasis = true;
            this.materialButton_save.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_save.Icon")));
            this.materialButton_save.Location = new System.Drawing.Point(384, -4);
            this.materialButton_save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_save.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_save.Name = "materialButton_save";
            this.materialButton_save.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_save.Size = new System.Drawing.Size(64, 36);
            this.materialButton_save.TabIndex = 21;
            this.materialButton_save.Text = " ";
            this.materialButton_save.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton_save.UseAccentColor = false;
            this.materialButton_save.UseVisualStyleBackColor = true;
            this.materialButton_save.MouseMove += new System.Windows.Forms.MouseEventHandler(this.materialButton_save_MouseMove);
            // 
            // materialButton_inverse
            // 
            this.materialButton_inverse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_inverse.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_inverse.Depth = 0;
            this.materialButton_inverse.HighEmphasis = true;
            this.materialButton_inverse.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_inverse.Icon")));
            this.materialButton_inverse.Location = new System.Drawing.Point(320, -4);
            this.materialButton_inverse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_inverse.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_inverse.Name = "materialButton_inverse";
            this.materialButton_inverse.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_inverse.Size = new System.Drawing.Size(64, 36);
            this.materialButton_inverse.TabIndex = 20;
            this.materialButton_inverse.Text = " ";
            this.materialButton_inverse.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton_inverse.UseAccentColor = false;
            this.materialButton_inverse.UseVisualStyleBackColor = true;
            this.materialButton_inverse.MouseMove += new System.Windows.Forms.MouseEventHandler(this.materialButton_inverse_MouseMove);
            // 
            // materialButton_resetImage
            // 
            this.materialButton_resetImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_resetImage.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_resetImage.Depth = 0;
            this.materialButton_resetImage.HighEmphasis = true;
            this.materialButton_resetImage.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_resetImage.Icon")));
            this.materialButton_resetImage.Location = new System.Drawing.Point(256, -4);
            this.materialButton_resetImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_resetImage.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_resetImage.Name = "materialButton_resetImage";
            this.materialButton_resetImage.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_resetImage.Size = new System.Drawing.Size(64, 36);
            this.materialButton_resetImage.TabIndex = 19;
            this.materialButton_resetImage.Text = " ";
            this.materialButton_resetImage.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton_resetImage.UseAccentColor = false;
            this.materialButton_resetImage.UseVisualStyleBackColor = true;
            this.materialButton_resetImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.materialButton_resetImage_MouseMove);
            // 
            // materialButton_imageInfo
            // 
            this.materialButton_imageInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_imageInfo.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_imageInfo.Depth = 0;
            this.materialButton_imageInfo.HighEmphasis = true;
            this.materialButton_imageInfo.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_imageInfo.Icon")));
            this.materialButton_imageInfo.Location = new System.Drawing.Point(192, -4);
            this.materialButton_imageInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_imageInfo.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_imageInfo.Name = "materialButton_imageInfo";
            this.materialButton_imageInfo.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_imageInfo.Size = new System.Drawing.Size(64, 36);
            this.materialButton_imageInfo.TabIndex = 18;
            this.materialButton_imageInfo.Text = " ";
            this.materialButton_imageInfo.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton_imageInfo.UseAccentColor = false;
            this.materialButton_imageInfo.UseVisualStyleBackColor = true;
            this.materialButton_imageInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.materialButton_imageInfo_MouseMove);
            // 
            // materialButton_fakeColor
            // 
            this.materialButton_fakeColor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_fakeColor.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_fakeColor.Depth = 0;
            this.materialButton_fakeColor.HighEmphasis = true;
            this.materialButton_fakeColor.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_fakeColor.Icon")));
            this.materialButton_fakeColor.Location = new System.Drawing.Point(128, -4);
            this.materialButton_fakeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_fakeColor.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_fakeColor.Name = "materialButton_fakeColor";
            this.materialButton_fakeColor.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_fakeColor.Size = new System.Drawing.Size(64, 36);
            this.materialButton_fakeColor.TabIndex = 17;
            this.materialButton_fakeColor.Text = " ";
            this.materialButton_fakeColor.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton_fakeColor.UseAccentColor = false;
            this.materialButton_fakeColor.UseVisualStyleBackColor = true;
            this.materialButton_fakeColor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.materialButton_fakeColor_MouseMove);
            // 
            // materialButton_imageChange
            // 
            this.materialButton_imageChange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_imageChange.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_imageChange.Depth = 0;
            this.materialButton_imageChange.HighEmphasis = true;
            this.materialButton_imageChange.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_imageChange.Icon")));
            this.materialButton_imageChange.Location = new System.Drawing.Point(64, -4);
            this.materialButton_imageChange.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_imageChange.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_imageChange.Name = "materialButton_imageChange";
            this.materialButton_imageChange.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_imageChange.Size = new System.Drawing.Size(64, 36);
            this.materialButton_imageChange.TabIndex = 16;
            this.materialButton_imageChange.Text = " ";
            this.materialButton_imageChange.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton_imageChange.UseAccentColor = false;
            this.materialButton_imageChange.UseVisualStyleBackColor = true;
            this.materialButton_imageChange.MouseMove += new System.Windows.Forms.MouseEventHandler(this.materialButton_imageChange_MouseMove);
            // 
            // materialButton_changeFormSize
            // 
            this.materialButton_changeFormSize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_changeFormSize.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.materialButton_changeFormSize.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_changeFormSize.Depth = 0;
            this.materialButton_changeFormSize.HighEmphasis = true;
            this.materialButton_changeFormSize.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_changeFormSize.Icon")));
            this.materialButton_changeFormSize.Location = new System.Drawing.Point(0, -4);
            this.materialButton_changeFormSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialButton_changeFormSize.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_changeFormSize.Name = "materialButton_changeFormSize";
            this.materialButton_changeFormSize.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_changeFormSize.Size = new System.Drawing.Size(64, 36);
            this.materialButton_changeFormSize.TabIndex = 15;
            this.materialButton_changeFormSize.Tag = "123";
            this.materialButton_changeFormSize.Text = " ";
            this.materialButton_changeFormSize.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.materialButton_changeFormSize.UseAccentColor = false;
            this.materialButton_changeFormSize.UseVisualStyleBackColor = false;
            this.materialButton_changeFormSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.materialButton_changeFormSize_MouseMove);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "導出.png");
            this.imageList1.Images.SetKeyName(1, "分析.png");
            this.imageList1.Images.SetKeyName(2, "风控.png");
            this.imageList1.Images.SetKeyName(3, "计数器.png");
            this.imageList1.Images.SetKeyName(4, "胶原蛋白.png");
            // 
            // materialButton_miniAnalyze
            // 
            this.materialButton_miniAnalyze.AutoSize = false;
            this.materialButton_miniAnalyze.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_miniAnalyze.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_miniAnalyze.Depth = 0;
            this.materialButton_miniAnalyze.HighEmphasis = true;
            this.materialButton_miniAnalyze.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_miniAnalyze.Icon")));
            this.materialButton_miniAnalyze.Location = new System.Drawing.Point(3, 254);
            this.materialButton_miniAnalyze.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton_miniAnalyze.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_miniAnalyze.Name = "materialButton_miniAnalyze";
            this.materialButton_miniAnalyze.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_miniAnalyze.Size = new System.Drawing.Size(166, 48);
            this.materialButton_miniAnalyze.TabIndex = 7;
            this.materialButton_miniAnalyze.Text = "微孔版分析";
            this.materialButton_miniAnalyze.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_miniAnalyze.UseAccentColor = false;
            this.materialButton_miniAnalyze.UseVisualStyleBackColor = true;
            // 
            // materialButton_roiAnalyze
            // 
            this.materialButton_roiAnalyze.AutoSize = false;
            this.materialButton_roiAnalyze.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_roiAnalyze.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Normal;
            this.materialButton_roiAnalyze.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_roiAnalyze.Depth = 0;
            this.materialButton_roiAnalyze.HighEmphasis = true;
            this.materialButton_roiAnalyze.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_roiAnalyze.Icon")));
            this.materialButton_roiAnalyze.Location = new System.Drawing.Point(3, 201);
            this.materialButton_roiAnalyze.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton_roiAnalyze.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_roiAnalyze.Name = "materialButton_roiAnalyze";
            this.materialButton_roiAnalyze.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_roiAnalyze.Size = new System.Drawing.Size(166, 48);
            this.materialButton_roiAnalyze.TabIndex = 6;
            this.materialButton_roiAnalyze.Text = "ROIs分析";
            this.materialButton_roiAnalyze.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_roiAnalyze.UseAccentColor = false;
            this.materialButton_roiAnalyze.UseVisualStyleBackColor = true;
            // 
            // materialButton_dotcounts
            // 
            this.materialButton_dotcounts.AutoSize = false;
            this.materialButton_dotcounts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_dotcounts.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_dotcounts.Depth = 0;
            this.materialButton_dotcounts.HighEmphasis = true;
            this.materialButton_dotcounts.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_dotcounts.Icon")));
            this.materialButton_dotcounts.Location = new System.Drawing.Point(3, 307);
            this.materialButton_dotcounts.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton_dotcounts.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_dotcounts.Name = "materialButton_dotcounts";
            this.materialButton_dotcounts.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_dotcounts.Size = new System.Drawing.Size(166, 48);
            this.materialButton_dotcounts.TabIndex = 8;
            this.materialButton_dotcounts.Text = "班点计数";
            this.materialButton_dotcounts.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_dotcounts.UseAccentColor = false;
            this.materialButton_dotcounts.UseVisualStyleBackColor = true;
            // 
            // materialButton_acidAnalyze
            // 
            this.materialButton_acidAnalyze.AutoSize = false;
            this.materialButton_acidAnalyze.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_acidAnalyze.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_acidAnalyze.Depth = 0;
            this.materialButton_acidAnalyze.HighEmphasis = true;
            this.materialButton_acidAnalyze.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_acidAnalyze.Icon")));
            this.materialButton_acidAnalyze.Location = new System.Drawing.Point(3, 149);
            this.materialButton_acidAnalyze.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton_acidAnalyze.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_acidAnalyze.Name = "materialButton_acidAnalyze";
            this.materialButton_acidAnalyze.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_acidAnalyze.Size = new System.Drawing.Size(166, 48);
            this.materialButton_acidAnalyze.TabIndex = 5;
            this.materialButton_acidAnalyze.Text = "核酸分析";
            this.materialButton_acidAnalyze.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_acidAnalyze.UseAccentColor = false;
            this.materialButton_acidAnalyze.UseVisualStyleBackColor = true;
            // 
            // materialButton_correction
            // 
            this.materialButton_correction.AutoSize = false;
            this.materialButton_correction.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_correction.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_correction.Depth = 0;
            this.materialButton_correction.HighEmphasis = true;
            this.materialButton_correction.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_correction.Icon")));
            this.materialButton_correction.Location = new System.Drawing.Point(3, 360);
            this.materialButton_correction.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton_correction.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_correction.Name = "materialButton_correction";
            this.materialButton_correction.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_correction.Size = new System.Drawing.Size(166, 48);
            this.materialButton_correction.TabIndex = 9;
            this.materialButton_correction.Text = "蛋白校正";
            this.materialButton_correction.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_correction.UseAccentColor = false;
            this.materialButton_correction.UseVisualStyleBackColor = true;
            // 
            // materialButton_imageProcess
            // 
            this.materialButton_imageProcess.AutoSize = false;
            this.materialButton_imageProcess.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_imageProcess.BackColor = System.Drawing.SystemColors.Control;
            this.materialButton_imageProcess.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_imageProcess.Depth = 0;
            this.materialButton_imageProcess.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.materialButton_imageProcess.HighEmphasis = true;
            this.materialButton_imageProcess.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_imageProcess.Icon")));
            this.materialButton_imageProcess.Location = new System.Drawing.Point(3, 97);
            this.materialButton_imageProcess.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton_imageProcess.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_imageProcess.Name = "materialButton_imageProcess";
            this.materialButton_imageProcess.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_imageProcess.Size = new System.Drawing.Size(166, 48);
            this.materialButton_imageProcess.TabIndex = 4;
            this.materialButton_imageProcess.Text = "图像处理";
            this.materialButton_imageProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.materialButton_imageProcess.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_imageProcess.UseAccentColor = false;
            this.materialButton_imageProcess.UseVisualStyleBackColor = false;
            // 
            // materialDivider3
            // 
            this.materialDivider3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("materialDivider3.BackgroundImage")));
            this.materialDivider3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.materialDivider3.Depth = 0;
            this.materialDivider3.Location = new System.Drawing.Point(3, 27);
            this.materialDivider3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider3.Name = "materialDivider3";
            this.materialDivider3.Size = new System.Drawing.Size(166, 68);
            this.materialDivider3.TabIndex = 2;
            this.materialDivider3.Text = "Title";
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(3, 97);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(166, 482);
            this.materialDivider1.TabIndex = 0;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(478, 295);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(134, 19);
            this.materialLabel2.TabIndex = 14;
            this.materialLabel2.Text = "Background Image";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1058, 582);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.materialSwitch_UI);
            this.Controls.Add(this.materialButton_correction);
            this.Controls.Add(this.materialButton_dotcounts);
            this.Controls.Add(this.materialButton_miniAnalyze);
            this.Controls.Add(this.materialButton_roiAnalyze);
            this.Controls.Add(this.materialButton_acidAnalyze);
            this.Controls.Add(this.materialButton_imageProcess);
            this.Controls.Add(this.materialDivider3);
            this.Controls.Add(this.materialDivider1);
            this.DrawerAutoHide = false;
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3, 24, 3, 3);
            this.Text = "MainForm";
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialSwitch materialSwitch_UI;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MaterialSkin.Controls.MaterialButton materialButton_LoadData;
        private MaterialSkin.Controls.MaterialButton materialButton_log;
        private MaterialSkin.Controls.MaterialButton materialButton_setting;
        private MaterialSkin.Controls.MaterialButton materialButton_curveimage;
        private MaterialSkin.Controls.MaterialButton materialButton_analyzedata;
        private MaterialSkin.Controls.MaterialButton materialButton_outimage;
        private System.Windows.Forms.ImageList imageList1;
        private MaterialSkin.Controls.MaterialButton materialButton_changeFormSize;
        private MaterialSkin.Controls.MaterialButton materialButton_forward;
        private MaterialSkin.Controls.MaterialButton materialButton_return;
        private MaterialSkin.Controls.MaterialButton materialButton_save;
        private MaterialSkin.Controls.MaterialButton materialButton_inverse;
        private MaterialSkin.Controls.MaterialButton materialButton_resetImage;
        private MaterialSkin.Controls.MaterialButton materialButton_imageInfo;
        private MaterialSkin.Controls.MaterialButton materialButton_fakeColor;
        private MaterialSkin.Controls.MaterialButton materialButton_imageChange;
        private System.Windows.Forms.ToolTip btnStartUpToolTip;
        private MaterialSkin.Controls.MaterialButton materialButton_miniAnalyze;
        private MaterialSkin.Controls.MaterialButton materialButton_roiAnalyze;
        private MaterialSkin.Controls.MaterialButton materialButton_dotcounts;
        private MaterialSkin.Controls.MaterialButton materialButton_acidAnalyze;
        private MaterialSkin.Controls.MaterialButton materialButton_correction;
        private MaterialSkin.Controls.MaterialButton materialButton_imageProcess;
        private MaterialSkin.Controls.MaterialDivider materialDivider3;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
    }
}