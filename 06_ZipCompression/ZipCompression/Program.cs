using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SharpCompress.Readers;

namespace ZipCompression
{
    class Program
    {
        static void Main(string[] args)
        {

            string targetPath = "";
            bool flag = getAnacondaPath(ref targetPath);


            if (flag)
            {
                Console.WriteLine("正在解壓縮Python函式庫");
                string start = targetPath;
                string Zip = "packages.zip";
                using (Stream stream = File.OpenRead(Zip))
                {
                    var reader = ReaderFactory.Open(stream);
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            Console.WriteLine(reader.Entry.Key);
                            reader.WriteEntryToDirectory(start, new SharpCompress.Common.ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                }

            }
            else
            {
                Console.WriteLine("請先安裝 Anaconda !");
            }






        }
        
        private static bool getAnacondaPath(ref string targetPath)
        { 
            List<string> Res = new List<string>();
            Res.Add(@"D:\");
            Res.Add(@"C:\Program Files (x86)");
            Res.Add(@"C:\Users");
            Res.Add(@"C:\Program Files");

            bool flag = false;
            for (int i = 0; i < 5; i++) Res = GetPaths(Res, ref flag);

            List<string> anaPath = new List<string>();
            foreach (var ss in Res)
            {
                if (ss.IndexOf("Anaconda") != -1)
                {
                    anaPath.Add(ss);
                }
            }
             
            if (flag)
            {
                foreach (var ana in anaPath)
                {
                    if (CkeckAnacondaFolder(ana))
                    {
                        targetPath = ana;
                        break;
                    }
                }
            }
            flag = targetPath == "" ? false : true;
             

            return flag;

        }

        private static bool CkeckAnacondaFolder(string path)
        {
            string[] ckeckStr = new string[] { "conda-meta", "Scripts", "Lib" };
            List<string> check = getFolders(path, new List<string>());
            int kk = 0;
            foreach (string cc in ckeckStr)
            {
                foreach (string ck in check)
                {
                    if (ck.IndexOf(cc) != -1)
                    {
                        kk = kk + 1;
                        break;
                    }
                }
            }
            return kk == 3 ? true : false;
        }

        private static List<string> GetPaths(List<string> Res,ref bool flag)
        {
            List<string> nRes = new List<string>();
            foreach (string ss in Res)
            {

                if (ss.IndexOf(@"\Local") != -1)
                {
                    continue;
                }

                if (ss.IndexOf("Anaconda") != -1) 
                {
                    //nRes.Clear();
                    nRes.Add(ss);
                    flag = true;
                }
                try
                {
                    nRes = getFolders(ss, nRes);
                }
                catch (System.Exception excpt)
                {
                }
            }
            return nRes;

        }
         


        private static List<string> getFolders(string path, List<string> Res)
        { 
            foreach (var ss in System.IO.Directory.GetDirectories(@path))
            {
                if (ss.IndexOf(".") == -1)
                {
                    Res.Add(ss);
                }

            }
            //showRes(Res);
            return Res;

        }
         

        private static void showRes(List<string> res)
        {
            foreach (var ss in res) 
                Console.WriteLine(ss); 
        }
         

    }
}
