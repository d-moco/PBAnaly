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
using System.Globalization;
using System.Resources;
using System.Threading;
using PBAnaly.Assist;

namespace PBAnaly.UI
{
    public partial class LanesImagePaletteForm : Form
    {
        
        public LanesImagePaletteForm()
        {
            InitializeComponent();

            if (GlobalData.GetProperty("Language") == "Chinese")
            {
                SetLanguage("zh-CN");
            }
            else
            {
                SetLanguage("en-US");
            }
        }

        #region 中英文切换
        ResourceManager resourceManager;
        private void SetLanguage(string cultureCode)
        {
            resourceManager = new ResourceManager("PBAnaly.Properties.Resources", typeof(MainForm).Assembly);

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

        #endregion
    }
}
