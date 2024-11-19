

using OpenCvSharp.ImgHash;
using PBAnaly.UI;
using System.Collections.Generic;

namespace PBAnaly.Module
{
    public static class ImageToolMannage
    {
        #region 变量
        public static bool lineDisON = false;
        public static bool rectON = false;
        public static bool linepolygonON = false; 
        public static bool linewandON = false;
        public static bool circleON = false;
        public static int color_min = 6000;
        public static int color_max = 65534;
        public static float alpha = 100;
        public static ImagePanel imagePanel { get; set; }
        public static ImagePanelUser imagePanelUser { get; set; }
        public static ReaLTaiizor.Controls.Panel right_panel { get; set; }
        public static int beta = 127;

        public static  double pixeltomm = 0.0825;

        public static Dictionary<string, ImagePanel> imageDataPath = new Dictionary<string, ImagePanel>();

        public static int RoiIndex = 0;
        public static int CircleIndex = 0;
        public static int Roi_w = 20;
        public static int Roi_h = 20;
        public static int Roi_r = 10;

        #endregion
    }
}
