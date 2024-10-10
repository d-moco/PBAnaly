#include "../include/PBColony.h"


int PBColony::RegionGrow(cv::Mat& src, cv::Mat& matDst, cv::Point2i pt, int th)
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

	while (!vcGrowPt.empty())
	{
		pt = vcGrowPt.back();
		vcGrowPt.pop_back();

		std::vector<cv::Point2i> temp_vcGrowPt;	
		int temp_vcGrowPt_size = 0;	
		nSrcValue = src.at<uchar>(pt.y, pt.x);
		for (int i = 0; i < 8; ++i)
		{
			ptGrowing.x = pt.x + DIR[i][0];
			ptGrowing.y = pt.y + DIR[i][1];
			if (ptGrowing.x < 0 || ptGrowing.y < 0 || ptGrowing.x >(src.cols - 1) || (ptGrowing.y > src.rows - 1))
				continue;

			nGrowLable = matDst.at<uchar>(ptGrowing.y, ptGrowing.x);
			if (nGrowLable == 0)
			{
				nCurValue = src.at<uchar>(ptGrowing.y, ptGrowing.x);
				if (abs(nCurValue - nSrcValue) < th)
				{
					temp_vcGrowPt_size++;
					temp_vcGrowPt.push_back(ptGrowing);
				}
			}
			else {
				temp_vcGrowPt_size++;
			}
		}
		if (temp_vcGrowPt_size >= 1) {
			mat_cnt++;
			matDst.at<uchar>(pt.y, pt.x) = 255;
			vcGrowPt.insert(vcGrowPt.end(), temp_vcGrowPt.begin(), temp_vcGrowPt.end());
		}
	}
	return mat_cnt;
}

void PBColony::FitCircleCenter(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R)
{

	double sumX1 = 0.0; 
	double sumY1 = 0.0;
	double sumX2 = 0.0;
	double sumY2 = 0.0;
	double sumX3 = 0.0;
	double sumY3 = 0.0;
	double sumX1Y1 = 0.0;
	double sumX1Y2 = 0.0;
	double sumX2Y1 = 0.0;
	const double N = (double)Circle_Data.size();

	for (int i = 0; i < Circle_Data.size(); ++i)
	{
		double x = 0;
		double y = 0;
		x = Circle_Data[i].x;
		y = Circle_Data[i].y;
		double x2 = x * x;
		double y2 = y * y;
		double x3 = x2 * x;
		double y3 = y2 * y;	
		double xy = x * y;	
		double x1y2 = x * y2;	
		double x2y1 = x2 * y;	

		sumX1 += x;						
		sumY1 += y;				
		sumX2 += x2;		
		sumY2 += y2;
		sumX3 += x3;	
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

int PBColony::RANSAC_FitCircleCenter(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R, float thresh)
{
	int iterations = 1000;
	int min_samples = 3;

	float best_radius = 0;
	Point2f best_center;
	std::vector<int> is_inlier(Circle_Data.size(), 0);
	std::vector<int> is_inlier_tmp(Circle_Data.size(), 0);
	int max_inlier_num = 0;
	double max_diff = 0;
	int sample_count = 0;

	while (sample_count < iterations)
	{
		vector<Point> points;
		for (int j = 0; j < min_samples; j++)
		{
			int index = rand() % Circle_Data.size();
			Point2f point(Circle_Data[index].x, Circle_Data[index].y);
			points.push_back(point);
		}
		float radius;
		Point2f center;
		FitCircleCenter(points, center, radius);
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
		if (inliers.size() > max_inlier_num) {
			max_inlier_num = inliers.size();
			is_inlier = is_inlier_tmp;
			best_radius = radius;
			best_center = center;
		}
		if (inliers.size() == 0)
		{
			iterations = 1000;
		}
		else
		{
			double epsilon = 1.0 - double(inliers.size()) / (double)Circle_Data.size();
			double p = 0.9; 
			double s = 3.0;
			iterations = int(std::log(1.0 - p) / std::log(1.0 - std::pow((1.0 - epsilon), s)));
		}
		sample_count++;
	}
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

void PBColony::RANSAC_FitCircleCenter_with_throw(vector<Point>& Circle_Data, Point2f& Circle_Center, float& Circle_R)
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

int PBColony::fine_grow_circle(Mat input, Point2f& center, float& radius)
{
	cv::Mat matDst;
	int mat_cnt = 0;
	int cal_cnt = 0;
	int grow_flag = 0;
	cv::Point2i pt(input.cols / 2, input.rows / 2);
	while (1)
	{
		mat_cnt = RegionGrow(input, matDst, pt, 3);
		if (mat_cnt > input.cols * input.rows / 10)
		{
			grow_flag = 1;
			break;
		}
		else if (cal_cnt > 10)
		{
			break;
		}
		pt = pt + cv::Point2i(2, 0);
		cal_cnt++;
	}


	if (grow_flag)
	{
		std::vector<std::vector<cv::Point>> contours;
		cv::findContours(matDst, contours, cv::RETR_EXTERNAL, cv::CHAIN_APPROX_SIMPLE);
		Mat drawImg = Mat::zeros(input.rows, input.cols, CV_8UC1);
		cv::drawContours(drawImg, contours, -1, Scalar(255, 255, 255), 1);
		int contour_size_th = (input.rows + input.cols) / 3;
		int radiusTh = std::min(input.cols, input.rows) / 4;
		float maxRadius = input.cols;
		cv::Point2f maxCenter;
		for (size_t i = 0; i < contours.size(); i++) {
			if (contours[i].size() < contour_size_th)
			{
				continue;
			}
			Point2f now_center;
			float now_radius;
			RANSAC_FitCircleCenter_with_throw(contours[i], now_center, now_radius);
			if (now_radius > radiusTh)
			{
				if (now_radius < maxRadius) {
					maxRadius = now_radius;
					maxCenter = now_center;
				}
			}
		}
		if (maxRadius < input.cols) {
			center = maxCenter;
			radius = maxRadius;
			return 1;
		}
		else {
			return 0;
		}
	}
	else {
		return 0;
	}
}

int PBColony::fine_max_circle(Mat input, Point2f& center, float& radius)
{
	Mat blurImg;
	cv::medianBlur(input, blurImg, 3);
	Mat edgeImg;
	cv::Canny(blurImg, edgeImg, 0, 255);

	std::vector<std::vector<cv::Point>> contours;
	cv::findContours(edgeImg, contours, cv::RETR_EXTERNAL, cv::CHAIN_APPROX_SIMPLE);
	cv::drawContours(edgeImg, contours, -1, Scalar(255, 255, 255), 5);
	contours.clear();

	cv::findContours(edgeImg, contours, cv::RETR_EXTERNAL, cv::CHAIN_APPROX_SIMPLE);
	Mat drawImg = Mat::zeros(input.rows, input.cols, CV_8UC1);
	cv::drawContours(drawImg, contours, -1, Scalar(255, 255, 255), 1);

	int contour_size_th = (input.rows + input.cols) / 3;
	int radiusTh = std::min(edgeImg.cols, edgeImg.rows) / 4;
	float maxRadius = edgeImg.cols;
	cv::Point2f maxCenter;
	for (size_t i = 0; i < contours.size(); i++) {
		if (contours[i].size() < contour_size_th)
		{
			continue;
		}
		Point2f now_center;
		float now_radius;
		RANSAC_FitCircleCenter_with_throw(contours[i], now_center, now_radius);
		if (now_radius > radiusTh)
		{
			if (now_radius < maxRadius) {
				maxRadius = now_radius;
				maxCenter = now_center;
			}
		}

	}
	if (maxRadius < edgeImg.cols) {
		center = maxCenter;
		radius = maxRadius;
		return 1;
	}
	else {
		return 0;
	}
}

int PBColony::IJIsoData(int* data) 
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
int PBColony::defaultIsoData(int* data) 
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


PBColony::PBColony() {}

PBColony::~PBColony() {}


int PBColony::colony_get_circle(Mat& src, Point2f& center, float& radius)
{
	Point2f center1;
	float radius1;
	int ret1 = fine_grow_circle(src, center1, radius1);

	Point2f center2;
	float radius2;
	int ret2 = fine_max_circle(src, center2, radius2);

	if (ret1 && ret2)
	{
		double distance = sqrt(pow(center1.x - center2.x, 2) + pow(center1.y - center2.y, 2));
		if (distance + radius1 <= radius2) {
			center = center1;
			radius = radius1 - 3;
		}
		else {
			center = center2;
			radius = radius2 - 10;
		}
		return 1;
	}
	else if (ret2)
	{
		center = center2;
		radius = radius2 - 10;
		return 1;
	}
	else
	{
		return 0;
	}
}

cv::Mat PBColony::generateMaskImage(int img_width, int img_height, int rect_x, int rect_y, int rect_width, int rect_height)
{
	cv::Mat image = cv::Mat::zeros(img_height, img_width, CV_8UC1);
	cv::Point ellipse_center(rect_x + rect_width / 2, rect_y + rect_height / 2);
	cv::Size ellipse_axes(rect_width / 2, rect_height / 2);
	cv::Scalar white_color(255);
	cv::ellipse(image, ellipse_center, ellipse_axes, 0, 0, 360, white_color, -1);
	return image;
}

cv::Mat PBColony::get_lower_upper(Mat& input_cn1, Mat& mask, int& lower, int& upper)
{
	input_cn1 = input_cn1 & mask;

	if (lower == -1 || upper == -1)
	{
		int pixel_count[256] = { 0 };
		unsigned char* pixel_point = input_cn1.data;
		for (int n = 0; n < input_cn1.rows * input_cn1.cols; n++) {
			pixel_count[*pixel_point++]++;
		}
		int value = defaultIsoData(pixel_count);
		int lowsum = std::accumulate(pixel_count + 1, pixel_count + value, 0);
		int upsum = std::accumulate(pixel_count + value, pixel_count + 255, 0);
		if (lowsum > upsum)
		{
			image_inverted_flag = 0;
			lower = value;
			upper = 255;
		}
		else
		{
			image_inverted_flag = 1;
			lower = 0;
			upper = value;
		}
	}

	Mat bin;
	cv::inRange(input_cn1, lower, upper, bin);
	bin = bin & mask;

	return bin;
}


void PBColony::init_classify_standard(ClassifyStandard& class_stand)
{
	class_stand.num = 2;
	class_stand.mind = 1;
	class_stand.maxd = 65535;
	class_stand.interval.clear();
	float data_interval = (class_stand.maxd - class_stand.mind) / (class_stand.num - 1);
	for (int n = 0; n < class_stand.num; n++)
	{
		class_stand.interval.push_back(class_stand.mind + n * data_interval);
	}
	class_stand.classes = AREA;
}

void PBColony::set_classify_standard_num(ClassifyStandard& class_stand, int num)
{
	class_stand.num = num;
	class_stand.interval.clear();
	float data_interval = (class_stand.maxd - class_stand.mind) / (class_stand.num - 1);
	for (int n = 0; n < class_stand.num; n++)
	{
		class_stand.interval.push_back(class_stand.mind + n * data_interval);
	}
}

void PBColony::set_classify_standard_classes(ClassifyStandard& class_stand, dataClass classes)
{
	class_stand.classes = classes;
}

void PBColony::set_classify_standard_interval(ClassifyStandard& class_stand, int n, float data)
{
	if (n < class_stand.num)
	{
		class_stand.interval[n] = data;
	}
}


vector<ColonyInfo> PBColony::get_colony_info(Mat src, Mat bin, Mat& dst_cn3, ClassifyStandard class_stand, bool inverted_flag)
{
	vector<ColonyInfo> Cinfo;
	std::vector<std::vector<cv::Point>> contours;
	cvtColor(src, dst_cn3, COLOR_GRAY2BGR);
	cv::findContours(bin, contours, cv::RETR_LIST, cv::CHAIN_APPROX_SIMPLE);
	if (inverted_flag)
	{
		src = 255 - src;
	}
	int cnt_colony = 0;
	for (int i = 0; i < contours.size(); i++)
	{
		cv::Mat mask = cv::Mat::zeros(src.rows, src.cols, CV_8UC1);
		cv::drawContours(mask, contours, i, cv::Scalar(255), 1);
		int perimeter = cv::countNonZero(mask);
		cv::drawContours(mask, contours, i, cv::Scalar(255), -1);
		int area = cv::countNonZero(mask);

		if (perimeter < 8) {
			continue;
		}
		cnt_colony++;
		cv::putText(dst_cn3, std::to_string(cnt_colony), contours[i][0], cv::FONT_HERSHEY_SIMPLEX, 0.5, Scalar(255, 0, 0), 1);
		cv::drawContours(dst_cn3, contours, i, Scalar(0, 0, 255), 1);

		float diameter = std::sqrt(4 * area / 3.141592653);
		int pixel_sum = cv::sum(src & mask)[0];

		int n = 0;
		switch (class_stand.classes)
		{
		case AREA:
			for (; n < class_stand.num; n++)
			{
				if (class_stand.interval[n] > area)  break;
			}
			break;
		case PERIMETER:
			for (; n < class_stand.num; n++)
			{
				if (class_stand.interval[n] > perimeter)  break;
			}
			break;
		case DIAMETER:
			for (; n < class_stand.num; n++)
			{
				if (class_stand.interval[n] > diameter)  break;
			}
			break;
		case IOD:
			for (; n < class_stand.num; n++)
			{
				if (class_stand.interval[n] > pixel_sum)  break;
			}
			break;
		default:
			break;
		}

		ColonyInfo info;
		info.IDX = cnt_colony;
		info.area = area;
		info.perimeter = perimeter;
		info.diameter = diameter;
		info.IOD = pixel_sum;
		info.classify = n;

		Cinfo.push_back(info);
	}
	return Cinfo;
}

ColonyStatistic PBColony::get_colony_statistics(vector<ColonyInfo> Cinfo)
{
	ColonyStatistic CStatistic;
	CStatistic.area.maxd = Cinfo[0].area;
	CStatistic.area.maxIDX = Cinfo[0].IDX;
	CStatistic.area.mind = Cinfo[0].area;
	CStatistic.area.minIDX = Cinfo[0].IDX;
	CStatistic.area.sum = Cinfo[0].area;

	CStatistic.perimeter.maxd = Cinfo[0].perimeter;
	CStatistic.perimeter.maxIDX = Cinfo[0].IDX;
	CStatistic.perimeter.mind = Cinfo[0].perimeter;
	CStatistic.perimeter.minIDX = Cinfo[0].IDX;
	CStatistic.perimeter.sum = Cinfo[0].perimeter;

	CStatistic.diameter.maxd = Cinfo[0].diameter;
	CStatistic.diameter.maxIDX = Cinfo[0].IDX;
	CStatistic.diameter.mind = Cinfo[0].diameter;
	CStatistic.diameter.minIDX = Cinfo[0].IDX;
	CStatistic.diameter.sum = Cinfo[0].diameter;

	CStatistic.IOD.maxd = Cinfo[0].IOD;
	CStatistic.IOD.maxIDX = Cinfo[0].IDX;
	CStatistic.IOD.mind = Cinfo[0].IOD;
	CStatistic.IOD.minIDX = Cinfo[0].IDX;
	CStatistic.IOD.sum = Cinfo[0].IOD;

	CStatistic.classify.maxd = Cinfo[0].classify;
	CStatistic.classify.maxIDX = Cinfo[0].IDX;
	CStatistic.classify.mind = Cinfo[0].classify;
	CStatistic.classify.minIDX = Cinfo[0].IDX;
	CStatistic.classify.sum = Cinfo[0].classify;

	for (int i = 1; i < Cinfo.size(); i++)
	{
		if (CStatistic.area.maxd < Cinfo[i].area)
		{
			CStatistic.area.maxd = Cinfo[i].area;
			CStatistic.area.maxIDX = Cinfo[i].IDX;
		}
		if (CStatistic.area.mind > Cinfo[i].area)
		{
			CStatistic.area.mind = Cinfo[i].area;
			CStatistic.area.minIDX = Cinfo[i].IDX;
		}
		CStatistic.area.sum += Cinfo[i].area;

		if (CStatistic.perimeter.maxd < Cinfo[i].perimeter)
		{
			CStatistic.perimeter.maxd = Cinfo[i].perimeter;
			CStatistic.perimeter.maxIDX = Cinfo[i].IDX;
		}
		if (CStatistic.perimeter.mind > Cinfo[i].perimeter)
		{
			CStatistic.perimeter.mind = Cinfo[i].perimeter;
			CStatistic.perimeter.minIDX = Cinfo[i].IDX;
		}
		CStatistic.perimeter.sum += Cinfo[i].perimeter;

		if (CStatistic.diameter.maxd < Cinfo[i].diameter)
		{
			CStatistic.diameter.maxd = Cinfo[i].diameter;
			CStatistic.diameter.maxIDX = Cinfo[i].IDX;
		}
		if (CStatistic.diameter.mind > Cinfo[i].diameter)
		{
			CStatistic.diameter.mind = Cinfo[i].diameter;
			CStatistic.diameter.minIDX = Cinfo[i].IDX;
		}
		CStatistic.diameter.sum += Cinfo[i].diameter;

		if (CStatistic.IOD.maxd < Cinfo[i].IOD)
		{
			CStatistic.IOD.maxd = Cinfo[i].IOD;
			CStatistic.IOD.maxIDX = Cinfo[i].IDX;
		}
		if (CStatistic.IOD.mind > Cinfo[i].IOD)
		{
			CStatistic.IOD.mind = Cinfo[i].IOD;
			CStatistic.IOD.minIDX = Cinfo[i].IDX;
		}
		CStatistic.IOD.sum += Cinfo[i].IOD;

		if (CStatistic.classify.maxd < Cinfo[i].classify)
		{
			CStatistic.classify.maxd = Cinfo[i].classify;
			CStatistic.classify.maxIDX = Cinfo[i].IDX;
		}
		if (CStatistic.classify.mind > Cinfo[i].classify)
		{
			CStatistic.classify.mind = Cinfo[i].classify;
			CStatistic.classify.minIDX = Cinfo[i].IDX;
		}
		CStatistic.classify.sum += Cinfo[i].classify;
	}
	CStatistic.area.range = CStatistic.area.maxd - CStatistic.area.mind;
	CStatistic.area.mean = CStatistic.area.sum / Cinfo.size();
	CStatistic.area.number = Cinfo.size();

	CStatistic.perimeter.range = CStatistic.perimeter.maxd - CStatistic.perimeter.mind;
	CStatistic.perimeter.mean = CStatistic.perimeter.sum / Cinfo.size();
	CStatistic.perimeter.number = Cinfo.size();

	CStatistic.diameter.range = CStatistic.diameter.maxd - CStatistic.diameter.mind;
	CStatistic.diameter.mean = CStatistic.diameter.sum / Cinfo.size();
	CStatistic.diameter.number = Cinfo.size();

	CStatistic.IOD.range = CStatistic.IOD.maxd - CStatistic.IOD.mind;
	CStatistic.IOD.mean = CStatistic.IOD.sum / Cinfo.size();
	CStatistic.IOD.number = Cinfo.size();

	CStatistic.classify.range = CStatistic.classify.maxd - CStatistic.classify.mind;
	CStatistic.classify.mean = CStatistic.classify.sum / Cinfo.size();
	CStatistic.classify.number = Cinfo.size();

	return CStatistic;
}