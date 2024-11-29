namespace PBAnaly.UI
{
    partial class SizeForm
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
            this.btb_row = new ReaLTaiizor.Controls.BigTextBox();
            this.btb_col = new ReaLTaiizor.Controls.BigTextBox();
            this.bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            this.btn_ok = new ReaLTaiizor.Controls.Button();
            this.btn_cancel = new ReaLTaiizor.Controls.Button();
            this.SuspendLayout();
            // 
            // btb_row
            // 
            this.btb_row.BackColor = System.Drawing.Color.Transparent;
            this.btb_row.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btb_row.ForeColor = System.Drawing.Color.DimGray;
            this.btb_row.Image = null;
            this.btb_row.Location = new System.Drawing.Point(12, 24);
            this.btb_row.MaxLength = 32767;
            this.btb_row.Multiline = false;
            this.btb_row.Name = "btb_row";
            this.btb_row.ReadOnly = false;
            this.btb_row.Size = new System.Drawing.Size(100, 41);
            this.btb_row.TabIndex = 0;
            this.btb_row.Text = "2";
            this.btb_row.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.btb_row.UseSystemPasswordChar = false;
            // 
            // btb_col
            // 
            this.btb_col.BackColor = System.Drawing.Color.Transparent;
            this.btb_col.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btb_col.ForeColor = System.Drawing.Color.DimGray;
            this.btb_col.Image = null;
            this.btb_col.Location = new System.Drawing.Point(185, 25);
            this.btb_col.MaxLength = 32767;
            this.btb_col.Multiline = false;
            this.btb_col.Name = "btb_col";
            this.btb_col.ReadOnly = false;
            this.btb_col.Size = new System.Drawing.Size(100, 41);
            this.btb_col.TabIndex = 1;
            this.btb_col.Text = "2";
            this.btb_col.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.btb_col.UseSystemPasswordChar = false;
            // 
            // bigLabel1
            // 
            this.bigLabel1.AutoSize = true;
            this.bigLabel1.BackColor = System.Drawing.Color.Transparent;
            this.bigLabel1.Font = new System.Drawing.Font("Segoe UI", 25F);
            this.bigLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.bigLabel1.Location = new System.Drawing.Point(128, 23);
            this.bigLabel1.Name = "bigLabel1";
            this.bigLabel1.Size = new System.Drawing.Size(40, 46);
            this.bigLabel1.TabIndex = 2;
            this.bigLabel1.Text = "X";
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.Transparent;
            this.btn_ok.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ok.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_ok.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_ok.Image = null;
            this.btn_ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ok.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_ok.Location = new System.Drawing.Point(12, 73);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_ok.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_ok.Size = new System.Drawing.Size(120, 40);
            this.btn_ok.TabIndex = 3;
            this.btn_ok.Text = "确定";
            this.btn_ok.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancel.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_cancel.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_cancel.Image = null;
            this.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_cancel.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_cancel.Location = new System.Drawing.Point(149, 73);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_cancel.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_cancel.Size = new System.Drawing.Size(120, 40);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // SizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 120);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.bigLabel1);
            this.Controls.Add(this.btb_col);
            this.Controls.Add(this.btb_row);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "SizeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SizeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.BigTextBox btb_row;
        private ReaLTaiizor.Controls.BigTextBox btb_col;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.Button btn_ok;
        private ReaLTaiizor.Controls.Button btn_cancel;
    }
}