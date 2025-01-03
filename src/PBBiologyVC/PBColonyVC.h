#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace PBBiologyVC{
	
	public ref struct ColonyInfoVC
	{
	public:
		int IDX;            //菌落序号
		int area;           //菌落面积
		int perimeter;      //菌落周长
		float diameter;     //菌落直径
		int IOD;            //菌落IOD
		int classify;       //菌落分类

	};
	public enum dataClassVC
	{
		AREA = 0,       //面积
		PERIMETER,      //周长
		DIAMETER,       //直径
		IOD,            //IOD
	};
	public ref struct ClassifyStandardVC
	{
		List<float>^ interval;     //分类依据间隔标志数据
		float maxd;                 //分类最大间隔标志
		float mind;                 //分类最小间隔标志
		int num;                    //分类间隔数
		dataClassVC classes;          //分类类别
	};
	public ref struct MinMaxInfoVC
	{
		float mind;         //最小值
		float minIDX;       //最小值对应序号
		float maxd;         //最大值
		float maxIDX;       //最大值对应序号
		float range;        //范围
		float mean;         //均值
		float sum;          //和
		int number;         //数量
	};
	

	public ref struct ColonyStatisticVC
	{
		MinMaxInfoVC area;        //面积统计结果
		MinMaxInfoVC perimeter;   //周长统计结果
		MinMaxInfoVC diameter;    //直径统计结果
		MinMaxInfoVC IOD;         //IOD统计结果
		MinMaxInfoVC classify;    //分类统计结果
	};
	public ref class PBColonyVC
	{
		~PBColonyVC();
	public:
		void run(System::Byte* image, int bit, unsigned short width, unsigned short height, int lower, int upper);
	};

	
	
	//public ref class PBColonyVC
	//{
	//	//PBColonyVC();

	//	//~PBColonyVC();

	//public:
	//	//void run(System::Byte* image, int bit, unsigned short width, unsigned short height, int lower , int upper );

	//private:
	//	//PBColony* pbcolony;
	//};
}