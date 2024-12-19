namespace PBAnaly.UI
{
    partial class SystemSettingForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_mode = new System.Windows.Forms.Panel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tab_UserManage = new System.Windows.Forms.TabPage();
            this.pnlMainMenu = new System.Windows.Forms.Panel();
            this.btn_ReadManage = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Min = new System.Windows.Forms.Button();
            this.btn_Max = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel_mode.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.pnlMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.panel_mode);
            this.panel1.Controls.Add(this.pnlMainMenu);
            this.panel1.Location = new System.Drawing.Point(-1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1159, 642);
            this.panel1.TabIndex = 455;
            // 
            // panel_mode
            // 
            this.panel_mode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_mode.BackColor = System.Drawing.SystemColors.Control;
            this.panel_mode.Controls.Add(this.tabMain);
            this.panel_mode.Location = new System.Drawing.Point(58, 0);
            this.panel_mode.Name = "panel_mode";
            this.panel_mode.Size = new System.Drawing.Size(1098, 639);
            this.panel_mode.TabIndex = 444;
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabMain.Controls.Add(this.tab_UserManage);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1098, 639);
            this.tabMain.TabIndex = 0;
            // 
            // tab_UserManage
            // 
            this.tab_UserManage.BackColor = System.Drawing.Color.White;
            this.tab_UserManage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tab_UserManage.Location = new System.Drawing.Point(22, 4);
            this.tab_UserManage.Name = "tab_UserManage";
            this.tab_UserManage.Padding = new System.Windows.Forms.Padding(3);
            this.tab_UserManage.Size = new System.Drawing.Size(1072, 631);
            this.tab_UserManage.TabIndex = 0;
            this.tab_UserManage.Text = "用户管理";
            // 
            // pnlMainMenu
            // 
            this.pnlMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMainMenu.BackColor = System.Drawing.Color.White;
            this.pnlMainMenu.Controls.Add(this.btn_ReadManage);
            this.pnlMainMenu.Location = new System.Drawing.Point(3, -3);
            this.pnlMainMenu.Name = "pnlMainMenu";
            this.pnlMainMenu.Size = new System.Drawing.Size(77, 642);
            this.pnlMainMenu.TabIndex = 443;
            // 
            // btn_ReadManage
            // 
            this.btn_ReadManage.BackColor = System.Drawing.Color.White;
            this.btn_ReadManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ReadManage.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_ReadManage.Image = global::PBAnaly.Properties.Resources.添加用户;
            this.btn_ReadManage.Location = new System.Drawing.Point(3, 3);
            this.btn_ReadManage.Name = "btn_ReadManage";
            this.btn_ReadManage.Size = new System.Drawing.Size(76, 86);
            this.btn_ReadManage.TabIndex = 3;
            this.btn_ReadManage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_ReadManage.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 25);
            this.label4.TabIndex = 456;
            this.label4.Text = "register";
            // 
            // btn_Min
            // 
            this.btn_Min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Min.BackColor = System.Drawing.Color.Transparent;
            this.btn_Min.FlatAppearance.BorderSize = 0;
            this.btn_Min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Min.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Min.Image = global::PBAnaly.Properties.Resources.最小化white;
            this.btn_Min.Location = new System.Drawing.Point(1014, -1);
            this.btn_Min.Name = "btn_Min";
            this.btn_Min.Size = new System.Drawing.Size(44, 32);
            this.btn_Min.TabIndex = 459;
            this.btn_Min.UseVisualStyleBackColor = false;
            this.btn_Min.Visible = false;
            // 
            // btn_Max
            // 
            this.btn_Max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Max.BackColor = System.Drawing.Color.Transparent;
            this.btn_Max.FlatAppearance.BorderSize = 0;
            this.btn_Max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Max.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Max.Image = global::PBAnaly.Properties.Resources.最大化white;
            this.btn_Max.Location = new System.Drawing.Point(1064, -1);
            this.btn_Max.Name = "btn_Max";
            this.btn_Max.Size = new System.Drawing.Size(44, 32);
            this.btn_Max.TabIndex = 458;
            this.btn_Max.UseVisualStyleBackColor = false;
            this.btn_Max.Visible = false;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Close.Image = global::PBAnaly.Properties.Resources.关闭White;
            this.btn_Close.Location = new System.Drawing.Point(1114, -1);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(44, 32);
            this.btn_Close.TabIndex = 457;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // SystemSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(1158, 675);
            this.Controls.Add(this.btn_Min);
            this.Controls.Add(this.btn_Max);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SystemSettingForm";
            this.Text = "SystemSettingForm";
            this.Load += new System.EventHandler(this.SystemSettingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel_mode.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.pnlMainMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Max;
        private System.Windows.Forms.Button btn_Min;
        private System.Windows.Forms.Panel pnlMainMenu;
        private System.Windows.Forms.Button btn_ReadManage;
        private System.Windows.Forms.Panel panel_mode;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tab_UserManage;
    }
}