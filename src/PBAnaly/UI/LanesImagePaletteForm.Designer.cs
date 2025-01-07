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
            this.mb_findLanes = new MaterialSkin.Controls.MaterialButton();
            this.materialButton2 = new MaterialSkin.Controls.MaterialButton();
            this.materialButton3 = new MaterialSkin.Controls.MaterialButton();
            this.materialButton4 = new MaterialSkin.Controls.MaterialButton();
            this.panel1 = new AntdUI.Panel();
            this.label1 = new AntdUI.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkbox1 = new AntdUI.Checkbox();
            this.clasi_init = new AntdUI.CollapseItem();
            this.materialCheckbox1 = new MaterialSkin.Controls.MaterialCheckbox();
            this.clasi_strips = new AntdUI.CollapseItem();
            this.clasi_conformity = new AntdUI.CollapseItem();
            this.checkbox2 = new AntdUI.Checkbox();
            this.collapse1.SuspendLayout();
            this.clasi_lanes.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.clasi_init.SuspendLayout();
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
            this.collapse1.Items.Add(this.clasi_init);
            this.collapse1.Items.Add(this.clasi_strips);
            this.collapse1.Items.Add(this.clasi_conformity);
            this.collapse1.Location = new System.Drawing.Point(0, 0);
            this.collapse1.Margin = new System.Windows.Forms.Padding(2);
            this.collapse1.Name = "collapse1";
            this.collapse1.Size = new System.Drawing.Size(304, 495);
            this.collapse1.TabIndex = 0;
            this.collapse1.Text = "collapse1";
            // 
            // clasi_lanes
            // 
            this.clasi_lanes.Controls.Add(this.tableLayoutPanel1);
            this.clasi_lanes.Expand = true;
            this.clasi_lanes.Location = new System.Drawing.Point(18, 58);
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
            this.tableLayoutPanel1.Controls.Add(this.mb_findLanes, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.materialButton2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.materialButton3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.materialButton4, 0, 3);
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
            // materialButton2
            // 
            this.materialButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton2.Depth = 0;
            this.materialButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialButton2.HighEmphasis = true;
            this.materialButton2.Icon = null;
            this.materialButton2.Location = new System.Drawing.Point(3, 41);
            this.materialButton2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.materialButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton2.Name = "materialButton2";
            this.materialButton2.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton2.Size = new System.Drawing.Size(76, 26);
            this.materialButton2.TabIndex = 1;
            this.materialButton2.Text = "添加泳道";
            this.materialButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton2.UseAccentColor = false;
            this.materialButton2.UseVisualStyleBackColor = true;
            // 
            // materialButton3
            // 
            this.materialButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton3.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton3.Depth = 0;
            this.materialButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialButton3.HighEmphasis = true;
            this.materialButton3.Icon = null;
            this.materialButton3.Location = new System.Drawing.Point(3, 77);
            this.materialButton3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.materialButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton3.Name = "materialButton3";
            this.materialButton3.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton3.Size = new System.Drawing.Size(76, 26);
            this.materialButton3.TabIndex = 2;
            this.materialButton3.Text = "删除泳道";
            this.materialButton3.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton3.UseAccentColor = false;
            this.materialButton3.UseVisualStyleBackColor = true;
            // 
            // materialButton4
            // 
            this.materialButton4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton4.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton4.Depth = 0;
            this.materialButton4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialButton4.HighEmphasis = true;
            this.materialButton4.Icon = null;
            this.materialButton4.Location = new System.Drawing.Point(3, 113);
            this.materialButton4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.materialButton4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton4.Name = "materialButton4";
            this.materialButton4.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton4.Size = new System.Drawing.Size(76, 26);
            this.materialButton4.TabIndex = 3;
            this.materialButton4.Text = "弯曲泳道";
            this.materialButton4.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton4.UseAccentColor = false;
            this.materialButton4.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 4);
            this.panel1.Controls.Add(this.checkbox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.checkbox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(84, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 4);
            this.panel1.Size = new System.Drawing.Size(160, 140);
            this.panel1.TabIndex = 4;
            this.panel1.Text = "panel1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "泳道宽度(像素)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(2, 69);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(57, 21);
            this.numericUpDown1.TabIndex = 6;
            // 
            // checkbox1
            // 
            this.checkbox1.AutoCheck = true;
            this.checkbox1.Location = new System.Drawing.Point(2, 11);
            this.checkbox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkbox1.Name = "checkbox1";
            this.checkbox1.Size = new System.Drawing.Size(106, 18);
            this.checkbox1.TabIndex = 0;
            this.checkbox1.Text = "统一泳道宽度";
            // 
            // clasi_init
            // 
            this.clasi_init.Controls.Add(this.materialCheckbox1);
            this.clasi_init.Expand = true;
            this.clasi_init.Location = new System.Drawing.Point(18, 305);
            this.clasi_init.Margin = new System.Windows.Forms.Padding(2);
            this.clasi_init.Name = "clasi_init";
            this.clasi_init.Size = new System.Drawing.Size(268, 162);
            this.clasi_init.TabIndex = 1;
            this.clasi_init.Text = "初始井";
            // 
            // materialCheckbox1
            // 
            this.materialCheckbox1.AutoSize = true;
            this.materialCheckbox1.Depth = 0;
            this.materialCheckbox1.Location = new System.Drawing.Point(120, 31);
            this.materialCheckbox1.Margin = new System.Windows.Forms.Padding(0);
            this.materialCheckbox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox1.Name = "materialCheckbox1";
            this.materialCheckbox1.ReadOnly = false;
            this.materialCheckbox1.Ripple = true;
            this.materialCheckbox1.Size = new System.Drawing.Size(131, 37);
            this.materialCheckbox1.TabIndex = 4;
            this.materialCheckbox1.Text = "统一泳道宽度";
            this.materialCheckbox1.UseVisualStyleBackColor = true;
            // 
            // clasi_strips
            // 
            this.clasi_strips.Location = new System.Drawing.Point(-75, -48);
            this.clasi_strips.Margin = new System.Windows.Forms.Padding(2);
            this.clasi_strips.Name = "clasi_strips";
            this.clasi_strips.Size = new System.Drawing.Size(75, 48);
            this.clasi_strips.TabIndex = 2;
            this.clasi_strips.Text = "条带";
            // 
            // clasi_conformity
            // 
            this.clasi_conformity.Location = new System.Drawing.Point(-75, -48);
            this.clasi_conformity.Margin = new System.Windows.Forms.Padding(2);
            this.clasi_conformity.Name = "clasi_conformity";
            this.clasi_conformity.Size = new System.Drawing.Size(75, 48);
            this.clasi_conformity.TabIndex = 3;
            this.clasi_conformity.Text = "整合";
            // 
            // checkbox2
            // 
            this.checkbox2.AutoCheck = true;
            this.checkbox2.Location = new System.Drawing.Point(0, 111);
            this.checkbox2.Margin = new System.Windows.Forms.Padding(2);
            this.checkbox2.Name = "checkbox2";
            this.checkbox2.Size = new System.Drawing.Size(106, 18);
            this.checkbox2.TabIndex = 8;
            this.checkbox2.Text = "总是显示泳道";
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.clasi_init.ResumeLayout(false);
            this.clasi_init.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Collapse collapse1;
        private AntdUI.CollapseItem clasi_lanes;
        private AntdUI.CollapseItem clasi_init;
        private AntdUI.CollapseItem clasi_strips;
        private AntdUI.CollapseItem clasi_conformity;
        private MaterialSkin.Controls.MaterialButton materialButton4;
        private MaterialSkin.Controls.MaterialButton materialButton3;
        private MaterialSkin.Controls.MaterialButton materialButton2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private MaterialSkin.Controls.MaterialCheckbox materialCheckbox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private AntdUI.Panel panel1;
        private AntdUI.Checkbox checkbox1;
        private AntdUI.Label label1;
        public MaterialSkin.Controls.MaterialButton mb_findLanes;
        private AntdUI.Checkbox checkbox2;
    }
}