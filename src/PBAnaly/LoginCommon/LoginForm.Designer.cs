namespace PBAnaly.LoginCommon
{
    partial class LoginForm
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
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.cb_Remember = new System.Windows.Forms.CheckBox();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.SIGNIN_materialButton = new MaterialSkin.Controls.MaterialButton();
            this.btn_Login = new MaterialSkin.Controls.MaterialButton();
            this.btn_Close = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.txt_Password);
            this.panel1.Controls.Add(this.cb_Remember);
            this.panel1.Controls.Add(this.txt_UserName);
            this.panel1.Controls.Add(this.SIGNIN_materialButton);
            this.panel1.Controls.Add(this.btn_Login);
            this.panel1.Location = new System.Drawing.Point(-1, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 332);
            this.panel1.TabIndex = 14;
            // 
            // txt_Password
            // 
            this.txt_Password.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_Password.Location = new System.Drawing.Point(106, 120);
            this.txt_Password.Multiline = true;
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(389, 33);
            this.txt_Password.TabIndex = 18;
            this.txt_Password.Text = "User Name";
            this.txt_Password.Click += new System.EventHandler(this.txt_Password_Click);
            // 
            // cb_Remember
            // 
            this.cb_Remember.AutoSize = true;
            this.cb_Remember.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_Remember.ForeColor = System.Drawing.Color.White;
            this.cb_Remember.Location = new System.Drawing.Point(106, 175);
            this.cb_Remember.Name = "cb_Remember";
            this.cb_Remember.Size = new System.Drawing.Size(142, 25);
            this.cb_Remember.TabIndex = 17;
            this.cb_Remember.Text = "Remember Me";
            this.cb_Remember.UseVisualStyleBackColor = true;
            // 
            // txt_UserName
            // 
            this.txt_UserName.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_UserName.Location = new System.Drawing.Point(106, 54);
            this.txt_UserName.Multiline = true;
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(389, 33);
            this.txt_UserName.TabIndex = 16;
            this.txt_UserName.Text = "User Name";
            this.txt_UserName.Click += new System.EventHandler(this.txt_UserName_Click);
            // 
            // SIGNIN_materialButton
            // 
            this.SIGNIN_materialButton.AutoSize = false;
            this.SIGNIN_materialButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SIGNIN_materialButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.SIGNIN_materialButton.Depth = 0;
            this.SIGNIN_materialButton.HighEmphasis = true;
            this.SIGNIN_materialButton.Icon = null;
            this.SIGNIN_materialButton.Location = new System.Drawing.Point(321, 220);
            this.SIGNIN_materialButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.SIGNIN_materialButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.SIGNIN_materialButton.Name = "SIGNIN_materialButton";
            this.SIGNIN_materialButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.SIGNIN_materialButton.Size = new System.Drawing.Size(147, 36);
            this.SIGNIN_materialButton.TabIndex = 15;
            this.SIGNIN_materialButton.Text = "Sign In";
            this.SIGNIN_materialButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.SIGNIN_materialButton.UseAccentColor = false;
            this.SIGNIN_materialButton.UseVisualStyleBackColor = true;
            // 
            // btn_Login
            // 
            this.btn_Login.AutoSize = false;
            this.btn_Login.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Login.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btn_Login.Depth = 0;
            this.btn_Login.HighEmphasis = true;
            this.btn_Login.Icon = null;
            this.btn_Login.Location = new System.Drawing.Point(126, 220);
            this.btn_Login.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_Login.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btn_Login.Size = new System.Drawing.Size(147, 36);
            this.btn_Login.TabIndex = 14;
            this.btn_Login.Text = "Login";
            this.btn_Login.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btn_Login.UseAccentColor = false;
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Close.Image = global::PBAnaly.Properties.Resources.关闭White;
            this.btn_Close.Location = new System.Drawing.Point(543, -1);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(44, 35);
            this.btn_Close.TabIndex = 447;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(587, 365);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.CheckBox cb_Remember;
        private System.Windows.Forms.TextBox txt_UserName;
        private MaterialSkin.Controls.MaterialButton SIGNIN_materialButton;
        private MaterialSkin.Controls.MaterialButton btn_Login;
        private System.Windows.Forms.Button btn_Close;
    }
}