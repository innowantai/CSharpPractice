using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindPrimeNumber
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n");

            //long data = 1233211234;
            long data = 206265;
            List<long> res = new List<long>();
            res.Add(1);
            long Residual = 0;

            FindAll(data, ref res,ref Residual);
            res.Add(Residual);

            Console.Write("數字 {0} 的質數有 : ",data);
            foreach (var ff in res)
            {
                Console.Write(ff + " ");
            }
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n");
        }

        private static void FindAll(long data, ref List<long> res,ref long Residual)
        {
            if (FindPrimeNumber(data, ref res, ref Residual))
                FindAll(Residual,ref res,ref Residual);
                          
        }

        private static bool FindPrimeNumber(long data, ref List<long> res ,ref long Residual)
        { 
            for (int i = 2; i < data; i++)
            {
                if (data % i == 0)
                {
                    res.Add(i);
                    Residual = data / i;
                    return true;
                }
            }

            return false;
        }



        private static bool FindPrimeNumber(int data)
        {

            for (int i = 2; i < data; i++)
            {
                if (data % i == 0)
                    return false;
            }

            return true;
        }
    }
}
