using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartixOperator
{
    class Matrix
    {
        double[,] data;
        Matrix Mdata;
         

        public Matrix(double[,] array)
        {
            data = new double[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    data[i, j] = array[i, j];
                }
            }

        } 

        public Matrix Add(Matrix input)
        {
            /// 矩陣大小不同 回傳-1
            if (this.data.Length != input.data.Length)
                return new Matrix(new double[,] { { -1 } });

            Matrix res = new Matrix(input.data);
            for (int i = 0; i < input.data.GetLength(0); i++)
            {
                for (int j = 0; j < input.data.GetLength(1); j++)
                {
                    res.data[i, j] = this.data[i, j] + input.data[i, j];
                }
            }
            return res;
        }

        public Matrix Minus(Matrix input)
        {

            /// 矩陣大小不同 回傳-1
            if (this.data.Length != input.data.Length)
                return new Matrix(new double[,] { { -1 } });

            Matrix res = new Matrix(input.data);
            for (int i = 0; i < input.data.GetLength(0); i++)
            {
                for (int j = 0; j < input.data.GetLength(1); j++)
                {
                    res.data[i, j] = this.data[i, j] - input.data[i, j];
                }
            }
            return res;
        }

        public Matrix Mult(Matrix input)
        {

            /// 矩陣大小不合法無法相乘時候回傳-1
            if (this.data.GetLength(1) != input.data.GetLength(0))
                return new Matrix(new double[,] { { -1 } });

            double[,] res = new double[this.data.GetLength(0), input.data.GetLength(1)];
            this.Mdata = new Matrix(this.data);
            for (int i = 0; i < this.data.GetLength(0); i++)
            {
                for (int j = 0; j < input.data.GetLength(1); j++)
                {
                    res[i, j] = this.Mdata.GetVector(i, 1).Dot(input.GetVector(j, 0));
                }
            }
            return new Matrix(res);
        }

        public Matrix Tran()
        {
            Matrix res = new Matrix(this.data);
            for (int i = 0; i < this.data.GetLength(0); i++)
            {
                for (int j = 0; j < this.data.GetLength(1); j++)
                {
                    res.data[i, j] = this.data[j, i];
                }
            }
            return res;
        }


        public Vector GetVector(int pos = 0, int ColorRow = 0)
        {
            double[] res = new double[this.data.GetLength(ColorRow)];
            for (int i = 0; i < this.data.GetLength(ColorRow); i++)
                res[i] = ColorRow == 0 ? this.data[i, pos] : this.data[pos, i];
            return new Vector(res);

        }

        public void print()
        {
            for (int i = 0; i < this.data.GetLength(0); i++)
            {
                for (int j = 0; j < this.data.GetLength(1); j++)
                    Console.Write(this.data[i, j] + " ");
                Console.WriteLine("");
            }
        }





        /// <summary>
        /// Use Operator to Control + - *  
        /// </summary>
        /// <param name="M1"></param>
        /// <param name="M2"></param>
        /// <returns></returns>

        public static Matrix operator +(Matrix M1, Matrix M2)
        {
            Matrix M3 = new Matrix(M1.data);
            for (int i = 0; i < M1.data.GetLength(0); i++)
            {
                for (int j = 0; j < M1.data.GetLength(1); j++)
                {
                    M3.data[i, j] = M1.data[i, j] + M2.data[i, j];
                }
            }
            return M3;
        }

        public static Matrix operator -(Matrix M1, Matrix M2)
        {
            Matrix M3 = new Matrix(M1.data);
            for (int i = 0; i < M1.data.GetLength(0); i++)
            {
                for (int j = 0; j < M1.data.GetLength(1); j++)
                {
                    M3.data[i, j] = M1.data[i, j] - M2.data[i, j];
                }
            }
            return M3;
        }


        public static Matrix operator *(Matrix M1, Matrix M2)
        {
            double[,] res = new double[M1.data.GetLength(0),M2.data.GetLength(1)]; 
            Matrix M3 = new Matrix(res);

            for (int i = 0; i < M1.data.GetLength(0); i++)
            {
                for (int j = 0; j < M2.data.GetLength(1); j++)
                {
                    M3.data[i, j] = M1.GetVector(i, 1)*M2.GetVector(j, 0);
                    //M3.data[i, j] = M1.GetVector(i, 1).Dot(M2.GetVector(j, 0));
                }
            }
            return M3;
        }

        public static bool operator >(Matrix M1, Matrix M2)
        {
            return M1 > M2;
        }
        public static bool operator <(Matrix M1, Matrix M2)
        {
            return M1 < M2;
        } 
    }


    class Vector
    {
        double[] data;
        public Vector(double[] array)
        {
            data = new double[array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
                data[i] = array[i];
        }

        public double Dot(Vector input)
        {
            double res = 0;
            for (int i = 0; i < input.data.GetLength(0); i++)
                res += input.data[i] * this.data[i];
            return res;
        } 
        public void Print()
        {

            for (int i = 0; i < this.data.GetLength(0); i++)
                Console.Write(this.data[i] + " ");
            Console.WriteLine("");
        }


        /// <summary>
        /// Operator Part below
        /// </summary>
        /// <param name="V1"></param>
        /// <param name="V2"></param>
        /// <returns></returns>
        public static double operator * (Vector V1,Vector V2)
        { 
            double res = 0;
            for (int i = 0; i < V1.data.GetLength(0); i++)
                res += V1.data[i] * V2.data[i];  

            return res;

        }
       

        public static bool operator > (Vector V1, Vector V2)
        {
            return V1 > V2;
        }

        public static bool operator <(Vector V1, Vector V2)
        {
            return V1 < V2;
        } 
    }


    class Program
    {
        static void Main(string[] args)
        { 

            Matrix M1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            Matrix M2 = new Matrix(new double[,] { { 4, 5  }, { 4, 5 }, { 4, 6 } }); 
            Matrix M5 = M1 * M2; 
            M5.print();
        }
    }
}
