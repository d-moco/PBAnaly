namespace PBAnaly.UI
{
    partial class colorbarControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.slb_name = new ReaLTaiizor.Controls.SkyLabel();
            this.pb_colorbar = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_colorbar)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.37313F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.62687F));
            this.tableLayoutPanel1.Controls.Add(this.slb_name, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pb_colorbar, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(67, 300);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // slb_name
            // 
            this.slb_name.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.slb_name, 2);
            this.slb_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slb_name.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            this.slb_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            this.slb_name.Location = new System.Drawing.Point(3, 0);
            this.slb_name.Name = "slb_name";
            this.slb_name.Size = new System.Drawing.Size(61, 19);
            this.slb_name.TabIndex = 0;
            this.slb_name.Text = "mark";
            this.slb_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_colorbar
            // 
            this.pb_colorbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_colorbar.Image = global::PBAnaly.Properties.Resources.Gray;
            this.pb_colorbar.Location = new System.Drawing.Point(0, 19);
            this.pb_colorbar.Margin = new System.Windows.Forms.Padding(0);
            this.pb_colorbar.Name = "pb_colorbar";
            this.pb_colorbar.Size = new System.Drawing.Size(16, 229);
            this.pb_colorbar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_colorbar.TabIndex = 1;
            this.pb_colorbar.TabStop = false;
            // 
            // colorbarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(70, 200);
            this.MinimumSize = new System.Drawing.Size(0, 300);
            this.Name = "colorbarControl";
            this.Size = new System.Drawing.Size(67, 300);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_colorbar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ReaLTaiizor.Controls.SkyLabel slb_name;
        private System.Windows.Forms.PictureBox pb_colorbar;
    }
}
