using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.UI
{
    public partial class LogForm : MaterialForm
    {
        string Inner_UserID;
        public LogForm(MaterialSkinManager materialSkinManager)
        {
            InitializeComponent();
            UIInit();
        }

        public MaterialSkinManager Inn_materialSkinManager;

        public void UIInit()
        {
            this.Text = string.Format("Current User : {0}", Inner_UserID);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            Inn_materialSkinManager = MaterialSkinManager.Instance; // 初始化 MaterialSkinManager 实例
            //Inn_materialSkinManager.AddFormToManage(this);  // 将要应用 Material Design 的窗体添加到管理列表中
            //Inn_materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;   // Theme 属性用来设置整体的主题
            //Inn_materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Cyan700, TextShade.WHITE);    // ColorScheme 属性来设置配色方案

            if (Inn_materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                dataGridView1.DefaultCellStyle.BackColor = Color.DimGray;
            }
            else
            {
                dataGridView1.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + $"OperatingRecord\\{DateTime.Now.ToString("yyyyMMdd") + ".csv"}";
            LoadCsvData(filePath);
        }

        private void LoadCsvData(string filePath)
        {
            // 检查文件是否存在
            if (File.Exists(filePath))
            {
                try
                {
                    // 读取CSV文件的所有行
                    var lines = File.ReadAllLines(filePath);

                    // 清空DataGridView之前的数据
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    if (lines.Length > 0)
                    {
                        // 使用第一行数据作为列标题
                        var headers = lines[0].Split(',');

                        // 添加列到DataGridView
                        foreach (var header in headers)
                        {
                            dataGridView1.Columns.Add(header, header);  // 第一个参数是列的Name，第二个是列的HeaderText
                        }

                        // 从第二行开始添加数据
                        for (int i = 1; i < lines.Length; i++)
                        {
                            var row = lines[i].Split(',');

                            // 将数据添加到DataGridView中
                            dataGridView1.Rows.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"读取CSV文件时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
