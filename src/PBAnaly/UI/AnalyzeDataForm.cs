using MaterialSkin.Controls;
using PBBiologyVC;
//using ReaLTaiizor.Util;
//using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.UI
{
    public partial class AnalyzeDataForm : MaterialForm
    {
        private List<_band_info> band_info;
        private List<TopHeader> _headers = new List<TopHeader>();

        public struct headInfo 
        {
            public int index;
            public List<string> name;
        }
        public List<TopHeader> Headers
        {
            get { return _headers; }
        }

        public struct TopHeader
        {
            public TopHeader(int index, int span, string text)
            {
                this.Index = index;
                this.Span = span;
                this.Text = text;
            }
            public int Index;
            public int Span;
            public string Text;
        }


        public AnalyzeDataForm(List<_band_info> _band_info)
        {
            InitializeComponent();
            band_info = _band_info;
            Draw();

            this.dataGridView1.CellPainting += DataGridView1_CellPainting;

        }


        public void Draw() 
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
            this.dataGridView1.RowCount = 1;
            this.dataGridView1.ColumnCount = band_info.Count*7 + 1;
            this.dataGridView1.Columns[0].HeaderText = "行/列";
            int index_c = 0;
            for (int i = 1; i < this.dataGridView1.ColumnCount; i=i+7)
            {
                this.dataGridView1.Columns[i].HeaderText ="分子量";
                this.dataGridView1.Columns[i+1].HeaderText = "条带含量";
                this.dataGridView1.Columns[i+2].HeaderText = "相对含量";
                this.dataGridView1.Columns[i+3].HeaderText = "IOD";
                this.dataGridView1.Columns[i+4].HeaderText = "最大OD";
                this.dataGridView1.Columns[i+5].HeaderText = "百分比";
                this.dataGridView1.Columns[i + 6].HeaderText = "匹配";
            }
            _headers = new List<TopHeader>();
            int index = 1;
            for (int i = 1; i < this.dataGridView1.ColumnCount; i=i+7)
            {
               
                TopHeader topHeader = new TopHeader(i, 7, "泳道" + index++);
                _headers.Add(topHeader);
                
            }
            int r_index = 0;
            int c_index = 1;
            this.dataGridView1.Rows[0].Cells[0].Value = "r1";
            foreach (var _bi in band_info) 
            {
                foreach (var minfo in _bi.Minfo)
                {
                    this.dataGridView1.Rows[r_index].Cells[c_index++].Value = minfo.molecular_weight;
                    this.dataGridView1.Rows[r_index].Cells[c_index++].Value = minfo.band_content;
                    this.dataGridView1.Rows[r_index].Cells[c_index++].Value = minfo.relative_content;
                    this.dataGridView1.Rows[r_index].Cells[c_index++].Value = minfo.IOD;
                    this.dataGridView1.Rows[r_index].Cells[c_index++].Value = minfo.maxOD;
                    this.dataGridView1.Rows[r_index].Cells[c_index++].Value = minfo.percentum;
                    this.dataGridView1.Rows[r_index].Cells[c_index++].Value = minfo.match;
                   
                }
             
            }

            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridView1.ColumnHeadersHeight = 50;
           
           this.dataGridView1.Refresh();
        }

        int top = 0;
        int left = 0;
        int height = 0;
        int width1 = 0;
        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            #region 重绘datagridview表头
            DataGridView dgv = (DataGridView)(sender);
            if (e.RowIndex != -1) return;
            foreach (TopHeader item in Headers)
            {
                if (e.ColumnIndex >= item.Index && e.ColumnIndex < item.Index + item.Span)
                {
                    if (e.ColumnIndex == item.Index)
                    {
                        top = e.CellBounds.Top;
                        left = e.CellBounds.Left;
                        height = e.CellBounds.Height;
                    }
                    int width = 0;//总长度
                    for (int i = item.Index; i < item.Span + item.Index; i++)
                    {
                        width += dgv.Columns[i].Width;
                    }
                    Rectangle rect = new Rectangle(left, top, width, e.CellBounds.Height);
                    using (Brush backColorBrush = new SolidBrush(e.CellStyle.BackColor)) //Cell背景颜色
                    {
                        //抹去原来的cell背景
                        e.Graphics.FillRectangle(backColorBrush, rect);
                    }
                    using (Pen gridLinePen = new Pen(dgv.GridColor)) //画笔颜色
                    {
                        e.Graphics.DrawLine(gridLinePen, left, top, left + width, top);
                        e.Graphics.DrawLine(gridLinePen, left, top + height / 2, left + width, top + height / 2);
                        e.Graphics.DrawLine(gridLinePen, left, top + height - 1, left + width, top + height - 1); //自定义区域下部横线
                        width1 = 0;
                        e.Graphics.DrawLine(gridLinePen, left - 1, top, left - 1, top + height);
                        for (int i = item.Index; i < item.Span + item.Index; i++)
                        {
                            if (i == 1 || i == 2)
                            {
                                width1 += dgv.Columns[i].Width - 1; //分隔区域首列
                            }
                            else
                            {
                                width1 += dgv.Columns[i].Width;
                            }
                            e.Graphics.DrawLine(gridLinePen, left + width1, top + height / 2, left + width1, top + height);
                        }
                        SizeF sf = e.Graphics.MeasureString(item.Text, e.CellStyle.Font);
                        float lstr = (width - sf.Width) / 2;
                        float rstr = (height / 2 - sf.Height) / 2;
                        //画出文本框
                        if (item.Text != "")
                        {
                            e.Graphics.DrawString(item.Text, e.CellStyle.Font,
                                                        new SolidBrush(e.CellStyle.ForeColor),
                                                            left + lstr,
                                                            top + rstr,
                                                            StringFormat.GenericDefault);
                        }
                        width = 0;
                        width1 = 0;
                        for (int i = item.Index; i < item.Span + item.Index; i++)
                        {
                            string columnValue = dgv.Columns[i].HeaderText;
                            width1 = dgv.Columns[i].Width;
                            sf = e.Graphics.MeasureString(columnValue, e.CellStyle.Font);
                            lstr = (width1 - sf.Width) / 2;
                            rstr = (height / 2 - sf.Height) / 2;
                            if (columnValue != "")
                            {
                                e.Graphics.DrawString(columnValue, e.CellStyle.Font,
                                                            new SolidBrush(e.CellStyle.ForeColor),
                                                                left + width + lstr,
                                                                top + height / 2 + rstr,
                                                                StringFormat.GenericDefault);
                            }
                            width += dgv.Columns[i].Width;
                        }
                    }
                    e.Handled = true;
                }
            }
        #endregion




        }
    }
}
