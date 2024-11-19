using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using PBAnaly.Module;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace PBAnaly
{
    public partial class DataProcessForm : MaterialForm
    {
        private Mat image = null;
        private string filePath = "";
        private List<PBAnalyCommMannager.band_infos> bands = new List<PBAnalyCommMannager.band_infos>(); 
        private Cursor cursor = null;
        private int bands_index = -1;
        
        public DataProcessForm(MaterialSkinManager materialSkinManager,string filePath)
        {
            InitializeComponent();
            UIInit();
            Inn_materialSkinManager = materialSkinManager;
            this.filePath = filePath;
            image = Cv2.ImRead(filePath, ImreadModes.Unchanged);
            
            if (image.Depth() != MatType.CV_8U)
            {
                Mat convertedImage = new Mat();
                image.ConvertTo(convertedImage, MatType.CV_8U, 0.00390625);
                pictureBox1.Image = convertedImage.ToBitmap();
            }
            else
            {
                pictureBox1.Image = image.ToBitmap();
            }
            this.MouseDown += DataProcessForm_MouseDown;
            this.pictureBox1.MouseMove += PictureBox1_MouseMove;
            this.pictureBox1.MouseDown += PictureBox1_MouseDown;
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (cursor != null)
            {
                if (cursor == Cursors.SizeAll)
                {
                    for (int i = 0; i < bands.Count(); i++) 
                    {
                        PBAnalyCommMannager.band_infos _band= bands[i];
                        if (bands_index == i) 
                        {
                            _band.thick = 2;
                            if(PBAnalyCommMannager.laneChartForm!=null)
                                PBAnalyCommMannager.laneChartForm.Draw(_band);
                        }
                        else
                        {
                            _band.thick = 1;
                        }
                        bands[i] = _band;
                    }
                    Draw();
                }
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null) return;
            #region 
            // 获取PictureBox的尺寸和图像的原始尺寸
            float imageWidth = pictureBox1.Image.Width;
            float imageHeight = pictureBox1.Image.Height;
            float pictureBoxWidth = pictureBox1.Width;
            float pictureBoxHeight = pictureBox1.Height;

            // 计算缩放比例
            float scaleX = pictureBoxWidth / imageWidth;
            float scaleY = pictureBoxHeight / imageHeight;
            float scale = Math.Min(scaleX, scaleY);

            // 计算图像实际显示的尺寸
            float displayWidth = imageWidth * scale;
            float displayHeight = imageHeight * scale;

            // 计算图像在PictureBox中的位置偏移
            float offsetX = (pictureBoxWidth - displayWidth) / 2;
            float offsetY = (pictureBoxHeight - displayHeight) / 2;

            // 转换坐标
            int actualX = (int)Math.Floor((e.X - offsetX) / scale);
            int actualY = (int)Math.Floor((e.Y - offsetY) / scale);

            this.pictureBox1.Cursor = Cursors.Default;
            // 确保坐标在有效范围内
            if (actualX >= 0 && actualX <= imageWidth && actualY >= 0 && actualY <= imageHeight)
            {
                int index = 0;
                foreach (var item in bands)
                {
                    if (actualX >= item.startX - 3 && actualX <= item.startX + 3)
                    {
                        bands_index = index;
                        cursor = Cursors.SizeWE;
                        this.pictureBox1.Cursor = Cursors.SizeWE;
                        break;
                    }
                    else if (actualX > item.startX + 3 && actualX < item.endX - 3)
                    {
                        bands_index = index;
                        cursor = Cursors.SizeAll;
                        this.pictureBox1.Cursor = Cursors.SizeAll;
                        break;
                    }
                    else if (actualX >= item.endX - 3 && actualX <= item.endX + 3)
                    {
                        bands_index = index;
                        cursor = Cursors.SizeWE;
                        this.pictureBox1.Cursor = Cursors.SizeWE;
                        break;
                    }
                    index++;
                }

            }

            #endregion

            #region 根据获得到的图像索引去找是那个矩形
            #endregion

        }

        private void DataProcessForm_MouseDown(object sender, MouseEventArgs e)
        {
           
            PBAnalyCommMannager.processForm = this;
            Console.WriteLine("切换form:"+filePath);
        }     

        public MaterialSkinManager Inn_materialSkinManager;

        public void UIInit()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            Inn_materialSkinManager = MaterialSkinManager.Instance; // 初始化 MaterialSkinManager 实例
            Inn_materialSkinManager.AddFormToManage(this);  // 将要应用 Material Design 的窗体添加到管理列表中
            //Inn_materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;   // Theme 属性用来设置整体的主题
            //Inn_materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Cyan700, TextShade.WHITE);    // ColorScheme 属性来设置配色方案
        }



        #region 对外接口
        public Mat getImage 
        {
            get { return image; }
        }

        public Mat SetImage 
        {
            set
            {
               
                var bitmapImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(value);
                pictureBox1.Image = bitmapImage;
            }
        }

        public List<PBAnalyCommMannager.band_infos> SetBands 
        {
            set 
            {
                this.bands = value;
            }
        }

        /// <summary>
        /// 在原图基础上绘制泳道或者条带
        /// </summary>
        public void Draw() 
        {
            Mat input_cn1 = new Mat();
            image.ConvertTo(input_cn1, MatType.CV_8U, 0.00390625);
            if (input_cn1.Channels() == 1)
            {
                Cv2.CvtColor(input_cn1, input_cn1, ColorConversionCodes.GRAY2BGR);
            }
            foreach (var _band in bands) 
            {
               
                OpenCvSharp.Point rectStart = new OpenCvSharp.Point(_band.startX, _band.startY);
                OpenCvSharp.Point rectEnd = new OpenCvSharp.Point(_band.endX, _band.endY);
                Cv2.Rectangle(input_cn1, rectStart, rectEnd, _band.color, _band.thick);  // 线条粗细为2


                // 绘制十字形
                // 十字线长度
                int lineLength = 5;  // 可以根据实际需求调整
                float centerX = (float)(rectStart.X +(rectEnd.X - rectStart.X)/2.0);
                float centerY = rectStart.Y + _band._Info.band_point[0][0];
                int thickness = 1;
                Scalar crossColor = new Scalar(0, 0, 255);  // 红色
                // 水平线
                Cv2.Line(input_cn1, new OpenCvSharp.Point(centerX - lineLength, centerY), new OpenCvSharp.Point(centerX + lineLength, centerY), crossColor, thickness);
                // 垂直线
                Cv2.Line(input_cn1, new OpenCvSharp.Point(centerX, centerY - lineLength), new OpenCvSharp.Point(centerX, centerY + lineLength), crossColor, thickness);

                // 绘制条带矩形
                crossColor = new Scalar(255, 0, 0);  // 蓝色
                OpenCvSharp.Point rectStart1 = new OpenCvSharp.Point(_band.startX, rectStart.Y + _band._Info.band_point[0][1]);
                OpenCvSharp.Point rectEnd1 = new OpenCvSharp.Point(_band.endX, rectStart.Y + _band._Info.band_point[0][2]);
                Cv2.Rectangle(input_cn1, rectStart1, rectEnd1, crossColor, thickness);  // 线条粗细为2
            }
            SetImage = input_cn1;
        }
        #endregion
    }
}
