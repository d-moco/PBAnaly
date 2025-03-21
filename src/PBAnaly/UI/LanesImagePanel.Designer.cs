﻿namespace PBAnaly.UI
{
    partial class LanesImagePanel
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
            this.wdb_title = new AntdUI.WindowBar();
            this.panel1 = new AntdUI.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new AntdUI.Panel();
            this.lb_imageIndex = new AntdUI.Label();
            this.flowPanel1 = new AntdUI.FlowPanel();
            this.ava_saveReport = new AntdUI.Avatar();
            this.ava_save = new AntdUI.Avatar();
            this.ava_zoom_out = new AntdUI.Avatar();
            this.ava__zoom_in = new AntdUI.Avatar();
            this.ava_auto = new AntdUI.Avatar();
            this.pl_bottom = new AntdUI.Panel();
            this.tlp_bottom_panel = new System.Windows.Forms.TableLayoutPanel();
            this.lb_name = new AntdUI.Label();
            this.lb_size = new AntdUI.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pl_panel_image = new AntdUI.Panel();
            this.pl_bg_panel = new AntdUI.Panel();
            this.image_pl = new System.Windows.Forms.PictureBox();
            this.ctms_strop = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctms_strop_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_strop_stickup = new System.Windows.Forms.ToolStripMenuItem();
            this.ctms_strop_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowPanel1.SuspendLayout();
            this.pl_bottom.SuspendLayout();
            this.tlp_bottom_panel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pl_panel_image.SuspendLayout();
            this.pl_bg_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image_pl)).BeginInit();
            this.ctms_strop.SuspendLayout();
            this.SuspendLayout();
            // 
            // wdb_title
            // 
            this.wdb_title.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.wdb_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.wdb_title.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.wdb_title.IsMax = false;
            this.wdb_title.Location = new System.Drawing.Point(3, 3);
            this.wdb_title.MinimizeBox = false;
            this.wdb_title.Name = "wdb_title";
            this.wdb_title.ShowIcon = false;
            this.wdb_title.Size = new System.Drawing.Size(352, 23);
            this.wdb_title.TabIndex = 0;
            this.wdb_title.Text = "  ";
            this.wdb_title.UseSystemStyleColor = true;
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
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
            this.panel2.Back = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.lb_imageIndex);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(131, 23);
            this.panel2.TabIndex = 0;
            this.panel2.Text = "panel2";
            // 
            // lb_imageIndex
            // 
            this.lb_imageIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_imageIndex.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_imageIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(83)))), ((int)(((byte)(36)))));
            this.lb_imageIndex.Location = new System.Drawing.Point(3, 0);
            this.lb_imageIndex.Name = "lb_imageIndex";
            this.lb_imageIndex.Size = new System.Drawing.Size(27, 23);
            this.lb_imageIndex.TabIndex = 0;
            this.lb_imageIndex.Text = "0";
            this.lb_imageIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowPanel1
            // 
            this.flowPanel1.BadgeAlign = AntdUI.TAlignFrom.TL;
            this.flowPanel1.Controls.Add(this.ava_save);
            this.flowPanel1.Controls.Add(this.ava_saveReport);
            this.flowPanel1.Controls.Add(this.ava_auto);
            this.flowPanel1.Controls.Add(this.ava_zoom_out);
            this.flowPanel1.Controls.Add(this.ava__zoom_in);
            this.flowPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel1.Location = new System.Drawing.Point(203, 0);
            this.flowPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowPanel1.Name = "flowPanel1";
            this.flowPanel1.Size = new System.Drawing.Size(149, 23);
            this.flowPanel1.TabIndex = 2;
            this.flowPanel1.Text = "flowPanel1";
            // 
            // ava_saveReport
            // 
            this.ava_saveReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ava_saveReport.Image = global::PBAnaly.Properties.Resources.数据报告__1_;
            this.ava_saveReport.ImageFit = AntdUI.TFit.Contain;
            this.ava_saveReport.Location = new System.Drawing.Point(91, 3);
            this.ava_saveReport.Name = "ava_saveReport";
            this.ava_saveReport.Size = new System.Drawing.Size(21, 17);
            this.ava_saveReport.TabIndex = 7;
            this.ava_saveReport.Text = "a";
            // 
            // ava_save
            // 
            this.ava_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ava_save.Image = global::PBAnaly.Properties.Resources.保存图片;
            this.ava_save.ImageFit = AntdUI.TFit.Contain;
            this.ava_save.Location = new System.Drawing.Point(118, 3);
            this.ava_save.Name = "ava_save";
            this.ava_save.Size = new System.Drawing.Size(23, 17);
            this.ava_save.TabIndex = 6;
            this.ava_save.Text = "a";
            this.ava_save.Click += new System.EventHandler(this.ava_save_Click);
            // 
            // ava_zoom_out
            // 
            this.ava_zoom_out.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ava_zoom_out.Image = global::PBAnaly.Properties.Resources.缩小;
            this.ava_zoom_out.ImageFit = AntdUI.TFit.Contain;
            this.ava_zoom_out.Location = new System.Drawing.Point(34, 3);
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
            this.ava__zoom_in.Location = new System.Drawing.Point(3, 3);
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
            this.ava_auto.Location = new System.Drawing.Point(65, 3);
            this.ava_auto.Name = "ava_auto";
            this.ava_auto.Size = new System.Drawing.Size(20, 17);
            this.ava_auto.TabIndex = 3;
            this.ava_auto.Text = "a";
            this.ava_auto.Click += new System.EventHandler(this.ava_auto_Click);
            // 
            // pl_bottom
            // 
            this.pl_bottom.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pl_bottom.Controls.Add(this.tlp_bottom_panel);
            this.pl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pl_bottom.Location = new System.Drawing.Point(3, 292);
            this.pl_bottom.Name = "pl_bottom";
            this.pl_bottom.Size = new System.Drawing.Size(352, 13);
            this.pl_bottom.TabIndex = 2;
            this.pl_bottom.Text = "panel3";
            // 
            // tlp_bottom_panel
            // 
            this.tlp_bottom_panel.ColumnCount = 5;
            this.tlp_bottom_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_bottom_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlp_bottom_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlp_bottom_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlp_bottom_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tlp_bottom_panel.Controls.Add(this.lb_name, 0, 0);
            this.tlp_bottom_panel.Controls.Add(this.lb_size, 4, 0);
            this.tlp_bottom_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_bottom_panel.Location = new System.Drawing.Point(0, 0);
            this.tlp_bottom_panel.Name = "tlp_bottom_panel";
            this.tlp_bottom_panel.RowCount = 1;
            this.tlp_bottom_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_bottom_panel.Size = new System.Drawing.Size(352, 13);
            this.tlp_bottom_panel.TabIndex = 1;
            // 
            // lb_name
            // 
            this.lb_name.BackColor = System.Drawing.Color.Transparent;
            this.lb_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.lb_name.Location = new System.Drawing.Point(0, 0);
            this.lb_name.Margin = new System.Windows.Forms.Padding(0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(348, 13);
            this.lb_name.TabIndex = 1;
            this.lb_name.Text = "800x600";
            // 
            // lb_size
            // 
            this.lb_size.BackColor = System.Drawing.Color.Transparent;
            this.lb_size.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_size.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            this.lb_size.Location = new System.Drawing.Point(351, 0);
            this.lb_size.Margin = new System.Windows.Forms.Padding(0);
            this.lb_size.Name = "lb_size";
            this.lb_size.Size = new System.Drawing.Size(1, 13);
            this.lb_size.TabIndex = 0;
            this.lb_size.Text = "800x600";
            this.lb_size.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lb_size.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel2.Controls.Add(this.pl_panel_image, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 49);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(352, 243);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // pl_panel_image
            // 
            this.pl_panel_image.ArrowSize = 0;
            this.pl_panel_image.Back = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.pl_panel_image.Badge = "";
            this.pl_panel_image.BadgeOffsetX = 0;
            this.pl_panel_image.BadgeOffsetY = -100;
            this.pl_panel_image.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.pl_panel_image.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.pl_panel_image.BorderWidth = 5F;
            this.pl_panel_image.Controls.Add(this.pl_bg_panel);
            this.pl_panel_image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_panel_image.Location = new System.Drawing.Point(3, 3);
            this.pl_panel_image.Name = "pl_panel_image";
            this.pl_panel_image.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.pl_panel_image.Size = new System.Drawing.Size(344, 237);
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
            this.image_pl.ContextMenuStrip = this.ctms_strop;
            this.image_pl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.image_pl.Location = new System.Drawing.Point(0, 0);
            this.image_pl.Name = "image_pl";
            this.image_pl.Size = new System.Drawing.Size(223, 185);
            this.image_pl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image_pl.TabIndex = 0;
            this.image_pl.TabStop = false;
            // 
            // ctms_strop
            // 
            this.ctms_strop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctms_strop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.ctms_strop_copy,
            this.ctms_strop_stickup,
            this.ctms_strop_delete});
            this.ctms_strop.Name = "ctms_strop";
            this.ctms_strop.Size = new System.Drawing.Size(101, 76);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // ctms_strop_copy
            // 
            this.ctms_strop_copy.Name = "ctms_strop_copy";
            this.ctms_strop_copy.Size = new System.Drawing.Size(100, 22);
            this.ctms_strop_copy.Text = "复制";
            // 
            // ctms_strop_stickup
            // 
            this.ctms_strop_stickup.Name = "ctms_strop_stickup";
            this.ctms_strop_stickup.Size = new System.Drawing.Size(100, 22);
            this.ctms_strop_stickup.Text = "粘贴";
            // 
            // ctms_strop_delete
            // 
            this.ctms_strop_delete.Name = "ctms_strop_delete";
            this.ctms_strop_delete.Size = new System.Drawing.Size(100, 22);
            this.ctms_strop_delete.Text = "删除";
            // 
            // LanesImagePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 308);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.pl_bottom);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.wdb_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LanesImagePanel";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Text = "BioanalyImagePanel";
            this.SizeChanged += new System.EventHandler(this.BioanalyImagePanel_SizeChanged);
            this.MouseEnter += new System.EventHandler(this.BioanalyImagePanel_MouseEnter);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.flowPanel1.ResumeLayout(false);
            this.pl_bottom.ResumeLayout(false);
            this.tlp_bottom_panel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pl_panel_image.ResumeLayout(false);
            this.pl_bg_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.image_pl)).EndInit();
            this.ctms_strop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private AntdUI.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private AntdUI.FlowPanel flowPanel1;
        private AntdUI.Avatar ava_auto;
        private AntdUI.Label lb_size;
        public AntdUI.Avatar ava_zoom_out;
        public AntdUI.Avatar ava__zoom_in;
        public AntdUI.Panel pl_bg_panel;
        public AntdUI.Panel pl_panel_image;
        public System.Windows.Forms.PictureBox image_pl;
        public AntdUI.WindowBar wdb_title;
        public AntdUI.Avatar ava_saveReport;
        private AntdUI.Avatar ava_save;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.ContextMenuStrip ctms_strop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem ctms_strop_copy;
        public System.Windows.Forms.ToolStripMenuItem ctms_strop_stickup;
        public System.Windows.Forms.ToolStripMenuItem ctms_strop_delete;
        private AntdUI.Label lb_name;
        public AntdUI.Panel pl_bottom;
        public System.Windows.Forms.TableLayoutPanel tlp_bottom_panel;
        private AntdUI.Panel panel2;
        public AntdUI.Label lb_imageIndex;
    }
}