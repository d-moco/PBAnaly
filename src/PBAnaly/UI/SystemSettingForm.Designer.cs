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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_mode = new System.Windows.Forms.Panel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tab_UserManage = new System.Windows.Forms.TabPage();
            this.pnlMainMenu = new System.Windows.Forms.Panel();
            this.btn_ConfigManage = new System.Windows.Forms.Button();
            this.btn_ComManage = new System.Windows.Forms.Button();
            this.btn_FormatManage = new System.Windows.Forms.Button();
            this.btn_ReadManage = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Min = new System.Windows.Forms.Button();
            this.btn_Max = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel_mode.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tab_UserManage.SuspendLayout();
            this.pnlMainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.panel_mode.Location = new System.Drawing.Point(50, 0);
            this.panel_mode.Name = "panel_mode";
            this.panel_mode.Size = new System.Drawing.Size(1106, 639);
            this.panel_mode.TabIndex = 444;
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tab_UserManage);
            this.tabMain.Location = new System.Drawing.Point(0, -3);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1114, 651);
            this.tabMain.TabIndex = 0;
            // 
            // tab_UserManage
            // 
            this.tab_UserManage.BackColor = System.Drawing.Color.White;
            this.tab_UserManage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tab_UserManage.Controls.Add(this.splitContainer1);
            this.tab_UserManage.Location = new System.Drawing.Point(22, 4);
            this.tab_UserManage.Name = "tab_UserManage";
            this.tab_UserManage.Padding = new System.Windows.Forms.Padding(3);
            this.tab_UserManage.Size = new System.Drawing.Size(1088, 643);
            this.tab_UserManage.TabIndex = 0;
            this.tab_UserManage.Text = "用户管理";
            // 
            // pnlMainMenu
            // 
            this.pnlMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMainMenu.BackColor = System.Drawing.Color.White;
            this.pnlMainMenu.Controls.Add(this.btn_ConfigManage);
            this.pnlMainMenu.Controls.Add(this.btn_ComManage);
            this.pnlMainMenu.Controls.Add(this.btn_FormatManage);
            this.pnlMainMenu.Controls.Add(this.btn_ReadManage);
            this.pnlMainMenu.Location = new System.Drawing.Point(3, 3);
            this.pnlMainMenu.Name = "pnlMainMenu";
            this.pnlMainMenu.Size = new System.Drawing.Size(77, 636);
            this.pnlMainMenu.TabIndex = 443;
            // 
            // btn_ConfigManage
            // 
            this.btn_ConfigManage.BackColor = System.Drawing.Color.White;
            this.btn_ConfigManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ConfigManage.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_ConfigManage.Location = new System.Drawing.Point(3, 279);
            this.btn_ConfigManage.Name = "btn_ConfigManage";
            this.btn_ConfigManage.Size = new System.Drawing.Size(76, 86);
            this.btn_ConfigManage.TabIndex = 6;
            this.btn_ConfigManage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_ConfigManage.UseVisualStyleBackColor = false;
            // 
            // btn_ComManage
            // 
            this.btn_ComManage.BackColor = System.Drawing.Color.White;
            this.btn_ComManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ComManage.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_ComManage.Location = new System.Drawing.Point(3, 187);
            this.btn_ComManage.Name = "btn_ComManage";
            this.btn_ComManage.Size = new System.Drawing.Size(76, 86);
            this.btn_ComManage.TabIndex = 5;
            this.btn_ComManage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_ComManage.UseVisualStyleBackColor = false;
            // 
            // btn_FormatManage
            // 
            this.btn_FormatManage.BackColor = System.Drawing.Color.White;
            this.btn_FormatManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FormatManage.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_FormatManage.Location = new System.Drawing.Point(3, 95);
            this.btn_FormatManage.Name = "btn_FormatManage";
            this.btn_FormatManage.Size = new System.Drawing.Size(76, 86);
            this.btn_FormatManage.TabIndex = 4;
            this.btn_FormatManage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_FormatManage.UseVisualStyleBackColor = false;
            // 
            // btn_ReadManage
            // 
            this.btn_ReadManage.BackColor = System.Drawing.Color.White;
            this.btn_ReadManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ReadManage.Font = new System.Drawing.Font("宋体", 9F);
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
            this.btn_Min.Location = new System.Drawing.Point(1014, 0);
            this.btn_Min.Name = "btn_Min";
            this.btn_Min.Size = new System.Drawing.Size(44, 32);
            this.btn_Min.TabIndex = 459;
            this.btn_Min.UseVisualStyleBackColor = false;
            // 
            // btn_Max
            // 
            this.btn_Max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Max.BackColor = System.Drawing.Color.Transparent;
            this.btn_Max.FlatAppearance.BorderSize = 0;
            this.btn_Max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Max.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Max.Image = global::PBAnaly.Properties.Resources.最大化white;
            this.btn_Max.Location = new System.Drawing.Point(1064, 0);
            this.btn_Max.Name = "btn_Max";
            this.btn_Max.Size = new System.Drawing.Size(44, 32);
            this.btn_Max.TabIndex = 458;
            this.btn_Max.UseVisualStyleBackColor = false;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Close.Image = global::PBAnaly.Properties.Resources.关闭White;
            this.btn_Close.Location = new System.Drawing.Point(1114, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(44, 32);
            this.btn_Close.TabIndex = 457;
            this.btn_Close.UseVisualStyleBackColor = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1082, 637);
            this.splitContainer1.SplitterDistance = 562;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column3,
            this.Column4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(562, 637);
            this.dataGridView1.TabIndex = 496;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "用户名";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "创建人";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "创建时间";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column4.HeaderText = "权限";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.panel1.ResumeLayout(false);
            this.panel_mode.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tab_UserManage.ResumeLayout(false);
            this.pnlMainMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.Button btn_ConfigManage;
        private System.Windows.Forms.Button btn_ComManage;
        private System.Windows.Forms.Button btn_FormatManage;
        private System.Windows.Forms.Button btn_ReadManage;
        private System.Windows.Forms.Panel panel_mode;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tab_UserManage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}