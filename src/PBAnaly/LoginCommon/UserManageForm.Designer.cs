namespace PBAnaly.LoginCommon
{
    partial class UserManageForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_fix = new System.Windows.Forms.TabPage();
            this.btn_fix_role = new System.Windows.Forms.Button();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbx_role_role = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tab_delete = new System.Windows.Forms.TabPage();
            this.tab_fix_password = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_edit_password = new System.Windows.Forms.Button();
            this.btn_editRole = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_delete_user = new System.Windows.Forms.Button();
            this.txt_fix_p_UserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_FixPassword = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tab_fix.SuspendLayout();
            this.tab_delete.SuspendLayout();
            this.tab_fix_password.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(863, 507);
            this.splitContainer1.SplitterDistance = 427;
            this.splitContainer1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle26;
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
            this.dataGridView1.Size = new System.Drawing.Size(427, 507);
            this.dataGridView1.TabIndex = 496;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column1
            // 
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle27;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle28;
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
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle29;
            this.Column3.HeaderText = "创建时间";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle30;
            this.Column4.HeaderText = "权限";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tab_fix);
            this.tabControl1.Controls.Add(this.tab_delete);
            this.tabControl1.Controls.Add(this.tab_fix_password);
            this.tabControl1.Location = new System.Drawing.Point(3, 22);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(429, 485);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_fix
            // 
            this.tab_fix.Controls.Add(this.btn_fix_role);
            this.tab_fix.Controls.Add(this.txt_UserName);
            this.tab_fix.Controls.Add(this.label11);
            this.tab_fix.Controls.Add(this.cbx_role_role);
            this.tab_fix.Controls.Add(this.label12);
            this.tab_fix.Location = new System.Drawing.Point(4, 22);
            this.tab_fix.Name = "tab_fix";
            this.tab_fix.Padding = new System.Windows.Forms.Padding(3);
            this.tab_fix.Size = new System.Drawing.Size(421, 459);
            this.tab_fix.TabIndex = 0;
            this.tab_fix.Text = "修改权限";
            this.tab_fix.UseVisualStyleBackColor = true;
            // 
            // btn_fix_role
            // 
            this.btn_fix_role.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_fix_role.FlatAppearance.BorderSize = 0;
            this.btn_fix_role.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fix_role.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btn_fix_role.ForeColor = System.Drawing.Color.White;
            this.btn_fix_role.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_fix_role.Location = new System.Drawing.Point(284, 215);
            this.btn_fix_role.Name = "btn_fix_role";
            this.btn_fix_role.Size = new System.Drawing.Size(101, 38);
            this.btn_fix_role.TabIndex = 505;
            this.btn_fix_role.Text = "修改权限";
            this.btn_fix_role.UseVisualStyleBackColor = false;
            this.btn_fix_role.Click += new System.EventHandler(this.btn_fix_role_Click);
            // 
            // txt_UserName
            // 
            this.txt_UserName.Font = new System.Drawing.Font("宋体", 13F);
            this.txt_UserName.Location = new System.Drawing.Point(93, 43);
            this.txt_UserName.Multiline = true;
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.ReadOnly = true;
            this.txt_UserName.Size = new System.Drawing.Size(292, 28);
            this.txt_UserName.TabIndex = 504;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 15F);
            this.label11.Location = new System.Drawing.Point(18, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 20);
            this.label11.TabIndex = 503;
            this.label11.Text = "用户名";
            // 
            // cbx_role_role
            // 
            this.cbx_role_role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_role_role.Font = new System.Drawing.Font("宋体", 15F);
            this.cbx_role_role.FormattingEnabled = true;
            this.cbx_role_role.Items.AddRange(new object[] {
            "Operator",
            "Engineer",
            "Administrator",
            "SuperAdministrator"});
            this.cbx_role_role.Location = new System.Drawing.Point(93, 145);
            this.cbx_role_role.Name = "cbx_role_role";
            this.cbx_role_role.Size = new System.Drawing.Size(292, 28);
            this.cbx_role_role.TabIndex = 502;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 15F);
            this.label12.Location = new System.Drawing.Point(38, 148);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 20);
            this.label12.TabIndex = 501;
            this.label12.Text = "权限";
            // 
            // tab_delete
            // 
            this.tab_delete.Controls.Add(this.btn_delete_user);
            this.tab_delete.Controls.Add(this.label1);
            this.tab_delete.Location = new System.Drawing.Point(4, 22);
            this.tab_delete.Name = "tab_delete";
            this.tab_delete.Padding = new System.Windows.Forms.Padding(3);
            this.tab_delete.Size = new System.Drawing.Size(421, 459);
            this.tab_delete.TabIndex = 1;
            this.tab_delete.Text = "删除";
            this.tab_delete.UseVisualStyleBackColor = true;
            // 
            // tab_fix_password
            // 
            this.tab_fix_password.Controls.Add(this.btn_FixPassword);
            this.tab_fix_password.Controls.Add(this.txt_password);
            this.tab_fix_password.Controls.Add(this.label3);
            this.tab_fix_password.Controls.Add(this.txt_fix_p_UserName);
            this.tab_fix_password.Controls.Add(this.label2);
            this.tab_fix_password.Location = new System.Drawing.Point(4, 22);
            this.tab_fix_password.Name = "tab_fix_password";
            this.tab_fix_password.Padding = new System.Windows.Forms.Padding(3);
            this.tab_fix_password.Size = new System.Drawing.Size(421, 459);
            this.tab_fix_password.TabIndex = 2;
            this.tab_fix_password.Text = "修改密码";
            this.tab_fix_password.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btn_delete);
            this.panel1.Controls.Add(this.btn_edit_password);
            this.panel1.Controls.Add(this.btn_editRole);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 43);
            this.panel1.TabIndex = 453;
            // 
            // btn_delete
            // 
            this.btn_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_delete.FlatAppearance.BorderSize = 0;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btn_delete.ForeColor = System.Drawing.Color.White;
            this.btn_delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_delete.Location = new System.Drawing.Point(181, 2);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(78, 38);
            this.btn_delete.TabIndex = 498;
            this.btn_delete.Text = "删除";
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_edit_password
            // 
            this.btn_edit_password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_edit_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_edit_password.FlatAppearance.BorderSize = 0;
            this.btn_edit_password.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_edit_password.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btn_edit_password.ForeColor = System.Drawing.Color.White;
            this.btn_edit_password.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_edit_password.Location = new System.Drawing.Point(321, 2);
            this.btn_edit_password.Name = "btn_edit_password";
            this.btn_edit_password.Size = new System.Drawing.Size(101, 38);
            this.btn_edit_password.TabIndex = 499;
            this.btn_edit_password.Text = "修改密码";
            this.btn_edit_password.UseVisualStyleBackColor = false;
            this.btn_edit_password.Click += new System.EventHandler(this.btn_edit_password_Click);
            // 
            // btn_editRole
            // 
            this.btn_editRole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_editRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_editRole.FlatAppearance.BorderSize = 0;
            this.btn_editRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_editRole.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btn_editRole.ForeColor = System.Drawing.Color.White;
            this.btn_editRole.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_editRole.Location = new System.Drawing.Point(7, 3);
            this.btn_editRole.Name = "btn_editRole";
            this.btn_editRole.Size = new System.Drawing.Size(101, 38);
            this.btn_editRole.TabIndex = 497;
            this.btn_editRole.Text = "修改权限";
            this.btn_editRole.UseVisualStyleBackColor = false;
            this.btn_editRole.Click += new System.EventHandler(this.btn_editRole_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选中左侧表格的一行即可删除";
            // 
            // btn_delete_user
            // 
            this.btn_delete_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_delete_user.FlatAppearance.BorderSize = 0;
            this.btn_delete_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete_user.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btn_delete_user.ForeColor = System.Drawing.Color.White;
            this.btn_delete_user.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_delete_user.Location = new System.Drawing.Point(155, 74);
            this.btn_delete_user.Name = "btn_delete_user";
            this.btn_delete_user.Size = new System.Drawing.Size(78, 38);
            this.btn_delete_user.TabIndex = 499;
            this.btn_delete_user.Text = "删除";
            this.btn_delete_user.UseVisualStyleBackColor = false;
            this.btn_delete_user.Click += new System.EventHandler(this.btn_delete_user_Click);
            // 
            // txt_fix_p_UserName
            // 
            this.txt_fix_p_UserName.Font = new System.Drawing.Font("宋体", 13F);
            this.txt_fix_p_UserName.Location = new System.Drawing.Point(94, 46);
            this.txt_fix_p_UserName.Multiline = true;
            this.txt_fix_p_UserName.Name = "txt_fix_p_UserName";
            this.txt_fix_p_UserName.ReadOnly = true;
            this.txt_fix_p_UserName.Size = new System.Drawing.Size(292, 28);
            this.txt_fix_p_UserName.TabIndex = 506;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(19, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 505;
            this.label2.Text = "用户名";
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_password.Location = new System.Drawing.Point(94, 148);
            this.txt_password.Multiline = true;
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(292, 28);
            this.txt_password.TabIndex = 508;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(39, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 507;
            this.label3.Text = "密码";
            // 
            // btn_FixPassword
            // 
            this.btn_FixPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_FixPassword.FlatAppearance.BorderSize = 0;
            this.btn_FixPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FixPassword.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btn_FixPassword.ForeColor = System.Drawing.Color.White;
            this.btn_FixPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_FixPassword.Location = new System.Drawing.Point(285, 216);
            this.btn_FixPassword.Name = "btn_FixPassword";
            this.btn_FixPassword.Size = new System.Drawing.Size(101, 38);
            this.btn_FixPassword.TabIndex = 509;
            this.btn_FixPassword.Text = "修改密码";
            this.btn_FixPassword.UseVisualStyleBackColor = false;
            this.btn_FixPassword.Click += new System.EventHandler(this.btn_FixPassword_Click);
            // 
            // UserManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 507);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserManageForm";
            this.Text = "UserManageForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tab_fix.ResumeLayout(false);
            this.tab_fix.PerformLayout();
            this.tab_delete.ResumeLayout(false);
            this.tab_delete.PerformLayout();
            this.tab_fix_password.ResumeLayout(false);
            this.tab_fix_password.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_edit_password;
        private System.Windows.Forms.Button btn_editRole;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_fix;
        private System.Windows.Forms.TabPage tab_delete;
        private System.Windows.Forms.TabPage tab_fix_password;
        private System.Windows.Forms.Button btn_fix_role;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbx_role_role;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btn_delete_user;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_FixPassword;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_fix_p_UserName;
        private System.Windows.Forms.Label label2;
    }
}