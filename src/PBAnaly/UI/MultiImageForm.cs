using AntdUI;
using ImageMagick;
using OpenCvSharp.Flann;
using PBAnaly.Assist;
using PBAnaly.Module;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace PBAnaly.UI
{
    public partial class MultiImageForm : AntdUI.BaseForm
    {
        public SixLabors.ImageSharp.Image<L16> curImage;
        private string path;
        private int pindex = 0;
        private List<SixLabors.ImageSharp.Image<L16>> imageList = new List<SixLabors.ImageSharp.Image<L16>>();
        public MultiImageForm(string _path)
        {
            InitializeComponent();
            this.path = _path;
            cb_path.Items.Add(path);
            cb_path.SelectedIndex = 0;
            ReadTiff();

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


        #region 中英文切换
        ResourceManager resourceManager;
        private void SetLanguage(string cultureCode)
        {
            resourceManager = new ResourceManager("PBAnaly.Properties.Resources", typeof(MultiImageForm).Assembly);

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


        private void ReadTiff()
        {
            imageList.Clear();
            using (MagickImageCollection images = new MagickImageCollection(path))
            {
                Console.WriteLine($"图像包含 {images.Count} 帧");

             

                // 遍历每一帧图像
                for (int i = 0; i < images.Count; i++)
                {
                    // 获取当前帧图像（MagickImage）
                    MagickImage magickImage = (MagickImage)images[i];

                    // 使用 MemoryStream 将 Magick.NET 图像转换为 ImageSharp 图像
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // 将 Magick.NET 图像保存到内存流
                        magickImage.Write(ms);
                        ms.Seek(0, SeekOrigin.Begin);

                        // 使用 ImageSharp 从内存流中加载图像
                        SixLabors.ImageSharp.Image<L16> image = SixLabors.ImageSharp.Image.Load<L16>(ms);
                        imageList.Add(image);
                        
                    }
                }

                if (imageList.Count > 0) 
                {
                    pindex = 0;
                    var bitmap = util.ConvertL16ToBitmap(imageList[0]);
                    pb_image.Image = bitmap;
                    lb_lable.Text = $"{1}/{imageList.Count}";
                }

            }
        }

        private void ab_one_Click(object sender, EventArgs e)
        {
            pindex = 0;
            RefreshImage(pindex);
        }
        private void ab_last_Click(object sender, EventArgs e)
        {
            if (pindex <= 0) return;

            pindex--;
            RefreshImage(pindex);
        }

        private void ab_next_Click(object sender, EventArgs e)
        {
            if (pindex >= imageList.Count-1) return;
            pindex++;
            RefreshImage(pindex);
        }
        private void ab_atLast_Click(object sender, EventArgs e)
        {
            pindex = imageList.Count -1;
            RefreshImage(pindex);
        }
        private void RefreshImage(int index) 
        {
            if (imageList.Count > 0)
            {
                var bitmap = util.ConvertL16ToBitmap(imageList[index]);
                pb_image.Image = bitmap;

                lb_lable.Text = $"{index+1}/{imageList.Count}";
            }
        }

        private void ab_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ab_open_cur_tif_Click(object sender, EventArgs e)
        {
            if (pindex < imageList.Count && pindex >= 0) 
            {
                curImage = imageList[pindex];
            }
             
            DialogResult = DialogResult.OK;
      
        }

        private void ab_saveTif_Click(object sender, EventArgs e)
        {
            if (pindex < imageList.Count && pindex>=0) 
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog()) 
                {
                    // 设置文件类型过滤器，确保用户只能选择 TIFF 文件
                    saveFileDialog.Filter = "TIFF 文件 (*.tif)|*.tif";
                    // 显示保存文件对话框
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string outputPath = saveFileDialog.FileName;

                        // 调用保存方法
                        SaveAsTiff(imageList[pindex], outputPath);
                        MessageBox.Show("图像已成功保存！", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        // 使用 Magick.NET 将 ImageSharp 图像保存为 TIFF 格式
        static void SaveAsTiff(Image<L16> image, string outputPath)
        {
            // 将 ImageSharp 图像转换为 MagickImage
            using (var ms = new MemoryStream())
            {
                // 使用 ImageSharp 将图像保存到内存流
                image.Save(ms, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                ms.Seek(0, SeekOrigin.Begin);

                // 使用 Magick.NET 从内存流加载图像
                using (MagickImage magickImage = new MagickImage(ms))
                {
                    // 设置图像的格式为 TIF 并保存
                    magickImage.Format = MagickFormat.Tif;
                    magickImage.Write(outputPath);
                }
            }
        }

        private void MultiImageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult != DialogResult.OK) 
            {
                this.DialogResult = DialogResult.Cancel;
            }
           
        }
    }
}
