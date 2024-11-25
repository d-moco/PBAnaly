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
using System.Net;


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


        private static System.Drawing.Rectangle GetImageRectangle(PictureBox pictureBox)
        {
            var container = pictureBox.ClientRectangle;
            var image = pictureBox.Image;
            if (image == null)
                return System.Drawing.Rectangle.Empty;

            var imageSize = image.Size;
            var fitSize = new System.Drawing.Rectangle(0, 0, container.Width, container.Height);

            switch (pictureBox.SizeMode)
            {
                case PictureBoxSizeMode.Normal:
                case PictureBoxSizeMode.AutoSize:
                    fitSize.Size = imageSize;
                    break;
                case PictureBoxSizeMode.StretchImage:
                    break;
                case PictureBoxSizeMode.CenterImage:
                    fitSize.X = (container.Width - imageSize.Width) / 2;
                    fitSize.Y = (container.Height - imageSize.Height) / 2;
                    fitSize.Size = imageSize;
                    break;
                case PictureBoxSizeMode.Zoom:
                    float r = Math.Min((float)container.Width / imageSize.Width, (float)container.Height / imageSize.Height);
                    fitSize.Width = (int)(imageSize.Width * r);
                    fitSize.Height = (int)(imageSize.Height * r);
                    fitSize.X = (container.Width - fitSize.Width) / 2;
                    fitSize.Y = (container.Height - fitSize.Height) / 2;
                    break;
            }
            return fitSize;
        }
        public static System.Drawing.Point ConvertRealToPictureBox(System.Drawing.Point realPoint, PictureBox pictureBox)
        {
            var rect = GetImageRectangle(pictureBox);
            var scaleX = (float)rect.Width / pictureBox.Image.Width;
            var scaleY = (float)rect.Height / pictureBox.Image.Height;
            return new System.Drawing.Point((int)(realPoint.X * scaleX) + rect.Left, (int)(realPoint.Y * scaleY) + rect.Top);
        }

        public static System.Drawing.Rectangle ConvertRealRectangleToPictureBox(System.Drawing.Rectangle realRect, PictureBox pictureBox)
        {
            var topLeft = ConvertRealToPictureBox(new System.Drawing.Point(realRect.Left, realRect.Top), pictureBox);
            var bottomRight = ConvertRealToPictureBox(new System.Drawing.Point(realRect.Right, realRect.Bottom), pictureBox);
            return new System.Drawing.Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }

        public static System.Drawing.Point ConvertPictureBoxToReal(System.Drawing.Point pictureBoxPoint, PictureBox pictureBox)
        {
            var rect = GetImageRectangle(pictureBox);
            var scaleX = pictureBox.Image.Width / (float)rect.Width;
            var scaleY = pictureBox.Image.Height / (float)rect.Height;
            var x = (int)((pictureBoxPoint.X - rect.Left) * scaleX);
            var y = (int)((pictureBoxPoint.Y - rect.Top) * scaleY);
            return new System.Drawing.Point(x, y);
        }

        public static System.Drawing.Rectangle ConvertPictureBoxRectangleToReal(System.Drawing.Rectangle pictureBoxRect, PictureBox pictureBox)
        {
            var topLeft = ConvertPictureBoxToReal(new System.Drawing.Point(pictureBoxRect.Left, pictureBoxRect.Top), pictureBox);
            var bottomRight = ConvertPictureBoxToReal(new System.Drawing.Point(pictureBoxRect.Right, pictureBoxRect.Bottom), pictureBox);
            return new System.Drawing.Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }


        public static bool IsNearCorner(System.Drawing.Point point, System.Drawing.Point corner, int tolerance)
        {
            return Math.Abs(point.X - corner.X) <= tolerance && Math.Abs(point.Y - corner.Y) <= tolerance;
        }

        public static void DrawCircle(Graphics g, System.Drawing.Point center, int radius, Pen pen, Brush brush)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            g.FillEllipse(brush, rect);
            g.DrawEllipse(pen, rect);
        }

        public static bool IsPointInCircle(System.Drawing.Point point, System.Drawing.Point circle,int CircleRadius)
        {
            double distance = Math.Sqrt(Math.Pow(point.X - circle.X, 2) + Math.Pow(point.Y - circle.Y, 2));
            return distance <= CircleRadius;
        }

        public static bool IsPointOnLine(System.Drawing.Point point, System.Drawing.Point start, System.Drawing.Point end,int CircleRadius)
        {

            // 使用线段的最小距离判断点是否在线段上
            double distance = DistanceToLine(point, start, end);
            return distance <= CircleRadius; // 如果距离小于等于圆圈半径，认为在线段上
        }

        public static double DistanceToLine(System.Drawing.Point p, System.Drawing.Point p1, System.Drawing.Point p2)
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
    }


   


}