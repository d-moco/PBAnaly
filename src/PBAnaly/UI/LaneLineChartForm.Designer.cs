namespace PBAnaly.UI
{
    partial class LaneLineChartForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单一泳道ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示多个泳道ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示所有泳道ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.选项ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(728, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 选项ToolStripMenuItem
            // 
            this.选项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单一泳道ToolStripMenuItem,
            this.显示多个泳道ToolStripMenuItem,
            this.显示所有泳道ToolStripMenuItem});
            this.选项ToolStripMenuItem.Name = "选项ToolStripMenuItem";
            this.选项ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.选项ToolStripMenuItem.Text = "选项";
            // 
            // 单一泳道ToolStripMenuItem
            // 
            this.单一泳道ToolStripMenuItem.Name = "单一泳道ToolStripMenuItem";
            this.单一泳道ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.单一泳道ToolStripMenuItem.Text = "显示单一泳道";
            // 
            // 显示多个泳道ToolStripMenuItem
            // 
            this.显示多个泳道ToolStripMenuItem.Name = "显示多个泳道ToolStripMenuItem";
            this.显示多个泳道ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示多个泳道ToolStripMenuItem.Text = "显示多个泳道";
            // 
            // 显示所有泳道ToolStripMenuItem
            // 
            this.显示所有泳道ToolStripMenuItem.Name = "显示所有泳道ToolStripMenuItem";
            this.显示所有泳道ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示所有泳道ToolStripMenuItem.Text = "显示所有泳道";
            // 
            // LaneLineChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 394);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LaneLineChartForm";
            this.Text = "泳道轮廓";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单一泳道ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示多个泳道ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示所有泳道ToolStripMenuItem;
    }
}