#pragma once



using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace PBBiologyVC {

	public ref struct Pseudo_infoVC
	{
	public:
		int maxOD;
		int minOD;
		int IOD;
		int Count;
		float AOD;

		Pseudo_infoVC(int _maxOD, int _minOD, int iOD, int count,float aod)
			: maxOD(_maxOD), minOD(_minOD), IOD(iOD), Count(count),AOD(aod) {}
	};

	public ref struct Point_VC
	{
		public:
			int x;
			int y;
			Point_VC(int _x, int _y)
				: x(_x), y(_y) {}
	};
	
	public ref class PBImageProcessVC
	{

		~PBImageProcessVC();

	public:
		
	/*	int render_mask_image_vc(System::Byte* mat, int bit,unsigned short width, unsigned short height, System::Byte* mark, System::Byte* dst, float max, float min, bool reverse);
		int blendImages_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, System::Byte* mark, System::Byte* dst,float alpha);
		int render_image_16_vc(System::Byte* mat, unsigned short width, unsigned short height, System::Byte* dst,
			int colorIndex, float max, float min, bool reverse, System::Byte* colobar, unsigned short colorbar_width, unsigned short colorbar_height,
			int h_onecolor);*/
		//PBBiologyVC::Pseudo_infoVC^ get_pseudo_info_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, int x, int y, int w, int h, float max, float min);

		/*int render_process(System::Byte* pseImage, System::Byte* markImage, System::Byte* renderpseImage, System::Byte* mergepseImage,
			System::Byte* colorBarImage,int colorIndex, unsigned short colorbarW, unsigned short colorbarH,
			int bit , unsigned short width, unsigned short height, float max, float min, bool reverse,
			unsigned short colorbarWW, unsigned short h_onecolor, int brightness_offset, double contrast_factor, double opacity_factor);

		int get_colorbar_VC(System::Byte* pseImage, int colorIndex, unsigned short colorbarW, unsigned short colorbarH, unsigned short colorbarWW, unsigned short h_onecolor, float max, float min, bool reverse);*/

		int render_process(System::Byte* pseImage, System::Byte* markImage, System::Byte* renderpseImage, System::Byte* mergepseImage,
			System::Byte* colorBarImage, int colorIndex, unsigned short colorbarW, unsigned short colorbarH,
			int bit, unsigned short width, unsigned short height,float sec, float max, float min,int scientific_flag, bool reverse,
			unsigned short colorbarWW, unsigned short h_onecolor, int brightness_offset, double contrast_factor, double opacity_factor);
		PBBiologyVC::Pseudo_infoVC^ get_pseudo_info_vc(System::Byte* mat, System::Byte* mask, int bit, unsigned short width, unsigned short height, float max, float min);
		PBBiologyVC::Pseudo_infoVC^ get_pseudo_info_rect_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, float max, float min,int x,int y,int w,int h);
		PBBiologyVC::Pseudo_infoVC^ get_pseudo_info_circle_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, float max, float min, int x, int y, int r);
		PBBiologyVC::Pseudo_infoVC^ get_pseudo_info_polygon_vc(System::Byte* mat, int bit, unsigned short width, unsigned short height, float max, float min, List<PBBiologyVC::Point_VC^>^ list_point);
		PBBiologyVC::Pseudo_infoVC^ get_pseudo_info_wand_vc(System::Byte* mat, System::Byte* dst, int bit, unsigned short width, unsigned short height, float max, float min, int x, int y, int th);
	private:
		
	};
}

