#include "../include/PBImageProcess.h"
#include <iostream>


//区域生长算法
int RegionGrow(cv::Mat& src, cv::Mat& matDst, cv::Point2i pt, int th)
{
	cv::Point2i ptGrowing;
	int nGrowLable = 0;
	int nSrcValue = 0;
	int nCurValue = 0;
	int mat_cnt = 0;
	matDst = cv::Mat::zeros(src.size(), CV_8UC1);
	int DIR[8][2] = { { -1, -1 }, { 0, -1 }, { 1, -1 }, { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 } };
	std::vector<cv::Point2i> vcGrowPt;
	vcGrowPt.push_back(pt);
	matDst.at<uchar>(pt.y, pt.x) = 255;

	while (!vcGrowPt.empty())						//生长栈不为空则生长
	{
		pt = vcGrowPt.back();						//取出一个生长点
		vcGrowPt.pop_back();

		std::vector<cv::Point2i> temp_vcGrowPt;						//临时生长点栈
		int temp_vcGrowPt_size = 0;	//可生长方向数量，因为存在被其余生长点先生长情况，不可直接使用temp_vcGrowPt.size()
		nSrcValue = src.at<uchar>(pt.y, pt.x);
		//分别对八个方向上的点进行生长
		for (int i = 0; i < 8; ++i)
		{
			ptGrowing.x = pt.x + DIR[i][0];
			ptGrowing.y = pt.y + DIR[i][1];
			//检查是否是边缘点
			if (ptGrowing.x < 0 || ptGrowing.y < 0 || ptGrowing.x >(src.cols - 1) || (ptGrowing.y > src.rows - 1))
				continue;

			nGrowLable = matDst.at<uchar>(ptGrowing.y, ptGrowing.x);		//当前待生长点的灰度值
			if (nGrowLable == 0)					//如果标记点还没有被生长
			{
				nCurValue = src.at<uchar>(ptGrowing.y, ptGrowing.x);
				if (abs(nCurValue - nSrcValue) < th)					//在阈值范围内则生长
				{
					// matDst.at<uchar>(ptGrowing.y, ptGrowing.x) = 255;
					temp_vcGrowPt_size++;
					temp_vcGrowPt.push_back(ptGrowing);					//将下一个生长点压入栈中
				}
			}
			else {
				temp_vcGrowPt_size++;
			}
		}
		//相邻的生长点不是单向生长，则生长点有效
		if (temp_vcGrowPt_size >= 1) {
			mat_cnt++;
			matDst.at<uchar>(pt.y, pt.x) = 255;
			vcGrowPt.insert(vcGrowPt.end(), temp_vcGrowPt.begin(), temp_vcGrowPt.end());
		}
	}
	return mat_cnt;
	// bitwise_and(src, matDst, matDst); //与运算可以保留原图像数据
}

// 定义拟合圆形的函数
void FitCircleCenter(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R)
{
	//定义计算中间变量
	double sumX1 = 0.0; //代表Xi的和(从1~n) ，X1代表X的1次方
	double sumY1 = 0.0;
	double sumX2 = 0.0;	//代表(Xi)^2的和(i从1~n)，X2代表X的二次方
	double sumY2 = 0.0;
	double sumX3 = 0.0;
	double sumY3 = 0.0;
	double sumX1Y1 = 0.0;
	double sumX1Y2 = 0.0;
	double sumX2Y1 = 0.0;
	const double N = (double)Circle_Data.size();//获得输入点的个数

	for (int i = 0; i < Circle_Data.size(); ++i)//遍历组中所有数据
	{
		double x = 0;
		double y = 0;
		x = Circle_Data[i].x;			//获得组中第i个点的x坐标
		y = Circle_Data[i].y;			//获得组中第i个点的y坐标
		double x2 = x * x;				//计算x^2
		double y2 = y * y;				//计算y^2
		double x3 = x2 * x;				//计算x^3
		double y3 = y2 * y;				//计算y^3
		double xy = x * y;				//计算xy
		double x1y2 = x * y2;			//计算x*y^2
		double x2y1 = x2 * y;			//计算x^2*y

		sumX1 += x;						//sumX=sumX+x;计算x坐标的和
		sumY1 += y;						//sumY=sumY+y;计算y坐标的和
		sumX2 += x2;					//计算x^2的和
		sumY2 += y2;					//计算各个点的y^2的和
		sumX3 += x3;					//计算x^3的和
		sumY3 += y3;
		sumX1Y1 += xy;
		sumX1Y2 += x1y2;
		sumX2Y1 += x2y1;
	}
	double C = N * sumX2 - sumX1 * sumX1;
	double D = N * sumX1Y1 - sumX1 * sumY1;
	double E = N * sumX3 + N * sumX1Y2 - (sumX2 + sumY2) * sumX1;
	double G = N * sumY2 - sumY1 * sumY1;
	double H = N * sumX2Y1 + N * sumY3 - (sumX2 + sumY2) * sumY1;

	double denominator = C * G - D * D;
	double a = (H * D - E * G) / (denominator);
	double b = (H * C - E * D) / (-denominator);
	double c = -(a * sumX1 + b * sumY1 + sumX2 + sumY2) / N;

	Circle_Center.x = a / (-2);
	Circle_Center.y = b / (-2);
	Circle_R = std::sqrt(a * a + b * b - 4 * c) / 2;
}


int RANSAC_FitCircleCenter(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R, float thresh)
{
	// 定义RANSAC迭代次数和最小样本数
	int iterations = 1000;
	int min_samples = 3;

	// 使用RANSAC算法拟合圆形
	float best_radius = 0;
	Point2f best_center;
	std::vector<int> is_inlier(Circle_Data.size(), 0);
	std::vector<int> is_inlier_tmp(Circle_Data.size(), 0);
	int max_inlier_num = 0;
	double max_diff = 0;
	int sample_count = 0;

	while (sample_count < iterations)
	{
		// 随机选择最小样本数个点
		vector<Point> points;
		for (int j = 0; j < min_samples; j++)
		{
			int index = rand() % Circle_Data.size();
			Point2f point(Circle_Data[index].x, Circle_Data[index].y);
			points.push_back(point);
		}

		// 使用最小二乘法拟合圆形
		float radius;
		Point2f center;
		FitCircleCenter(points, center, radius);

		// 计算所有点与圆之间的距离，以确定内点
		vector<Point2f> inliers;
		for (int i = 0; i < Circle_Data.size(); i++)
		{
			Point2f point(Circle_Data[i].x, Circle_Data[i].y);
			is_inlier_tmp[i] = 0;

			float distance = norm(point - center) - radius;
			if (abs(distance) < thresh)
			{
				is_inlier_tmp[i] = 1;
				inliers.push_back(point);
			}
		}
		// 更新最佳拟合圆形
		if (inliers.size() > max_inlier_num) {
			max_inlier_num = inliers.size();
			is_inlier = is_inlier_tmp;
			best_radius = radius;
			best_center = center;
		}
		//6. 更新迭代的最佳次数
		if (inliers.size() == 0)
		{
			iterations = 1000;
		}
		else
		{
			double epsilon = 1.0 - double(inliers.size()) / (double)Circle_Data.size(); //野值点比例
			double p = 0.9;                                                //所有样本中存在1个好样本的概率
			double s = 3.0;
			iterations = int(std::log(1.0 - p) / std::log(1.0 - std::pow((1.0 - epsilon), s)));
		}
		sample_count++;
	}
	//7. 基于最优的结果所对应的内点做最终拟合
	std::vector<cv::Point2f> inliers;
	inliers.reserve(max_inlier_num);
	for (int i = 0; i < is_inlier.size(); i++)
	{
		if (1 == is_inlier[i])
		{
			inliers.push_back(Circle_Data[i]);
		}
	}
	float radius = 0.0f;
	cv::Point2f center;
	if (max_inlier_num == 0) {
		return 0;
	}
	else {
		minEnclosingCircle(inliers, center, radius);
		Circle_R = radius;
		Circle_Center = center;
		return 1;
	}
}

void RANSAC_FitCircleCenter_with_throw(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R)
{
	float thresh = 2;
	int ret = 0;

	while (1) {
		if (Circle_Data.size() < 3) {
			break;
		}
		ret = RANSAC_FitCircleCenter(Circle_Data, Circle_Center, Circle_R, thresh);
		if (ret == 0) {
			thresh = thresh + 0.5;
			continue;
		}
		break;
	}
}

int IJIsoData(int* data)
{
	int level;
	int maxValue = 256 - 1;
	double result, sum1, sum2, sum3, sum4;
	int count0 = data[0];
	data[0] = 0; //set to zero so erased areas aren't included
	int countMax = data[maxValue];
	data[maxValue] = 0;
	int min = 0;
	while ((data[min] == 0) && (min < maxValue))
		min++;
	int max = maxValue;
	while ((data[max] == 0) && (max > 0))
		max--;
	if (min >= max) {
		data[0] = count0; data[maxValue] = countMax;
		level = 256 / 2;
		return level;
	}
	int movingIndex = min;
	int inc = std::max(max / 40, 1);
	do {
		sum1 = sum2 = sum3 = sum4 = 0.0;
		for (int i = min; i <= movingIndex; i++) {
			sum1 += (double)i * data[i];
			sum2 += data[i];
		}
		for (int i = (movingIndex + 1); i <= max; i++) {
			sum3 += (double)i * data[i];
			sum4 += data[i];
		}
		result = (sum1 / sum2 + sum3 / sum4) / 2.0;
		movingIndex++;
	} while ((movingIndex + 1) <= result && movingIndex < max - 1);
	data[0] = count0; data[maxValue] = countMax;
	level = (int)round(result);
	return level;
}

int defaultIsoData(int* data)
{
	int n = 256;
	int* data2 = new int[n];
	int mode = 0, maxCount = 0;
	for (int i = 0; i < n; i++) {
		int count = data[i];
		data2[i] = count;
		if (count > maxCount) {
			maxCount = count;
			mode = i;
		}
	}
	int maxCount2 = 0;
	for (int i = 0; i < n; i++) {
		if ((data2[i] > maxCount2) && (i != mode))
			maxCount2 = data2[i];
	}
	int hmax = maxCount;
	if ((hmax > (maxCount2 * 2)) && (maxCount2 != 0)) {
		hmax = (int)(maxCount2 * 1.5);
		data2[mode] = hmax;
	}
	int value = IJIsoData(data2);
	delete[] data2;
	return value;
}
//
//Mat render_mask_image(Mat src, Mat pseudoImg, int brightness_offset, double contrast_factor, double opacity_factor)
//{
//	Mat dst = pseudoImg.clone();
//	if (src.type() != CV_16UC1 || pseudoImg.type() != CV_8UC3)
//	{
//		return dst;
//	}
//	//brightness_offset:亮度偏移范围 -255 到 +255
//	//contrast_factor:对比度因子范围 0.1 到 3.0（1.0为不变）
//	//opacity_factor:透明度因子范围 0 到 1（0为透明，1为不透明）
//
//	Mat img8bit;
//	src.convertTo(img8bit, CV_8UC1, 0.00390625);
//	Mat img_brightness_contrast;
//	img8bit.convertTo(img_brightness_contrast, -1, contrast_factor, brightness_offset);
//	Mat img_with_opacity = img_brightness_contrast * opacity_factor;
//	img_with_opacity = cv::min(img_with_opacity, 255.0);
//	img_with_opacity = cv::max(img_with_opacity, 0.0);
//	img_with_opacity.convertTo(img_with_opacity, CV_8UC1);
//
//	Mat img_with_opacity_rgb;
//	cvtColor(img_with_opacity, img_with_opacity_rgb, COLOR_GRAY2BGR);  // 将单通道灰度图像转换为三通道RGB图像
//
//	for (int y = 0; y < pseudoImg.rows; y++) {
//		for (int x = 0; x < pseudoImg.cols; x++) {
//			Vec3b pixel = pseudoImg.at<Vec3b>(y, x);
//			if (pixel == Vec3b(0, 0, 0)) {
//				dst.at<Vec3b>(y, x) = img_with_opacity_rgb.at<Vec3b>(y, x);
//			}
//		}
//	}
//	return dst;
//}
Mat bgr_scale_image(Mat src, float maxVal, float minVal)
{
	int w = src.cols;
	int h = src.rows;
	int start = 2 * h / 255;
	Mat image = Mat(h + 2 * start, w + w + 100, CV_8UC3);
	image.setTo(255);
	src.copyTo(image(Rect(0, start, w, h)));

	int scale_w = 40;
	int scale_h = 5;
	int numTicks = 10;
	float tickInterval = float(maxVal - minVal) / numTicks;

	for (int i = 0; i <= numTicks; i++)
	{
		int value = minVal + i * tickInterval;
		int yPos = h - (i * h / numTicks);
		line(image, Point(w, yPos + start), Point(scale_w + w, yPos + start), Scalar(0, 0, 0), scale_h);
		putText(image, to_string(int(value)), Point(scale_w * 1.5 + w, yPos + 5 + start), FONT_HERSHEY_SIMPLEX, 1, Scalar(0, 0, 0), 2, LINE_AA);
		for (int j = 1; j < 5; j++) {
			int smallTickY = yPos - (h / numTicks) * j / 5;
			line(image, Point(w, smallTickY + start), Point(scale_w * 2 / 3 + w, smallTickY + start), Scalar(0, 0, 0), scale_h / 2);
		}
	}

	return image;
}

//int render_mask_image(Mat src, Mat mask, Mat dst, float max, float min, ColorTable color, bool reverse)
//{
//	
//	if (src.type() != CV_16UC1 || mask.type() != CV_16UC1)
//	{
//		return -1;
//	}
//	uint8_t bgr_tab[445][3] = { 0 };
//	switch (color)
//	{
//	case YellowHot:
//		if (reverse) {
//			int n = 0;
//			for (int i = 67; i <= 255; i++, n++) {
//				bgr_tab[n][0] = 0;
//				bgr_tab[n][1] = 0;
//				bgr_tab[n][2] = i;
//			}
//			for (int i = 0; i <= 255; i++, n++)
//			{
//				bgr_tab[n][0] = 0;
//				bgr_tab[n][1] = i;
//				bgr_tab[n][2] = 255;
//			}
//		}
//		else {
//			int n = 0;
//			for (int i = 255; i >= 0; i--, n++) {
//				bgr_tab[n][0] = 0;
//				bgr_tab[n][1] = i;
//				bgr_tab[n][2] = 255;
//			}
//			for (int i = 255; i >= 67; i--, n++)
//			{
//				bgr_tab[n][0] = 0;
//				bgr_tab[n][1] = 0;
//				bgr_tab[n][2] = i;
//			}
//		}
//		break;
//
//	default:break;
//	}
//
//	
//
//	Mat temp;
//	src.convertTo(temp, CV_8UC1, 0.00390625);
//	cvtColor(temp, dst, COLOR_GRAY2BGR);
//	
//	uint16_t* pMask = mask.ptr<uint16_t>(0);
//	uint8_t* pDst = dst.ptr<uint8_t>(0);
//	
//	float rotia = 444.0 / (max - min);
//	for (int n = 0; n < src.rows * src.cols; n++)
//	{
//		if (*pMask > min && *pMask <= max)
//		{
//			int c = (*pMask - min) * rotia;
//			*(pDst + 0) = bgr_tab[c][0];
//			*(pDst + 1) = bgr_tab[c][1];
//			*(pDst + 2) = bgr_tab[c][2];
//		}
//		pDst += 3;
//		pMask++;
//	}
//	
//	return 0;
//}

//int blendImages(const Mat& src, const Mat& mark, const Mat& dst, double alpha)
//{
//	// 检查输入图像的类型和大小
//	if (src.type() != CV_16UC1 || mark.type() != CV_8UC3)
//	{
//		return -1; // 错误处理
//	}
//
//	// 将 alpha 从 0-100 的范围转换为 0-1
//	double alpha_normalized = alpha / 100.0;
//
//	// 将 src 从 16 位转换为 8 位
//	Mat src8U;
//	src.convertTo(src8U, CV_8UC1, 1.0 / 256); // 将16位值缩放到0-255范围
//
//	// 将 src8U 转换为彩色图像，以便与 mark 融合
//	Mat srcColor;
//	cvtColor(src8U, srcColor, COLOR_GRAY2RGB); // 转换为 BGR 彩色图像
//
//	// 创建一个输出图像
//	Mat blended;
//
//	// 使用 addWeighted 进行融合
//	addWeighted(srcColor, 1, mark,alpha_normalized, 0.0, blended);
//	blended.copyTo(dst);
//	return 1; // 成功
//}


//int render_image(Mat src, Mat& dst, float max, float min, ColorTable color, bool reverse)
//{
//	if (src.type() != CV_16UC1)
//	{
//		return -1;
//	}
//	uint8_t bgr_tab[445][3] = { 0 };
//	switch (color)
//	{
//	case YellowHot:
//		if (reverse) {
//			int n = 0;
//			for (int i = 67; i <= 255; i++, n++) {
//				bgr_tab[n][0] = 0;
//				bgr_tab[n][1] = 0;
//				bgr_tab[n][2] = i;
//			}
//			for (int i = 0; i <= 255; i++, n++)
//			{
//				bgr_tab[n][0] = 0;
//				bgr_tab[n][1] = i;
//				bgr_tab[n][2] = 255;
//			}
//		}
//		else {
//			int n = 0;
//			for (int i = 255; i >= 0; i--, n++) {
//				bgr_tab[n][0] = 0;
//				bgr_tab[n][1] = i;
//				bgr_tab[n][2] = 255;
//			}
//			for (int i = 255; i >= 67; i--, n++)
//			{
//				bgr_tab[n][0] = 0;
//				bgr_tab[n][1] = 0;
//				bgr_tab[n][2] = i;
//			}
//		}
//		break;
//
//	default:break;
//	}
//
//
//
//	Mat temp;
//	src.convertTo(temp, CV_8UC1, 0.00390625);
//	cvtColor(temp, dst, COLOR_GRAY2BGR);
//
//	uint16_t* pMask = src.ptr<uint16_t>(0);
//	uint8_t* pDst = dst.ptr<uint8_t>(0);
//
//	float rotia = 444.0 / (max - min);
//	for (int n = 0; n < src.rows * src.cols; n++)
//	{
//		if (*pMask > min && *pMask <= max)
//		{
//			int c = (*pMask - min) * rotia;
//			*(pDst + 0) = bgr_tab[c][0];
//			*(pDst + 1) = bgr_tab[c][1];
//			*(pDst + 2) = bgr_tab[c][2];
//		}
//		pDst += 3;
//		pMask++;
//	}
//
//	return 0;
//
//}
//
//void get_bgr_tab(ColorTable color, uint8_t(*bgr_tab)[3], bool reverse)
//{
//	// uint8_t bgr_tab[256][3] = {0};
//	int n = 0;
//	
//	switch (color)
//	{
//	case YellowHot:
//		for (int i = 0; i <= 128; i++, n++) {
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 255.0 - i * 255.0 / 128.0;
//			bgr_tab[n][2] = 255;
//		}
//		n--;
//		for (int i = 0; i <= 127; i++, n++)
//		{
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 0;
//			bgr_tab[n][2] = 255.0 - i * (255.0 - 67.0) / 127.0;
//		}
//		break;
//	case Pseudo:
//		for (int i = 0; i <= 25; i++, n++) {
//			bgr_tab[n][0] = i * 255.0 / 25.0;
//			bgr_tab[n][1] = 0;
//			bgr_tab[n][2] = 0;
//		}
//		n--;
//		for (int i = 0; i <= 26; i++, n++)
//		{
//			bgr_tab[n][0] = 255;
//			bgr_tab[n][1] = i * 175.0 / 26.0;
//			bgr_tab[n][2] = 0;
//		}
//		n--;
//		for (int i = 0; i <= 24; i++, n++)
//		{
//			bgr_tab[n][0] = 255;
//			bgr_tab[n][1] = 175 + i * (255.0 - 175.0) / 24.0;
//			bgr_tab[n][2] = 0;
//		}
//		n--;
//		for (int i = 0; i <= 27; i++, n++)
//		{
//			bgr_tab[n][0] = 255 - i * 80.0 / 27.0;
//			bgr_tab[n][1] = 255;
//			bgr_tab[n][2] = 0;
//		}
//		n--;
//		for (int i = 0; i <= 25; i++, n++)
//		{
//			bgr_tab[n][0] = 175.0 - i * 175.0 / 25.0;
//			bgr_tab[n][1] = 255;
//			bgr_tab[n][2] = 0;
//		}
//		n--;
//		for (int i = 0; i <= 28; i++, n++)
//		{
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 255;
//			bgr_tab[n][2] = i * 175.0 / 28.0;
//		}
//		n--;
//		for (int i = 0; i <= 30; i++, n++)
//		{
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 255;
//			bgr_tab[n][2] = 175.0 + i * 80.0 / 30.0;
//		}
//		n--;
//		for (int i = 0; i <= 30; i++, n++)
//		{
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 255.0 - i * 80.0 / 30.0;
//			bgr_tab[n][2] = 255;
//		}
//		n--;
//		for (int i = 0; i <= 40; i++, n++)
//		{
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 175.0 - i * 175.0 / 40.0;
//			bgr_tab[n][2] = 255;
//		}
//		break;
//	case Black_Red:
//		for (int i = 0; i <= 255; i++, n++) {
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 0;
//			bgr_tab[n][2] = i;
//		}
//		break;
//	case Black_Green:
//		for (int i = 0; i <= 255; i++, n++) {
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = i;
//			bgr_tab[n][2] = i * 17.0 / 255.0;
//		}
//		break;
//	case Black_Blue:
//		for (int i = 0; i <= 255; i++, n++) {
//			bgr_tab[n][0] = i;
//			bgr_tab[n][1] = 0;
//			bgr_tab[n][2] = 0;
//		}
//		break;
//	case Black_Yley:
//		for (int i = 0; i <= 255; i++, n++) {
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = i * 125.0 / 255.0;
//			bgr_tab[n][2] = i;
//		}
//		break;
//	
//	case RGB:
//		for (int i = 0; i <= 25; i++, n++) {
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 0;
//			bgr_tab[n][2] = 0;
//		}
//		n--;
//		for (int i = 0; i <= 50; i++, n++)
//		{
//			bgr_tab[n][0] = i * 255.0 / 50.0;
//			bgr_tab[n][1] = 0;
//			bgr_tab[n][2] = 0;
//		}
//		n--;
//		for (int i = 0; i <= 50; i++, n++)
//		{
//			bgr_tab[n][0] = 255;
//			bgr_tab[n][1] = i * 255.0 / 50.0;
//			bgr_tab[n][2] = 0;
//		}
//		n--;
//		for (int i = 0; i <= 50; i++, n++)
//		{
//			bgr_tab[n][0] = 255 - i * 255.0 / 50.0;
//			bgr_tab[n][1] = 255;
//			bgr_tab[n][2] = 0;
//		}
//		n--;
//		for (int i = 0; i <= 29; i++, n++)
//		{
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 255;
//			bgr_tab[n][2] = i * 255.0 / 29.0;
//		}
//		n--;
//		for (int i = 0; i <= 51; i++, n++)
//		{
//			bgr_tab[n][0] = 0;
//			bgr_tab[n][1] = 255 - i * 255.0 / 51.0;
//			bgr_tab[n][2] = 255;
//		}
//		break;
//	case Gray:
//		for (int i = 0; i <= 255; i++, n++) {
//			bgr_tab[n][0] = i;
//			bgr_tab[n][1] = i;
//			bgr_tab[n][2] = i;
//		}
//		break;
//	default:break;
//	}
//	if (reverse)
//	{
//		for (int i = 0; i < 128; ++i) {
//			for (int j = 0; j < 3; ++j) {
//				uint8_t temp = bgr_tab[i][j];
//				bgr_tab[i][j] = bgr_tab[255 - i][j];
//				bgr_tab[255 - i][j] = temp;
//			}
//		}
//	}
//}
//
//Mat bgr_tab_image(int w, int h_onecolor, uint8_t(*bgr_tab)[3])
//{
//	Mat dst(h_onecolor * 256, w, CV_8UC3);
//	for (int n = 0; n < h_onecolor * 256; n++)
//	{
//		dst.row(n) = cv::Scalar(bgr_tab[n / h_onecolor][0], bgr_tab[n / h_onecolor][1], bgr_tab[n / h_onecolor][2]);
//	}
//	return dst;
//}
//
//int pseudo_color_processing(Mat src, Mat dst, float max, float min, uint8_t(*bgr_tab)[3])
//{
//
//	if (src.type() != CV_16UC1 || dst.type() != CV_8UC3)
//	{
//		return -1;
//	}
//	uint16_t* pSrc = src.ptr<uint16_t>(0);
//	uint8_t* pDst = dst.ptr<uint8_t>(0);
//
//	float rotia = 255.0 / (max - min);
//	for (int n = 0; n < src.rows * src.cols; n++)
//	{
//		if (*pSrc <= min)
//		{
//			*(pDst + 0) = 0;//bgr_tab[0][0];
//			*(pDst + 1) = 0;//bgr_tab[0][1];
//			*(pDst + 2) = 0;//bgr_tab[0][2];
//		}
//		else if (*pSrc > min && *pSrc <= max)
//		{
//			int c = (*pSrc - min) * rotia;
//			*(pDst + 0) = bgr_tab[c][0];
//			*(pDst + 1) = bgr_tab[c][1];
//			*(pDst + 2) = bgr_tab[c][2];
//		}
//		else
//		{
//			*(pDst + 0) = bgr_tab[255][0];
//			*(pDst + 1) = bgr_tab[255][1];
//			*(pDst + 2) = bgr_tab[255][2];
//		}
//		pDst += 3;
//		pSrc++;
//	}
//
//	return 0;
//}
//
//PseudoInfo get_pseudo_info(Mat src, int x, int y, int w, int h, float max, float min)
//{
//	PseudoInfo info;
//	info.maxOD = min;
//	info.minOD = max;
//	info.IOD = 0;
//	info.count = 0;
//	info.AOD = 0;
//
//	Mat cut;
//	Rect roi(x, y, w, h);
//	cut = src(roi).clone();
//	uint16_t* pCut = cut.ptr<uint16_t>(0);
//	for (int n = 0; n < w * h; n++)
//	{
//		if (*pCut > min)
//		{
//			if (*pCut < info.minOD)
//				info.minOD = *pCut;
//			if (*pCut > info.maxOD)
//				info.maxOD = *pCut;
//			info.IOD += *pCut;
//			info.count++;
//		}
//		pCut++;
//	}
//	if(info.count !=0 )
//		info.AOD = info.IOD / info.count;
//	else
//	{
//		info.maxOD = 0;
//		info.minOD = 0;
//	}
//	return info;
//}
//
//


Mat render_mask_image(Mat src, Mat pseudoImg, int brightness_offset, double contrast_factor, double opacity_factor)
{
    Mat dst = pseudoImg.clone();
    if (src.type() != CV_16UC1 || pseudoImg.type() != CV_8UC3)
    {
        return dst;
    }
    //brightness_offset:亮度偏移范围 -255 到 +255
    //contrast_factor:对比度因子范围 0.1 到 3.0（1.0为不变）
    //opacity_factor:透明度因子范围 0 到 1（0为透明，1为不透明）

    Mat img8bit;
    src.convertTo(img8bit, CV_8UC1, 0.00390625);
    Mat img_brightness_contrast;
    img8bit.convertTo(img_brightness_contrast, -1, contrast_factor, brightness_offset);
    Mat img_with_opacity = img_brightness_contrast * opacity_factor;
    img_with_opacity = cv::min(img_with_opacity, 255.0);
    img_with_opacity = cv::max(img_with_opacity, 0.0);
    img_with_opacity.convertTo(img_with_opacity, CV_8UC1);

    Mat img_with_opacity_rgb;
    cvtColor(img_with_opacity, img_with_opacity_rgb, COLOR_GRAY2BGR);  // 将单通道灰度图像转换为三通道RGB图像

    for (int y = 0; y < pseudoImg.rows; y++) {
        for (int x = 0; x < pseudoImg.cols; x++) {
            Vec3b pixel = pseudoImg.at<Vec3b>(y, x);
            if (pixel == Vec3b(0, 0, 0)) {
                dst.at<Vec3b>(y, x) = img_with_opacity_rgb.at<Vec3b>(y, x);
            }
        }
    }
    return dst;
}

void get_bgr_tab(ColorTable color, uint8_t(*bgr_tab)[3], bool reverse)
{
    // uint8_t bgr_tab[256][3] = {0};
    int n = 0;
    switch (color)
    {
    case YellowHot:
        for (int i = 0; i <= 128; i++, n++) {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 255.0 - i * 255.0 / 128.0;
            bgr_tab[n][2] = 255;
        }
        n--;
        for (int i = 0; i <= 127; i++, n++)
        {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 0;
            bgr_tab[n][2] = 255.0 - i * (255.0 - 67.0) / 127.0;
        }
        break;
    case Pseudo:
        for (int i = 0; i <= 25; i++, n++) {
            bgr_tab[n][0] = i * 255.0 / 25.0;
            bgr_tab[n][1] = 0;
            bgr_tab[n][2] = 0;
        }
        n--;
        for (int i = 0; i <= 26; i++, n++)
        {
            bgr_tab[n][0] = 255;
            bgr_tab[n][1] = i * 175.0 / 26.0;
            bgr_tab[n][2] = 0;
        }
        n--;
        for (int i = 0; i <= 24; i++, n++)
        {
            bgr_tab[n][0] = 255;
            bgr_tab[n][1] = 175 + i * (255.0 - 175.0) / 24.0;
            bgr_tab[n][2] = 0;
        }
        n--;
        for (int i = 0; i <= 27; i++, n++)
        {
            bgr_tab[n][0] = 255 - i * 80.0 / 27.0;
            bgr_tab[n][1] = 255;
            bgr_tab[n][2] = 0;
        }
        n--;
        for (int i = 0; i <= 25; i++, n++)
        {
            bgr_tab[n][0] = 175.0 - i * 175.0 / 25.0;
            bgr_tab[n][1] = 255;
            bgr_tab[n][2] = 0;
        }
        n--;
        for (int i = 0; i <= 28; i++, n++)
        {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 255;
            bgr_tab[n][2] = i * 175.0 / 28.0;
        }
        n--;
        for (int i = 0; i <= 30; i++, n++)
        {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 255;
            bgr_tab[n][2] = 175.0 + i * 80.0 / 30.0;
        }
        n--;
        for (int i = 0; i <= 30; i++, n++)
        {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 255.0 - i * 80.0 / 30.0;
            bgr_tab[n][2] = 255;
        }
        n--;
        for (int i = 0; i <= 40; i++, n++)
        {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 175.0 - i * 175.0 / 40.0;
            bgr_tab[n][2] = 255;
        }
        break;
    case Black_Red:
        for (int i = 0; i <= 255; i++, n++) {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 0;
            bgr_tab[n][2] = i;
        }
        break;
    case Black_Green:
        for (int i = 0; i <= 255; i++, n++) {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = i;
            bgr_tab[n][2] = i * 17.0 / 255.0;
        }
        break;
    case Black_Blue:
        for (int i = 0; i <= 255; i++, n++) {
            bgr_tab[n][0] = i;
            bgr_tab[n][1] = 0;
            bgr_tab[n][2] = 0;
        }
        break;
    case Black_Yley:
        for (int i = 0; i <= 255; i++, n++) {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = i * 125.0 / 255.0;
            bgr_tab[n][2] = i;
        }
        break;
   
    case RGB:
        for (int i = 0; i <= 25; i++, n++) {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 0;
            bgr_tab[n][2] = 0;
        }
        n--;
        for (int i = 0; i <= 50; i++, n++)
        {
            bgr_tab[n][0] = i * 255.0 / 50.0;
            bgr_tab[n][1] = 0;
            bgr_tab[n][2] = 0;
        }
        n--;
        for (int i = 0; i <= 50; i++, n++)
        {
            bgr_tab[n][0] = 255;
            bgr_tab[n][1] = i * 255.0 / 50.0;
            bgr_tab[n][2] = 0;
        }
        n--;
        for (int i = 0; i <= 50; i++, n++)
        {
            bgr_tab[n][0] = 255 - i * 255.0 / 50.0;
            bgr_tab[n][1] = 255;
            bgr_tab[n][2] = 0;
        }
        n--;
        for (int i = 0; i <= 29; i++, n++)
        {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 255;
            bgr_tab[n][2] = i * 255.0 / 29.0;
        }
        n--;
        for (int i = 0; i <= 51; i++, n++)
        {
            bgr_tab[n][0] = 0;
            bgr_tab[n][1] = 255 - i * 255.0 / 51.0;
            bgr_tab[n][2] = 255;
        }
        break;
    case Gray:
        for (int i = 0; i <= 255; i++, n++) {
            bgr_tab[n][0] = i;
            bgr_tab[n][1] = i;
            bgr_tab[n][2] = i;
        }
        break;
    default:break;
    }
    if (reverse)
    {
        for (int i = 0; i < 128; ++i) {
            for (int j = 0; j < 3; ++j) {
                uint8_t temp = bgr_tab[i][j];
                bgr_tab[i][j] = bgr_tab[255 - i][j];
                bgr_tab[255 - i][j] = temp;
            }
        }
    }
}

Mat bgr_tab_image(int w, int h_onecolor, uint8_t(*bgr_tab)[3])
{
    Mat dst(h_onecolor * 256, w, CV_8UC3);
    for (int n = 0; n < h_onecolor * 256; n++)
    {
        dst.row(h_onecolor * 256 - 1 - n) = cv::Scalar(bgr_tab[n / h_onecolor][0], bgr_tab[n / h_onecolor][1], bgr_tab[n / h_onecolor][2]);
    }
    return dst;
}

float get_img_data(Mat src, int x, int y)
{
    float data = 0.0;
    if (src.type() == CV_16UC1)
    {
        data = src.at<unsigned short>(y, x);
    }
    else if (src.type() == CV_32FC1)
    {
        data = src.at<float>(y, x);
    }
    else if (src.type() == CV_8UC1)
    {
        data = src.at<unsigned char>(y, x);
    }
    return data;
}

PseudoInfo get_pseudo_info(Mat src, Mat mask, float max, float min)
{
    PseudoInfo info;
    info.maxOD = min;
    info.minOD = max;
    info.IOD = 0;
    info.count = 0;
    info.AOD = 0;

    unsigned char* pMask = mask.ptr<unsigned char>(0);
    for (int y = 0; y < src.rows; y++)
    {
        for (int x = 0; x < src.cols; x++)
        {
            if (*pMask == 255)
            {
                float data = get_img_data(src, x, y);
                if (data > min)
                {
                    if (data < info.minOD)
                        info.minOD = data;
                    if (data > info.maxOD)
                        info.maxOD = data;
                    info.IOD += data;
                    info.count++;
                }
            }
            pMask++;
        }
    }
    if (info.count != 0)
        info.AOD = info.IOD / info.count;
    else {
        info.maxOD = 0;
        info.minOD = 0;
    }
    return info;
}

int pseudo_color_processing(Mat src, Mat dst, float max, float min, uint8_t(*bgr_tab)[3])
{
    if (dst.type() != CV_8UC3)
    {
        return -1;
    }
    uint8_t* pDst = dst.ptr<uint8_t>(0);

    float rotia = 255.0 / (max - min);
    for (int y = 0; y < src.rows; y++)
    {
        for (int x = 0; x < src.cols; x++)
        {

            float data = get_img_data(src, x, y);
            if (data <= min)
            {
                *(pDst + 0) = 0;//bgr_tab[0][0];
                *(pDst + 1) = 0;//bgr_tab[0][1];
                *(pDst + 2) = 0;//bgr_tab[0][2];
            }
            else if (data > min && data <= max)
            {
                int c = (data - min) * rotia;
                *(pDst + 0) = bgr_tab[c][0];
                *(pDst + 1) = bgr_tab[c][1];
                *(pDst + 2) = bgr_tab[c][2];
            }
            else
            {
                *(pDst + 0) = bgr_tab[255][0];
                *(pDst + 1) = bgr_tab[255][1];
                *(pDst + 2) = bgr_tab[255][2];
            }
            pDst += 3;
        }
    }
    return 0;
}

Mat bgr_scale_image(Mat src, float maxVal, float minVal, int scientific_flag)
{
    int w = src.cols;
    int h = src.rows;
    int h_color = h / 256;
    int start = 2 * h_color;
    float rotia = 255.0 / (maxVal - minVal);
    Mat image = Mat(h + 2 * start, 3 * w, CV_8UC3);
    image.setTo(255);
    src.copyTo(image(Rect(0,start,w,h)));
    cv::rectangle(image, Rect(0,start,w,h), cv::Scalar(0,0,0), 2);

    float diff = maxVal - minVal;
    int exponent = static_cast<int>(std::log10(diff));
    float normalized = diff / std::pow(10, exponent);
    int firstDigit = static_cast<int>(normalized);
    float result = firstDigit * std::pow(10, exponent);

    int numTicks = 0;
    float tickInterval = 0;
    if (firstDigit >= 6) {
        tickInterval = 2 * std::pow(10, exponent);
    }
    else if (firstDigit >= 3) {
        tickInterval = std::pow(10, exponent);
    }
    else {
        tickInterval = std::pow(10, exponent) / 2;
    }

    std::vector<float> tickData;
    float tick = tickInterval;
    while (1)
    {
        if (tick >= minVal && tick <= maxVal)
        {
            tickData.push_back(tick);
        }
        else if (tick > maxVal)
        {
            break;
        }
        tick += tickInterval;
    }
    std::vector<float> smallTickData;
    tick = tickInterval / 5.0;
    while (1)
    {
        if (tick >= minVal && tick <= maxVal)
        {
            smallTickData.push_back(tick);
        }
        else if (tick > maxVal)
        {
            break;
        }
        tick += tickInterval / 5.0;
    }


    if (scientific_flag)
    {
        std::stringstream ss;
        ss << "x10^" << exponent;
        std::string text = ss.str();
        putText(image, text, Point(2 * w, image.rows / 2), FONT_HERSHEY_SIMPLEX, 1.5, Scalar(0, 0, 0), 3, LINE_AA);
    }

    int scale_w = 40;
    int scale_h = 5;
    for (int i = 0; i < smallTickData.size(); i++)
    {
        int yPos = image.rows - start - (smallTickData[i] - minVal) * rotia * h_color;
        line(image, Point(w, yPos), Point(scale_w * 2 / 3 + w, yPos), Scalar(0, 0, 0), scale_h / 2);
    }
    for (int i = 0; i < tickData.size(); i++)
    {
        int yPos = image.rows - start - (tickData[i] - minVal) * rotia * h_color;
        line(image, Point(w, yPos), Point(scale_w + w, yPos), Scalar(0, 0, 0), scale_h);

        if (scientific_flag)
        {
            tickData[i] /= std::pow(10, exponent);
        }

        std::ostringstream oss;
        if (tickData[i] == int(tickData[i])) {
            oss << static_cast<int>(tickData[i]);
        } else {
            oss.precision(1);
            oss << std::fixed << tickData[i];
        }
        std::string text = oss.str();
        putText(image, text, Point(scale_w * 3 / 2 + w, yPos + 5), FONT_HERSHEY_SIMPLEX, 1.5, Scalar(0, 0, 0), 2, LINE_AA);
    }

    return image;
}

Mat get_photon_image(Mat src, float sec, float Wcm, float Hcm, float sr)
{
    cv::Mat src_float32;
    src.convertTo(src_float32, CV_32FC1);
    float pixel_W = Wcm / src.cols;
    float pixel_H = Hcm / src.rows;

    Mat dst = src_float32 / sec / (pixel_W * pixel_H) / sr;

    return dst;
}

Mat get_magic_wand_image(Mat src,int x,int y,float max,float min)
{
    Mat matDst = cv::Mat::zeros(src.size(), CV_8UC1);
    cv::Point2i pt(x,y);
    int w = src.cols;
    int h = src.rows;
    // int nSrcValue = src.at<uchar>(pt.y, pt.x);
    int nSrcValue = src.data[pt.y * w + pt.x];
    if(nSrcValue < min)
    {
        return matDst;
    }

    cv::Point2i ptGrowing;	
	int nGrowLable = 0;	
	int nCurValue = 0;	
    int mat_cnt = 0;
	int DIR[8][2] = { { -1, -1 }, { 0, -1 }, { 1, -1 }, { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 } };
	std::vector<cv::Point2i> vcGrowPt;	
	vcGrowPt.push_back(pt);	
	// matDst.at<uchar>(pt.y, pt.x) = 255;	
	matDst.data[pt.y * w + pt.x] = 255;	
	while (!vcGrowPt.empty())						
	{
		pt = vcGrowPt.back();						
		vcGrowPt.pop_back();

		std::vector<cv::Point2i> temp_vcGrowPt;	
		int temp_vcGrowPt_size = 0;	
        // nSrcValue = src.at<uchar>(pt.y, pt.x);
		//分别对八个方向上的点进行生长
		for (int i = 0; i < 8; ++i)
		{
			ptGrowing.x = pt.x + DIR[i][0];
			ptGrowing.y = pt.y + DIR[i][1];
			//检查是否是边缘点
			if (ptGrowing.x < 0 || ptGrowing.y < 0 || ptGrowing.x >(src.cols - 1) || (ptGrowing.y > src.rows - 1))
				continue;

			// nGrowLable = matDst.at<uchar>(ptGrowing.y, ptGrowing.x);
			nGrowLable = matDst.data[ptGrowing.y * w + ptGrowing.x];		//当前待生长点的灰度值
			if (nGrowLable == 0)					//如果标记点还没有被生长
			{
				nCurValue = src.data[ptGrowing.y * w + ptGrowing.x];
				if (nCurValue >= min)					//在阈值范围内则生长
				{
					// matDst.at<uchar>(ptGrowing.y, ptGrowing.x) = 255;
					temp_vcGrowPt_size++;
					temp_vcGrowPt.push_back(ptGrowing);					//将下一个生长点压入栈中
				}
			}
			else {
				temp_vcGrowPt_size++;
			}
		}
		//相邻的生长点不是单向生长，则生长点有效
		if (temp_vcGrowPt_size >= 1) {
            mat_cnt++;
			// matDst.at<uchar>(pt.y, pt.x) = 255;
            matDst.data[pt.y * w + pt.x] = 255;	
			vcGrowPt.insert(vcGrowPt.end(), temp_vcGrowPt.begin(), temp_vcGrowPt.end());
		}
	}
    return matDst;
}


Mat SetSharpen(Mat src) 
{
    Mat blur_img, usm;

    GaussianBlur(src,blur_img,Size(0,0),10);
    usm = src + 0.5 * (src - blur_img);
  
    return usm;
}
bool camera_calibration(Mat gray,cv::Size patternSize,float grid_size,cv::Mat& cameraMatrix,cv::Mat& distCoeffs,float& pixel_size)
{
    if(gray.type() != CV_8UC1)
    {
        std::cerr << "Error: gray not CV_8UC1!" << std::endl;
        return 0;
    }
    std::vector<cv::Point2f> corners;
    bool found = cv::findChessboardCorners(gray, patternSize, corners,
                                           cv::CALIB_CB_ADAPTIVE_THRESH | cv::CALIB_CB_NORMALIZE_IMAGE);

    if (found) {
        cv::cornerSubPix(gray, corners, cv::Size(3, 3), cv::Size(-1, -1),
                         cv::TermCriteria(cv::TermCriteria::EPS | cv::TermCriteria::MAX_ITER, 30, 0.1));
    } else {
        std::cerr << "Error: Chessboard corners not found!" << std::endl;
        return 0;
    }
    std::vector<std::vector<cv::Point3f>> objectPoints;
    std::vector<std::vector<cv::Point2f>> imagePoints;

    std::vector<cv::Point3f> objPts;
    for (int i = 0; i < patternSize.height; ++i) {
        for (int j = 0; j < patternSize.width; ++j) {
            objPts.push_back(cv::Point3f(j, i, 0.0f));
        }
    }
    objectPoints.push_back(objPts);
    imagePoints.push_back(corners);
    cameraMatrix = cv::Mat::eye(3, 3, CV_64F);  // 相机矩阵初始化
    distCoeffs = cv::Mat::zeros(8, 1, CV_64F);  // 畸变系数初始化
    std::vector<cv::Mat> rvecs, tvecs;
	int calibrationFlags = 0;
	TermCriteria criteria = TermCriteria(TermCriteria::MAX_ITER + TermCriteria::EPS, 30, DBL_EPSILON);
    cv::calibrateCamera(objectPoints, imagePoints, gray.size(), cameraMatrix, distCoeffs, rvecs, tvecs,calibrationFlags,criteria);

    vector<Point2f> undistortedCorners;
    undistortPoints(corners, undistortedCorners, cameraMatrix, distCoeffs);

    std::vector<cv::Point3f> homogeneousPoints;
    for (const auto& p : undistortedCorners) {
        homogeneousPoints.push_back(cv::Point3f(p.x, p.y, 1.0f));  // 将 (x, y) 转换为 (x, y, 1)
    }
    std::vector<cv::Point2f> pixelPoints;
    for (const auto& p : homogeneousPoints) {
        cv::Mat pointMat = (cv::Mat_<double>(3, 1) << p.x, p.y, p.z);
        cv::Mat transformedPoint = cameraMatrix * pointMat;
        float x = transformedPoint.at<double>(0, 0) / transformedPoint.at<double>(2, 0);
        float y = transformedPoint.at<double>(1, 0) / transformedPoint.at<double>(2, 0);
        pixelPoints.push_back(cv::Point2f(x, y));
    }
    int numCols = patternSize.width;
    int numRows = patternSize.height;
    float meanSideLength = 0;
    int num = 0;
    for (int i = 0; i < (patternSize.height - 1); ++i) {
        for (int j = 0; j < (patternSize.width - 1); ++j) {
            int index = i * patternSize.width + j;
            if (j + 1 < numCols) {
                float width = static_cast<float>(norm(pixelPoints[index] - pixelPoints[index + 1]));
                meanSideLength+=(width);
                num++;
            }
            if (i + 1 < patternSize.height) {
                float height = static_cast<float>(norm(pixelPoints[index] - pixelPoints[index + numCols]));
                meanSideLength+=(height);
                num++;
            }
        }
    }
    meanSideLength /= num;
    pixel_size = grid_size/meanSideLength;
    return 1;
}

Mat distortion_correction(Mat image,cv::Mat cameraMatrix,cv::Mat distCoeffs)
{
    cv::Mat undistortedImage;
    cv::undistort(image, undistortedImage, cameraMatrix, distCoeffs);
    return undistortedImage;
}

Mat mid_img_merge_deal(Mat Bgray,Mat Ggray,Mat Rgray,Rect roi)
{
    Mat dst = Mat::zeros(Bgray.rows,Bgray.cols,CV_8UC3);
    if(Rgray.type() != CV_8UC1 || Ggray.type() != CV_8UC1 || Bgray.type() != CV_8UC1)
    {
        std::cerr << "Error: Bgray||Ggray||Rgray not CV_8UC1!" << std::endl;
        return dst;
    }
    if (Bgray.size() != Ggray.size() || Bgray.size() != Rgray.size()) {
        std::cout << "Error: Images have different sizes!" << std::endl;
        return dst;
    }
    Mat channels[3] = {Bgray, Ggray, Rgray};
    Mat result = Mat::zeros(channels[0].size(), channels[0].type());
    if(roi.width == Bgray.cols && roi.height == Bgray.rows)
    {

        int ddepth = CV_32F;
        int scale = 1; 
        int delta = 0; 
        Mat grad_x_b, grad_y_b, grad_b;
        Mat grad_x_g, grad_y_g, grad_g;
        Mat grad_x_r, grad_y_r, grad_r;
        Sobel(Bgray, grad_x_b, ddepth, 1, 0, 3, scale, delta, BORDER_DEFAULT);
        Sobel(Bgray, grad_y_b, ddepth, 0, 1, 3, scale, delta, BORDER_DEFAULT);
        Sobel(Ggray, grad_x_g, ddepth, 1, 0, 3, scale, delta, BORDER_DEFAULT);
        Sobel(Ggray, grad_y_g, ddepth, 0, 1, 3, scale, delta, BORDER_DEFAULT);
        Sobel(Rgray, grad_x_r, ddepth, 1, 0, 3, scale, delta, BORDER_DEFAULT);
        Sobel(Rgray, grad_y_r, ddepth, 0, 1, 3, scale, delta, BORDER_DEFAULT);

        Mat grad_b_magnitude, grad_g_magnitude, grad_r_magnitude;
        magnitude(grad_x_b, grad_y_b, grad_b_magnitude);
        magnitude(grad_x_g, grad_y_g, grad_g_magnitude);
        magnitude(grad_x_r, grad_y_r, grad_r_magnitude);
        Mat grad_combined = grad_b_magnitude + grad_g_magnitude + grad_r_magnitude;
        Mat grad_display;
        convertScaleAbs(grad_combined, grad_display);

        int pixel_count[256] = {0};
        unsigned char* pixel_point = grad_display.data;
        for (int n = 0; n < Bgray.rows * Bgray.cols; n++) {
            pixel_count[*pixel_point++]++;
        }
        int temp = 0;
        int upperSize = 3000;
        int max_val = 0;
        int value = 255;
        for (int i = 255; i > 0; i--)
        {
            temp += pixel_count[i];
            if(temp > 0 && max_val == 0){
                max_val = i;
            }
            if (temp > upperSize) {
                value = i - 1;
                break;
            }
        }
        if(value > max_val - 5)
        {
            value = max_val - 5;
        }else if(value < max_val - 10)
        {
            value = max_val - 10;
        }
        Mat binary;
        threshold(grad_display, binary, value, 255, THRESH_BINARY);
        Mat kernel = getStructuringElement(MORPH_RECT, Size(11, 11));
        Mat dilated;
        cv::dilate(binary, dilated, kernel);
        cv::erode(dilated, binary, kernel);

        vector<vector<Point>> contours;
        vector<Vec4i> hierarchy;
        findContours(binary, contours, hierarchy, RETR_EXTERNAL, CHAIN_APPROX_SIMPLE);
        Point center(Bgray.cols / 2, Bgray.rows / 2);
        double minAvgDistance = DBL_MAX;
        int closestContourIndex = -1;
        for (size_t i = 0; i < contours.size(); i++) {
            vector<Point>& contour = contours[i];
            double area = contourArea(contour);
            if (area <= 5000) continue;
            
            double totalDistance = 0.0;
            int validPointCount = 0;
            int jump_step = contour.size() / 50 + 1;
            for (int j = 0; j < contour.size(); j += jump_step) {
                double distance = norm(contour[j] - center);
                totalDistance += distance;
                validPointCount++;
            }
            double avgDistance = (validPointCount > 0) ? totalDistance / validPointCount : 0;
            if (avgDistance < minAvgDistance) {
                minAvgDistance = avgDistance;
                closestContourIndex = i;
            }
        }
        drawContours(result, contours, closestContourIndex, Scalar(255), FILLED);       
    }
    else{
        rectangle(result, roi, Scalar(255), FILLED);
    }
    for(int n = 0; n < 3; n++){
        Mat temp;
        channels[n].copyTo(temp, result);
        channels[n] = temp.clone();
    }
    merge(channels, 3, dst);
    return dst;
}