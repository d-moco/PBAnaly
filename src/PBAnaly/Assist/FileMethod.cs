
using AntdUI;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using static AntdUI.Table;
using System.IO;
using System.Text;
using System;
using GroupDocs.Conversion.Options.Convert;



namespace PBAnaly.Assist
{
    public static class FileMethod
    {
        public static void ConvertExcelToPdf(string excelPath, string pdfPath)
        {

            using (var converter = new GroupDocs.Conversion.Converter(excelPath))
            {
                // 设置起始张数和连续张数
                var convertOptions = new PdfConvertOptions()
                {
                    PageNumber = 2,
                    PagesCount = 3
                };
                // 将电子表格转换并保存为 PDF 格式
                converter.Convert(pdfPath, convertOptions);
            }

        }

        /// <summary>
        /// 将文件转换为byte数组
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns>转换后的byte数组</returns>
        public static byte[] File2Bytes(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return new byte[0];
            }

            FileInfo fi = new FileInfo(path);
            byte[] buff = new byte[fi.Length];

            FileStream fs = fi.OpenRead();
            fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            return buff;
        }

    }
}
