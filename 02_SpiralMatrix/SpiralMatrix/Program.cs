using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] input = new int[4, 2] { { 3, 0 }, { 20, 1 }, { 5, 2 }, { 9, 2 } };
            for (int i = 1; i <= input[0, 0]; i++)
            {
                Console.WriteLine("-------Case : {0}-------", i);
                int T = input[i, 0];
                int method = input[i, 1];
                int[,] M = Matrix(T, method);
                PrintResult(M);
            }

        }


        public static int[,] Matrix(int T, int method)
        {

            int[,] M = new int[T, T];
            int count = 1;
            int boundRight = T - 1;
            int boundDown = T - 1;
            int boundLeft = 0;
            int boundUp = 1;
            int CASE = 1;
            int col = -1;
            int row = 0;
            while (count <= T * T)
            {
                if (CASE == 1)
                {
                    col++;
                    if (col == boundRight)
                    {
                        CASE = 2;
                        boundRight--;
                    }
                }
                else if (CASE == 2)
                {

                    row++;
                    if (row == boundDown)
                    {
                        CASE = 3;
                        boundDown--;
                    }
                }
                else if (CASE == 3)
                {

                    col--;
                    if (col == boundLeft)
                    {
                        CASE = 4;
                        boundLeft++;
                    }
                }
                else if (CASE == 4)
                {
                    row--;
                    if (row == boundUp)
                    {
                        CASE = 1;
                        boundUp++;
                    }
                }
                M[row, col] = count;
                count++;
            }

            if (method == 1)
            {
                return M;

            }
            return Inverse(M);

        }


        public static int[,] Inverse(int[,] M)
        {
            int L = M.GetLength(0);
            int[,] newM = new int[L, L];

            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j < L; j++)
                {
                    newM[i, j] = M[j, i];
                }
            }
            return newM;

        }

        public static void PrintResult(int[,] M)
        {
            int T = M.GetLength(0);
            for (int i = 0; i < T; i++)
            {
                for (int j = 0; j < T; j++)
                {
                    Console.Write("{0} ", M[i, j]);
                }
                Console.WriteLine("");
            }

        }

    }



}
