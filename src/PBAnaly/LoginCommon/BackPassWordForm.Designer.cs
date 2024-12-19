namespace PBAnaly.LoginCommon
{
    partial class BackPassWordForm
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
            this.btn_back = new MaterialSkin.Controls.MaterialButton();
            this.btn_Frin_Password = new MaterialSkin.Controls.MaterialButton();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.txt_broblem = new System.Windows.Forms.TextBox();
            this.txt_answer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_back
            // 
            this.btn_back.AutoSize = false;
            this.btn_back.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_back.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btn_back.Depth = 0;
            this.btn_back.HighEmphasis = true;
            this.btn_back.Icon = null;
            this.btn_back.Location = new System.Drawing.Point(14, 9);
            this.btn_back.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_back.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_back.Name = "btn_back";
            this.btn_back.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btn_back.Size = new System.Drawing.Size(62, 30);
            this.btn_back.TabIndex = 14;
            this.btn_back.Text = "back";
            this.btn_back.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btn_back.UseAccentColor = false;
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_Frin_Password
            // 
            this.btn_Frin_Password.AutoSize = false;
            this.btn_Frin_Password.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Frin_Password.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btn_Frin_Password.Depth = 0;
            this.btn_Frin_Password.HighEmphasis = true;
            this.btn_Frin_Password.Icon = null;
            this.btn_Frin_Password.Location = new System.Drawing.Point(411, 381);
            this.btn_Frin_Password.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btn_Frin_Password.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_Frin_Password.Name = "btn_Frin_Password";
            this.btn_Frin_Password.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btn_Frin_Password.Size = new System.Drawing.Size(147, 36);
            this.btn_Frin_Password.TabIndex = 15;
            this.btn_Frin_Password.Text = "Find";
            this.btn_Frin_Password.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btn_Frin_Password.UseAccentColor = false;
            this.btn_Frin_Password.UseVisualStyleBackColor = true;
            this.btn_Frin_Password.Click += new System.EventHandler(this.btn_Frin_Password_Click);
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
            this.txt_UserName.TextChanged += new System.EventHandler(this.txt_UserName_TextChanged);
            // 
            // txt_Password
            // 
            this.txt_Password.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_Password.Location = new System.Drawing.Point(195, 297);
            this.txt_Password.Multiline = true;
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(389, 33);
            this.txt_Password.TabIndex = 18;
            // 
            // txt_broblem
            // 
            this.txt_broblem.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_broblem.Location = new System.Drawing.Point(195, 138);
            this.txt_broblem.Multiline = true;
            this.txt_broblem.Name = "txt_broblem";
            this.txt_broblem.ReadOnly = true;
            this.txt_broblem.Size = new System.Drawing.Size(389, 33);
            this.txt_broblem.TabIndex = 20;
            // 
            // txt_answer
            // 
            this.txt_answer.Font = new System.Drawing.Font("宋体", 18F);
            this.txt_answer.Location = new System.Drawing.Point(195, 217);
            this.txt_answer.Multiline = true;
            this.txt_answer.Name = "txt_answer";
            this.txt_answer.Size = new System.Drawing.Size(389, 33);
            this.txt_answer.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(78, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 25);
            this.label1.TabIndex = 455;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(41, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 25);
            this.label2.TabIndex = 456;
            this.label2.Text = "Your Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(23, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 25);
            this.label5.TabIndex = 458;
            this.label5.Text = "security question";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(109, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 25);
            this.label6.TabIndex = 459;
            this.label6.Text = "Answer";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_answer);
            this.panel1.Controls.Add(this.txt_broblem);
            this.panel1.Controls.Add(this.txt_Password);
            this.panel1.Controls.Add(this.txt_UserName);
            this.panel1.Controls.Add(this.btn_Frin_Password);
            this.panel1.Controls.Add(this.btn_back);
            this.panel1.Location = new System.Drawing.Point(-3, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 540);
            this.panel1.TabIndex = 453;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(221, 25);
            this.label4.TabIndex = 455;
            this.label4.Text = "find back the password";
            // 
            // BackPassWordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(637, 570);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BackPassWordForm";
            this.Text = "BackPassWordForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton btn_back;
        private MaterialSkin.Controls.MaterialButton btn_Frin_Password;
        private System.Windows.Forms.TextBox txt_UserName;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.TextBox txt_broblem;
        private System.Windows.Forms.TextBox txt_answer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
    }
}