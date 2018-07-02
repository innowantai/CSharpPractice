using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using MartixOperator;

namespace CostStatstic
{
    class Program
    {
        static string oriPath = System.Environment.CurrentDirectory;
        static void Main(string[] args)
        {
            string fileName = "106組工成本歸屬(瑞遠)宏匯凱悅.xls";
            string[,] data = ExcelClass.ExcelSaveAndRead.Read(Path.Combine(oriPath, fileName),1,1,13);
            MatrixS mStr = new MatrixS(data);
            int startPo = mStr.GetVector(0, 0).Find("日期") + 2;
            int endPo = mStr.GetVector(0, 0).Find("總計") - 1;
            int conColPo = 10; ///工作內容位置



            string[] content = data[startPo, 10].Split(' ');
            foreach (string ff in content)
            {
                Console.WriteLine(ff);
            }

            for (int i = startPo; i <= endPo; i++)
            { 
            }

            Console.WriteLine(data[endPo, 10]);
           
        }







        //ExcelClass.ExcelSaveAndRead.SaveCreat(Path.Combine(oriPath, "test.xls"), "1", 1, 1, data);
    }
}
