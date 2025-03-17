using AntdUI;
using MiniExcelLibs;
using OfficeOpenXml;
using PBAnaly.Assist;
using PBAnaly.UI;
using PBBiologyVC;
using ReaLTaiizor.Extension;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Shell;
using static PBAnaly.Module.BioanalysisMannage;

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

            public bool sharpen; //锐化

            public float pixel_size;
        }

        public struct RectAttribute 
        {
            public System.Drawing.Rectangle rect;
            public Pseudo_infoVC pdinfovc;
        }
        public struct CirceAndInfo
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
        public struct TextBoxInfo 
        {
            public int index;
            public string value;
            public System.Drawing.Rectangle rect;
        }

        private enum ShapeForm
        {
            None,
            Line,
            Polygon,
            Rect,
            WandRect,
            Circle,
            TextBoxRect
        }
        #endregion
        #region 变量
        private ShapeForm curTmpDownShape = ShapeForm.None;// 用于快捷键  临时确认点击了那个矩形
        private ShapeForm curShape = ShapeForm.None;
        private int curTmpDownShapeIndex;
        private int curShapeIndex;
        private System.Drawing.Point curTmpDownShapePoint;
        private System.Drawing.Point curShapePoint;

        public bool IsActive { get; set; } // 当前窗口是否在活跃状态  用来判断是否需要操作
        public int ImageIndex { get; set; }// 图片加载进来的序号
        public int Arrangement { get; set; } // 0:代表单张图 1:代表是合并图图但不做处理 2:代表是合并图 并且为处理图
        private Dictionary<string, BioanalysisMannage> bioanalysisMannages;
        public string path { get; set; }
        public string curImagePath { get; private set; }
        private string mark_path;
        private string tif_marker_path;
        private string tif_org_path;
        private Image<L16> image_mark_L16 = null;
        private byte[] image_mark_byte = null;
        private byte[] image_mark_sharpen_byte = null;
        private Image<L16> image_mark_sharpen_L16 = null;
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
        public PBBiologyVC.PBImageProcessVC pbpvc = new PBBiologyVC.PBImageProcessVC();

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

        private bool isContinuous = false;

        private const int CircleRadius = 5;
        private bool lineOn =false;
        private bool drawLine = false;
        private bool TextboxOn  = false;// 绘制textbox的标签


        private bool CircleOn = false;
        private bool rectOn = false;
        private bool linepolygonON = false;
        private bool isRecDragging = false;
        private bool isTextBoxRecDragging = false;
        private bool iswandON = false;
        private bool curIsCopy = false; // 是否需要拷贝
        public List<RectAttribute> rectangles = new List<RectAttribute>(); // 存储所有绘制完成的矩形
        private RectAttribute oldCopyRect; //当前需要赋值的矩形
        private System.Drawing.Rectangle? currentRectangle = null; // 当前正在绘制的矩形
        private System.Drawing.Point leftTopPoint; // 矩形左上角的起始点
        private bool drawRect = false; // 是否正在绘制
        private System.Drawing.Point recDragStart;
        private System.Drawing.Rectangle recDragRect;
        private RectAttribute rectOriginalRect;
        private int rectDragStartIndex = -1;

        private bool drawCircle = false;//是否绘制圆
        private bool isCirDragging = false;
        public List<CirceAndInfo> CircleAndInfoList = new List<CirceAndInfo>();
        private CirceAndInfo oldCopyCircle;
        private PolygonAndInfo oldPolygonAndInfoList;
        private System.Drawing.Point cirDragStart;
        private System.Drawing.Point circleCenter;
        private System.Drawing.Point circleRadio;
        private int cirDragStartIndex = -1;
        private CirceAndInfo cireOriginalCire;

        private List<TextBoxInfo> textBoxInfos = new List<TextBoxInfo>();
        private bool drawTextBox = false;
        private int curTextBox = -1;
        private TextBoxInfo curTextBoxinfo;
        private List<RectAttribute> wandRectangle = new List<RectAttribute>(); // 存储所有绘制完成的矩形
        private bool drawpolygon = false;
        private List<PolygonAndInfo> PolygonAndInfoList = new List<PolygonAndInfo>();
        private PolygonAndInfo curPolygonAndInfoList = new PolygonAndInfo();
        private System.Drawing.Point curStartPolygonPoint = new System.Drawing.Point(0, 0);
        private System.Drawing.Point curPolygonPoint = new System.Drawing.Point(0, 0);

        private System.Drawing.Point startPoint = new System.Drawing.Point(-100, 0);
        private System.Drawing.Point endPoint = new System.Drawing.Point(-100, 0);


        private bool isStartCircleDragged, isEndCircleDragged;

        private enum Corner { None,TopLeft,TopRight,BottomLeft,BottomRight,drawMouse}

        private Corner rectActiveCorner = Corner.None;
        #endregion
        #endregion

        #region 构造函数

        public bool Sharpen 
        {
            get { return algAttribute.sharpen; }
            set 
            {
                if (algAttribute.sharpen != value) 
                {
                    algAttribute.sharpen = value;
                    bool fix = true;
                    if (algAttribute.sharpen)
                    {
                        if (image_mark_sharpen_L16 != null) 
                        {
                            var _mark_L16 = image_mark_L16.Clone();
                            byte[] _mark_byte = new byte[image_mark_sharpen_byte.Length];
                            Array.Copy(image_mark_byte, _mark_byte, image_mark_byte.Length);
                            image_mark_L16 = image_mark_sharpen_L16.Clone();
                            Array.Copy(image_mark_sharpen_byte, image_mark_byte, image_mark_byte.Length);
                            image_mark_sharpen_L16 = _mark_L16;
                            image_mark_sharpen_byte = _mark_byte;
                        }
                        else
                        {
                            image_mark_sharpen_byte = new byte[image_mark_byte.Length];
                            image_mark_sharpen_L16 = image_mark_L16.Clone();
                            Array.Copy(image_mark_byte, image_mark_sharpen_byte, image_mark_byte.Length);
                            unsafe
                            {
                                fixed (byte* mark_byte = image_mark_byte)
                                {
                                    pbpvc.setSharpen_vc(mark_byte, 16, (ushort)image_mark_L16.Width, (ushort)image_mark_L16.Height);
                                }
                            }

                            image_mark_L16 = util.ConvertByteArrayToL16Image(image_mark_byte, (ushort)image_mark_L16.Width, (ushort)image_mark_L16.Height, 1);

                        }

                    }
                    else
                    {
                        if (image_mark_sharpen_L16 != null) 
                        {
                            var _mark_L16 = image_mark_L16.Clone();
                            byte[] _mark_byte = new byte[image_mark_sharpen_byte.Length];
                            image_mark_sharpen_byte.CopyTo(_mark_byte, 0);
                            image_mark_L16 = image_mark_sharpen_L16.Clone();
                            image_mark_byte.CopyTo(image_mark_sharpen_byte, 0);
                            image_mark_sharpen_L16 = _mark_L16;

                            image_mark_sharpen_byte = _mark_byte;
                        }
                        else
                        {
                            fix = false; 
                        }
                       
                    }

                    if (fix && isUpdateAlg) 
                    {
                        queueAlgAttribute.Enqueue(algAttribute);
                    }

                }
            }
        }
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

                    if (algAttribute.scientificON) 
                    {
                        if (imagePaletteForm.tb_max != null) 
                        {
                            imagePaletteForm.tb_max.Text = algAttribute.colorValue.ToString("E");
                        }
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
                    if (algAttribute.scientificON)
                    {
                        if (imagePaletteForm.tb_min != null)
                        {
                            imagePaletteForm.tb_min.Text = algAttribute.colorMinValue.ToString("E");
                        }
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
            imagePanel = new BioanalyImagePanel();
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

            isUpdateAlg = false;
            this.pl_right = _pl_right;
          

            imagePaletteForm = new BioanayImagePaletteForm();
            imagePaletteForm.TopLevel = false;
            imagePaletteForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_right.Controls.Add(imagePaletteForm);
            imagePaletteForm.BringToFront();
            imagePaletteForm.Show();


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
            foreach (var item in bioanalysisMannages)
            {
                if (item.Value.ImageIndex > ImageIndex) 
                {
                    ImageIndex = item.Value.ImageIndex;
                }
            }
            ImageIndex++;
            imagePanel.lb_imageIndex.Text = ImageIndex.ToString();
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
            imagePaletteForm.nud_colorMin.Minimum = 0;
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


            imagePaletteForm.cb_sharpen.CheckedChanged += Cb_sharpen_CheckedChanged;
            imagePanel.cb_scientific.CheckedChanged += Cb_scientific_CheckedChanged;
            imagePaletteForm.cb_scientific.CheckedChanged += Cb_imagepalette_scientific_CheckedChanged;
            imagePanel.image_pl.MouseDown += Image_pl_MouseDown;
            imagePanel.image_pl.DoubleClick += Image_pl_DoubleClick;
            imagePanel.image_pl.MouseMove += Image_pl_MouseMove;
            imagePanel.image_pl.MouseUp += Image_pl_MouseUp;
            imagePanel.image_pl.Paint += Image_pl_Paint;
            imagePanel.ctms_strop_delete.Click += Ctms_strop_delete_Click;
            imagePanel.ctms_strop_copy.Click += Ctms_strop_copy_Click;
            imagePanel.ctms_strop_stickup.Click += Ctms_strop_stickup_Click;

            imagePaletteForm.hpb_line.Click += Hpb_line_Click;

            imagePanel.wdb_title.MouseDown += Wdb_title_Click;
            imagePanel.FormClosing += ImagePanel_FormClosing;
            imagePanel.FormClosed += ImagePanel_FormClosed;
            imagePanel.ava_saveReport.Click += Ava_saveReport_Click;
            imagePaletteForm.hpb_rect.Click += hpb_rect_Click;
            imagePaletteForm.hpb_circe.Click += Hpb_circe_Click;
            imagePaletteForm.hpb_xianduan.Click += Hpb_xianduan_Click;
            imagePaletteForm.hpb_wand.Click += Hpb_wand_Click;
            imagePaletteForm.fb_fixSetting.Click += Fb_fixSetting_Click;
            imagePaletteForm.cb_continuous.CheckedChanged += Cb_continuous_CheckedChanged;

            imagePaletteForm.ava_textbox.Click += Ava_textbox_Click;
            imagePaletteForm.dtb_textbox.TextChanged += Dtb_textbox_TextChanged;

            KeyboardListener.Register(OnKeyPressed); // 创建键盘钩子
        }

       

        private bool ReadTif() 
        {
            try
            {
                string[] tifFiles = Directory.GetFiles(path, "*.tif", SearchOption.TopDirectoryOnly);
                foreach (string tifFile in tifFiles)
                {
                    if (tifFile.ToUpper().Contains("MARKER"))
                    {
                        tif_marker_path = tifFile;
                        curImagePath = tif_marker_path;
                        image_mark_L16 = util.LoadTiffAsL16(tif_marker_path);
                        image_mark_byte = util.ConvertL16ImageToByteArray(image_mark_L16);
                        byte[] bytes = new byte[image_mark_byte.Length];
                        unsafe
                        {
                            fixed (byte* p = image_mark_byte) 
                            {
                                fixed (byte* p1 = bytes) 
                                {
                                    algAttribute.pixel_size = pbpvc.distortion_correction_vc(p, 16, (ushort)image_mark_L16.Width, (ushort)image_mark_L16.Height, p1);
                                }
                            }
                            image_mark_byte = bytes;
                            image_mark_L16 = util.ConvertByteArrayToL16Image(image_mark_byte, image_mark_L16.Width, image_mark_L16.Height, 1);
                        }
                    }
                    else
                    {
                        tif_org_path = tifFile;
                        image_org_L16 = util.LoadTiffAsL16(tif_org_path);
                        image_org_byte = util.ConvertL16ImageToByteArray(image_org_L16);
                    }
                }
                if (image_mark_L16 == null) 
                {
                    MessageBox.Show("样品图加载不正确,请重新加载.....");
                    return false;
                }
                if (image_org_L16 == null) 
                {
                    MessageBox.Show("缺少伪彩图 请检查文件夹中是否存在伪彩图");
                    return false;
                }
                imagePanel.SetButtomLabel($"{image_mark_L16.Width} x {image_mark_L16.Height}");
                if (tifFiles.Length > 0)
                {
                    var t = tifFiles[0].Split("\\");
                    if (t.Length > 2)
                    {
                        imagePanel.SetButtomName($"{t[t.Length - 2]} {image_mark_L16.Width} x {image_mark_L16.Height}");
                    }

                }
                return true;
            }
            catch (Exception)
            {

                MessageBox.Show("图片格式加载不正确");
                return false;
            }
            
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
                            {
                                imagePanel.SetImage(image_mark_and_org_rgb24, colorbar_rgb24_image);
                            }
                                
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
                        {
                            imagePanel.SetImage(image_mark_and_org_rgb24, colorbar_rgb24_image);
                        } 
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
                imagePanel.lb_top_info.Text = "Trans-fluorescence";
                imagePanel.lb_wh.Text = "Radiance (p/sec/cm²/sr)\n color scale\n min=" + util.GetscientificNotation(algAttribute.colorMinValue) + "\n max=" + util.GetscientificNotation(algAttribute.colorValue);
            }
            else
            {
                imagePanel.lb_top_info.Text = "";
                imagePanel.lb_wh.Text = "color scale\n min=" + algAttribute.colorMinValue.ToString() + "\n max=" + algAttribute.colorValue.ToString();
            }

        }

        private bool IsPointInRectangles(System.Drawing.Point point, List<TextBoxInfo> rectangles, out Corner cner, out TextBoxInfo curRect, out int index)
        {
            curRect = new TextBoxInfo();
            cner = Corner.None;
            index = 0;
            foreach (var rect in rectangles)
            {

                System.Drawing.Point topLeft = new System.Drawing.Point(rect.rect.Left, rect.rect.Top);
                System.Drawing.Point topRight = new System.Drawing.Point(rect.rect.Right, rect.rect.Top);
                System.Drawing.Point bottomLeft = new System.Drawing.Point(rect.rect.Left, rect.rect.Bottom);
                System.Drawing.Point bottomRight = new System.Drawing.Point(rect.rect.Right, rect.rect.Bottom);

                if (ImageProcess.IsNearCorner(point, new System.Drawing.Point(rect.rect.Left, rect.rect.Top), CircleRadius))
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

        private bool IsPointInPolygon(System.Drawing.Point testPoint, List<PolygonAndInfo> polygon, out Corner cner, out int index)
        {
            cner = Corner.None;
            index = 0;
            foreach (PolygonAndInfo polygonAndInfo in polygon) 
            {
                var points = polygonAndInfo.points;
                bool result = false;
                int j = points.Count - 1; // The last vertex is the 'previous' one to the first

                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].Y < testPoint.Y && points[j].Y >= testPoint.Y || points[j].Y < testPoint.Y && points[i].Y >= testPoint.Y)
                    {
                        if (points[i].X + (testPoint.Y - points[i].Y) / (points[j].Y - points[i].Y) * (points[j].X - points[i].X) < testPoint.X)
                        {
                         
                            imagePanel.image_pl.Cursor = Cursors.Hand;
                            cner = Corner.drawMouse;
                            result = !result;
                            return true;
                        }
                    }
                    j = i; // j is previous vertex to i
                }
                index++;
            }

           

            return false;
        }

        private unsafe CirceAndInfo UpdateCireInfo(float _max,float _min, CirceAndInfo rab,int radius,int _cirDragStartIndex=-1) 
        {
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

            if (CircleAndInfoList.Count > 0 && _cirDragStartIndex < CircleAndInfoList.Count && _cirDragStartIndex != -1) 
            {
                
                CircleAndInfoList[_cirDragStartIndex] = rab;
            }
            
        
            cirDragStartIndex = -1;
            
            drawCircle = false;
            isCirDragging = false;
            if (!isContinuous)
                CircleOn = false;
            imagePanel.image_pl.Invalidate();

            return rab;
        }


        public unsafe RectAttribute UpdateRectInfo(float _max, float _min, RectAttribute rab, int _cirDragStartIndex = -1) 
        {
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

            if (rectangles.Count > 0 && _cirDragStartIndex < rectangles.Count && _cirDragStartIndex != -1)
            {

                rectangles[_cirDragStartIndex] = rab;
            }

         
            isRecDragging = false;
            rectActiveCorner = Corner.None;
            rectDragStartIndex = -1;
            imagePanel.image_pl.Invalidate();
            currentRectangle = null;
            drawRect = false;
            imagePanel.image_pl.Invalidate();
            return rab;
        }
        #endregion


        #region 事件
        private void Wdb_title_Click(object sender, EventArgs e)
        {
            if (Arrangement == 2 || Arrangement == 0) 
            {
                this.pl_right.Controls.Clear();
                this.pl_right.Controls.Add(this.imagePaletteForm);
            }
                
            foreach (var item in bioanalysisMannages)
            {
                item.Value.IsActive = false;
            }
            IsActive = true;
            this.imagePanel.BringToFront();
        }
        private void Cb_sharpen_CheckedChanged(object sender, BoolEventArgs e)
        {
            if (Arrangement == 2)
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.Sharpen  = imagePaletteForm.cb_sharpen.Checked;
                   
                }
            }
            else
            {
                Sharpen = imagePaletteForm.cb_sharpen.Checked;
            }
        }
        private void Cb_scientific_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            if (imagePaletteForm.cb_scientific.Checked != imagePanel.cb_scientific.Checked) 
            {
                imagePaletteForm.cb_scientific.Checked = imagePanel.cb_scientific.Checked;
            }
            if (Arrangement == 2) 
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.ScientificON = imagePanel.cb_scientific.Checked;
                    item.Value.imagePaletteForm.RefreshscientificON(ScientificON);
                }
            }
            else
            {
                ScientificON = imagePanel.cb_scientific.Checked;
                imagePaletteForm.RefreshscientificON(ScientificON);
            }
           
        }
        private void Cb_imagepalette_scientific_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            imagePanel.cb_scientific.Checked = imagePaletteForm.cb_scientific.Checked;
        }

        private void Cbb_mode_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(imagePanel.cbb_mode.Text == "mark") 
            {
                imagePaletteForm.cb_colortable.SelectedIndex = 7;
            }
            RefreshCbb();


        }
        private void Dtb_brightness_ValueChanged()
        {
            if (Arrangement == 2) 
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.Brightness = imagePaletteForm.dtb_brightness.Value;
                }
            }
            else
            {
                Brightness = imagePaletteForm.dtb_brightness.Value;
            }
           
        }
        private void Dtb_opacity_ValueChanged()
        {
            if (Arrangement == 2)
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.Opacity = imagePaletteForm.dtb_opacity.Value;
                }
            }
            else
            {
                Opacity = imagePaletteForm.dtb_opacity.Value;
            }
           
        }
        private void Nud_opacity_ValueChanged(object sender, System.EventArgs e)
        {
            if (Arrangement == 2)
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.Opacity = (int)imagePaletteForm.nud_opacity.Value;
                }
            }
            else
            {
                Opacity = (int)imagePaletteForm.nud_opacity.Value;
            }
            
        }

        private void Nud_brightness_ValueChanged(object sender, System.EventArgs e)
        {
            if (Arrangement == 2)
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.Brightness = (int)imagePaletteForm.nud_brightness.Value;
                }
            }
            else
            {
                Brightness = (int)imagePaletteForm.nud_brightness.Value;
            }
            
        }
        private void Nud_colorMin_ValueChanged(object sender, System.EventArgs e)
        {
            if (Arrangement == 2)
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.ColorMin = (int)imagePaletteForm.nud_colorMin.Value;
                }
            }
            else
            {
                ColorMin = (int)imagePaletteForm.nud_colorMin.Value;
            }
            
        }

        private void Nud_colorMax_ValueChanged(object sender, System.EventArgs e)
        {
            if (Arrangement == 2)
            {
                foreach (var item in bioanalysisMannages)
                { 
                    item.Value.ColorMax = (int)imagePaletteForm.nud_colorMax.Value;
                }
            }
            else
            {
                ColorMax = (int)imagePaletteForm.nud_colorMax.Value;
            }
            
        }

        private void Dtb_colorMin_ValueChanged()
        {
            if (Arrangement == 2)
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.ColorMin = (int)imagePaletteForm.dtb_colorMin.Value;
                }
            }
            else
            {
                ColorMin = (int)imagePaletteForm.dtb_colorMin.Value;
            }
            
        }

        private void Dtb_colorMax_ValueChanged()
        {
            if (Arrangement == 2)
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.ColorMax = (int)imagePaletteForm.dtb_colorMax.Value;
                }
            }
            else
            {
                ColorMax = (int)imagePaletteForm.dtb_colorMax.Value;
            }
            
        }
        private void Cb_colortable_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (Arrangement == 2)
            {
                foreach (var item in bioanalysisMannages)
                {
                    item.Value.ColorIndex = imagePaletteForm.cb_colortable.SelectedIndex;
                }
            }
            else
            {
                ColorIndex = imagePaletteForm.cb_colortable.SelectedIndex;
            }
           
        }


        #region imagepanel
        private void Image_pl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;


            // 绘制直线
            if ((startPoint != System.Drawing.Point.Empty && endPoint != System.Drawing.Point.Empty))
            {
                if (Arrangement == 2) 
                {
                    foreach (var item in bioanalysisMannages)
                    {
                        if (item.Value.path != path) 
                        {
                            item.Value.SetLine(startPoint, endPoint);
                            item.Value.GetImage.Invalidate();
                        }
                    }
                }
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

            if (drawRect|| drawTextBox) 
            {
                if (currentRectangle.HasValue)
                {
                    var r = ImageProcess.ConvertRealRectangleToPictureBox(currentRectangle.Value, imagePanel.image_pl);
                    e.Graphics.DrawRectangle(Pens.Red, r);
                }
            }

            foreach (var rect in wandRectangle) 
            {
                System.Drawing.Rectangle p = rect.rect;
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


                        labelText = $"ROI:{index + 1},AOD:{util.GetscientificNotation(rect.pdinfovc.AOD)},IOD:{util.GetscientificNotation(rect.pdinfovc.IOD)}," +
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
                    foreach (var item1 in PolygonAndInfoList)
                    {
                        isStart = 0;
                        foreach (var item in item1.points)
                        {
                            if (isStart == 0)
                            {
                                point = ImageProcess.ConvertRealToPictureBox(item, imagePanel.image_pl);
                            }
                            System.Drawing.Point curpoint = ImageProcess.ConvertRealToPictureBox(item, imagePanel.image_pl);
                            ImageProcess.DrawCircle(g, curpoint, CircleRadius, Pens.Blue, Brushes.LightBlue);
                            g.DrawLine(Pens.Red, curpoint, point);
                            point = curpoint;
                            isStart++;
                        }
                      
                        if (item1.pdinfovc != null)
                        {

                            string labelText = "";
                            if (algAttribute.scientificON)
                            {


                                labelText = $"ROI:{index + 1},AOD:{util.GetscientificNotation(item1.pdinfovc.AOD)},IOD:{util.GetscientificNotation(item1.pdinfovc.IOD)}," +
                                           $"\r\nmaxOD:{util.GetscientificNotation(item1.pdinfovc.maxOD)},minOD:{util.GetscientificNotation(item1.pdinfovc.minOD)},Count:{util.GetscientificNotation(item1.pdinfovc.Count)}";
                            }
                            else
                            {
                                labelText = $"ROI:{index + 1},AOD:{item1.pdinfovc.AOD},IOD:{item1.pdinfovc.IOD}," +
                                           $"\r\nmaxOD:{item1.pdinfovc.maxOD},minOD:{item1.pdinfovc.minOD},Count:{item1.pdinfovc.Count}"; // 标签编号
                            }
                            Font font = new Font("Arial", 8); // 字体
                            Brush brush = Brushes.Red; // 字体颜色
                            System.Drawing.Point curpoint = ImageProcess.ConvertRealToPictureBox(item1.points[0], imagePanel.image_pl);
                            g.DrawString(labelText, font, brush, curpoint.X - 10, curpoint.Y - 15);
                        }

                    }
                  
                }
                index++;
            }
            index = 0;
            foreach (var textBoxInfo in textBoxInfos)
            {
                System.Drawing.Rectangle p = textBoxInfo.rect;
                if (isTextBoxRecDragging)
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

                    ImageProcess.DrawCircle(g, new System.Drawing.Point(item.X, item.Y), 2, Pens.Blue, Brushes.LightBlue);
                }

                // 居中显示值
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                Font customFont = new Font("Arial", 12, FontStyle.Bold);
                g.DrawString(textBoxInfo.value, customFont, Brushes.Red, r, sf);
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
                    value = value * algAttribute.pixel_size;
                    value += 4;
                    imagePaletteForm.flb_act_mm.Text = value.ToString() + " mm";
                    imagePaletteForm.flb_act_mm.Refresh();
                }
                else if (drawTextBox) 
                {
                   
                    if (currentRectangle.HasValue)
                    {
                        TextBoxInfo textBoxInfo = new TextBoxInfo();
                        imagePaletteForm.dtb_textbox.Text = "info";
                        textBoxInfo.value = "info";
                        textBoxInfo.rect = currentRectangle.Value;

                        if (textBoxInfos.Count == 0)
                        {
                            textBoxInfo.index = 0;
                        }
                        else if (textBoxInfos.Count == 1)
                        {
                            if (textBoxInfos[0].index == 0)
                            {
                                textBoxInfo.index = 1;
                            }
                            else
                            {
                                textBoxInfo.index = 0;
                            }
                        }
                        else
                        {
                            List<int> ints = new List<int>();
                            foreach (var i in textBoxInfos)
                            {
                                ints.Add(i.index);
                            }
                            ints.Sort();

                            Random rand = new Random();
                            int potentialInt;

                            
                            int lowerBound = 0; 
                            int upperBound = 65535;
                           
                            while (true)
                            {
                                potentialInt = rand.Next(lowerBound, upperBound + 1);
                                if (ints.Contains(potentialInt) == false)
                                {
                                    textBoxInfo.index = potentialInt;
                                    break;
                                }
                            }
                            

                        }

                        // 完成绘制并保存矩形
                        if (Arrangement == 2)
                        {
                            foreach (var item in bioanalysisMannages)
                            {

                                item.Value.textBoxInfos.Add(textBoxInfo);
                                item.Value.curTextBox = textBoxInfos.Count - 1;
                                item.Value.drawTextBox = false;
                                item.Value.currentRectangle = null;
                                item.Value.imagePanel.image_pl.Invalidate();
                            }
                        }
                        else
                        {

                            textBoxInfos.Add(textBoxInfo);
                            curTextBox = textBoxInfos.Count - 1;
                            drawTextBox = false;
                            currentRectangle = null;
                            imagePanel.image_pl.Invalidate();
                            
                          
                        }
                        imagePaletteForm.SetInfo = "w:" + textBoxInfo.rect.Width.ToString() + "h:" + textBoxInfo.rect.Height.ToString();

                    }

                    drawTextBox = false;
                    if (!isContinuous)
                        TextboxOn = false;
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


                        // 完成绘制并保存矩形
                        if (Arrangement == 2)
                        {
                            foreach (var item in bioanalysisMannages)
                            {
                                item.Value.currentRectangle = currentRectangle;
                                rab = item.Value.UpdateRectInfo(_max, _min, rab);
                                item.Value.rectangles.Add(rab);
                                

                            }
                        }
                        else
                        {
                            rab = UpdateRectInfo(_max,_min,rab);


                            rectangles.Add(rab);
                           
                        }
                        imagePaletteForm.SetInfo = "w:" + rab.rect.Width.ToString() + "h:" + rab.rect.Height.ToString();

                       
                    }

                    drawRect = false;
                    if (!isContinuous)
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

                    // 完成绘制并保存矩形
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            rab = item.Value.UpdateCireInfo(_max, _min, rab, radius, cirDragStartIndex);
                            item.Value.CircleAndInfoList.Add(rab);
                        }
                    }
                    else
                    {
                        rab = UpdateCireInfo(_max, _min, rab, radius, cirDragStartIndex);
                        CircleAndInfoList.Add(rab);
                    }



                    imagePanel.image_pl.Invalidate();


                }
                else if (isTextBoxRecDragging)
                {
                    TextBoxInfo rattb = new TextBoxInfo();
                    rattb.rect = recDragRect;
                    rattb.value = textBoxInfos[rectDragStartIndex].value;
                    rattb.index = textBoxInfos[rectDragStartIndex].index;


                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            for (int i = 0; i < item.Value.textBoxInfos.Count; i++)
                            {
                                if (item.Value.textBoxInfos[i].index == rattb.index) 
                                {
                                    item.Value.textBoxInfos[i] = rattb;
                                    item.Value.isTextBoxRecDragging = false;
                                    item.Value.rectActiveCorner = Corner.None;
                                    item.Value.rectDragStartIndex = -1;
                                    item.Value.imagePanel.image_pl.Invalidate();
                                    break;
                                }
                            }
                           
                        }
                    }
                    else
                    {
                        textBoxInfos[rectDragStartIndex] = rattb;
                        isTextBoxRecDragging = false;
                        rectActiveCorner = Corner.None;
                        rectDragStartIndex = -1;
                        imagePanel.image_pl.Invalidate();
                    }

                    imagePaletteForm.SetInfo = "w:" + recDragRect.Width.ToString() + "h:" + recDragRect.Height.ToString();


                    
                    
                }
                else if (isRecDragging)
                {
                    RectAttribute rattb = new RectAttribute();
                    rattb.rect = recDragRect;
                    currentRectangle = recDragRect;
                    // 计算光子数并展示出来
                    float _max = algAttribute.colorValue;
                    float _min = algAttribute.colorMinValue;
                    Pseudo_infoVC curpdinfovc = null;


                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            item.Value.currentRectangle = currentRectangle;
                            rattb = item.Value.UpdateRectInfo(_max, _min, rattb, rectDragStartIndex);
                           
                           
                        }
                    }
                    else
                    {
                        rattb = UpdateRectInfo(_max, _min, rattb, rectDragStartIndex);
                       
                    }

                    imagePaletteForm.SetInfo = "w:" + recDragRect.Width.ToString() + "h:" + recDragRect.Height.ToString();


                    

                }
                else if (isCirDragging)
                {
                    CirceAndInfo circeAndInfo = new CirceAndInfo();

                    circeAndInfo.Radius = circleRadio;
                    circeAndInfo.center = circleCenter;

                    float _max = algAttribute.colorValue;
                    float _min = algAttribute.colorMinValue;
                    int radius = (int)Math.Sqrt(Math.Pow(circeAndInfo.center.X - circeAndInfo.Radius.X, 2) + Math.Pow(circeAndInfo.center.Y - circeAndInfo.Radius.Y, 2));


                    imagePaletteForm.SetInfo = "radio:" + radius.ToString();
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            item.Value.UpdateCireInfo(_max, _min, circeAndInfo, radius, cirDragStartIndex);
                          
                        }
                    }
                    else
                    {
                        UpdateCireInfo(_max, _min, circeAndInfo, radius, cirDragStartIndex);
                       
                        imagePanel.image_pl.Invalidate();
                    }


                    
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
            else if (drawTextBox && e.Button == MouseButtons.Left) 
            {
                // 动态调整矩形大小
                int x = Math.Min(leftTopPoint.X, readLoction.X);
                int y = Math.Min(leftTopPoint.Y, readLoction.Y);
                int width = Math.Abs(readLoction.X - leftTopPoint.X);
                int height = Math.Abs(readLoction.Y - leftTopPoint.Y);
                imagePaletteForm.SetInfo = "w:" + width + "h:" + height;
                currentRectangle = new System.Drawing.Rectangle(x, y, width, height);
                imagePanel.image_pl.Invalidate(); // 触发重绘
            }
            else if (drawRect && e.Button == MouseButtons.Left)
            {
                // 动态调整矩形大小
                int x = Math.Min(leftTopPoint.X, readLoction.X);
                int y = Math.Min(leftTopPoint.Y, readLoction.Y);
                int width = Math.Abs(readLoction.X - leftTopPoint.X);
                int height = Math.Abs(readLoction.Y - leftTopPoint.Y);
                imagePaletteForm.SetInfo = "w:" + width + "h:" + height;
                currentRectangle = new System.Drawing.Rectangle(x, y, width, height);
                imagePanel.image_pl.Invalidate(); // 触发重绘
            }
            else if (drawCircle && e.Button == MouseButtons.Left)
            {
                circleRadio = readLoction;
                int radius = (int)Math.Sqrt(Math.Pow(circleCenter.X - circleRadio.X, 2)
                    + Math.Pow(circleCenter.Y - circleRadio.Y, 2));
                imagePaletteForm.SetInfo = "radio:" + radius.ToString();
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
            else if (isTextBoxRecDragging)
            {
                recDragRect = curTextBoxinfo.rect;
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
                imagePaletteForm.SetInfo = "w:" + recDragRect.Width + "h:" + recDragRect.Height;
                imagePanel.image_pl.Invalidate(); // 触发重绘
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
                imagePaletteForm.SetInfo = "w:" + recDragRect.Width + "h:" + recDragRect.Height;
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
                int radius = (int)Math.Sqrt(Math.Pow(circleCenter.X - circleRadio.X, 2)
                    + Math.Pow(circleCenter.Y - circleRadio.Y, 2));
                imagePaletteForm.SetInfo = "radio:" + radius.ToString();
                imagePanel.image_pl.Invalidate();


            }
            else if (IsPointInRectangles(readLoction, textBoxInfos, out var cner3, out var cr3, out var index3)) // 遍历是否在所有矩形或者角点附近
            {

            }
            else if (IsPointInRectangles(readLoction, rectangles, out var cner, out var cr, out var index)) // 遍历是否在所有矩形或者角点附近
            {

            }
            else if (IsPointInRectangles(readLoction, wandRectangle, out var cner2, out var cr2, out var index2)) // 遍历是否在所有矩形或者角点附近
            {

            }
            else if (IsPointInCircle(readLoction, CircleAndInfoList, out var cner1, out var curRect, out var index1))
            {

            }
            if (drawpolygon == false && IsPointInPolygon(readLoction, PolygonAndInfoList, out var cr5, out var index5)) 
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
                    if (value <= 10)
                    {
                       // PolygonAndInfoList.Clear();
                        lastPoint = firstPoint;
                        drawpolygon = false;
                        linepolygonON = false;
                        imagePanel.image_pl.Invalidate();

                        // 计算光子量

                        float _max = algAttribute.colorValue;
                        float _min = algAttribute.colorMinValue;
                        List<Point_VC> curVclist = new List<Point_VC>();
                        
                        foreach (var item in curPolygonAndInfoList.points)
                        {
                            Point_VC pvc = new Point_VC(item.X, item.Y);
                            curVclist.Add(pvc);

                        }
                        
                        if (Arrangement == 2)
                        {
                            foreach (var item in bioanalysisMannages)
                            {
                               // item.Value.PolygonAndInfoList.Clear();
                                item.Value.curPolygonAndInfoList.pdinfovc = null;
                                Pseudo_infoVC curpdinfovc = null;
                                unsafe
                                {
                                    fixed (byte* pseu_16_byte_src = item.Value.image_org_byte)
                                    {
                                        curpdinfovc = item.Value.pbpvc.get_pseudo_info_polygon_vc(pseu_16_byte_src, 16,
                                            (ushort)image_org_L16.Width, (ushort)image_org_L16.Height, _max, _min, curVclist);

                                    }
                                }
                                item.Value.curPolygonAndInfoList.points = curPolygonAndInfoList.points;
                                item.Value.curPolygonAndInfoList.pdinfovc = curpdinfovc;
                                PolygonAndInfo pai = new PolygonAndInfo();
                                pai.points = new List<System.Drawing.Point>();
                                foreach (var ad in item.Value.curPolygonAndInfoList.points)
                                {
                                    pai.points.Add(ad);
                                }
                                pai.pdinfovc = new Pseudo_infoVC(item.Value.curPolygonAndInfoList.pdinfovc.maxOD, item.Value.curPolygonAndInfoList.pdinfovc.minOD, item.Value.curPolygonAndInfoList.pdinfovc.IOD,
                                    item.Value.curPolygonAndInfoList.pdinfovc.Count, item.Value.curPolygonAndInfoList.pdinfovc.AOD);
                                item.Value.PolygonAndInfoList.Add(pai);
                                item.Value.imagePanel.image_pl.Invalidate();
                            }
                        }
                        else
                        {
                            Pseudo_infoVC curpdinfovc = null;
                            unsafe
                            {
                                fixed (byte* pseu_16_byte_src = image_org_byte)
                                {
                                    curpdinfovc = pbpvc.get_pseudo_info_polygon_vc(pseu_16_byte_src, 16,
                                        (ushort)image_org_L16.Width, (ushort)image_org_L16.Height, _max, _min, curVclist);

                                }
                            }
                            curPolygonAndInfoList.pdinfovc = curpdinfovc;
                            PolygonAndInfo pai = new PolygonAndInfo();
                            pai.points = new List<System.Drawing.Point>();
                            foreach (var ad in curPolygonAndInfoList.points)
                            {
                                pai.points.Add(ad);
                            }
                            pai.pdinfovc = new Pseudo_infoVC(curPolygonAndInfoList.pdinfovc.maxOD,curPolygonAndInfoList.pdinfovc.minOD,curPolygonAndInfoList.pdinfovc.IOD,
                                curPolygonAndInfoList.pdinfovc.Count,curPolygonAndInfoList.pdinfovc.AOD);
                            PolygonAndInfoList.Add(pai);
                            imagePanel.image_pl.Invalidate();
                        }
                        

                    }

                    
                }
            }
        }

        private void Image_pl_MouseDown(object sender, MouseEventArgs e)
        {
           
            curTmpDownShape = ShapeForm.None;
            curTextBox = -1;
            Wdb_title_Click(null, null);
            System.Drawing.Point readLoction = ImageProcess.ConvertPictureBoxToReal( e.Location, imagePanel.image_pl);
            if (e.Button == MouseButtons.Left)
            {
                curTmpDownShapePoint = readLoction;
                if (IsPointInRectangles(readLoction, textBoxInfos, out var cner3, out var cr3, out var index3)) // 遍历是否在所有矩形或者角点附近
                {
                    rectActiveCorner = cner3;

                    if (rectActiveCorner != Corner.None)
                    {
                        curTmpDownShape = ShapeForm.TextBoxRect;
                        curTextBox = index3;
                        isTextBoxRecDragging = true;
                        recDragStart = readLoction;
                        curTextBoxinfo = cr3;
                        rectDragStartIndex = index3;
                    }


                }
                else if (IsPointInRectangles(readLoction, rectangles, out var cner, out var cr, out var index))
                {
                    rectActiveCorner = cner;

                    if (rectActiveCorner != Corner.None)
                    {
                        curTmpDownShape = ShapeForm.Rect;
                        curTmpDownShapeIndex = index;
                        isRecDragging = true;
                        recDragStart = readLoction;
                        rectOriginalRect = cr;
                        rectDragStartIndex = index;
                    }
                }
                else if (IsPointInCircle(readLoction, CircleAndInfoList, out var cner1, out var curRect, out var index1))
                {
                    rectActiveCorner = cner1;
                    if (rectActiveCorner != Corner.None)
                    {
                        curTmpDownShape = ShapeForm.Circle;
                        curTmpDownShapeIndex = index;
                        isCirDragging = true;
                        cirDragStart = readLoction;
                        cireOriginalCire = curRect;
                        circleCenter = curRect.center;
                        circleRadio = curRect.Radius;
                        cirDragStartIndex = index1;
                    }

                }
                else if (TextboxOn)
                {
                    // 开始绘制新矩形
                    drawTextBox = true;
                    leftTopPoint = readLoction;
                    currentRectangle = new System.Drawing.Rectangle(readLoction.X, readLoction.Y, 0, 0);

                }
                else if (iswandON)
                {
                    // 魔术棒功能
                    float _max = algAttribute.colorValue;
                    float _min = algAttribute.colorMinValue;
                    Pseudo_infoVC curpdinfovc = null;
                    byte[] dd = new byte[image_org_L16.Height * image_org_L16.Width * 2];
                    int dst_x = 0, dst_y = 0, dst_w = 0, dst_h = 0;
                    int th = int.Parse(imagePaletteForm.dtb_th.Text.ToString());
                    unsafe
                    {

                        fixed (byte* pseu_16_byte_src = image_org_byte)
                        {
                            fixed (byte* dda = dd)
                            {
                                curpdinfovc = pbpvc.get_pseudo_info_wand_vc(pseu_16_byte_src, dda, 16, (ushort)image_org_L16.Width, (ushort)image_org_L16.Height,
                                _max, _min, readLoction.X, readLoction.Y, th, ref dst_w, ref dst_h, ref dst_x, ref dst_y);

                            }


                        }

                    }
                    if (curpdinfovc.Count > 0)
                    {
                       //wandRectangle.Clear();
                        RectAttribute attribute = new RectAttribute();
                        attribute.rect = new System.Drawing.Rectangle(dst_x, dst_y, dst_w, dst_h);
                        attribute.pdinfovc = curpdinfovc;
                        wandRectangle.Add(attribute);
                        imagePanel.image_pl.Invalidate();
                        iswandON = false;
                    }

                }
                else if (lineOn)
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
                else if (drawpolygon) 
                {
                    
                    if (curPolygonAndInfoList.points == null)
                    {
                        curPolygonAndInfoList.points = new List<System.Drawing.Point>();
                    }
                    System.Drawing.Point curPoint = readLoction;
                    curPolygonAndInfoList.points.Add(curPoint);
                }
                else if (linepolygonON)
                {
                    drawpolygon = true;
                    if (curPolygonAndInfoList.points == null)
                    {
                        curPolygonAndInfoList.points = new List<System.Drawing.Point>();
                    }
                    else
                    {
                        curPolygonAndInfoList.points.Clear();
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
                
               
            }
            else if (e.Button == MouseButtons.Right)
            {
                imagePanel.ctms_strop.Enabled = true;
                imagePanel.ctms_strop_copy.Enabled = false;
                imagePanel.ctms_strop_delete.Enabled = false;
                imagePanel.ctms_strop_stickup.Enabled = curIsCopy;
                if (IsPointInRectangles(readLoction, textBoxInfos, out var cner3, out var cr3, out var index3)) // 遍历是否在所有矩形或者角点附近
                {
                    curShape = ShapeForm.TextBoxRect;
                    imagePaletteForm.dtb_textbox.Text = cr3.value;
                    curShapeIndex = index3;
                    imagePanel.ctms_strop_delete.Enabled = true;
                }
                else if (IsPointInRectangles(readLoction, wandRectangle, out var cner4, out var cr4, out var index4))
                {
                    curShape = ShapeForm.WandRect;
                    curShapeIndex = index4;
                    imagePanel.ctms_strop_delete.Enabled = true;

                }
                else if (ImageProcess.IsPointOnLine(readLoction,startPoint,endPoint,CircleRadius))
                {
                    curShape = ShapeForm.Line;
                    imagePanel.ctms_strop_delete.Enabled = true;

                }
                else if (IsPointInCircle(readLoction, CircleAndInfoList, out var cner1, out var curRect, out var index1))
                {
                    curShape = ShapeForm.Circle;
                    curShapeIndex = index1;
                    imagePanel.ctms_strop_copy.Enabled = true;
                    imagePanel.ctms_strop_delete.Enabled = true;
                }
                else if (IsPointInRectangles(readLoction, rectangles, out var cner, out var cr, out var index))
                {
                    curShape = ShapeForm.Rect;
                    curShapeIndex = index;
                    imagePanel.ctms_strop_copy.Enabled = true;
                    imagePanel.ctms_strop_delete.Enabled = true;
                }
                else if (IsPointInRectangles(readLoction, rectangles, out var cner2, out var cr2, out var index2))
                {
                    curShape = ShapeForm.WandRect;
                    curShapeIndex = index2;
                    imagePanel.ctms_strop_copy.Enabled = false;
                    imagePanel.ctms_strop_delete.Enabled = false;
                }
                else if (drawpolygon==false && IsPointInPolygon(readLoction, PolygonAndInfoList, out var cr5, out var index5))
                {
                    curShapeIndex = index5;

                    curShape = ShapeForm.Polygon;
                    imagePanel.ctms_strop_delete.Enabled = true;

                }
                curShapePoint = readLoction;
            }
            
        }
        private void Ctms_strop_copy_Click(object sender, EventArgs e)
        {
            // 复制矩形 目前只允许复制矩形和圆形
            switch (curShape)
            {
                case ShapeForm.None:
                    break;
                case ShapeForm.Line:
                    break;
                case ShapeForm.Polygon:
                    oldPolygonAndInfoList = PolygonAndInfoList[curShapeIndex];
                    curIsCopy = true;
                    break;
                case ShapeForm.Rect:
                    oldCopyRect = rectangles[curShapeIndex];
                    curIsCopy = true;
                    break;
                case ShapeForm.Circle:
                    oldCopyCircle = CircleAndInfoList[curShapeIndex];
                    curIsCopy = true;
                    break;
                default:
                    break;
            }
        }
        private void Ctms_strop_stickup_Click(object sender, EventArgs e)
        {
            switch (curShape)
            {
                case ShapeForm.None:
                    break;
                case ShapeForm.Line:
                    break;
                case ShapeForm.Polygon:
                    if (Arrangement == 2) 
                    {
                        foreach (var bms in bioanalysisMannages) 
                        {
                            PolygonAndInfo polygonAndInfo = new PolygonAndInfo();
                            polygonAndInfo.points = new List<System.Drawing.Point>();
                            foreach (var item in oldPolygonAndInfoList.points)
                            {
                                polygonAndInfo.points.Add(item);
                            }
                            polygonAndInfo.pdinfovc = new Pseudo_infoVC(oldPolygonAndInfoList.pdinfovc.maxOD, oldPolygonAndInfoList.pdinfovc.minOD, oldPolygonAndInfoList.pdinfovc.IOD,
                                       oldPolygonAndInfoList.pdinfovc.Count, oldPolygonAndInfoList.pdinfovc.AOD);
                            bms.Value.PolygonAndInfoList.Add(polygonAndInfo);
                        }
                    }
                    else
                    {
                        PolygonAndInfo polygonAndInfo = new PolygonAndInfo();
                        polygonAndInfo.points = new List<System.Drawing.Point>();
                        foreach (var item in oldPolygonAndInfoList.points)
                        {
                            polygonAndInfo.points.Add(item);
                        }
                        polygonAndInfo.pdinfovc = new Pseudo_infoVC(oldPolygonAndInfoList.pdinfovc.maxOD, oldPolygonAndInfoList.pdinfovc.minOD, oldPolygonAndInfoList.pdinfovc.IOD,
                                   oldPolygonAndInfoList.pdinfovc.Count, oldPolygonAndInfoList.pdinfovc.AOD);
                        PolygonAndInfoList.Add(polygonAndInfo);
                    }
                      
                    break;
                case ShapeForm.Rect:
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(curShapePoint, new System.Drawing.Size(oldCopyRect.rect.Width,oldCopyRect.rect.Height));
                    oldCopyRect.rect = rectangle;
                    currentRectangle = rectangle;
                    float _max = algAttribute.colorValue;
                    float _min = algAttribute.colorMinValue;
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            item.Value.currentRectangle = currentRectangle;
                            oldCopyRect = item.Value.UpdateRectInfo(_max, _min, oldCopyRect, rectDragStartIndex);
                            item.Value.rectangles.Add(oldCopyRect);
                        }
                    }
                    else
                    {
                        oldCopyRect = UpdateRectInfo(_max, _min, oldCopyRect, rectDragStartIndex);
                        rectangles.Add(oldCopyRect);
                    }
                    
                    
                    break;
                case ShapeForm.Circle:
                    int offsetX = curShapePoint.X - oldCopyCircle.center.X;
                    int offsetY = curShapePoint.Y - oldCopyCircle.center.Y;

                    // 更新圆心位置
                    oldCopyCircle.center.X += offsetX;
                    oldCopyCircle.center.Y += offsetY;
                    System.Drawing.Point point = new System.Drawing.Point(oldCopyCircle.Radius.X, oldCopyCircle.Radius.Y);
                    point.X += offsetX;
                    point.Y += offsetY;
                    oldCopyCircle.Radius = point;

                    float _max1 = algAttribute.colorValue;
                    float _min1 = algAttribute.colorMinValue;
                    int radius = (int)Math.Sqrt(Math.Pow(oldCopyCircle.center.X - oldCopyCircle.Radius.X, 2) + Math.Pow(oldCopyCircle.center.Y - oldCopyCircle.Radius.Y, 2));
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            oldCopyCircle = item.Value.UpdateCireInfo(_max1, _min1, oldCopyCircle, radius);
                            item.Value.CircleAndInfoList.Add(oldCopyCircle);
                        }
                    }
                    else
                    {
                        oldCopyCircle = UpdateCireInfo(_max1, _min1, oldCopyCircle, radius);
                        CircleAndInfoList.Add(oldCopyCircle);
                    }

                    
                    break;
                default:
                    break;
            }
         
            imagePanel.image_pl.Invalidate();
        }

        private void Ctms_strop_delete_Click(object sender, EventArgs e)
        {
            switch (curShape)
            {
                case ShapeForm.None:
                    break;
                case ShapeForm.Line:
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            item.Value.startPoint = new System.Drawing.Point(-10, 0);
                            item.Value.endPoint = new System.Drawing.Point(-10, 0);
                            item.Value.imagePanel.image_pl.Invalidate();
                            item.Value.imagePaletteForm.flb_act_mm.Text = ("0");
                            item.Value.imagePaletteForm.flb_act_mm.Refresh();
                        }
                    }
                    else
                    {
                        startPoint = new System.Drawing.Point(-10, 0);
                        endPoint = new System.Drawing.Point(-10, 0);
                        imagePanel.image_pl.Invalidate();
                        imagePaletteForm.flb_act_mm.Text = ("0");
                        imagePaletteForm.flb_act_mm.Refresh();
                    }
                    
                    break;
                case ShapeForm.Polygon:
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            //item.Value.PolygonAndInfoList.Clear();
                            item.Value.PolygonAndInfoList.RemoveAt(curShapeIndex);
                            if (item.Value.curPolygonAndInfoList.points!=null)
                                item.Value.curPolygonAndInfoList.points.Clear();
                            item.Value.curPolygonAndInfoList.pdinfovc = null;
                        }
                    }
                    else
                    {
                        PolygonAndInfoList.RemoveAt(curShapeIndex);
                        curPolygonAndInfoList.points.Clear();
                        curPolygonAndInfoList.pdinfovc = null;
                    }
                   
                   
                    break;
                case ShapeForm.Rect:
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            item.Value.rectangles.RemoveAt(curShapeIndex);
                        }
                    }
                    else
                    {
                        rectangles.RemoveAt(curShapeIndex);
                    }
                    
                   
                    break;
                case ShapeForm.Circle:
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            item.Value.CircleAndInfoList.RemoveAt(curShapeIndex);
                        }
                    }
                    else
                    {
                        CircleAndInfoList.RemoveAt(curShapeIndex);
                    }
                    
                   
                    
                    break;
                case ShapeForm.WandRect:
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            item.Value.wandRectangle.RemoveAt(curShapeIndex);
                        }
                    }
                    else
                    {
                        wandRectangle.RemoveAt(curShapeIndex);
                    }
                    break;
                case ShapeForm.TextBoxRect:
                    if (Arrangement == 2)
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            if(item.Value.textBoxInfos.Count > curShapeIndex)
                                item.Value.textBoxInfos.RemoveAt(curShapeIndex);
                        }
                    }
                    else
                    {
                        textBoxInfos.RemoveAt(curShapeIndex);
                    }
                    break;
                default:
                    break;
            }
          //  curShape = ShapeForm.None;
            imagePanel.image_pl.Invalidate();
        }
        
        private  void OnKeyPressed(Keys key, bool ctrl, bool shift, bool alt)
        {
            if (IsActive == false) 
            {
                return;
            }
           
            if (ctrl && key == Keys.C)
            {
                curShape = curTmpDownShape;
                curTmpDownShapeIndex = curShapeIndex;
                // 复制矩形 目前只允许复制矩形和圆形
                Ctms_strop_copy_Click(null, null);
            }

            if (ctrl && key == Keys.V)
            {
                curShapePoint = curTmpDownShapePoint;
                Ctms_strop_stickup_Click(null, null);
            }
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
                            string _pdfPath = directoryPath + "\\" + fileNameWithoutExtension + ".pdf";
                            FileMethod.ConvertExcelToPdf(path, _pdfPath);

                            var digitalSignature = new DigitalSignature(
                                FileMethod.File2Bytes(_pdfPath),
                                FileMethod.File2Bytes("logo.jpg"),
                                "jingyi",
                                FileMethod.File2Bytes("generate.pfx"),
                                "Aa123456");
                            var stream = digitalSignature.Signature();
                            File.Delete(_pdfPath);
                            using (var files = File.Create(_pdfPath))
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.CopyTo(files);
                            }
                            File.Delete(path);
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
                            string _pdfPath = directoryPath + "\\" + fileNameWithoutExtension + ".pdf";
                            FileMethod.ConvertExcelToPdf(path, _pdfPath);

                            var digitalSignature = new DigitalSignature(
                                FileMethod.File2Bytes(_pdfPath),
                                FileMethod.File2Bytes("logo.jpg"),
                                "jingyi",
                                FileMethod.File2Bytes("generate.pfx"),
                                "Aa123456");
                            var stream = digitalSignature.Signature();
                            File.Delete(_pdfPath);
                            using (var files = File.Create(_pdfPath)) 
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.CopyTo(files);
                            }

                            File.Delete(path);
                        }
                    }
                    
                }
            }
        }
        #endregion
        #region imagePaletteForm
        private void Dtb_textbox_TextChanged(object sender, EventArgs e)
        {
            if (curTextBox > -1) 
            {
                if (imagePaletteForm.dtb_textbox.Text != textBoxInfos[curTextBox].value) 
                {
                    if (Arrangement == 2) 
                    {
                        foreach (var item in bioanalysisMannages)
                        {
                            TextBoxInfo textBoxInfo = new TextBoxInfo();
                            textBoxInfo.value = imagePaletteForm.dtb_textbox.Text;
                            textBoxInfo.rect = textBoxInfos[curTextBox].rect;
                            textBoxInfo.index = textBoxInfos[curTextBox].index;
                            item.Value.textBoxInfos[curTextBox] = textBoxInfo;
                            item.Value.imagePanel.image_pl.Invalidate();
                        }
                    }
                    else
                    {
                        TextBoxInfo textBoxInfo = new TextBoxInfo();
                        textBoxInfo.value = imagePaletteForm.dtb_textbox.Text;
                        textBoxInfo.rect = textBoxInfos[curTextBox].rect;
                        textBoxInfo.index = textBoxInfos[curTextBox].index;
                        textBoxInfos[curTextBox] = textBoxInfo;
                        imagePanel.image_pl.Invalidate();
                    }
                    
                }
                
            }
        }

        private void Ava_textbox_Click(object sender, EventArgs e)
        {
            TextboxOn = true;
        }
        private void Cb_continuous_CheckedChanged(object sender, BoolEventArgs e)
        {
            isContinuous = imagePaletteForm.cb_continuous.Checked;
            if(isContinuous == false) 
            {
                rectOn = false;
                CircleOn = false;
            }
        }
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
        private void Hpb_wand_Click(object sender, EventArgs e)
        {
            iswandON = true;
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
        public void SaveMark(string path) 
        {
            if (image_mark_and_org_rgb24 != null)
            {

                image_mark_and_org_rgb24.SaveAsBmp(path);
            }
            
        }
        public void SavePseu(string path) 
        {
            if (image_org_rgb24 != null)
            {

                image_org_rgb24.SaveAsBmp(path);
            }
            else
            {

                image_org_L16.SaveAsBmp(path);
            }
        }
        public void SaveOrg(string path) 
        {
            if (image_org_L16 != null)
            {

                image_org_L16.SaveAsBmp(path);
            }
            else
            {

                image_org_L16.SaveAsBmp(path);
            }
        }
        public BioanalyImagePanel GetImagePanel 
        {
            get { return imagePanel; }
        }

        public void WindowAdaptive() 
        {
            imagePanel.WindowState = FormWindowState.Maximized;
        }
        public void WindowNormalAdaptive()
        {
            imagePanel.WindowState = FormWindowState.Normal;
        }

        public PictureBox GetImage
        {
            get 
            {
                return imagePanel.image_pl;
            }
          
        }
        public void SaveImage(string imgPath) 
        {
            imagePanel.image_pl.Invoke(new MethodInvoker(() =>
            {
                imagePanel.image_pl.Image.Save(imgPath);
            }));
           
        }
        public Image3D GetBarImage 
        {
            get
            {
                return imagePanel.image_pr;
            }
        }
        public BioanayImagePaletteForm GetBioanayImagePanel 
        {
            get 
            {
                return imagePaletteForm;
            }
            set 
            {
                imagePaletteForm = value;
            }
        }

        public void SetLine(System.Drawing.Point _StartPoint,System.Drawing.Point _EndPOint) 
        {
            startPoint = _StartPoint;
            endPoint = _EndPOint;
        }
        public TableLayoutPanel GetRight
        {
            get { return imagePanel.tlp_right_panel; }
            
           
        }
        public AntdUI.Panel GetPanel 
        {
            get { return imagePanel.pl_panel_image; }
        }
        public TableLayoutPanel GetBottomPanel
        {
            get{ return imagePanel.tlp_bottom_panel; }
        }
        public void Rifresh() 
        {
            imagePanel.pl_panel_image.Dock = DockStyle.Fill;
            imagePanel.image_pl.SizeMode = PictureBoxSizeMode.StretchImage;
            imagePanel.tableLayoutPanel2.Controls.Add(imagePanel.pl_panel_image, 0, 0);
            imagePanel.tableLayoutPanel2.Controls.Add(imagePanel.tlp_right_panel, 1, 0);
            imagePanel.tlp_right_panel.Controls.Add(imagePanel.lb_top_info, 0, 0);
            imagePanel.tlp_right_panel.Controls.Add(imagePanel.image_pr,0, 1);
            imagePanel.tlp_right_panel.Controls.Add(imagePanel.lb_wh, 0, 2);
            imagePanel.image_pr.ImageFit = TFit.Fill;

            imagePanel.pl_bottom.Controls.Add(imagePanel.tlp_bottom_panel);
            imagePanel.ava_auto_Click(null,null);

        }
        #endregion
    }
}
