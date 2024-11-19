using PBAnaly.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.UI
{
    public partial class colorbarControl : UserControl
    {
        public colorbarControl()
        {
            InitializeComponent();
          
        }

        public string SetSytingName 
        {
            set { slb_name.Text = value; }
        }
        public void SetDrawBar(string type) 
        {
            Bitmap rotatedImage = null;
            switch (type)
            {
                case "YellowHot":
                    rotatedImage = Resources.YellowHot_1;
                  

                    break;
                case "Black_Red":
                    rotatedImage = Resources.Black_Blue_1;
                   
                    break;
                case "Black_Green":
                    rotatedImage = Resources.Black_Green_1;
                   
                    break;
                case "Black_Blue":
                    rotatedImage = Resources.Black_Blue_1;
                  
                    break;
                case "Black_Yley":
                    rotatedImage = Resources.Black_Yley_1;
                    
                    break;
                case "Black_SDS":
                    rotatedImage = Resources.Black_SDS_1;
                   
                    break;
                case "EtBr":
                    rotatedImage = Resources.EtBr_1;
                    
                    break;
                case "Pseudo":
                    rotatedImage = Resources.Pseudo_1;
                    
                    break;
                case "Gray":
                    rotatedImage = Resources.Gray;
                    
                    break;


            }
            if (rotatedImage != null)
            {
                pb_colorbar.Image = rotatedImage;
                pb_colorbar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            }
        }

        //public void SetHistogramGradient(int[] histom,int min,int max) 
        //{
        //    using (Graphics g = panel1.CreateGraphics())
        //    {
        //        panel1.Refresh(); // 清除之前的绘制
        //        Rectangle rect = new Rectangle(0, 0, 30, panel1.Height);
        //        byte StartgrayScaleValue = (byte)((min / 65535.0) * 255);
        //        byte EndgrayScaleValue = (byte)((max / 65535.0) * 255);
        //        Color startColor = Color.FromArgb(StartgrayScaleValue, StartgrayScaleValue, StartgrayScaleValue);
        //        Color endColor = Color.FromArgb(EndgrayScaleValue, EndgrayScaleValue, EndgrayScaleValue);

        //        using (LinearGradientBrush brush = new LinearGradientBrush(rect, endColor, startColor, LinearGradientMode.Vertical))
        //        {
        //            g.FillRectangle(brush, rect);
        //        }

        //        Pen pen = new Pen(Color.Black, 1);
        //        Font font = new Font("Arial", 8);
        //        int diff = max - min;
        //        int numberOfTicks = CalculateNumberOfTicks(diff);  // 计算刻度数量

        //        // 循环现在从1开始并在numberOfTicks-1结束，避免绘制首尾标签
        //        for (int i = 1; i < numberOfTicks; i++)
        //        {
        //            int labelValue = max - (diff / numberOfTicks * i);
        //            int y = rect.Top + (i * (rect.Height / numberOfTicks));
        //            g.DrawLine(pen, rect.Right, y, rect.Right + 5, y); // 绘制刻度
        //            string labelText = labelValue.ToString();
        //            g.DrawString(labelText, font, Brushes.Black, rect.Right + 10, y - font.Height / 2);
        //        }
        //    }

        //}
        //private int CalculateNumberOfTicks(int diff)
        //{
        //    if (diff > 10000) return 10;
        //    if (diff > 5000) return 8;
        //    if (diff > 1000) return 5;
        //    return 3; // 最小的刻度数量
        //}


    }
}
