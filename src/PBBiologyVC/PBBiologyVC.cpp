#include "pch.h"
#include "PBBiologyVC.h"
#include <opencv2/opencv.hpp>
#include<vector>

namespace PBBiologyVC 
{
  
    PBBiology::~PBBiology()
    {

    }


    
    List<RectVC^>^ PBBiology::getProteinRectVC(unsigned char* mat, unsigned short width, unsigned short height)
    {
        cv::Mat src(height, width, CV_8UC1, mat);
        cv::Mat edges;
        cv::Canny(src, edges, 50, 150);

       
        std::vector<cv::Rect> rects = pblane->getProteinRect(src);

        List<RectVC^>^ results = gcnew List<RectVC^>();
        for (const auto& rect : rects) {
            results->Add(gcnew RectVC(rect.x, rect.y, rect.width, rect.height));
        }

        return results;
    }
    List<_band_info^>^ PBBiology::getProteinBandsVC(unsigned char* mat, unsigned short width, unsigned short height, List<RectVC^>^ lanes)
    {
        cv::Mat src(height, width, CV_8UC1, mat);
        std::vector<cv::Rect> rects(lanes->Count);

        for (size_t i = 0; i < lanes->Count; i++)
        {
            RectVC^ laneRect = lanes[i];
            rects[i] = cv::Rect(laneRect->X, laneRect->Y, laneRect->Width, laneRect->Height);
        }

        std::vector<BandInfo> bandinfo = pblane->getProteinBands(src, rects);

        List<_band_info^>^ results = gcnew List<_band_info^>();
        for (const auto& rect : bandinfo) {
            _band_info^ info = gcnew _band_info();
            for (const auto& land : rect.land_data) {
                info->land_data->Add(land);
            }
            for (const auto& y : rect.ydata) {
                info->ydata->Add(y);
            }

            for (const auto& x : rect.xdata) {
                info->xdata->Add(x);
            }
            for (const auto& point : rect.band_point) {
                List<int>^ points = gcnew List<int>();
                points->Add(point[0]); // ¶¥·å
                points->Add(point[1]); // ×óÀ¨ºÅ
                points->Add(point[2]); // ÓÒÀ¨ºÅ
                info->band_point->Add(points);
            }

            info->Minfo = gcnew List<MolecularInfoVC^>(rect.Minfo.size());
            for (int i = 0; i < rect.Minfo.size(); ++i) {
                MolecularInfoVC^ minfo = gcnew MolecularInfoVC();

                minfo->band_content = rect.Minfo[i].band_content;
                minfo->IOD = rect.Minfo[i].IOD;
                minfo->match = rect.Minfo[i].match;
                minfo->maxOD = rect.Minfo[i].maxOD;
                minfo->molecular_weight = rect.Minfo[i].molecular_weight;
                minfo->percentum = rect.Minfo[i].percentum;
                minfo->relative_content = rect.Minfo[i].relative_content;
                info->Minfo[i] = minfo;
            }

            results->Add(info);
        }
        return results;

    }

    void PBBiology::adjustBands(List<_band_info^>^ bands, int range)
    {
        std::vector<BandInfo> bandinfo(bands->Count);

        for (size_t i = 0; i < bands->Count; i++)
        {
            BandInfo _bandinfo;
            
            for each (unsigned short land in bands[i]->land_data)
            {
                _bandinfo.land_data.push_back(land);
            }

            
            for each (float y in bands[i]->ydata)
            {
                _bandinfo.ydata.push_back(y);
            }

            
            for each (float x in bands[i]->xdata)
            {
                _bandinfo.xdata.push_back(x);
            }

            for each (List<int> ^ points in bands[i]->band_point)
            {
                if (points->Count == 3) 
                {
                    std::array<int, 3> pointArray = { points[0], points[1], points[2] };
                    _bandinfo.band_point.push_back(pointArray);
                }
            }
            _bandinfo.Minfo.resize(bands[i]->Minfo->Count);
            for (int j = 0; j < bands[i]->Minfo->Count; j++)
            {
                MolecularInfo molecularInfo;
             
                molecularInfo.band_content = bands[i]->Minfo[j]->band_content;
                molecularInfo.IOD = bands[i]->Minfo[j]->IOD;
                molecularInfo.match = bands[i]->Minfo[j]->match;
                molecularInfo.maxOD = bands[i]->Minfo[j]->maxOD;
                molecularInfo.molecular_weight = bands[i]->Minfo[j]->molecular_weight;
                molecularInfo.percentum = bands[i]->Minfo[j]->percentum;
                molecularInfo.relative_content = bands[i]->Minfo[j]->relative_content;
                _bandinfo.Minfo[i] = molecularInfo;
           
            }

       
            bandinfo[i] = _bandinfo;
        }

        pblane->adjustBands(bandinfo, range);
    }

    void PBBiology::molecularWeightResult(List<RectVC^>^ lanes, List<_band_info^>^ bands)
    {
        std::vector<cv::Rect> rects(lanes->Count);

        for (size_t i = 0; i < lanes->Count; i++)
        {
            RectVC^ laneRect = lanes[i];
            rects[i] = cv::Rect(laneRect->X, laneRect->Y, laneRect->Width, laneRect->Height);
        }

        std::vector<BandInfo> bandinfo(bands->Count);

        for (size_t i = 0; i < bands->Count; i++)
        {
            BandInfo _bandinfo;

            for each (unsigned short land in bands[i]->land_data)
            {
                _bandinfo.land_data.push_back(land);
            }


            for each (float y in bands[i]->ydata)
            {
                _bandinfo.ydata.push_back(y);
            }


            for each (float x in bands[i]->xdata)
            {
                _bandinfo.xdata.push_back(x);
            }

            for each (List<int> ^ points in bands[i]->band_point)
            {
                if (points->Count == 3)
                {
                    std::array<int, 3> pointArray = { points[0], points[1], points[2] };
                    _bandinfo.band_point.push_back(pointArray);
                }
            }
            _bandinfo.Minfo.resize(bands[i]->Minfo->Count);
            for (int j = 0; j < bands[i]->Minfo->Count; j++)
            {
                MolecularInfo molecularInfo;

                molecularInfo.band_content = bands[i]->Minfo[j]->band_content;
                molecularInfo.IOD = bands[i]->Minfo[j]->IOD;
                molecularInfo.match = bands[i]->Minfo[j]->match;
                molecularInfo.maxOD = bands[i]->Minfo[j]->maxOD;
                molecularInfo.molecular_weight = bands[i]->Minfo[j]->molecular_weight;
                molecularInfo.percentum = bands[i]->Minfo[j]->percentum;
                molecularInfo.relative_content = bands[i]->Minfo[j]->relative_content;
                _bandinfo.Minfo[i] = molecularInfo;

            }


            bandinfo[i] = _bandinfo;
        }

        pblane->molecularWeightResult(rects, bandinfo);
    }

}

