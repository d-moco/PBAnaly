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
            this.clasi_init = new AntdUI.CollapseItem();
            this.clasi_strips = new AntdUI.CollapseItem();
            this.clasi_conformity = new AntdUI.CollapseItem();
            this.collapse1.SuspendLayout();
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
            this.collapse1.Name = "collapse1";
            this.collapse1.Size = new System.Drawing.Size(405, 619);
            this.collapse1.TabIndex = 0;
            this.collapse1.Text = "collapse1";
            // 
            // clasi_lanes
            // 
            this.clasi_lanes.Expand = true;
            this.clasi_lanes.Location = new System.Drawing.Point(23, 72);
            this.clasi_lanes.Name = "clasi_lanes";
            this.clasi_lanes.Size = new System.Drawing.Size(359, 170);
            this.clasi_lanes.TabIndex = 0;
            this.clasi_lanes.Text = "泳道";
            // 
            // clasi_init
            // 
            this.clasi_init.Location = new System.Drawing.Point(-359, -60);
            this.clasi_init.Name = "clasi_init";
            this.clasi_init.Size = new System.Drawing.Size(359, 60);
            this.clasi_init.TabIndex = 1;
            this.clasi_init.Text = "初始井";
            // 
            // clasi_strips
            // 
            this.clasi_strips.Location = new System.Drawing.Point(-100, -60);
            this.clasi_strips.Name = "clasi_strips";
            this.clasi_strips.Size = new System.Drawing.Size(100, 60);
            this.clasi_strips.TabIndex = 2;
            this.clasi_strips.Text = "条带";
            // 
            // clasi_conformity
            // 
            this.clasi_conformity.Location = new System.Drawing.Point(-100, -60);
            this.clasi_conformity.Name = "clasi_conformity";
            this.clasi_conformity.Size = new System.Drawing.Size(100, 60);
            this.clasi_conformity.TabIndex = 3;
            this.clasi_conformity.Text = "整合";
            // 
            // LanesImagePaletteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 619);
            this.Controls.Add(this.collapse1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LanesImagePaletteForm";
            this.Text = "BioanayImagePaletteForm";
            this.collapse1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Collapse collapse1;
        private AntdUI.CollapseItem clasi_lanes;
        private AntdUI.CollapseItem clasi_init;
        private AntdUI.CollapseItem clasi_strips;
        private AntdUI.CollapseItem clasi_conformity;
    }
}