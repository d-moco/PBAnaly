using MiniExcelLibs;
using PBAnaly.UI;
using PBBiologyVC;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PBAnaly.Module
{
   
    /// <summary>
    /// 生物分析 管理类
    /// </summary>
    public class BioanalysisMannage
    {
        #region 结构体
        public struct AlgAttribute
        {
            public int brightness;
            public int brightnessMax;
            public int brightnessMin;

            public int opacity;
            public int opacityMax;
            public int opacityMin;

            public int colorValue;
            public int colorMinValue;
            public int colorMin;
            public int colorMax;

            public bool scientificON;

            public int colorIndex;
        }

        public struct RectAttribute 
        {
            public System.Drawing.Rectangle rect;
            public Pseudo_infoVC pdinfovc;
        }
        private struct CirceAndInfo
        {
            public System.Drawing.Point center;
            public System.Drawing.Point Radius { get; set; }
            public Pseudo_infoVC pdinfovc;
        }
        private struct PolygonAndInfo
        {
            public List<System.Drawing.Point> points;
            public Pseudo_infoVC pdinfovc;
        }
        #endregion
        #region 变量
        private Dictionary<string, BioanalysisMannage> bioanalysisMannages;
        private string path;
        private string mark_path;
        private string tif_marker_path;
        private string tif_org_path;
        private Image<L16> image_mark_L16 = null;
        private byte[] image_mark_byte = null;
        private Image<Rgb24> image_mark_rgb24 = null;
        private Image<Rgb24> image_mark_and_org_rgb24 = null;
        private Image<L16> image_org_L16 = null;
        private byte[] image_org_byte = null;
        private Image<Rgb24> image_org_rgb24 = null;
        private Image<Rgb24> colorbar_rgb24_image = null;
        private int colorbarWW = 200, colorh_onecolor = 10;
        private int colorbarW = 600, colorbarH = 2600;

        private ReaLTaiizor.Controls.Panel pl_right;
        private BioanalyImagePanel imagePanel = null;
        private BioanayImagePaletteForm imagePaletteForm = null;
        PBBiologyVC.PBImageProcessVC pbpvc = new PBBiologyVC.PBImageProcessVC();

        private Thread algThread;
        private bool isalgRun = false;
        private bool isUpdateAlg = false;
        private Queue<AlgAttribute> queueAlgAttribute = new Queue<AlgAttribute>();

        #region 参数
        AlgAttribute algAttribute = new AlgAttribute();

        #region imagepanel 操作的参数
        private bool isDragging = false;
        private System.Drawing.Point mouseDownPosition;
        private System.Drawing.Point pictureBoxStartPosition;

        private const int CircleRadius = 5;
        private bool lineOn =false;
        private bool drawLine = false;

        private bool CircleOn = false;
        private bool rectOn = false;
        private bool linepolygonON = false;
        private bool isRecDragging = false;
        private List<RectAttribute> rectangles = new List<RectAttribute>(); // 存储所有绘制完成的矩形
        private System.Drawing.Rectangle? currentRectangle = null; // 当前正在绘制的矩形
        private System.Drawing.Point leftTopPoint; // 矩形左上角的起始点
        private bool drawRect = false; // 是否正在绘制
        private System.Drawing.Point recDragStart;
        private System.Drawing.Rectangle recDragRect;
        private RectAttribute rectOriginalRect;
        private int rectDragStartIndex = -1;

        private bool drawCircle = false;//是否绘制圆
        private bool isCirDragging = false;
        private List<CirceAndInfo> CircleAndInfoList = new List<CirceAndInfo>();
        private System.Drawing.Point cirDragStart;
        private System.Drawing.Point circleCenter;
        private System.Drawing.Point circleRadio;
        private int cirDragStartIndex = -1;
        private CirceAndInfo cireOriginalCire;

        private bool drawpolygon = false;
        private List<PolygonAndInfo> PolygonAndInfoList = new List<PolygonAndInfo>();
        private PolygonAndInfo curPolygonAndInfoList = new PolygonAndInfo();
        private System.Drawing.Point curStartPolygonPoint = new System.Drawing.Point(0, 0);
        private System.Drawing.Point curPolygonPoint = new System.Drawing.Point(0, 0);

        private System.Drawing.Point startPoint = new System.Drawing.Point(-10, 0);
        private System.Drawing.Point endPoint = new System.Drawing.Point(-10, 0);


        private bool isStartCircleDragged, isEndCircleDragged;

        private enum Corner { None,TopLeft,TopRight,BottomLeft,BottomRight,drawMouse}

        private Corner rectActiveCorner = Corner.None;
        #endregion
        #endregion

        #region 构造函数

        public int Brightness 
        {
            get { return algAttribute.brightness; }
            set 
            {
                if (algAttribute.brightness != value) 
                {
                    bool fix = false;
                    algAttribute.brightness = value;
                    if (imagePaletteForm.dtb_brightness.Value != algAttribute.brightness) 
                    {
                        imagePaletteForm.dtb_brightness.Value = algAttribute.brightness;
                        fix = true;
                    }

                    if (imagePaletteForm.nud_brightness.Value != algAttribute.brightness) 
                    {
                        imagePaletteForm.nud_brightness.Value = algAttribute.brightness;
                        fix = true;
                    }

                    if (fix && isUpdateAlg) 
                    {
                        queueAlgAttribute.Enqueue(algAttribute);
                    }
                }
                
            }
        }
        public int Opacity
        {
            get { return algAttribute.opacity; }
            set
            {
                if (algAttribute.brightness != value) 
                {
                    bool fix = false;
                    algAttribute.opacity = value;
                    if (imagePaletteForm.dtb_opacity.Value != algAttribute.opacity) 
                    {
                        imagePaletteForm.dtb_opacity.Value = algAttribute.opacity;
                        fix = true;
                    }

                    if (imagePaletteForm.nud_opacity.Value != algAttribute.opacity) 
                    {

                        imagePaletteForm.nud_opacity.Value = algAttribute.opacity;
                        fix = true;
                    }
                        
                    if (fix && isUpdateAlg)
                    {
                        queueAlgAttribute.Enqueue(algAttribute);
                    }
                }
                    
            }
        }
        public int ColorMax
        {
            get { return algAttribute.colorValue; }
            set
            {
                if (algAttribute.colorValue != value) 
                {
                    var fix = false;
                    algAttribute.colorValue = value;

                    if(algAttribute.colorValue != imagePaletteForm.dtb_colorMax.Value)
                        imagePaletteForm.dtb_colorMax.Value = algAttribute.colorValue;
                    if (algAttribute.colorValue != imagePaletteForm.nud_colorMax.Value)
                        imagePaletteForm.nud_colorMax.Value = algAttribute.colorValue;
                    if (algAttribute.colorValue > imagePaletteForm.nud_colorMin.Value)
                    {
                        
                        imagePaletteForm.nud_colorMin.Maximum = algAttribute.colorValue - 1;
                        imagePaletteForm.dtb_colorMin.Maximum = algAttribute.colorValue - 1;
                        fix = true;
                    }
                    else if(algAttribute.colorValue < imagePaletteForm.nud_colorMin.Value)
                    {
                        imagePaletteForm.nud_colorMin.Maximum = algAttribute.colorValue - 1;
                        imagePaletteForm.dtb_colorMin.Maximum = algAttribute.colorValue - 1;
                        imagePaletteForm.nud_colorMin.Value = algAttribute.colorValue - 1;
                        fix |= true;
                    }

                    if (fix && isUpdateAlg)
                    {
                        queueAlgAttribute.Enqueue(algAttribute);
                    }
                }
                
            }
        }
        public int ColorMin
        {
            get { return algAttribute.colorMinValue; }
            set
            {
                if (algAttribute.colorMinValue != value) 
                {
                    bool fix = false;
                    algAttribute.colorMinValue = value;
                    if (algAttribute.colorMinValue != imagePaletteForm.dtb_colorMin.Value) 
                    {
                        imagePaletteForm.dtb_colorMin.Value = algAttribute.colorMinValue;
                        fix = true;
                    }

                    if (algAttribute.colorMinValue != imagePaletteForm.nud_colorMin.Value) 
                    {
                        imagePaletteForm.nud_colorMin.Value = algAttribute.colorMinValue;
                        fix = true;
                    }
                        

                    if (fix && isUpdateAlg)
                    {
                        queueAlgAttribute.Enqueue(algAttribute);
                    }
                }
               
                
            }
        }

        public int ColorIndex 
        {
            get { return algAttribute.colorIndex; }
            set 
            {
                if (algAttribute.colorIndex != value) 
                {
                    bool fix = true;
                    algAttribute.colorIndex = value;

                    if (fix && isUpdateAlg)
                    {
                        queueAlgAttribute.Enqueue(algAttribute);
                    }
                }
            }
        }

        public bool ScientificON 
        {
            get { return algAttribute.scientificON; }
            set 
            {
                if (algAttribute.scientificON != value)
                {
                    bool fix = true;
                    algAttribute.scientificON = value;

                    if (fix && isUpdateAlg)
                    {
                        queueAlgAttribute.Enqueue(algAttribute);
                    }
                }
            }
        }
        #endregion
        #endregion

        public BioanalysisMannage(string _path, ReaLTaiizor.Controls.Panel _pl_right, Dictionary<string, BioanalysisMannage> bioanalysisMannages) 
        {
            isUpdateAlg = false;
            this.pl_right = _pl_right;
            imagePanel = new BioanalyImagePanel();
            imagePanel.TopLevel = false;
            imagePanel.Show();
            imagePanel.BringToFront();

            imagePaletteForm = new BioanayImagePaletteForm();
            imagePaletteForm.TopLevel = false;
            imagePaletteForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_right.Controls.Add(imagePaletteForm);
            imagePaletteForm.BringToFront();
            imagePaletteForm.Show();

            this.path = _path;

            ReadTif();// 读tif档

            algAttribute.brightness = 127;
            algAttribute. brightnessMax = 254;
            algAttribute. brightnessMin = 0;

            algAttribute. opacity = 100;
            algAttribute. opacityMax = 100;
            algAttribute. opacityMin = 0;

            algAttribute. colorValue = 65534;
            algAttribute. colorMinValue = 5999;
            algAttribute. colorMin = 2;
            algAttribute. colorMax = 65535;

            algAttribute.scientificON = false;
            algAttribute.colorIndex = 0;

            // 初始化控件
            Init();

            // 初始化执行算法
            ImageAlg(algAttribute);
            isalgRun = true;
            algThread = new Thread(new ThreadStart(AlgRun));
            algThread.IsBackground = true;
            algThread.Start();

            isUpdateAlg = true;// 开始可以更新算法


            bioanalysisMannages[_path] = this;
            this.bioanalysisMannages = bioanalysisMannages;
        }

        #region 方法
        private void Init()
        {
            imagePanel.cbb_mode.SelectedIndexChanged += Cbb_mode_SelectedIndexChanged; ;
            imagePanel.cbb_mode.SelectedIndex = 0;

            imagePaletteForm.dtb_brightness.Maximum = algAttribute.brightnessMax;
            imagePaletteForm.dtb_brightness.Minimum = algAttribute.brightnessMin;
            imagePaletteForm.dtb_brightness.Value = algAttribute.brightness;
            imagePaletteForm.nud_brightness.Minimum = algAttribute.brightnessMin;
            imagePaletteForm.nud_brightness.Maximum = algAttribute.brightnessMax;
            imagePaletteForm.nud_brightness.Value = algAttribute.brightness;
            


            imagePaletteForm.dtb_opacity.Maximum = algAttribute.opacityMax;
            imagePaletteForm.dtb_opacity.Minimum = algAttribute.opacityMin;
            imagePaletteForm.dtb_opacity.Value = algAttribute.opacity;
            imagePaletteForm.nud_opacity.Maximum = algAttribute.opacityMax;
            imagePaletteForm.nud_opacity.Minimum = algAttribute.opacityMin;
            imagePaletteForm.nud_opacity.Value = algAttribute.opacity;

            imagePaletteForm.dtb_colorMax.Maximum = algAttribute.colorMax;
            imagePaletteForm.dtb_colorMax.Minimum = algAttribute.colorMin;
            imagePaletteForm.dtb_colorMax.Value = algAttribute.colorValue;
            imagePaletteForm.nud_colorMax.Maximum = algAttribute.colorMax;
            imagePaletteForm.nud_colorMax.Minimum= algAttribute.colorMin;
            imagePaletteForm.nud_colorMax.Value= algAttribute.colorValue;

            imagePaletteForm.dtb_colorMin.Maximum = algAttribute.colorValue-1;
            imagePaletteForm.dtb_colorMin.Value = algAttribute.colorMinValue;
            imagePaletteForm.dtb_colorMin.Minimum = 0;
            imagePaletteForm.nud_colorMin.Maximum = algAttribute.colorValue-1;
            imagePaletteForm.nud_colorMin.Minimum = algAttribute.colorMin;
            imagePaletteForm.nud_colorMin.Value= algAttribute.colorMinValue;
            imagePaletteForm.cb_colortable.SelectedIndex = algAttribute.colorIndex;

            imagePaletteForm.dtb_brightness.ValueChanged += Dtb_brightness_ValueChanged;
            imagePaletteForm.dtb_opacity.ValueChanged += Dtb_opacity_ValueChanged;
            imagePaletteForm.nud_brightness.ValueChanged += Nud_brightness_ValueChanged;
            imagePaletteForm.nud_opacity.ValueChanged += Nud_opacity_ValueChanged;
            imagePaletteForm.dtb_colorMax.ValueChanged += Dtb_colorMax_ValueChanged;
            imagePaletteForm.dtb_colorMin.ValueChanged += Dtb_colorMin_ValueChanged;
            imagePaletteForm.nud_colorMax.ValueChanged += Nud_colorMax_ValueChanged;
            imagePaletteForm.nud_colorMin.ValueChanged += Nud_colorMin_ValueChanged;
            imagePaletteForm.cb_colortable.SelectedIndexChanged += Cb_colortable_SelectedIndexChanged;

            

            imagePanel.cb_scientific.CheckedChanged += Cb_scientific_CheckedChanged;

            imagePanel.image_pl.MouseDown += Image_pl_MouseDown;
            imagePanel.image_pl.DoubleClick += Image_pl_DoubleClick;
            imagePanel.image_pl.MouseMove += Image_pl_MouseMove;
            imagePanel.image_pl.MouseUp += Image_pl_MouseUp;
            imagePanel.image_pl.Paint += Image_pl_Paint;


            imagePaletteForm.hpb_line.Click += Hpb_line_Click;

            imagePanel.wdb_title.MouseDown += Wdb_title_Click;
            imagePanel.FormClosing += ImagePanel_FormClosing;
            imagePanel.FormClosed += ImagePanel_FormClosed;
            imagePanel.ava_saveReport.Click += Ava_saveReport_Click;
            imagePaletteForm.hpb_rect.Click += hpb_rect_Click;
            imagePaletteForm.hpb_circe.Click += Hpb_circe_Click;
            imagePaletteForm.hpb_xianduan.Click += Hpb_xianduan_Click;
            imagePaletteForm.fb_fixSetting.Click += Fb_fixSetting_Click;

        }

        

        private void ReadTif() 
        {
            string[] tifFiles = Directory.GetFiles(path, "*.tif", SearchOption.TopDirectoryOnly);
            foreach (string tifFile in tifFiles) 
            {
                if (tifFile.ToUpper().Contains("MARKER"))
                {
                    tif_marker_path = tifFile;
                    image_mark_L16 = util.LoadTiffAsL16(tif_marker_path);
                    image_mark_byte = util.ConvertL16ImageToByteArray(image_mark_L16);
                  
                }
                else
                {
                    tif_org_path = tifFile;
                    image_org_L16 = util.LoadTiffAsL16(tif_org_path);
                    image_org_byte = util.ConvertL16ImageToByteArray(image_org_L16);
                }
            }

            imagePanel.SetButtomLabel($"{image_mark_L16.Width} x {image_mark_L16.Height}" );

        }

        private void ImageAlg(AlgAttribute aatb) 
        {

            var pseu_8bit_3_byte = new byte[image_org_byte.Length*3];
            var pseu_and_mark_8bit_3_byte = new byte[image_org_byte.Length * 3];
            var colorbarimage = new byte[colorbarW * colorbarH * 3];
            double opacity = (aatb.opacity / 100.0);
            int brightness = aatb.brightness - 127;
            unsafe
            {
                fixed (byte* pseu_16_byte_src = image_org_byte)
                {
                    fixed (byte* pseu_8bit_3_byte_src = pseu_8bit_3_byte)
                    {
                        fixed (byte* pseu_and_mark_8bit_3_byte_src = pseu_and_mark_8bit_3_byte)
                        {
                            fixed (byte* mark_L16_byte_src = image_mark_byte)
                            {
                                fixed (byte* colorbarimage_src = colorbarimage)
                                {
                                    var ret = pbpvc.render_process(pseu_16_byte_src, mark_L16_byte_src, pseu_8bit_3_byte_src,
                                        pseu_and_mark_8bit_3_byte_src, colorbarimage_src, aatb.colorIndex, (ushort)(colorbarW), (ushort)colorbarH, 16, (ushort)image_mark_L16.Width, (ushort)image_mark_L16.Height,
                                        1, aatb.colorValue, aatb.colorMinValue, aatb.scientificON == true ? 1 : 0, false, (ushort)colorbarWW, (ushort)colorh_onecolor, brightness, 1.0, opacity);
                                    if (ret != -1)
                                    {
                                        image_org_rgb24 = util.ConvertByteArrayToRgb24Image(pseu_8bit_3_byte, image_mark_L16.Width, (ushort)image_mark_L16.Height, 3);
                                        image_mark_and_org_rgb24 = util.ConvertByteArrayToRgb24Image(pseu_and_mark_8bit_3_byte, image_mark_L16.Width, (ushort)image_mark_L16.Height, 3);
                                        colorbar_rgb24_image = util.ConvertByteArrayToRgb24Image(colorbarimage, colorbarW, colorbarH, 3);
                                    }
                                }


                            }

                        }

                    }
                }
            }

            RefreshCbb();

        }


        private void AlgRun() 
        {
            while (isalgRun) 
            {
                if (isUpdateAlg == false) continue;
                AlgAttribute? aatb = null ;
                if (queueAlgAttribute.Count > 1) 
                {
                    while (queueAlgAttribute.Count>1)
                    {
                        queueAlgAttribute.Dequeue();
                    }
                }
                if (queueAlgAttribute.Count > 0)
                {
                    aatb = queueAlgAttribute.Dequeue();
                }
                if (aatb !=null) 
                {
                    ImageAlg((AlgAttribute)aatb);

                }
                Thread.Sleep(5);
            }
        }
        public void RefreshCbb() 
        {
            if (imagePanel.cbb_mode.InvokeRequired)
            {
                imagePanel.cbb_mode.Invoke(new MethodInvoker(() =>
                {
                    switch (imagePanel.cbb_mode.Text)
                    {
                        case "merge":
                            if (image_mark_and_org_rgb24 != null)
                                imagePanel.SetImage(image_mark_and_org_rgb24, colorbar_rgb24_image);
                            break;
                        case "mark":
                            if (image_mark_rgb24 != null)
                                imagePanel.SetImage(image_mark_rgb24, colorbar_rgb24_image);
                            else
                                imagePanel.SetImage(image_mark_L16, colorbar_rgb24_image);
                            break;
                        case "pseudocolor":
                            if (image_org_rgb24 != null)
                                imagePanel.SetImage(image_org_rgb24, colorbar_rgb24_image);
                            else
                                imagePanel.SetImage(image_org_L16);

                            break;
                    }
                    UpdateImages();
                }));

            }
            else
            {
                switch (imagePanel.cbb_mode.Text)
                {
                    case "merge":
                        if (image_mark_and_org_rgb24 != null)
                            imagePanel.SetImage(image_mark_and_org_rgb24, colorbar_rgb24_image);
                        break;
                    case "mark":
                        if (image_mark_rgb24 != null)
                            imagePanel.SetImage(image_mark_rgb24, colorbar_rgb24_image);
                        else
                            imagePanel.SetImage(image_mark_L16, colorbar_rgb24_image);
                        break;
                    case "pseudocolor":
                        if (image_org_rgb24 != null)
                            imagePanel.SetImage(image_org_rgb24, colorbar_rgb24_image);
                        else
                            imagePanel.SetImage(image_org_L16);

                        break;
                }
                UpdateImages();
            }
        }

        private void UpdateImages()
        {
            if (algAttribute.scientificON)
            {

                imagePanel.lb_wh.Text = "Radiance (p/sec/cm²/sr)\n color scale\n min=" + util.GetscientificNotation(algAttribute.colorMinValue) + "\n max=" + util.GetscientificNotation(algAttribute.colorValue);
            }
            else
            {
                imagePanel.lb_wh.Text = "Radiance (p/sec/cm²/sr)\n color scale\n min=" + algAttribute.colorMinValue.ToString() + "\n max=" + algAttribute.colorValue.ToString();
            }

        }


        private bool IsPointInRectangles(System.Drawing.Point point, List<RectAttribute> rectangles,out Corner cner,out RectAttribute curRect,out int index) 
        {
            curRect = new RectAttribute();  
            cner = Corner.None;
            index = 0;
            foreach (var rect in rectangles)
            {

                System.Drawing.Point topLeft = new System.Drawing.Point(rect.rect.Left, rect.rect.Top);
                System.Drawing.Point topRight = new System.Drawing.Point(rect.rect.Right, rect.rect.Top);
                System.Drawing.Point bottomLeft = new System.Drawing.Point(rect.rect.Left, rect.rect.Bottom);
                System.Drawing.Point bottomRight = new System.Drawing.Point(rect.rect.Right, rect.rect.Bottom);

                if (ImageProcess.IsNearCorner(point,new System.Drawing.Point(rect.rect.Left,rect.rect.Top),CircleRadius)) 
                {
                    imagePanel.image_pl.Cursor = Cursors.SizeNWSE;
                    cner = Corner.TopLeft;
                    curRect = rect;
                    return true;
                }
                else if (ImageProcess.IsNearCorner(point, new System.Drawing.Point(rect.rect.Right, rect.rect.Top), CircleRadius))
                {
                    imagePanel.image_pl.Cursor = Cursors.SizeNESW;
                    cner = Corner.TopRight;
                    curRect = rect;
                    return true;
                }
                else if (ImageProcess.IsNearCorner(point, new System.Drawing.Point(rect.rect.Left, rect.rect.Bottom), CircleRadius))
                {
                    imagePanel.image_pl.Cursor = Cursors.SizeNESW;
                    cner = Corner.BottomLeft;
                    curRect = rect;
                    return true;
                }
                else if (ImageProcess.IsNearCorner(point, new System.Drawing.Point(rect.rect.Right, rect.rect.Bottom), CircleRadius))
                {
                    imagePanel.image_pl.Cursor = Cursors.SizeNWSE;
                    cner = Corner.BottomRight;
                    curRect = rect;
                    return true;
                }

                else if (rect.rect.Contains(point)) 
                {
                    imagePanel.image_pl.Cursor = Cursors.SizeAll;
                    cner = Corner.drawMouse;
                    curRect = rect;
                    return true;
                }
                index++;

            }
            return false;
        }
        // 判断点是否在圆圈内
        private bool IsPointInCircle(System.Drawing.Point point, List<CirceAndInfo> circleCenter ,out Corner cner, out CirceAndInfo curRect, out int index)
        {
            cner = Corner.None;
            curRect = new CirceAndInfo();
            index = 0;
            foreach (var circle in circleCenter)
            {
                int radius = (int)Math.Sqrt(Math.Pow(circle.center.X - circle.Radius.X, 2) + Math.Pow(circle.center.Y - circle.Radius.Y, 2));
                double distance = Math.Sqrt(Math.Pow(point.X - circle.center.X, 2) + Math.Pow(point.Y - circle.center.Y, 2));
                if (ImageProcess.IsNearCorner(point, circle.Radius, CircleRadius)) 
                {
                    imagePanel.image_pl.Cursor = Cursors.SizeNESW;
                    curRect = circle;
                    cner = Corner.BottomLeft;
                    return true;
                }
                else if (distance <= radius) 
                {
                    curRect = circle;
                    cner = Corner.drawMouse;
                    imagePanel.image_pl.Cursor = Cursors.SizeAll;
                    return true;
                }
                index++;
            }

            return false;
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
        #endregion


        #region 事件
        private void Wdb_title_Click(object sender, EventArgs e)
        {
           this.pl_right.Controls.Clear();
            this.pl_right.Controls.Add(this.imagePaletteForm);
        }
        private void Cb_scientific_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            ScientificON = imagePanel.cb_scientific.Checked;
        }
       

        private void Cbb_mode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(imagePanel.cbb_mode.Text == "mark") 
            {
                imagePaletteForm.cb_colortable.SelectedIndex = 7;
            }
            else
            {
                RefreshCbb();
            }
            
           
        }
        private void Dtb_brightness_ValueChanged()
        {
            Brightness = imagePaletteForm.dtb_brightness.Value;
        }
        private void Dtb_opacity_ValueChanged()
        {
            Opacity = imagePaletteForm.dtb_opacity.Value;
        }
        private void Nud_opacity_ValueChanged(object sender, System.EventArgs e)
        {
            Opacity = (int)imagePaletteForm.nud_opacity.Value;
        }

        private void Nud_brightness_ValueChanged(object sender, System.EventArgs e)
        {
            Brightness = (int)imagePaletteForm.nud_brightness.Value;
        }
        private void Nud_colorMin_ValueChanged(object sender, System.EventArgs e)
        {
            ColorMin = (int)imagePaletteForm.nud_colorMin.Value;
        }

        private void Nud_colorMax_ValueChanged(object sender, System.EventArgs e)
        {
            ColorMax = (int)imagePaletteForm.nud_colorMax.Value;
            
                
        }

        private void Dtb_colorMin_ValueChanged()
        {
            ColorMin = (int)imagePaletteForm.dtb_colorMin.Value;
        }

        private void Dtb_colorMax_ValueChanged()
        {
            ColorMax = (int)imagePaletteForm.dtb_colorMax.Value;
        }
        private void Cb_colortable_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ColorIndex = imagePaletteForm.cb_colortable.SelectedIndex;
        }


        #region imagepanel
        private void Image_pl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;


            // 绘制直线
            if ((startPoint != System.Drawing.Point.Empty && endPoint != System.Drawing.Point.Empty))
            {
                var srart = ImageProcess.ConvertRealToPictureBox(startPoint, imagePanel.image_pl);
                var end = ImageProcess.ConvertRealToPictureBox(endPoint, imagePanel.image_pl);
                g.DrawLine(Pens.Red, srart, end);

                // 绘制起点和终点的圆圈
                ImageProcess.DrawCircle(g, srart, CircleRadius, Pens.Blue, Brushes.LightBlue);
                ImageProcess.DrawCircle(g, end, CircleRadius, Pens.Blue, Brushes.LightBlue);
            }


            int index = 0;
            foreach (var rect in rectangles)
            {

                System.Drawing.Rectangle p = rect.rect;
                if (isRecDragging)
                {
                    if (index == rectDragStartIndex)
                    {
                        p = recDragRect;

                    }
                }

                var r = ImageProcess.ConvertRealRectangleToPictureBox(p, imagePanel.image_pl);
                e.Graphics.DrawRectangle(Pens.Red, r);

                System.Drawing.Point[] corners = new System.Drawing.Point[]
                {
                    new System.Drawing.Point(r.Left, r.Top), // 左上角
                    new System.Drawing.Point(r.Right, r.Top), // 右上角
                    new System.Drawing.Point(r.Left, r.Bottom), // 左下角
                    new System.Drawing.Point(r.Right, r.Bottom) // 右下角
                };
                foreach (var item in corners)
                {

                    ImageProcess.DrawCircle(g, new System.Drawing.Point(item.X, item.Y), CircleRadius, Pens.Blue, Brushes.LightBlue);
                }

                if (!isRecDragging) 
                {
                    
                    // 画标签
                    if (rect.pdinfovc != null)
                    {
                        // 指向线的起点在矩形的顶部中心
                        System.Drawing.Point centerTopPoint = new System.Drawing.Point(
                            r.Left + r.Width / 2,
                            r.Top
                        );

                        // 指向线的终点在矩形上方10像素
                        System.Drawing.Point labelPoint = new System.Drawing.Point(
                            centerTopPoint.X,
                            centerTopPoint.Y - 10
                        );
                        // 画垂直的指向线
                        g.DrawLine(Pens.Red, centerTopPoint, labelPoint);
                        string labelText = "";
                        if (algAttribute.scientificON)
                        {


                            labelText = $"ROI:{index+1},AOD:{util.GetscientificNotation(rect.pdinfovc.AOD)},IOD:{util.GetscientificNotation(rect.pdinfovc.IOD)}," +
                                       $"\r\nmaxOD:{util.GetscientificNotation(rect.pdinfovc.maxOD)},minOD:{util.GetscientificNotation(rect.pdinfovc.minOD)},Count:{util.GetscientificNotation(rect.pdinfovc.Count)}";
                        }
                        else
                        {
                            labelText = $"ROI:{index + 1},AOD:{rect.pdinfovc.AOD},IOD:{rect.pdinfovc.IOD}," +
                                       $"\r\nmaxOD:{rect.pdinfovc.maxOD},minOD:{rect.pdinfovc.minOD},Count:{rect.pdinfovc.Count}"; // 标签编号
                        }

                        Font font = new Font("Arial", 8); // 字体
                        Brush brush = Brushes.Red; // 字体颜色
                        g.DrawString(labelText, font, brush, labelPoint.X - 10, labelPoint.Y - 15);
                    }
                }
                index++;
            }

            if (drawRect) 
            {
                if (currentRectangle.HasValue)
                {
                    var r = ImageProcess.ConvertRealRectangleToPictureBox(currentRectangle.Value, imagePanel.image_pl);
                    e.Graphics.DrawRectangle(Pens.Red, r);
                }
            }

            index = 0;
            foreach (var item in CircleAndInfoList)
            {
                var centerPoint = ImageProcess.ConvertRealToPictureBox(item.center, imagePanel.image_pl);
                var radiusPoint = ImageProcess.ConvertRealToPictureBox(item.Radius, imagePanel.image_pl);
                int radius = (int)Math.Sqrt(Math.Pow(centerPoint.X - radiusPoint.X, 2) + Math.Pow(centerPoint.Y - radiusPoint.Y, 2));
                if (isCirDragging) 
                {
                    if (index == cirDragStartIndex) 
                    {
                        centerPoint = ImageProcess.ConvertRealToPictureBox(circleCenter, imagePanel.image_pl);
                        radiusPoint = ImageProcess.ConvertRealToPictureBox(circleRadio, imagePanel.image_pl); ;
                        radius = (int)Math.Sqrt(Math.Pow(centerPoint.X - radiusPoint.X, 2) + Math.Pow(centerPoint.Y - radiusPoint.Y, 2));
                    }
                   
                }
                
                e.Graphics.DrawEllipse(Pens.Red, centerPoint.X - radius, centerPoint.Y - radius, radius * 2, radius * 2);
                ImageProcess.DrawCircle(g, new System.Drawing.Point(radiusPoint.X, radiusPoint.Y), CircleRadius, Pens.Blue, Brushes.LightBlue);

                if (!isCirDragging) 
                {
                    // 画标签
                    if (item.pdinfovc != null)
                    {
                        // 指向线的终点在矩形上方10像素
                        System.Drawing.Point labelPoint = new System.Drawing.Point(
                            centerPoint.X,
                            centerPoint.Y - radius - 10
                        );
                        // 画垂直的指向线
                        g.DrawLine(Pens.Red, centerPoint, labelPoint);
                        string labelText = "";
                        if (algAttribute.scientificON)
                        {


                            labelText = $"ROI:{index + 1},AOD:{util.GetscientificNotation(item.pdinfovc.AOD)},IOD:{util.GetscientificNotation(item.pdinfovc.IOD)}," +
                                       $"\r\nmaxOD:{util.GetscientificNotation(item.pdinfovc.maxOD)},minOD:{util.GetscientificNotation(item.pdinfovc.minOD)},Count:{util.GetscientificNotation(item.pdinfovc.Count)}";
                        }
                        else
                        {
                            labelText = $"ROI:{index + 1},AOD:{item.pdinfovc.AOD},IOD:{item.pdinfovc.IOD}," +
                                       $"\r\nmaxOD:{item.pdinfovc.maxOD},minOD:{item.pdinfovc.minOD},Count:{item.pdinfovc.Count}"; // 标签编号
                        }
                        Font font = new Font("Arial", 8); // 字体
                        Brush brush = Brushes.Red; // 字体颜色
                        g.DrawString(labelText, font, brush, labelPoint.X - 10, labelPoint.Y - 15);
                    }
                }
                index++;
            }

            if (drawCircle) 
            {
                var curCirRadioPoint = ImageProcess.ConvertRealToPictureBox(circleRadio, imagePanel.image_pl);
                var curCirCenterPoint = ImageProcess.ConvertRealToPictureBox(circleCenter, imagePanel.image_pl);
                int radius = (int)Math.Sqrt(Math.Pow(curCirCenterPoint.X - curCirRadioPoint.X, 2) + Math.Pow(curCirCenterPoint.Y - curCirRadioPoint.Y, 2));
                e.Graphics.DrawEllipse(Pens.Red, curCirCenterPoint.X - radius, curCirCenterPoint.Y - radius, radius * 2, radius * 2);
                ImageProcess.DrawCircle(g, new System.Drawing.Point(curCirRadioPoint.X, curCirRadioPoint.Y), CircleRadius, Pens.Blue, Brushes.LightBlue);
            }

            index = 0;
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
                            point = ImageProcess.ConvertRealToPictureBox( item,imagePanel.image_pl);
                        }
                        System.Drawing.Point curpoint = ImageProcess.ConvertRealToPictureBox(item, imagePanel.image_pl);
                        ImageProcess.DrawCircle(g, curpoint, CircleRadius, Pens.Blue, Brushes.LightBlue);
                        g.DrawLine(Pens.Red, curpoint, point);
                        point = curpoint;
                        isStart++;
                    }

                }

                if (drawpolygon)
                {

                    point = ImageProcess.ConvertRealToPictureBox( curPolygonAndInfoList.points[curPolygonAndInfoList.points.Count - 1], imagePanel.image_pl);
                    var p1 = ImageProcess.ConvertRealToPictureBox(curPolygonPoint, imagePanel.image_pl);
                    g.DrawLine(Pens.Red, p1, point);
                    foreach (var item in curPolygonAndInfoList.points)
                    {
                        if (isStart == 0)
                        {
                            point = ImageProcess.ConvertRealToPictureBox(item, imagePanel.image_pl);
                        }
                        System.Drawing.Point curpoint = ImageProcess.ConvertRealToPictureBox( item, imagePanel.image_pl);
                        ImageProcess.DrawCircle(g, curpoint, CircleRadius, Pens.Blue, Brushes.LightBlue);
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
                        if (algAttribute.scientificON)
                        {


                            labelText = $"ROI:{index+1},AOD:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.AOD)},IOD:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.IOD)}," +
                                       $"\r\nmaxOD:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.maxOD)},minOD:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.minOD)},Count:{util.GetscientificNotation(curPolygonAndInfoList.pdinfovc.Count)}";
                        }
                        else
                        {
                            labelText = $"ROI:{index + 1},AOD:{curPolygonAndInfoList.pdinfovc.AOD},IOD:{curPolygonAndInfoList.pdinfovc.IOD}," +
                                       $"\r\nmaxOD:{curPolygonAndInfoList.pdinfovc.maxOD},minOD:{curPolygonAndInfoList.pdinfovc.minOD},Count:{curPolygonAndInfoList.pdinfovc.Count}"; // 标签编号
                        }
                        Font font = new Font("Arial", 8); // 字体
                        Brush brush = Brushes.Red; // 字体颜色
                        System.Drawing.Point curpoint = ImageProcess.ConvertRealToPictureBox( curPolygonAndInfoList.points[0], imagePanel.image_pl);
                        g.DrawString(labelText, font, brush, curpoint.X - 10, curpoint.Y - 15);
                    }
                }
                index++;
            }

        }

        private void Image_pl_MouseUp(object sender, MouseEventArgs e)
        {
            System.Drawing.Point readLoction = ImageProcess.GetRealImageCoordinates(imagePanel.image_pl, e.Location);

            if (e.Button == MouseButtons.Left) 
            {
                if (isDragging)
                {
                    imagePanel.pl_bg_panel.Cursor = Cursors.Default;
                    isDragging = false;
                }
                else if ((drawLine) || (isStartCircleDragged || isEndCircleDragged))
                {
                    drawLine = false;
                    lineOn = false;
                    isStartCircleDragged = false;
                    isEndCircleDragged = false;
                    imagePanel.image_pl.Invalidate();

                    // 计算距离
                    double deltaX = endPoint.X - startPoint.X;
                    double deltaY = endPoint.Y - startPoint.Y;
                    var value = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                    imagePaletteForm.flb_act_mm.Text = value.ToString() + " mm";
                    imagePaletteForm.flb_act_mm.Refresh();
                }
                else if (drawRect)
                {
                    if (currentRectangle.HasValue)
                    {
                        RectAttribute rab = new RectAttribute();
                        rab.rect = currentRectangle.Value;

                        // 计算光子数并展示出来
                        float _max = algAttribute.colorValue;
                        float _min = algAttribute.colorMinValue;
                        Pseudo_infoVC curpdinfovc = null;
                        unsafe
                        {
                            fixed (byte* pseu_16_byte_src = image_org_byte)
                            {
                                curpdinfovc = pbpvc.get_pseudo_info_rect_vc(pseu_16_byte_src, 16, (ushort)image_org_L16.Width, (ushort)image_org_L16.Height,
                                    _max, _min, currentRectangle.Value.X, currentRectangle.Value.Y, currentRectangle.Value.Width, currentRectangle.Value.Height);

                            }
                        }
                        if (curpdinfovc != null)
                            rab.pdinfovc = curpdinfovc;
                        imagePaletteForm.SetInfo = "w:" + rab.rect.Width.ToString() + "h:" + rab.rect.Height.ToString();
                        // 完成绘制并保存矩形
                        rectangles.Add(rab);
                        currentRectangle = null;
                        drawRect = false;
                        imagePanel.image_pl.Invalidate();
                    }

                    drawRect = false;
                    rectOn = false;


                }
                else if (drawCircle && CircleOn)
                {
                    CirceAndInfo rab = new CirceAndInfo();
                    rab.center = circleCenter;
                    rab.Radius = circleRadio;


                    // 计算光子数并展示出来
                    float _max = algAttribute.colorValue;
                    float _min = algAttribute.colorMinValue;
                    int radius = (int)Math.Sqrt(Math.Pow(rab.center.X - rab.Radius.X, 2) + Math.Pow(rab.center.Y - rab.Radius.Y, 2));

                    imagePaletteForm.SetInfo = "radio:" + radius.ToString();
                    Pseudo_infoVC curpdinfovc = null;
                    unsafe
                    {
                        fixed (byte* pseu_16_byte_src = image_org_byte)
                        {
                            curpdinfovc = pbpvc.get_pseudo_info_circle_vc(pseu_16_byte_src, 16,
                                (ushort)image_org_L16.Width, (ushort)image_org_L16.Height, _max, _min, rab.center.X, rab.center.Y, radius);

                        }
                    }
                    if (curpdinfovc != null)
                        rab.pdinfovc = curpdinfovc;
                    // 完成绘制并保存矩形
                    CircleAndInfoList.Add(rab);

                    drawCircle = false;
                    CircleOn = false;
                    imagePanel.image_pl.Invalidate();


                }
                else if (isRecDragging)
                {
                    RectAttribute rattb = new RectAttribute();
                    rattb.rect = recDragRect;
                    // 计算光子数并展示出来
                    float _max = algAttribute.colorValue;
                    float _min = algAttribute.colorMinValue;
                    Pseudo_infoVC curpdinfovc = null;
                    unsafe
                    {
                        fixed (byte* pseu_16_byte_src = image_org_byte)
                        {
                            curpdinfovc = pbpvc.get_pseudo_info_rect_vc(pseu_16_byte_src, 16, (ushort)image_org_L16.Width, (ushort)image_org_L16.Height,
                                _max, _min, recDragRect.X, recDragRect.Y, recDragRect.Width, recDragRect.Height);

                        }
                    }
                    if (curpdinfovc != null)
                        rattb.pdinfovc = curpdinfovc;

                    rectangles[rectDragStartIndex] = rattb;
                    imagePaletteForm.SetInfo = "w:" + recDragRect.Width.ToString() + "h:" + recDragRect.Height.ToString();
                    isRecDragging = false;
                    rectActiveCorner = Corner.None;
                    rectDragStartIndex = -1;

                    imagePanel.image_pl.Invalidate();

                }
                else if (isCirDragging) 
                {
                    CirceAndInfo circeAndInfo = new CirceAndInfo();

                    circeAndInfo.Radius = circleRadio;
                    circeAndInfo.center = circleCenter;

                    float _max = algAttribute.colorValue;
                    float _min = algAttribute.colorMinValue;
                    int radius = (int)Math.Sqrt(Math.Pow(circeAndInfo.center.X - circeAndInfo.Radius.X, 2) + Math.Pow(circeAndInfo.center.Y - circeAndInfo.Radius.Y, 2));
                    Pseudo_infoVC curpdinfovc = null;
                    unsafe
                    {
                        fixed (byte* pseu_16_byte_src = image_org_byte)
                        {
                            curpdinfovc = pbpvc.get_pseudo_info_circle_vc(pseu_16_byte_src, 16,
                                (ushort)image_org_L16.Width, (ushort)image_org_L16.Height, _max, _min, circeAndInfo.center.X, circeAndInfo.center.Y, radius);

                        }
                    }
                    circeAndInfo.pdinfovc = curpdinfovc;
                    imagePaletteForm.SetInfo = "radio:" + radius.ToString();
                    CircleAndInfoList[cirDragStartIndex] = circeAndInfo;
                    isCirDragging = false;
                    cirDragStartIndex = -1;
                    imagePanel.image_pl.Invalidate();
                }
            }
            

        }

        private void Image_pl_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point readLoction = ImageProcess.ConvertPictureBoxToReal(e.Location, imagePanel.image_pl);
            if (lineOn && drawLine)
            {
                endPoint = readLoction; // 更新终点位置
                imagePanel.image_pl.Invalidate(); // 触发重绘
            }
            else if (drawRect && e.Button == MouseButtons.Left)
            {
                // 动态调整矩形大小
                int x = Math.Min(leftTopPoint.X, readLoction.X);
                int y = Math.Min(leftTopPoint.Y, readLoction.Y);
                int width = Math.Abs(readLoction.X - leftTopPoint.X);
                int height = Math.Abs(readLoction.Y - leftTopPoint.Y);

                currentRectangle = new System.Drawing.Rectangle(x, y, width, height);
                imagePanel.image_pl.Invalidate(); // 触发重绘
            }
            else if (drawCircle && e.Button == MouseButtons.Left)
            {
                circleRadio = readLoction;
                imagePanel.image_pl.Invalidate();
            }
            else if (drawpolygon && linepolygonON) 
            {
                curPolygonPoint = readLoction;
                imagePanel.image_pl.Invalidate();
            }
            else if (isDragging && e.Button == MouseButtons.Left)
            {
                int deltaX = e.X - mouseDownPosition.X;
                int deltaY = e.Y - mouseDownPosition.Y;

                // 仅当鼠标移动时调整位置，减少频繁重绘
                if (Math.Abs(deltaX) > 1 || Math.Abs(deltaY) > 1)
                {
                    imagePanel.pl_bg_panel.Left = pictureBoxStartPosition.X + deltaX;
                    imagePanel.pl_bg_panel.Top = pictureBoxStartPosition.Y + deltaY;

                    // 防止 pl_bg_image 拖动超出 pl_image 的范围
                    if (imagePanel.pl_bg_panel.Left > 0) imagePanel.pl_bg_panel.Left = 0;
                    if (imagePanel.pl_bg_panel.Top > 0) imagePanel.pl_bg_panel.Top = 0;
                    if (imagePanel.pl_bg_panel.Right < imagePanel.pl_panel_image.ClientSize.Width)
                        imagePanel.pl_bg_panel.Left = imagePanel.pl_panel_image.ClientSize.Width - imagePanel.pl_bg_panel.Width;
                    if (imagePanel.pl_bg_panel.Bottom < imagePanel.pl_panel_image.ClientSize.Height)
                        imagePanel.pl_bg_panel.Top = imagePanel.pl_panel_image.ClientSize.Height - imagePanel.pl_bg_panel.Height;
                }
            }
            else if (ImageProcess.IsNearCorner(readLoction, startPoint, CircleRadius) || ImageProcess.IsNearCorner(readLoction, endPoint, CircleRadius))
            {
                imagePanel.image_pl.Cursor = Cursors.Hand;
            }
            else if (isRecDragging)
            {
                recDragRect = rectOriginalRect.rect;
                switch (rectActiveCorner)
                {
                    case Corner.drawMouse:
                        int offsetX = readLoction.X - recDragStart.X;
                        int offsetY = readLoction.Y - recDragStart.Y;
                        recDragRect.X += offsetX;
                        recDragRect.Y += offsetY;

                        break;
                    case Corner.TopLeft:
                        recDragRect.Width += recDragRect.X - readLoction.X;
                        recDragRect.Height += recDragRect.Y - readLoction.Y;
                        recDragRect.X = readLoction.X;
                        recDragRect.Y = readLoction.Y;

                        break;
                    case Corner.TopRight:
                        recDragRect.Width = readLoction.X - recDragRect.X;
                        recDragRect.Height += recDragRect.Y - readLoction.Y;
                        recDragRect.Y = readLoction.Y;

                        break;
                    case Corner.BottomLeft:
                        recDragRect.Width += recDragRect.X - readLoction.X;
                        recDragRect.Height = readLoction.Y - recDragRect.Y;
                        recDragRect.X = readLoction.X;

                        break;
                    case Corner.BottomRight:
                        recDragRect.Width = readLoction.X - recDragRect.X;
                        recDragRect.Height = readLoction.Y - recDragRect.Y;

                        break;
                    default:
                        break;
                }
                imagePanel.image_pl.Invalidate(); // 触发重绘
            }
            else if (isCirDragging)
            {
                if (rectActiveCorner != Corner.None)
                {
                    if (rectActiveCorner == Corner.drawMouse)
                    {
                        // 计算鼠标位置与起始拖拽点的偏移量
                        int offsetX = readLoction.X - cirDragStart.X;
                        int offsetY = readLoction.Y - cirDragStart.Y;

                        // 更新圆心位置
                        circleCenter.X += offsetX;
                        circleCenter.Y += offsetY;
                        circleRadio.X += offsetX;
                        circleRadio.Y += offsetY;

                        // 重新设置起始拖拽点为当前鼠标位置，以便下一次计算
                        cirDragStart = readLoction;


                    }
                    else
                    {
                        circleRadio = readLoction;
                    }
                }
                imagePanel.image_pl.Invalidate();


            }
            else if (IsPointInRectangles(readLoction, rectangles, out var cner, out var cr, out var index)) // 遍历是否在所有矩形或者角点附近
            {

            }
            else if (IsPointInCircle(readLoction, CircleAndInfoList, out var cner1, out var curRect, out var index1))
            {

            }
            else if (isStartCircleDragged)
            {
                startPoint = readLoction;

                imagePanel.image_pl.Invalidate();
            }
            else if (isEndCircleDragged)
            {
                endPoint = readLoction;
                imagePanel.image_pl.Invalidate();
            }
            else
            {
                imagePanel.image_pl.Cursor = Cursors.Default;
            }
        }

        private void Image_pl_DoubleClick(object sender, System.EventArgs e)
        {
            if (linepolygonON && drawpolygon) 
            {
                
                if (curPolygonAndInfoList.points != null)
                {
                    System.Drawing.Point firstPoint = curPolygonAndInfoList.points[0];
                    System.Drawing.Point lastPoint = curPolygonAndInfoList.points[curPolygonAndInfoList.points.Count - 1];
                    firstPoint = ImageProcess.ConvertRealToPictureBoxCoordinates(imagePanel.image_pl, firstPoint);
                    lastPoint = ImageProcess.ConvertRealToPictureBoxCoordinates(imagePanel.image_pl, lastPoint);
                    double deltaX = lastPoint.X - firstPoint.X;
                    double deltaY = lastPoint.Y - firstPoint.Y;
                    var value = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                    if (value <= 5)
                    {
                        lastPoint = firstPoint;
                        drawpolygon = false;
                        linepolygonON = false;
                        imagePanel.image_pl.Invalidate();

                        // 计算光子量

                        float _max = algAttribute.colorValue;
                        float _min = algAttribute.colorMinValue;
                        List<Point_VC> curVclist = new List<Point_VC>();
                        Pseudo_infoVC curpdinfovc = null;
                        foreach (var item in curPolygonAndInfoList.points)
                        {
                            Point_VC pvc = new Point_VC(item.X, item.Y);
                            curVclist.Add(pvc);

                        }
                        unsafe
                        {
                            fixed (byte* pseu_16_byte_src = image_org_byte)
                            {
                                curpdinfovc = pbpvc.get_pseudo_info_polygon_vc(pseu_16_byte_src, 16,
                                    (ushort)image_org_L16.Width, (ushort)image_org_L16.Height, _max, _min, curVclist);

                            }
                        }
                        curPolygonAndInfoList.pdinfovc = curpdinfovc;

                        PolygonAndInfoList.Add(curPolygonAndInfoList);

                    }

                    imagePanel.image_pl.Invalidate();
                }
            }
        }

        private void Image_pl_MouseDown(object sender, MouseEventArgs e)
        {
            System.Drawing.Point readLoction = ImageProcess.ConvertPictureBoxToReal( e.Location, imagePanel.image_pl);
            if (e.Button == MouseButtons.Left)
            {
                if (lineOn)
                {
                    drawLine = true;
                    startPoint = readLoction;
                }
                else if (rectOn)
                {
                    // 开始绘制新矩形
                    drawRect = true;
                    leftTopPoint = readLoction;
                    currentRectangle = new System.Drawing.Rectangle(readLoction.X, readLoction.Y, 0, 0);
                }
                else if (CircleOn)
                {
                    //开始绘制圆形
                    drawCircle = true;
                    circleRadio = readLoction;
                    circleCenter = readLoction;
                }
                else if (linepolygonON) 
                {
                    drawpolygon = true;
                    if (curPolygonAndInfoList.points == null)
                    {
                        curPolygonAndInfoList.points = new List<System.Drawing.Point>();
                    }
                    System.Drawing.Point curPoint = readLoction;
                    curPolygonAndInfoList.points.Add(curPoint);
                }
                else if (imagePanel.IsImageLargerThanPanel())
                {
                    isDragging = true;
                    mouseDownPosition = e.Location;
                    pictureBoxStartPosition = imagePanel.pl_bg_panel.Location;
                    imagePanel.pl_bg_panel.Cursor = Cursors.Hand;
                }
                else if (ImageProcess.IsNearCorner(readLoction, startPoint, CircleRadius))
                {

                    isStartCircleDragged = true;


                }
                else if (ImageProcess.IsNearCorner(readLoction, endPoint, CircleRadius))
                {
                    isEndCircleDragged = true;

                }
                else if (IsPointInCircle(readLoction, CircleAndInfoList, out var cner1, out var curRect, out var index1))
                {
                    rectActiveCorner = cner1;
                    if (rectActiveCorner != Corner.None)
                    {
                        isCirDragging = true;
                        cirDragStart = readLoction;
                        cireOriginalCire = curRect;
                        circleCenter = curRect.center;
                        circleRadio = curRect.Radius;
                        cirDragStartIndex = index1;
                    }

                }
                else if (IsPointInRectangles(readLoction, rectangles, out var cner, out var cr, out var index))
                {
                    rectActiveCorner = cner;

                    if (rectActiveCorner != Corner.None)
                    {
                        isRecDragging = true;
                        recDragStart = readLoction;
                        rectOriginalRect = cr;
                        rectDragStartIndex = index;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right) 
            {
                if (ImageProcess.IsPointOnLine(readLoction,startPoint,endPoint,CircleRadius))
                {
                    startPoint = new System.Drawing.Point(-10, 0);
                    endPoint = new System.Drawing.Point(-10, 0);
                    imagePanel.image_pl.Invalidate();
                    imagePaletteForm.flb_act_mm.Text = ("0");
                    imagePaletteForm.flb_act_mm.Refresh();
                }
                else if (IsPointInCircle(readLoction, CircleAndInfoList, out var cner1, out var curRect, out var index1))
                {
                    CircleAndInfoList.RemoveAt(index1);
                    imagePanel.image_pl.Invalidate();
                }
                else if (IsPointInRectangles(readLoction, rectangles, out var cner, out var cr, out var index))
                {
                    rectangles.RemoveAt(index);
                    imagePanel.image_pl.Invalidate();
                }
                else if (drawpolygon==false &&  IsPointInPolygon(readLoction, curPolygonAndInfoList))
                {
                    PolygonAndInfoList.Clear();
                    curPolygonAndInfoList.points.Clear();
                    curPolygonAndInfoList.pdinfovc = null;
                    imagePanel.image_pl.Invalidate();
                }
            }
            
        }
        

        private void ImagePanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.imagePaletteForm != null)
            {
                this.imagePaletteForm.Close();
                this.imagePaletteForm.Dispose();
                this.imagePaletteForm = null;
            }
            this.pl_right.Controls.Clear();

        }

        private void ImagePanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.bioanalysisMannages[path] = null;
            this.bioanalysisMannages.Remove(path);
        }

        private void Ava_saveReport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()) 
            {
                saveFileDialog.Title = "保存Panel图像";
                saveFileDialog.Filter = "Excel 文件 (*.xlsx)|*.xlsx";
                if (saveFileDialog.ShowDialog()
                    == DialogResult.OK) 
                {
                    // 保存光子数到xlsx里  先保存矩形的
                    if (algAttribute.scientificON) 
                    {
                        var records = new List<DataRecordString>();
                        int index = 0;
                        foreach (var item in rectangles)
                        {
                            index++;
                            if (algAttribute.scientificON)
                            {
                                DataRecordString dr = new DataRecordString();
                                dr.index = index;

                                dr.IOD = util.GetscientificNotation(item.pdinfovc.IOD);
                                dr.AOD = util.GetscientificNotation(item.pdinfovc.AOD);
                                dr.max = util.GetscientificNotation(item.pdinfovc.maxOD);
                                dr.min = util.GetscientificNotation(item.pdinfovc.minOD);
                                dr.Count = util.GetscientificNotation(item.pdinfovc.Count);
                                records.Add(dr);
                            }

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
                        foreach (var item in rectangles)
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
                    
                }
            }
        }
        #endregion
        #region imagePaletteForm
        private void Hpb_line_Click(object sender, EventArgs e)
        {
            lineOn = true;
        }

        private void hpb_rect_Click(object sender, EventArgs e)
        {
            rectOn = true;
        }
        private void Hpb_circe_Click(object sender, EventArgs e)
        {
            CircleOn = true;
        }
        private void Hpb_xianduan_Click(object sender, EventArgs e)
        {
            linepolygonON = true;
        }
        private void Fb_fixSetting_Click(object sender, EventArgs e)
        {
          
            // 将矩形和圆形按照大小进行统一设定
            for (int i = 0; i < rectangles.Count; i++)
            {
                RectAttribute rattb = new RectAttribute();
                rattb.rect = rectangles[i].rect;
                rattb.rect.Width = imagePaletteForm.ROI_W;
                rattb.rect.Height = imagePaletteForm.ROI_H;
                rattb.pdinfovc = rectangles[i].pdinfovc;
                
                rectangles[i] = rattb;
            }
            

           
            for (int i = 0; i < CircleAndInfoList.Count; i++) 
            {
                CirceAndInfo circeAndInfo = new CirceAndInfo();
                circeAndInfo.pdinfovc = CircleAndInfoList[i].pdinfovc;
                double angleInRadians = 90 * Math.PI / 180; // Convert degrees to radians
                double x = CircleAndInfoList[i].center.X + imagePaletteForm.CIRCLE_R * Math.Cos(angleInRadians);
                double y = CircleAndInfoList[i].center.Y + imagePaletteForm.CIRCLE_R * Math.Sin(angleInRadians);
                circeAndInfo.Radius = new System.Drawing.Point((int)x, (int)y);
                circeAndInfo.center = CircleAndInfoList[i].center;

                CircleAndInfoList[i] = circeAndInfo;

            }
            imagePanel.image_pl.Invalidate();
        }
        #endregion
        #endregion
        #region 对外接口
        public BioanalyImagePanel GetImagePanel 
        {
            get { return imagePanel; }
        }

        public void WindowAdaptive() 
        {
            imagePanel.WindowState = FormWindowState.Maximized;
        }

        #endregion
    }
}
