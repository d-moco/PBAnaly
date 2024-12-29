namespace PBAnaly.UI
{
    partial class MultiImageForm
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
            this.label1 = new AntdUI.Label();
            this.pb_image = new System.Windows.Forms.PictureBox();
            this.ab_one = new ReaLTaiizor.Controls.AirButton();
            this.ab_last = new ReaLTaiizor.Controls.AirButton();
            this.ab_next = new ReaLTaiizor.Controls.AirButton();
            this.ab_atLast = new ReaLTaiizor.Controls.AirButton();
            this.lb_lable = new AntdUI.Label();
            this.ab_saveTif = new ReaLTaiizor.Controls.AirButton();
            this.ab_close = new ReaLTaiizor.Controls.AirButton();
            this.ab_open_cur_tif = new ReaLTaiizor.Controls.AirButton();
            this.cb_path = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "路径:";
            // 
            // pb_image
            // 
            this.pb_image.Location = new System.Drawing.Point(24, 67);
            this.pb_image.Name = "pb_image";
            this.pb_image.Size = new System.Drawing.Size(752, 447);
            this.pb_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_image.TabIndex = 2;
            this.pb_image.TabStop = false;
            // 
            // ab_one
            // 
            this.ab_one.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ab_one.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.ab_one.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ab_one.Image = null;
            this.ab_one.Location = new System.Drawing.Point(24, 530);
            this.ab_one.Name = "ab_one";
            this.ab_one.NoRounding = false;
            this.ab_one.Size = new System.Drawing.Size(81, 40);
            this.ab_one.TabIndex = 3;
            this.ab_one.Text = "第一幅";
            this.ab_one.Transparent = false;
            this.ab_one.Click += new System.EventHandler(this.ab_one_Click);
            // 
            // ab_last
            // 
            this.ab_last.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ab_last.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.ab_last.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ab_last.Image = null;
            this.ab_last.Location = new System.Drawing.Point(142, 530);
            this.ab_last.Name = "ab_last";
            this.ab_last.NoRounding = false;
            this.ab_last.Size = new System.Drawing.Size(81, 40);
            this.ab_last.TabIndex = 4;
            this.ab_last.Text = "上一幅";
            this.ab_last.Transparent = false;
            this.ab_last.Click += new System.EventHandler(this.ab_last_Click);
            // 
            // ab_next
            // 
            this.ab_next.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ab_next.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.ab_next.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ab_next.Image = null;
            this.ab_next.Location = new System.Drawing.Point(337, 530);
            this.ab_next.Name = "ab_next";
            this.ab_next.NoRounding = false;
            this.ab_next.Size = new System.Drawing.Size(81, 40);
            this.ab_next.TabIndex = 5;
            this.ab_next.Text = "下一幅";
            this.ab_next.Transparent = false;
            this.ab_next.Click += new System.EventHandler(this.ab_next_Click);
            // 
            // ab_atLast
            // 
            this.ab_atLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ab_atLast.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.ab_atLast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ab_atLast.Image = null;
            this.ab_atLast.Location = new System.Drawing.Point(445, 530);
            this.ab_atLast.Name = "ab_atLast";
            this.ab_atLast.NoRounding = false;
            this.ab_atLast.Size = new System.Drawing.Size(81, 40);
            this.ab_atLast.TabIndex = 6;
            this.ab_atLast.Text = "最后";
            this.ab_atLast.Transparent = false;
            this.ab_atLast.Click += new System.EventHandler(this.ab_atLast_Click);
            // 
            // lb_lable
            // 
            this.lb_lable.Location = new System.Drawing.Point(257, 530);
            this.lb_lable.Name = "lb_lable";
            this.lb_lable.Size = new System.Drawing.Size(64, 42);
            this.lb_lable.TabIndex = 7;
            this.lb_lable.Text = "0/0";
            // 
            // ab_saveTif
            // 
            this.ab_saveTif.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ab_saveTif.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.ab_saveTif.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ab_saveTif.Image = null;
            this.ab_saveTif.Location = new System.Drawing.Point(355, 590);
            this.ab_saveTif.Name = "ab_saveTif";
            this.ab_saveTif.NoRounding = false;
            this.ab_saveTif.Size = new System.Drawing.Size(123, 40);
            this.ab_saveTif.TabIndex = 8;
            this.ab_saveTif.Text = "另存为单帧TIF";
            this.ab_saveTif.Transparent = false;
            this.ab_saveTif.Click += new System.EventHandler(this.ab_saveTif_Click);
            // 
            // ab_close
            // 
            this.ab_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ab_close.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.ab_close.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ab_close.Image = null;
            this.ab_close.Location = new System.Drawing.Point(511, 590);
            this.ab_close.Name = "ab_close";
            this.ab_close.NoRounding = false;
            this.ab_close.Size = new System.Drawing.Size(123, 40);
            this.ab_close.TabIndex = 9;
            this.ab_close.Text = "关闭";
            this.ab_close.Transparent = false;
            this.ab_close.Click += new System.EventHandler(this.ab_close_Click);
            // 
            // ab_open_cur_tif
            // 
            this.ab_open_cur_tif.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ab_open_cur_tif.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            this.ab_open_cur_tif.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ab_open_cur_tif.Image = null;
            this.ab_open_cur_tif.Location = new System.Drawing.Point(665, 590);
            this.ab_open_cur_tif.Name = "ab_open_cur_tif";
            this.ab_open_cur_tif.NoRounding = false;
            this.ab_open_cur_tif.Size = new System.Drawing.Size(123, 40);
            this.ab_open_cur_tif.TabIndex = 10;
            this.ab_open_cur_tif.Text = "打开当前帧";
            this.ab_open_cur_tif.Transparent = false;
            this.ab_open_cur_tif.Click += new System.EventHandler(this.ab_open_cur_tif_Click);
            // 
            // cb_path
            // 
            this.cb_path.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_path.FormattingEnabled = true;
            this.cb_path.Location = new System.Drawing.Point(93, 12);
            this.cb_path.Name = "cb_path";
            this.cb_path.Size = new System.Drawing.Size(632, 23);
            this.cb_path.TabIndex = 11;
            // 
            // MultiImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(818, 642);
            this.Controls.Add(this.cb_path);
            this.Controls.Add(this.ab_open_cur_tif);
            this.Controls.Add(this.ab_close);
            this.Controls.Add(this.ab_saveTif);
            this.Controls.Add(this.lb_lable);
            this.Controls.Add(this.ab_atLast);
            this.Controls.Add(this.ab_next);
            this.Controls.Add(this.ab_last);
            this.Controls.Add(this.ab_one);
            this.Controls.Add(this.pb_image);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MultiImageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "序列图像管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MultiImageForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pb_image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Label label1;
        private System.Windows.Forms.PictureBox pb_image;
        private ReaLTaiizor.Controls.AirButton ab_one;
        private ReaLTaiizor.Controls.AirButton ab_last;
        private ReaLTaiizor.Controls.AirButton ab_next;
        private ReaLTaiizor.Controls.AirButton ab_atLast;
        private AntdUI.Label lb_lable;
        private ReaLTaiizor.Controls.AirButton ab_saveTif;
        private ReaLTaiizor.Controls.AirButton ab_close;
        private ReaLTaiizor.Controls.AirButton ab_open_cur_tif;
        private System.Windows.Forms.ComboBox cb_path;
    }
}