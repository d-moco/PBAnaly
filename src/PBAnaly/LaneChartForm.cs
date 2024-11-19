using MaterialSkin.Controls;
using PBBiologyVC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Markup;
using System.Xml.Linq;
using static PBAnaly.Module.PBAnalyCommMannager;

namespace PBAnaly
{
    public partial class LaneChartForm : MaterialForm
    {
      
       
        public LaneChartForm()
        {
            InitializeComponent();
        } 

        public void Draw(band_infos band_info) 
        {
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.Annotations.Clear();
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 1;

            var xdata = band_info._Info.xdata;
            var ydata = band_info._Info.ydata;
            float maxY = ydata.Max();
            double maxX = xdata[ydata.IndexOf(maxY)];
            curmaxY = maxY;
            curmaxX = maxX;
            // 显著增加Y轴的显示范围以确保箭头可见
            chartArea.AxisY.Minimum = 0; // 适当设置以显示更低的值
            chartArea.AxisY.Maximum = maxY * 1.3; // 增加额外空间显示箭头
     
            chart1.ChartAreas.Add(chartArea);

            Series series = new Series
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2
            };

            for (int i = 0; i < xdata.Count; i++)
            {
                series.Points.AddXY(xdata[i], ydata[i]);
            }

            chart1.Series.Add(series);

            // 确保箭头和标注在可见范围
            chart1.ResetAutoValues();

            // 创建一个显著可见的箭头注释
            ArrowAnnotation arrow = new ArrowAnnotation();
            arrow.ArrowSize = 2; // 增大箭头大小
            arrow.ArrowStyle = ArrowStyle.Simple;
            arrow.LineColor = Color.Black; // 使用红色以增加可见性
            arrow.LineWidth = 2; // 增加线宽
            arrow.AnchorDataPoint = series.Points.FindByValue(maxY); // 直接锚定到最大值点
            arrow.Height = 10; // 增加箭头长度
            arrow.Width = 0; // 宽度为0
            arrow.AllowMoving = false; // 允许移动
            arrow.IsSizeAlwaysRelative = false; // 设置大小不总是相对的，以便可拖动
           
            chart1.Annotations.Add(arrow);


            int  x = band_info._Info.band_point[0][1];
            int x1 = band_info._Info.band_point[0][2];
            float curY = ydata[x];
            float curY1 = ydata[x1];
            float curX = xdata[x];
            //// 添加括号的文本注释
            TextAnnotation leftBracket = new TextAnnotation();
            leftBracket.Name = "left";
            leftBracket.Text = "[";
            leftBracket.Font = new Font("Arial", 12, FontStyle.Regular); // 设置字体样式
            leftBracket.ForeColor = Color.Black;
            var da = series.Points.FindAllByValue(curY);
            foreach (var d in da) 
            {
                if (d.XValue == curX) 
                {
                    leftBracket.AnchorDataPoint = d; // 直接锚定到最大值点
                    break;
                }
                
            }
            //leftBracket.AnchorDataPoint = series.Points.FindAllByValue*.FindByValue(curY); // 直接锚定到最大值点
            leftBracket.Height = 1; // 增加箭头长度
            leftBracket.Width =10; // 宽度为0


            chart1.Annotations.Add(leftBracket);

            TextAnnotation reghtBracket = new TextAnnotation();
            reghtBracket.Name = "right";
            reghtBracket.Text = "]";
            reghtBracket.Font = new Font("Arial", 12, FontStyle.Regular); // 设置字体样式
            reghtBracket.ForeColor = Color.Black;
            reghtBracket.AnchorDataPoint = series.Points.FindByValue(curY1); // 直接锚定到最大值点
            reghtBracket.Height = 1; // 增加箭头长度
            reghtBracket.Width = 10; // 宽度为0


            chart1.Annotations.Add(reghtBracket);

            chart1.MouseMove += chart1_MouseMove;

        }
        private bool isAnnotationMoving = false;
        private Annotation selectedAnnotation = null;
        private PointF startLocation;
        private float curmaxY = 0;
        private double curmaxX = 0;

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {

           
            var hit = chart1.HitTest(e.X, e.Y, ChartElementType.Annotation);

            if (hit.ChartElementType == ChartElementType.Annotation) 
            {
                var annotation = hit.Object as Annotation;
                if (annotation != null)
                {
                    if (annotation.Name == "left" || annotation.Name == "right")
                    {
                        chart1.Cursor = Cursors.SizeWE;
                    }
                }
              
            }
            else
            {
                chart1.Cursor = Cursors.Default;
            }


            if (isAnnotationMoving && selectedAnnotation != null) 
            {
                double xVal = 0;
                try
                {
                     xVal = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                }
                catch (Exception)
                {

                    return;
                }
                if (selectedAnnotation.Name == "left")
                {
                    if (xVal < 0 || xVal > curmaxX)
                    {
                        return;
                    }
                }
                else if (selectedAnnotation.Name == "right") 
                {
                    if (xVal < curmaxX || xVal> 1)
                    {
                        return;
                    }
                }
                
              

                int index = FindClosestPointIndex(xVal);
               
                if (index != -1)
                {
                    selectedAnnotation.AnchorDataPoint = chart1.Series[0].Points[index];
                    
                }
            }

       
        }

        private int FindClosestPointIndex(double xVal)
        {
            Series series = chart1.Series[0];
            double minDistance = double.MaxValue;
            int closestIndex = -1;

            for (int i = 0; i < series.Points.Count; i++)
            {
                double distance = Math.Abs(series.Points[i].XValue - xVal);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            var hit = chart1.HitTest(e.X, e.Y, ChartElementType.Annotation);

            if (hit.ChartElementType == ChartElementType.Annotation)
            {
                var annotation = hit.Object as Annotation;
                if (annotation != null)
                {
                    if (annotation.Name == "left" || annotation.Name == "right")
                    {
                        chart1.Cursor = Cursors.SizeWE;
                        isAnnotationMoving = true;
                        selectedAnnotation = annotation;
                        startLocation = new PointF(e.X, e.Y);
                        Console.WriteLine(annotation.Name);
                    }
                  
                }

            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isAnnotationMoving)
            {
                isAnnotationMoving = false;
                selectedAnnotation = null;
               
            }
        }

        private void LaneChartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();//窗体只被隐藏不被关闭
            e.Cancel = true;
        }
    }
}