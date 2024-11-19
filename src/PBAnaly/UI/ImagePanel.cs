using MaterialSkin;
using MaterialSkin.Controls;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using PBAnaly.Module;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Metadata;
using Aspose.Pdf.Drawing;
using System.Data.SqlTypes;
using System.Net.PeerToPeer.Collaboration;
using ScottPlot.Panels;
using ScottPlot;
using static Aspose.Pdf.CollectionItem;
using System.Collections.Generic;
using PBBiologyVC;
using System.Threading;
using AntdUI;
using Sunny.UI;
using System.Linq;
using MiniExcelLibs;
using ScottPlot.Colormaps;
using ReaLTaiizor.Extension;
using OpenTK.Graphics.ES11;

namespace PBAnaly.UI
{
    public partial class ImagePanel : MaterialForm
    {
        #region 变量
        private MaterialSkinManager Inn_materialSkinManager; // 风格
        private string path;
        private string markFilePath;
        private string stnFilePath;
        private bool scientificON = false;
        public ImageToolPaletteForm paletteForm = null;
        public ReaLTaiizor.Controls.Panel main_panel = null;

        PBBiologyVC.PBImageProcessVC pbpvc = new PBBiologyVC.PBImageProcessVC();

        private Image<Rgb24> pseu_rgb24_image = null;
        private Image<Rgb24> pseu_and_mark__rgb24_image = null;
        
        private Image<L16> pseu_L16_image = null;
        private Image<L16> mark_L16_image = null;

        private Image<Rgb24> colorbar_rgb24_image = null;
        private byte[] colorbarimage = null;
        private byte[] pseu_16_byte = null;
        private byte[] pseu_8bit_3_byte = null;
        private byte[] pseu_and_mark_8bit_3_byte = null;
        private byte[] mark_L16_byte = null;

        private int colorbarWW = 200, colorh_onecolor = 10;
        private int colorbarW = 600, colorbarH = 2600;
        private int[] mark_h = null;
        public int mark_max;
        public int mark_min;

        private Bitmap curBitmap = null;

        private const float ZoomFactor = 1.2f;
        private float currentZoom = 1.0f;
        // 鼠标拖动相关变量
        private bool isDragging = false;
        private System.Drawing.Point mouseDownPosition;
        private System.Drawing.Point pictureBoxStartPosition;

        private bool drawLine = false;
        private bool drawRect = false;
        private bool drawCircle = false;
        private bool drawpolygon = false;
        private System.Drawing.Point startPoint = new System.Drawing.Point(-10,0);
        private System.Drawing.Point endPoint = new System.Drawing.Point(-10, 0);
        private System.Drawing.Point startRectPoint = new System.Drawing.Point(-10, 0);
        private System.Drawing.Point startCirclePoint = new System.Drawing.Point(-10, 0);
        private struct RectAndInfo 
        {
            public System.Drawing.Rectangle rectangles;
            public Pseudo_infoVC pdinfovc;
        }
        private List<RectAndInfo> rectAndInfoList = new List<RectAndInfo>();
        private System.Drawing.Rectangle curRectangle = new System.Drawing.Rectangle(0,0,0,0);
        private System.Drawing.Point curRectanglePoint;
        private System.Drawing.Point originalMousePosition;
        private RectAndInfo movingRectangle;
        private int curRectIndex = -1;
        private Pseudo_infoVC curpdinfovc = null;
        private struct CirceAndInfo
        {
            public System.Drawing.Point center;
            public System.Drawing.Point Radius { get; set; }
            public Pseudo_infoVC pdinfovc;
        }
        private List<CirceAndInfo> CircleAndInfoList = new List<CirceAndInfo>();
        private System.Drawing.Point curCirRadioPoint = new System.Drawing.Point( 0, 0);
        private System.Drawing.Point curCirCenterPoint = new System.Drawing.Point(0, 0);
        private System.Drawing.Point curCirPoint = new System.Drawing.Point(0, 0);
        private System.Drawing.Point cirMousePosition;
        private System.Drawing.Point cirRadioMousePosition;
        private int curCircleIndex = -1;
        private struct PolygonAndInfo
        {
            public List<System.Drawing.Point> points ;
            public Pseudo_infoVC pdinfovc;
        }
        private List<PolygonAndInfo> PolygonAndInfoList = new List<PolygonAndInfo>();
        private PolygonAndInfo curPolygonAndInfoList = new PolygonAndInfo();
        private System.Drawing.Point curStartPolygonPoint = new System.Drawing.Point(0, 0);
        private System.Drawing.Point curPolygonPoint = new System.Drawing.Point(0, 0);

        private bool isStartCircleDragged, isEndCircleDragged,isEndRectDragged,isCircleFragged;
        private bool  isLineDragged;
        private System.Drawing.Point previousMousePosition; // 记录鼠标的前一个位置
        private const int CircleRadius = 5; // 圆圈半径
        private string _colorValue;
        private int _colorIndex;
        public string colorValue 
        {
            set {

                _colorValue = value;
            }
        }
        public int colorIndex 
        {
            set 
            {
                _colorIndex = value;

            }
        }
        #endregion
        public ImagePanel(string filePath, ReaLTaiizor.Controls.Panel _main_panel)
        {
            InitializeComponent();
            path = filePath;
            pl_bg_image.Dock = DockStyle.None;
            main_panel = _main_panel;
            RefreshUI();
            Init();
            CenterPictureBox();

            pb_image.MouseDown += pb_image_MouseDown;
            pb_image.DoubleClick += Pb_image_DoubleClick;
            pb_image.MouseMove += pb_image_MouseMove;
            pb_image.MouseUp += pb_image_MouseUp;
            pb_image.Paint += Pb_image_Paint;

            pl_image.MouseWheel += Pl_image_MouseWheel;
            pl_image.Focus();

            // 使用反射设置双缓冲
            System.Reflection.PropertyInfo property = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            SetDoubleBuffering(pb_image,true);

           // ImageProcess.DrawRuler(pictureBox1);
        }

        


        #region 方法
        public void RefreshUI() 
        {
            ImageToolMannage.imagePanel = this;
            main_panel.Controls.Clear();
            if(paletteForm == null)
                paletteForm = new ImageToolPaletteForm();
            paletteForm.TopLevel = false;
            main_panel.Controls.Add(paletteForm);
            paletteForm.Dock = DockStyle.Fill;
            paletteForm.Show();
        }
        private void SetDoubleBuffering(Control control, bool value)
        {
            // 使用反射设置双缓冲
            System.Reflection.PropertyInfo property = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            property.SetValue(control, value, null);
        }
        public void Init() 
        {
            string[] tifFiles = Directory.GetFiles(path, "*.tif", SearchOption.TopDirectoryOnly);

            foreach (string tifFile in tifFiles) 
            {
                if (tifFile.ToUpper().Contains("MARKER"))
                {
                    markFilePath = tifFile;
                    mark_L16_image = util.LoadTiffAsL16(markFilePath);
                    mark_L16_byte = util.ConvertL16ImageToByteArray(mark_L16_image);
                    (mark_h, mark_min,mark_max) = ImageProcess.CalculateHistogram(mark_L16_image);
                    
                }
                else
                {
                    stnFilePath = tifFile;
                    pseu_L16_image = util.LoadTiffAsL16(stnFilePath);
                }
            }

            if (mark_L16_image == null || pseu_L16_image == null) 
            {
                MessageBox.Show("图片格式加载不正确");
                return;
            }
            mlb_bottomLabel.Text = pseu_L16_image.Width.ToString() + "x" + pseu_L16_image.Height.ToString();
            pseu_16_byte = util.ConvertL16ImageToByteArray(pseu_L16_image);
            pseu_8bit_3_byte = new byte[pseu_L16_image.Width * pseu_L16_image.Height * 3];
            pseu_and_mark_8bit_3_byte = new byte[pseu_L16_image.Width * pseu_L16_image.Height * 3];
           
            colorbarimage = new byte[colorbarW* colorbarH * 3];
            colorbar_rgb24_image = util.ConvertByteArrayToRgb24Image(colorbarimage, colorbarW , colorbarH , 3);
            int colorIndex = paletteForm.colorbarIndex;
            float _max = paletteForm.ColorMax;
            float _min = paletteForm.ColorMin;
           
            double opactity = paletteForm.opacity;
            int brightness = paletteForm.brightness;
            unsafe
            {
                fixed (byte* pseu_16_byte_src = pseu_16_byte) 
                {
                    fixed (byte* pseu_8bit_3_byte_src = pseu_8bit_3_byte)
                    {
                        fixed (byte* pseu_and_mark_8bit_3_byte_src= pseu_and_mark_8bit_3_byte) 
                        {
                            fixed (byte* mark_L16_byte_src = mark_L16_byte) 
                            {
                                fixed (byte* colorbarimage_src = colorbarimage) 
                                {
                                    var ret = pbpvc.render_process(pseu_16_byte_src, mark_L16_byte_src, pseu_8bit_3_byte_src,
                                        pseu_and_mark_8bit_3_byte_src, colorbarimage_src, colorIndex, (ushort)(colorbarW), (ushort)colorbarH, 16, (ushort)pseu_L16_image.Width, (ushort)pseu_L16_image.Height,
                                        1,_max, _min,scientificON == true?1:0, false, (ushort)colorbarWW, (ushort)colorh_onecolor, brightness, 1.0, opactity);
                                    if (ret != -1) 
                                    {
                                        if (colorIndex != 7) // 不为gray表
                                        {
                                            pseu_rgb24_image = util.ConvertByteArrayToRgb24Image(pseu_8bit_3_byte, pseu_L16_image.Width, (ushort)pseu_L16_image.Height, 3);
                                            pseu_and_mark__rgb24_image = util.ConvertByteArrayToRgb24Image(pseu_and_mark_8bit_3_byte, pseu_L16_image.Width, (ushort)pseu_L16_image.Height, 3);
                                            colorbar_rgb24_image = util.ConvertByteArrayToRgb24Image(colorbarimage, colorbarW, colorbarH, 3);
                                        }
                                    }
                                }
                                    
                              
                            }
                           
                        }
                       
                    }
                }
            }
            
            mcb_mode.SelectedIndex = 0;
        }
        private void CenterPictureBox()
        {
            // 设置 pl_bg_image 的位置，使其在 pl_image 中居中
            pl_bg_image.Left = (pl_image.ClientSize.Width - pl_bg_image.Width) / 2;
            pl_bg_image.Top = (pl_image.ClientSize.Height - pl_bg_image.Height) / 2;

            //// 确保Panel的AutoScroll属性为true以便在放大后查看整个图片
            pl_image.AutoScroll = true;

            //// 防止图片超过panel的边界
            if (pl_bg_image.Left < 0) pl_bg_image.Left = 0;
            if (pl_bg_image.Top < 0) pl_bg_image.Top = 0;
        }
        private void ZoomPictureBox(float factor)
        {
            // 按照缩放倍数调整图片的宽度和高度
            //pl_bg_image.Width = (int)(pl_bg_image.Width * factor);
            //pl_bg_image.Height = (int)(pl_bg_image.Height * factor);
            currentZoom *= factor;

            // 按照缩放比例调整pl_bg_image的宽度和高度
            pl_bg_image.Width = (int)(pl_bg_image.Width * factor);
            pl_bg_image.Height = (int)(pl_bg_image.Height * factor);
            // 调用方法使PB_image在pl_image中居中
            CenterPictureBox();
        }
        private bool IsImageLargerThanPanel()
        {
            return pl_bg_image.Width > pl_image.ClientSize.Width || pl_bg_image.Height > pl_image.ClientSize.Height;
        }
        private DateTime lastExecutionTime = DateTime.MinValue;
        private int throttleTime = 150; // 设置节流时间间隔（毫秒）
        public async void ThisRefresh()
        {
            if ((DateTime.Now - lastExecutionTime).TotalMilliseconds < throttleTime)
            {
                return; // 如果距离上次调用时间间隔小于节流时间，则不执行
            }

            lastExecutionTime = DateTime.Now;
            if (mcb_mode.SelectedIndex == 0)
            {
               await Task.Run(() =>
                {
                    // 渲染操作在异步任务中执行

                    byte[] colorbarimage = new byte[colorbarW * colorbarH * 3];
                    int colorIndex = paletteForm.colorbarIndex;
                    float _max = paletteForm.ColorMax;
                    float _min = paletteForm.ColorMin;
                
                    double opactity = paletteForm.opacity;
                    int brightness = paletteForm.brightness;
                    unsafe
                    {

                        fixed (byte* pseu_16_byte_src = pseu_16_byte)
                        {
                            fixed (byte* pseu_8bit_3_byte_src = pseu_8bit_3_byte)
                            {
                                fixed (byte* pseu_and_mark_8bit_3_byte_src = pseu_and_mark_8bit_3_byte)
                                {
                                    fixed (byte* mark_L16_byte_src = mark_L16_byte)
                                    {
                                        fixed (byte* colorbarimage_src = colorbarimage)
                                        {
                                            var ret = pbpvc.render_process(pseu_16_byte_src, mark_L16_byte_src, pseu_8bit_3_byte_src,
                                         pseu_and_mark_8bit_3_byte_src, colorbarimage_src, colorIndex, (ushort)(colorbarW), (ushort)colorbarH, 16, (ushort)pseu_L16_image.Width, (ushort)pseu_L16_image.Height,
                                         1, _max, _min, scientificON == true ? 1 : 0, false, (ushort)colorbarWW, (ushort)colorh_onecolor, brightness, 1.0, opactity);
                                            if (ret != -1)
                                            {
                                                pseu_rgb24_image = util.ConvertByteArrayToRgb24Image(pseu_8bit_3_byte, pseu_L16_image.Width, (ushort)pseu_L16_image.Height, 3);
                                                pseu_and_mark__rgb24_image = util.ConvertByteArrayToRgb24Image(pseu_and_mark_8bit_3_byte, pseu_L16_image.Width, (ushort)pseu_L16_image.Height, 3);
                                                colorbar_rgb24_image = util.ConvertByteArrayToRgb24Image(colorbarimage, colorbarW, colorbarH, 3);
                                            }
                                        }
                                    }

                                }

                            }
                        }
                       
                    }

                    // 更新 PictureBox 图像
                    //Action updateImageAction = () =>
                    //{
                    //    pb_image.Image = util.ConvertRgb24ImageToBitmap(pseu_and_mark__rgb24_image);
                    //    curBitmap = (Bitmap)pb_image.Image;
                    //    pb_coloarbar_image.Image = util.ConvertRgb24ImageToBitmap(colorbar_rgb24_image);
                    //    pb_image.Refresh();
                    //    lb_wh.Text = "color scale\n min=" + _min.ToString() + "\n max=" + _max.ToString();
                    //};

                   // 检查是否需要在 UI 线程上更新
                    if (pb_image.InvokeRequired)
                    {
                        pb_image.Invoke((Action)(() => UpdateImages(_min, _max)));
                    }
                    else
                    {
                        UpdateImages(_min, _max);
                    }
                });
            }
            
        }
        private void UpdateImages(float _min, float _max)
        {
            pb_image.Image = util.ConvertRgb24ImageToBitmap(pseu_and_mark__rgb24_image);
            curBitmap = (Bitmap)pb_image.Image;
            pb_coloarbar_image.Image = util.ConvertRgb24ImageToBitmap(colorbar_rgb24_image);
            pb_image.Refresh();
            if (scientificON)
            {

                lb_wh.Text = "Radiance (p/sec/cm²/sr)\n color scale\n min=" + util.GetscientificNotation(_min) + "\n max=" + util.GetscientificNotation(_max);
            }
            else
            {
                lb_wh.Text = "Radiance (p/sec/cm²/sr)\n color scale\n min=" + _min.ToString() + "\n max=" + _max.ToString();
            }
           
        }
        public void SampleOneSize() 
        {
            var _w =  paletteForm.ROI_W;
            var _h = paletteForm.ROI_H;
            for (int i = 0; i < rectAndInfoList.Count; i++)
            {
                RectAndInfo raif = new RectAndInfo();
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(rectAndInfoList[i].rectangles.X, rectAndInfoList[i].rectangles.Y, _w, _h);
                Pseudo_infoVC pivc = rectAndInfoList[i].pdinfovc;
                raif.pdinfovc = pivc;
                rectAndInfoList[i] = raif;
            }
            var _r = paletteForm.CIRCLE_R;

            for (int i = 0; i < CircleAndInfoList.Count; i++)
            {
                CirceAndInfo raif = new CirceAndInfo();
                double angleInRadians = 90 * Math.PI / 180; // Convert degrees to radians
                double x = CircleAndInfoList[i].center.X + paletteForm.CIRCLE_R * Math.Cos(angleInRadians);
                double y = CircleAndInfoList[i].center.Y + paletteForm.CIRCLE_R * Math.Sin(angleInRadians);

                raif.Radius = new System.Drawing.Point((int)x, (int)y);

                Pseudo_infoVC pivc = rectAndInfoList[i].pdinfovc;
                raif.pdinfovc = pivc;
                raif.center = CircleAndInfoList[i].center;
                CircleAndInfoList[i] = raif;
            }

            pb_image.Invalidate();
        }

        public void updateBrightness() 
        {
            
            //Mat mat = curBitmap.ToMat();
            //int beta = ImageToolMannage.beta - 127;
            //Cv2.ConvertScaleAbs(mat,mat,1,beta);
            //pb_image.Image = mat.ToBitmap();
            
        }
        private bool IsPointInRectangleCorner(System.Drawing.Point point, System.Drawing.Rectangle rect)
        {
            const int cornerDistance = 10; // 角落拖动区域的距离

            // 计算矩形四个角落的位置
            System.Drawing.Point[] corners = new System.Drawing.Point[]
            {
                new System.Drawing.Point(rect.Left, rect.Top),     // 左上角
                new System.Drawing.Point(rect.Right, rect.Top),    // 右上角
                new System.Drawing.Point(rect.Left, rect.Bottom),  // 左下角
                new System.Drawing.Point(rect.Right, rect.Bottom)  // 右下角
            };

            // 判断鼠标是否在某个角落的拖动区域内
            foreach (var corner in corners)
            {
                if (Math.Abs(corner.X - point.X) <= cornerDistance && Math.Abs(corner.Y - point.Y) <= cornerDistance)
                {
                    return true;
                }
            }

            return false;
        }
        // 判断点是否在矩形里
        private bool IsPointInRectangles(System.Drawing.Point point, List<RectAndInfo> rectangles)
        {
            foreach (var rect in rectangles)
            {
                if (rect.rectangles.Contains(point))
                    return true;
            }
            return false;
        }

        private bool IsPointInRectangles(System.Drawing.Point point, System.Drawing.Rectangle rectangles)
        {
            return rectangles.Contains(point);
        }
        // 判断点是否在圆圈内
        private bool IsPointInCircle(System.Drawing.Point point, List<CirceAndInfo> circleCenter)
        {
            foreach (var circle in circleCenter) 
            {
                int radius = (int)Math.Sqrt(Math.Pow(circle.center.X - circle.Radius.X, 2) + Math.Pow(circle.center.Y - circle.Radius.Y, 2));
                double distance = Math.Sqrt(Math.Pow(point.X - circle.center.X, 2) + Math.Pow(point.Y - circle.center.Y, 2));
                if (distance <= radius) return true;
            }

            return false;
        }
        private bool IsPointInCircle(System.Drawing.Point point, System.Drawing.Point circle)
        {
            double distance = Math.Sqrt(Math.Pow(point.X - circle.X, 2) + Math.Pow(point.Y - circle.Y, 2));
            return distance <= CircleRadius;
        }

        private bool IsPointInPolygon(System.Drawing.Point testPoint, PolygonAndInfo polygon)
        {
            if (polygon.points == null) return false; ;
            var points = polygon.points;
            bool result = false;
            int j = points.Count - 1; // The last vertex is the 'previous' one to the first

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y < testPoint.Y && points[j].Y >= testPoint.Y || points[j].Y < testPoint.Y && points[i].Y >= testPoint.Y)
                {
                    if (points[i].X + (testPoint.Y - points[i].Y) / (points[j].Y - points[i].Y) * (points[j].X - points[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i; // j is previous vertex to i
            }

            return result;
        }
        // 绘制圆圈的帮助方法
        private void DrawCircle(Graphics g, System.Drawing.Point center, int radius, Pen pen, Brush brush)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            g.FillEllipse(brush, rect);
            g.DrawEllipse(pen, rect);
        }
        private bool IsPointOnLine(System.Drawing.Point point)
        {
            System.Drawing.Point readLoction = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, point);
            // 使用线段的最小距离判断点是否在线段上
            double distance = DistanceToLine(readLoction, startPoint, endPoint);
            return distance <= CircleRadius; // 如果距离小于等于圆圈半径，认为在线段上
        }
        private double DistanceToLine(System.Drawing.Point p, System.Drawing.Point p1, System.Drawing.Point p2)
        {
            double A = p.X - p1.X;
            double B = p.Y - p1.Y;
            double C = p2.X - p1.X;
            double D = p2.Y - p1.Y;

            double dot = A * C + B * D;
            double lenSq = C * C + D * D;
            double param = (lenSq != 0) ? dot / lenSq : -1;

            double xx, yy;

            if (param < 0)
            {
                xx = p1.X;
                yy = p1.Y;
            }
            else if (param > 1)
            {
                xx = p2.X;
                yy = p2.Y;
            }
            else
            {
                xx = p1.X + param * C;
                yy = p1.Y + param * D;
            }

            double dx = p.X - xx;
            double dy = p.Y - yy;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        #endregion

        #region 事件
        private void Pb_image_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 绘制直线
            if (startPoint != System.Drawing.Point.Empty && endPoint != System.Drawing.Point.Empty)
            {
                g.DrawLine(Pens.Red, startPoint, endPoint);

                // 绘制起点和终点的圆圈
                DrawCircle(g, startPoint, CircleRadius, Pens.Blue, Brushes.LightBlue);
                DrawCircle(g, endPoint, CircleRadius, Pens.Blue, Brushes.LightBlue);
            }
            int labelIndex = 1;
            foreach (var rect in rectAndInfoList)
            {
                var rectangles = ImageProcess.ConvertRealToPictureBoxRectangle(pb_image, rect.rectangles);
               
                g.DrawRectangle(Pens.Red, rectangles);
                System.Drawing.Point[] corners = new System.Drawing.Point[]
                {
                    new System.Drawing.Point(rectangles.Left, rectangles.Top), // 左上角
                    new System.Drawing.Point(rectangles.Right, rectangles.Top), // 右上角
                    new System.Drawing.Point(rectangles.Left, rectangles.Bottom), // 左下角
                    new System.Drawing.Point(rectangles.Right, rectangles.Bottom) // 右下角
                };
                foreach (var item in corners)
                {
                   
                    DrawCircle(g, new System.Drawing.Point(item.X, item.Y), CircleRadius, Pens.Blue, Brushes.LightBlue);
                }

                // 指向线的起点在矩形的顶部中心
                System.Drawing.Point centerTopPoint = new System.Drawing.Point(
                    rectangles.Left + rectangles.Width / 2,
                    rectangles.Top
                );

                // 指向线的终点在矩形上方10像素
                System.Drawing.Point labelPoint = new System.Drawing.Point(
                    centerTopPoint.X,
                    centerTopPoint.Y - 10
                );
                // 画垂直的指向线
                g.DrawLine(Pens.Red, centerTopPoint, labelPoint);
                // 画标签
                if (rect.pdinfovc != null) 
                {
                    string labelText = "";
                    if (scientificON)
                    {
                      

                        labelText = $"ROI:{labelIndex++},AOD:{util.GetscientificNotation(rect.pdinfovc.AOD) },IOD:{util.GetscientificNotation(rect.pdinfovc.IOD) }," +
                                   $"\r\nmaxOD:{util.GetscientificNotation(rect.pdinfovc.maxOD)},minOD:{util.GetscientificNotation(rect.pdinfovc.minOD)},Count:{util.GetscientificNotation(rect.pdinfovc.Count)}";
                    }
                    else
                    {
                        labelText = $"ROI:{labelIndex++},AOD:{rect.pdinfovc.AOD},IOD:{rect.pdinfovc.IOD}," +
                                   $"\r\nmaxOD:{rect.pdinfovc.maxOD},minOD:{rect.pdinfovc.minOD},Count:{rect.pdinfovc.Count}"; // 标签编号
                    }
                   
                    Font font = new Font("Arial", 8); // 字体
                    Brush brush = Brushes.Red; // 字体颜色
                    g.DrawString(labelText, font, brush, labelPoint.X - 10, labelPoint.Y - 15);
                }
               

            }
            if (drawRect)
            {
                g.DrawRectangle(Pens.Red, curRectangle);
            }
            else if (isEndRectDragged)
            {
                var rectangles = ImageProcess.ConvertRealToPictureBoxRectangle(pb_image, movingRectangle.rectangles);
                g.DrawRectangle(Pens.Red, rectangles);
            }
            labelIndex = 0;
            foreach (var item in CircleAndInfoList)
            {
                var centerPoint = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, item.center);
                var radiusPoint = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, item.Radius);
                int radius = (int)Math.Sqrt(Math.Pow(centerPoint.X - radiusPoint.X, 2) + Math.Pow(centerPoint.Y - radiusPoint.Y, 2));
                e.Graphics.DrawEllipse(Pens.Red, centerPoint.X - radius, centerPoint.Y - radius, radius * 2, radius * 2);
                DrawCircle(g, new System.Drawing.Point(radiusPoint.X, radiusPoint.Y), CircleRadius, Pens.Blue, Brushes.LightBlue);

                
                // 指向线的终点在矩形上方10像素
                System.Drawing.Point labelPoint = new System.Drawing.Point(
                    centerPoint.X,
                    centerPoint.Y - radius-10
                );
                // 画垂直的指向线
                g.DrawLine(Pens.Red, centerPoint, labelPoint);
                // 画标签
                if (item.pdinfovc != null)
                {

                    string labelText = "";
                    if (scientificON)
                    {


                        labelText = $"ROI:{labelIndex++},AOD:{util.GetscientificNotation(item.pdinfovc.AOD)},IOD:{util.GetscientificNotation(item.pdinfovc.IOD)}," +
                                   $"\r\nmaxOD:{util.GetscientificNotation(item.pdinfovc.maxOD)},minOD:{util.GetscientificNotation(item.pdinfovc.minOD)},Count:{util.GetscientificNotation(item.pdinfovc.Count)}";
                    }
                    else
                    {
                        labelText = $"ROI:{labelIndex++},AOD:{item.pdinfovc.AOD},IOD:{item.pdinfovc.IOD}," +
                                   $"\r\nmaxOD:{item.pdinfovc.maxOD},minOD:{item.pdinfovc.minOD},Count:{item.pdinfovc.Count}"; // 标签编号
                    }
                    Font font = new Font("Arial", 8); // 字体
                    Brush brush = Brushes.Red; // 字体颜色
                    g.DrawString(labelText, font, brush, labelPoint.X - 10, labelPoint.Y - 15);
                }
            }
            if (drawCircle)
            {
                int radius = (int)Math.Sqrt(Math.Pow(curCirCenterPoint.X - curCirRadioPoint.X, 2) + Math.Pow(curCirCenterPoint.Y - curCirRadioPoint.Y, 2));
                e.Graphics.DrawEllipse(Pens.Red, curCirCenterPoint.X - radius, curCirCenterPoint.Y - radius, radius * 2, radius * 2);
                DrawCircle(g, new System.Drawing.Point(curCirRadioPoint.X, curCirRadioPoint.Y), CircleRadius, Pens.Blue, Brushes.LightBlue);
            }
            else if (isCircleFragged) 
            {
                var centerPoint = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, cirMousePosition);
                var radiusPoint = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, cirRadioMousePosition);
                int radius = (int)Math.Sqrt(Math.Pow(centerPoint.X - radiusPoint.X, 2) + Math.Pow(centerPoint.Y - radiusPoint.Y, 2));
                e.Graphics.DrawEllipse(Pens.Red, centerPoint.X - radius, centerPoint.Y - radius, radius * 2, radius * 2);
               
            }
            if (PolygonAndInfoList != null) 
            {
                int isStart = 0;
                System.Drawing.Point point = new System.Drawing.Point();
                foreach (var item1 in PolygonAndInfoList)
                {
                    foreach (var item in item1.points)
                    {
                        if (isStart == 0)
                        {
                            point = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, item);
                        }
                        System.Drawing.Point curpoint = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, item);
                        DrawCircle(g, curpoint, CircleRadius, Pens.Blue, Brushes.LightBlue);
                        g.DrawLine(Pens.Red, curpoint, point);
                        point = curpoint;
                        isStart++;
                    }
                    
                }

                if (drawpolygon)
                {
                    
                    point = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, curPolygonAndInfoList.points[curPolygonAndInfoList.points.Count-1]);
                    g.DrawLine(Pens.Red, curPolygonPoint, point);
                    foreach (var item in curPolygonAndInfoList.points)
                    {
                        if (isStart == 0)
                        {
                            point = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, item);
                        }
                        System.Drawing.Point curpoint = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, item);
                        DrawCircle(g, curpoint, CircleRadius, Pens.Blue, Brushes.LightBlue);
                        g.DrawLine(Pens.Red, curpoint, point);
                        point = curpoint;
                        isStart++;
                    }
                }
                else 
                {
                    if (curPolygonAndInfoList.pdinfovc != null) 
                    {

                        string labelText = "";
                        if (scientificON)
                        {


                            labelText = $"ROI:{labelIndex++},AOD:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.AOD)},IOD:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.IOD)}," +
                                       $"\r\nmaxOD:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.maxOD)},minOD:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.minOD)},Count:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.Count)}";
                        }
                        else
                        {
                            labelText = $"ROI:{labelIndex++},AOD:{curPolygonAndInfoList.pdinfovc.AOD},IOD:{curPolygonAndInfoList.pdinfovc.IOD}," +
                                       $"\r\nmaxOD:{curPolygonAndInfoList.pdinfovc.maxOD},minOD:{curPolygonAndInfoList.pdinfovc.minOD},Count:{curPolygonAndInfoList.pdinfovc.Count}"; // 标签编号
                        }
                        Font font = new Font("Arial", 8); // 字体
                        Brush brush = Brushes.Red; // 字体颜色
                        System.Drawing.Point curpoint = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, curPolygonAndInfoList.points[0]);
                        g.DrawString(labelText, font, brush, curpoint.X - 10, curpoint.Y - 15);
                    }
                }
            }
            
        }

        private void Pl_image_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                // 滚轮向上，放大图片
                ZoomPictureBox(ZoomFactor);
            }
            else if (e.Delta < 0)
            {
                ZoomPictureBox(1 / ZoomFactor);
                // 滚轮向下，缩小图片，但不能缩小到小于pl_image的尺寸
                //if (pl_bg_image.Width > pl_bg_image.Width / ZoomFactor && pl_bg_image.Height > pl_bg_image.Height / ZoomFactor)
                //{
                //    ZoomPictureBox(1 / ZoomFactor);
                //}
            }
        }


        private void pb_image_MouseDown(object sender, MouseEventArgs e)
        {
            System.Drawing.Point readLoction = ImageProcess.GetRealImageCoordinates(pb_image, e.Location);
            if (ImageToolMannage.lineDisON && e.Button == MouseButtons.Left)
            {
                drawLine = true;
                startPoint = e.Location;
            }
            else if (ImageToolMannage.rectON && e.Button == MouseButtons.Left)
            {
                drawRect = true;
                startRectPoint = e.Location;
                curRectangle = new System.Drawing.Rectangle(e.Location, new System.Drawing.Size(0, 0));
            }
            else if (ImageToolMannage.circleON && e.Button == MouseButtons.Left)
            {
                drawCircle = true;
                startCirclePoint = e.Location;
                curCirCenterPoint = e.Location;
                curCirRadioPoint = e.Location;
            }
            else if (ImageToolMannage.linepolygonON && e.Button == MouseButtons.Left)
            {
                drawpolygon = true;
                if (curPolygonAndInfoList.points == null)
                {
                    curPolygonAndInfoList.points = new List<System.Drawing.Point>();
                }
                System.Drawing.Point curPoint = ImageProcess.GetRealImageCoordinates(pb_image, e.Location);
                curPolygonAndInfoList.points.Add(curPoint);
            }
            else if (IsPointInCircle(readLoction, startPoint) && e.Button == MouseButtons.Left)
            {
                isStartCircleDragged = true;
            }
            else if (IsPointInCircle(readLoction, endPoint) && e.Button == MouseButtons.Left)
            {
                isEndCircleDragged = true;
            }
            else if (IsPointInCircle(readLoction, CircleAndInfoList) && e.Button == MouseButtons.Left) 
            {
                int index = 0;
                foreach (var circle in CircleAndInfoList)
                {
                    int radius = (int)Math.Sqrt(Math.Pow(circle.center.X - circle.Radius.X, 2) + Math.Pow(circle.center.Y - circle.Radius.Y, 2));
                    double distance = Math.Sqrt(Math.Pow(readLoction.X - circle.center.X, 2) + Math.Pow(readLoction.Y - circle.center.Y, 2));
                    if (distance <= radius) 
                    {
                        curCirPoint = readLoction;
                        
                        curCircleIndex = index;
                        cirMousePosition =circle.center;
                        cirRadioMousePosition = circle.Radius;
                        isCircleFragged = true;
                        break;
                    }
                    index++;
                }
            }
            else if (IsPointInRectangles(readLoction, rectAndInfoList) && e.Button == MouseButtons.Left)
            {
                int index = 0;
                foreach (var rect in rectAndInfoList)
                {
                    if (rect.rectangles.Contains(readLoction))
                    {
                        isEndRectDragged = true;
                        movingRectangle = rect;
                        curRectanglePoint = readLoction;
                        curRectIndex = index;
                        break;
                    }
                    index++;
                }

            }
            else if (e.Button == MouseButtons.Left && IsImageLargerThanPanel())
            {
                isDragging = true;
                mouseDownPosition = e.Location;
                pictureBoxStartPosition = pl_bg_image.Location;
                pl_bg_image.Cursor = Cursors.Hand;

            }
        }
        private void Pb_image_DoubleClick(object sender, EventArgs e)
        {
            if (ImageToolMannage.linepolygonON && drawpolygon == true)
            { 
               
                if (curPolygonAndInfoList.points != null) 
                {
                    System.Drawing.Point firstPoint = curPolygonAndInfoList.points[0];
                    System.Drawing.Point lastPoint = curPolygonAndInfoList.points[curPolygonAndInfoList.points.Count - 1];
                    firstPoint = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, firstPoint);
                    lastPoint = ImageProcess.ConvertRealToPictureBoxCoordinates(pb_image, lastPoint);
                    double deltaX = lastPoint.X - firstPoint.X;
                    double deltaY = lastPoint.Y - firstPoint.Y;
                    var value = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                    if (value <= 5) 
                    {
                        lastPoint = firstPoint;
                        drawpolygon = false;
                        ImageToolMannage.linepolygonON = false;
                        pb_image.Invalidate();

                        // 计算光子量

                        float _max = paletteForm.ColorMax;
                        float _min = paletteForm.ColorMin;
                        List<Point_VC> curVclist = new List<Point_VC>();
                    
                        foreach (var item in curPolygonAndInfoList.points)
                        {
                            Point_VC pvc = new Point_VC(item.X,item.Y);
                            curVclist.Add(pvc);
                           
                        }
                        unsafe
                        {
                            fixed (byte* pseu_16_byte_src = pseu_16_byte)
                            {
                                curpdinfovc = pbpvc.get_pseudo_info_polygon_vc(pseu_16_byte_src, 16, (ushort)pseu_L16_image.Width, (ushort)pseu_L16_image.Height, _max, _min, curVclist);

                            }
                        }
                        curPolygonAndInfoList.pdinfovc = curpdinfovc;

                        PolygonAndInfoList.Add(curPolygonAndInfoList);
                    }

                   
                }
               
               
            }
        }
        private void pb_image_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point readLoction =  ImageProcess.GetRealImageCoordinates(pb_image, e.Location);
            if (ImageToolMannage.lineDisON && drawLine)
            {
                endPoint = e.Location; // 更新终点位置
                pb_image.Invalidate(); // 触发重绘
            }
            else if (ImageToolMannage.rectON && drawRect)
            {
                curRectangle.Width = Math.Abs(e.X - startRectPoint.X);
                curRectangle.Height = Math.Abs(e.Y - startRectPoint.Y);

                // 处理矩形位置，以确保其起点是左上角
                curRectangle.X = Math.Min(startRectPoint.X, e.X);
                curRectangle.Y = Math.Min(startRectPoint.Y, e.Y);
                pb_image.Invalidate();
            }
            else if (ImageToolMannage.circleON && drawCircle)
            {
                curCirRadioPoint = e.Location;
                pb_image.Invalidate();
            }
            else if (ImageToolMannage.linepolygonON && drawpolygon)
            {
                curPolygonPoint = e.Location;
                pb_image.Invalidate();
            }
            else if (IsPointInCircle(readLoction, startPoint) || IsPointInCircle(readLoction, endPoint))
            {
                pb_image.Cursor = Cursors.Hand;
            }
            else if (IsPointInRectangles(readLoction, rectAndInfoList))
            {
                pb_image.Cursor = Cursors.Hand;

            }
            else if (IsPointInCircle(readLoction, CircleAndInfoList))
            {
                pb_image.Cursor = Cursors.Hand;
            }
            else if (isStartCircleDragged)
            {

                // 拖动起始点
                startPoint = e.Location;
                pb_image.Invalidate();
            }
            else if (isEndCircleDragged)
            {
                // 拖动终点
                endPoint = e.Location;
                pb_image.Invalidate();
            }

            else if (isDragging)
            {
                int deltaX = e.X - mouseDownPosition.X;
                int deltaY = e.Y - mouseDownPosition.Y;

                // 仅当鼠标移动时调整位置，减少频繁重绘
                if (Math.Abs(deltaX) > 1 || Math.Abs(deltaY) > 1)
                {
                    pl_bg_image.Left = pictureBoxStartPosition.X + deltaX;
                    pl_bg_image.Top = pictureBoxStartPosition.Y + deltaY;

                    // 防止 pl_bg_image 拖动超出 pl_image 的范围
                    if (pl_bg_image.Left > 0) pl_bg_image.Left = 0;
                    if (pl_bg_image.Top > 0) pl_bg_image.Top = 0;
                    if (pl_bg_image.Right < pl_image.ClientSize.Width)
                        pl_bg_image.Left = pl_image.ClientSize.Width - pl_bg_image.Width;
                    if (pl_bg_image.Bottom < pl_image.ClientSize.Height)
                        pl_bg_image.Top = pl_image.ClientSize.Height - pl_bg_image.Height;
                }


            }
            else if (isEndRectDragged)
            {
                if (rectAndInfoList.Count > 0)
                {
                    int _xvalue = readLoction.X - curRectanglePoint.X;
                    int _yvalue = readLoction.Y - curRectanglePoint.Y;
                    movingRectangle.rectangles.X += _xvalue; movingRectangle.rectangles.Y += _yvalue;
                    curRectanglePoint = readLoction;
                    pb_image.Invalidate();

                    rectAndInfoList[curRectIndex] = movingRectangle;
                }

            }
            else if (isCircleFragged) 
            {

                if (CircleAndInfoList.Count > 0) 
                {
                    int _xvalue = readLoction.X - curCirPoint.X;
                    int _yvalue = readLoction.Y - curCirPoint.Y;
                  
                    cirMousePosition.X += _xvalue; cirMousePosition.Y += _yvalue;
                    cirRadioMousePosition.X += _xvalue;
                    cirRadioMousePosition.Y += _yvalue;
                    curCirPoint = readLoction;
                   
                    CirceAndInfo caif = new CirceAndInfo();
                    caif.center = cirMousePosition;
                    caif.Radius = cirRadioMousePosition;
                    caif.pdinfovc = CircleAndInfoList[curCircleIndex].pdinfovc;
                    CircleAndInfoList[curCircleIndex] = caif;
                    pb_image.Invalidate();
                }
               
            }
            else if (IsPointInRectangles(e.Location, rectAndInfoList))
            {
                pb_image.Cursor = Cursors.Hand;
            }
            else
            {
                pb_image.Cursor = Cursors.Default;
            }
        }
        private void pb_image_MouseUp(object sender, MouseEventArgs e)
        {
            System.Drawing.Point readLoction = ImageProcess.GetRealImageCoordinates(pb_image, e.Location);
            isDragging = false;
            if (e.Button == MouseButtons.Left)
            {
                drawLine = false;
                if (drawRect) 
                {
                    RectAndInfo rai = new RectAndInfo();
                    rai.rectangles = ImageProcess.GetRealImageRectangle(pb_image,curRectangle);
                    if (ImageToolMannage.RoiIndex == 0) 
                    {
                        ImageToolMannage.Roi_w = rai.rectangles.Width;
                        ImageToolMannage.Roi_h = rai.rectangles.Height;

                        paletteForm.ROI_W = rai.rectangles.Width;
                        paletteForm.ROI_H = rai.rectangles.Height;
                    }
                    else
                    {
                        rai.rectangles.Width = ImageToolMannage.Roi_w;
                        rai.rectangles.Height = ImageToolMannage.Roi_h;
                        paletteForm.ROI_W = ImageToolMannage.Roi_w;
                        paletteForm.ROI_H = ImageToolMannage.Roi_h;
                    }
                    ImageToolMannage.RoiIndex++;
                    System.Drawing.Point curPoint = ImageProcess.GetRealImageCoordinates(pb_image,new System.Drawing.Point( curRectangle.X,curRectangle.Y));
                    // 计算光子数并展示出来
                    float _max = paletteForm.ColorMax;
                    float _min = paletteForm.ColorMin;
                   
                    unsafe
                    {
                        fixed (byte* pseu_16_byte_src = pseu_16_byte)
                        {
                            curpdinfovc = pbpvc.get_pseudo_info_rect_vc(pseu_16_byte_src, 16, (ushort)pseu_L16_image.Width, (ushort)pseu_L16_image.Height, _max, _min, curPoint.X, curPoint.Y, curRectangle.Width, curRectangle.Height);

                        }
                    }
                    rai.pdinfovc = curpdinfovc;
                    rectAndInfoList.Add(rai);
                    drawRect = false;
                   


                }
                if (drawCircle) 
                {
                    CirceAndInfo circeAndInfo = new CirceAndInfo();
                    
                    circeAndInfo.Radius = ImageProcess.GetRealImageCoordinates(pb_image, curCirRadioPoint); 
                    circeAndInfo.center = ImageProcess.GetRealImageCoordinates(pb_image, curCirCenterPoint);
                    if (ImageToolMannage.CircleIndex == 0) 
                    {
                        paletteForm.CIRCLE_R = (int)Math.Sqrt(Math.Pow(circeAndInfo.center.X - circeAndInfo.Radius.X, 2) + Math.Pow(circeAndInfo.center.Y - circeAndInfo.Radius.Y, 2));
                        ImageToolMannage.Roi_r = paletteForm.CIRCLE_R;
                    }
                    else
                    {
                        paletteForm.CIRCLE_R = ImageToolMannage.Roi_r;
                        double angleInRadians = 90 * Math.PI / 180; // Convert degrees to radians
                        double x = circeAndInfo.center.X + paletteForm.CIRCLE_R * Math.Cos(angleInRadians);
                        double y = circeAndInfo.center.Y + paletteForm.CIRCLE_R * Math.Sin(angleInRadians);

                        circeAndInfo.Radius = new System.Drawing.Point((int)x, (int)y);
                    }
                    ImageToolMannage.CircleIndex++;
                   
                    float _max = paletteForm.ColorMax;
                    float _min = paletteForm.ColorMin;
                    int radius = (int)Math.Sqrt(Math.Pow(circeAndInfo.center.X - circeAndInfo.Radius.X, 2) + Math.Pow(circeAndInfo.center.Y - circeAndInfo.Radius.Y, 2));
                    unsafe
                    {
                        fixed (byte* pseu_16_byte_src = pseu_16_byte)
                        {
                            curpdinfovc = pbpvc.get_pseudo_info_circle_vc(pseu_16_byte_src, 16, (ushort)pseu_L16_image.Width, (ushort)pseu_L16_image.Height, _max, _min, circeAndInfo.center.X, circeAndInfo.center.Y, radius);

                        }
                    }
                    circeAndInfo.pdinfovc = curpdinfovc;

                    CircleAndInfoList.Add(circeAndInfo);
                    drawCircle = false;
                }
                ImageToolMannage.rectON = false;
                ImageToolMannage.circleON = false;
                if (isEndRectDragged) 
                {
                    RectAndInfo rai = new RectAndInfo();
                    rai.rectangles = movingRectangle.rectangles;
                    
                    rai.rectangles.Width = paletteForm.ROI_W;
                    rai.rectangles.Height = paletteForm.ROI_H;
                    
                    
                    // 计算光子数并展示出来
                    float _max = paletteForm.ColorMax;
                    float _min = paletteForm.ColorMin;

                    unsafe
                    {
                        fixed (byte* pseu_16_byte_src = pseu_16_byte)
                        {
                            curpdinfovc = pbpvc.get_pseudo_info_rect_vc(pseu_16_byte_src, 16, (ushort)pseu_L16_image.Width, (ushort)pseu_L16_image.Height, _max, _min, rai.rectangles.X, rai.rectangles.Y, curRectangle.Width, curRectangle.Height);

                        }
                    }
                    rai.pdinfovc = curpdinfovc;
                    rectAndInfoList[curRectIndex] = rai;
                    isEndRectDragged = false;
                }
                if (isCircleFragged) 
                {
                    CirceAndInfo circeAndInfo = new CirceAndInfo();

                    circeAndInfo.Radius = cirRadioMousePosition;
                    circeAndInfo.center = cirMousePosition;

                    float _max = paletteForm.ColorMax;
                    float _min = paletteForm.ColorMin;
                    int radius = (int)Math.Sqrt(Math.Pow(circeAndInfo.center.X - circeAndInfo.Radius.X, 2) + Math.Pow(circeAndInfo.center.Y - circeAndInfo.Radius.Y, 2));
                    unsafe
                    {
                        fixed (byte* pseu_16_byte_src = pseu_16_byte)
                        {
                            curpdinfovc = pbpvc.get_pseudo_info_circle_vc(pseu_16_byte_src, 16, (ushort)pseu_L16_image.Width, (ushort)pseu_L16_image.Height, _max, _min, circeAndInfo.center.X, circeAndInfo.center.Y, radius);

                        }
                    }
                    circeAndInfo.pdinfovc = curpdinfovc;

                    CircleAndInfoList[curCircleIndex] = circeAndInfo;
                    isCircleFragged = false;
                    curCircleIndex = -1;
                }
                pb_image.Invalidate();
                if (isStartCircleDragged || isEndCircleDragged || ImageToolMannage.lineDisON)
                {
                    System.Drawing.Point curEndPoint = ImageProcess.GetRealImageCoordinates(pb_image, endPoint);
                    System.Drawing.Point curStartPoint = ImageProcess.GetRealImageCoordinates(pb_image, startPoint);
                    // 计算距离
                    double deltaX = curEndPoint.X - curStartPoint.X;
                    double deltaY = curEndPoint.Y - curStartPoint.Y;
                    var value = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                    paletteForm.SetactMM(value.ToString() + " mm");


                }
                isStartCircleDragged = false;
                isEndCircleDragged = false;
                ImageToolMannage.lineDisON = false;


            }
            else if (e.Button == MouseButtons.Right) 
            {
                if (IsPointOnLine(readLoction))
                {
                    startPoint = new System.Drawing.Point(-10, 0);
                    endPoint = new System.Drawing.Point(-10, 0);
                    pb_image.Invalidate();
                    paletteForm.SetactMM("0");
                }
                else if (IsPointInRectangles(readLoction, rectAndInfoList))
                {
                    int _index = -1;
                    foreach (var rect in rectAndInfoList)
                    {
                        _index++;
                        if (rect.rectangles.Contains(readLoction))
                            break;
                    }
                    if (_index >= 0)
                    {
                        rectAndInfoList.RemoveAt(_index);
                        ImageToolMannage.RoiIndex--;
                        pb_image.Invalidate();
                    }
                }
                else if (IsPointInCircle(readLoction, CircleAndInfoList))
                {
                    int _index = -1;
                    foreach (var rect in CircleAndInfoList)
                    {
                        _index++;
                        if (rect.center.X == readLoction.X && rect.center.Y == readLoction.Y)
                            break;
                    }
                    if (_index >= 0)
                    {
                        CircleAndInfoList.RemoveAt(_index);
                        ImageToolMannage.CircleIndex--;
                        pb_image.Invalidate();
                    }
                }
                else if (IsPointInPolygon(readLoction, curPolygonAndInfoList)) 
                {
                    PolygonAndInfoList.Clear();
                    curPolygonAndInfoList.points.Clear();
                    curPolygonAndInfoList.pdinfovc = null;
                    pb_image.Invalidate();
                }
                else if (ImageToolMannage.linewandON)
                {
                    //RectAndInfo rai = new RectAndInfo();
                    //rai.rectangles = ImageProcess.GetRealImageRectangle(pb_image, );

                    //System.Drawing.Point curPoint = ImageProcess.GetRealImageCoordinates(pb_image, new System.Drawing.Point(curRectangle.X, curRectangle.Y));
                    //// 计算光子数并展示出来
                    //float _max = paletteForm.ColorMax;
                    //float _min = paletteForm.ColorMin;

                    //unsafe
                    //{
                    //    fixed (byte* pseu_16_byte_src = pseu_16_byte)
                    //    {
                    //        curpdinfovc = pbpvc.get_pseudo_info_wand_vc(pseu_16_byte_src, 16, (ushort)pseu_L16_image.Width, (ushort)pseu_L16_image.Height, _max, _min, curPoint.X, curPoint.Y, curRectangle.Width, curRectangle.Height);

                    //    }
                    //}
                    //rai.pdinfovc = curpdinfovc;
                    //rectAndInfoList.Add(rai);
                    //drawRect = false;
                    //pb_image.Invalidate();
                }
            }
          


        }
        private void hpb_zoom_in_Click(object sender, System.EventArgs e)
        {
            ZoomPictureBox(ZoomFactor);
        }

        private void hpb_zoom_out_Click(object sender, System.EventArgs e)
        {
            ZoomPictureBox(1 / ZoomFactor);
        }
        private void hpb_auto_Click(object sender, EventArgs e)
        {
            if (pb_image.Image == null) return;


            // 设置PictureBox的宽度和高度
            pb_image.Location = new System.Drawing.Point(pl_image.Location.X,pl_image.Location.Y);
            pb_image.Width = pl_image.Width;
            pb_image.Height = pl_image.Height;

           
        }
        private void mcb_mode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
           
            lb_modename.Text = mcb_mode.Text;
            float _max = paletteForm.ColorMax;
            float _min = paletteForm.ColorMin;
            if (scientificON) 
            {
                lb_wh.Text = "Radiance (p/sec/cm²/sr)\n color scale\n min=" + util.GetscientificNotation(_min) + "\n max=" + util.GetscientificNotation(_max);
            }
            else
            {
                lb_wh.Text = "Radiance (p/sec/cm²/sr)\n color scale\n min=" + _min.ToString() + "\n max=" + _max.ToString();
            }
            
            if (colorbar_rgb24_image != null) 
            {
                pb_coloarbar_image.Image = util.ConvertRgb24ImageToBitmap(colorbar_rgb24_image);
            }
            if (mcb_mode.SelectedIndex == 0) 
            {
              
                pb_image.Image = util.ConvertRgb24ImageToBitmap(pseu_and_mark__rgb24_image);
                
            }
            else if (mcb_mode.SelectedIndex == 1)
            {
                
                pb_image.Image =util.ConvertL16ToBitmap(mark_L16_image);

             

            }
            else if (mcb_mode.SelectedIndex == 2) 
            {
                pb_image.Image = util.ConvertRgb24ImageToBitmap(pseu_rgb24_image);
                
            }
            
            curBitmap = (Bitmap)pb_image.Image;
        }

        



        private void ImagePanel_Enter(object sender, EventArgs e)
        {
            this.BringToFront();
            RefreshUI();
           

        }
        private void ImagePanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImageToolMannage.imageDataPath.Remove(path);
            paletteForm.Invalidate();
            paletteForm.Dispose();
            paletteForm = null;
        }

        private void ImagePanel_Resize(object sender, EventArgs e)
        {
            if (pb_image.Image == null) return;


            // 设置PictureBox的宽度和高度
            pb_image.Location = new System.Drawing.Point(pl_image.Location.X, pl_image.Location.Y);
            pb_image.Width = pl_image.Width;
            pb_image.Height = pl_image.Height;

            CenterPictureBox();
        }

        private void hhpb_save_Click(object sender, EventArgs e)
        {

            // 创建一个位图，其大小与panel相同
            Bitmap bitmap = new Bitmap(tlpanl_image_bar.Width, tlpanl_image_bar.Height);

            // 将panel的视图渲染到位图上
            tlpanl_image_bar.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, tlpanl_image_bar.Width, tlpanl_image_bar.Height));

            // 弹出保存文件对话框
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "保存Panel图像";
                saveFileDialog.Filter = "PNG 图片|*.png|JPEG 图片|*.jpg|BMP 图片|*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 根据文件扩展名选择格式
                    System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Bmp;
                    switch (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower())
                    {
                        case ".jpg":
                            format = System.Drawing.Imaging.ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = System.Drawing.Imaging.ImageFormat.Bmp;
                            break;
                    }
                    bitmap.Save(saveFileDialog.FileName, format); // 保存图像到文件

#if true //保存报告
                    if (scientificON) 
                    {
                        var records = new List<DataRecordString>();

                        // 目前只保存矩形下的光子数
                        int index = 0;
                        foreach (var item in rectAndInfoList)
                        {
                            index++;

                            DataRecordString dr = new DataRecordString();
                            dr.index = index;

                            dr.IOD =util.GetscientificNotation( item.pdinfovc.IOD);
                            dr.AOD = util.GetscientificNotation(item.pdinfovc.AOD); 
                            dr.max = util.GetscientificNotation(item.pdinfovc.maxOD); 
                            dr.min = util.GetscientificNotation(item.pdinfovc.minOD); 
                            dr.Count = util.GetscientificNotation(item.pdinfovc.Count); 
                            records.Add(dr);
                        }
                        foreach (var item in CircleAndInfoList)
                        {
                            index++;

                            DataRecordString dr = new DataRecordString();
                            dr.index = index;

                            dr.IOD = util.GetscientificNotation(item.pdinfovc.IOD);
                            dr.AOD = util.GetscientificNotation(item.pdinfovc.AOD);
                            dr.max = util.GetscientificNotation(item.pdinfovc.maxOD);
                            dr.min = util.GetscientificNotation(item.pdinfovc.minOD);
                            dr.Count = util.GetscientificNotation(item.pdinfovc.Count); 
                            records.Add(dr);
                        }
                        foreach (var item in PolygonAndInfoList)
                        {
                            index++;

                            DataRecordString dr = new DataRecordString();
                            dr.index = index;

                            dr.IOD = util.GetscientificNotation(item.pdinfovc.IOD);
                            dr.AOD = util.GetscientificNotation(item.pdinfovc.AOD);
                            dr.max = util.GetscientificNotation(item.pdinfovc.maxOD);
                            dr.min = util.GetscientificNotation(item.pdinfovc.minOD);
                            dr.Count = util.GetscientificNotation(item.pdinfovc.Count);
                            records.Add(dr);
                        }
                        if (records.Count > 0)
                        {
                            string directoryPath = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                            string path = directoryPath + "\\" + fileNameWithoutExtension + ".xlsx";
                            if (File.Exists(path))
                            {
                                try
                                {
                                    File.Delete(path);
                                }
                                catch (Exception)
                                {

                                    MessageBox.Show("文件被占用，无法删除!!!");
                                    return;
                                }
                            }
                            MiniExcel.SaveAs(path, records);
                        }
                    }
                    else 
                    {
                        var records = new List<DataRecord>();

                        // 目前只保存矩形下的光子数
                        int index = 0;
                        foreach (var item in rectAndInfoList)
                        {
                            index++;

                            DataRecord dr = new DataRecord();
                            dr.index = index;
                            dr.IOD = item.pdinfovc.IOD;
                            dr.AOD = item.pdinfovc.AOD;
                            dr.max = item.pdinfovc.maxOD;
                            dr.min = item.pdinfovc.minOD;
                            dr.Count = item.pdinfovc.Count;
                            records.Add(dr);
                        }
                        foreach (var item in CircleAndInfoList)
                        {
                            index++;

                            DataRecord dr = new DataRecord();
                            dr.index = index;
                            dr.IOD = item.pdinfovc.IOD;
                            dr.AOD = item.pdinfovc.AOD;
                            dr.max = item.pdinfovc.maxOD;
                            dr.min = item.pdinfovc.minOD;
                            dr.Count = item.pdinfovc.Count;
                            records.Add(dr);
                        }
                        foreach (var item in PolygonAndInfoList)
                        {
                            index++;

                            DataRecord dr = new DataRecord();
                            dr.index = index;
                            dr.IOD = item.pdinfovc.IOD;
                            dr.AOD = item.pdinfovc.AOD;
                            dr.max = item.pdinfovc.maxOD;
                            dr.min = item.pdinfovc.minOD;
                            dr.Count = item.pdinfovc.Count;
                            records.Add(dr);
                        }
                        if (records.Count > 0)
                        {
                            string directoryPath = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                            string path = directoryPath + "\\" + fileNameWithoutExtension + ".xlsx";
                            if (File.Exists(path))
                            {
                                try
                                {
                                    File.Delete(path);
                                }
                                catch (Exception)
                                {

                                    MessageBox.Show("文件被占用，无法删除!!!");
                                    return;
                                }
                            }
                            MiniExcel.SaveAs(path, records);
                        }
                    }
                    
#endif
                }
            }

            // 清理资源
            bitmap.Dispose();


        }
        private void ImagePanel_MouseDown(object sender, MouseEventArgs e)
        {
            this.BringToFront();
            RefreshUI();
        }
#endregion

        #region 对外接口
        public Bitmap GetPseuImage
        {
            get 
            {
                return util.ConvertRgb24ImageToBitmap(pseu_rgb24_image);
            }
        }

        

        private void cb_scientific_CheckedChanged(object sender)
        {
            scientificON = cb_scientific.Checked;
            ThisRefresh();
        }

        public Bitmap GetMarkImage
        {
            get
            {
                return util.ConvertL16ToBitmap(mark_L16_image);
            }
        }
        public Bitmap GetpseuAndMark
        {
            get
            {
                return util.ConvertRgb24ImageToBitmap(pseu_and_mark__rgb24_image);
            }
        }
        public Bitmap pictureBox 
        {
            get { return (Bitmap)pb_image.Image; }
        } 
        public void initPicturebox() 
        {
            pl_bg_image.Controls.Clear();
            pl_bg_image.Controls.Add(pb_image);
        }

        public Image<Rgb24> GetImage() 
        {
            mcb_mode.SelectedIndex = 0;
            pb_image.SaveToImage("2.bmp",System.Drawing.Imaging.ImageFormat.Bmp);
            return pseu_and_mark__rgb24_image;
        }
        #endregion
    }
}
