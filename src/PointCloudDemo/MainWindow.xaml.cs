using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.IO;

namespace PointCloudDemo
{
    public partial class MainWindow : Window
    {
        private PointsVisual3D pointCloud;
        private MeshGeometry3D terrainMesh;
        private ModelVisual3D terrainModel;
        private ushort[,] depthData;
        private ushort[,] psurData = null;
        private ImageSource terrainTexture;
        private double zScale = 0.1;

        private int smoothingLevel = 0;
        private Vector3D[,] vertexNormals;
        private bool isShowingTerrain = false;
        private byte[] psurByte = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <param name="type">类型: 0 生物分析 1:泳道</param>
        public MainWindow(string path)
        {
            InitializeComponent();

            InitializeVisuals();
            if (path != "")
            {
                try
                {
                    var v = path.Split(',');
                    string languge = "0"; // 1是中文 0是英文
                    if (v[0] == "0") 
                    {
                       
                        languge = v[1];
                        
                        // 先读融合图  用于显示
                        string marge = v[2];
                      
                        string psue = v[3];
                       
                        psurData = ReadTiffData(psue);
                    
                        using (var bmp = new Bitmap(marge))
                        {
                            // 加载2D预览图
                            var bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = new MemoryStream(File.ReadAllBytes(marge));
                            bitmapImage.EndInit();
                            imagePreview.Source = bitmapImage;
                            terrainTexture = bitmapImage; // 保存为地形纹理
                            
                            ProcessTiffData(bmp);
                            UpdateZScale();
                            
                        }

                       
                       
                    }

                    if (languge == "1") // 默认是英文不需要做处理
                    {
                        cbTextureView.Content = "3D 视图";
                        tb_z_axis.Text = "Z 轴拉伸";
                        tb_smoothing.Text = "滤波";
                     //   gb_preview.Content = "预览";
                        tb_2d_preview.Text = "2D 预览图";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading file: {ex.Message}");
                }
            }
            this.Topmost = true;
        }
        private void InitializeVisuals()
        {
            // 设置3D视口背景
            viewport3D.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(240, 240, 240));

            // 创建灯光系统（保持原有代码）
            var lightVisual = new ModelVisual3D
            {
                Content = new Model3DGroup
                {
                    Children =
                    {
                        new AmbientLight(Colors.White),
                new DirectionalLight(Colors.White, new Vector3D(1, -1, -1))
                    }
                }
            };
            
            // 初始化点云和地形模型（不立即添加到视图）
            pointCloud = new PointsVisual3D
            {
                Color = Colors.Red,
                Size = 2
            };

            terrainMesh = new MeshGeometry3D();
            terrainModel = new ModelVisual3D
            {
                Content = new GeometryModel3D
                {
                    Geometry = terrainMesh,
                    Material = new DiffuseMaterial(new SolidColorBrush(Colors.SteelBlue))
                }
            };

            // 只添加灯光和初始视图（点云）
            viewport3D.Children.Add(lightVisual);
            viewport3D.Children.Add(pointCloud); // 初始显示点云
        }
        private void UpdateZScale()
        {
            if (depthData == null) return;

            if (cbTextureView.IsChecked == true)
                UpdateTerrainMesh();
            else
                UpdatePointCloud();
        }

        private void UpdatePointCloud()
        {
            var points = new Point3DCollection();
            int step = 4;
            int width = depthData.GetLength(0);
            int height = depthData.GetLength(1);

            for (int y = 0; y < height; y += step)
            {
                for (int x = 0; x < width; x += step)
                {
                    double z = depthData[x, y] * zScale;
                    points.Add(new Point3D(x, y, z));
                }
            }

            pointCloud.Points = points;
        }

        private void UpdateTerrainMesh()
        {
            terrainMesh.Positions = new Point3DCollection();
            terrainMesh.TriangleIndices = new Int32Collection();
            terrainMesh.TextureCoordinates = new PointCollection();
            terrainMesh.Normals = new Vector3DCollection();

            int step = 1;
            int width = depthData.GetLength(0);
            int height = depthData.GetLength(1);

            // 应用平滑处理
            ushort[,] smoothedData = ApplySmoothing(depthData, smoothingLevel);
            ushort[,] smoothedPsurData = null;
            if (psurData != null) 
            {
                smoothedPsurData = ApplySmoothing(psurData, smoothingLevel);
            }
            
            var positions = new List<Point3D>();
            var texCoords = new List<System.Windows.Point>();
            var indices = new List<int>();
            var normals = new List<Vector3D>();
           
            // 生成顶点数据
            for (int y = 0; y < height; y += step)
            {
                for (int x = 0; x < width; x += step)
                {
                    double z = 0;
                    if (smoothedPsurData != null)
                    {
                        if (smoothedPsurData[x, y] > 0)
                        {
                            z = smoothedData[x, y] * zScale;
                           
                        }

                    }
                    else 
                    {
                        z = smoothedData[x, y] * zScale;
                    }
                    positions.Add(new Point3D(x, height - y, z));

                    texCoords.Add(new System.Windows.Point(
                        (double)x / (width - 1),
                        (double)y / (height - 1)
                    ));
                }
            }

            // 生成三角形索引
            int cols = width;
            int rows = height;
            for (int y = 0; y < rows - 1; y++)
            {
                for (int x = 0; x < cols - 1; x++)
                {
                    int i = y * cols + x;
                    indices.Add(i);
                    indices.Add(i + cols);
                    indices.Add(i + 1);

                    indices.Add(i + 1);
                    indices.Add(i + cols);
                    indices.Add(i + cols + 1);
                }
            }

            // 计算法线
            CalculateNormals(positions, cols, rows, ref normals);

            terrainMesh.Positions = new Point3DCollection(positions);
            terrainMesh.TextureCoordinates = new PointCollection(texCoords);
            terrainMesh.TriangleIndices = new Int32Collection(indices);
            terrainMesh.Normals = new Vector3DCollection(normals);

            UpdateMaterial();
        }
        // 新增的法线计算方法
        private void CalculateNormals(List<Point3D> positions, int cols, int rows, ref List<Vector3D> normals)
        {
            vertexNormals = new Vector3D[cols, rows];

            // 遍历所有三角形计算面法线
            for (int i = 0; i < terrainMesh.TriangleIndices.Count; i += 3)
            {
                int i0 = terrainMesh.TriangleIndices[i];
                int i1 = terrainMesh.TriangleIndices[i + 1];
                int i2 = terrainMesh.TriangleIndices[i + 2];

                Point3D p0 = positions[i0];
                Point3D p1 = positions[i1];
                Point3D p2 = positions[i2];

                Vector3D v1 = p1 - p0;
                Vector3D v2 = p2 - p0;
                Vector3D normal = Vector3D.CrossProduct(v1, v2);
                normal.Normalize();

                // 累加到顶点法线
                vertexNormals[i0 % cols, i0 / cols] += normal;
                vertexNormals[i1 % cols, i1 / cols] += normal;
                vertexNormals[i2 % cols, i2 / cols] += normal;
                        }

            // 标准化法线
            foreach (var normal in vertexNormals)
            {
                Vector3D n = normal;
                n.Normalize();
                normals.Add(n);
            }
        }

        // 新增的平滑处理方法
        private ushort[,] ApplySmoothing(ushort[,] data, int level)
        {
            if (level == 0) return data;

            int width = data.GetLength(0);
            int height = data.GetLength(1);
            ushort[,] result = new ushort[width, height];

            int kernelSize = level * 2 + 1;
            int halfKernel = kernelSize / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int sum = 0;
                    int count = 0;

                    for (int ky = -halfKernel; ky <= halfKernel; ky++)
                    {
                        for (int kx = -halfKernel; kx <= halfKernel; kx++)
                        {
                            int nx = x + kx;
                            int ny = y + ky;
                            if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                            {
                                sum += data[nx, ny];
                                count++;
                            }
                        }
                    }
                    result[x, y] = (ushort)(sum / count);
                }
            }
            return result;
        }

        // 新增的材质更新方法
        private void UpdateMaterial()
        {
            Material material;

            if (terrainTexture != null)
            {
                material = new DiffuseMaterial(new ImageBrush(terrainTexture))
                {
                    AmbientColor = Colors.White // 增强纹理亮度
                };
            }
            else
            {
                // 无纹理时使用高对比度颜色
                material = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Color.FromRgb(70, 130, 180))) // 深蓝色
                {
                    AmbientColor = Colors.LightGray
                };
            }

    ((GeometryModel3D)terrainModel.Content).Material = material;
            ((GeometryModel3D)terrainModel.Content).BackMaterial = material;
        }
        private void InitializePointCloud()
        {
            pointCloud = new PointsVisual3D
            {
                Color = Colors.Red,
                Size = 2
            };
            viewport3D.Children.Add(pointCloud);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "TIFF Files|*.tif;*.tiff|BMP Files|*.bmp;*.bmp"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    psurData = null;
                    psurByte = null;
                    
                    using (var bmp = new Bitmap(dialog.FileName))
                    {
                        // 加载2D预览图
                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(File.ReadAllBytes(dialog.FileName));
                        bitmapImage.EndInit();
                        imagePreview.Source = bitmapImage;
                        terrainTexture = bitmapImage; // 保存为地形纹理

                        ProcessTiffData(bmp);
                        UpdateZScale();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading file: {ex.Message}");
                }
            }
        }

        private void ProcessTiffData(Bitmap bitmap)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;
            depthData = new ushort[width, height];
            
            var rect = new System.Drawing.Rectangle(0, 0, width, height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            unsafe
            {
                try
                {
                    byte* ptr = (byte*)data.Scan0;
                    int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

                    for (int y = 0; y < height; y++)
                    {
                        byte* row = ptr + y * data.Stride;
                        for (int x = 0; x < width; x++)
                        {
                            int offset = x * bytesPerPixel;

                            // 对于16-bit图像（每像素2字节），直接读取内存
                            if (bytesPerPixel == 2)
                            {
                                // 小端模式读取（低字节在前）
                                depthData[x, y] = (ushort)(row[offset] | (row[offset + 1] << 8));
                            }
                            else if (bytesPerPixel == 3) 
                            {
                                byte r = row[offset + 2];
                                byte g = row[offset + 1];
                                byte b = row[offset];
                                depthData[x, y] = (ushort)((r + g + b) / 3);
                            }
                            // 如果是32-bit ARGB（每像素4字节）
                            else if (bytesPerPixel == 4)
                            {
                                // 取RGB分量的平均值作为深度
                                byte r = row[offset + 2];
                                byte g = row[offset + 1];
                                byte b = row[offset];
                                depthData[x, y] = (ushort)((r + g + b) / 3);
                            }
                        }
                    }
                }
                finally
                {
                    bitmap.UnlockBits(data);
                }
            }
            
        }
        private ushort[,] ReadTiffData(string path)
        {
            var bitmap = new Bitmap(path);

            var width = bitmap.Width;
            var height = bitmap.Height;

            ushort[,] datas = new ushort[width , height];
            var rect = new System.Drawing.Rectangle(0, 0, width, height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            unsafe
            {
                try
                {
                    byte* ptr = (byte*)data.Scan0;
                    int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;

                    for (int y = 0; y < height; y++)
                    {
                        byte* row = ptr + y * data.Stride;
                        for (int x = 0; x < width; x++)
                        {
                            int offset = x * bytesPerPixel;

                            // 对于16-bit图像（每像素2字节），直接读取内存
                            if (bytesPerPixel == 2)
                            {
                                // 小端模式读取（低字节在前）
                                datas[x, y] = (ushort)(row[offset] | (row[offset + 1] << 8));
                            }
                            else if (bytesPerPixel == 3)
                            {
                                byte r = row[offset + 2];
                                byte g = row[offset + 1];
                                byte b = row[offset];
                                datas[x, y] = (ushort)((r + g + b) / 3);
                            }
                            // 如果是32-bit ARGB（每像素4字节）
                            else if (bytesPerPixel == 4)
                            {
                                // 取RGB分量的平均值作为深度
                                byte r = row[offset + 2];
                                byte g = row[offset + 1];
                                byte b = row[offset];
                                datas[x, y] = (ushort)((r + g + b) / 3);
                            }
                            
                        }
                    }
                }
                finally
                {
                    bitmap.UnlockBits(data);
                }
            }
            return datas;
        }

        private void GeneratePointCloud()
        {
            var points = new Point3DCollection();
            int step = 4; // Sampling step to reduce points
            double scale = 0.1; // Scale factor for Z-axis

            int width = depthData.GetLength(0);
            int height = depthData.GetLength(1);

            for (int y = 0; y < height; y += step)
            {
                for (int x = 0; x < width; x += step)
                {
                    double z = depthData[x, y] * scale;
                    points.Add(new Point3D(x, y, z));
                }
            }

            pointCloud.Points = points;
        }

        private void ImagePreview_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (imagePreview.Source is BitmapSource bmp && depthData != null)
            {
                var pos = e.GetPosition(imagePreview);
                int x = (int)(pos.X * bmp.PixelWidth / imagePreview.ActualWidth);
                int y = (int)(pos.Y * bmp.PixelHeight / imagePreview.ActualHeight);

                if (x >= 0 && x < bmp.PixelWidth && y >= 0 && y < bmp.PixelHeight)
                {
                    pixelInfo.Text = $"X: {x}, Y: {y}, Depth: {depthData[x, y]}";
                }
            }
        }
        private void CbTextureView_Checked(object sender, RoutedEventArgs e)
        {
            if (isShowingTerrain) return;
            if (!viewport3D.Children.Contains(terrainModel))
            {
                viewport3D.Children.Remove(pointCloud);
                viewport3D.Children.Add(terrainModel);
                
            }
            isShowingTerrain = true;
            UpdateTerrainMesh();
        }
        // 新增事件处理
      
        private void CbTextureView_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!isShowingTerrain) return;
            if (!viewport3D.Children.Contains(pointCloud))
            {
                viewport3D.Children.Remove(terrainModel);
                viewport3D.Children.Add(pointCloud);
            }
            isShowingTerrain = false;
            UpdatePointCloud();
        }

        private void SliderZScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            zScale = sliderZScale.Value;
            UpdateZScale();
        }

        // 新增的平滑级别选择事件处理
        private void CbSmoothing_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            smoothingLevel = cbSmoothing.SelectedIndex;
            if (depthData != null) UpdateTerrainMesh();
        }

    }
}