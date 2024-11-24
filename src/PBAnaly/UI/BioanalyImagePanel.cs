using PBAnaly.Module;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.UI
{
    public partial class BioanalyImagePanel : Form
    {

        #region Key 
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int HTLEFT = 0x10;
        private const int HTRIGHT = 0x11;
        private const int HTTOP = 0x12;
        private const int HTTOPLEFT = 0x13;
        private const int HTTOPRIGHT = 0x14;
        private const int HTBOTTOM = 0x15;
        private const int HTBOTTOMLEFT = 0x16;
        private const int HTBOTTOMRIGHT = 0x17;
        #endregion
        private const float ZoomMaxFactor = 2.0f;
        private const float ZoomMinFactor = 1.2f;
        private const float ZoomFactor = 1.2f;
        private float currentZoom = 1.0f;
        public BioanalyImagePanel()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            pl_bg_panel.Dock = DockStyle.None;
            pl_bg_panel.Location = new System.Drawing.Point(pl_panel_image.Location.X, pl_panel_image.Location.Y);
            pl_bg_panel.Width = pl_panel_image.Width;
            pl_bg_panel.Height = pl_panel_image.Height;
            CenterPictureBox();

            image_pl.MouseWheel += Image_pl_MouseWheel;
        }

       

        #region 对外方法
        public void SetButtomLabel(string value) 
        {
            lb_size.Text = value;
        }

        public void SetImage(Image<L16> image) 
        {
            if (image_pl.InvokeRequired) 
            {
                image_pl.Invoke(new MethodInvoker(() =>
                {
                    image_pl.Image = util.ConvertL16ToBitmap(image);
                }));
                
            }
            else
            {
                image_pl.Image = util.ConvertL16ToBitmap(image);
            }
           
           
        }
        public void SetImage(Image<L16> image, Image<Rgb24> colorbar)
        {
            if (image_pl.InvokeRequired)
            {
                image_pl.Invoke(new MethodInvoker(() =>
                {
                    image_pl.Image = util.ConvertL16ToBitmap(image);
                    image_pr.Image = util.ConvertRgb24ImageToBitmap(colorbar);
                }));

            }
            else
            {
                image_pl.Image = util.ConvertL16ToBitmap(image);
                image_pr.Image = util.ConvertRgb24ImageToBitmap(colorbar);
            }


        }
        public void SetImage(Image<Rgb24> image)
        {
            if (image_pl.InvokeRequired)
            {
                image_pl.Invoke(new MethodInvoker(() => 
                {
                    image_pl.Image = util.ConvertRgb24ImageToBitmap(image);
                }));
               
            }
            else
            {
                image_pl.Image = util.ConvertRgb24ImageToBitmap(image);
            }
            
        }
        public void SetImage(Image<Rgb24> image, Image<Rgb24> colorbar)
        {
            if (image_pl.InvokeRequired)
            {
                image_pl.Invoke(new MethodInvoker(() =>
                {
                    image_pl.Image = util.ConvertRgb24ImageToBitmap(image);
                    image_pr.Image = util.ConvertRgb24ImageToBitmap(colorbar);
                }));

            }
            else
            {
                image_pl.Image = util.ConvertRgb24ImageToBitmap(image);
                image_pr.Image = util.ConvertRgb24ImageToBitmap(colorbar);
            }

        }

        public bool IsImageLargerThanPanel()
        {
            return pl_bg_panel.Width > pl_panel_image.ClientSize.Width || pl_bg_panel.Height > pl_panel_image.ClientSize.Height;
        }

        #endregion
        #region WndProc
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    System.Drawing.Point pos = this.PointToClient(new System.Drawing.Point(m.LParam.ToInt32() & 0xFFFF, m.LParam.ToInt32() >> 16));


                    if (pos.X < 10)
                    {
                        if (pos.Y < 10) m.Result = (IntPtr)HTTOPLEFT;
                        else if (pos.Y > this.ClientSize.Height - 10) m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    }
                    else if (pos.X > this.ClientSize.Width - 10)
                    {
                        if (pos.Y < 10) m.Result = (IntPtr)HTTOPRIGHT;
                        else if (pos.Y > this.ClientSize.Height - 10) m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    }
                    else if (pos.Y < 10)
                    {
                        m.Result = (IntPtr)HTTOP;
                    }
                    else if (pos.Y > this.ClientSize.Height - 10)
                    {
                        m.Result = (IntPtr)HTBOTTOM;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        #region 事件
        private void ava_auto_Click(object sender, EventArgs e)
        {
           
            pl_bg_panel.Location = new System.Drawing.Point(pl_panel_image.Location.X, pl_panel_image.Location.Y);
            pl_bg_panel.Width = pl_panel_image.Width;
            pl_bg_panel.Height = pl_panel_image.Height;
            CenterPictureBox();
        }
        private void ava__zoom_in_Click(object sender, EventArgs e)
        {
            ZoomPictureBox(ZoomFactor);
        }
        private void ava_zoom_out_Click(object sender, EventArgs e)
        {
            ZoomPictureBox(1 / ZoomFactor);
        }
        private void Image_pl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                // 滚轮向上，放大图片
                ZoomPictureBox(ZoomFactor);
            }
            else if (e.Delta < 0)
            {
               
               ZoomPictureBox(1 / ZoomFactor);
            }
        }
        #endregion

        #region 方法
        public void CenterPictureBox()
        {
            // 设置 pl_bg_image 的位置，使其在 pl_image 中居中
            pl_bg_panel.Left = (pl_panel_image.ClientSize.Width - pl_bg_panel.Width) / 2;
            pl_bg_panel.Top = (pl_panel_image.ClientSize.Height - pl_bg_panel.Height) / 2;



            //// 防止图片超过panel的边界
            if (pl_bg_panel.Left < 0) pl_bg_panel.Left = 0;
            if (pl_bg_panel.Top < 0) pl_bg_panel.Top = 0;
        }
        private void ZoomPictureBox(float factor)
        {
           
            currentZoom *= factor;
            int w = (int)(pl_bg_panel.Width * factor);
            int h = (int)(pl_bg_panel.Height * factor);
            if (w < pl_panel_image.Width || h < pl_panel_image.Height) 
            {
                w = pl_panel_image.Width;
                h = pl_panel_image.Height;
            }

            if (w > pl_panel_image.Width * 5 || h > pl_panel_image.Height * 5)
            {
                w = pl_panel_image.Width * 5;
                h = pl_panel_image.Height * 5;
            }
            // 按照缩放比例调整pl_bg_image的宽度和高度
            pl_bg_panel.Width = w;
            pl_bg_panel.Height = h;
            // 调用方法使PB_image在pl_image中居中
            CenterPictureBox();
        }


        #endregion

        private void BioanalyImagePanel_MouseEnter(object sender, EventArgs e)
        {

        }
    }
}
