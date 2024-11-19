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
	//Ӿ�����˴�����
	//����������������Ӿ��������X�����꣨groupsX�����Լ�Ӿ������С�����루minDifference/maxDifference�������˵����Ϲ��Ӿ��
	std::vector<int> processArray(std::vector<int>& groupsX, int minDifference, int maxDifference);

	//���/*��*/���㺯��������������Ӿ�������
	vector<int> get_bar_num(vector<unsigned char> buf);

	//Ӿ����ⲹȫ����
	//����Ӿ��λ�ã�processX����Ӿ������루meanW������ͼ��src���м���ٽ���Ӿ��λ���Ƿ����Ӿ�����ԣ�Ӿ�����������صĿ����Ӧ�����Ҵ���������
	std::vector<int> checkArray(std::vector<int>& processX, Mat src, int start_y, int end_y, int meanW);

	//������Ϣ���㺯��������8bit���飨rbuf���Լ����˷�Χ��range�����˷�Χ�ڽ�����һ�����ӣ�
	vector<std::array<int, 3>> get_top_point(vector<unsigned char> rbuf, int range);

	//����Ӿ����������Ϣ���㣨����λ�õȣ�
	BandInfo get_protein_lane_data(Mat src, Rect lane);
	static bool myCompare(const int& a, const int& b);
	static bool myCompare2(const std::array<int, 4>& a, const std::array<int, 4>& b);


public:
	//���������ͼ����Ӿ��λ��
	//src������ͼ������8bit��ͨ��ͼ��
	//����ֵ��Ӿ������λ����Ϣ
	std::vector<cv::Rect> getProteinRect(Mat src);

	////���������ͼ����Ӿ����Ӧ��������Ϣ
	////src������ͼ������16bit��ͨ��ͼ��
	////lanes��Ӿ������λ����Ϣ
	////����ֵ����Ӧ����λ����Ϣ
	std::vector<BandInfo> getProteinBands(Mat src, std::vector<cv::Rect> lanes);

	////��������������ƥ��������
	////bands����������λ����Ϣ
	////range��Y�����range��Χ�ڵı�ʾ��Ӿ��1ƥ��
	////����ֵ������޸���bands��
	void adjustBands(std::vector<BandInfo>& bands, int range);


	////�������������������ķ�����Ϣ
	////lanes������Ӿ����Ϣ
	////bands������������Ϣ����Ӧ����ķ�����Ϣ������ṹ����
	void molecularWeightResult(std::vector<cv::Rect> lanes, std::vector<BandInfo>& bands);
};