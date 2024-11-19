#pragma once
#include <vector>


using namespace std;

#define ZBAR_FIXED  	5
#define ROUND (1 << (ZBAR_FIXED - 1))
#define ZBAR_SCANNER_THRESH_FADE 	8
#define EWMA_WEIGHT ((unsigned)((.78  * (1 << (ZBAR_FIXED + 1)) + 1) / 2))
#define THRESH_INIT ((unsigned)((.44 * (1 << (ZBAR_FIXED + 1)) + 1) / 2))


struct MolecularInfo
{
    float molecular_weight;     // 分子量
    int band_content;           // 条带含量
    float relative_content;     // 相对含量
    int IOD;                    // IOD
    float maxOD;                // 最大OD
    float percentum;            // 百分比
    int match;                  // 匹配
};

struct BandInfo
{
    std::vector<unsigned short> land_data;        // 泳道数据（16bit）
    std::vector<float> ydata;                     // 泳道波形y轴（0.0-255.0）
    std::vector<float> xdata;                     // 泳道波形x轴（0.0-1.0）
    std::vector<std::array<int, 3>> band_point;   // 条带位置（顶峰、左括号、右括号）
    std::vector<MolecularInfo> Minfo;          // 对应条带的分子计算结果
};




enum dataClass
{
    AREA = 0,       //面积
    PERIMETER,      //周长
    DIAMETER,       //直径
    IOD,            //IOD
};

struct ClassifyStandard
{
    vector<float> interval;     //分类依据间隔标志数据
    float maxd;                 //分类最大间隔标志
    float mind;                 //分类最小间隔标志
    int num;                    //分类间隔数
    dataClass classes;          //分类类别
};

struct ColonyInfo
{
    int IDX;            //菌落序号
    int area;           //菌落面积
    int perimeter;      //菌落周长
    float diameter;     //菌落直径
    int IOD;            //菌落IOD
    int classify;       //菌落分类
};

struct MinMaxInfo
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

struct ColonyStatistic
{
    MinMaxInfo area;        //面积统计结果
    MinMaxInfo perimeter;   //周长统计结果
    MinMaxInfo diameter;    //直径统计结果
    MinMaxInfo IOD;         //IOD统计结果
    MinMaxInfo classify;    //分类统计结果
};

typedef enum _color_table {

    YellowHot=0,
    Black_Red=1,
    Black_Green=2,
    Black_Blue=3,
    Black_Yley=4,
    RGB=5,
    Pseudo=6,
    Gray

}ColorTable;


typedef struct _Pseudo_info
{
    int maxOD;
    int minOD;
    int IOD;
    int count;
    float AOD;
}PseudoInfo;