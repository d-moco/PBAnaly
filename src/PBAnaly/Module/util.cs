using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Bmp;
using System.IO;
using System.Drawing;
using Image = SixLabors.ImageSharp.Image;
using System.Net.NetworkInformation;
using System.Management;



namespace PBAnaly.Module
{
    public class util
    {
        /// <summary>
        /// 将16bit unshort 拆成2 个8bit
        /// </summary>
        /// <param name="ushortArray"></param>
        /// <returns></returns>
        public static byte[] ConvertUShortArrayToByteArrayComplete(ushort[] ushortArray)
        {
            // 创建一个byte数组，长度是ushort数组的两倍
            byte[] byteArray = new byte[ushortArray.Length * 2];

            for (int i = 0, j = 0; i < ushortArray.Length; i++, j += 2)
            {
                // 高位byte
                byteArray[j + 1] = (byte)(ushortArray[i] >> 8);
                // 低位byte
                byteArray[j] = (byte)(ushortArray[i] & 0xFF);
            }

            return byteArray;
        }

        /// <summary>
        /// 数据转成mat
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Mat ConvertByteArrayToMat(byte[] imageData, int width, int height, MatType type)
        {
            Mat image = new Mat(height, width, type);
            if (type == MatType.CV_8UC1)
            {
                Marshal.Copy(imageData, 0, image.Data, width * height);
            }
            else if (type == MatType.CV_16UC1)
            {
                Marshal.Copy(imageData, 0, image.Data, width * height * 2);
            }
            return image;
        }

        public static byte[] TiffTo16BitGrayByteArray(string filePath)
        {
            using (Image<L16> image = Image.Load<L16>(filePath))
            {
                int width = image.Width;
                int height = image.Height;
                byte[] pixels = new byte[width * height * sizeof(ushort)];

                int index = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        ushort pixelValue = image[x, y].PackedValue;
                        BitConverter.GetBytes(pixelValue).CopyTo(pixels, index);
                        index += sizeof(ushort);
                    }
                }

                return pixels;
            }
        }
        public static Image<L16> LoadTiffAsL16(string filePath)
        {
            // 加载图像并确保其为16位灰度图像
            Image<L16> image = Image.Load<L16>(filePath);
            return image;
        }
        public static byte[] ConvertL16ImageToByteArray(Image<L16> image)
        {
            int width = image.Width;
            int height = image.Height;
            byte[] pixels = new byte[width * height * sizeof(ushort)];

            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 直接访问每个像素的值并转换为 byte[]
                    ushort pixelValue = image[x, y].PackedValue;
                    BitConverter.GetBytes(pixelValue).CopyTo(pixels, index);
                    index += sizeof(ushort);
                }
            }

            return pixels;
        }

        public static Bitmap ConvertL16ToBitmap(Image<L16> image)
        {
            using (var ms = new MemoryStream())
            {
                // 保存为 BMP 格式的内存流
                image.Save(ms, new BmpEncoder());
                ms.Position = 0; // 重置流的位置
                return new Bitmap(ms);
            }
        }
        public static Image<L16> ConvertByteArrayToL16Image(byte[] byteArray, int width, int height, int channels)
        {
            // 确保输入参数有效
            if (channels != 1)
            {
                throw new ArgumentException("通道数必须为1，适用于L16格式。");
            }

            // 创建一个新的Image<L16>对象
            Image<L16> image = new Image<L16>(width, height);

            // 填充图像数据
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 计算在字节数组中的位置
                    int pixelIndex = (y * width + x) * channels * sizeof(ushort);

                    // 确保byteArray足够大
                    if (pixelIndex + sizeof(ushort) > byteArray.Length)
                    {
                        throw new ArgumentException("字节数组长度不足以填充图像。");
                    }

                    // 从字节数组中读取16位灰度值
                    ushort pixelValue = BitConverter.ToUInt16(byteArray, pixelIndex);
                    image[x, y] = new L16(pixelValue);
                }
            }

            return image;
        }
        public static Image<L16> ConvertL8ToL16Image(Image<L8> inputImage)
        {
            int width = inputImage.Width;
            int height = inputImage.Height;

            // 创建一个新的 Image<L16> 对象
            Image<L16> outputImage = new Image<L16>(width, height);

            // 遍历每个像素，将 L8 格式转换为 L16 格式
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 获取 L8 格式的像素值
                    byte pixelValueL8 = inputImage[x, y].PackedValue;

                    // 将 8 位值扩展为 16 位
                    ushort pixelValueL16 = (ushort)(pixelValueL8 << 8); // 例如，简单左移

                    // 设置 L16 图像的像素值
                    outputImage[x, y] = new L16(pixelValueL16);
                }
            }

            return outputImage;
        }
        public static Image<L8> ConvertByteArrayToL8Image(byte[] byteArray, int width, int height, int channels)
        {
            // 验证通道数至少为1
            if (channels < 1)
            {
                throw new ArgumentException("通道数必须至少为1。");
            }

            // 创建一个新的 Image<L8> 对象
            Image<L8> image = new Image<L8>(Configuration.Default, width, height);

            // 填充图像数据
            int bytesPerSample = 1; // 每个样本（每通道）1字节
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int baseIndex = (y * width + x) * channels * bytesPerSample;
                    if (channels == 1)
                    {
                        // 单通道灰度
                        byte grayValue = byteArray[baseIndex];
                        image[x, y] = new L8(grayValue);
                    }
                    else if (channels >= 3)
                    {
                        // 三通道RGB，计算加权平均灰度值
                        float r = byteArray[baseIndex];
                        float g = byteArray[baseIndex + 1];
                        float b = byteArray[baseIndex + 2];
                        byte gray = (byte)(0.299 * r + 0.587 * g + 0.114 * b);
                        image[x, y] = new L8(gray);
                    }
                }
            }

            return image;
        }

        public static Image<L16> ConvertByteArrayToL16Image(byte[] byteArray, int width, int height)
        {
            // 确保输入参数有效
            if (byteArray.Length < width * height)
            {
                throw new ArgumentException("字节数组长度不足以填充图像。");
            }

            // 创建一个新的 Image<L16> 对象
            Image<L16> image = new Image<L16>(width, height);

            // 填充图像数据
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 获取 8 位的像素值
                    byte pixelValueL8 = byteArray[y * width + x];

                    // 将 8 位值扩展为 16 位
                    ushort pixelValueL16 = (ushort)(pixelValueL8 << 8); // 或者可以直接设置为 (ushort)pixelValueL8

                    // 设置到 L16 图像
                    image[x, y] = new L16(pixelValueL16);
                }
            }

            return image;
        }

        public static Bitmap ConvertL8ImageToBitmap(Image<L8> image)
        {
            using (var ms = new MemoryStream())
            {
                // 将 Image<L8> 保存到内存流中，格式为 BMP
                image.Save(ms, new BmpEncoder());
                ms.Position = 0; // 重置流位置

                // 使用内存流创建 Bitmap
                return new Bitmap(ms);
            }
        }

        public static Image<Rgb24> ConvertByteArrayToRgb24Image(byte[] byteArray, int width, int height, int channels)
        {
            // 验证通道数必须为3（RGB）
            if (channels != 3)
            {
                throw new ArgumentException("通道数必须为3，适用于RGB彩色图像。");
            }

            // 创建一个新的 Image<Rgb24> 对象
            Image<Rgb24> image = new Image<Rgb24>(width, height);

            // 填充图像数据
            int bytesPerPixel = channels; // 每个像素的字节数为3（R、G、B）
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 计算当前像素在字节数组中的起始位置
                    int baseIndex = (y * width + x) * bytesPerPixel;

                    // 确保数组边界
                    if (baseIndex + 2 >= byteArray.Length)
                    {
                        throw new ArgumentException("字节数组长度不足以填充图像。");
                    }

                    // 提取 R、G、B 值
                    byte r = byteArray[baseIndex];
                    byte g = byteArray[baseIndex + 1];
                    byte b = byteArray[baseIndex + 2];

                    // 设置图像像素
                    image[x, y] = new Rgb24(r, g, b);
                }
            }

            return image;
        }

        public static Bitmap ConvertRgb24ImageToBitmap(Image<Rgb24> image)
        {
            using (var ms = new MemoryStream())
            {
                // 将 Image<Rgb24> 保存到内存流中，格式为 BMP
                image.Save(ms, new BmpEncoder());
                ms.Position = 0; // 重置流位置

                // 使用内存流创建 Bitmap
                return new Bitmap(ms);
            }
        }
        public static Bitmap ConvertRgba64ImageToBitmap(Image<Rgba64> image)
        {
            // 获取图像的宽度和高度
            int width = image.Width;
            int height = image.Height;

            // 创建一个新的 Bitmap 对象
            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // 使用锁定的像素数据进行高效复制
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 获取当前像素的 RGBA 值
                    Rgba64 pixel = image[x, y];

                    // 将 Rgba64 的 16 位值转换为 8 位值
                    byte r = (byte)(pixel.R >> 8); // 右移8位，转换为8位
                    byte g = (byte)(pixel.G >> 8);
                    byte b = (byte)(pixel.B >> 8);
                    byte a = (byte)(pixel.A >> 8);

                    // 设置 Bitmap 中对应像素的颜色
                    bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(a, r, g, b));
                }
            }

            return bitmap;
        }
        public static Image<Rgb24> ConvertBgrByteArrayToRgb24Image(byte[] byteArray, int width, int height, int channels)
        {
            // 验证通道数必须为3（BGR）
            if (channels != 3)
            {
                throw new ArgumentException("通道数必须为3，适用于BGR彩色图像。");
            }

            // 创建一个新的 Image<Rgb24> 对象
            Image<Rgb24> image = new Image<Rgb24>(width, height);

            // 填充图像数据
            int bytesPerPixel = channels; // 每个像素的字节数为3（B、G、R）
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 计算当前像素在字节数组中的起始位置
                    int baseIndex = (y * width + x) * bytesPerPixel;

                    // 确保数组边界
                    if (baseIndex + 2 >= byteArray.Length)
                    {
                        throw new ArgumentException("字节数组长度不足以填充图像。");
                    }

                    // 提取 B、G、R 值
                    byte b = byteArray[baseIndex];
                    byte g = byteArray[baseIndex + 1];
                    byte r = byteArray[baseIndex + 2];

                    // 设置图像像素（注意转换为 RGB 顺序）
                    image[x, y] = new Rgb24(r, g, b);
                }
            }

            return image;
        }

        public static Image<Rgba64> ConvertByteArrayToRgba64Image(byte[] byteArray, int width, int height, int channels)
        {
            // 验证通道数必须为4（RGBA）
            if (channels != 4)
            {
                throw new ArgumentException("通道数必须为4，适用于RGBA彩色图像。");
            }

            // 创建一个新的 Image<Rgba64> 对象
            Image<Rgba64> image = new Image<Rgba64>(width, height);

            // 每个通道的字节数为2（因为是16位）
            int bytesPerPixel = channels * 2; // 每个像素的字节数（4通道，每个通道2字节）

            // 填充图像数据
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 计算当前像素在字节数组中的起始位置
                    int baseIndex = (y * width + x) * bytesPerPixel;

                    // 确保数组边界
                    if (baseIndex + 7 >= byteArray.Length)
                    {
                        throw new ArgumentException("字节数组长度不足以填充图像。");
                    }

                    // 提取 R、G、B、A 值（每个通道是16位，即2个字节）
                    ushort r = (ushort)((byteArray[baseIndex] << 8) | byteArray[baseIndex + 1]);
                    ushort g = (ushort)((byteArray[baseIndex + 2] << 8) | byteArray[baseIndex + 3]);
                    ushort b = (ushort)((byteArray[baseIndex + 4] << 8) | byteArray[baseIndex + 5]);
                    ushort a = (ushort)((byteArray[baseIndex + 6] << 8) | byteArray[baseIndex + 7]);

                    // 设置图像像素（RGBA顺序）
                    image[x, y] = new Rgba64(r, g, b, a);
                }
            }

            return image;
        }

        public static Image<Rgba64> ConvertByteArrayToRgba64Image(short[] byteArray, int width, int height, int channels)
        {
            // 验证通道数必须为4（RGBA）
            if (channels != 4)
            {
                throw new ArgumentException("通道数必须为4，适用于RGBA彩色图像。");
            }

            // 创建一个新的 Image<Rgba64> 对象
            Image<Rgba64> image = new Image<Rgba64>(width, height);

            // 填充图像数据
            int totalPixels = width * height;

            // 填充图像数据
            for (int i = 0; i < totalPixels; i++)
            {
                // 计算当前像素在字节数组中的起始位置
                int baseIndex = i * channels;

                // 确保数组边界
                if (baseIndex + 3 >= byteArray.Length)
                {
                    throw new ArgumentException("字节数组长度不足以填充图像。");
                }

                // 提取 R、G、B、A 值
                ushort r = (ushort)(byteArray[baseIndex] & 0xFFFF); // 读取 R 通道
                ushort g = (ushort)(byteArray[baseIndex + 1] & 0xFFFF); // 读取 G 通道
                ushort b = (ushort)(byteArray[baseIndex + 2] & 0xFFFF); // 读取 B 通道
                ushort a = (ushort)(byteArray[baseIndex + 3] & 0xFFFF); // 读取 A 通道

                // 设置图像像素（RGBA顺序）
                image[i % width, i / width] = new Rgba64(r, g, b, a);
            }

            return image;
        }

        #region 读取WMIMac地址
        public static string GetMotherboardSerial()
        {
            string serialNumber = string.Empty;
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                foreach (ManagementObject mo in searcher.Get())
                {
                    serialNumber = mo["SerialNumber"].ToString();
                    break; // 只需一个序列号
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取主板序列号时发生错误: " + ex.Message);
            }
            return serialNumber;
        }
        #endregion

        public static string GetscientificNotation(float diff) 
        {
            int exponent = (int)Math.Floor(Math.Log10(diff));
            float normalized = (float)(diff / Math.Pow(10, exponent));

            // 以科学计数法格式化输出
            string scientificNotation = $"{normalized}e{exponent}";
            return scientificNotation ;
        }
    }
}
