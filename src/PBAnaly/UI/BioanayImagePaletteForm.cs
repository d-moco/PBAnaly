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
            get { return roi_w; }
            set { roi_w = value; ftb_w.Text = roi_w.ToString(); }
        }
        public int ROI_H
        {
            get { return roi_h; }
            set { roi_h = value; ftb_h.Text = roi_h.ToString(); }
        }
        public int CIRCLE_R
        {
            get { return circle_r; }
            set { circle_r = value; ftb_r.Text = circle_r.ToString(); }
        }
        #endregion
    }
}
