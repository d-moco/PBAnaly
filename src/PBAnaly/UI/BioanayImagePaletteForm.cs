using Aspose.Pdf.Drawing;
using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.UI
{
    public partial class BioanayImagePaletteForm : Form
    {
        public TextBox tb_min;
        public TextBox tb_max;
        private int roi_w = 20;
        private int roi_h = 20;
        private int circle_r = 10;
        public BioanayImagePaletteForm()
        {
            InitializeComponent();
        }

        #region 对外接口
        public int ROI_W
        {
            get {
                roi_w = Convert.ToInt32(dtb_w.Text.ToString());
                return roi_w; 
            }
            set { roi_w = value; dtb_w.Text = roi_w.ToString(); }
        }
        public int ROI_H
        {
            get {
                roi_h = Convert.ToInt32(dtb_h.Text.ToString());
                return roi_h;
            }
            set { roi_h = value; dtb_h.Text = roi_h.ToString(); }
        }
        public int CIRCLE_R
        {
            get {
                circle_r = Convert.ToInt32(dtb_r.Text.ToString());
                return circle_r; 
            }
            set { circle_r = value; dtb_r.Text = circle_r.ToString(); }
        }

        public string SetInfo
        {
            set { flb_info.Text = value; flb_info.Refresh(); }
        }

        public void RefreshscientificON(bool scientificON) 
        {
            if (scientificON == false) 
            {

                pl_max.Controls.Clear();
                pl_min.Controls.Clear();
                if (tb_max == null) return;
                tb_max.Dispose();
                tb_min.Dispose();
                tb_max = null;
                tb_min = null;
                pl_max.Controls.Add(nud_colorMax);
                pl_min.Controls.Add(nud_colorMin);
            }
            else
            {
                pl_max.Controls.Clear();
                pl_min.Controls.Clear();
                if (tb_max == null) 
                {
                    tb_max = new TextBox();
                    tb_max.Dock = DockStyle.Fill;
                    tb_max.Enabled = false;
                    tb_max.Multiline = true;
                    tb_max.Text = nud_colorMax.Value.ToString("E");
                }
                if (tb_min == null)
                {
                    tb_min = new TextBox();
                    tb_min.Dock = DockStyle.Fill;
                    tb_min.Enabled = false;
                    tb_min.Multiline = true;
                    tb_min.Text = nud_colorMin.Value.ToString("E");
                }
                pl_max.Controls.Add(tb_max);
                pl_min.Controls.Add(tb_min);
            }
        }
        #endregion
    }
}
