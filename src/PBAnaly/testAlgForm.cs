using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PBBiologyVC;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PBAnaly
{
    public partial class testAlgForm : Form
    {
        public testAlgForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mat image = Cv2.ImRead("1.tif", ImreadModes.Unchanged);

            Cv2.ImShow("a", image);
            Cv2.WaitKey(0);
            // 读tiff文件
            byte[] byte_image = image.ToBytes();

            byte[] kk = new byte[1028*800];
            PBBiology dd =new PBBiology();
            List <RectVC> l = new List<RectVC>();
            unsafe
            {
                fixed (byte* p = byte_image)
                {
                     l = dd.getProteinRectVC(p, (ushort)image.Width, (ushort)image.Height);
                }

                Console.WriteLine();
            }




        }
    }
}
