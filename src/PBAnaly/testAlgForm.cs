using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PBBiologyVC;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Markup;
using System.Runtime.InteropServices;

namespace PBAnaly
{
    public partial class testAlgForm : Form
    {
        public testAlgForm()
        {
            InitializeComponent();
        }
        public Mat ConvertByteArrayToMat(byte[] imageData, int width, int height, MatType type)
        {
            Mat image = new Mat(height, width, type);
            if (type == MatType.CV_8UC1)
            {
                Marshal.Copy(imageData, 0, image.Data, width * height);
            }
            else if (type == MatType.CV_16UC1) 
            {
                Marshal.Copy(imageData, 0, image.Data, width * height*2);
            }
            return image;
        }
        public byte[] ConvertUShortArrayToByteArrayComplete(ushort[] ushortArray)
        {
            // 创建一个byte数组，长度是ushort数组的两倍
            byte[] byteArray = new byte[ushortArray.Length * 2];

            for (int i = 0, j = 0; i < ushortArray.Length; i++, j += 2)
            {
                // 高位byte
                byteArray[j+1] = (byte)(ushortArray[i] >> 8);
                // 低位byte
                byteArray[j] = (byte)(ushortArray[i] & 0xFF);
            }

            return byteArray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mat image = Cv2.ImRead("1.tif", ImreadModes.Unchanged);
            //Cv2.ImShow("image", image);
            //Cv2.WaitKey(0);
            Mat whiteBackgroundImg=new Mat();
            Scalar meanValue = Cv2.Mean(image);
            if (meanValue[0] < 10000) 
            {
                Cv2.BitwiseNot(image, whiteBackgroundImg);
            }
            else
            {
                whiteBackgroundImg = image.Clone();
            }
            //Cv2.ImShow("whiteBackgroundImg", whiteBackgroundImg);
            //Cv2.WaitKey(0);
            Mat input_cn1 = new Mat();
            whiteBackgroundImg.ConvertTo(input_cn1, MatType.CV_8U, 0.00390625);
          
            //Cv2.ImShow("whiteBackgroundImg", whiteBackgroundImg);
            //Cv2.WaitKey(0);
            if (image.Depth() != MatType.CV_8U)
            {
                Mat convertedImage = new Mat();
                image.ConvertTo(convertedImage, MatType.CV_8U);
                pictureBox1.Image = convertedImage.ToBitmap();
            }
            else
            {
                pictureBox1.Image = image.ToBitmap();
            }
            //// 读tiff文件
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
            List<RectVC> proteinRect = new List<RectVC>();
            List<_band_info> band_info = new List<_band_info>();
           

            unsafe
            {
                fixed (byte* p = byte_image)
                {
                   // proteinRect = dd.getProteinRectVC(p, (ushort)input_cn1.Width, (ushort)input_cn1.Height);
                }

                byte[] bytes = ConvertUShortArrayToByteArrayComplete(whiteBackgroundImg_image);
               
               
                fixed (byte* p = bytes)
                {
                  //  dd.getProteinBandsVC(p, 16, (ushort)input_cn1.Width, (ushort)input_cn1.Height, proteinRect,ref band_info);
                }
               // dd.adjustBands(band_info, 10);
                //dd.molecularWeightResult(ref proteinRect, ref band_info);
                Console.WriteLine();
            }


            //PBAnaly.LaneChartForm laneChartForm = new PBAnaly.LaneChartForm(proteinRect, band_info);
            //laneChartForm.Show();

        }
    }
}
