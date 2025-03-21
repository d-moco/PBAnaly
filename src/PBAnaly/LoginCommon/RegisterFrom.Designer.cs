namespace PBAnaly.LoginCommon
{
    partial class RegisterFrom
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
            this.label_re_Answer = new System.Windows.Forms.Label();
            this.label_re_question = new System.Windows.Forms.Label();
            this.label_enterPassword = new System.Windows.Forms.Label();
            this.label_re_password = new System.Windows.Forms.Label();
            this.label_re_userNma = new System.Windows.Forms.Label();
            this.txt_answer = new System.Windows.Forms.TextBox();
            this.txt_broblem = new System.Windows.Forms.TextBox();
            this.txt_enter_password = new System.Windows.Forms.TextBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.btn_re_register = new MaterialSkin.Controls.MaterialButton();
            this.btn_back_form_re = new MaterialSkin.Controls.MaterialButton();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.label_re_Answer);
            this.panel1.Controls.Add(this.label_re_question);
            this.panel1.Controls.Add(this.label_enterPassword);
            this.panel1.Controls.Add(this.label_re_password);
            this.panel1.Controls.Add(this.label_re_userNma);
            this.panel1.Controls.Add(this.txt_answer);
            this.panel1.Controls.Add(this.txt_broblem);
            this.panel1.Controls.Add(this.txt_enter_password);
            this.panel1.Controls.Add(this.txt_Password);
            this.panel1.Controls.Add(this.txt_UserName);
            this.panel1.Controls.Add(this.btn_re_register);
            this.panel1.Controls.Add(this.btn_back_form_re);
            this.panel1.Location = new System.Drawing.Point(-1, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 537);
            this.panel1.TabIndex = 452;
            // 
            // label_re_Answer
            // 
            this.label_re_Answer.AutoSize = true;
            this.label_re_Answer.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label_re_Answer.ForeColor = System.Drawing.Color.White;
            this.label_re_Answer.Location = new System.Drawing.Point(90, 361);
            this.label_re_Answer.Name = "label_re_Answer";
            this.label_re_Answer.Size = new System.Drawing.Size(80, 25);
            this.label_re_Answer.TabIndex = 459;
            this.label_re_Answer.Text = "Answer";
            // 
            // label_re_question
            // 
            this.label_re_question.AutoSize = true;
            this.label_re_question.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label_re_question.ForeColor = System.Drawing.Color.White;
            this.label_re_question.Location = new System.Drawing.Point(90, 282);
            this.label_re_question.Name = "label_re_question";
            this.label_re_question.Size = new System.Drawing.Size(94, 25);
            this.label_re_question.TabIndex = 458;
            this.label_re_question.Text = "Question";
            // 
            // label_enterPassword
            // 
            this.label_enterPassword.AutoSize = true;
            this.label_enterPassword.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label_enterPassword.ForeColor = System.Drawing.Color.White;
            this.label_enterPassword.Location = new System.Drawing.Point(90, 209);
            this.label_enterPassword.Name = "label_enterPassword";
            this.label_enterPassword.Size = new System.Drawing.Size(99, 25);
            this.label_enterPassword.TabIndex = 457;
            this.label_enterPassword.Text = "Password";
            // 
            // label_re_password
            // 
            this.label_re_password.AutoSize = true;
            this.label_re_password.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label_re_password.ForeColor = System.Drawing.Color.White;
            this.label_re_password.Location = new System.Drawing.Point(90, 138);
            this.label_re_password.Name = "label_re_password";
            this.label_re_password.Size = new System.Drawing.Size(99, 25);
            this.label_re_password.TabIndex = 456;
            this.label_re_password.Text = "Password";
            // 
            // label_re_userNma
            // 
            this.label_re_userNma.AutoSize = true;
            this.label_re_userNma.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label_re_userNma.ForeColor = System.Drawing.Color.White;
            this.label_re_userNma.Location = new System.Drawing.Point(90, 72);
            this.label_re_userNma.Name = "label_re_userNma";
            this.label_re_userNma.Size = new System.Drawing.Size(65, 25);
            this.label_re_userNma.TabIndex = 455;
            this.label_re_userNma.Text = "Name";
            // 
            // txt_answer
            // 
            this.txt_answer.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_answer.Location = new System.Drawing.Point(195, 355);
            this.txt_answer.Multiline = true;
            this.txt_answer.Name = "txt_answer";
            this.txt_answer.Size = new System.Drawing.Size(389, 33);
            this.txt_answer.TabIndex = 21;
            this.txt_answer.Text = "Security answer";
            this.txt_answer.Click += new System.EventHandler(this.txt_answer_Click);
            // 
            // txt_broblem
            // 
            this.txt_broblem.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_broblem.Location = new System.Drawing.Point(195, 276);
            this.txt_broblem.Multiline = true;
            this.txt_broblem.Name = "txt_broblem";
            this.txt_broblem.Size = new System.Drawing.Size(389, 33);
            this.txt_broblem.TabIndex = 20;
            this.txt_broblem.Text = "Security problem";
            this.txt_broblem.Click += new System.EventHandler(this.txt_broblem_Click);
            // 
            // txt_enter_password
            // 
            this.txt_enter_password.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_enter_password.Location = new System.Drawing.Point(195, 203);
            this.txt_enter_password.Multiline = true;
            this.txt_enter_password.Name = "txt_enter_password";
            this.txt_enter_password.PasswordChar = '*';
            this.txt_enter_password.Size = new System.Drawing.Size(389, 33);
            this.txt_enter_password.TabIndex = 19;
            this.txt_enter_password.Text = "User Name";
            this.txt_enter_password.Click += new System.EventHandler(this.txt_enter_password_Click);
            // 
            // txt_Password
            // 
            this.txt_Password.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_Password.Location = new System.Drawing.Point(195, 132);
            this.txt_Password.Multiline = true;
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(389, 33);
            this.txt_Password.TabIndex = 18;
            this.txt_Password.Text = "User Name";
            this.txt_Password.Click += new System.EventHandler(this.txt_Password_Click);
            // 
            // txt_UserName
            // 
            this.txt_UserName.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_UserName.Location = new System.Drawing.Point(195, 66);
            this.txt_UserName.Multiline = true;
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(389, 33);
            this.txt_UserName.TabIndex = 16;
            this.txt_UserName.Text = "User Name";
            this.txt_UserName.Click += new System.EventHandler(this.txt_UserName_Click);
            // 
            // btn_re_register
            // 
            this.btn_re_register.AutoSize = false;
            this.btn_re_register.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_re_register.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btn_re_register.Depth = 0;
            this.btn_re_register.HighEmphasis = true;
            this.btn_re_register.Icon = null;
            this.btn_re_register.Location = new System.Drawing.Point(394, 431);
            this.btn_re_register.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_re_register.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_re_register.Name = "btn_re_register";
            this.btn_re_register.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btn_re_register.Size = new System.Drawing.Size(147, 36);
            this.btn_re_register.TabIndex = 15;
            this.btn_re_register.Text = "register";
            this.btn_re_register.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btn_re_register.UseAccentColor = false;
            this.btn_re_register.UseVisualStyleBackColor = true;
            this.btn_re_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // btn_back_form_re
            // 
            this.btn_back_form_re.AutoSize = false;
            this.btn_back_form_re.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_back_form_re.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btn_back_form_re.Depth = 0;
            this.btn_back_form_re.HighEmphasis = true;
            this.btn_back_form_re.Icon = null;
            this.btn_back_form_re.Location = new System.Drawing.Point(14, 9);
            this.btn_back_form_re.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_back_form_re.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_back_form_re.Name = "btn_back_form_re";
            this.btn_back_form_re.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btn_back_form_re.Size = new System.Drawing.Size(62, 30);
            this.btn_back_form_re.TabIndex = 14;
            this.btn_back_form_re.Text = "back";
            this.btn_back_form_re.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btn_back_form_re.UseAccentColor = false;
            this.btn_back_form_re.UseVisualStyleBackColor = true;
            this.btn_back_form_re.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 25);
            this.label4.TabIndex = 454;
            this.label4.Text = "register";
            // 
            // RegisterFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(637, 570);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegisterFrom";
            this.Text = "RegisterFrom";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_answer;
        private System.Windows.Forms.TextBox txt_broblem;
        private System.Windows.Forms.TextBox txt_enter_password;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.TextBox txt_UserName;
        private MaterialSkin.Controls.MaterialButton btn_re_register;
        private MaterialSkin.Controls.MaterialButton btn_back_form_re;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_re_Answer;
        private System.Windows.Forms.Label label_re_question;
        private System.Windows.Forms.Label label_enterPassword;
        private System.Windows.Forms.Label label_re_password;
        private System.Windows.Forms.Label label_re_userNma;
    }
}