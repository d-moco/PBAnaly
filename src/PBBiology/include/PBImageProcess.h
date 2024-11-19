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

////根据mask渲染图像
////src、mask：输入CV_16UC1图像
////dst：输出CV_8UC3彩色图像
////max、min：mask像素选择渲染的最大最小值
////color：颜色类型
////reverse：是否反转颜色
//int render_mask_image(Mat src, Mat mask, Mat dst, float max, float min, ColorTable color, bool reverse);
//
///// <summary>
///// 融合两张图
///// </summary>
///// <param name="src"></param>
///// <param name="mark"></param>
///// <param name="dst"></param>
///// <param name="alpha"></param>
/////  <returns></returns>
//int blendImages(const Mat& src, const Mat& mark, const Mat& dst, double alpha);
////int render_image(Mat src, Mat& dst, float max, float min, ColorTable color, bool reverse);
////合成渲染图像，src是老鼠图，pseudoImg是光子渲染图，brightness_offset亮度，contrast_factor对比度，contrast_factor透明度，返回融合图
////brightness_offset:亮度偏移范围 -255 到 +255
////contrast_factor:对比度因子范围 0.1 到 3.0（1.0为不变）
////opacity_factor:透明度因子范围 0 到 1（0为透明，1为不透明）
//Mat render_mask_image(Mat src, Mat pseudoImg, int brightness_offset, double contrast_factor, double opacity_factor);
////获取颜色表，color颜色类型，bgr_tab是有空间的颜色表指针，reverse是否反转
//void get_bgr_tab(ColorTable color, uint8_t(*bgr_tab)[3], bool reverse);
////生产颜色表的直条图，w=200,h_color=10是一个颜色高，bgr_tab是有空间的颜色表指针
//Mat bgr_tab_image(int w, int h_onecolor, uint8_t(*bgr_tab)[3]);
//int pseudo_color_processing(Mat src, Mat dst, float max, float min, uint8_t(*bgr_tab)[3]);
//
//// 获得选中区域的光子数
//PseudoInfo get_pseudo_info(Mat src,int x,int y,int w,int h,float max,float min);
//
//Mat bgr_scale_image(Mat src, float maxVal, float minVal);



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
//魔术棒功能，src是处理成8bit的图，x,y是点击位置的坐标，
//th是设定的像素差（10或20之类的，可以实际调一下），就是和点击位置的像素差在th范围内的连在一起的像素，都会被框选
Mat get_magic_wand_image(Mat src, int x, int y, int th);