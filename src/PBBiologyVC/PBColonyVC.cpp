#include "pch.h"
//#include "PBColonyVC.h"
//#include <opencv2/opencv.hpp>
//#include <PBColony.h>
//
//PBBiologyVC::PBColonyVC::PBColonyVC()
//{
//   // pbcolony = new PBColony;
//}
//PBBiologyVC::PBColonyVC::~PBColonyVC()
//{
//    //delete pbcolony;
//}
//
//void PBBiologyVC::PBColonyVC::run(System::Byte* image, int bit, unsigned short width, unsigned short height,int lower=-1,int upper=-1)
//{
//    //cv::Mat input_cn1;
//    //if (bit == 16) 
//    //{
//    //    input_cn1 = cv::Mat(height, width, CV_16UC1, image);
//    //    cv::normalize(input_cn1, input_cn1, 0, 255, cv::NORM_MINMAX); // 归一化到 0-255
//    //    input_cn1.convertTo(input_cn1, CV_8UC1);                 // 转换为 CV_8UC1
//    //}
//    //else if (bit == 8)
//    //{
//    //    input_cn1 = cv::Mat(height, width, CV_8UC1, image);
//    //}
//    //else
//    //{
//    //    return;
//    //}
//    //Mat input_cn3;
//    //Point2f center;
//    //float radius;
//    //int ret =pbcolony->colony_get_circle(input_cn1, center, radius);
//    //if (ret)
//    //{
//    //    cv::Mat mask = pbcolony->generateMaskImage(input_cn1.cols, input_cn1.rows, center.x - radius, center.y - radius, 2 * radius, 2 * radius);
//    //    int lower = -1;
//    //    int upper = -1;
//    //    Mat bin = pbcolony->get_lower_upper(input_cn1, mask, lower, upper);
//
//    //    ClassifyStandard class_stand;
//    //    pbcolony->init_classify_standard(class_stand);
//    //    vector<ColonyInfo> Cinfo = pbcolony->get_colony_info(input_cn1, bin, input_cn3, class_stand, pbcolony->image_inverted_flag);
//    //    ColonyStatistic CStatistic = pbcolony->get_colony_statistics(Cinfo);
//
//    //    cv::imshow("a", input_cn3);
//    //    cv::waitKey(0);
//
//
//   // }
//}
