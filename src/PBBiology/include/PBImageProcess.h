#pragma once
#include "PBType.h"
#include <opencv2/opencv.hpp>
#include <vector>
#include <algorithm>
#include <map>
#include <numeric>

using namespace std;
using namespace cv;


//��������
int RegionGrow(cv::Mat& src, cv::Mat& matDst, cv::Point2i pt, int th);
//��С���˷�ȡԲ
void FitCircleCenter(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R);
//������С���˷�ȡԲ
int RANSAC_FitCircleCenter(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R, float thresh);
//����������С���˷�ȡԲ
void RANSAC_FitCircleCenter_with_throw(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R);
//��ֵ����ֵ����
int IJIsoData(int* data);
int defaultIsoData(int* data);

////����mask��Ⱦͼ��
////src��mask������CV_16UC1ͼ��
////dst�����CV_8UC3��ɫͼ��
////max��min��mask����ѡ����Ⱦ�������Сֵ
////color����ɫ����
////reverse���Ƿ�ת��ɫ
//int render_mask_image(Mat src, Mat mask, Mat dst, float max, float min, ColorTable color, bool reverse);
//
///// <summary>
///// �ں�����ͼ
///// </summary>
///// <param name="src"></param>
///// <param name="mark"></param>
///// <param name="dst"></param>
///// <param name="alpha"></param>
/////  <returns></returns>
//int blendImages(const Mat& src, const Mat& mark, const Mat& dst, double alpha);
////int render_image(Mat src, Mat& dst, float max, float min, ColorTable color, bool reverse);
////�ϳ���Ⱦͼ��src������ͼ��pseudoImg�ǹ�����Ⱦͼ��brightness_offset���ȣ�contrast_factor�Աȶȣ�contrast_factor͸���ȣ������ں�ͼ
////brightness_offset:����ƫ�Ʒ�Χ -255 �� +255
////contrast_factor:�Աȶ����ӷ�Χ 0.1 �� 3.0��1.0Ϊ���䣩
////opacity_factor:͸�������ӷ�Χ 0 �� 1��0Ϊ͸����1Ϊ��͸����
//Mat render_mask_image(Mat src, Mat pseudoImg, int brightness_offset, double contrast_factor, double opacity_factor);
////��ȡ��ɫ��color��ɫ���ͣ�bgr_tab���пռ����ɫ��ָ�룬reverse�Ƿ�ת
//void get_bgr_tab(ColorTable color, uint8_t(*bgr_tab)[3], bool reverse);
////������ɫ���ֱ��ͼ��w=200,h_color=10��һ����ɫ�ߣ�bgr_tab���пռ����ɫ��ָ��
//Mat bgr_tab_image(int w, int h_onecolor, uint8_t(*bgr_tab)[3]);
//int pseudo_color_processing(Mat src, Mat dst, float max, float min, uint8_t(*bgr_tab)[3]);
//
//// ���ѡ������Ĺ�����
//PseudoInfo get_pseudo_info(Mat src,int x,int y,int w,int h,float max,float min);
//
//Mat bgr_scale_image(Mat src, float maxVal, float minVal);



//�ϳ���Ⱦͼ��src������ͼ��pseudoImg�ǹ�����Ⱦͼ��brightness_offset���ȣ�contrast_factor�Աȶȣ�contrast_factor͸���ȣ������ں�ͼ
//brightness_offset:����ƫ�Ʒ�Χ -255 �� +255
//contrast_factor:�Աȶ����ӷ�Χ 0.1 �� 3.0��1.0Ϊ���䣩
//opacity_factor:͸�������ӷ�Χ 0 �� 1��0Ϊ͸����1Ϊ��͸����
Mat render_mask_image(Mat src, Mat pseudoImg, int brightness_offset, double contrast_factor, double opacity_factor);
//��ȡ��ɫ��color��ɫ���ͣ�bgr_tab���пռ����ɫ��ָ�룬reverse�Ƿ�ת
void get_bgr_tab(ColorTable color, uint8_t(*bgr_tab)[3], bool reverse);
//������ɫ���ֱ��ͼ��w=200,h_color=10��һ����ɫ�ߣ�bgr_tab���пռ����ɫ��ָ��
Mat bgr_tab_image(int w, int h_onecolor, uint8_t(*bgr_tab)[3]);
//ͳ�Ƽ�������src������ͼ��16bit��countͼ����float�Ĺ��Ӽ�����ͼ���������룻mask����Ĥͼ��max��min���趨�Ĵ�С
PseudoInfo get_pseudo_info(Mat src, Mat mask, float max, float min);
//���ɹ�����Ⱦͼ��src����Ⱦǰͼ��dst����Ⱦ��ͼ��max��min���趨�Ĵ�С��bgr_tab���пռ����ɫ��ָ��
int pseudo_color_processing(Mat src, Mat dst, float max, float min, uint8_t(*bgr_tab)[3]);
//���ɴ���ߵ�ֱ��ͼ��src��bgr_tab_image���ɵ�ͼ��maxVal��minVal���趨�Ĵ�С��scientific_flag�Ƿ��ѧ������
Mat bgr_scale_image(Mat src, float maxVal, float minVal, int scientific_flag);
//��ȡ���Ӽ���ͼ��src������Ⱦǰԭʼͼ��sec������������Wcm=27��ʵ�ʿ�Hcm=18��ʵ�ʸߣ�sr��Ĭ��1.0������CV_32FC1�ĸ�����ӽ��ͼ
Mat get_photon_image(Mat src, float sec, float Wcm, float Hcm, float sr);
//ħ�������ܣ�src�Ǵ����8bit��ͼ��x,y�ǵ��λ�õ����꣬
//th���趨�����ز10��20֮��ģ�����ʵ�ʵ�һ�£������Ǻ͵��λ�õ����ز���th��Χ�ڵ�����һ������أ����ᱻ��ѡ
Mat get_magic_wand_image(Mat src, int x, int y, int th);