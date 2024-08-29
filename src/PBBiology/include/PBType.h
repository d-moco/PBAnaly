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
