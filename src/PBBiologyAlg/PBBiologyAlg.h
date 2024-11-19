
namespace PBBiologyAlg
{
	public ref struct MolecularInfoVC
	{
        float molecular_weight;     // 分子量
        int band_content;           // 条带含量
        float relative_content;     // 相对含量
        int IOD;                    // IOD
        float maxOD;                // 最大OD
        float percentum;            // 百分比
        int match;                  // 匹配
	};

	public ref class MyClass
	{
	public:
		MyClass();
		~MyClass();

	private:

	};

	MyClass::MyClass()
	{
	}

	MyClass::~MyClass()
	{
	}
}




//#pragma once
//
//#include<opencv2/opencv.hpp>
//#include<iostream>
//#include <vector>
//#include <algorithm>
//#include <map>
//#include <numeric>
//
//using namespace System;
//using namespace std;
//using namespace cv;
//
//
//typedef struct _molecular_info
//{
//	float molecular_weight;		//分子量
//	int band_content;			//条带含量
//	float relative_content;		//相对含量
//	int IOD;					//IOD
//	float maxOD;				//最大OD
//	float percentum;			//百分比
//	int match;					//匹配
//}MolecularInfo;
//
//typedef struct _band_info
//{
//	vector<unsigned short> land_data;		//泳道数据（16bit）
//	std::vector<float> ydata;				//泳道波形y轴（0.0-255.0）
//	std::vector<float> xdata;				//泳道波形x轴（0.0-1.0）
//	vector<std::array<int, 3>> band_point;	//条带位置（顶峰、左括号、右括号）
//	vector<MolecularInfo> Minfo;			//对应条带的分子计算结果
//}BandInfo;
//
//
//namespace PBBiologyAlg {
//	public ref class biologyAPI
//	{
//	private:
//		//泳道过滤处理函数
//		//根据输入所有类似泳道的中心X轴坐标（groupsX），以及泳道的最小最大距离（minDifference/maxDifference），过滤掉不合规的泳道
//		std::vector<int> processArray(std::vector<int>& groupsX, int minDifference, int maxDifference);
//
//		//宽度流计算函数，用来计算检测泳道宽度流
//		vector<int> get_bar_num(vector<unsigned char> buf);
//
//		//泳道检测补全函数
//		//根据泳道位置（processX）和泳道间距离（meanW），从图像（src）中检测临近类泳道位置是否符合泳道特性（泳道内两条像素的宽度流应类似且存在条带）
//		std::vector<int> checkArray(std::vector<int>& processX, Mat src, int start_y, int end_y, int meanW);
//
//		//条带信息计算函数，输入8bit数组（rbuf）以及过滤范围（range）（此范围内仅保留一个分子）
//		vector<std::array<int, 3>> get_top_point(vector<unsigned char> rbuf, int range);
//
//		//单个泳道中条带信息计算（条带位置等）
//		BandInfo get_protein_lane_data(Mat src, Rect lane);
//
//	public:
//		biologyAPI();
//		~biologyAPI();
//
//		//函数：获得图像中泳道位置
//		//src：输入图像，需是8bit单通道图像
//		//返回值：泳道矩形位置信息
//		std::vector<cv::Rect> getProteinRect(Mat src);
//
//		//函数：获得图像中泳道对应的条带信息
//		//src：输入图像，需是16bit单通道图像
//		//lanes：泳道矩形位置信息
//		//返回值：对应条带位置信息
//		std::vector<BandInfo> getProteinBands(Mat src, std::vector<cv::Rect> lanes);
//
//		//函数：条带分子匹配排序函数
//		//bands：所有条带位置信息
//		//range：Y轴差在range范围内的表示和泳道1匹配
//		//返回值：结果修改在bands中
//		void adjustBands(std::vector<BandInfo>& bands, int range);
//
//
//		//函数：计算所有条带的分子信息
//		//lanes：输入泳道信息
//		//bands：输入条带信息，对应输出的分子信息在这个结构体中
//		void molecularWeightResult(std::vector<cv::Rect> lanes, std::vector<BandInfo>& bands);
//	};
//}
