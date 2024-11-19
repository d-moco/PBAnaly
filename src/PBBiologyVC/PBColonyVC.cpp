#include "pch.h"
//#include "PBColonyVC.h"
//#include <opencv2/opencv.hpp>
//#include<vector>
//#include "PBColony.h"
//
//
//
//namespace PBBiologyVC 
//{
//
//	PBColonyVC::~PBColonyVC()
//	{
//
//	}
//
//	void PBColonyVC::run(System::Byte* mat, unsigned short width, unsigned short height)
//	{
//
//		/*for (size_t i = 0; i < width * height; i++)
//		{
//			std::cout << mat[i];
//		}
//		std::cout << std::endl;
//
//		cv::Mat src(height, width, CV_8UC1, mat);
//		Mat input_cn3;
//		cv::Point2f center;
//		float radius;
//		int ret = pbcolony->colony_get_circle(src,center,radius);
//		if (ret)
//		{
//			Mat mask = pbcolony->generateMaskImage(src.cols, src.rows, center.x - radius, center.y - radius, 2 * radius, 2 * radius);
//			int lower = -1;
//			int upper = -1;
//			Mat bin = pbcolony->get_lower_upper(src, mask, lower, upper);
//
//			ClassifyStandard class_stand;
//			pbcolony->init_classify_standard(class_stand);
//			vector<ColonyInfo> Cinfo = pbcolony->get_colony_info(src, bin, input_cn3, class_stand, pbcolony->image_inverted_flag);
//			ColonyStatistic CStatistic = pbcolony->get_colony_statistics(Cinfo);
//
//		
//		}*/
//	}
//}