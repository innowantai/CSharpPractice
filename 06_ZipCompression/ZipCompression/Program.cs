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

            string start = @"C:\Users\Wantai\Desktop";
            string Zip = @"C:\Users\Wantai\Desktop\Lib.zip";
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
    }
}
