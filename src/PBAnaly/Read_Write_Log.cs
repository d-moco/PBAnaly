using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBAnaly
{
    public class Read_Write_Log
    {
        public Read_Write_Log()
        {

        }

        public string LogFile = Global.mDataUser + "Log.csv";

        public List<Log> ReadCsv(string FilePath)
        {
            List<Log> returnInfo = new List<Log>();

            using (TextFieldParser parser = new TextFieldParser(FilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                // Read and parse each line of the CSV file
                try
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        int count = 0;
                        string UserId = "";
                        string Item = "";
                        string Description = "";
                        string Time = "";
                        foreach (var item in fields)
                        {
                            if ((count % 4) == 0)
                            {
                                UserId = item;
                            }
                            else if ((count % 4) == 1)
                            {
                                Item = item;
                            }
                            else if ((count % 4) == 2)
                            {
                                Description = item;
                            }
                            else
                            {
                                Time = item;
                                returnInfo.Add(new Log() { UserID = UserId, ITEM = Item, Description = Description, Time = Time});
                            }

                            count++;
                        }
                    }
                    parser.Close();
                    return returnInfo;
                }
                catch
                {
                    return null;
                }
            }
        }

        public void WriteCsv(string FilePath, List<Log> LogInfos)
        {
            using (var file = new StreamWriter(FilePath))
            {
                foreach (var item in LogInfos)
                {
                    file.WriteLineAsync($"{item.UserID},{item.ITEM},{item.Description},{item.Time}");
                }

                file.Close();
            }
        }

    }
}
