using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PBAnaly.Util
{
    public class DrawComm
    {
        /// <summary>
        /// 在图像上绘制中文文本
        /// </summary>
        /// <param name="image">目标图像 (Mat 类型)</param>
        /// <param name="text">要绘制的中文文本</param>
        /// <param name="position">文本的起始坐标</param>
        /// <param name="color">文本颜色</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="padding">左、右边界的最小间距</param>
        public static Bitmap DrawChineseTextOnImage(Bitmap image, string text, System.Drawing.Point position, Color color, int fontSize, int padding = 20)
        {
            // 创建一个 Bitmap 对象
            Bitmap bitmap = image;
            // 检查是否为索引像素格式
            if (Image.IsAlphaPixelFormat(bitmap.PixelFormat) ||
                bitmap.PixelFormat == PixelFormat.Format1bppIndexed ||
                bitmap.PixelFormat == PixelFormat.Format4bppIndexed ||
                bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                // 创建一个新的非索引格式的 Bitmap
                Bitmap nonIndexedBitmap = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
                using (Graphics g = Graphics.FromImage(nonIndexedBitmap))
                {
                    g.DrawImage(bitmap, new Rectangle(0, 0, nonIndexedBitmap.Width, nonIndexedBitmap.Height));
                }
                bitmap.Dispose(); // 释放原始 Bitmap
                bitmap = nonIndexedBitmap; // 使用新的 Bitmap
            }
            // 使用 Graphics 对象来绘制中文文本
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // 清除背景为白色
                // g.Clear(System.Drawing.Color.White);

                // 设置字体
                Font font = new Font("Microsoft YaHei", fontSize);

                // 设置画笔颜色
                SolidBrush brush = new SolidBrush(color);

                // 计算文本的宽度
                SizeF textSize = g.MeasureString(text, font);

                // 计算左边距和右边距后检查是否超出边界
                if (position.X + textSize.Width + padding > image.Width)
                {
                    // 超出右边界，调整 x 坐标，确保文本与右边界有指定的间距
                    position.X = image.Width - (int)textSize.Width - padding;
                }

                // 检查左边界
                if (position.X - padding < 0)
                {
                    // 超出左边界，调整 x 坐标，确保文本与左边界有指定的间距
                    position.X = padding;
                }

                // 绘制文本
                g.DrawString(text, font, brush, position);
            }

            // 将 Bitmap 转换为字节数组
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            byte[] bytes = new byte[data.Stride * bitmap.Height];
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            // 清理资源
            bitmap.UnlockBits(data);
            return bitmap;
        }
        
    }
}
