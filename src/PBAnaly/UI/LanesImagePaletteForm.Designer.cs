namespace PBAnaly.UI
{
    partial class LanesImagePaletteForm
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
            this.collapse1 = new AntdUI.Collapse();
            this.clasi_lanes = new AntdUI.CollapseItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mb_updateInitWell = new MaterialSkin.Controls.MaterialButton();
            this.mb_findLanes = new MaterialSkin.Controls.MaterialButton();
            this.mb_addLanes = new MaterialSkin.Controls.MaterialButton();
            this.mb_deleteLane = new MaterialSkin.Controls.MaterialButton();
            this.panel1 = new AntdUI.Panel();
            this.nud_lane_fixedWidth = new System.Windows.Forms.NumericUpDown();
            this.cb_alwaysShowLane = new AntdUI.Checkbox();
            this.lb_lane_width = new AntdUI.Label();
            this.cb_lane_width = new AntdUI.Checkbox();
            this.clasi_strips = new AntdUI.CollapseItem();
            this.clasi_conformity = new AntdUI.CollapseItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.mb_addBands = new MaterialSkin.Controls.MaterialButton();
            this.mb_deleteBands = new MaterialSkin.Controls.MaterialButton();
            this.mb_findBands = new MaterialSkin.Controls.MaterialButton();
            this.panel2 = new AntdUI.Panel();
            this.cb_alwaysShowBands = new AntdUI.Checkbox();
            this.collapse1.SuspendLayout();
            this.clasi_lanes.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_lane_fixedWidth)).BeginInit();
            this.clasi_strips.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // collapse1
            // 
            this.collapse1.BackColor = System.Drawing.SystemColors.Control;
            this.collapse1.BadgeOffsetX = 10;
            this.collapse1.BadgeOffsetY = 100;
            this.collapse1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.collapse1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.collapse1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collapse1.HeaderBg = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(163)))), ((int)(((byte)(168)))));
            this.collapse1.Items.Add(this.clasi_lanes);
            this.collapse1.Items.Add(this.clasi_strips);
            this.collapse1.Items.Add(this.clasi_conformity);
            this.collapse1.Location = new System.Drawing.Point(0, 0);
            this.collapse1.Margin = new System.Windows.Forms.Padding(2);
            this.collapse1.Name = "collapse1";
            this.collapse1.Size = new System.Drawing.Size(304, 495);
            this.collapse1.TabIndex = 0;
            this.collapse1.Text = "collapse1";
            this.collapse1.Unique = true;
            // 
            // clasi_lanes
            // 
            this.clasi_lanes.Controls.Add(this.tableLayoutPanel1);
            this.clasi_lanes.Location = new System.Drawing.Point(-268, -175);
            this.clasi_lanes.Margin = new System.Windows.Forms.Padding(2);
            this.clasi_lanes.Name = "clasi_lanes";
            this.clasi_lanes.Size = new System.Drawing.Size(268, 175);
            this.clasi_lanes.TabIndex = 0;
            this.clasi_lanes.Text = "泳道";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Controls.Add(this.mb_updateInitWell, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.mb_findLanes, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mb_addLanes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mb_deleteLane, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(268, 175);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // mb_updateInitWell
            // 
            this.mb_updateInitWell.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mb_updateInitWell.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.mb_updateInitWell.Depth = 0;
            this.mb_updateInitWell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_updateInitWell.HighEmphasis = true;
            this.mb_updateInitWell.Icon = null;
            this.mb_updateInitWell.Location = new System.Drawing.Point(3, 113);
            this.mb_updateInitWell.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mb_updateInitWell.MouseState = MaterialSkin.MouseState.HOVER;
            this.mb_updateInitWell.Name = "mb_updateInitWell";
            this.mb_updateInitWell.NoAccentTextColor = System.Drawing.Color.Empty;
            this.mb_updateInitWell.Size = new System.Drawing.Size(76, 26);
            this.mb_updateInitWell.TabIndex = 8;
            this.mb_updateInitWell.Text = "修改初始井";
            this.mb_updateInitWell.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.mb_updateInitWell.UseAccentColor = false;
            this.mb_updateInitWell.UseVisualStyleBackColor = true;
            // 
            // mb_findLanes
            // 
            this.mb_findLanes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mb_findLanes.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.mb_findLanes.Depth = 0;
            this.mb_findLanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_findLanes.HighEmphasis = true;
            this.mb_findLanes.Icon = null;
            this.mb_findLanes.Location = new System.Drawing.Point(3, 5);
            this.mb_findLanes.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mb_findLanes.MouseState = MaterialSkin.MouseState.HOVER;
            this.mb_findLanes.Name = "mb_findLanes";
            this.mb_findLanes.NoAccentTextColor = System.Drawing.Color.Empty;
            this.mb_findLanes.Size = new System.Drawing.Size(76, 26);
            this.mb_findLanes.TabIndex = 0;
            this.mb_findLanes.Text = "查找泳道";
            this.mb_findLanes.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.mb_findLanes.UseAccentColor = false;
            this.mb_findLanes.UseVisualStyleBackColor = true;
            // 
            // mb_addLanes
            // 
            this.mb_addLanes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mb_addLanes.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.mb_addLanes.Depth = 0;
            this.mb_addLanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_addLanes.HighEmphasis = true;
            this.mb_addLanes.Icon = null;
            this.mb_addLanes.Location = new System.Drawing.Point(3, 41);
            this.mb_addLanes.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mb_addLanes.MouseState = MaterialSkin.MouseState.HOVER;
            this.mb_addLanes.Name = "mb_addLanes";
            this.mb_addLanes.NoAccentTextColor = System.Drawing.Color.Empty;
            this.mb_addLanes.Size = new System.Drawing.Size(76, 26);
            this.mb_addLanes.TabIndex = 1;
            this.mb_addLanes.Text = "添加泳道";
            this.mb_addLanes.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.mb_addLanes.UseAccentColor = false;
            this.mb_addLanes.UseVisualStyleBackColor = true;
            // 
            // mb_deleteLane
            // 
            this.mb_deleteLane.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mb_deleteLane.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.mb_deleteLane.Depth = 0;
            this.mb_deleteLane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_deleteLane.HighEmphasis = true;
            this.mb_deleteLane.Icon = null;
            this.mb_deleteLane.Location = new System.Drawing.Point(3, 77);
            this.mb_deleteLane.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mb_deleteLane.MouseState = MaterialSkin.MouseState.HOVER;
            this.mb_deleteLane.Name = "mb_deleteLane";
            this.mb_deleteLane.NoAccentTextColor = System.Drawing.Color.Empty;
            this.mb_deleteLane.Size = new System.Drawing.Size(76, 26);
            this.mb_deleteLane.TabIndex = 2;
            this.mb_deleteLane.Text = "删除泳道";
            this.mb_deleteLane.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.mb_deleteLane.UseAccentColor = false;
            this.mb_deleteLane.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Back = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 4);
            this.panel1.Controls.Add(this.nud_lane_fixedWidth);
            this.panel1.Controls.Add(this.cb_alwaysShowLane);
            this.panel1.Controls.Add(this.lb_lane_width);
            this.panel1.Controls.Add(this.cb_lane_width);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(84, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 5);
            this.panel1.Size = new System.Drawing.Size(160, 171);
            this.panel1.TabIndex = 4;
            this.panel1.Text = "panel1";
            // 
            // nud_lane_fixedWidth
            // 
            this.nud_lane_fixedWidth.Location = new System.Drawing.Point(3, 52);
            this.nud_lane_fixedWidth.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nud_lane_fixedWidth.Name = "nud_lane_fixedWidth";
            this.nud_lane_fixedWidth.Size = new System.Drawing.Size(76, 21);
            this.nud_lane_fixedWidth.TabIndex = 0;
            this.nud_lane_fixedWidth.Value = new decimal(new int[] {
            44,
            0,
            0,
            0});
            // 
            // cb_alwaysShowLane
            // 
            this.cb_alwaysShowLane.AutoCheck = true;
            this.cb_alwaysShowLane.BackColor = System.Drawing.Color.Transparent;
            this.cb_alwaysShowLane.Checked = true;
            this.cb_alwaysShowLane.Location = new System.Drawing.Point(0, 85);
            this.cb_alwaysShowLane.Margin = new System.Windows.Forms.Padding(2);
            this.cb_alwaysShowLane.Name = "cb_alwaysShowLane";
            this.cb_alwaysShowLane.Size = new System.Drawing.Size(106, 18);
            this.cb_alwaysShowLane.TabIndex = 8;
            this.cb_alwaysShowLane.Text = "总是显示泳道";
            // 
            // lb_lane_width
            // 
            this.lb_lane_width.BackColor = System.Drawing.Color.Transparent;
            this.lb_lane_width.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_lane_width.Location = new System.Drawing.Point(2, 27);
            this.lb_lane_width.Margin = new System.Windows.Forms.Padding(2);
            this.lb_lane_width.Name = "lb_lane_width";
            this.lb_lane_width.Size = new System.Drawing.Size(114, 25);
            this.lb_lane_width.TabIndex = 7;
            this.lb_lane_width.Text = "泳道宽度(像素)";
            // 
            // cb_lane_width
            // 
            this.cb_lane_width.AutoCheck = true;
            this.cb_lane_width.BackColor = System.Drawing.Color.Transparent;
            this.cb_lane_width.Location = new System.Drawing.Point(2, 6);
            this.cb_lane_width.Margin = new System.Windows.Forms.Padding(2);
            this.cb_lane_width.Name = "cb_lane_width";
            this.cb_lane_width.Size = new System.Drawing.Size(106, 18);
            this.cb_lane_width.TabIndex = 0;
            this.cb_lane_width.Text = "统一泳道宽度";
            // 
            // clasi_strips
            // 
            this.clasi_strips.Controls.Add(this.tableLayoutPanel2);
            this.clasi_strips.Location = new System.Drawing.Point(-268, -186);
            this.clasi_strips.Margin = new System.Windows.Forms.Padding(2);
            this.clasi_strips.Name = "clasi_strips";
            this.clasi_strips.Size = new System.Drawing.Size(268, 186);
            this.clasi_strips.TabIndex = 2;
            this.clasi_strips.Text = "条带";
            // 
            // clasi_conformity
            // 
            this.clasi_conformity.Location = new System.Drawing.Point(-268, -48);
            this.clasi_conformity.Margin = new System.Windows.Forms.Padding(2);
            this.clasi_conformity.Name = "clasi_conformity";
            this.clasi_conformity.Size = new System.Drawing.Size(268, 48);
            this.clasi_conformity.TabIndex = 3;
            this.clasi_conformity.Text = "整合";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.Controls.Add(this.mb_addBands, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.mb_deleteBands, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.mb_findBands, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(268, 186);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // mb_addBands
            // 
            this.mb_addBands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mb_addBands.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.mb_addBands.Depth = 0;
            this.mb_addBands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_addBands.HighEmphasis = true;
            this.mb_addBands.Icon = null;
            this.mb_addBands.Location = new System.Drawing.Point(3, 5);
            this.mb_addBands.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mb_addBands.MouseState = MaterialSkin.MouseState.HOVER;
            this.mb_addBands.Name = "mb_addBands";
            this.mb_addBands.NoAccentTextColor = System.Drawing.Color.Empty;
            this.mb_addBands.Size = new System.Drawing.Size(76, 26);
            this.mb_addBands.TabIndex = 0;
            this.mb_addBands.Text = "添加条带";
            this.mb_addBands.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.mb_addBands.UseAccentColor = false;
            this.mb_addBands.UseVisualStyleBackColor = true;
            // 
            // mb_deleteBands
            // 
            this.mb_deleteBands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mb_deleteBands.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.mb_deleteBands.Depth = 0;
            this.mb_deleteBands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_deleteBands.HighEmphasis = true;
            this.mb_deleteBands.Icon = null;
            this.mb_deleteBands.Location = new System.Drawing.Point(3, 41);
            this.mb_deleteBands.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mb_deleteBands.MouseState = MaterialSkin.MouseState.HOVER;
            this.mb_deleteBands.Name = "mb_deleteBands";
            this.mb_deleteBands.NoAccentTextColor = System.Drawing.Color.Empty;
            this.mb_deleteBands.Size = new System.Drawing.Size(76, 26);
            this.mb_deleteBands.TabIndex = 1;
            this.mb_deleteBands.Text = "删除条带";
            this.mb_deleteBands.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.mb_deleteBands.UseAccentColor = false;
            this.mb_deleteBands.UseVisualStyleBackColor = true;
            // 
            // mb_findBands
            // 
            this.mb_findBands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mb_findBands.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.mb_findBands.Depth = 0;
            this.mb_findBands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mb_findBands.HighEmphasis = true;
            this.mb_findBands.Icon = null;
            this.mb_findBands.Location = new System.Drawing.Point(3, 77);
            this.mb_findBands.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.mb_findBands.MouseState = MaterialSkin.MouseState.HOVER;
            this.mb_findBands.Name = "mb_findBands";
            this.mb_findBands.NoAccentTextColor = System.Drawing.Color.Empty;
            this.mb_findBands.Size = new System.Drawing.Size(76, 26);
            this.mb_findBands.TabIndex = 2;
            this.mb_findBands.Text = "查找条带";
            this.mb_findBands.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.mb_findBands.UseAccentColor = false;
            this.mb_findBands.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Back = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.SetColumnSpan(this.panel2, 4);
            this.panel2.Controls.Add(this.cb_alwaysShowBands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(84, 2);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel2.SetRowSpan(this.panel2, 5);
            this.panel2.Size = new System.Drawing.Size(160, 182);
            this.panel2.TabIndex = 4;
            this.panel2.Text = "panel2";
            // 
            // cb_alwaysShowBands
            // 
            this.cb_alwaysShowBands.AutoCheck = true;
            this.cb_alwaysShowBands.BackColor = System.Drawing.Color.Transparent;
            this.cb_alwaysShowBands.Checked = true;
            this.cb_alwaysShowBands.Location = new System.Drawing.Point(2, 29);
            this.cb_alwaysShowBands.Margin = new System.Windows.Forms.Padding(2);
            this.cb_alwaysShowBands.Name = "cb_alwaysShowBands";
            this.cb_alwaysShowBands.Size = new System.Drawing.Size(106, 18);
            this.cb_alwaysShowBands.TabIndex = 8;
            this.cb_alwaysShowBands.Text = "总是显示条带";
            // 
            // LanesImagePaletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 495);
            this.Controls.Add(this.collapse1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LanesImagePaletteForm";
            this.Text = "BioanayImagePaletteForm";
            this.collapse1.ResumeLayout(false);
            this.clasi_lanes.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_lane_fixedWidth)).EndInit();
            this.clasi_strips.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Collapse collapse1;
        private AntdUI.CollapseItem clasi_lanes;
        private AntdUI.CollapseItem clasi_strips;
        private AntdUI.CollapseItem clasi_conformity;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private AntdUI.Panel panel1;
        private AntdUI.Label lb_lane_width;
        public MaterialSkin.Controls.MaterialButton mb_findLanes;
        public System.Windows.Forms.NumericUpDown nud_lane_fixedWidth;
        public AntdUI.Checkbox cb_lane_width;
        public MaterialSkin.Controls.MaterialButton mb_addLanes;
        public AntdUI.Checkbox cb_alwaysShowLane;
        public MaterialSkin.Controls.MaterialButton mb_deleteLane;
        public MaterialSkin.Controls.MaterialButton mb_updateInitWell;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public MaterialSkin.Controls.MaterialButton mb_addBands;
        public MaterialSkin.Controls.MaterialButton mb_deleteBands;
        public MaterialSkin.Controls.MaterialButton mb_findBands;
        private AntdUI.Panel panel2;
        public AntdUI.Checkbox cb_alwaysShowBands;
    }
}