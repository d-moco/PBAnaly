﻿namespace PBAnaly.UI
{
    partial class BioanalyImagePanel
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
            this.windowBar1 = new AntdUI.WindowBar();
            this.panel1 = new AntdUI.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new AntdUI.Panel();
            this.cbb_mode = new System.Windows.Forms.ComboBox();
            this.label1 = new AntdUI.Label();
            this.cb_scientific = new AntdUI.Checkbox();
            this.flowPanel1 = new AntdUI.FlowPanel();
            this.ava_save = new AntdUI.Avatar();
            this.ava_zoom_out = new AntdUI.Avatar();
            this.ava__zoom_in = new AntdUI.Avatar();
            this.ava_auto = new AntdUI.Avatar();
            this.panel3 = new AntdUI.Panel();
            this.lb_size = new AntdUI.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.image_pr = new AntdUI.Image3D();
            this.pl_panel_image = new AntdUI.Panel();
            this.pl_bg_panel = new AntdUI.Panel();
            this.image_pl = new AntdUI.Image3D();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pl_panel_image.SuspendLayout();
            this.pl_bg_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowBar1
            // 
            this.windowBar1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.windowBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowBar1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.windowBar1.IsMax = false;
            this.windowBar1.Location = new System.Drawing.Point(3, 3);
            this.windowBar1.Name = "windowBar1";
            this.windowBar1.ShowIcon = false;
            this.windowBar1.Size = new System.Drawing.Size(352, 23);
            this.windowBar1.TabIndex = 0;
            this.windowBar1.Text = "  ";
            this.windowBar1.UseSystemStyleColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 23);
            this.panel1.TabIndex = 1;
            this.panel1.Text = "panel1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_scientific, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowPanel1, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(352, 23);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbb_mode);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(131, 23);
            this.panel2.TabIndex = 0;
            this.panel2.Text = "panel2";
            // 
            // cbb_mode
            // 
            this.cbb_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_mode.FormattingEnabled = true;
            this.cbb_mode.Items.AddRange(new object[] {
            "merge",
            "mark",
            "pseudocolor"});
            this.cbb_mode.Location = new System.Drawing.Point(45, 1);
            this.cbb_mode.Name = "cbb_mode";
            this.cbb_mode.Size = new System.Drawing.Size(83, 20);
            this.cbb_mode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "模式:";
            // 
            // cb_scientific
            // 
            this.cb_scientific.AutoCheck = true;
            this.cb_scientific.Location = new System.Drawing.Point(134, 3);
            this.cb_scientific.Name = "cb_scientific";
            this.cb_scientific.Size = new System.Drawing.Size(63, 17);
            this.cb_scientific.TabIndex = 1;
            this.cb_scientific.Text = "光子数";
            // 
            // flowPanel1
            // 
            this.flowPanel1.Controls.Add(this.ava_save);
            this.flowPanel1.Controls.Add(this.ava_zoom_out);
            this.flowPanel1.Controls.Add(this.ava__zoom_in);
            this.flowPanel1.Controls.Add(this.ava_auto);
            this.flowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel1.Location = new System.Drawing.Point(227, 0);
            this.flowPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowPanel1.Name = "flowPanel1";
            this.flowPanel1.Size = new System.Drawing.Size(125, 23);
            this.flowPanel1.TabIndex = 2;
            this.flowPanel1.Text = "flowPanel1";
            // 
            // ava_save
            // 
            this.ava_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ava_save.Image = global::PBAnaly.Properties.Resources.保存图片;
            this.ava_save.ImageFit = AntdUI.TFit.Contain;
            this.ava_save.Location = new System.Drawing.Point(96, 3);
            this.ava_save.Name = "ava_save";
            this.ava_save.Size = new System.Drawing.Size(25, 17);
            this.ava_save.TabIndex = 6;
            this.ava_save.Text = "a";
            // 
            // ava_zoom_out
            // 
            this.ava_zoom_out.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ava_zoom_out.Image = global::PBAnaly.Properties.Resources.缩小;
            this.ava_zoom_out.ImageFit = AntdUI.TFit.Contain;
            this.ava_zoom_out.Location = new System.Drawing.Point(65, 3);
            this.ava_zoom_out.Name = "ava_zoom_out";
            this.ava_zoom_out.Size = new System.Drawing.Size(25, 17);
            this.ava_zoom_out.TabIndex = 5;
            this.ava_zoom_out.Text = "a";
            this.ava_zoom_out.Click += new System.EventHandler(this.ava_zoom_out_Click);
            // 
            // ava__zoom_in
            // 
            this.ava__zoom_in.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ava__zoom_in.Image = global::PBAnaly.Properties.Resources.放大;
            this.ava__zoom_in.ImageFit = AntdUI.TFit.Contain;
            this.ava__zoom_in.Location = new System.Drawing.Point(34, 3);
            this.ava__zoom_in.Name = "ava__zoom_in";
            this.ava__zoom_in.Size = new System.Drawing.Size(25, 17);
            this.ava__zoom_in.TabIndex = 4;
            this.ava__zoom_in.Text = "";
            this.ava__zoom_in.Click += new System.EventHandler(this.ava__zoom_in_Click);
            // 
            // ava_auto
            // 
            this.ava_auto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ava_auto.HandCursor = System.Windows.Forms.Cursors.IBeam;
            this.ava_auto.Image = global::PBAnaly.Properties.Resources.全屏;
            this.ava_auto.ImageFit = AntdUI.TFit.Contain;
            this.ava_auto.Location = new System.Drawing.Point(3, 3);
            this.ava_auto.Name = "ava_auto";
            this.ava_auto.Size = new System.Drawing.Size(25, 17);
            this.ava_auto.TabIndex = 3;
            this.ava_auto.Text = "a";
            this.ava_auto.Click += new System.EventHandler(this.ava_auto_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lb_size);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 292);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(352, 13);
            this.panel3.TabIndex = 2;
            this.panel3.Text = "panel3";
            // 
            // lb_size
            // 
            this.lb_size.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lb_size.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_size.Location = new System.Drawing.Point(0, 0);
            this.lb_size.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.lb_size.Name = "lb_size";
            this.lb_size.Size = new System.Drawing.Size(352, 13);
            this.lb_size.TabIndex = 0;
            this.lb_size.Text = "800x600";
            this.lb_size.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel2.Controls.Add(this.image_pr, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.pl_panel_image, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 49);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(352, 243);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // image_pr
            // 
            this.image_pr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image_pr.Duration = 0;
            this.image_pr.ImageFit = AntdUI.TFit.Fill;
            this.image_pr.Location = new System.Drawing.Point(289, 3);
            this.image_pr.Name = "image_pr";
            this.image_pr.Size = new System.Drawing.Size(60, 237);
            this.image_pr.Speed = 1;
            this.image_pr.TabIndex = 1;
            this.image_pr.Text = "image3D1";
            // 
            // pl_panel_image
            // 
            this.pl_panel_image.Back = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(6)))), ((int)(((byte)(6)))));
            this.pl_panel_image.Controls.Add(this.pl_bg_panel);
            this.pl_panel_image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_panel_image.Location = new System.Drawing.Point(3, 3);
            this.pl_panel_image.Name = "pl_panel_image";
            this.pl_panel_image.Size = new System.Drawing.Size(280, 237);
            this.pl_panel_image.TabIndex = 0;
            this.pl_panel_image.Text = "panel4";
            // 
            // pl_bg_panel
            // 
            this.pl_bg_panel.Controls.Add(this.image_pl);
            this.pl_bg_panel.Location = new System.Drawing.Point(0, 0);
            this.pl_bg_panel.Name = "pl_bg_panel";
            this.pl_bg_panel.Size = new System.Drawing.Size(223, 185);
            this.pl_bg_panel.TabIndex = 0;
            this.pl_bg_panel.Text = "panel4";
            // 
            // image_pl
            // 
            this.image_pl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image_pl.Duration = 0;
            this.image_pl.ImageFit = AntdUI.TFit.Fill;
            this.image_pl.Location = new System.Drawing.Point(0, 0);
            this.image_pl.Name = "image_pl";
            this.image_pl.Size = new System.Drawing.Size(223, 185);
            this.image_pl.Speed = 1;
            this.image_pl.TabIndex = 0;
            this.image_pl.Text = "image3D1";
            // 
            // BioanalyImagePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 308);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BioanalyImagePanel";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "BioanalyImagePanel";
            this.MouseEnter += new System.EventHandler(this.BioanalyImagePanel_MouseEnter);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.flowPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pl_panel_image.ResumeLayout(false);
            this.pl_bg_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.WindowBar windowBar1;
        private AntdUI.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private AntdUI.Panel panel2;
        private AntdUI.Label label1;
        private AntdUI.FlowPanel flowPanel1;
        private AntdUI.Avatar ava_auto;
        private AntdUI.Panel panel3;
        private AntdUI.Label lb_size;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.ComboBox cbb_mode;
        private AntdUI.Image3D image_pr;
        public AntdUI.Checkbox cb_scientific;
        public AntdUI.Avatar ava_save;
        public AntdUI.Avatar ava_zoom_out;
        public AntdUI.Avatar ava__zoom_in;
        public AntdUI.Panel pl_bg_panel;
        public AntdUI.Image3D image_pl;
        public AntdUI.Panel pl_panel_image;
    }
}