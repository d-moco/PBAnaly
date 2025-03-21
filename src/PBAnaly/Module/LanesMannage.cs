﻿using AntdUI;
using Aspose.Pdf.AI;
using OpenCvSharp;
using OpenCvSharp.Flann;
using PBAnaly.UI;
using PBBiologyVC;
using ScottPlot;
using SharpDX.D3DCompiler;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PBAnaly.Module.BioanalysisMannage;
using static PBAnaly.Module.LanesMannage;

namespace PBAnaly.Module
{
    public class LanesMannage
    {
        #region 构造函数
        private enum Corner { None, TopLeft, TopRight, BottomLeft, BottomRight, drawMouse }
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

        public struct Lanes_info
        {
            public bool isSelect;//是否被选中
            public int colorIndex;
            public RectVC rect;
            public _band_info band_Info;
        }
        public enum CurLaneEnum 
        {
            None = 0,
            FindLane,
            AddLane,
            DeleteLane,
            AddBands,
            DeleteBands,
            FindBands
        }
        public struct LanesAttribute 
        {
            public CurLaneEnum laneEnum;
            public bool isSameLineWidth;
            public int ProteinRect_width;
            public bool showLanes;
            public bool showbands;
            public bool showLanesWell;
            public bool showAlwayLanesWell;
            public System.Drawing.Point? laneWell;
            public int curLaneIndex;
            public int curBandIndex;
        }
        #endregion
        #region 参数
        private string path { get; set; }
        private string curImagePath;

        PBBiology pbb = new PBBiology();
        private Image<L16> image_L16;
        private byte[] image_byte;
        private byte[] image_8bit_byte;
        private byte[] image_8bit_rgb_byte;
        private Image<Rgb24> image_rgb_24 = null;
        public bool IsActive { get; set; } // 当前窗口是否在活跃状态  用来判断是否需要操作
        public int ImageIndex { get; set; }// 图片加载进来的序号
        public int Arrangement { get; set; } // 0:代表单张图 1:代表是合并图图但不做处理 2:代表是合并图 并且为处理图
        private Dictionary<string, LanesMannage> lanesMannages;
        private ReaLTaiizor.Controls.Panel pl_right;
        private LanesImagePanel imagePanel = null;
        private LanesImagePaletteForm imagePaletteForm = null;
        private LaneInitialWellsForm neuronInitialWellsForm = null;
        private Thread algThread;
        private bool isalgRun = false;
        private bool isUpdateAlg = false;
        private Queue<LanesAttribute> queueAlgAttribute = new Queue<LanesAttribute>();
        private List<Lanes_info> lanes_Infos = new List<Lanes_info>();
        private bool isAddLine = false;
        private bool isDeleteLineOn = false;
        private bool isShowLanelWellOn = false;// 是否设定初始井
        public bool isAddBandsOn = false;// 是否添加条带
        public bool isDeleteBandsOn = false;//删除指定条带
        public bool isFindBandsOn = false;//查找条带
        private System.Drawing.Point? laneWellPoint;//显示初始井的点
        private System.Drawing.Rectangle curLanes ;
        private System.Drawing.Point curPoint;
        private List<System.Drawing.Color> laneColorList = new List<System.Drawing.Color>();// 泳道的颜色表
        private LanesAttribute lanesAttribute = new LanesAttribute();
        #endregion


        #region 构造函数
        public bool ShowLanes 
        {
            get { return lanesAttribute.showLanes; }
            set 
            {
                if (lanesAttribute.showLanes != value) 
                {
                    lanesAttribute.laneEnum = CurLaneEnum.None;
                    lanesAttribute.showLanes = value;
                    imagePanel.image_pl.Invalidate();
                }
            }
        }
        public bool IsSameLineWidth 
        {
            get { return lanesAttribute.isSameLineWidth; }
            set
            {
                if (lanesAttribute.isSameLineWidth != value) 
                {
                    imagePaletteForm.nud_lane_fixedWidth.Enabled = value;
                    lanesAttribute.laneEnum = CurLaneEnum.None;
                    lanesAttribute.isSameLineWidth = value;
                    LaneEnum = CurLaneEnum.FindLane;
                }
              
            }
        }
        public int ProteinRect_Width
        {
            get { return lanesAttribute.ProteinRect_width; }
            set
            {
                if (lanesAttribute.ProteinRect_width != value)
                {
                    lanesAttribute.laneEnum = CurLaneEnum.None;
                    lanesAttribute.ProteinRect_width = value;
                    LaneEnum = CurLaneEnum.FindLane;
                }

            }
        }
        public CurLaneEnum LaneEnum 
        {
            get { return lanesAttribute.laneEnum; }
            set 
            {
                if (lanesAttribute.laneEnum != value) 
                {
                    lanesAttribute.laneEnum = value;
                    bool fix = true;

                    if (fix && isUpdateAlg) 
                    {
                        queueAlgAttribute.Enqueue(lanesAttribute);
                    }
                }
            }
        }

        public System.Drawing.Point? LaneWall 
        {
            get { return lanesAttribute.laneWell; }
            set
            {
                bool fix = false;
                if (lanesAttribute.laneWell == null) 
                {
                    fix = true;
                    lanesAttribute.laneWell = value;
                }
                else if (lanesAttribute.laneWell.Value.X != value.Value.X || lanesAttribute.laneWell.Value.Y != value.Value.Y)
                {
                    fix = true;
                    lanesAttribute.laneWell = value;
                    
                }
                if (fix && isUpdateAlg)
                {
                    queueAlgAttribute.Enqueue(lanesAttribute);
                }
            }
        }

        public bool ShowLaneWallOn 
        {
            get { return lanesAttribute.showLanesWell; }
            set
            {
                if (lanesAttribute.showLanesWell != value)
                {
                    LaneEnum = CurLaneEnum.None;
                    lanesAttribute.showLanesWell = value;
                    imagePanel.image_pl.Invalidate();
                }
            }
        }
        public bool ShowAlwayLanesWall
        {
            get { return lanesAttribute.showAlwayLanesWell; }
            set
            {
                if (lanesAttribute.showAlwayLanesWell != value)
                {
                    LaneEnum = CurLaneEnum.None;
                    lanesAttribute.showAlwayLanesWell = value;
                    imagePanel.image_pl.Invalidate();
                }
            }
        }
        public bool ShowBands 
        {
            get { return lanesAttribute.showbands; }
            set
            {
                if (lanesAttribute.showbands != value)
                {
                    lanesAttribute.laneEnum = CurLaneEnum.None;
                    lanesAttribute.showbands = value;
                    imagePanel.image_pl.Invalidate();
                }
            }
        }
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

            neuronInitialWellsForm = new LaneInitialWellsForm();
            InitColorList();
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
                LanesAttribute? aatb = null;
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
                    ImageAlg((LanesAttribute)aatb);

                }
                Thread.Sleep(5);
            }
        }

        private void ImageAlg(LanesAttribute aatb)
        {
            switch (aatb.laneEnum)
            {
                case CurLaneEnum.None:
                    break;
                case CurLaneEnum.FindLane:
                   
                    byte[] gray = image_8bit_byte;
                    unsafe
                    {
                        lanes_Infos.Clear();
                        fixed (byte* data = gray)
                        {
                            var rectvc = pbb.getProteinRectVC(data, 8, (ushort)image_L16.Width, (ushort)image_L16.Height,aatb.ProteinRect_width, aatb.isSameLineWidth, 90);
                            fixed (byte* data_16bit = image_byte) 
                            {
                                var pbands =  pbb.getProteinBandsVC(data_16bit, 16, (ushort)image_L16.Width, (ushort)image_L16.Height, rectvc);
                                int index = 0;
                                foreach (var item in rectvc)
                                {
                                    Lanes_info lanes_Info = new Lanes_info();
                                    lanes_Info.rect = item;
                                    lanes_Info.band_Info = pbands[index];
                                    lanes_Infos.Add(lanes_Info);
                                    index++;
                                }
                            }
                        }
                    }
                    LaneEnum = CurLaneEnum.None;
                    break;
                case CurLaneEnum.AddLane:
                    unsafe
                    {
                        
                        fixed (byte* data = image_byte)
                        {
                            RectVC rectVC = new RectVC(curLanes.X,curLanes.Y,curLanes.Width,curLanes.Height);
                            _band_info ll = pbb.get_protein_lane_dataVC(data, 16, (ushort)image_L16.Width, (ushort)image_L16.Height, rectVC);

                            Lanes_info lanes_Info = new Lanes_info();
                            lanes_Info.rect = rectVC;
                            lanes_Info.band_Info = ll;
                            lanes_Infos.Add(lanes_Info);
                        }
                    }
                    break;
                case CurLaneEnum.DeleteLane:

                    break;
                case CurLaneEnum.AddBands:
                    unsafe 
                    {
                        List<RectVC> rectVCs1 = new List<RectVC>();
                        List<_band_info> unadjustbands =new List<_band_info>();
                        foreach (var item in lanes_Infos)
                        {
                            rectVCs1.Add(item.rect);
                            unadjustbands.Add(item.band_Info);
                        }
                        pbb.addProteinBandVC(ref rectVCs1, lanesAttribute.curLaneIndex,ref unadjustbands,curPoint.Y);

                        lanes_Infos.Clear();
                        int index = 0;
                        foreach (var item in rectVCs1)
                        {
                            Lanes_info lanes_Info = new Lanes_info();
                            lanes_Info.rect = item;
                            lanes_Info.band_Info = unadjustbands[index];
                            lanes_Infos.Add(lanes_Info);
                            index++;
                        }
                    }
                    break;
                case CurLaneEnum.DeleteBands:
                    List<_band_info> _unadjustbands = new List<_band_info>();
                    foreach (var item in lanes_Infos)
                    {
                       
                        _unadjustbands.Add(item.band_Info);
                    }

                    pbb.deleteProteinBandVC(lanesAttribute.curLaneIndex, ref _unadjustbands, lanesAttribute.curBandIndex);

                    for (int i = 0; i < lanes_Infos.Count; i++)
                    {
                        Lanes_info _lane = lanes_Infos[i];
                        _lane.band_Info = _unadjustbands[i];
                        lanes_Infos[i] = _lane;
                    }
                  
                    break;
                case CurLaneEnum.FindBands:
                    List<RectVC> rectVCs = new List<RectVC>();
                    foreach (var item in lanes_Infos)
                    {
                        rectVCs.Add(item.rect);
                    }
                  
                    unsafe
                    {
                        lanes_Infos.Clear();
                        fixed (byte* data_16bit = image_byte)
                        {
                            var pbands = pbb.getProteinBandsVC(data_16bit, 16, (ushort)image_L16.Width, (ushort)image_L16.Height, rectVCs);
                            int index = 0;
                            foreach (var item in rectVCs)
                            {
                                Lanes_info lanes_Info = new Lanes_info();
                                lanes_Info.rect = item;
                                lanes_Info.band_Info = pbands[index];
                                lanes_Infos.Add(lanes_Info);
                                index++;
                            }
                        }
                    }
                    LaneEnum = CurLaneEnum.None;
                    break;
                default:
                    break;

            }
            imagePanel.image_pl.Invalidate();
        }

        private void InitColorList() 
        {
            laneColorList.Clear();

            laneColorList.Add(System.Drawing.Color.Red);        // 红色
            laneColorList.Add(System.Drawing.Color.Green);      // 绿色
            laneColorList.Add(System.Drawing.Color.Blue);       // 蓝色
            laneColorList.Add(System.Drawing.Color.Yellow);     // 黄色
            laneColorList.Add(System.Drawing.Color.Purple);     // 紫色
            laneColorList.Add(System.Drawing.Color.Orange);     // 橙色
            laneColorList.Add(System.Drawing.Color.Pink);       // 粉色
            laneColorList.Add(System.Drawing.Color.Brown);      // 棕色
            laneColorList.Add(System.Drawing.Color.Gray);       // 灰色
            laneColorList.Add(System.Drawing.Color.Cyan);       // 青色
            laneColorList.Add(System.Drawing.Color.Magenta);    // 品红
            laneColorList.Add(System.Drawing.Color.Lime);       // 酸橙绿
            laneColorList.Add(System.Drawing.Color.Teal);       // 水鸭色
            laneColorList.Add(System.Drawing.Color.Olive);      // 橄榄色
            laneColorList.Add(System.Drawing.Color.Navy);       // 海军蓝
            laneColorList.Add(System.Drawing.Color.Silver);     // 银色
            laneColorList.Add(System.Drawing.Color.Gold);       // 金色
            laneColorList.Add(System.Drawing.Color.Violet);     // 紫罗兰色
        }
        /// <summary>
        /// 初始化控件 初始化配置
        /// </summary>
        private void Init()
        {
            lanesAttribute.laneEnum = CurLaneEnum.None;
            lanesAttribute.isSameLineWidth = false;
            lanesAttribute.ProteinRect_width = 44;
            lanesAttribute.showLanes = true;
            lanesAttribute.showbands = true;

            imagePaletteForm.cb_alwaysShowBands.Checked = lanesAttribute.showbands;
            imagePaletteForm.cb_alwaysShowLane.Checked = lanesAttribute.showLanes;

            imagePanel.image_pl.MouseDown += Image_pl_MouseDown;
            imagePanel.image_pl.DoubleClick += Image_pl_DoubleClick;
            imagePanel.image_pl.MouseMove += Image_pl_MouseMove;
            imagePanel.image_pl.MouseUp += Image_pl_MouseUp;
            imagePanel.image_pl.Paint += Image_pl_Paint;


            imagePanel.wdb_title.MouseDown += Wdb_title_Click;
            imagePanel.FormClosing += ImagePanel_FormClosing;
            imagePanel.FormClosed += ImagePanel_FormClosed;



            imagePaletteForm.mb_findLanes.Click += Mb_findLanes_Click;
            imagePaletteForm.mb_addLanes.Click += Mb_addLanes_Click;
            imagePaletteForm.mb_deleteLane.Click += Mb_deleteLane_Click;
            imagePaletteForm.cb_lane_width.CheckedChanged += Cb_lane_width_CheckedChanged;
            imagePaletteForm.nud_lane_fixedWidth.ValueChanged += Nud_lane_fixedWidth_ValueChanged;
            imagePaletteForm.cb_alwaysShowLane.CheckedChanged += Cb_alwaysShowLane_CheckedChanged;
            imagePaletteForm.mb_updateInitWell.Click += Mb_updateInitWell_Click;
            imagePaletteForm.mb_addBands.Click += Mb_addBands_Click;
            imagePaletteForm.mb_deleteBands.Click += Mb_deleteBands_Click;
            imagePaletteForm.cb_alwaysShowBands.CheckedChanged += Cb_alwaysShowBands_CheckedChanged;
            imagePaletteForm.mb_findBands.Click += Mb_findBands_Click;

            neuronInitialWellsForm.cb_alwayShowWell.CheckedChanged += Cb_alwayShowWell_CheckedChanged;
            neuronInitialWellsForm.btn_CencalLaneWell.Click += Btn_CencalLaneWell_Click;
            neuronInitialWellsForm.btn_deleteLaneWell.Click += Btn_deleteLaneWell_Click;
            neuronInitialWellsForm.btn_okLaneWell.Click += Btn_okLaneWell_Click;
            neuronInitialWellsForm.FormClosing += NeuronInitialWellsForm_FormClosing;
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

            image_8bit_rgb_byte = new byte[image_L16.Width * image_L16.Height * 3];
            image_8bit_byte = new byte[image_L16.Width * image_L16.Height];
            for (int i = 0; i < image_L16.Width * image_L16.Height; i++) 
            {
                // 获取16位图像数据中的当前像素值
                ushort pixel16bit = (ushort)(image_byte[i * 2] | (image_byte[i * 2 + 1] << 8));
                byte gray = (byte)((pixel16bit / 65535.0) * 255) ;
                image_8bit_byte[i] = gray;
                // 将R、G、B分量存储到RGB格式的数组中
                image_8bit_rgb_byte[i * 3] = gray;
                image_8bit_rgb_byte[i * 3 + 1] = gray;
                image_8bit_rgb_byte[i * 3 + 2] = gray;
            }

            image_rgb_24 = util.ConvertByteArrayToRgb24Image(image_8bit_rgb_byte, image_L16.Width, image_L16.Height,3);
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


        private bool IsPointInRectangles(System.Drawing.Point point, List<Lanes_info> _lanes, out Corner cner, out Lanes_info curRect, out int index)
        {
            curRect = new Lanes_info();
            cner = Corner.None;
            index = 0;
            foreach (var lanes in _lanes)
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(lanes.rect.X,lanes.rect.Y,lanes.rect.Width,lanes.rect.Height);
                System.Drawing.Point topLeft = new System.Drawing.Point(rect.Left, rect.Top);
                System.Drawing.Point topRight = new System.Drawing.Point(rect.Right, rect.Top);
                System.Drawing.Point bottomLeft = new System.Drawing.Point(rect.Left, rect.Bottom);
                System.Drawing.Point bottomRight = new System.Drawing.Point(rect.Right, rect.Bottom);

                if (rect.Contains(point))
                {
                    imagePanel.image_pl.Cursor = Cursors.Hand;
                    cner = Corner.drawMouse;
                    curRect = lanes;
                    return true;
                }
                index++;

            }
            return false;
        }

        private bool IsPointInLanesBandsRectangles(System.Drawing.Point point, Lanes_info _lanes,out int _index )
        {
            System.Drawing.Rectangle p = new System.Drawing.Rectangle(_lanes.rect.X, _lanes.rect.Y, _lanes.rect.Width, _lanes.rect.Height);
            _index = 0;
            foreach (var item in _lanes.band_Info.band_point)
            {
                var p1 = new System.Drawing.Point(p.X, p.Top + item[1]);
                var p2 = new System.Drawing.Point(p.Right, p.Top + item[2]);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(p1,new System.Drawing.Size(p2.X - p1.X,p2.Y-p1.Y));
                if (rect.Contains(point))
                {
                    imagePanel.image_pl.Cursor = Cursors.Hand;
                   
                    return true;
                }
                _index++;
            }
            return false;
        }
        #endregion


        #region 事件
        #region imagepanel

        private void Image_pl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int index = 0;
            List<int> colorUpdateIndex = new List<int>();
            foreach (var rect in lanes_Infos)
            {
                if (index >= laneColorList.Count) 
                {
                    index = 0;
                }
                colorUpdateIndex.Add(index);
                var color = laneColorList[index];
                System.Drawing.Rectangle p = new System.Drawing.Rectangle(rect.rect.X, rect.rect.Y, rect.rect.Width, rect.rect.Height);
                
                var r = ImageProcess.ConvertRealRectangleToPictureBox(p, imagePanel.image_pl);
                // 绘制编号
                System.Drawing.Font l  = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                SolidBrush solidBrush = new SolidBrush(color); 
                
                if (lanesAttribute.showLanes) 
                {
                    if (lanesAttribute.showLanesWell) 
                    {
                        if (lanesAttribute.laneWell != null) 
                        {
                            var r1 = ImageProcess.ConvertRealToPictureBox(lanesAttribute.laneWell.Value, imagePanel.image_pl);
                            r.Y = r1.Y;
                            r.Height = r.Height - r1.Y;
                            if (lanesAttribute.showAlwayLanesWell) 
                            {
                                Pen pen = new Pen(System.Drawing.Color.Red,5);
                                e.Graphics.DrawLine(pen, new System.Drawing.Point(r.X, r.Y-1), new System.Drawing.Point(r.X + r.Width, r.Y-1));
                            }
                           
                        }
                       
                    }

                    e.Graphics.DrawString("L" + index, l, solidBrush, new System.Drawing.RectangleF(r.X + r.Width / 3, r.Y - 20, r.Width + 10, 100));
                    if (rect.isSelect)
                    {
                        e.Graphics.DrawRectangle(color, r, false, 6);
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(color, r, false, 2);
                    }
                }

                int centerX = (int)(p.X + (p.Right - p.X) / 2.0);
                int centerY = 0;
                if (rect.band_Info != null) 
                {
                    foreach (var item in rect.band_Info.band_point)
                    {
                        if (item[0] == -1) continue;
                        // 绘制十字
                        centerY = p.Y + item[0];
                        var p1 = new System.Drawing.Point(centerX - 5, centerY);
                        var p2 = new System.Drawing.Point(centerX + 5, centerY);
                        p1 = ImageProcess.ConvertRealToPictureBox(p1, imagePanel.image_pl);
                        p2 = ImageProcess.ConvertRealToPictureBox(p2, imagePanel.image_pl);

                        e.Graphics.DrawLine(Pens.Red, p1, p2);
                        p1 = new System.Drawing.Point(centerX, centerY - 5);
                        p2 = new System.Drawing.Point(centerX, centerY + 5);
                        p1 = ImageProcess.ConvertRealToPictureBox(p1, imagePanel.image_pl);
                        p2 = ImageProcess.ConvertRealToPictureBox(p2, imagePanel.image_pl);
                        e.Graphics.DrawLine(Pens.Red, p1, p2);

                        // 显示条带
                        if (lanesAttribute.showbands)
                        {
                            p1 = new System.Drawing.Point(p.X, p.Top + item[1]);
                            p2 = new System.Drawing.Point(p.Right, p.Top + item[2]);
                            p1 = ImageProcess.ConvertRealToPictureBox(p1, imagePanel.image_pl);
                            p2 = ImageProcess.ConvertRealToPictureBox(p2, imagePanel.image_pl);

                            e.Graphics.DrawRectangle(Pens.Blue, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
                        }

                    }
                }
               
                
               
               
                index++;
            }
            for (int i = 0; i < colorUpdateIndex.Count; i++) 
            {
                Lanes_info _Info = lanes_Infos[i];
                _Info.colorIndex = colorUpdateIndex[i];
                lanes_Infos[i] = _Info;
            }

            if (isAddLine) 
            {
               

                var r = ImageProcess.ConvertRealRectangleToPictureBox(curLanes, imagePanel.image_pl);
                e.Graphics.DrawRectangle(System.Drawing.Color.Red, r, false, 6);
            }

            if (isShowLanelWellOn) 
            {
                if (laneWellPoint != null) 
                {
                    var  p1 = ImageProcess.ConvertRealToPictureBox((System.Drawing.Point)laneWellPoint, imagePanel.image_pl);
                  
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Brush bush = new SolidBrush(System.Drawing.Color.Red);//填充的颜色                 
                    e.Graphics.FillEllipse(bush, p1.X, p1.Y,10,10);//画椭圆的方法，x坐标、y坐标、宽、高，如果是100，则半径为50
                }
            }
        }

        private void Image_pl_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void Image_pl_MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point readLoction = ImageProcess.ConvertPictureBoxToReal(e.Location, imagePanel.image_pl);
            imagePanel.image_pl.Cursor = Cursors.Default;
            if (isAddLine) 
            {
                int leftX = readLoction.X - lanesAttribute.ProteinRect_width / 2;
                int leftY = (int)(image_L16.Height * 0.05);
                int width = lanesAttribute.ProteinRect_width;
                int height = (int)(image_L16.Height * 0.9);
                curLanes = new System.Drawing.Rectangle(leftX, leftY, width, height);
                imagePanel.image_pl.Invalidate();
            }
            else if (IsPointInRectangles(readLoction, lanes_Infos, out var cner, out var curRect, out int index)) 
            {
                if (isAddBandsOn)
                {
                    imagePanel.image_pl.Cursor = Cursors.IBeam;
                }
                else if (isDeleteBandsOn)
                {
                    if (IsPointInLanesBandsRectangles(readLoction, curRect, out var _index)) 
                    {
                        imagePanel.image_pl.Cursor = Cursors.SizeAll;
                    }
                      
                }
            }

        }

        private void Image_pl_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void Image_pl_MouseDown(object sender, MouseEventArgs e)
        {
            System.Drawing.Point readLoction = ImageProcess.ConvertPictureBoxToReal(e.Location, imagePanel.image_pl);
            Wdb_title_Click(null, null);
            if (isAddLine) 
            {
                curPoint = readLoction;
                isAddLine = false;
                LaneEnum = CurLaneEnum.None;
                LaneEnum = CurLaneEnum.AddLane;
            }
            else if (IsPointInRectangles(readLoction, lanes_Infos, out var cner, out var curRect, out int index))
            {
                if (isDeleteLineOn)
                {
                    isDeleteLineOn = false;
                    lanes_Infos.RemoveAt(index);
                    imagePanel.image_pl.Invalidate();
                }
                else if (isShowLanelWellOn)
                {
                    laneWellPoint = new System.Drawing.Point(curRect.rect.X + (curRect.rect.Width / 3), readLoction.Y);
                    imagePanel.image_pl.Invalidate();
                }
                else if (isAddBandsOn) 
                {
                    LaneEnum = CurLaneEnum.None;
                    lanesAttribute.curLaneIndex = index;
                    curPoint = readLoction;
                    LaneEnum = CurLaneEnum.AddBands;
                    isAddBandsOn = false;
                }
                else if (isDeleteBandsOn)
                {
                    if (IsPointInLanesBandsRectangles(readLoction, curRect, out var _index))
                    {
                        LaneEnum = CurLaneEnum.None;
                        lanesAttribute.curLaneIndex = index;
                        lanesAttribute.curBandIndex = _index;
                        imagePanel.image_pl.Cursor = Cursors.SizeAll;
                        LaneEnum = CurLaneEnum.DeleteBands;
                       
                    }
                    isDeleteBandsOn = false;
                }
                else
                {
                    for (int i = 0; i < lanes_Infos.Count; i++)
                    {
                        Lanes_info lanes = lanes_Infos[i];
                        lanes.isSelect = false;
                        lanes_Infos[i] = lanes;
                    }
                    curRect.isSelect = true;
                    lanes_Infos[index] = curRect;
                    imagePanel.image_pl.Invalidate();
                }
               
            }
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
            LaneEnum = CurLaneEnum.FindLane;
        }
        private void Mb_addLanes_Click(object sender, EventArgs e)
        {
            isAddLine = true;
        }
        private void Mb_deleteLane_Click(object sender, EventArgs e)
        {
            isDeleteLineOn = true;
        }
        private void Cb_lane_width_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            IsSameLineWidth = imagePaletteForm.cb_lane_width.Checked;
            
        }
        private void Nud_lane_fixedWidth_ValueChanged(object sender, EventArgs e)
        {
            ProteinRect_Width = (int)imagePaletteForm.nud_lane_fixedWidth.Value;
        }
        private void Cb_alwaysShowLane_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            ShowLanes = imagePaletteForm.cb_alwaysShowLane.Checked;
        }
        private void Mb_updateInitWell_Click(object sender, EventArgs e)
        {
            isShowLanelWellOn = true;
            if (neuronInitialWellsForm != null) 
            {
                neuronInitialWellsForm.TopMost = true;
                neuronInitialWellsForm.Show();
            }
            laneWellPoint = null;
            ShowLaneWallOn = true;
        }

        private void Mb_addBands_Click(object sender, EventArgs e)
        {
            isAddBandsOn = true;
        }
        private void Mb_deleteBands_Click(object sender, EventArgs e)
        {
            isDeleteBandsOn = true;
        }
        private void Cb_alwaysShowBands_CheckedChanged(object sender, BoolEventArgs e)
        {
            ShowBands = imagePaletteForm.cb_alwaysShowBands.Checked;
        }
        private void Mb_findBands_Click(object sender, EventArgs e)
        {
            LaneEnum = CurLaneEnum.FindBands;
        }
        #endregion

        #region LaneInitialWells
        private void NeuronInitialWellsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Btn_CencalLaneWell_Click(null,null);
            e.Cancel = true;
        }
        private void Btn_okLaneWell_Click(object sender, EventArgs e)
        {
            if (neuronInitialWellsForm != null)
            {
                neuronInitialWellsForm.Hide();
                
            }
            if (laneWellPoint != null) 
            {
                LaneWall = (System.Drawing.Point)laneWellPoint;
                laneWellPoint = null;
            }
            
            isShowLanelWellOn = false;
        }

        private void Btn_deleteLaneWell_Click(object sender, EventArgs e)
        {
            ShowLaneWallOn = false;
            laneWellPoint = null;
        }

        private void Btn_CencalLaneWell_Click(object sender, EventArgs e)
        {
            if (neuronInitialWellsForm != null)
            {
                neuronInitialWellsForm.Hide();

            }
            if (laneWellPoint != null)
            {
                LaneWall = (System.Drawing.Point)laneWellPoint;
                laneWellPoint = null;
            }
            isShowLanelWellOn = false;
        }

        private void Cb_alwayShowWell_CheckedChanged(object sender)
        {
            ShowAlwayLanesWall = neuronInitialWellsForm.cb_alwayShowWell.Checked;
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
