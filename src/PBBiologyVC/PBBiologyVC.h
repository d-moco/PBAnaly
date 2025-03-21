#pragma once
#include "PBLane.h"
using namespace System;
using namespace System::Collections::Generic;

namespace PBBiologyVC {

	public ref struct RectVC
	{
	public:
		int X;
		int Y;
		int Width;
		int Height;

		RectVC(int x, int y, int width, int height)
			: X(x), Y(y), Width(width), Height(height) {}
	};

	public ref struct MolecularInfoVC
	{
		float molecular_weight;     // 分子量
		int band_content;           // 条带含量
		float relative_content;     // 相对含量
		int IOD;                    // IOD
		float maxOD;                // 最大OD
		float percentum;            // 百分比
		int match;                  // 匹配
	};


	public ref struct _band_info
	{
		List<unsigned short>^ land_data;		//泳道数据（16bit）
		List<float>^ ydata;				//泳道波形y轴（0.0-255.0）
		List<float>^ xdata;				//泳道波形x轴（0.0-1.0）
		List<List<int>^>^ band_point;	//条带位置（顶峰、左括号、右括号）
		List<MolecularInfoVC^>^ Minfo;			//对应条带的分子计算结果

		_band_info() 
		{
			land_data = gcnew List<unsigned short>();
			ydata = gcnew  List<float>();
			xdata = gcnew List<float>();
			band_point = gcnew List<List<int>^>(3); 
			Minfo = gcnew List<MolecularInfoVC^>(1);
			
		
		}
	};


	public ref class PBBiology
	{
		
		~PBBiology();

	public:
		List<RectVC^>^ getProteinRectVC(System::Byte* mat, int bit, unsigned short width, unsigned short height, int ProteinRect_width, bool keep_width, int ProteinRect_height_ratio);
		List<_band_info^>^ getProteinBandsVC(System::Byte* mat, int bit, unsigned short width, unsigned short height, List<RectVC^>^ lanes);
		void addProteinRectVC(System::Byte* mat, int bit, unsigned short width, unsigned short height, int ProteinRect_width, int ProteinRect_height_ratio, List<RectVC^>^% proteinRect,int x, List<_band_info^>^% unadjustbands);
		_band_info^ get_protein_lane_dataVC(System::Byte* mat, int bit, unsigned short width, unsigned short height, RectVC^ lane);
		void addProteinBandVC(List<RectVC^>^% lanes, int lanesIndex, List<_band_info^>^% unadjustbands, int y);
		void deleteProteinBandVC(int lanesIndex, List<_band_info^>^% unadjustbands, int bandsIndex);
	private:
		void PBBiology::band_InfoTo_band_info(std::vector<BandInfo> src, List<_band_info^>^% results);
		void PBBiology::_band_infoToBand_Info(std::vector<BandInfo>& bandinfo, List<_band_info^>^ bands);
	
	};
}

