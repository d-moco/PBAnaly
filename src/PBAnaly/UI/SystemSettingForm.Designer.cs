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
            this.tab_Main = new System.Windows.Forms.TabControl();
            this.tab_UserManage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Min = new System.Windows.Forms.Button();
            this.btn_Max = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.pnl_MainMenu = new System.Windows.Forms.Panel();
            this.btn_SystemSetting = new System.Windows.Forms.Button();
            this.btn_UserManager = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tab_SystemSetting = new System.Windows.Forms.TabPage();
            this.panel_System = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbx_System_Language = new System.Windows.Forms.ComboBox();
            this.label_Language = new System.Windows.Forms.Label();
            this.btn_save_ZH_US = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tab_Main.SuspendLayout();
            this.pnl_MainMenu.SuspendLayout();
            this.tab_SystemSetting.SuspendLayout();
            this.panel_System.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.tab_Main);
            this.panel1.Controls.Add(this.pnl_MainMenu);
            this.panel1.Location = new System.Drawing.Point(-1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1159, 642);
            this.panel1.TabIndex = 455;
            // 
            // tab_Main
            // 
            this.tab_Main.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tab_Main.Controls.Add(this.tab_SystemSetting);
            this.tab_Main.Controls.Add(this.tab_UserManage1);
            this.tab_Main.Location = new System.Drawing.Point(174, 3);
            this.tab_Main.Multiline = true;
            this.tab_Main.Name = "tab_Main";
            this.tab_Main.SelectedIndex = 0;
            this.tab_Main.Size = new System.Drawing.Size(982, 636);
            this.tab_Main.TabIndex = 0;
            // 
            // tab_UserManage1
            // 
            this.tab_UserManage1.BackColor = System.Drawing.Color.White;
            this.tab_UserManage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tab_UserManage1.Location = new System.Drawing.Point(22, 4);
            this.tab_UserManage1.Name = "tab_UserManage1";
            this.tab_UserManage1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_UserManage1.Size = new System.Drawing.Size(956, 628);
            this.tab_UserManage1.TabIndex = 0;
            this.tab_UserManage1.Text = "用户管理";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 25);
            this.label4.TabIndex = 456;
            this.label4.Text = "System";
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
            // pnl_MainMenu
            // 
            this.pnl_MainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnl_MainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnl_MainMenu.Controls.Add(this.btn_SystemSetting);
            this.pnl_MainMenu.Controls.Add(this.btn_UserManager);
            this.pnl_MainMenu.Controls.Add(this.panel4);
            this.pnl_MainMenu.Location = new System.Drawing.Point(5, 3);
            this.pnl_MainMenu.Name = "pnl_MainMenu";
            this.pnl_MainMenu.Size = new System.Drawing.Size(191, 636);
            this.pnl_MainMenu.TabIndex = 445;
            // 
            // btn_SystemSetting
            // 
            this.btn_SystemSetting.BackColor = System.Drawing.SystemColors.Control;
            this.btn_SystemSetting.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btn_SystemSetting.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumAquamarine;
            this.btn_SystemSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SystemSetting.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.btn_SystemSetting.Location = new System.Drawing.Point(1, 0);
            this.btn_SystemSetting.Name = "btn_SystemSetting";
            this.btn_SystemSetting.Size = new System.Drawing.Size(184, 50);
            this.btn_SystemSetting.TabIndex = 1;
            this.btn_SystemSetting.Text = "系统设置";
            this.btn_SystemSetting.UseVisualStyleBackColor = false;
            this.btn_SystemSetting.Click += new System.EventHandler(this.btn_SystemSetting_Click);
            // 
            // btn_UserManager
            // 
            this.btn_UserManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(83)))));
            this.btn_UserManager.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btn_UserManager.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumAquamarine;
            this.btn_UserManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UserManager.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.btn_UserManager.ForeColor = System.Drawing.Color.White;
            this.btn_UserManager.Location = new System.Drawing.Point(1, 49);
            this.btn_UserManager.Name = "btn_UserManager";
            this.btn_UserManager.Size = new System.Drawing.Size(184, 50);
            this.btn_UserManager.TabIndex = 0;
            this.btn_UserManager.Text = "用户管理";
            this.btn_UserManager.UseVisualStyleBackColor = false;
            this.btn_UserManager.Click += new System.EventHandler(this.btn_UserManager_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(56)))), ((int)(((byte)(83)))));
            this.panel4.Location = new System.Drawing.Point(185, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(3, 636);
            this.panel4.TabIndex = 7;
            // 
            // tab_SystemSetting
            // 
            this.tab_SystemSetting.BackColor = System.Drawing.Color.White;
            this.tab_SystemSetting.Controls.Add(this.panel_System);
            this.tab_SystemSetting.Location = new System.Drawing.Point(22, 4);
            this.tab_SystemSetting.Name = "tab_SystemSetting";
            this.tab_SystemSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tab_SystemSetting.Size = new System.Drawing.Size(956, 628);
            this.tab_SystemSetting.TabIndex = 1;
            this.tab_SystemSetting.Text = "系统设置";
            // 
            // panel_System
            // 
            this.panel_System.Controls.Add(this.panel2);
            this.panel_System.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_System.Location = new System.Drawing.Point(3, 3);
            this.panel_System.Name = "panel_System";
            this.panel_System.Size = new System.Drawing.Size(950, 622);
            this.panel_System.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbx_System_Language);
            this.panel2.Controls.Add(this.label_Language);
            this.panel2.Controls.Add(this.btn_save_ZH_US);
            this.panel2.Location = new System.Drawing.Point(2, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(394, 618);
            this.panel2.TabIndex = 512;
            // 
            // cbx_System_Language
            // 
            this.cbx_System_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_System_Language.Font = new System.Drawing.Font("宋体", 15F);
            this.cbx_System_Language.FormattingEnabled = true;
            this.cbx_System_Language.Items.AddRange(new object[] {
            "English",
            "简体中文"});
            this.cbx_System_Language.Location = new System.Drawing.Point(106, 17);
            this.cbx_System_Language.Name = "cbx_System_Language";
            this.cbx_System_Language.Size = new System.Drawing.Size(249, 28);
            this.cbx_System_Language.TabIndex = 513;
            // 
            // label_Language
            // 
            this.label_Language.AutoSize = true;
            this.label_Language.Font = new System.Drawing.Font("宋体", 13F);
            this.label_Language.Location = new System.Drawing.Point(15, 22);
            this.label_Language.Name = "label_Language";
            this.label_Language.Size = new System.Drawing.Size(53, 18);
            this.label_Language.TabIndex = 512;
            this.label_Language.Text = "语言:";
            // 
            // btn_save_ZH_US
            // 
            this.btn_save_ZH_US.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_save_ZH_US.FlatAppearance.BorderSize = 0;
            this.btn_save_ZH_US.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save_ZH_US.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.btn_save_ZH_US.ForeColor = System.Drawing.Color.White;
            this.btn_save_ZH_US.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save_ZH_US.Location = new System.Drawing.Point(276, 562);
            this.btn_save_ZH_US.Name = "btn_save_ZH_US";
            this.btn_save_ZH_US.Size = new System.Drawing.Size(101, 38);
            this.btn_save_ZH_US.TabIndex = 511;
            this.btn_save_ZH_US.Text = "保存";
            this.btn_save_ZH_US.UseVisualStyleBackColor = false;
            this.btn_save_ZH_US.Click += new System.EventHandler(this.btn_save_ZH_US_Click);
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
            this.tab_Main.ResumeLayout(false);
            this.pnl_MainMenu.ResumeLayout(false);
            this.tab_SystemSetting.ResumeLayout(false);
            this.panel_System.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Max;
        private System.Windows.Forms.Button btn_Min;
        private System.Windows.Forms.TabControl tab_Main;
        private System.Windows.Forms.TabPage tab_UserManage1;
        private System.Windows.Forms.Panel pnl_MainMenu;
        private System.Windows.Forms.Button btn_SystemSetting;
        private System.Windows.Forms.Button btn_UserManager;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabPage tab_SystemSetting;
        private System.Windows.Forms.Panel panel_System;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbx_System_Language;
        private System.Windows.Forms.Label label_Language;
        private System.Windows.Forms.Button btn_save_ZH_US;
    }
}