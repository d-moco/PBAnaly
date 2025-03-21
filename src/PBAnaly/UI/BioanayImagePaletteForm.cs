using Aspose.Pdf.Drawing;
using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PBAnaly.Assist;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace PBAnaly.UI
{
    public partial class BioanayImagePaletteForm : Form
    {
        public TextBox tb_min;
        public TextBox tb_max;
        private int roi_w = 20;
        private int roi_h = 20;
        private int circle_r = 10;
        public BioanayImagePaletteForm()
        {
            InitializeComponent();

            GlobalData.PropertyChanged += OnGlobalDataPropertyChanged;

            if (GlobalData.GetProperty("Language") == "Chinese")
            {
                SetLanguage("zh-CN");
            }
            else
            {
                SetLanguage("en-US");
            }
        }

        #region 对外接口
        public int ROI_W
        {
            get {
                roi_w = Convert.ToInt32(dtb_w.Text.ToString());
                return roi_w; 
            }
            set { roi_w = value; dtb_w.Text = roi_w.ToString(); }
        }
        public int ROI_H
        {
            get {
                roi_h = Convert.ToInt32(dtb_h.Text.ToString());
                return roi_h;
            }
            set { roi_h = value; dtb_h.Text = roi_h.ToString(); }
        }
        public int CIRCLE_R
        {
            get {
                circle_r = Convert.ToInt32(dtb_r.Text.ToString());
                return circle_r; 
            }
            set { circle_r = value; dtb_r.Text = circle_r.ToString(); }
        }

        public string SetInfo
        {
            set { flb_info.Text = value; flb_info.Refresh(); }
        }

        public void RefreshscientificON(bool scientificON) 
        {
            if (scientificON == false) 
            {

                pl_max.Controls.Clear();
                pl_min.Controls.Clear();
                if (tb_max == null) return;
                tb_max.Dispose();
                tb_min.Dispose();
                tb_max = null;
                tb_min = null;
                pl_max.Controls.Add(nud_colorMax);
                pl_min.Controls.Add(nud_colorMin);
            }
            else
            {
                pl_max.Controls.Clear();
                pl_min.Controls.Clear();
                if (tb_max == null) 
                {
                    tb_max = new TextBox();
                    tb_max.Dock = DockStyle.Fill;
                    tb_max.Enabled = false;
                    tb_max.Multiline = true;
                    tb_max.Text = nud_colorMax.Value.ToString("E");
                }
                if (tb_min == null)
                {
                    tb_min = new TextBox();
                    tb_min.Dock = DockStyle.Fill;
                    tb_min.Enabled = false;
                    tb_min.Multiline = true;
                    tb_min.Text = nud_colorMin.Value.ToString("E");
                }
                pl_max.Controls.Add(tb_max);
                pl_min.Controls.Add(tb_min);
            }
        }
        #endregion

        #region 中英文切换
        ResourceManager resourceManager;
        private void SetLanguage(string cultureCode)
        {
            resourceManager = new ResourceManager("PBAnaly.Properties.Resources", typeof(BioanayImagePaletteForm).Assembly);

            // 设置当前线程的文化信息
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            // 更新所有控件的文本
            UpdateControlsText();
        }

        // 更新所有控件的文本
        private void UpdateControlsText()
        {
            //// 遍历所有控件并更新文本
            foreach (Control control in this.Controls)
            {
                UpdateControlText(control);
            }
        }
        // 更新单个控件的文本
        private void UpdateControlText(Control control)
        {
            //// 直接通过控件的 Name 属性获取资源字符串
            string resourceText = resourceManager.GetString(control.Name);
            if (!string.IsNullOrEmpty(resourceText))
            {
                control.Text = resourceText;
            }

            // 如果控件包含子控件，则递归更新子控件
            foreach (Control subControl in control.Controls)
            {
                UpdateControlText(subControl);
            }
        }

        #region OnGlobalDataPropertyChanged 处理全局属性更改事件
        /// <summary> 
        /// 处理全局属性更改事件
        /// </summary>
        /// <param name="name">发生变化的属性名</param>
        /// <param name="value">更改的属性值</param>
        private void OnGlobalDataPropertyChanged(string name, string value)
        {
            switch (name)
            {
                case "Language":
                    if (GlobalData.GetProperty("Language") == "Chinese")
                    {
                        SetLanguage("zh-CN");
                    }
                    else
                    {
                        SetLanguage("en-US");
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #endregion


    }
}
