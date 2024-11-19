namespace PBAnaly
{
    partial class DataProcessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataProcessForm));
            this.PictureBoxpanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Control_panel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.materialButton_Load = new MaterialSkin.Controls.MaterialButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialFloatingActionButton_Plus = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.materialFloatingActionButton_Minus = new MaterialSkin.Controls.MaterialFloatingActionButton();
            this.PictureBoxpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Control_panel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBoxpanel
            // 
            this.PictureBoxpanel.Controls.Add(this.pictureBox1);
            this.PictureBoxpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PictureBoxpanel.Location = new System.Drawing.Point(3, 72);
            this.PictureBoxpanel.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBoxpanel.Name = "PictureBoxpanel";
            this.PictureBoxpanel.Size = new System.Drawing.Size(408, 351);
            this.PictureBoxpanel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(408, 351);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Control_panel
            // 
            this.Control_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Control_panel.Controls.Add(this.panel2);
            this.Control_panel.Controls.Add(this.panel1);
            this.Control_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Control_panel.Location = new System.Drawing.Point(3, 24);
            this.Control_panel.Name = "Control_panel";
            this.Control_panel.Size = new System.Drawing.Size(408, 44);
            this.Control_panel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.materialButton_Load);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(42, 40);
            this.panel2.TabIndex = 1;
            // 
            // materialButton_Load
            // 
            this.materialButton_Load.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialButton_Load.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton_Load.Depth = 0;
            this.materialButton_Load.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialButton_Load.HighEmphasis = true;
            this.materialButton_Load.Icon = ((System.Drawing.Image)(resources.GetObject("materialButton_Load.Icon")));
            this.materialButton_Load.Location = new System.Drawing.Point(0, 0);
            this.materialButton_Load.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialButton_Load.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton_Load.Name = "materialButton_Load";
            this.materialButton_Load.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton_Load.Size = new System.Drawing.Size(64, 40);
            this.materialButton_Load.TabIndex = 0;
            this.materialButton_Load.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton_Load.UseAccentColor = true;
            this.materialButton_Load.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.materialLabel1);
            this.panel1.Controls.Add(this.materialFloatingActionButton_Plus);
            this.panel1.Controls.Add(this.materialFloatingActionButton_Minus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(259, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(145, 40);
            this.panel1.TabIndex = 0;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            this.materialLabel1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.materialLabel1.Location = new System.Drawing.Point(54, 12);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(16, 14);
            this.materialLabel1.TabIndex = 2;
            this.materialLabel1.Text = "1X";
            // 
            // materialFloatingActionButton_Plus
            // 
            this.materialFloatingActionButton_Plus.Depth = 0;
            this.materialFloatingActionButton_Plus.Icon = ((System.Drawing.Image)(resources.GetObject("materialFloatingActionButton_Plus.Icon")));
            this.materialFloatingActionButton_Plus.Location = new System.Drawing.Point(88, 2);
            this.materialFloatingActionButton_Plus.Mini = true;
            this.materialFloatingActionButton_Plus.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton_Plus.Name = "materialFloatingActionButton_Plus";
            this.materialFloatingActionButton_Plus.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton_Plus.TabIndex = 1;
            this.materialFloatingActionButton_Plus.Text = "materialFloatingActionButton2";
            this.materialFloatingActionButton_Plus.UseVisualStyleBackColor = true;
            // 
            // materialFloatingActionButton_Minus
            // 
            this.materialFloatingActionButton_Minus.Depth = 0;
            this.materialFloatingActionButton_Minus.Icon = ((System.Drawing.Image)(resources.GetObject("materialFloatingActionButton_Minus.Icon")));
            this.materialFloatingActionButton_Minus.Location = new System.Drawing.Point(2, 2);
            this.materialFloatingActionButton_Minus.Mini = true;
            this.materialFloatingActionButton_Minus.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFloatingActionButton_Minus.Name = "materialFloatingActionButton_Minus";
            this.materialFloatingActionButton_Minus.Size = new System.Drawing.Size(40, 40);
            this.materialFloatingActionButton_Minus.TabIndex = 0;
            this.materialFloatingActionButton_Minus.Text = "materialFloatingActionButton1";
            this.materialFloatingActionButton_Minus.UseVisualStyleBackColor = true;
            // 
            // DataProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 426);
            this.Controls.Add(this.PictureBoxpanel);
            this.Controls.Add(this.Control_panel);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "DataProcessForm";
            this.Padding = new System.Windows.Forms.Padding(3, 24, 3, 3);
            this.Text = "DataProcessForm";
            this.PictureBoxpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Control_panel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PictureBoxpanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel Control_panel;
        private System.Windows.Forms.Panel panel2;
        private MaterialSkin.Controls.MaterialButton materialButton_Load;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton_Plus;
        private MaterialSkin.Controls.MaterialFloatingActionButton materialFloatingActionButton_Minus;
    }
}