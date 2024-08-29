namespace PBAnaly
{
    partial class SettingForm
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
            this.materialSwitch_UI = new MaterialSkin.Controls.MaterialSwitch();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.materialSwitch_UI);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 232);
            this.panel1.TabIndex = 0;
            // 
            // materialSwitch_UI
            // 
            this.materialSwitch_UI.AutoSize = true;
            this.materialSwitch_UI.Depth = 0;
            this.materialSwitch_UI.Location = new System.Drawing.Point(11, 17);
            this.materialSwitch_UI.Margin = new System.Windows.Forms.Padding(0);
            this.materialSwitch_UI.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialSwitch_UI.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSwitch_UI.Name = "materialSwitch_UI";
            this.materialSwitch_UI.Ripple = true;
            this.materialSwitch_UI.Size = new System.Drawing.Size(195, 37);
            this.materialSwitch_UI.TabIndex = 12;
            this.materialSwitch_UI.Text = "Dark / Light Theme";
            this.materialSwitch_UI.UseVisualStyleBackColor = true;
            this.materialSwitch_UI.CheckedChanged += new System.EventHandler(this.materialSwitch_UI_CheckedChanged);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 299);
            this.Controls.Add(this.panel1);
            this.Name = "SettingForm";
            this.Text = "Setting";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialSwitch materialSwitch_UI;
    }
}