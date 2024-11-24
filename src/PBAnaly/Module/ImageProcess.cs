using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using AntdUI;
using System.Windows.Forms;
using OpenCvSharp;
using SixLabors.ImageSharp.Advanced;
using System.Runtime.InteropServices;
using Sunny.UI.Win32;


namespace PBAnaly.Module
{
    public class DataRecordString 
    {
        public int index { get; set; }
        public string max { get; set; }
        public string min { get; set; }
        public string IOD { get; set; }
        public string Count { get; set; }
        public string AOD { get; set; }
    }
    public class DataRecord
    {
        public int index { get; set; }
        public int max { get; set; }    
        public int min { get; set; }
        public int IOD { get; set; }
        public int Count { get; set; }
        public float AOD { get; set; }
    };
    public static class ImageProcess
    {

        public static (int[] histogram, int minVal, int maxVal) CalculateHistogram(Image<L16> image)
        {
            int[] histogram = new int[65536]; // 16-bit image has values from 0 to 65535
            int minVal = ushort.MaxValue;
            int maxVal = ushort.MinValue;

            // 使用MemoryGroup访问像素数据
            var memoryGroup = image.GetPixelMemoryGroup();
            var o = memoryGroup.ToArray().Max();

            foreach (var memory in memoryGroup)
            {
                var span = memory.Span;
                foreach (var pixel in span)
                {
                    ushort pixelValue = pixel.PackedValue;
                    histogram[pixelValue]++; // 增加对应亮度值的计数

                    if (pixelValue < minVal)
                        minVal = pixelValue; // 更新最小值

                    if (pixelValue > maxVal)
                        maxVal = pixelValue; // 更新最大值
                }
            }

            return (histogram, minVal, maxVal);
        }

        public static System.Drawing.Point GetRealImageCoordinates(PictureBox pictureBox, System.Drawing.Point mousePoint)
        {
            if (pictureBox.Image == null)
                return mousePoint;

            System.Drawing.Size imageSize = pictureBox.Image.Size;
            System.Drawing.Size boxSize = pictureBox.ClientSize;

            float imageAspectRatio = (float)imageSize.Width / imageSize.Height;
            float boxAspectRatio = (float)boxSize.Width / boxSize.Height;

            float scaleFactor = (pictureBox.SizeMode == PictureBoxSizeMode.Zoom) ?
                                (boxAspectRatio > imageAspectRatio ? (float)boxSize.Height / imageSize.Height : (float)boxSize.Width / imageSize.Width) :
                                (float)boxSize.Width / imageSize.Width; // StretchImage模式

            // 图像在PictureBox内居中，因此计算起始偏移
            int imageX = (pictureBox.SizeMode == PictureBoxSizeMode.Zoom && boxAspectRatio > imageAspectRatio) ?
                         (boxSize.Width - (int)(imageSize.Width * scaleFactor)) / 2 : 0;
            int imageY = (pictureBox.SizeMode == PictureBoxSizeMode.Zoom && boxAspectRatio < imageAspectRatio) ?
                         (boxSize.Height - (int)(imageSize.Height * scaleFactor)) / 2 : 0;

            int realX = (int)((mousePoint.X - imageX) / scaleFactor);
            int realY = (int)((mousePoint.Y - imageY) / scaleFactor);

            return new System.Drawing.Point(realX, realY);
        }


        public static System.Drawing.Rectangle GetRealImageRectangle(PictureBox pictureBox, System.Drawing.Rectangle mouseRectangle)
        {
            if (pictureBox.Image == null)
                return mouseRectangle;

            // 获得矩形的左上角和右下角的坐标
            System.Drawing.Point topLeft = new System.Drawing.Point(mouseRectangle.Left, mouseRectangle.Top);
            System.Drawing.Point bottomRight = new System.Drawing.Point(mouseRectangle.Right, mouseRectangle.Bottom);

            // 转换这些坐标
            System.Drawing.Point realTopLeft = GetRealImageCoordinates(pictureBox, topLeft);
            System.Drawing.Point realBottomRight = GetRealImageCoordinates(pictureBox, bottomRight);

            // 计算新的矩形的宽度和高度
            int width = realBottomRight.X - realTopLeft.X;
            int height = realBottomRight.Y - realTopLeft.Y;

            return new System.Drawing.Rectangle(realTopLeft.X, realTopLeft.Y, width, height);
        }

        // 将真实图像坐标转换为PictureBox控件坐标
        public static System.Drawing.Point ConvertRealToPictureBoxCoordinates(PictureBox pictureBox, System.Drawing.Point realPoint)
        {
            if (pictureBox.Image == null)
                return realPoint;

            System.Drawing.Size imageSize = pictureBox.Image.Size;
            System.Drawing.Size boxSize = pictureBox.ClientSize;

            float imageAspectRatio = (float)imageSize.Width / imageSize.Height;
            float boxAspectRatio = (float)boxSize.Width / boxSize.Height;

            float scaleFactor = (pictureBox.SizeMode == PictureBoxSizeMode.Zoom) ?
                                (boxAspectRatio > imageAspectRatio ? (float)boxSize.Height / imageSize.Height : (float)boxSize.Width / imageSize.Width) :
                                1f; // 默认为1，这里假设是StretchImage模式

            int imageX = (pictureBox.SizeMode == PictureBoxSizeMode.Zoom && boxAspectRatio > imageAspectRatio) ?
                         (boxSize.Width - (int)(imageSize.Width * scaleFactor)) / 2 : 0;
            int imageY = (pictureBox.SizeMode == PictureBoxSizeMode.Zoom && boxAspectRatio < imageAspectRatio) ?
                         (boxSize.Height - (int)(imageSize.Height * scaleFactor)) / 2 : 0;

            int pictureBoxX = (int)(realPoint.X * scaleFactor) + imageX;
            int pictureBoxY = (int)(realPoint.Y * scaleFactor) + imageY;

            return new System.Drawing.Point(pictureBoxX, pictureBoxY);
        }

        // 将真实图像的矩形坐标转换为PictureBox控件的矩形坐标
        public static System.Drawing.Rectangle ConvertRealToPictureBoxRectangle(PictureBox pictureBox, System.Drawing.Rectangle realRectangle)
        {
            if (pictureBox.Image == null)
                return realRectangle;

            System.Drawing.Point realTopLeft = new System.Drawing.Point(realRectangle.Left, realRectangle.Top);
            System.Drawing.Point realBottomRight = new System.Drawing.Point(realRectangle.Right, realRectangle.Bottom);

            System.Drawing.Point picBoxTopLeft = ConvertRealToPictureBoxCoordinates(pictureBox, realTopLeft);
            System.Drawing.Point picBoxBottomRight = ConvertRealToPictureBoxCoordinates(pictureBox, realBottomRight);

            return new System.Drawing.Rectangle(picBoxTopLeft.X, picBoxTopLeft.Y,
                                 picBoxBottomRight.X - picBoxTopLeft.X,
                                 picBoxBottomRight.Y - picBoxTopLeft.Y);
        }
    }


   


}