#pragma once

#include <opencv2/opencv.hpp>
#include <vector>
#include <algorithm>
#include <map>
#include <numeric>
#include "PBType.h"
using namespace std;
using namespace cv;

class PBColony
{
public:
	bool image_inverted_flag;//记录菌落是白色还是黑色
	PBColony();
	~PBColony();

private:
	//生长方式找到圆
	int fine_grow_circle(Mat input, Point2f& center, float& radius);
	//测边方式找到圆
	int fine_max_circle(Mat input, Point2f& center, float& radius);

public:
	////函数：获得图像培养皿圆的位置
	////src：输入图像，需是8bit单通道图像
	////center：圆心
	////radius：半径
	////返回值：是否找到圆，0表示没有找到
	int colony_get_circle(Mat& src, Point2f& center, float& radius);

	////函数：根据矩形生成白色椭圆，黑色背景的掩膜图像
	////img_width、img_height：生成掩膜图像宽高
	////rect_x、rect_y、rect_width、rect_height：椭圆的位置
	////返回值：对应掩膜图像
	cv::Mat generateMaskImage(int img_width, int img_height, int rect_x, int rect_y, int rect_width, int rect_height);

	////函数：根据掩膜图像和输入图像生成对应二值化图像
	////input_cn1：输入图像
	////mask：掩膜图像
	////lower、upper：二值化阈值
	////返回值：二值化图像
	cv::Mat get_lower_upper(Mat& input_cn1, Mat& mask, int& lower, int& upper);

	////函数：初始化分类标准
	void init_classify_standard(ClassifyStandard& class_stand);
	////函数：设置分类间隔
	void set_classify_standard_num(ClassifyStandard& class_stand, int num);
	////函数：设置分类类别
	void set_classify_standard_classes(ClassifyStandard& class_stand, dataClass classes);
	////函数：修改分类标准数值
	void set_classify_standard_interval(ClassifyStandard& class_stand, int n, float data);

	////函数：计算得到菌落信息
	////src：输入图像，8bit灰度图像
	////bin：二值化图像
	////dst_cn3：绘制菌落轮廓以及计数标志的输出图像
	////class_stand：分类标准
	////inverted_flag：菌落是黑色还是白色的标志
	////返回值：菌落信息
	vector<ColonyInfo> get_colony_info(Mat src, Mat bin, Mat& dst_cn3, ClassifyStandard class_stand, bool inverted_flag);

	////函数：统计菌落信息
	////Cinfo：输入菌落信息
	////返回值：菌落信息统计结果
	ColonyStatistic get_colony_statistics(vector<ColonyInfo> Cinfo);
};

/*  text
* 
	Mat input_cn1;//输入8bit单通道灰度图像
	Mat input_cn3;//显示出来的带有轮廓的图像

	Point2f center;
	float radius;
	int ret = colony_get_circle(input_cn1, center, radius);

	if (ret)
	{
		Mat mask = generateMaskImage(input_cn1.cols, input_cn1.rows, center.x - radius, center.y - radius, 2 * radius, 2 * radius);
		int lower = -1;
		int upper = -1;
		Mat bin = get_lower_upper(input_cn1, mask, lower, upper);

		ClassifyStandard class_stand;
		init_classify_standard(class_stand);
		vector<ColonyInfo> Cinfo = get_colony_info(input_cn1, bin, input_cn3, class_stand, image_inverted_flag);
		ColonyStatistic CStatistic = get_colony_statistics(Cinfo);
	}

*/
