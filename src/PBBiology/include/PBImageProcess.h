#pragma once
#include "PBType.h"
#include <opencv2/opencv.hpp>
#include <vector>
#include <algorithm>
#include <map>
#include <numeric>

using namespace std;
using namespace cv;


//生长函数
int RegionGrow(cv::Mat& src, cv::Mat& matDst, cv::Point2i pt, int th);
//最小二乘法取圆
void FitCircleCenter(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R);
//最优最小二乘法取圆
int RANSAC_FitCircleCenter(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R, float thresh);
//生长最优最小二乘法取圆
void RANSAC_FitCircleCenter_with_throw(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R);
//二值化阈值计算
int IJIsoData(int* data);
int defaultIsoData(int* data);



//合成渲染图像，src是老鼠图，pseudoImg是光子渲染图，brightness_offset亮度，contrast_factor对比度，contrast_factor透明度，返回融合图
//brightness_offset:亮度偏移范围 -255 到 +255
//contrast_factor:对比度因子范围 0.1 到 3.0（1.0为不变）
//opacity_factor:透明度因子范围 0 到 1（0为透明，1为不透明）
Mat render_mask_image(Mat src, Mat pseudoImg, int brightness_offset, double contrast_factor, double opacity_factor);
//获取颜色表，color颜色类型，bgr_tab是有空间的颜色表指针，reverse是否反转
void get_bgr_tab(ColorTable color, uint8_t(*bgr_tab)[3], bool reverse);
//生产颜色表的直条图，w=200,h_color=10是一个颜色高，bgr_tab是有空间的颜色表指针
Mat bgr_tab_image(int w, int h_onecolor, uint8_t(*bgr_tab)[3]);
//统计计算结果，src是输入图像，16bit的count图或者float的光子计算结果图都可以输入；mask是掩膜图；max和min是设定的大小
PseudoInfo get_pseudo_info(Mat src, Mat mask, float max, float min);
//生成光子渲染图，src是渲染前图，dst是渲染后图，max和min是设定的大小，bgr_tab是有空间的颜色表指针
int pseudo_color_processing(Mat src, Mat dst, float max, float min, uint8_t(*bgr_tab)[3]);
//生成带标尺的直条图，src是bgr_tab_image生成的图，maxVal和minVal是设定的大小，scientific_flag是否科学计数法
Mat bgr_scale_image(Mat src, float maxVal, float minVal, int scientific_flag);
//获取光子计算图，src输入渲染前原始图，sec是拍摄秒数，Wcm=27是实际宽，Hcm=18是实际高，sr是默认1.0；返回CV_32FC1的浮点光子结果图
Mat get_photon_image(Mat src, float sec, float Wcm, float Hcm, float sr);
//魔术棒功能，src是处理成8bit的图，x,y是点击位置的坐标，max和min是设定的大小,max和min需要注意除以256，使用0-255数据
//点击位置的像素差在[min,max]范围内的连在一起的像素，都会被框选
Mat get_magic_wand_image(Mat src,int x,int y,float max,float min);


// 锐化
Mat SetSharpen(Mat src);
//相机标定功能，gray是包含完整棋盘格灰度图像，patternSize是棋盘格内角点的数量（假如棋盘格的尺寸是9*7，那内角点就是8*6），grid_size是每个格子的物理大小
//cameraMatrix和distCoeffs是后续图像畸变矫正需要的参数，标定一次后，镜头无变动情况下，后续图像直接用此参数矫正就可以，pixel_size是矫正后每个pixel的物理大小
//返回值是0表示标定错误，返回值是1表示标定正确
//注：相机标定功能得到的结果参数只能用于矫正和计算得到结果的输入图像分辨率一致的图
bool camera_calibration(Mat gray,cv::Size patternSize,float grid_size,cv::Mat& cameraMatrix,cv::Mat& distCoeffs,float& pixel_size);
//图像畸变矫正功能，image是输入图像（无要求），cameraMatrix和distCoeffs是图像畸变矫正的参数
//返回值是矫正后的图像，输入图像是什么格式，输出图像就是什么格式
//注：相机标定功能得到的结果参数只能用于矫正和计算得到结果的输入图像分辨率一致的图
Mat distortion_correction(Mat image,cv::Mat cameraMatrix,cv::Mat distCoeffs);
//蛋白中心区域叠加成彩色图，Bgray为蓝色灯下灰度图，Ggray为绿色灯下灰度图，Rgray为红色灯下灰度图
//roi为手动标注区域，自动检测时 Rect roi = Rect(0,0,Bgray.cols,Bgray.rows)，否则直接按照roi区域进行标定叠加
//返回三通道彩色处理图
Mat mid_img_merge_deal(Mat Bgray,Mat Ggray,Mat Rgray,Rect roi);
