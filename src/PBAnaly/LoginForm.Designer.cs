namespace PBAnaly
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
            this.userName_materialTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            this.password_materialTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            this.Login_materialButton = new MaterialSkin.Controls.MaterialButton();
            this.SIGNIN_materialButton = new MaterialSkin.Controls.MaterialButton();
            this.rememberme_materialCheckbox = new MaterialSkin.Controls.MaterialCheckbox();
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
            this.userName_materialTextBox.Location = new System.Drawing.Point(59, 90);
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
            this.userName_materialTextBox.Size = new System.Drawing.Size(342, 48);
            this.userName_materialTextBox.TabIndex = 0;
            this.userName_materialTextBox.TabStop = false;
            this.userName_materialTextBox.Text = "User Name";
            this.userName_materialTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.userName_materialTextBox.TrailingIcon = null;
            this.userName_materialTextBox.UseSystemPasswordChar = false;
            this.userName_materialTextBox.Click += new System.EventHandler(this.userName_materialTextBox_Click);
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
            this.password_materialTextBox.Location = new System.Drawing.Point(59, 158);
            this.password_materialTextBox.MaxLength = 32767;
            this.password_materialTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.password_materialTextBox.Name = "password_materialTextBox";
            this.password_materialTextBox.PasswordChar = '*';
            this.password_materialTextBox.PrefixSuffixText = null;
            this.password_materialTextBox.ReadOnly = false;
            this.password_materialTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.password_materialTextBox.SelectedText = "";
            this.password_materialTextBox.SelectionLength = 0;
            this.password_materialTextBox.SelectionStart = 0;
            this.password_materialTextBox.ShortcutsEnabled = true;
            this.password_materialTextBox.Size = new System.Drawing.Size(342, 48);
            this.password_materialTextBox.TabIndex = 1;
            this.password_materialTextBox.TabStop = false;
            this.password_materialTextBox.Text = "Password";
            this.password_materialTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.password_materialTextBox.TrailingIcon = null;
            this.password_materialTextBox.UseSystemPasswordChar = false;
            this.password_materialTextBox.Click += new System.EventHandler(this.password_materialTextBox_Click);
            // 
            // Login_materialButton
            // 
            this.Login_materialButton.AutoSize = false;
            this.Login_materialButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Login_materialButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Login_materialButton.Depth = 0;
            this.Login_materialButton.HighEmphasis = true;
            this.Login_materialButton.Icon = null;
            this.Login_materialButton.Location = new System.Drawing.Point(59, 256);
            this.Login_materialButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Login_materialButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.Login_materialButton.Name = "Login_materialButton";
            this.Login_materialButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Login_materialButton.Size = new System.Drawing.Size(147, 36);
            this.Login_materialButton.TabIndex = 2;
            this.Login_materialButton.Text = "Login";
            this.Login_materialButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Login_materialButton.UseAccentColor = false;
            this.Login_materialButton.UseVisualStyleBackColor = true;
            this.Login_materialButton.Click += new System.EventHandler(this.Login_materialButton_Click);
            // 
            // SIGNIN_materialButton
            // 
            this.SIGNIN_materialButton.AutoSize = false;
            this.SIGNIN_materialButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SIGNIN_materialButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.SIGNIN_materialButton.Depth = 0;
            this.SIGNIN_materialButton.HighEmphasis = true;
            this.SIGNIN_materialButton.Icon = null;
            this.SIGNIN_materialButton.Location = new System.Drawing.Point(254, 256);
            this.SIGNIN_materialButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.SIGNIN_materialButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.SIGNIN_materialButton.Name = "SIGNIN_materialButton";
            this.SIGNIN_materialButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.SIGNIN_materialButton.Size = new System.Drawing.Size(147, 36);
            this.SIGNIN_materialButton.TabIndex = 3;
            this.SIGNIN_materialButton.Text = "Sign In";
            this.SIGNIN_materialButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.SIGNIN_materialButton.UseAccentColor = false;
            this.SIGNIN_materialButton.UseVisualStyleBackColor = true;
            this.SIGNIN_materialButton.Click += new System.EventHandler(this.SIGNIN_materialButton_Click);
            // 
            // rememberme_materialCheckbox
            // 
            this.rememberme_materialCheckbox.AutoSize = true;
            this.rememberme_materialCheckbox.Depth = 0;
            this.rememberme_materialCheckbox.Location = new System.Drawing.Point(59, 213);
            this.rememberme_materialCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.rememberme_materialCheckbox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rememberme_materialCheckbox.MouseState = MaterialSkin.MouseState.HOVER;
            this.rememberme_materialCheckbox.Name = "rememberme_materialCheckbox";
            this.rememberme_materialCheckbox.ReadOnly = false;
            this.rememberme_materialCheckbox.Ripple = true;
            this.rememberme_materialCheckbox.Size = new System.Drawing.Size(137, 37);
            this.rememberme_materialCheckbox.TabIndex = 4;
            this.rememberme_materialCheckbox.Text = "Remember Me";
            this.rememberme_materialCheckbox.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 318);
            this.Controls.Add(this.rememberme_materialCheckbox);
            this.Controls.Add(this.SIGNIN_materialButton);
            this.Controls.Add(this.Login_materialButton);
            this.Controls.Add(this.password_materialTextBox);
            this.Controls.Add(this.userName_materialTextBox);
            this.Name = "LoginForm";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox2 userName_materialTextBox;
        private MaterialSkin.Controls.MaterialTextBox2 password_materialTextBox;
        private MaterialSkin.Controls.MaterialButton Login_materialButton;
        private MaterialSkin.Controls.MaterialButton SIGNIN_materialButton;
        private MaterialSkin.Controls.MaterialCheckbox rememberme_materialCheckbox;
    }
}