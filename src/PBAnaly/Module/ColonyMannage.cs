using PBAnaly.UI;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Web.UI.WebControls;


namespace PBAnaly.Module
{
    public class ColonyMannage
    {
        #region 结构体
        #endregion

        #region 变量
        public string path { get; set; }
        private Image<L16> image_L16;
        private byte[] image_byte;
        private byte[] image_8bit_rgb_byte;
        private Image<Rgb24> image_rgb_24 = null;
        private LanesImagePanel imagePanel = null;
        private BioanayImagePaletteForm imagePaletteForm = null;
        private PBBiologyVC.PBColonyVC pbvc = new PBBiologyVC.PBColonyVC();
        
        private Thread algThread;
        private bool isalgRun = false;
        private bool isUpdateAlg = false;

        #endregion

        public ColonyMannage(string _path, ReaLTaiizor.Controls.Panel _pl_right, Dictionary<string,ColonyMannage> colonyMannages) 
        {
            imagePanel = new LanesImagePanel();
            imagePanel.TopLevel = false;
            imagePanel.Show();
            imagePanel.BringToFront();

            this.path = _path;
            var ret = ReadTif();// 读tif档
            if (ret == false)
            {
                imagePanel.Dispose();
                imagePanel = null;
                return;
            }

            ImageAlg();
            RefreshImage();
        }


        #region 方法

        private void ImageAlg() 
        {
            unsafe
            {
                fixed (byte* p = image_byte) 
                {
                    pbvc.run(p, 16, (ushort)image_L16.Width, (ushort)image_L16.Height, -1, -1);
                }
            }
        }
        private bool ReadTif()
        {
            // 读tif 或 tiff 
            // 如果是tiff 需要弹出选择的一帧

            var extension = Path.GetExtension(path).Trim();
            if (extension == ".tif")
            {

                image_L16 = util.LoadTiffAsL16(path);
                image_byte = util.ConvertL16ImageToByteArray(image_L16);

            }

            if (image_L16 == null)
            {
                MessageBox.Show("图片加载失败");
                return false;
            }



            image_8bit_rgb_byte = new byte[image_L16.Width * image_L16.Height * 3];

            for (int i = 0; i < image_L16.Width * image_L16.Height; i++)
            {
                // 获取16位图像数据中的当前像素值
                ushort pixel16bit = (ushort)(image_byte[i * 2] | (image_byte[i * 2 + 1] << 8));
                byte gray = (byte)((pixel16bit / 65535.0) * 255);
                // 将R、G、B分量存储到RGB格式的数组中
                image_8bit_rgb_byte[i * 3] = gray;
                image_8bit_rgb_byte[i * 3 + 1] = gray;
                image_8bit_rgb_byte[i * 3 + 2] = gray;
            }

            image_rgb_24 = util.ConvertByteArrayToRgb24Image(image_8bit_rgb_byte, image_L16.Width, image_L16.Height, 3);
            imagePanel.SetButtomLabel($"{image_L16.Width} x {image_L16.Height}");
            if (path.Length > 0)
            {
                var t = path.Split("\\");
                if (t.Length > 2)
                {
                    imagePanel.SetButtomName($"{t[t.Length - 2]} {image_L16.Width} x {image_L16.Height}");
                }

            }
            return true;
        }

        private void RefreshImage()
        {
            if (imagePanel.image_pl.InvokeRequired)
            {
                imagePanel.image_pl.Invoke(new MethodInvoker(() =>
                {
                    RefreshImage();
                }));

            }
            else
            {
                imagePanel.SetImage(image_rgb_24);
            }
        }
        #endregion

        #region 对外接口
        #region imagepanel
        public LanesImagePanel GetImagePanel
        {
            get { return imagePanel; }
        }

        #endregion
        #endregion
    }
}
