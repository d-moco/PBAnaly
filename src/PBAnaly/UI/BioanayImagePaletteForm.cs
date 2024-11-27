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
        #endregion
    }
}
