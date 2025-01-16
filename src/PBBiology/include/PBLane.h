#pragma once
#include <opencv2/opencv.hpp>
#include <vector>
#include <algorithm>
#include <map>
#include <numeric>
#include "PBType.h"

using namespace std;
using namespace cv;

class PBLane
{
public:
	PBLane();
	~PBLane();

private:
	std::vector<int> processArray(std::vector<int>& groupsX, int minDifference, int maxDifference);

	vector<int> get_bar_num(vector<unsigned char> buf);

	std::vector<int> checkArray(std::vector<int>& processX, Mat src, int start_y, int end_y, int meanW);

	vector<std::array<int, 3>> get_top_point(vector<unsigned short> rbuf, int range);

	static bool myCompare(const int& a, const int& b);
	static bool myCompare2(const std::array<int, 4>& a, const std::array<int, 4>& b);


public:
	// std::vector<cv::Rect> getProteinRect(Mat src);
	//获得蛋白泳道位置，src是灰度图像CV_8UC1，*ProteinRect_width 是蛋白泳道宽度（keep_width = 1时，输入固定宽度；keep_width = 0时,输出计算得到宽度），
	//ProteinRect_height_ratio高度比例，高度占图像高度百分之几，一般输入90
	//返回所有蛋白泳道矩形
	std::vector<cv::Rect> getProteinRect(Mat src,int* ProteinRect_width,bool keep_width,int ProteinRect_height_ratio);
	//添加泳道，proteinRect是当前泳道矩形，x是新添加泳道的中心x坐标，一般就是鼠标点击图形的x坐标,
	//src是输入图形（CV_16UC1）,unadjustbands是未对齐条带信息，需要同步修改
	//proteinRect在没有泳道时，会使用输入的ProteinRect_width 作为蛋白泳道宽度,ProteinRect_height_ratio作为高度比例，高度占图像高度百分之几，一般输入90
	void addProteinRect(std::vector<cv::Rect>& proteinRect,int x,Mat src,std::vector<BandInfo>& unadjustbands,int ProteinRect_width,int ProteinRect_height_ratio);
	//删除泳道，proteinRect是当前泳道矩形，idx是删除泳道的下标序号,unadjustbands是未对齐条带信息，需要同步修改
	void deleteProteinRect(std::vector<cv::Rect>& proteinRect,int idx,std::vector<BandInfo>& unadjustbands);
	//计算单个泳道条带信息，src是输入图形（CV_16UC1），lane是泳道对应矩形，
	//返回BandInfo条带结果
	BandInfo get_protein_lane_data(Mat src,Rect lane);
	//获得所有泳道未对齐的条带结果，src是输入图形（CV_16UC1），lane是泳道对应矩形
	//返回所有泳道未对齐的条带结果
	std::vector<BandInfo> getProteinBands(Mat src,std::vector<cv::Rect> lanes);
	//修改泳道宽度和高度和已经计算出的未对齐条带信息，src是输入图形（CV_16UC1），proteinRect是当前泳道矩形，
	//ProteinRect_width是新的蛋白泳道宽度,ProteinRect_height_ratio是新的高度比例，高度占图像高度百分之几，一般输入90
	//unadjustbands是未对齐的所有泳道条带信息
	//输入的条带必须是未对齐的，调整后数据需要重新对齐和计算分子量（调用adjustBands和molecularWeightResult）
	void modifyProteinRectAndBands(Mat src,std::vector<cv::Rect>& proteinRect,int ProteinRect_width,int ProteinRect_height_ratio,std::vector<BandInfo>& unadjustbands);
	//根据鼠标点击图形的坐标，输出当前位置的泳道下标和条带下标
	//lanes是泳道信息，bands是对应条带信息（不关心对齐或者未对齐），x、y是坐标，lanesIndex是传回鼠标点击位置的泳道下标，bandsIndex是鼠标点击位置的条带下标
	void getLaneBandsIndex(std::vector<cv::Rect> lanes,std::vector<BandInfo> bands,int x,int y,int* lanesIndex,int* bandsIndex);
	//根据鼠标点击图形的坐标添加条带,lanes是泳道信息，lanesIndex是鼠标点击位置是第几条泳道，unadjustbands是未对齐的所有泳道条带信息，原始鼠标点击位置的y坐标
	//输入的条带必须是未对齐的，调整后数据需要重新对齐和计算分子量（调用adjustBands和molecularWeightResult）
	void addProteinBand(std::vector<cv::Rect> lanes,int lanesIndex,std::vector<BandInfo>& unadjustbands,int y);
	//根据鼠标点击图形的坐标删除条带,lanesIndex是鼠标点击位置是第几条泳道，unadjustbands是未对齐的所有泳道条带信息，bandsIndex是鼠标点击位置是第几个条带
	//输入的条带必须是未对齐的，调整后数据需要重新对齐和计算分子量（调用adjustBands和molecularWeightResult）
	void deleteProteinBand(int lanesIndex,std::vector<BandInfo>& unadjustbands,int bandsIndex);
	//对齐所有泳道的条带结果，unadjustbands是所有泳道的条带结果，range是条带中心在多少像素范围内属于同一类型条带，一般写10
	//返回对齐后的所有泳道的条带结果（未计算分子量）
	std::vector<BandInfo> adjustBands(std::vector<BandInfo> unadjustbands, int range);
	//计算得到分子量结果
	void molecularWeightResult(std::vector<cv::Rect> lanes,std::vector<BandInfo>& bands);

};