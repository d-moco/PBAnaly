#include "pch.h"
#include "PBImageProcessVC.h"
#include "PBImageProcess.h"


PBBiologyVC::PBImageProcessVC::~PBImageProcessVC()
{
	throw gcnew System::NotImplementedException();
}

int PBBiologyVC::PBImageProcessVC::render_process(System::Byte* pseImage, System::Byte* markImage, System::Byte* renderpseImage, System::Byte* mergepseImage, System::Byte* colorBarImage, int colorIndex, unsigned short colorbarW, unsigned short colorbarH, int bit, unsigned short width, unsigned short height, float sec, float max, float min, int scientific_flag, bool reverse, unsigned short colorbarWW, unsigned short h_onecolor, int brightness_offset, double contrast_factor, double opacity_factor)
{
		cv::Mat cvPseImage(height, width, CV_16UC1, pseImage);
		cv::Mat cvmarkImage(height, width, CV_16UC1, markImage);
		cv::Mat cvRenderpseImage(height, width, CV_8UC3, renderpseImage);

		

		uint8_t bgr_tab[256][3] = { 0 };

		ColorTable colortype = ColorTable(colorIndex); //当前色卡

		get_bgr_tab(colortype, bgr_tab, false);
		pseudo_color_processing(cvPseImage, cvRenderpseImage, max, min, bgr_tab); // 做伪彩 拿到伪彩图

		Mat r = render_mask_image(cvmarkImage, cvRenderpseImage, brightness_offset, contrast_factor, opacity_factor);
		int byteCount = r.rows * r.cols * r.channels();
		cvtColor(r, r, COLOR_BGR2RGB);
		std::memcpy(mergepseImage, r.data, byteCount);
		cvtColor(cvRenderpseImage, cvRenderpseImage, COLOR_BGR2RGB);
		

		Mat bgr_tab_img = bgr_tab_image(colorbarWW, h_onecolor, bgr_tab);
		 std::cout << "w = " << bgr_tab_img.cols << "c= " << bgr_tab_img.rows << "c = " << bgr_tab_img.channels() << std::endl;
		Mat bgr_scale_img = bgr_scale_image(bgr_tab_img, max, min,scientific_flag); //拿到标尺图
		std::cout << "w = " << bgr_scale_img.cols << "c= " << bgr_scale_img.rows<<"c = "<<bgr_scale_img.channels() << std::endl;;
		cvtColor(bgr_scale_img, bgr_scale_img, COLOR_BGR2RGB);
		byteCount = bgr_scale_img.rows * bgr_scale_img.cols * bgr_scale_img.channels();
		std::memcpy(colorBarImage, bgr_scale_img.data, byteCount);
    
		return 1;
}

PBBiologyVC::Pseudo_infoVC^ PBBiologyVC::PBImageProcessVC::get_pseudo_info_vc(System::Byte* mat, System::Byte* mask, int bit, unsigned short width, unsigned short height,  float max, float min)
{
		cv::Mat src(height, width, CV_16UC1, mat);
		cv::Mat mackCV(height, width, CV_16UC1, mask);
		PseudoInfo pinfo = get_pseudo_info(src, mackCV,max,min);
		PBBiologyVC::Pseudo_infoVC^ ppinfovc = gcnew PBBiologyVC::Pseudo_infoVC(pinfo.maxOD, pinfo.minOD, pinfo.IOD, pinfo.count, pinfo.AOD);    
		return ppinfovc;

}

PBBiologyVC::Pseudo_infoVC^ PBBiologyVC::PBImageProcessVC::get_pseudo_info_rect_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, float max, float min, int x, int y, int w, int h)
{
	// 1. 将原始数据转换为 OpenCV 的 Mat 格式
	cv::Mat image(height, width, CV_16UC1, mat);  // 16-bit 单通道图像

	cv::Mat mask = cv::Mat::zeros(image.size(), CV_8UC1);
	
	for (int _y = y; _y < y + h; _y++) {
		for (int _x = x; _x < x + w; _x++) {
			mask.at<uint8_t>(_y, _x) = 255;
		}
	}
	////// 显示图像
	/*cv::imshow("Processed Image", mask);
	cv::waitKey(0);*/

	// 6. 获取伪信息
	PseudoInfo pinfo = get_pseudo_info(image, mask, max, min);

	// 7. 创建并返回 Pseudo_infoVC 对象
	PBBiologyVC::Pseudo_infoVC^ ppinfovc = gcnew PBBiologyVC::Pseudo_infoVC(
		pinfo.maxOD, pinfo.minOD, pinfo.IOD, pinfo.count, pinfo.AOD);
	return ppinfovc;
}

PBBiologyVC::Pseudo_infoVC^ PBBiologyVC::PBImageProcessVC::get_pseudo_info_circle_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, float max, float min, int x, int y, int r)
{
	// 1. 将原始数据转换为 OpenCV 的 Mat 格式
	cv::Mat image(height, width, CV_16UC1, mat);  // 16-bit 单通道图像

	cv::Mat mask = cv::Mat::zeros(image.size(), CV_8UC1);

	cv::circle(mask, cv::Point(x, y), r, cv::Scalar(255), cv::FILLED);
	////// 显示图像
	/*cv::imshow("Processed Image", mask);
	cv::waitKey(0);*/

	// 6. 获取伪信息
	PseudoInfo pinfo = get_pseudo_info(image, mask, max, min);

	// 7. 创建并返回 Pseudo_infoVC 对象
	PBBiologyVC::Pseudo_infoVC^ ppinfovc = gcnew PBBiologyVC::Pseudo_infoVC(
		pinfo.maxOD, pinfo.minOD, pinfo.IOD, pinfo.count, pinfo.AOD);
	return ppinfovc;
}

PBBiologyVC::Pseudo_infoVC^ PBBiologyVC::PBImageProcessVC::get_pseudo_info_polygon_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, float max, float min, List<PBBiologyVC::Point_VC^>^ list_point)
{
	// 1. 将原始数据转换为 OpenCV 的 Mat 格式
	cv::Mat image(height, width, CV_16UC1, mat);  // 16-bit 单通道图像

	cv::Mat mask = cv::Mat::zeros(image.size(), CV_8UC1);

	std::vector<cv::Point> points;
	for each (auto var in list_point)
	{
		cv::Point t(var->x, var->y);
		points.push_back(t);
	}
	const cv::Point* ppt[1] = { points.data() };
	int npt[] = { static_cast<int>(points.size()) };

	// 使用cv::fillPoly绘制并填充多边形
	cv::fillPoly(mask, ppt, npt, 1, cv::Scalar(255), 8);

	////// 显示图像
	/*cv::imshow("Processed Image", mask);
	cv::waitKey(0);*/

	// 6. 获取伪信息
	PseudoInfo pinfo = get_pseudo_info(image, mask, max, min);

	// 7. 创建并返回 Pseudo_infoVC 对象
	PBBiologyVC::Pseudo_infoVC^ ppinfovc = gcnew PBBiologyVC::Pseudo_infoVC(
		pinfo.maxOD, pinfo.minOD, pinfo.IOD, pinfo.count, pinfo.AOD);
	return ppinfovc;
}

PBBiologyVC::Pseudo_infoVC^ PBBiologyVC::PBImageProcessVC::get_pseudo_info_wand_vc(System::Byte* mat, System::Byte* dst, int bit, unsigned short width, unsigned short height, float max, float min, int x, int y, int th, int% _dstW,int%  _dstH,int% _dstX,int% _dstY)
{
	cv::Mat image(height, width, CV_16UC1, mat);
	cv::Mat image_8bit;
	cv::normalize(image, image_8bit, 0, 255, cv::NORM_MINMAX); // 归一化到 0-255
	image_8bit.convertTo(image_8bit, CV_8UC1);                 // 转换为 CV_8UC1
	int _max = max / 256;
	int _min = min / 256;
	cv::Mat mask = get_magic_wand_image(image_8bit, x, y, _max,_min);
	PseudoInfo pinfo = get_pseudo_info(image, mask, max, min);

	
	PBBiologyVC::Pseudo_infoVC^ ppinfovc = gcnew PBBiologyVC::Pseudo_infoVC(
		pinfo.maxOD, pinfo.minOD, pinfo.IOD, pinfo.count, pinfo.AOD);

	// 使用Canny边缘检测
	cv::Mat edges;
	cv::Canny(mask, edges, 100, 200);
	std::vector<std::vector<cv::Point>> contours;
	std::vector<cv::Vec4i> hierarchy;
	cv::findContours(edges, contours, hierarchy, cv::RETR_EXTERNAL, cv::CHAIN_APPROX_SIMPLE);

	// 找到最大轮廓
	int max_contour_index = -1;
	double max_contour_area = 0.0;
	for (int i = 0; i < contours.size(); i++) {
		double area = cv::contourArea(contours[i]);
		if (area > max_contour_area) {
			max_contour_area = area;
			max_contour_index = i;
		}
	}

	// 创建一个黑色背景
	cv::Mat output = cv::Mat::zeros(image.size(), CV_8UC3);

	// 如果找到了最大轮廓，绘制在黑色背景上
	if (max_contour_index != -1) {
		cv::drawContours(output, contours, max_contour_index, cv::Scalar(255, 255, 255), 2); // 白色线条，线宽为2
		cv::Rect boundingBox = cv::boundingRect(contours[max_contour_index]);
		cv::rectangle(output, boundingBox, cv::Scalar(0, 255, 0), 2);
		_dstX = boundingBox.x;
		_dstY = boundingBox.y;
		_dstW = boundingBox.width;
		_dstH = boundingBox.height;

	}

	/*imshow("a", output);
	waitKey(0);*/
	return ppinfovc;
}

//int PBBiologyVC::PBImageProcessVC::render_mask_image_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, System::Byte* mark, System::Byte* dst, float max, float min, bool reverse)
//{
//	
//  /*  if (bit != 16)
//    {
//        return -1;
//    }
//    cv::Mat src(height, width, CV_16UC1, mat);
//    cv::Mat cvMart(height, width, CV_16UC1, mark);
//    cv::Mat cvdst(height, width, CV_8UC3, dst);
//    int ret = render_mask_image(cvMart, src , cvdst, max, min, ColorTable::YellowHot, reverse);
//    if (ret == -1) return -1;*/
//    return 0;
//
//}

//int PBBiologyVC::PBImageProcessVC::blendImages_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, System::Byte* mark, System::Byte* dst, float alpha)
//{
//   /* if (bit != 16)
//    {
//        return -1;
//    }
//    cv::Mat src(height, width, CV_16UC1, mat);
//    cv::Mat cvMart(height, width, CV_8UC3, mark);
//    cv::Mat cvdstrgb(height, width, CV_8UC3, dst);
//    
//    int ret = blendImages(src, cvMart, cvdstrgb,alpha);
//    if (ret == 0) return -1;
//   */
//    return 0;
//
//}

//int PBBiologyVC::PBImageProcessVC::render_image_16_vc(System::Byte* mat, unsigned short width, unsigned short height, System::Byte* dst,
//    int colorIndex,float max, float min, bool reverse, System::Byte* colobar,unsigned short colorbar_width, unsigned short colorbar_height,
//    int h_onecolor)
//{
//    // 创建16位单通道输入图像
//    cv::Mat src(height, width, CV_16UC1, mat);
//    // 创建8位3通道输出图像
//    cv::Mat cvdst(height, width, CV_8UC3, dst);
//
//    uint8_t bgr_tab[256][3] = { 0 };
//
//    ColorTable colortype = ColorTable(colorIndex);
//    
//    get_bgr_tab(colortype, bgr_tab, false);
//    if(colortype != Gray)
//        pseudo_color_processing(src, cvdst, max, min, bgr_tab); // 做伪彩 拿到伪彩图
//    Mat bgr_tab_img = bgr_tab_image(colorbar_width, h_onecolor, bgr_tab);
//    Mat bgr_scale_img = bgr_scale_image(bgr_tab_img, max, min);
//    /*imshow("a", bgr_scale_img);
//    waitKey(0);*/
//
//    return 0;
//}

//PBBiologyVC::Pseudo_infoVC^ PBBiologyVC::PBImageProcessVC::get_pseudo_info_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, int x, int y, int w, int h, float max, float min)
//{
//    cv::Mat src(height, width, CV_16UC1, mat);
//    PseudoInfo pinfo = get_pseudo_info(src, x, y, w, h, max, min);
//    PBBiologyVC::Pseudo_infoVC^ ppinfovc = gcnew PBBiologyVC::Pseudo_infoVC(pinfo.maxOD, pinfo.minOD, pinfo.IOD, pinfo.count, pinfo.AOD);    
//    return ppinfovc;
//}
//
//int PBBiologyVC::PBImageProcessVC::render_process(System::Byte* pseImage, System::Byte* markImage, System::Byte* renderpseImage, System::Byte* mergepseImage,
//    System::Byte* colorBarImage, int colorIndex, unsigned short colorbarW, unsigned short colorbarH,
//    int bit, unsigned short width, unsigned short height, float max, float min, bool reverse,
//    unsigned short colorbarWW, unsigned short h_onecolor, int brightness_offset, double contrast_factor, double opacity_factor)
//{
//    
//    cv::Mat cvPseImage(height, width, CV_16UC1, pseImage);
//    cv::Mat cvmarkImage(height, width, CV_16UC1, markImage);
//    cv::Mat cvRenderpseImage(height, width, CV_8UC3, renderpseImage);
//
//
//
//    uint8_t bgr_tab[256][3] = { 0 };
//
//    ColorTable colortype = ColorTable(colorIndex); //当前色卡
//
//    get_bgr_tab(colortype, bgr_tab, false);
//    if (colortype != Gray) 
//    {
//        pseudo_color_processing(cvPseImage, cvRenderpseImage, max, min, bgr_tab); // 做伪彩 拿到伪彩图
//
//        Mat r = render_mask_image(cvmarkImage, cvRenderpseImage, brightness_offset, contrast_factor, opacity_factor);
//        int byteCount = r.rows * r.cols * r.channels();
//        cvtColor(r, r, COLOR_BGR2RGB);
//        std::memcpy(mergepseImage, r.data, byteCount);
//        cvtColor(cvRenderpseImage, cvRenderpseImage, COLOR_BGR2RGB);
//        /*imshow("a", cvRenderpseImage);
//        waitKey(1);*/
//    }
//    else
//    {
//
//    }
//
//
//    Mat bgr_tab_img = bgr_tab_image(colorbarWW, h_onecolor, bgr_tab);
//   // std::cout << "w = " << bgr_tab_img.cols << "c= " << bgr_tab_img.rows << "c = " << bgr_tab_img.channels() << std::endl;
//    Mat bgr_scale_img = bgr_scale_image(bgr_tab_img, max, min); //拿到标尺图
//    //std::cout << "w = " << bgr_scale_img.cols << "c= " << bgr_scale_img.rows<<"c = "<<bgr_scale_img.channels() << std::endl;;
//    cvtColor(bgr_scale_img, bgr_scale_img, COLOR_BGR2RGB);
//    int byteCount = bgr_scale_img.rows * bgr_scale_img.cols * bgr_scale_img.channels();
//    std::memcpy(colorBarImage, bgr_scale_img.data, byteCount);
//    
//    return 1;
//}
//
//int PBBiologyVC::PBImageProcessVC::get_colorbar_VC(System::Byte* colorBarImage, int colorIndex, unsigned short colorbarW, unsigned short colorbarH, unsigned short colorbarWW, unsigned short h_onecolor, float max, float min, bool reverse)
//{
//    uint8_t bgr_tab[256][3] = { 0 };
//
//    ColorTable colortype = ColorTable(colorIndex); //当前色卡
//
//    get_bgr_tab(colortype, bgr_tab, false);
//    Mat bgr_tab_img = bgr_tab_image(colorbarWW, h_onecolor, bgr_tab);
//    // std::cout << "w = " << bgr_tab_img.cols << "c= " << bgr_tab_img.rows << "c = " << bgr_tab_img.channels() << std::endl;
//    Mat bgr_scale_img = bgr_scale_image(bgr_tab_img, max, min); //拿到标尺图
//    //std::cout << "w = " << bgr_scale_img.cols << "c= " << bgr_scale_img.rows<<"c = "<<bgr_scale_img.channels() << std::endl;;
//    cvtColor(bgr_scale_img, bgr_scale_img, COLOR_BGR2RGB);
//    int byteCount = bgr_scale_img.rows * bgr_scale_img.cols * bgr_scale_img.channels();
//    std::memcpy(colorBarImage, bgr_scale_img.data, byteCount);
//    return 0;
//}
