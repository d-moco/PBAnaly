using OpenCvSharp;
using PBAnaly.UI;
using PBBiologyVC;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PBAnaly.Module.BioanalysisMannage;

namespace PBAnaly.Module
{
    public class LanesMannage
    {
        #region 构造函数
        public struct band_infos
        {
            public float startX; // X作为筛选的必要条件  当鼠标进入x的范围就是进入某一个泳道,在根据y的范围判断是否在这个泳道里
            public float endX;
            public float startY;
            public float endY;
            public List<float> xdata;
            public List<float> ydata;
            public Scalar color;
            public int thick;
            public _band_info _Info;
        }
        #endregion
        #region 参数
        private string path { get; set; }
        private string curImagePath;

        PBBiology pbb = new PBBiology();
        private Image<L16> image_L16;
        private byte[] image_byte;
        private byte[] image_8bit_byte;
        private Image<Rgb24> image_rgb_24 = null;
        public bool IsActive { get; set; } // 当前窗口是否在活跃状态  用来判断是否需要操作
        public int ImageIndex { get; set; }// 图片加载进来的序号
        public int Arrangement { get; set; } // 0:代表单张图 1:代表是合并图图但不做处理 2:代表是合并图 并且为处理图
        private Dictionary<string, LanesMannage> lanesMannages;
        private ReaLTaiizor.Controls.Panel pl_right;
        private LanesImagePanel imagePanel = null;
        private LanesImagePaletteForm imagePaletteForm = null;
        private Thread algThread;
        private bool isalgRun = false;
        private bool isUpdateAlg = false;
        private Queue<band_infos> queueAlgAttribute = new Queue<band_infos>();
        #endregion

        public LanesMannage(string _path, ReaLTaiizor.Controls.Panel _pl_right, Dictionary<string, LanesMannage> lanesMannages) 
        {
            imagePanel = new  LanesImagePanel();
            imagePanel.TopLevel = false;
            imagePanel.Show();
            imagePanel.BringToFront();
            this.path = _path;

            var ret = ReadTiff(); // 读tif或者tiff
            if (ret == false) 
            {
                imagePanel.Dispose();
                imagePanel = null;
                return ;
            }

            isUpdateAlg = false;
            this.pl_right = _pl_right;


            imagePaletteForm = new  LanesImagePaletteForm();
            imagePaletteForm.TopLevel = false;
            imagePaletteForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_right.Controls.Add(imagePaletteForm);
            imagePaletteForm.BringToFront();
            imagePaletteForm.Show();

            Init();
            RefreshImage();// 初始化图像

            isalgRun = true;
            algThread = new Thread(new ThreadStart(AlgRun)); // 启动线程队列一直监听是否需要进行算法计算
            algThread.IsBackground = true;
            algThread.Start();

            isUpdateAlg = true;// 开始可以更新算法
            foreach (var item in lanesMannages)
            {
                if (item.Value.ImageIndex > ImageIndex)
                {
                    ImageIndex = item.Value.ImageIndex;
                }
            }
            ImageIndex++;
            imagePanel.lb_imageIndex.Text = ImageIndex.ToString();
            lanesMannages[_path] = this;
            this.lanesMannages = lanesMannages;
        }


        #region 方法

        private void AlgRun()
        {
            while (isalgRun)
            {
                if (isUpdateAlg == false) continue;
                band_infos? aatb = null;
                if (queueAlgAttribute.Count > 1)
                {
                    while (queueAlgAttribute.Count > 1)
                    {
                        queueAlgAttribute.Dequeue();
                    }
                }
                if (queueAlgAttribute.Count > 0)
                {
                    aatb = queueAlgAttribute.Dequeue();
                }
                if (aatb != null)
                {
                    ImageAlg((band_infos)aatb);

                }
                Thread.Sleep(5);
            }
        }

        private void ImageAlg(band_infos aatb)
        {

            //Mat image = new Mat(image_L16.Height,image_L16.Width,MatType.CV_8UC1);
            //Marshal.Copy(image_8bit_byte,0,image.Data,image_8bit_byte.Length);

            //Mat whiteBackgroundImg = new Mat();
            //Scalar meanValue = Cv2.Mean(image);
            //if (meanValue[0] < 10000)
            //{
            //    Cv2.BitwiseNot(image, whiteBackgroundImg);
            //}
            //else
            //{
            //    whiteBackgroundImg = image.Clone(); 
            //}

            //List<RectVC> proteinRect = new List<RectVC>();
            //// 算法使用的是8bit的图进行计算
            //unsafe
            //{
            //    fixed (byte* p = image_8bit_byte) 
            //    {
            //        proteinRect = pbb.getProteinRectVC(p, (ushort)image_L16.Width, (ushort)image_L16.Height);
            //    }


            //}
        }

        /// <summary>
        /// 初始化控件 初始化配置
        /// </summary>
        private void Init()
        {
            imagePanel.image_pl.MouseDown += Image_pl_MouseDown;
            imagePanel.image_pl.DoubleClick += Image_pl_DoubleClick;
            imagePanel.image_pl.MouseMove += Image_pl_MouseMove;
            imagePanel.image_pl.MouseUp += Image_pl_MouseUp;
            imagePanel.image_pl.Paint += Image_pl_Paint;


            imagePanel.wdb_title.MouseDown += Wdb_title_Click;
            imagePanel.FormClosing += ImagePanel_FormClosing;
            imagePanel.FormClosed += ImagePanel_FormClosed;



            imagePaletteForm.mb_findLanes.Click += Mb_findLanes_Click;

            KeyboardListener.Register(OnKeyPressed); // 创建键盘钩子
        }

       

        private bool ReadTiff()
        {
            // 读tif 或 tiff 
            // 如果是tiff 需要弹出选择的一帧

            var extension = Path.GetExtension(path).Trim();
            if (extension == ".tif")
            {
                curImagePath = path;
                image_L16 = util.LoadTiffAsL16(curImagePath);
                image_byte = util.ConvertL16ImageToByteArray(image_L16);

            }
            else if (extension == ".tiff")
            {
                curImagePath = path;
                MultiImageForm multiImageForm = new MultiImageForm(curImagePath);
                if (multiImageForm.ShowDialog() == DialogResult.OK)
                {
                    image_L16 = multiImageForm.curImage.Clone();
                    image_byte = util.ConvertL16ImageToByteArray(image_L16);
                    multiImageForm.Dispose();
                    multiImageForm = null;
                }
                else 
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            if (image_L16 == null)
            {
                MessageBox.Show("图片加载失败");
                return false;
            }

            image_8bit_byte = new byte[image_L16.Width * image_L16.Height * 3];
          
            for (int i = 0; i < image_L16.Width * image_L16.Height; i++) 
            {
                // 获取16位图像数据中的当前像素值
                ushort pixel16bit = (ushort)(image_byte[i * 2] | (image_byte[i * 2 + 1] << 8));
                byte gray = (byte)((pixel16bit / 65535.0) * 255) ;
                // 将R、G、B分量存储到RGB格式的数组中
                image_8bit_byte[i * 3] = gray;
                image_8bit_byte[i * 3 + 1] = gray;
                image_8bit_byte[i * 3 + 2] = gray;
            }

            image_rgb_24 = util.ConvertByteArrayToRgb24Image(image_8bit_byte, image_L16.Width, image_L16.Height,3);
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


        #region 事件
        #region imagepanel

        private void Image_pl_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void Image_pl_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void Image_pl_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Image_pl_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void Image_pl_MouseDown(object sender, MouseEventArgs e)
        {
            Wdb_title_Click(null, null);
        }
        private void ImagePanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.lanesMannages[path] = null;
            this.lanesMannages.Remove(path);
        }

        private void ImagePanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.imagePaletteForm != null)
            {
                KeyboardListener.Unregister(OnKeyPressed);
                this.imagePaletteForm.Close();
                this.imagePaletteForm.Dispose();
                this.imagePaletteForm = null;
            }
            this.pl_right.Controls.Clear();
        }

        private void Wdb_title_Click(object sender, MouseEventArgs e)
        {
            if (Arrangement == 2 || Arrangement == 0)
            {
                this.pl_right.Controls.Clear();
                this.pl_right.Controls.Add(this.imagePaletteForm);
            }

            foreach (var item in lanesMannages)
            {
                item.Value.IsActive = false;
            }
            IsActive = true;
            this.imagePanel.BringToFront();
        }
        #endregion

        #region imagePalette
        private void Mb_findLanes_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        private void OnKeyPressed(Keys key, bool ctrl, bool shift, bool alt)
        {
            
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
