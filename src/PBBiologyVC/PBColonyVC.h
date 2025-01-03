#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace PBBiologyVC{
	
	public ref struct ColonyInfoVC
	{
	public:
		int IDX;            //�������
		int area;           //�������
		int perimeter;      //�����ܳ�
		float diameter;     //����ֱ��
		int IOD;            //����IOD
		int classify;       //�������

	};
	public enum dataClassVC
	{
		AREA = 0,       //���
		PERIMETER,      //�ܳ�
		DIAMETER,       //ֱ��
		IOD,            //IOD
	};
	public ref struct ClassifyStandardVC
	{
		List<float>^ interval;     //�������ݼ����־����
		float maxd;                 //�����������־
		float mind;                 //������С�����־
		int num;                    //��������
		dataClassVC classes;          //�������
	};
	public ref struct MinMaxInfoVC
	{
		float mind;         //��Сֵ
		float minIDX;       //��Сֵ��Ӧ���
		float maxd;         //���ֵ
		float maxIDX;       //���ֵ��Ӧ���
		float range;        //��Χ
		float mean;         //��ֵ
		float sum;          //��
		int number;         //����
	};
	

	public ref struct ColonyStatisticVC
	{
		MinMaxInfoVC area;        //���ͳ�ƽ��
		MinMaxInfoVC perimeter;   //�ܳ�ͳ�ƽ��
		MinMaxInfoVC diameter;    //ֱ��ͳ�ƽ��
		MinMaxInfoVC IOD;         //IODͳ�ƽ��
		MinMaxInfoVC classify;    //����ͳ�ƽ��
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