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
        static string savePath = Path.Combine(oriPath, "0_Result");
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n\n\n"); 


            string res = Main_Process();

            Console.WriteLine("{0}\n\n\n\n",res); 
        }

        private static string Main_Process()
        { 
            string fileName = null;
            DirectoryInfo info = new DirectoryInfo(oriPath);
            foreach (var ff in info.GetFiles())
            {
                if (ff.ToString().IndexOf(".xls") != -1)
                {
                    fileName = ff.ToString();
                    break;
                }
            }
            if (fileName == null)
                return "無excel檔案";


            Console.WriteLine("處理檔案檔名為 : {0} \n",fileName); 
            Console.Write("輸入要處理的sheet頁數 : ");
            int sheetPo = Convert.ToInt32(Console.ReadLine());
             
            Console.WriteLine("\n---處理中---\n"); 

            string[,] data = ExcelClass.ExcelSaveAndRead.Read(Path.Combine(oriPath, fileName), 1, 1, sheetPo);
            MatrixS mStrData = new MatrixS(data);


            int startPo = mStrData.GetVector(0, 0).Find("日期") + 2;
            int endPo = mStrData.GetVector(0, 0).Find("總計") - 1;
            int savePo1 = mStrData.GetVector(0, 0).Find("承商") + 1;
            int savePo2 = 9;


            DictStatstic DICT = new DictStatstic();
            List<string> Blong = new List<string>();
            string[,] Table2 = new string[data.GetLength(0), 2];

            MainCatch(ref DICT, ref Blong, startPo, endPo, data, ref Table2);


            string[,] OutPut = DICT.ToArray();

            string[,] OutBlong = new string[Blong.Count, 1];
            string[] tmp = Blong.ToArray();
            for (int i = 0; i < Blong.Count; i++)
                OutBlong[i, 0] = tmp[i];

            File.Delete(Path.Combine(savePath, fileName));
            File.Copy(Path.Combine(oriPath, fileName), Path.Combine(savePath, fileName));
            ExcelClass.ExcelSaveAndRead.Save(Path.Combine(savePath, fileName), sheetPo, savePo1 + 1, 1, OutPut);
            ExcelClass.ExcelSaveAndRead.Save(Path.Combine(savePath, fileName), sheetPo, startPo + 1, savePo2 + 1, OutBlong);
            ExcelClass.ExcelSaveAndRead.SaveCreat(Path.Combine(savePath, "Table2.xls"), "test", 2, 1, Table2);
            return "處理完成";
        }


        private static void MainCatch(ref DictStatstic DICT, ref List<string> Blong, int startPo, int endPo, string[,] data, ref string[,] Table2)
        {
            MatrixS Mdata = new MatrixS(data);
            int kk = 0;
            for (int i = startPo; i <= endPo; i++)
            {
                /// 欲處理資料
                string[] content = data[i, 10].Split(' ');

                /// 至原始資料之字串矩陣取除目標列數，並將原始之Work與Stone工時與加班轉換並儲存至向量oriVWS中
                VectorS Vcontent = Mdata.GetVector(i, 1);
                Vector oriVWS = new Vector(new double[] { Convert.ToDouble(Vcontent.Data[1]), Convert.ToDouble(Vcontent.Data[2]), Convert.ToDouble(Vcontent.Data[3]), Convert.ToDouble(Vcontent.Data[4]) });

                /// 用來統計每一天所有之Work與Stone工時與加班
                Vector VWS = new Vector(new double[4]);

                /// 用來儲存每一天之歸屬資料
                string blong = "";

                foreach (string ff in content)
                {
                    if (ff.IndexOf(")") != -1)
                    {
                        Table2[kk, 0] = i.ToString();
                        Table2[kk, 0] = data[i, 0];
                        Table2[kk, 1] = ff.Split('.')[1];
                        string catData = ff.Substring(ff.IndexOf("(") + 1, ff.IndexOf(")") - ff.IndexOf("(") - 1);
                        string CompanyName = null;
                        double[] WorkAndStone = CatDataProcess(catData, ref CompanyName, ref blong);
                        Vector vws = new Vector(WorkAndStone);
                        DICT.Add(CompanyName, WorkAndStone);
                        VWS += vws;
                        kk++;
                    }
                }
                Vector ResVWS = oriVWS - VWS;
                blong = OriComBlongProcess(ResVWS, blong);

                Blong.Add(blong);
                DICT.Add("利晉", ResVWS.Data);
            }

        }




        /// <summary>
        /// 將公司名稱與work stone資訊分開,"亞泥2+8h" --> {"亞泥" "2+8h"}
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        private static string[] SpliteFromCompanyName(string Data)
        {
            string[] res = new string[2];
            int kk = 0;
            foreach (char ff in Data)
            {
                if (char.IsNumber(ff))
                    return new string[] { Data.Substring(0, kk), Data.Substring(kk, Data.Length - kk) };
                if (ff == ',')
                    return new string[] { Data.Substring(0, kk), Data.Substring(kk + 1, Data.Length - kk - 1) };
                kk++;
            }
            return null;
        }


        /// <summary>
        /// 將input:"亞泥2+8h,打"1+2h""做處理
        /// 1.擷取公司名稱
        /// 2.以'打'字串為分界，前半部為Work，後半部為Stone
        /// 3.擷取Work與Stone之工時與加班依序儲存至陣列輸出
        /// </summary>
        /// <param name="catData"></param>
        /// <param name="CompanyName"></param>
        /// <param name="blong"></param>
        /// <returns></returns>
        private static double[] CatDataProcess(string catData, ref string CompanyName, ref string blong)
        {
            string[] CatData = SpliteFromCompanyName(catData);
            CompanyName = CatData[0];
            string ProcessData = CatData[1];
            string[] twoPart = ProcessData.Split('打');
            string WorkPart = twoPart[0];
            string StonePart = twoPart.GetLength(0) == 1 ? null : twoPart[1];
            blong += CompanyName + ProcessData.Replace(",", "");

            double[] WorkData = Statstic(WorkPart);
            double[] StoneData = new double[2];
            if (StonePart != null)
                StoneData = Statstic(StonePart);

            return new double[4] { WorkData[0], WorkData[1], StoneData[0], StoneData[1] };
        }

        /// <summary>
        /// input為 "2+3h"形式，分別擷取input內數字儲存至res中輸出，若沒有則為0
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static double[] Statstic(string data)
        {
            double[] res = new double[2];
            if (data == "")
                return res;

            string[] splitData = data.Split('+');
            res[0] = Convert.ToDouble(splitData[0]);

            if (splitData.GetLength(0) == 2)
                res[1] = Convert.ToDouble(splitData[1].Split('h')[0]);

            return res;

        }


        private static string OriComBlongProcess(Vector ResVWS, string blong)
        {
            string res = "利晉";
            if (ResVWS.Sum() == 0)
                return blong;

            if (ResVWS.Data[0] != 0)
                res += ResVWS.Data[0].ToString();
            if (ResVWS.Data[1] != 0)
                res += "+" + ResVWS.Data[1].ToString() + "h";
            if (ResVWS.Data[2] != 0)
                res += "打" + ResVWS.Data[2].ToString();
            if (ResVWS.Data[3] != 0)
                res += "+" + ResVWS.Data[3].ToString() + "h";


            return res + blong; ;
        }
        //ExcelClass.ExcelSaveAndRead.SaveCreat(Path.Combine(oriPath, "test.xls"), "1", 1, 1, data);
    }


    class DictStatstic
    {
        private Dictionary<string, double>[] Dict;
        private string[,] DictArray;
        public Dictionary<string, double>[] GetDict { get { return Dict; } }
        //public string[,] GetArray { get { return DictArray; } }

        public DictStatstic()
        {
            Dict = new Dictionary<string, double>[4];
            for (int i = 0; i < 4; i++)
                Dict[i] = new Dictionary<string, double>();
        }

        public void Add(string Key, double[] WorkAndStone)
        {
            Dictionary<string, double> index;
            for (int i = 0; i < 4; i++)
            {
                index = this.Dict[i];
                if (index.ContainsKey(Key))
                    index[Key] += WorkAndStone[i];
                else
                    index[Key] = WorkAndStone[i];
            }
        }

        public string[,] ToArray()
        {
            List<string[,]> Res = new List<string[,]>();

            DictArray = new string[this.Dict[0].Keys.ToArray().GetLength(0), 5];
            int po = 1;
            foreach (Dictionary<string, double> Dtmp in this.Dict)
            {
                string[] KEYS = Dtmp.Keys.ToArray();
                int kk = 1;
                foreach (var ff in KEYS)
                {
                    if (ff != "利晉")
                    {
                        DictArray[kk, 0] = ff;
                        DictArray[kk, po] = Dtmp[ff].ToString();
                        kk++;
                    }
                    else
                    {
                        DictArray[0, 0] = ff;
                        DictArray[0, po] = Dtmp[ff].ToString();
                    }
                }
                po++;
            }
            return DictArray;
        }


        public void Print()
        {
            Dictionary<string, double> index;
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("====Case {0}====", i);
                index = this.Dict[i];
                foreach (var ff in index)
                {
                    Console.WriteLine("{0} : {1}", ff.Key, index[ff.Key]);
                }
            }
        }

        public void PrintArray()
        {
            for (int i = 0; i < this.DictArray.GetLength(0); i++)
            {
                for (int j = 0; j < this.DictArray.GetLength(1); j++)
                {
                    Console.Write(this.DictArray[i, j] + " ");
                }
                Console.WriteLine(" ");
            }
        }
    }


}
