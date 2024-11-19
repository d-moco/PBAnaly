namespace PBAnaly
{
    partial class SignInForm
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
            this.userName_materialTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            this.email_materialTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            this.password_materialTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            this.confirmPassword_materialTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            this.signIn_materialButton = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel_ID = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel_email = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel_PS = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel_PW = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // userName_materialTextBox
            // 
            this.userName_materialTextBox.AnimateReadOnly = false;
            this.userName_materialTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.userName_materialTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.userName_materialTextBox.Depth = 0;
            this.userName_materialTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.userName_materialTextBox.HideSelection = true;
            this.userName_materialTextBox.LeadingIcon = null;
            this.userName_materialTextBox.Location = new System.Drawing.Point(39, 92);
            this.userName_materialTextBox.MaxLength = 32767;
            this.userName_materialTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.userName_materialTextBox.Name = "userName_materialTextBox";
            this.userName_materialTextBox.PasswordChar = '\0';
            this.userName_materialTextBox.PrefixSuffixText = null;
            this.userName_materialTextBox.ReadOnly = false;
            this.userName_materialTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.userName_materialTextBox.SelectedText = "";
            this.userName_materialTextBox.SelectionLength = 0;
            this.userName_materialTextBox.SelectionStart = 0;
            this.userName_materialTextBox.ShortcutsEnabled = true;
            this.userName_materialTextBox.Size = new System.Drawing.Size(401, 48);
            this.userName_materialTextBox.TabIndex = 1;
            this.userName_materialTextBox.TabStop = false;
            this.userName_materialTextBox.Text = "UserName Or ID";
            this.userName_materialTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.userName_materialTextBox.TrailingIcon = null;
            this.userName_materialTextBox.UseSystemPasswordChar = false;
            this.userName_materialTextBox.Click += new System.EventHandler(this.userName_materialTextBox_Click);
            // 
            // email_materialTextBox
            // 
            this.email_materialTextBox.AnimateReadOnly = false;
            this.email_materialTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.email_materialTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.email_materialTextBox.Depth = 0;
            this.email_materialTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.email_materialTextBox.HideSelection = true;
            this.email_materialTextBox.LeadingIcon = null;
            this.email_materialTextBox.Location = new System.Drawing.Point(39, 174);
            this.email_materialTextBox.MaxLength = 32767;
            this.email_materialTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.email_materialTextBox.Name = "email_materialTextBox";
            this.email_materialTextBox.PasswordChar = '\0';
            this.email_materialTextBox.PrefixSuffixText = null;
            this.email_materialTextBox.ReadOnly = false;
            this.email_materialTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.email_materialTextBox.SelectedText = "";
            this.email_materialTextBox.SelectionLength = 0;
            this.email_materialTextBox.SelectionStart = 0;
            this.email_materialTextBox.ShortcutsEnabled = true;
            this.email_materialTextBox.Size = new System.Drawing.Size(401, 48);
            this.email_materialTextBox.TabIndex = 2;
            this.email_materialTextBox.TabStop = false;
            this.email_materialTextBox.Text = "E-Mail";
            this.email_materialTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.email_materialTextBox.TrailingIcon = null;
            this.email_materialTextBox.UseSystemPasswordChar = false;
            this.email_materialTextBox.Click += new System.EventHandler(this.email_materialTextBox_Click);
            // 
            // password_materialTextBox
            // 
            this.password_materialTextBox.AnimateReadOnly = false;
            this.password_materialTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.password_materialTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.password_materialTextBox.Depth = 0;
            this.password_materialTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.password_materialTextBox.HideSelection = true;
            this.password_materialTextBox.LeadingIcon = null;
            this.password_materialTextBox.Location = new System.Drawing.Point(39, 256);
            this.password_materialTextBox.MaxLength = 32767;
            this.password_materialTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.password_materialTextBox.Name = "password_materialTextBox";
            this.password_materialTextBox.PasswordChar = '\0';
            this.password_materialTextBox.PrefixSuffixText = null;
            this.password_materialTextBox.ReadOnly = false;
            this.password_materialTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.password_materialTextBox.SelectedText = "";
            this.password_materialTextBox.SelectionLength = 0;
            this.password_materialTextBox.SelectionStart = 0;
            this.password_materialTextBox.ShortcutsEnabled = true;
            this.password_materialTextBox.Size = new System.Drawing.Size(401, 48);
            this.password_materialTextBox.TabIndex = 3;
            this.password_materialTextBox.TabStop = false;
            this.password_materialTextBox.Text = "Password";
            this.password_materialTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.password_materialTextBox.TrailingIcon = null;
            this.password_materialTextBox.UseSystemPasswordChar = false;
            this.password_materialTextBox.Click += new System.EventHandler(this.password_materialTextBox_Click);
            // 
            // confirmPassword_materialTextBox
            // 
            this.confirmPassword_materialTextBox.AnimateReadOnly = false;
            this.confirmPassword_materialTextBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.confirmPassword_materialTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.confirmPassword_materialTextBox.Depth = 0;
            this.confirmPassword_materialTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.confirmPassword_materialTextBox.HideSelection = true;
            this.confirmPassword_materialTextBox.LeadingIcon = null;
            this.confirmPassword_materialTextBox.Location = new System.Drawing.Point(39, 342);
            this.confirmPassword_materialTextBox.MaxLength = 32767;
            this.confirmPassword_materialTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.confirmPassword_materialTextBox.Name = "confirmPassword_materialTextBox";
            this.confirmPassword_materialTextBox.PasswordChar = '\0';
            this.confirmPassword_materialTextBox.PrefixSuffixText = null;
            this.confirmPassword_materialTextBox.ReadOnly = false;
            this.confirmPassword_materialTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.confirmPassword_materialTextBox.SelectedText = "";
            this.confirmPassword_materialTextBox.SelectionLength = 0;
            this.confirmPassword_materialTextBox.SelectionStart = 0;
            this.confirmPassword_materialTextBox.ShortcutsEnabled = true;
            this.confirmPassword_materialTextBox.Size = new System.Drawing.Size(401, 48);
            this.confirmPassword_materialTextBox.TabIndex = 4;
            this.confirmPassword_materialTextBox.TabStop = false;
            this.confirmPassword_materialTextBox.Text = "Confirm Password";
            this.confirmPassword_materialTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.confirmPassword_materialTextBox.TrailingIcon = null;
            this.confirmPassword_materialTextBox.UseSystemPasswordChar = false;
            this.confirmPassword_materialTextBox.Click += new System.EventHandler(this.confirmPassword_materialTextBox_Click);
            this.confirmPassword_materialTextBox.TextChanged += new System.EventHandler(this.confirmPassword_materialTextBox_TextChanged);
            // 
            // signIn_materialButton
            // 
            this.signIn_materialButton.AutoSize = false;
            this.signIn_materialButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.signIn_materialButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.signIn_materialButton.Depth = 0;
            this.signIn_materialButton.HighEmphasis = true;
            this.signIn_materialButton.Icon = null;
            this.signIn_materialButton.Location = new System.Drawing.Point(232, 421);
            this.signIn_materialButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.signIn_materialButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.signIn_materialButton.Name = "signIn_materialButton";
            this.signIn_materialButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.signIn_materialButton.Size = new System.Drawing.Size(208, 36);
            this.signIn_materialButton.TabIndex = 5;
            this.signIn_materialButton.Text = "SIGN IN";
            this.signIn_materialButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.signIn_materialButton.UseAccentColor = false;
            this.signIn_materialButton.UseVisualStyleBackColor = true;
            this.signIn_materialButton.Click += new System.EventHandler(this.signIn_materialButton_Click);
            // 
            // materialLabel_ID
            // 
            this.materialLabel_ID.AutoSize = true;
            this.materialLabel_ID.Depth = 0;
            this.materialLabel_ID.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel_ID.Location = new System.Drawing.Point(42, 143);
            this.materialLabel_ID.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel_ID.Name = "materialLabel_ID";
            this.materialLabel_ID.Size = new System.Drawing.Size(1, 0);
            this.materialLabel_ID.TabIndex = 7;
            this.materialLabel_ID.UseAccent = true;
            // 
            // materialLabel_email
            // 
            this.materialLabel_email.AutoSize = true;
            this.materialLabel_email.BackColor = System.Drawing.Color.Red;
            this.materialLabel_email.Depth = 0;
            this.materialLabel_email.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel_email.Location = new System.Drawing.Point(41, 225);
            this.materialLabel_email.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel_email.Name = "materialLabel_email";
            this.materialLabel_email.Size = new System.Drawing.Size(1, 0);
            this.materialLabel_email.TabIndex = 8;
            this.materialLabel_email.UseAccent = true;
            // 
            // materialLabel_PS
            // 
            this.materialLabel_PS.AutoSize = true;
            this.materialLabel_PS.Depth = 0;
            this.materialLabel_PS.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel_PS.Location = new System.Drawing.Point(40, 307);
            this.materialLabel_PS.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel_PS.Name = "materialLabel_PS";
            this.materialLabel_PS.Size = new System.Drawing.Size(1, 0);
            this.materialLabel_PS.TabIndex = 9;
            this.materialLabel_PS.UseAccent = true;
            // 
            // materialLabel_PW
            // 
            this.materialLabel_PW.AutoSize = true;
            this.materialLabel_PW.BackColor = System.Drawing.SystemColors.Control;
            this.materialLabel_PW.Depth = 0;
            this.materialLabel_PW.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel_PW.Location = new System.Drawing.Point(41, 393);
            this.materialLabel_PW.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel_PW.Name = "materialLabel_PW";
            this.materialLabel_PW.Size = new System.Drawing.Size(1, 0);
            this.materialLabel_PW.TabIndex = 10;
            this.materialLabel_PW.UseAccent = true;
            // 
            // SignInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 486);
            this.Controls.Add(this.materialLabel_PW);
            this.Controls.Add(this.materialLabel_PS);
            this.Controls.Add(this.materialLabel_email);
            this.Controls.Add(this.materialLabel_ID);
            this.Controls.Add(this.signIn_materialButton);
            this.Controls.Add(this.confirmPassword_materialTextBox);
            this.Controls.Add(this.password_materialTextBox);
            this.Controls.Add(this.email_materialTextBox);
            this.Controls.Add(this.userName_materialTextBox);
            this.Name = "SignInForm";
            this.Text = "SIGN IN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox2 userName_materialTextBox;
        private MaterialSkin.Controls.MaterialTextBox2 email_materialTextBox;
        private MaterialSkin.Controls.MaterialTextBox2 password_materialTextBox;
        private MaterialSkin.Controls.MaterialTextBox2 confirmPassword_materialTextBox;
        private MaterialSkin.Controls.MaterialButton signIn_materialButton;
        private MaterialSkin.Controls.MaterialLabel materialLabel_ID;
        private MaterialSkin.Controls.MaterialLabel materialLabel_email;
        private MaterialSkin.Controls.MaterialLabel materialLabel_PS;
        private MaterialSkin.Controls.MaterialLabel materialLabel_PW;
    }
}