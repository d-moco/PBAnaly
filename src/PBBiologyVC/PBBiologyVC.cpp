#include "pch.h"
#include "PBBiologyVC.h"
#include <opencv2/opencv.hpp>
#include<vector>


void printMatData(const cv::Mat& mat)
{
    Console::WriteLine("First few pixels:");
    for (int i = 0; i < std::min(10, mat.rows); ++i)
    {
        for (int j = 0; j < std::min(10, mat.cols); ++j)
        {
            if (mat.type() == CV_8UC1) {
                // 输出8位单通道图像的数据
                Console::Write("{0} ", mat.at<uchar>(i, j));
            }
            else if (mat.type() == CV_16UC1) {
                // 输出16位单通道图像的数据
                Console::Write("{0} ", mat.at<ushort>(i, j));
            }
        }
        Console::WriteLine();
    }
}
void showNormalizedImage(const cv::Mat& src)
{
    // 检查图像是否为16位单通道
    if (src.type() == CV_16UC1)
    {
        // 创建一个新的空Mat对象，用于存放转换后的图像
        cv::Mat normalizedImg;

        // 将16位图像归一化到8位范围
        double min, max;
        cv::minMaxLoc(src, &min, &max); // 计算图像的最小和最大值
        src.convertTo(normalizedImg, CV_8U, 255.0 / (max - min), -min * 255.0 / (max - min));

        // 显示图像
        cv::imshow("Normalized src", normalizedImg);
        cv::waitKey(0); // 等待按键
    }
    else
    {
        // 如果不是16位单通道，直接显示
        cv::imshow("src", src);
        cv::waitKey(0);
    }
}
namespace PBBiologyVC 
{
  
    PBBiology::~PBBiology()
    {

    }

    List<RectVC^>^ PBBiology::getProteinRectVC(System::Byte* mat,int bit, unsigned short width, unsigned short height, int ProteinRect_width, bool keep_width, int ProteinRect_height_ratio)
    {
        cv::Mat src;
        if (bit == 8) 
        {
            src = cv::Mat(height, width, CV_8UC1, mat);
        }
        else if (bit == 16)
        {
            src = cv::Mat(height, width, CV_16UC1, mat);
            cv::normalize(src, src, 0, 255, cv::NORM_MINMAX); // 归一化到 0-255
            src.convertTo(src, CV_8UC1);                 // 转换为 CV_8UC1
        }

        std::vector<cv::Rect> proteinRect = getProteinRect(src, &ProteinRect_width,keep_width, ProteinRect_height_ratio);
        List<RectVC^>^ results = gcnew List<RectVC^>();
        for (const auto& rect : proteinRect) {
            results->Add(gcnew RectVC(rect.x, rect.y, rect.width, rect.height));
        }
        return results;
    }

    List<_band_info^>^ PBBiology::getProteinBandsVC(System::Byte* mat, int bit, unsigned short width, unsigned short height, List<RectVC^>^ lanes)
    {
        if (bit != 16) return nullptr;
        cv::Mat src;
        if (bit == 16)
        {
            src = cv::Mat(height, width, CV_16UC1, mat);
        }
        std::vector<cv::Rect> rects(lanes->Count);
       
        for (size_t i = 0; i < lanes->Count; i++)
        {
            RectVC^ laneRect = lanes[i];
            rects[i] = cv::Rect(laneRect->X, laneRect->Y, laneRect->Width, laneRect->Height);
        }
        std::vector<BandInfo> bandinfo = getProteinBands(src, rects);

        List<_band_info^>^ results = gcnew List<_band_info^>();
        band_InfoTo_band_info(bandinfo, results);


        return results;
    }

    void PBBiology::addProteinRectVC(System::Byte* mat, int bit, unsigned short width, unsigned short height , int ProteinRect_width, int ProteinRect_height_ratio, List<RectVC^>^% proteinRect, int x, List<_band_info^>^% unadjustbands)
    {
        if (bit != 16) return ;
        cv::Mat src;
        if (bit == 16)
        {
            src = cv::Mat(height, width, CV_16UC1, mat);
        }
        std::vector<cv::Rect> rects(proteinRect->Count);
       
        for (size_t i = 0; i < proteinRect->Count; i++)
        {
            RectVC^ laneRect = proteinRect[i];
            rects[i] = cv::Rect(laneRect->X, laneRect->Y, laneRect->Width, laneRect->Height);
        }
        std::vector<BandInfo> bandinfo(unadjustbands->Count);

        _band_infoToBand_Info(bandinfo, unadjustbands);

         
        addProteinRect(rects,x,src, bandinfo,  ProteinRect_width,  ProteinRect_height_ratio);

        band_InfoTo_band_info(bandinfo, unadjustbands);

        proteinRect->Clear();
        for (const auto& rect : rects) {
            proteinRect->Add(gcnew RectVC(rect.x, rect.y, rect.width, rect.height));
        }

    }

    _band_info^ PBBiology::get_protein_lane_dataVC(System::Byte* mat, int bit, unsigned short width, unsigned short height, RectVC^ lane)
    {
        if (bit != 16) return nullptr;
        cv::Mat src;
        if (bit == 16)
        {
            src = cv::Mat(height, width, CV_16UC1, mat);
        }
        cv::Rect rect = cv::Rect(lane->X, lane->Y, lane->Width, lane->Height);
        BandInfo bandinfo = get_protein_lane_data(src,rect);
        _band_info^ info = gcnew _band_info();
        for (const auto& land : bandinfo.land_data) {
            info->land_data->Add(land);
        }
        for (const auto& y : bandinfo.ydata) {
            info->ydata->Add(y);
        }

        for (const auto& x : bandinfo.xdata) {
            info->xdata->Add(x);
        }

        for (const auto& point : bandinfo.band_point) {

            List<int>^ points = gcnew List<int>();
            points->Add(point[0]); // 顶峰
            points->Add(point[1]); // 左括号
            points->Add(point[2]); // 右括号
            info->band_point->Add(points);
        }
       
        info->Minfo = gcnew List<MolecularInfoVC^>();
        for (int i = 0; i < bandinfo.Minfo.size(); ++i) {
            MolecularInfoVC^ minfo = gcnew MolecularInfoVC();

            minfo->band_content = bandinfo.Minfo[i].band_content;
            minfo->IOD = bandinfo.Minfo[i].IOD;
            minfo->match = bandinfo.Minfo[i].match;
            minfo->maxOD = bandinfo.Minfo[i].maxOD;
            minfo->molecular_weight = bandinfo.Minfo[i].molecular_weight;
            minfo->percentum = bandinfo.Minfo[i].percentum;
            minfo->relative_content = bandinfo.Minfo[i].relative_content;
            info->Minfo->Add(minfo);
        }
        return info;
    }

    void PBBiology::addProteinBandVC(List<RectVC^>^% lanes, int lanesIndex, List<_band_info^>^% unadjustbands, int y)
    {
        std::vector<cv::Rect> rects(lanes->Count);

        for (size_t i = 0; i < lanes->Count; i++)
        {
            RectVC^ laneRect = lanes[i];
            rects[i] = cv::Rect(laneRect->X, laneRect->Y, laneRect->Width, laneRect->Height);
        }
        std::vector<BandInfo> bandinfo(unadjustbands->Count);

        _band_infoToBand_Info(bandinfo, unadjustbands);

        addProteinBand(rects,lanesIndex, bandinfo,y);

        band_InfoTo_band_info(bandinfo, unadjustbands);

        lanes->Clear();
        for (const auto& rect : rects) {
            lanes->Add(gcnew RectVC(rect.x, rect.y, rect.width, rect.height));
        }
    }

    void PBBiology::deleteProteinBandVC(int lanesIndex, List<_band_info^>^% unadjustbands, int bandsIndex)
    {
        std::vector<BandInfo> bandinfo(unadjustbands->Count);

        _band_infoToBand_Info(bandinfo, unadjustbands);

        deleteProteinBand(lanesIndex, bandinfo, bandsIndex);

        band_InfoTo_band_info(bandinfo, unadjustbands);
    }

    void PBBiology::band_InfoTo_band_info(std::vector<BandInfo> src, List<_band_info^>^% results)
    {
  
        results->Clear();
        for (const auto& rect : src) {
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
                points->Add(point[0]); // 顶峰
                points->Add(point[1]); // 左括号
                points->Add(point[2]); // 右括号
                info->band_point->Add(points);
            }
            std::cout << rect.Minfo.size()<< std::endl;
            info->Minfo = gcnew List<MolecularInfoVC^>();
            for (int i = 0; i < rect.Minfo.size(); ++i) {
                MolecularInfoVC^ minfo = gcnew MolecularInfoVC();

                minfo->band_content = rect.Minfo[i].band_content;
                minfo->IOD = rect.Minfo[i].IOD;
                minfo->match = rect.Minfo[i].match;
                minfo->maxOD = rect.Minfo[i].maxOD;
                minfo->molecular_weight = rect.Minfo[i].molecular_weight;
                minfo->percentum = rect.Minfo[i].percentum;
                minfo->relative_content = rect.Minfo[i].relative_content;
                info->Minfo->Add( minfo);
            }

            results->Add(info);
        }
     
    }

    void PBBiology::_band_infoToBand_Info(std::vector<BandInfo>& bandinfo, List<_band_info^>^ bands)
    {
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
    }
    
  //  List<RectVC^>^ PBBiology::getProteinRectVC(System::Byte* mat, unsigned short width, unsigned short height)
  //  {
  //     /* for (size_t i = 0; i < width*height; i++)
  //      {
  //          std::cout << mat[i] ;
  //      }
  //      std::cout << std::endl;*/
  //      
  //      cv::Mat src(height, width, CV_8UC1,mat);
  //      cv::Mat edges;
  //      cv::Canny(src, edges, 50, 150);
  //      std::vector<cv::Rect> rects = pblane->getProteinRect(src,(int*)width, (int*)height,1);

  //      List<RectVC^>^ results = gcnew List<RectVC^>();
  //      for (const auto& rect : rects) {
  //          results->Add(gcnew RectVC(rect.x, rect.y, rect.width, rect.height));
  //      }

  //      return results;
  //  }
  //  void PBBiology::getProteinBandsVC(System::Byte* mat, int bit, unsigned short width, unsigned short height, List<RectVC^>^ lanes, List<_band_info^>^% band)
  //  {
  //      cv::Mat src;
  //      if (bit == 8) 
  //      {
  //          src = cv::Mat(height, width, CV_8UC1, mat);
  //      }
  //      else if (bit == 16)
  //      {
  //          src = cv::Mat(height, width, CV_16UC1, mat);
  //      }
  //    
  //      std::vector<cv::Rect> rects(lanes->Count);
  //     
  //      for (size_t i = 0; i < lanes->Count; i++)
  //      {
  //          RectVC^ laneRect = lanes[i];
  //          rects[i] = cv::Rect(laneRect->X, laneRect->Y, laneRect->Width, laneRect->Height);
  //      }

  //      std::vector<BandInfo> bandinfo = pblane->getProteinBands(src, rects);
  // 
  //      
  //      band_InfoTo_band_info(bandinfo,band);
  //     
  //  }

  //  List<_band_info^>^ PBBiology::adjustBands(List<_band_info^>^ bands, int range)
  //  {
  //      std::vector<BandInfo> bandinfo(bands->Count);

  //      _band_infoToBand_Info(bandinfo,bands);
  //     
  //      pblane->adjustBands(bandinfo, range);
  //      band_InfoTo_band_info(bandinfo,bands);
  //      return bands;
  //  }

  //  void PBBiology::molecularWeightResult(List<RectVC^>^% lanes, List<_band_info^>^% bands)
  //  {
  //      std::vector<cv::Rect> rects(lanes->Count);

  //      for (size_t i = 0; i < lanes->Count; i++)
  //      {
  //          RectVC^ laneRect = lanes[i];
  //          rects[i] = cv::Rect(laneRect->X, laneRect->Y, laneRect->Width, laneRect->Height);
  //      }

  //      std::vector<BandInfo> bandinfo(bands->Count);
  //      _band_infoToBand_Info(bandinfo,bands);
  //      band_InfoTo_band_info(bandinfo,bands);

  //      pblane->molecularWeightResult(rects, bandinfo);
  ///*      for (int i = 0; i < bandinfo.size(); i++)
  //      {
  //          std::cout << bandinfo[i].Minfo.size() << std::endl;
  //          for (int j = 0; j < bandinfo[i].Minfo.size(); j++)
  //          {
  //              std::cout << bandinfo[i].Minfo[j].band_content;
  //          }
  //          std::cout << std::endl;
  //      }*/
  //      band_InfoTo_band_info(bandinfo, bands);
  //  }

  //  void PBBiology::_band_infoToBand_Info(std::vector<BandInfo>& bandinfo, List<_band_info^>^ bands)
  //  {
  //      
  //      for (size_t i = 0; i < bands->Count; i++)
  //      {
  //          BandInfo _bandinfo;

  //          for each (unsigned short land in bands[i]->land_data)
  //          {
  //              _bandinfo.land_data.push_back(land);
  //          }


  //          for each (float y in bands[i]->ydata)
  //          {
  //              _bandinfo.ydata.push_back(y);
  //          }


  //          for each (float x in bands[i]->xdata)
  //          {
  //              _bandinfo.xdata.push_back(x);
  //          }

  //          for each (List<int> ^ points in bands[i]->band_point)
  //          {
  //              if (points->Count == 3)
  //              {
  //                  std::array<int, 3> pointArray = { points[0], points[1], points[2] };
  //                  _bandinfo.band_point.push_back(pointArray);
  //              }
  //          }
  //          _bandinfo.Minfo.resize(bands[i]->Minfo->Count);
  //          for (int j = 0; j < bands[i]->Minfo->Count; j++)
  //          {
  //              MolecularInfo molecularInfo;

  //              molecularInfo.band_content = bands[i]->Minfo[j]->band_content;
  //              molecularInfo.IOD = bands[i]->Minfo[j]->IOD;
  //              molecularInfo.match = bands[i]->Minfo[j]->match;
  //              molecularInfo.maxOD = bands[i]->Minfo[j]->maxOD;
  //              molecularInfo.molecular_weight = bands[i]->Minfo[j]->molecular_weight;
  //              molecularInfo.percentum = bands[i]->Minfo[j]->percentum;
  //              molecularInfo.relative_content = bands[i]->Minfo[j]->relative_content;
  //              _bandinfo.Minfo[i] = molecularInfo;

  //          }


  //          bandinfo[i] = _bandinfo;
  //      }
  //  }

  //  void PBBiology::band_InfoTo_band_info(std::vector<BandInfo> src, List<_band_info^>^% results)
  //  {
  //   /*   for (int i = 0; i < src.size(); i++)
  //      {
  //         
  //          for (int j = 0; j < src[i].Minfo.size(); j++)
  //          {
  //              std::cout << src[i].Minfo[j].band_content;
  //          }
  //          std::cout << std::endl;
  //      }*/
  //      results->Clear();
  //      for (const auto& rect : src) {
  //          _band_info^ info = gcnew _band_info();
  //          for (const auto& land : rect.land_data) {
  //              info->land_data->Add(land);
  //          }
  //          for (const auto& y : rect.ydata) {
  //              info->ydata->Add(y);
  //          }

  //          for (const auto& x : rect.xdata) {
  //              info->xdata->Add(x);
  //          }

  //          for (const auto& point : rect.band_point) {
  //           
  //              List<int>^ points = gcnew List<int>();
  //              points->Add(point[0]); // 顶峰
  //              points->Add(point[1]); // 左括号
  //              points->Add(point[2]); // 右括号
  //              info->band_point->Add(points);
  //          }
  //          std::cout << rect.Minfo.size()<< std::endl;
  //          info->Minfo = gcnew List<MolecularInfoVC^>();
  //          for (int i = 0; i < rect.Minfo.size(); ++i) {
  //              MolecularInfoVC^ minfo = gcnew MolecularInfoVC();

  //              minfo->band_content = rect.Minfo[i].band_content;
  //              minfo->IOD = rect.Minfo[i].IOD;
  //              minfo->match = rect.Minfo[i].match;
  //              minfo->maxOD = rect.Minfo[i].maxOD;
  //              minfo->molecular_weight = rect.Minfo[i].molecular_weight;
  //              minfo->percentum = rect.Minfo[i].percentum;
  //              minfo->relative_content = rect.Minfo[i].relative_content;
  //              info->Minfo->Add( minfo);
  //          }

  //          results->Add(info);
  //      }
  //     
   // }

}

