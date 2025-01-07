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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_fix = new System.Windows.Forms.TabPage();
            this.btn_role_manage = new System.Windows.Forms.Button();
            this.btn_fix_role = new System.Windows.Forms.Button();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.label_role_userName = new System.Windows.Forms.Label();
            this.cbx_role_role = new System.Windows.Forms.ComboBox();
            this.label1_role = new System.Windows.Forms.Label();
            this.tab_delete = new System.Windows.Forms.TabPage();
            this.btn_delete_user = new System.Windows.Forms.Button();
            this.label_DeleteTips = new System.Windows.Forms.Label();
            this.tab_fix_password = new System.Windows.Forms.TabPage();
            this.btn_FixPassword = new System.Windows.Forms.Button();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.label_password_formUserManage = new System.Windows.Forms.Label();
            this.txt_fix_p_UserName = new System.Windows.Forms.TextBox();
            this.label_Username_form_userManage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_delete_role = new System.Windows.Forms.Button();
            this.btn_edit_password_role = new System.Windows.Forms.Button();
            this.btn_editRole_head = new System.Windows.Forms.Button();
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
            this.dataGridView1.Size = new System.Drawing.Size(427, 507);
            this.dataGridView1.TabIndex = 496;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "UserName";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Creator";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "CreationTime";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column4.HeaderText = "Role";
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
            this.tab_fix.Controls.Add(this.btn_role_manage);
            this.tab_fix.Controls.Add(this.btn_fix_role);
            this.tab_fix.Controls.Add(this.txt_UserName);
            this.tab_fix.Controls.Add(this.label_role_userName);
            this.tab_fix.Controls.Add(this.cbx_role_role);
            this.tab_fix.Controls.Add(this.label1_role);
            this.tab_fix.Location = new System.Drawing.Point(4, 22);
            this.tab_fix.Name = "tab_fix";
            this.tab_fix.Padding = new System.Windows.Forms.Padding(3);
            this.tab_fix.Size = new System.Drawing.Size(421, 459);
            this.tab_fix.TabIndex = 0;
            this.tab_fix.Text = "修改权限";
            this.tab_fix.UseVisualStyleBackColor = true;
            // 
            // btn_role_manage
            // 
            this.btn_role_manage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_role_manage.FlatAppearance.BorderSize = 0;
            this.btn_role_manage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_role_manage.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.btn_role_manage.ForeColor = System.Drawing.Color.White;
            this.btn_role_manage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_role_manage.Location = new System.Drawing.Point(284, 305);
            this.btn_role_manage.Name = "btn_role_manage";
            this.btn_role_manage.Size = new System.Drawing.Size(101, 38);
            this.btn_role_manage.TabIndex = 506;
            this.btn_role_manage.Text = "权限管理";
            this.btn_role_manage.UseVisualStyleBackColor = false;
            this.btn_role_manage.Click += new System.EventHandler(this.btn_role_manage_Click);
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
            // label_role_userName
            // 
            this.label_role_userName.AutoSize = true;
            this.label_role_userName.Font = new System.Drawing.Font("宋体", 15F);
            this.label_role_userName.Location = new System.Drawing.Point(18, 51);
            this.label_role_userName.Name = "label_role_userName";
            this.label_role_userName.Size = new System.Drawing.Size(69, 20);
            this.label_role_userName.TabIndex = 503;
            this.label_role_userName.Text = "用户名";
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
            // label1_role
            // 
            this.label1_role.AutoSize = true;
            this.label1_role.Font = new System.Drawing.Font("宋体", 15F);
            this.label1_role.Location = new System.Drawing.Point(18, 145);
            this.label1_role.Name = "label1_role";
            this.label1_role.Size = new System.Drawing.Size(49, 20);
            this.label1_role.TabIndex = 501;
            this.label1_role.Text = "权限";
            // 
            // tab_delete
            // 
            this.tab_delete.Controls.Add(this.btn_delete_user);
            this.tab_delete.Controls.Add(this.label_DeleteTips);
            this.tab_delete.Location = new System.Drawing.Point(4, 22);
            this.tab_delete.Name = "tab_delete";
            this.tab_delete.Padding = new System.Windows.Forms.Padding(3);
            this.tab_delete.Size = new System.Drawing.Size(421, 459);
            this.tab_delete.TabIndex = 1;
            this.tab_delete.Text = "删除";
            this.tab_delete.UseVisualStyleBackColor = true;
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
            // label_DeleteTips
            // 
            this.label_DeleteTips.AutoSize = true;
            this.label_DeleteTips.Location = new System.Drawing.Point(119, 17);
            this.label_DeleteTips.Name = "label_DeleteTips";
            this.label_DeleteTips.Size = new System.Drawing.Size(161, 12);
            this.label_DeleteTips.TabIndex = 0;
            this.label_DeleteTips.Text = "选中左侧表格的一行即可删除";
            // 
            // tab_fix_password
            // 
            this.tab_fix_password.Controls.Add(this.btn_FixPassword);
            this.tab_fix_password.Controls.Add(this.txt_password);
            this.tab_fix_password.Controls.Add(this.label_password_formUserManage);
            this.tab_fix_password.Controls.Add(this.txt_fix_p_UserName);
            this.tab_fix_password.Controls.Add(this.label_Username_form_userManage);
            this.tab_fix_password.Location = new System.Drawing.Point(4, 22);
            this.tab_fix_password.Name = "tab_fix_password";
            this.tab_fix_password.Padding = new System.Windows.Forms.Padding(3);
            this.tab_fix_password.Size = new System.Drawing.Size(421, 459);
            this.tab_fix_password.TabIndex = 2;
            this.tab_fix_password.Text = "修改密码";
            this.tab_fix_password.UseVisualStyleBackColor = true;
            // 
            // btn_FixPassword
            // 
            this.btn_FixPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_FixPassword.FlatAppearance.BorderSize = 0;
            this.btn_FixPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FixPassword.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
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
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_password.Location = new System.Drawing.Point(98, 148);
            this.txt_password.Multiline = true;
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(292, 28);
            this.txt_password.TabIndex = 508;
            // 
            // label_password_formUserManage
            // 
            this.label_password_formUserManage.AutoSize = true;
            this.label_password_formUserManage.Font = new System.Drawing.Font("宋体", 13F);
            this.label_password_formUserManage.Location = new System.Drawing.Point(19, 158);
            this.label_password_formUserManage.Name = "label_password_formUserManage";
            this.label_password_formUserManage.Size = new System.Drawing.Size(44, 18);
            this.label_password_formUserManage.TabIndex = 507;
            this.label_password_formUserManage.Text = "密码";
            // 
            // txt_fix_p_UserName
            // 
            this.txt_fix_p_UserName.Font = new System.Drawing.Font("宋体", 13F);
            this.txt_fix_p_UserName.Location = new System.Drawing.Point(98, 46);
            this.txt_fix_p_UserName.Multiline = true;
            this.txt_fix_p_UserName.Name = "txt_fix_p_UserName";
            this.txt_fix_p_UserName.ReadOnly = true;
            this.txt_fix_p_UserName.Size = new System.Drawing.Size(292, 28);
            this.txt_fix_p_UserName.TabIndex = 506;
            // 
            // label_Username_form_userManage
            // 
            this.label_Username_form_userManage.AutoSize = true;
            this.label_Username_form_userManage.Font = new System.Drawing.Font("宋体", 13F);
            this.label_Username_form_userManage.Location = new System.Drawing.Point(19, 54);
            this.label_Username_form_userManage.Name = "label_Username_form_userManage";
            this.label_Username_form_userManage.Size = new System.Drawing.Size(62, 18);
            this.label_Username_form_userManage.TabIndex = 505;
            this.label_Username_form_userManage.Text = "用户名";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btn_delete_role);
            this.panel1.Controls.Add(this.btn_edit_password_role);
            this.panel1.Controls.Add(this.btn_editRole_head);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 43);
            this.panel1.TabIndex = 453;
            // 
            // btn_delete_role
            // 
            this.btn_delete_role.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete_role.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_delete_role.FlatAppearance.BorderSize = 0;
            this.btn_delete_role.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete_role.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.btn_delete_role.ForeColor = System.Drawing.Color.White;
            this.btn_delete_role.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_delete_role.Location = new System.Drawing.Point(181, 2);
            this.btn_delete_role.Name = "btn_delete_role";
            this.btn_delete_role.Size = new System.Drawing.Size(78, 38);
            this.btn_delete_role.TabIndex = 498;
            this.btn_delete_role.Text = "删除";
            this.btn_delete_role.UseVisualStyleBackColor = false;
            this.btn_delete_role.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_edit_password_role
            // 
            this.btn_edit_password_role.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_edit_password_role.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_edit_password_role.FlatAppearance.BorderSize = 0;
            this.btn_edit_password_role.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_edit_password_role.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.btn_edit_password_role.ForeColor = System.Drawing.Color.White;
            this.btn_edit_password_role.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_edit_password_role.Location = new System.Drawing.Point(321, 2);
            this.btn_edit_password_role.Name = "btn_edit_password_role";
            this.btn_edit_password_role.Size = new System.Drawing.Size(101, 38);
            this.btn_edit_password_role.TabIndex = 499;
            this.btn_edit_password_role.Text = "修改密码";
            this.btn_edit_password_role.UseVisualStyleBackColor = false;
            this.btn_edit_password_role.Click += new System.EventHandler(this.btn_edit_password_Click);
            // 
            // btn_editRole_head
            // 
            this.btn_editRole_head.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_editRole_head.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(32)))), ((int)(((byte)(96)))));
            this.btn_editRole_head.FlatAppearance.BorderSize = 0;
            this.btn_editRole_head.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_editRole_head.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.btn_editRole_head.ForeColor = System.Drawing.Color.White;
            this.btn_editRole_head.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_editRole_head.Location = new System.Drawing.Point(7, 3);
            this.btn_editRole_head.Name = "btn_editRole_head";
            this.btn_editRole_head.Size = new System.Drawing.Size(101, 38);
            this.btn_editRole_head.TabIndex = 497;
            this.btn_editRole_head.Text = "修改权限";
            this.btn_editRole_head.UseVisualStyleBackColor = false;
            this.btn_editRole_head.Click += new System.EventHandler(this.btn_editRole_Click);
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_delete_role;
        private System.Windows.Forms.Button btn_edit_password_role;
        private System.Windows.Forms.Button btn_editRole_head;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_fix;
        private System.Windows.Forms.TabPage tab_delete;
        private System.Windows.Forms.TabPage tab_fix_password;
        private System.Windows.Forms.Button btn_fix_role;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.Label label_role_userName;
        private System.Windows.Forms.ComboBox cbx_role_role;
        private System.Windows.Forms.Label label1_role;
        private System.Windows.Forms.Button btn_delete_user;
        private System.Windows.Forms.Label label_DeleteTips;
        private System.Windows.Forms.Button btn_FixPassword;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label label_password_formUserManage;
        private System.Windows.Forms.TextBox txt_fix_p_UserName;
        private System.Windows.Forms.Label label_Username_form_userManage;
        private System.Windows.Forms.Button btn_role_manage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}