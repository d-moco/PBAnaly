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

void PBBiologyVC::PBImageProcessVC::setSharpen_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height)
{
	cv::Mat image(height, width, CV_16UC1, mat);

	cv::Mat dst = SetSharpen(image);
	int byteCount = dst.rows * dst.cols * dst.channels() * 2;
	std::memcpy(mat, dst.data, byteCount);
}

float PBBiologyVC::PBImageProcessVC::distortion_correction_vc(System::Byte* mat,int bit, unsigned short width, unsigned short height, System::Byte* dst)
{
	cv::Mat image(height, width, CV_16UC1, mat);
	//cv::Mat mat = cv::imread("testImage/6.tif", cv::IMREAD_ANYDEPTH);
	//std::cout << mat.type() << std::endl;
	//cv::Mat gray;
	//mat.convertTo(gray, CV_8U, 1.0 / 256);
	float pixel_size = 0;
	cv::Mat cameraMatrix = cv::Mat::eye(3, 3, CV_64F);  // 相机矩阵初始化
	cv::Mat distCoeffs = cv::Mat::zeros(8, 1, CV_64F);  // 畸变系数初始化
	if (width == 1413 && height == 944) 
	{
		pixel_size = 0.146854;

		cameraMatrix = (cv::Mat_<double>(3, 3) <<
			13424.3, 0, 699.116,
			0, 13418.2, 457.194,
			0, 0, 1);
		distCoeffs = (cv::Mat_<double>(1, 5) <<
			-13.09138374714043,
			1096.760467172357,
			-0.01255686300425431,
			-0.02193325448828992,
			6.210517199273205);
	
	}
	else if (width == 2120 && height == 1416) 
	{
		pixel_size = 0.0979912;

		cameraMatrix = (cv::Mat_<double>(3, 3) <<
			30615.6, 0, 1075.35,
			0 ,30549.6, 671.703,
			0, 0, 1);
		distCoeffs = (cv::Mat_<double>(1, 5) <<
			-28.27314484356243,
			4861.062216658945,
			-0.005201762355309857,
			-0.04199660720547452,
			16.86437358623617);
		
	}
	else if (width == 4240 && height == 2832) 
	{
		pixel_size = 0.0488135;
		cameraMatrix = (cv::Mat_<double>(3, 3) <<
			55720.2, 0 ,2069.75,
			0, 55692.2 ,1330.99,
			0, 0, 1);
		distCoeffs = (cv::Mat_<double>(1, 5) <<
			-26.49169599273552,
			4221.103679926392,
			0.002303751568779423,
			-0.01228639356798468,
			16.46220656874338);

	
	}
	else if (width == 1046 && height == 700) 
	{
		pixel_size = 0.197999;
		cameraMatrix = (cv::Mat_<double>(3, 3) <<
			12403.6 ,0, 509.269,
			0 ,12392.3 ,317.452,
			0, 0, 1);
		distCoeffs = (cv::Mat_<double>(1, 5) <<
			-20.53803884839358,
			2982.635509064052,
			0.006638792032366512,
			-0.02166487691462552,
			12.66787776344682);
	}
	else if (width == 1570 && height == 1051) 
	{
		pixel_size = 0.132058;
		cameraMatrix = (cv::Mat_<double>(3, 3) <<
			13074.3, 0, 782.395,
			0 ,13067.7, 500.374,
			0, 0, 1);
		distCoeffs = (cv::Mat_<double>(1, 5) <<
			-10.46123267078845,
			837.3603920854447,
			-0.006721123253779827,
			-0.0264263519058049,
			5.094811028131693);

	}
	else if (width == 3140 && height == 2102) 
	{
		pixel_size = 0.0657731;
		cameraMatrix = (cv::Mat_<double>(3, 3) <<
			39272.7, 0, 1553.26,
			0 ,39357.7, 776.553,
			0, 0, 1);
		distCoeffs = (cv::Mat_<double>(1, 5) <<
			-22.14092395647976,
			2198.79303457571,
			0.0926857564915898,
			-0.01814724403191658,
			6.983496103826432);
		
	}
	Mat dstR = distortion_correction(image, cameraMatrix, distCoeffs);
	std::memcpy(dst, dstR.data, dstR.cols * dstR.rows *2);

	return pixel_size;
	/*std::cout << "wxh" << width << "x" << height << "d wxh" << dstR.cols << "x" << dstR.rows << std::endl;*/

	//cv::Size patternSize(8, 5);
	//cv::Mat cameraMatrix;
	//cv::Mat distCoeffs;
	//float pixel_size = 0;
	//bool ret = camera_calibration(gray, patternSize,28.0f,cameraMatrix, distCoeffs, pixel_size);
	//if (ret) 
	//{
	//	std::cout << "f=" << pixel_size << std::endl;

	//	// 打印 cameraMatrix
	//	std::cout << "cameraMatrix: " << std::endl;
	//	for (int i = 0; i < cameraMatrix.rows; ++i) {
	//		for (int j = 0; j < cameraMatrix.cols; ++j) {
	//			std::cout << cameraMatrix.at<double>(i, j) << " ";
	//		}
	//		std::cout << std::endl;
	//	}

	//	// 打印 distCoeffs
	//	std::cout << "distCoeffs: " << std::endl;
	//	for (int i = 0; i < distCoeffs.rows; ++i) {
	//		for (int j = 0; j < distCoeffs.cols; ++j) {
	//			std::cout << distCoeffs.at<double>(i, j) << " ";
	//		}
	//		std::cout << std::endl;
	//	}

	//	std::cout << "cameraMatrix: " << cameraMatrix << std::endl;
	//	std::cout << "distCoeffs: " << distCoeffs << std::endl;
	//}
	
}

