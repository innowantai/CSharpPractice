using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace TrangToEnglishSubtitles
{
    class Program
    {
        public static string path = System.Environment.CurrentDirectory;
        static void Main(string[] args)
        {
            
            string loadPath = Path.Combine(path, "ori");
            string savePath = Path.Combine(path, "res");

            /// 檢查儲存資料夾是否存在,不存在則創立
            DirectoryInfo save = new DirectoryInfo(savePath);
            if (!save.Exists)
                save.Create(); 
            
            /// 遍歷檔案資料夾所有檔案並處理將結果儲存至res資料夾
            DirectoryInfo info = new DirectoryInfo(Path.Combine(loadPath));
            foreach (var file in info.GetFiles())
            {
                string[] data = Process(loading(file.ToString(), loadPath));
                Saving(data, file.ToString(), savePath);
            }

        }


        /// <summary>
        /// 處理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string[] Process(string[] data)
        {
            int kk = 0;
            foreach (var ff in data)
            {
                if (ff.IndexOf("Dialogue") != -1)
                {
                    int po1 = ff.IndexOf(",,") + 2;
                    string resStr1 = ff.Substring(0, po1);
                    string index = ff.Substring(po1, ff.Length - po1);
                    int po2 = index.IndexOf("{");
                    string resStr2 = po2 == -1 ? "" : index.Substring(po2, index.Length - po2);
                    string resStr = resStr1 + resStr2;


                    if (resStr2 == "")
                    {
                        resStr = ff;
                        Console.WriteLine(resStr2);
                    }

                    int po3 = resStr.IndexOf("fs");
                    if (po3 != -1)
                    {
                        string fsSize = resStr.Substring(po3 , 4);
                        resStr = resStr.Replace(fsSize, "fs16");

                    }

                    resStr = resStr.Replace("\\fn", "");

                    data[kk] = resStr;
                }
                // Console.WriteLine(data[kk]);
                kk++;
            }

            return data;
        }


        private static string[] loading(string fileName,string loadPath)
        {
            StreamReader sr = new StreamReader(Path.Combine(loadPath, fileName));
            List<string> data = new List<string>(); 
            while (sr.Peek() != -1)
            {
                string index = sr.ReadLine();
                data.Add(index);
            }
            sr.Close();
            return data.ToArray();
        }

        private static void Saving(string[] data, string fileName, string savePath)
        {
            StreamWriter sw = new StreamWriter(Path.Combine(savePath, fileName));
            foreach (var ff in data)
            {
                sw.WriteLine(ff);
            }
        }


    }
}
