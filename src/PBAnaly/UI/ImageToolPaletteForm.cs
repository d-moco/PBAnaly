using System;
using System.Drawing;
using MaterialSkin;
using MaterialSkin.Controls;
using PBAnaly.Module;
using PBAnaly.Properties;

namespace PBAnaly.UI
{
    public partial class ImageToolPaletteForm : MaterialForm
    {
        private int roi_w = 20;
        private int roi_h = 20;
        private int circle_r = 10;
        public ImageToolPaletteForm()
        {
            InitializeComponent();
            mts_brightness.Value = ImageToolMannage.beta;
            nud_brightness.Value = mts_brightness.Value;
            nud_opacity.Value = mts_opacity.Value = (int)ImageToolMannage.alpha;
            mts_color_max.Value = ImageToolMannage.color_max;
            nud_color_max.Value = ImageToolMannage.color_max;
            mts_color_min.RangeMax = ImageToolMannage.color_max;
            mts_color_min.RangeMin = 5;
            nud_color_min.Maximum = ImageToolMannage.color_max;
            nud_color_min.Value = ImageToolMannage.color_min;
            mts_color_min.Value = ImageToolMannage.color_min;
         
            cb_colortable.SelectedIndex = 0;
        }

        private void mts_brightness_onValueChanged(object sender, int newValue)
        {
            //if (newValue > 254) newValue = 254;
            //if (newValue < 0) newValue = 0;
            //mts_brightness.Value = newValue;
            nud_brightness.Value = mts_brightness.Value;
            ImageToolMannage.beta = mts_brightness.Value;
           
            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();
        }
        private void nud_brightness_ValueChanged(object sender, EventArgs e)
        {
            mts_brightness.Value = (int)nud_brightness.Value;
            ImageToolMannage.beta = mts_brightness.Value;
            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();


        }
        private void mts_opacity_onValueChanged(object sender, int newValue)
        {
            nud_opacity.Value = mts_opacity.Value;
            ImageToolMannage.alpha = mts_opacity.Value;
            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();
        }

        private void mts_color_max_onValueChanged(object sender, int newValue)
        {
            if (newValue < 10)
                return;
            //mts_color_min.RangeMax = newValue;
            //mts_color_min.ValueMax = newValue;
            nud_color_min.Maximum = newValue;
            //mts_color_max.Value = newValue;
            nud_color_max.Value = newValue;
            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();

           
        }

        private void mts_color_min_onValueChanged(object sender, int newValue)
        {
            if (newValue < 5)
                return;
            //mts_color_min.Value = newValue;
            nud_color_min.Value = newValue;
            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();

          
        }

        private void nud_color_min_ValueChanged(object sender, EventArgs e)
        {

            if (nud_color_min.Value < 5) return;
            mts_color_min.Value = (int)nud_color_min.Value;
            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();
        }

        private void nud_color_max_ValueChanged(object sender, EventArgs e)
        {
           
            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();
           
            mts_color_max.Value = (int)nud_color_max.Value;
            mts_color_min.RangeMax = (int)nud_color_max.Value;
            mts_color_min.ValueMax = (int)nud_color_max.Value;

            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();
        }

        private void nud_opacity_ValueChanged(object sender, EventArgs e)
        {
            mts_opacity.Value = (int)nud_opacity.Value;
            ImageToolMannage.alpha = mts_opacity.Value;
            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();
        }

        public void SetactMM(string value) 
        {
            flb_act_mm.Text = value;
            flb_act_mm.Refresh();
        }

        private void hpb_line_Click(object sender, EventArgs e)
        {
            ImageToolMannage.lineDisON = true;
        }
        private void hpb_xianduan_Click(object sender, EventArgs e)
        {
            ImageToolMannage.linepolygonON = true;
        }

        private void hpb_wand_Click(object sender, EventArgs e)
        {
            ImageToolMannage.linewandON = true;
        }
        public void SetMaxMin(int min, int max) 
        {
            mts_color_min.Value = min;
            mts_color_max.Value = max;
            cb_colortable.SelectedIndex = 0;
        }

        private void cb_colortable_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bitmap rotatedImage = null;
            switch (cb_colortable.Text)
            {
                case "YellowHot":
                    rotatedImage = Resources.YellowHot_1;
                    rotatedImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                    
                    break;
                case "Black_Red":
                    rotatedImage = Resources.Black_Red_1;
                    rotatedImage.RotateFlip(RotateFlipType.Rotate90FlipX);

                    break;
                case "Black_Green":
                    rotatedImage = Resources.Black_Green_1;
                    rotatedImage.RotateFlip(RotateFlipType.Rotate90FlipX);

                    break;
                case "Black_Blue":
                    rotatedImage = Resources.Black_Blue_1;
                    rotatedImage.RotateFlip(RotateFlipType.Rotate90FlipX);

                    break;
                case "Black_Yley":
                    rotatedImage = Resources.Black_Yley_1;
                    rotatedImage.RotateFlip(RotateFlipType.Rotate90FlipX);

                    break;
                case "RGB":
                    rotatedImage = Resources.EtBr_1;
                    rotatedImage.RotateFlip(RotateFlipType.Rotate90FlipX);

                    break;
                case "Pseudo":
                    rotatedImage = Resources.Pseudo_1;
                    rotatedImage.RotateFlip(RotateFlipType.Rotate90FlipX);

                    break;
                case "Gray":
                    rotatedImage = Resources.Gray;
                    rotatedImage.RotateFlip(RotateFlipType.Rotate90FlipX);

                    break;
              

            }
            if (ImageToolMannage.imagePanel != null) 
            {
                ImageToolMannage.imagePanel.colorValue = cb_colortable.Text;
                ImageToolMannage.imagePanel.colorIndex = cb_colortable.SelectedIndex;
            }
            if (ImageToolMannage.imagePanel != null)
                ImageToolMannage.imagePanel.ThisRefresh();
            if (rotatedImage != null) 
            {
                pb_bgimage.Image = rotatedImage;
                pb_bgimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            }
            
          
        }
        

        private void hpb_rect_Click(object sender, EventArgs e)
        {
            ImageToolMannage.rectON = true;
        }
        private void hpb_circe_Click(object sender, EventArgs e)
        {
            ImageToolMannage.circleON = true;
        }
        private void fb_fixSetting_Click(object sender, EventArgs e)
        {
            roi_w = int.Parse(ftb_w.Text.ToString());
            roi_h = int.Parse(ftb_h.Text.ToString());
            circle_r = int.Parse(ftb_r.Text.ToString());
           // ImageToolMannage.imagePanel.SampleOneSize();

        }
        #region 对外接口
        int lastColor = -1;
        bool isMark = false;
        public void SetMark(bool ret = true)
        {
            if (ret)
            {
                isMark = true;
                lastColor = cb_colortable.SelectedIndex;
                cb_colortable.SelectedIndex = 7;
                cb_colortable.Enabled = false;
            }
            else
            {
                cb_colortable.Enabled = true;
                if (isMark == true) 
                {
                    if (lastColor > 0) 
                    {
                        cb_colortable.SelectedIndex = lastColor;
                        lastColor = -1;
                        isMark = false;
                    }
                   
                }
            }
        }

        public float ColorMax 
        {
            get 
            {
                if (nud_color_max.InvokeRequired)
                {

                    return (float)nud_color_max.Invoke(new Func<float>(() => (float)nud_color_max.Value));
                }
                else
                {

                    return (float)nud_color_max.Value;
                }
            }
        }

        public float ColorMin
        {
            get
            {
                if (nud_color_min.InvokeRequired)
                {

                    return (float)nud_color_min.Invoke(new Func<float>(() => (float)nud_color_min.Value));
                }
                else
                {

                    return (float)nud_color_min.Value;
                }
            }
        }
       
        public int brightness 
        {
            get
            {
                if (mts_brightness.InvokeRequired)
                {

                    return (int)mts_brightness.Invoke(new Func<int>(() => ( mts_brightness.Value -127)));
                }
                else
                {

                    return (mts_brightness.Value - 127);
                }
            }
        }
        public double opacity 
        {
            get
            {
                if (mts_opacity.InvokeRequired)
                {

                    return (double)mts_opacity.Invoke(new Func<double>(() => (mts_opacity.Value / 100.0)));
                }
                else
                {

                    return (mts_opacity.Value / 100.0);
                }
              
            }
        }
        public int colorbarIndex 
        {
            get {
                if (cb_colortable.InvokeRequired)
                {
                    
                    return (int)cb_colortable.Invoke(new Func<int>(() => cb_colortable.SelectedIndex));
                }
                else
                {
                   
                    return cb_colortable.SelectedIndex;
                }
            }
        }
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
