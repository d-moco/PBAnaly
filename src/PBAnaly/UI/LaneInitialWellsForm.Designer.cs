namespace PBAnaly.UI
{
    partial class LaneInitialWellsForm
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
            this.lb_initWellInfo = new AntdUI.Label();
            this.cb_alwayShowWell = new ReaLTaiizor.Controls.CheckBox();
            this.btn_deleteLaneWell = new ReaLTaiizor.Controls.Button();
            this.btn_okLaneWell = new ReaLTaiizor.Controls.Button();
            this.btn_CencalLaneWell = new ReaLTaiizor.Controls.Button();
            this.SuspendLayout();
            // 
            // lb_initWellInfo
            // 
            this.lb_initWellInfo.Location = new System.Drawing.Point(12, 21);
            this.lb_initWellInfo.Name = "lb_initWellInfo";
            this.lb_initWellInfo.Size = new System.Drawing.Size(359, 23);
            this.lb_initWellInfo.TabIndex = 0;
            this.lb_initWellInfo.Text = "请点击泳道的初始位置";
            this.lb_initWellInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_alwayShowWell
            // 
            this.cb_alwayShowWell.Checked = false;
            this.cb_alwayShowWell.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(76)))), ((int)(((byte)(85)))));
            this.cb_alwayShowWell.CheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(76)))), ((int)(((byte)(85)))));
            this.cb_alwayShowWell.CheckedDisabledColor = System.Drawing.Color.Gray;
            this.cb_alwayShowWell.CheckedEnabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.cb_alwayShowWell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cb_alwayShowWell.Enable = true;
            this.cb_alwayShowWell.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cb_alwayShowWell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(125)))), ((int)(((byte)(132)))));
            this.cb_alwayShowWell.Location = new System.Drawing.Point(12, 63);
            this.cb_alwayShowWell.Name = "cb_alwayShowWell";
            this.cb_alwayShowWell.Size = new System.Drawing.Size(121, 16);
            this.cb_alwayShowWell.TabIndex = 1;
            this.cb_alwayShowWell.Text = "总是显示起始井";
            // 
            // btn_deleteLaneWell
            // 
            this.btn_deleteLaneWell.BackColor = System.Drawing.Color.Transparent;
            this.btn_deleteLaneWell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_deleteLaneWell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_deleteLaneWell.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_deleteLaneWell.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_deleteLaneWell.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_deleteLaneWell.Image = null;
            this.btn_deleteLaneWell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_deleteLaneWell.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_deleteLaneWell.Location = new System.Drawing.Point(48, 99);
            this.btn_deleteLaneWell.Name = "btn_deleteLaneWell";
            this.btn_deleteLaneWell.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_deleteLaneWell.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_deleteLaneWell.Size = new System.Drawing.Size(85, 34);
            this.btn_deleteLaneWell.TabIndex = 2;
            this.btn_deleteLaneWell.Text = "删除线";
            this.btn_deleteLaneWell.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btn_okLaneWell
            // 
            this.btn_okLaneWell.BackColor = System.Drawing.Color.Transparent;
            this.btn_okLaneWell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_okLaneWell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_okLaneWell.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_okLaneWell.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_okLaneWell.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_okLaneWell.Image = null;
            this.btn_okLaneWell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_okLaneWell.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_okLaneWell.Location = new System.Drawing.Point(157, 99);
            this.btn_okLaneWell.Name = "btn_okLaneWell";
            this.btn_okLaneWell.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_okLaneWell.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_okLaneWell.Size = new System.Drawing.Size(78, 35);
            this.btn_okLaneWell.TabIndex = 3;
            this.btn_okLaneWell.Text = "确定";
            this.btn_okLaneWell.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btn_CencalLaneWell
            // 
            this.btn_CencalLaneWell.BackColor = System.Drawing.Color.Transparent;
            this.btn_CencalLaneWell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_CencalLaneWell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_CencalLaneWell.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_CencalLaneWell.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_CencalLaneWell.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_CencalLaneWell.Image = null;
            this.btn_CencalLaneWell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_CencalLaneWell.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_CencalLaneWell.Location = new System.Drawing.Point(268, 99);
            this.btn_CencalLaneWell.Name = "btn_CencalLaneWell";
            this.btn_CencalLaneWell.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_CencalLaneWell.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_CencalLaneWell.Size = new System.Drawing.Size(78, 35);
            this.btn_CencalLaneWell.TabIndex = 4;
            this.btn_CencalLaneWell.Text = "取消";
            this.btn_CencalLaneWell.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // LaneInitialWellsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 145);
            this.Controls.Add(this.btn_CencalLaneWell);
            this.Controls.Add(this.btn_okLaneWell);
            this.Controls.Add(this.btn_deleteLaneWell);
            this.Controls.Add(this.cb_alwayShowWell);
            this.Controls.Add(this.lb_initWellInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(399, 184);
            this.MinimumSize = new System.Drawing.Size(399, 184);
            this.Name = "LaneInitialWellsForm";
            this.Text = "初始井";
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Label lb_initWellInfo;
        public ReaLTaiizor.Controls.CheckBox cb_alwayShowWell;
        public ReaLTaiizor.Controls.Button btn_deleteLaneWell;
        public ReaLTaiizor.Controls.Button btn_okLaneWell;
        public ReaLTaiizor.Controls.Button btn_CencalLaneWell;
    }
}