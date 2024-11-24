using Aspose.Pdf.AI;
using PBAnaly.UI;
using ScottPlot.Panels;
using ScottPlot.Plottables;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections;
using System.Collections.Generic;
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
        #region 变量
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
                        
                        imagePaletteForm.nud_colorMin.Maximum = algAttribute.colorValue;
                        imagePaletteForm.dtb_colorMin.Maximum = algAttribute.colorValue;
                        fix = true;
                    }
                    else if(algAttribute.colorValue < imagePaletteForm.nud_colorMin.Value)
                    {
                        imagePaletteForm.nud_colorMin.Maximum = algAttribute.colorValue;
                        imagePaletteForm.dtb_colorMin.Maximum = algAttribute.colorValue;
                        imagePaletteForm.nud_colorMin.Value = algAttribute.colorValue;
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

        public BioanalysisMannage(string _path, ReaLTaiizor.Controls.Panel _pl_right) 
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
            algAttribute. colorMin = 0;
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

            imagePaletteForm.dtb_colorMin.Maximum = algAttribute.colorMin;
            imagePaletteForm.dtb_colorMin.Maximum = algAttribute.colorValue;
            imagePaletteForm.dtb_colorMin.Value = algAttribute.colorValue;
            imagePaletteForm.nud_colorMin.Maximum = algAttribute.colorValue;
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

            }
        }
        #endregion


        #region 事件
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
           
        }

        private void Image_pl_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left) 
            {
                imagePanel.pl_bg_panel.Cursor = Cursors.Default;
            }

        }

        private void Image_pl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left) 
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
        }

        private void Image_pl_DoubleClick(object sender, System.EventArgs e)
        {
            
        }

        private void Image_pl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && imagePanel.IsImageLargerThanPanel()) 
            {
                isDragging = true;
                mouseDownPosition = e.Location;
                pictureBoxStartPosition = imagePanel.pl_bg_panel.Location;
                imagePanel.pl_bg_panel.Cursor = Cursors.Hand;
            }
        }

        #endregion
        #endregion
        #region 对外接口
        public BioanalyImagePanel GetImagePanel 
        {
            get { return imagePanel; }
       }
        #endregion
    }
}
