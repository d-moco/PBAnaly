#include "../include/PBLane.h"


bool myCompare(const int& a, const int& b)
{
	return a < b;
}
bool myCompare2(const std::array<int, 4>& a, const std::array<int, 4>& b)
{
	return a[0] < b[0];
}

std::vector<int> processArray(std::vector<int>& groupsX, int minDifference, int maxDifference)
{
	std::vector<int> result;

	for (size_t i = 0; i < groupsX.size(); ++i) {
		if (i == groupsX.size() - 1) {
			result.push_back(groupsX[i]);
			break;
		}

		int diff = groupsX[i + 1] - groupsX[i];

		if (diff < minDifference) {
			groupsX[i + 1] = groupsX[i];
			continue;
		}
		else {
			result.push_back(groupsX[i]);
			if (diff > maxDifference) {
				int numInserts = (int)std::ceil(diff / static_cast<double>(maxDifference)) - 1;
				int step = diff / (numInserts + 1);

				for (int j = 1; j <= numInserts; ++j) {
					result.push_back(groupsX[i] + j * step);
				}
				for (size_t i = 1; i < result.size(); ++i) {
					if ((std::abs(result[i] - result[i - 1]) > maxDifference) || (std::abs(result[i] - result[i - 1]) < minDifference)) {
						result.erase(result.end() - numInserts, result.end());
					}
				}
			}
		}
	}
	return result;
}

vector<int> get_bar_num(vector<unsigned char> buf)
{
	vector<int> w;
	unsigned int y1_thresh = 4;
	unsigned int y1_min_thresh = 4;
	unsigned int width = 0;
	unsigned int last_edge = 0;
	unsigned int cur_edge = 0;
	int y1_sign = 0;
	int y0_0, y0_1, y0_2, y0_3;
	int y1_1, y2_1, y2_2;
	int y0[4];
	y0_0 = y0_1 = y0[0] = y0[1] = y0[2] = y0[3] = buf[0];
	for (int i = 0; i < buf.size(); i++)
	{
		y0_0 = y0_1 = y0[(i - 1) & 3];
		y0_0 += ((int)((buf[i] - y0_1) * EWMA_WEIGHT)) >> ZBAR_FIXED;
		y0[i & 3] = y0_0;
		y0_2 = y0[(i - 2) & 3];
		y0_3 = y0[(i - 3) & 3];
		y1_1 = y0_1 - y0_2;

		int abs_y1_1 = abs(y1_1);
		int y1_2 = y0_2 - y0_3;
		y2_1 = y0_0 - y0_1 - y1_1;
		y2_2 = y1_1 - y1_2;

		if ((abs_y1_1 < abs(y1_2)) && ((y1_1 >= 0) == (y1_2 >= 0)))
			y1_1 = y1_2;

		if (!y2_1 || ((y2_1 >= 0) ? y2_2 < 0 : y2_2 > 0))
		{
			unsigned temp_thresh;
			unsigned dx, thresh = y1_thresh;
			unsigned long t;

			if ((thresh <= y1_min_thresh) || !width) {
				temp_thresh = y1_min_thresh;
			}
			else {
				dx = (i << ZBAR_FIXED) - last_edge;
				t = thresh * dx;
				t /= width;
				t /= ZBAR_SCANNER_THRESH_FADE;
				int temp_t = thresh - t;
				if ((temp_t > 0) && (temp_t > y1_min_thresh)) {
					temp_thresh = temp_t;
				}
				else {
					y1_thresh = y1_min_thresh;
					temp_thresh = y1_min_thresh;
				}
			}
			if ((temp_thresh) <= abs_y1_1)
			{
				char y1_rev = (y1_sign > 0) ? y1_1 < 0 : y1_1 > 0;
				if (y1_rev) {
					if (!y1_sign)
						last_edge = cur_edge = (1 << ZBAR_FIXED) + ROUND;
					else if (!last_edge)
						last_edge = cur_edge;
					width = cur_edge - last_edge;
					last_edge = cur_edge;
					// printf("i=%d,width=%d\n",i,width);
					w.push_back(i);
				}
				if (y1_rev || (abs(y1_sign) < abs_y1_1)) {
					int d;
					y1_sign = y1_1;
					y1_thresh = (abs_y1_1 * THRESH_INIT + ROUND) >> ZBAR_FIXED;
					if (y1_thresh < y1_min_thresh)
						y1_thresh = y1_min_thresh;

					d = y2_1 - y2_2;
					cur_edge = 1 << ZBAR_FIXED;
					if (!d) {
						cur_edge >>= 1;
					}
					else if (y2_1) {
						cur_edge -= ((y2_1 << ZBAR_FIXED) + 1) / d;
					}
					cur_edge += i << ZBAR_FIXED;
				}
			}
		}
	}
	return w;
}


std::vector<int> checkArray(std::vector<int>& processX, Mat src, int start_y, int end_y, int meanW)
{
	std::vector<int> result;
	std::vector<int> diffX(processX.size() - 1);
	for (int i = 1; i < processX.size(); i++)
	{
		diffX[i - 1] = processX[i] - processX[i - 1];
	}
	std::sort(diffX.begin(), diffX.end(), myCompare);
	int meanDiffX = 0;
	int cnt = 0;
	for (int i = diffX.size() / 3; i <= 2 * diffX.size() / 3; i++)
	{
		meanDiffX += diffX[i];
		cnt++;
	}
	meanDiffX /= cnt;

	int ret = 0;
	int len_H = end_y - start_y;
	vector<int> w1;
	vector<int> w2;
	vector<unsigned char> rowPixels1(len_H);
	vector<unsigned char> rowPixels2(len_H);
	cnt = 0;
	while (1)
	{
		cnt++;
		int dealX = processX[0] - (meanDiffX * cnt);
		if (dealX < meanDiffX / 2) { break; }

		for (int col = 0; col < len_H; col++) {
			rowPixels1[col] = src.at<uchar>(start_y + col, dealX - meanW / 4);
			rowPixels2[col] = src.at<uchar>(start_y + col, dealX + meanW / 4);
		}
		w1 = get_bar_num(rowPixels1);
		w2 = get_bar_num(rowPixels2);
		ret = 0;
		if (w1.size() && w2.size())
		{
			for (int i = 0; i < w1.size(); i++)
			{
				for (int j = 0; j < w2.size(); j++)
				{
					if (abs(w1[i] - w2[j]) <= 2) {
						ret = 1;
						break;
					}
				}
			}
		}
		if (ret) {
			result.push_back(dealX);
		}
		else {
			break;
		}
	}

	for (int i = 0; i < processX.size(); i++)
	{
		for (int col = 0; col < len_H; col++) {
			rowPixels1[col] = src.at<uchar>(start_y + col, processX[i] - meanW / 4);
			rowPixels2[col] = src.at<uchar>(start_y + col, processX[i] + meanW / 4);
		}
		w1 = get_bar_num(rowPixels1);
		w2 = get_bar_num(rowPixels2);
		ret = 0;
		if (w1.size() && w2.size())
		{
			for (int i = 0; i < w1.size(); i++)
			{
				for (int j = 0; j < w2.size(); j++)
				{
					if (abs(w1[i] - w2[j]) <= 2) {
						ret = 1;
						break;
					}
				}
			}
		}
		if (ret) {
			result.push_back(processX[i]);
		}
	}

	cnt = 0;
	while (1)
	{
		cnt++;
		int dealX = processX[processX.size() - 1] + (meanDiffX * cnt);
		if (dealX > (src.cols - meanDiffX / 2)) { break; }

		for (int col = 0; col < len_H; col++) {
			rowPixels1[col] = src.at<uchar>(start_y + col, dealX - meanW / 4);
			rowPixels2[col] = src.at<uchar>(start_y + col, dealX + meanW / 4);
		}
		w1 = get_bar_num(rowPixels1);
		w2 = get_bar_num(rowPixels2);
		ret = 0;
		if (w1.size() && w1.size() == w2.size())
		{
			for (int i = 0; i < w1.size(); i++)
			{
				if (abs(w1[i] - w2[i]) > 2) {
					ret = 0;
					break;
				}
			}
			ret = 1;
		}
		if (ret) {
			result.push_back(dealX);
		}
		else {
			break;
		}
	}

	std::sort(result.begin(), result.end(), myCompare);
	return result;
}

vector<std::array<int, 3>> get_top_point(vector<unsigned short> rbuf, int range)
{
	vector<std::array<int, 2>> point;
	vector<char> diff(rbuf.size());
	vector<std::array<int, 3>> topPoint;

	int maxValue = *max_element(rbuf.begin(), rbuf.end());
	int minValue = *min_element(rbuf.begin(), rbuf.end());
	if(maxValue - minValue < 65535/10)
	{
		return topPoint;
	}
	int limit = (maxValue - minValue) / 10;
	if(limit < 65535/40)
	{
		limit = 65535/40;
	}

	for (int i = 0; i < rbuf.size() - 1; i++)
	{
		if (rbuf[i + 1] - rbuf[i] > 0)
			diff[i] = 1;
		else if (rbuf[i + 1] - rbuf[i] < 0)
			diff[i] = -1;
		else
			diff[i] = 0;
	}
	for (int i = rbuf.size() - 1; i >= 0; i--)
	{
		if (i == rbuf.size() - 1 && diff[i] == 0)
		{
			diff[i] = 1;
		}
		else if (diff[i] == 0)
		{
			if (diff[i + 1] >= 0)
				diff[i] = 1;
			else
				diff[i] = -1;
		}
	}
	for (int i = 0; i != rbuf.size() - 1; i++)
	{
		diff[i] = diff[i + 1] - diff[i];
	}

	unsigned char flag = 0;
	for (int i = 0; i != rbuf.size() - 1; i++)
	{
		if (flag == 0 && diff[i] == 2)//bottom
		{
			point.push_back({ i + 1, 0 });
			flag = 1;
		}
		else if (flag == 1 && diff[i] == -2)//top
		{
			point.push_back({ i + 1, 1 });
			flag = 0;
		}
	}

	for (int i = 0; i < point.size(); i++)
	{
		for (int j = 0; j < point.size(); j++)
		{
			if (point[i][1] == 1 && point[j][1] == 1)
			{
				if (point[i][0] > point[j][0])
				{
					if (point[i][0] - point[j][0] <= range && rbuf[point[i][0]] < rbuf[point[j][0]])
					{
						point[i][1] = 255;
						break;
					}
				}
				else if (point[i][0] < point[j][0])
				{
					if (point[j][0] - point[i][0] <= range)
					{
						if (rbuf[point[i][0]] < rbuf[point[j][0]])
						{
							point[i][1] = 255;
							break;
						}
					}
					else
					{
						break;
					}
				}
			}
			else if (point[i][1] == 0 && point[j][1] == 0)
			{
				if (point[i][0] > point[j][0])
				{
					if (point[i][0] - point[j][0] <= range && rbuf[point[i][0]] > rbuf[point[j][0]])
					{
						point[i][1] = 255;
						break;
					}
				}
				else if (point[i][0] < point[j][0])
				{
					if (point[j][0] - point[i][0] <= range)
					{
						if (rbuf[point[i][0]] > rbuf[point[j][0]])
						{
							point[i][1] = 255;
							break;
						}
					}
					else
					{
						break;
					}
				}
			}
		}
	}

	vector<int> filterPoint;
	flag = 0;
	int temp = 0;
	for (int i = 0; i < point.size(); i++)
	{
		if (flag == 1)//top
		{
			if (point[i][1] == 1)
			{
				if (rbuf[point[temp][0]] <= rbuf[point[i][0]])
				{
					temp = i;
				}
			}
			else if (point[i][1] == 0)
			{
				filterPoint.push_back(point[temp][0]);
				temp = i;
				flag = 0;
			}
		}
		else if (flag == 0)//bottom
		{
			if (point[i][1] == 0)
			{
				if (rbuf[point[temp][0]] >= rbuf[point[i][0]])
				{
					temp = i;
				}
			}
			else if (point[i][1] == 1)
			{
				filterPoint.push_back(point[temp][0]);
				temp = i;
				flag = 1;
			}
		}
	}
	filterPoint.push_back(point[temp][0]);

	
	if (filterPoint.size() < 3)
	{
		return topPoint;
	}

	for (int i = 1; i < filterPoint.size() - 1; i += 1)
	{
		if (rbuf[filterPoint[i]] - rbuf[filterPoint[i - 1]] > limit && rbuf[filterPoint[i]] - rbuf[filterPoint[i + 1]] > limit)
		{
			int len_left = filterPoint[i] - filterPoint[i - 1];
			int len_right = filterPoint[i + 1] - filterPoint[i];
			if (len_left > len_right && len_left / len_right >= 3)
			{
				filterPoint[i - 1] = filterPoint[i] - len_right * 3;
			}
			else if (len_right > len_left && len_right / len_left >= 3)
			{
				filterPoint[i + 1] = filterPoint[i] + len_left * 3;
			}

			topPoint.push_back({ filterPoint[i],filterPoint[i - 1],filterPoint[i + 1] });
		}
	}
	return topPoint;
}




std::vector<cv::Rect> getProteinRect(Mat src,int* ProteinRect_width,bool keep_width,int ProteinRect_height_ratio)
{
	cv::Mat edges;
	Canny(src, edges, 50, 150);

	std::vector<std::vector<cv::Point>> contours;
	findContours(edges, contours, cv::RETR_LIST, cv::CHAIN_APPROX_SIMPLE);

	std::vector<cv::Rect> boundingRects;
	for (const auto& contour : contours) {
		cv::Rect rect = cv::boundingRect(contour);
		double area = rect.area(); 
		if (area < 20 || area > 700) {
			continue; 
		}
		double rotia = 0.0;
		rotia = (double)rect.width/rect.height;
		if (rotia >= 7 || rotia <= 2) {
			continue; 
		}
		boundingRects.push_back(rect);
	}

	std::vector<int> allX(boundingRects.size());
	std::vector<int> allY(boundingRects.size());                        
	std::vector<int> allW(boundingRects.size());
	int up = src.rows;
	int down = 0;
	for (int i = 0;i<boundingRects.size();i++) 
	{
		allX[i] = boundingRects[i].x + boundingRects[i].width/2;
		allY[i] = boundingRects[i].y + boundingRects[i].height/2;
		allW[i] = boundingRects[i].width;
		up = min(boundingRects[i].y,up);
		down = max(boundingRects[i].y + boundingRects[i].height,down);
	}
	int meanW = 0;
	int meanY = 0;
	int maxH = 0;
	std::sort(allW.begin(), allW.end(), [](const int& a, const int& b) {
		return a < b;
	});
	std::sort(allY.begin(), allY.end(), [](const int& a, const int& b) {
		return a < b;
	});
	std::sort(allX.begin(), allX.end(), [](const int& a, const int& b) {
		return a < b;
	});
	int cnt = 0;
	for(int i = boundingRects.size()/3;i <= 2*boundingRects.size()/3;i++)
	{
		cnt++;
		meanW += allW[i];
		meanY += allY[i];
	}
	meanW /= cnt;
	meanY /= cnt;
	maxH = max(down - meanY, meanY - up) * 2;
	maxH = max(maxH,src.rows/3);
	std::vector<int> groupsX;
	const int n = 3;
	int sumX = allX[0];
	cnt = 1;
	for (int i = 1; i < boundingRects.size(); i++) {
		if (allX[i]  - allX[i-1] > n)
		{
			groupsX.push_back(sumX/cnt);
			sumX = 0;
			cnt = 0;
		}
		sumX += allX[i];
		cnt++;
	}
	groupsX.push_back(sumX/cnt);

	std::vector<int> processX = processArray(groupsX,meanW,meanW*1.8);
	std::vector<int> proteinX = checkArray(processX, src,15,src.rows - 30,meanW);
	std::vector<cv::Rect> proteinRect(proteinX.size());
	for(int i = 0;i<proteinX.size();i++)
	{
		if(keep_width == 1){
			meanW = *ProteinRect_width;
		}
		else{
			*ProteinRect_width = meanW;
		}
		int y_start = src.rows * (100 - ProteinRect_height_ratio) / 200;
		proteinRect[i].x = proteinX[i] - meanW/2;
		proteinRect[i].y = y_start;
		proteinRect[i].width = meanW;
		proteinRect[i].height = src.rows - y_start *  2;
	}
	return proteinRect;
}
void addProteinRect(std::vector<cv::Rect>& proteinRect,int x,Mat src,std::vector<BandInfo>& unadjustbands,int ProteinRect_width,int ProteinRect_height_ratio)
{
	Rect new_proteinRect;
	if(proteinRect.size() > 0)
	{
		new_proteinRect.x = x - proteinRect[0].width/2;
		new_proteinRect.y = proteinRect[0].y;
		new_proteinRect.width = proteinRect[0].width;
		new_proteinRect.height = proteinRect[0].height;
	}
	else
	{
		int y_start = src.rows * (100 - ProteinRect_height_ratio) / 200;
		new_proteinRect.x = x - ProteinRect_width/2;
		new_proteinRect.y = y_start;
		new_proteinRect.width = ProteinRect_width;
		new_proteinRect.height = src.rows - y_start *  2;
	}
	
	BandInfo band = get_protein_lane_data(src,new_proteinRect);

    for (auto it = proteinRect.begin(); it != proteinRect.end(); ++it) {
        if (it->x > new_proteinRect.x) {
			size_t index = std::distance(proteinRect.begin(), it);
            proteinRect.insert(it, new_proteinRect);
			unadjustbands.insert(unadjustbands.begin() + index, band);
            return;
        }
    }
	proteinRect.push_back(new_proteinRect);
	unadjustbands.push_back(band);
	return;
}

void deleteProteinRect(std::vector<cv::Rect>& proteinRect,int idx,std::vector<BandInfo>& unadjustbands)
{
	if (idx < proteinRect.size()) {
        proteinRect.erase(proteinRect.begin() + idx);
        unadjustbands.erase(unadjustbands.begin() + idx);
    } else {
        std::cout << "idx out of range!" << std::endl;
    }
	return;
}

BandInfo get_protein_lane_data(Mat src, Rect lane)
{
	BandInfo band;
	int sum = 0;
	vector<unsigned short> cdata;
	for (int i = lane.y; i < lane.y + lane.height; i++)
	{
		sum = 0;
		for (int j = lane.x; j < lane.x + lane.width; j++)
		{
			sum += src.at<unsigned short>(i, j);
		}
		sum /= lane.width;
		band.land_data.push_back((unsigned short)sum);
		band.ydata.push_back((float)((65535 - sum) / 255.0));
		cdata.push_back(65535 - sum);
	}
	for (int i = 0; i < lane.height; i++)
	{
		band.xdata.push_back((float)i / lane.height);
	}
	band.band_point = get_top_point(cdata, 8);
	return band;
}

std::vector<BandInfo> getProteinBands(Mat src, std::vector<cv::Rect> lanes)
{
	std::vector<BandInfo> bands;
	if (src.type() != CV_16UC1)
	{
		std::cout << "The image is not 16-bit single-channel as expected." << std::endl;
		return bands;
	}
	int lanes_num = lanes.size();
	if (lanes_num <= 0)
	{
		std::cout << "The lanes_num is 0." << std::endl;
		return bands;
	}
	bands.resize(lanes_num);

	for (int i = 0; i < lanes_num; i++)
	{
		bands[i] = get_protein_lane_data(src, lanes[i]);
	}
	//adjustBands(bands, 10);
	return bands;
}

void modifyProteinRectAndBands(Mat src,std::vector<cv::Rect>& proteinRect,int ProteinRect_width,int ProteinRect_height_ratio,std::vector<BandInfo>& unadjustbands)
{	
	int y_start = src.rows * (100 - ProteinRect_height_ratio) / 200;
	int det_y = proteinRect[0].y - y_start;
	for(int i = 0;i<proteinRect.size();i++)
	{
		proteinRect[i].x = proteinRect[i].x - (ProteinRect_width - proteinRect[i].width)/2;
		proteinRect[i].y = y_start;
		proteinRect[i].width = ProteinRect_width;
		proteinRect[i].height = src.rows - y_start *  2;
	}
	int sum = 0;
	for(int n = 0; n < proteinRect.size(); n++)
	{
		unadjustbands[n].land_data.clear();
		unadjustbands[n].ydata.clear();
		unadjustbands[n].xdata.clear();
		for(int i = proteinRect[n].y; i < proteinRect[n].y + proteinRect[n].height; i++)
		{
			sum = 0;
			for(int j = proteinRect[n].x; j < proteinRect[n].x + proteinRect[n].width; j++)
			{
				sum+=src.at<unsigned short>(i,j);
			}
			sum/=proteinRect[n].width;
			unadjustbands[n].land_data.push_back((unsigned short)sum);
			unadjustbands[n].ydata.push_back((float)((65535 - sum)/255.0));
		}
		for(int i = 0; i < proteinRect[n].height; i++)
		{
			unadjustbands[n].xdata.push_back((float)i/proteinRect[n].height);
		}

		for(int i = 0; i < unadjustbands[n].band_point.size(); i++)
		{
			//fix left and right
			unadjustbands[n].band_point[i][1] += det_y;
			unadjustbands[n].band_point[i][2] += det_y;
			//fix top
			int y1 = unadjustbands[n].band_point[i][1];
			int y2 = unadjustbands[n].band_point[i][2];
			float temp = unadjustbands[n].ydata[y1];
			for(int j = y1 + 1; j < y2; j++)
			{
				if(unadjustbands[n].ydata[j] > temp){
					temp = unadjustbands[n].ydata[j];
					unadjustbands[n].band_point[i][0] = j;
				}
			}
		}
	}
}

void getLaneBandsIndex(std::vector<cv::Rect> lanes,std::vector<BandInfo> bands,int x,int y,int* lanesIndex,int* bandsIndex)
{
	*lanesIndex = -1;
	*bandsIndex = -1;
	int ystart = lanes[0].y;
	int yend = lanes[0].y + lanes[0].height - 1;
	if(y > yend || y < ystart)
	{
		return ;
	}
	for (int i = 0; i < lanes.size(); i++) {
		int xstart = lanes[i].x;
		int xend = lanes[i].x + lanes[i].width - 1;
        if(x <= xend && x >= xstart){
            *lanesIndex = i;
            break;
        }
    }
	for (int j = 0; j < bands[*lanesIndex].band_point.size(); j++) {
		int y1 = bands[*lanesIndex].band_point[j][1] + ystart;
		int y2 = bands[*lanesIndex].band_point[j][2] + ystart;
        if(y <= y2 && y >= y1){
            *bandsIndex = j;
            break;
        }
    }
	return;
}

void addProteinBand(std::vector<cv::Rect> lanes,int lanesIndex,std::vector<BandInfo>& unadjustbands,int y)
{
	int ystart = lanes[lanesIndex].y;
	int yend = lanes[lanesIndex].y + lanes[lanesIndex].height - 1;
	int y1 = 0;
	int y2 = lanes[lanesIndex].height;
	if(y < ystart || y > yend)
	{
		return ;
	}
	int rect_y = y - lanes[lanesIndex].y;
	for(int i = 0; i < unadjustbands[lanesIndex].band_point.size(); i++)
	{
		y2 = unadjustbands[lanesIndex].band_point[i][1];
		if(rect_y >= y1 && rect_y < y2)
		{
			break;
		}
		else
		{
			y1 = unadjustbands[lanesIndex].band_point[i][2];
		}
		
	}
	if(y1 > y2)
	{
		y2 = lanes[lanesIndex].height;
	}

	//fine top
	int range = 8;
	int top_start = rect_y - range;
	int top_end = rect_y + range;
	if(top_start < y1){
		top_start = y1;
	}
	if(top_end > y2){
		top_end = y2;
	}
	std::array<int, 3> new_band_point;
	new_band_point[0] = top_start;
	float temp = unadjustbands[lanesIndex].ydata[top_start];
	float mean = unadjustbands[lanesIndex].ydata[top_start];
	for(int i = top_start + 1; i < top_end; i++)
	{
		mean += unadjustbands[lanesIndex].ydata[i];
		if(unadjustbands[lanesIndex].ydata[i] > temp){
			temp = unadjustbands[lanesIndex].ydata[i];
			new_band_point[0] = i;
		}
	}
	mean /= (top_end - top_start);

	//fine left
	temp = unadjustbands[lanesIndex].ydata[new_band_point[0]];
	new_band_point[1] = y1;
	for(int i = new_band_point[0] - 1; i > y1; i--)
	{
		if(unadjustbands[lanesIndex].ydata[i] > temp)
		{
			if(temp < mean)
			{
				new_band_point[1] = i+1;
				break;
			}
		}
		temp = unadjustbands[lanesIndex].ydata[i];
	}
	//fine right
	temp = unadjustbands[lanesIndex].ydata[new_band_point[0]];
	new_band_point[2] = y2;
	for(int i = new_band_point[0] + 1; i < y2; i++)
	{
		if(unadjustbands[lanesIndex].ydata[i] > temp)
		{
			if(temp < mean)
			{
				new_band_point[2] = i+1;
				break;
			}
		}
		temp = unadjustbands[lanesIndex].ydata[i];
	}

	for (auto it = unadjustbands[lanesIndex].band_point.begin(); it != unadjustbands[lanesIndex].band_point.end(); ++it) {
        if ((*it)[0] > new_band_point[0]) {
            unadjustbands[lanesIndex].band_point.insert(it, new_band_point);
            return;
        }
    }
    unadjustbands[lanesIndex].band_point.push_back(new_band_point);
	return ;
}

void deleteProteinBand(int lanesIndex,std::vector<BandInfo>& unadjustbands,int bandsIndex)
{
	if (bandsIndex < unadjustbands[lanesIndex].band_point.size()) {
        unadjustbands[lanesIndex].band_point.erase(unadjustbands[lanesIndex].band_point.begin() + bandsIndex);
    } else {
        std::cout << "bandsIndex out of range!" << std::endl;
    }
	return;
}


std::vector<BandInfo> adjustBands(std::vector<BandInfo> unadjustbands, int range)
{
    // 用于存储所有的值及其对应的列索引
    std::map<int, int> columnMap;
	std::vector<BandInfo> bands = unadjustbands;

	std::vector<std::array<int, 4>> vec;
	for (int i = 0; i < bands.size(); i++)
	{
		for (int j = 0; j < bands[i].band_point.size(); j++)
		{
			std::array<int, 4> bp = { bands[i].band_point[j][0],bands[i].band_point[j][1],bands[i].band_point[j][2],i };
			vec.push_back(bp);
		}
		bands[i].band_point.clear();
	}
	std::sort(vec.begin(), vec.end(), myCompare2);

	vector<int> point;
	point.push_back(0);
	vector<int> onep;
	onep.push_back(vec[0][3]);

	for (int i = 1; i < vec.size(); i++)
	{
		if (std::find(onep.begin(), onep.end(), vec[i][3]) != onep.end())
		{
			point.push_back(i);
			onep.clear();
			onep.push_back(vec[i][3]);
		}
		else
		{
			if (std::abs(vec[i][0] - vec[point.back()][0]) > range)
			{
				point.push_back(i);
				onep.clear();
				onep.push_back(vec[i][3]);
			}
		}
	}

	for (int i = 0; i < bands.size(); i++)
	{
		bands[i].band_point.resize(point.size(), { -1,-1,-1 });
	}

	int columnIndex = 1;
	point.push_back(-1);
	bands[vec[0][3]].band_point[0] = { vec[0][0],vec[0][1],vec[0][2] };
	for (int i = 1; i < vec.size(); i++)
	{
		if (i != point[columnIndex])
		{
			bands[vec[i][3]].band_point[columnIndex - 1] = { vec[i][0],vec[i][1],vec[i][2] };
		}
		else
		{
			columnIndex++;
			bands[vec[i][3]].band_point[columnIndex - 1] = { vec[i][0],vec[i][1],vec[i][2] };
		}
	}
	return bands;
}

void molecularWeightResult(std::vector<cv::Rect> lanes, std::vector<BandInfo>& bands)
{
	for (int i = 0; i < bands.size(); i++)
	{
		int rNum = bands[i].band_point.size();
		bands[i].Minfo.resize(rNum);

		unsigned long land_sum = lanes[i].width * std::accumulate(bands[i].ydata.begin(), bands[i].ydata.end(), 0UL);
		unsigned long band_sum = 0;
		for (int j = 0; j < rNum; j++)
		{
			std::array<int, 3> point = bands[i].band_point[j];
			if (point[0] != -1)
			{
				bands[i].Minfo[j].molecular_weight = bands[i].xdata[point[0]];
				bands[i].Minfo[j].band_content = 0;
				for (int s = point[1]; s < point[2]; s++)
				{
					bands[i].Minfo[j].band_content += bands[i].ydata[s] * lanes[i].width;
				}
				bands[i].Minfo[j].IOD = bands[i].Minfo[j].band_content;
				band_sum += bands[i].Minfo[j].band_content;
				bands[i].Minfo[j].maxOD = bands[i].ydata[point[0]];
			}
			else
			{
				bands[i].Minfo[j].molecular_weight = -1;
				bands[i].Minfo[j].band_content = -1;
				bands[i].Minfo[j].IOD = -1;
				bands[i].Minfo[j].maxOD = -1;
			}
		}
		for (int j = 0; j < rNum; j++)
		{
			std::array<int, 3> point = bands[i].band_point[j];
			if (point[0] != -1)
			{
				bands[i].Minfo[j].relative_content = 100.0 * bands[i].Minfo[j].band_content / band_sum;
				bands[i].Minfo[j].percentum = 100.0 * bands[i].Minfo[j].IOD / land_sum;

				if (bands[0].band_point[j][0] != -1)
				{
					bands[i].Minfo[j].match = 1;
				}
				else
				{
					bands[i].Minfo[j].match = -1;
				}
			}
			else
			{
				bands[i].Minfo[j].relative_content = -1;
				bands[i].Minfo[j].percentum = -1;

				if (bands[0].band_point[j][0] != -1)
				{
					bands[i].Minfo[j].match = 0;
				}
				else
				{
					bands[i].Minfo[j].match = -255;
				}
			}
		}
	}
	return;
}