using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
    }
}
