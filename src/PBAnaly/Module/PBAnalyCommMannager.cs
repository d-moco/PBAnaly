using OpenCvSharp;
using OpenCvSharp.Extensions;
using PBBiologyVC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBAnaly.Module
{
    public class PBAnalyCommMannager
    {
        public static LaneChartForm laneChartForm = null;
        public static DataProcessForm processForm = null;
        public static List<RectVC> proteinRect = null;
        public static List<_band_info> band_info = null;

        //public List<ushort> land_data;

        //public List<float> ydata;

        //public List<float> xdata;

        //public List<List<int>> band_point;

        //public List<MolecularInfoVC> Minfo;
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

        public static bool processcurveAlg() 
        {
            
            if (processForm == null) return false;
            var image = processForm.getImage;
            Mat whiteBackgroundImg = new Mat();
            Scalar meanValue = Cv2.Mean(image);
            if (meanValue[0] < 10000)
            {
                Cv2.BitwiseNot(image, whiteBackgroundImg);
            }
            else
            {
                whiteBackgroundImg = image.Clone();
            }

            Mat input_cn1 = new Mat();
            whiteBackgroundImg.ConvertTo(input_cn1, MatType.CV_8U, 0.00390625);

            byte[] byte_image = new byte[input_cn1.Width * input_cn1.Height];
            ushort[] whiteBackgroundImg_image = new ushort[whiteBackgroundImg.Width * whiteBackgroundImg.Height];
            int index = 0;
            for (int i = 0; i < input_cn1.Rows; i++)
            {
                for (int j = 0; j < input_cn1.Cols; j++)
                {
                    byte value = input_cn1.At<byte>(i, j); // 使用At方法访问数据
                    ushort value1 = whiteBackgroundImg.At<ushort>(i, j); // 使用At方法访问数据
                    byte_image[index] = value;
                    whiteBackgroundImg_image[index++] = value1;

                }

            }

            PBBiology dd = new PBBiology();
            proteinRect = new List<RectVC>();
            band_info = new List<_band_info>();


            //unsafe
            //{
            //    fixed (byte* p = byte_image)
            //    {
            //        proteinRect = dd.getProteinRectVC(p, (ushort)input_cn1.Width, (ushort)input_cn1.Height);
            //    }

            //    byte[] bytes = util.ConvertUShortArrayToByteArrayComplete(whiteBackgroundImg_image);


            //    fixed (byte* p = bytes)
            //    {
            //        dd.getProteinBandsVC(p, 16, (ushort)input_cn1.Width, (ushort)input_cn1.Height, proteinRect, ref band_info);
            //    }
            //    dd.adjustBands(band_info, 10);
            //    dd.molecularWeightResult(ref proteinRect, ref band_info);
                
            //}
            Mat mat = new Mat();
            if (input_cn1.Channels() == 1) 
            {
                Cv2.CvtColor(input_cn1, mat, ColorConversionCodes.GRAY2BGR);
            }
            // 随机数生成器
            Random random = new Random();

            // 定义多个矩形的位置和大小
            (int x, int y, int width, int height)[] rectangles = {
            (100, 150, 200, 100),
            (300, 200, 150, 90),
            (50, 30, 180, 120),
            // 添加更多矩形
            };
            List<PBAnalyCommMannager.band_infos> bands = new List<band_infos>();
            index = 0;
            foreach (var m in proteinRect)
            {
                Scalar color = new Scalar(random.Next(256), random.Next(256), random.Next(256));  // 随机RGB值
                OpenCvSharp.Point rectStart = new OpenCvSharp.Point(m.X, m.Y);
                OpenCvSharp.Point rectEnd = new OpenCvSharp.Point(m.X + m.Width, m.Y + m.Height);
                band_infos _Infos = new band_infos();
                _Infos.startX = rectStart.X;
                _Infos.startY = rectStart.Y;
                _Infos.endX = rectEnd.X;
                _Infos.endY = rectEnd.Y;
                _Infos.color = color;
                _Infos.thick = 1;
                _Infos._Info = band_info[index++];
                bands.Add(_Infos);
            }
            processForm.SetBands = bands;
            processForm.Draw();

            if (laneChartForm == null)
                laneChartForm = new LaneChartForm();
            laneChartForm.TopMost = true;
            laneChartForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            laneChartForm.Draw(bands[0]);
            laneChartForm.Show();
            return true;
        }
    }
}
