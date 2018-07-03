using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartixOperator
{
    public class Matrix
    {
        private double[,] data;
        public double[,] Data { get { return data; } }

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

        /// <summary>
        /// 轉置
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 取得某一行或某一列向量
        /// </summary>
        /// <param name="pos"></param>       空置位置
        /// <param name="ColorRow"></param>  空置行或列
        /// <returns></returns>
        public Vector GetVector(int pos = 0, int ColorRow = 0)
        {
            double[] res = new double[this.data.GetLength(ColorRow)];
            for (int i = 0; i < this.data.GetLength(ColorRow); i++)
                res[i] = ColorRow == 0 ? this.data[i, pos] : this.data[pos, i];
            return new Vector(res);

        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < this.data.GetLength(0); i++)
            {
                for (int j = 0; j < this.data.GetLength(1); j++)
                    Console.Write(this.data[i, j] + " ");
                Console.WriteLine("");
            }
        }        


        /// <summary>
        /// 使用operator實現矩陣+ - * 
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
            double[,] res = new double[M1.data.GetLength(0), M2.data.GetLength(1)];
            Matrix M3 = new Matrix(res);

            for (int i = 0; i < M1.data.GetLength(0); i++)
            {
                for (int j = 0; j < M2.data.GetLength(1); j++)
                {
                    M3.data[i, j] = M1.GetVector(i, 1) * M2.GetVector(j, 0);
                    //M3.data[i, j] = M1.GetVector(i, 1).Dot(M2.GetVector(j, 0));
                }
            }
            return M3;
        }
        public static bool operator >(Matrix M1, Matrix M2)  {    return M1 > M2;  }
        public static bool operator <(Matrix M1, Matrix M2)  {    return M1 < M2;  }
    }


    public class Vector
    {
        private double[] data;
        public double[] Data { get { return data; } }
        public Vector(double[] array)
        {
            data = new double[array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
                data[i] = array[i];
        }


        /// <summary>
        /// 找出指定數字於向量中的位置，若不存在返回-1
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public int Find(double res)
        {
            int po = 0;
            foreach (double dd in this.data)
            {
                if (dd == res)
                    return po;
                po++;
            }
            return -1;
        }

        /// <summary>
        /// 求出向量總和
        /// </summary>
        /// <returns></returns>
        public double Sum()
        {
            double res = 0;
            foreach (double dd in this.data)
            {
                res += dd;
            }
            return res;
        }

        /// <summary>
        /// 使用operator實現向量內積
        /// </summary>
        /// <param name="V1"></param>
        /// <param name="V2"></param>
        /// <returns></returns>
        public static double operator *(Vector V1, Vector V2)
        {
            double res = 0;
            for (int i = 0; i < V1.data.GetLength(0); i++)
                res += V1.data[i] * V2.data[i];
            return res;
        }

        /// <summary>
        /// 使用operator實現向量相加
        /// </summary>
        /// <param name="V1"></param>
        /// <param name="V2"></param>
        /// <returns></returns>
        public static Vector operator + (Vector V1, Vector V2)
        {
            Vector res = new Vector(new double[V1.data.GetLength(0)]);
            for (int i = 0; i < V1.data.GetLength(0); i++)
                res.data[i] = V1.data[i] + V2.data[i];
            return res;
        }

        /// <summary>
        /// 使用operator實現向量相減
        /// </summary>
        /// <param name="V1"></param>
        /// <param name="V2"></param>
        /// <returns></returns>
        public static Vector operator -(Vector V1, Vector V2)
        {
            Vector res = new Vector(new double[V1.data.GetLength(0)]);
            for (int i = 0; i < V1.data.GetLength(0); i++)
                res.data[i] = V1.data[i] - V2.data[i];
            return res;
        }


        public static bool operator >(Vector V1, Vector V2)  {    return V1 > V2;  }
        public static bool operator <(Vector V1, Vector V2)  {    return V1 < V2;  }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {

            for (int i = 0; i < this.data.GetLength(0); i++)
                Console.Write(this.data[i] + " ");
            Console.WriteLine("");
        }
    }


    /// <summary>
    /// 字串形式的矩陣
    /// </summary>
    public class MatrixS
    {
        private string[,] data;
        public string[,] Data { get { return data; } }

        public MatrixS(string[,] array)
        {
            data = new string[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    data[i, j] = array[i, j];
                }
            }
        }

        /// <summary>
        /// 取的某一行或某一列之字串向量
        /// </summary>
        /// <param name="pos"></param>       控制位置
        /// <param name="ColorRow"></param>  控制行或列
        /// <returns></returns>
        public VectorS GetVector(int pos = 0, int ColorRow = 0)
        {
            string[] res = new string[this.data.GetLength(ColorRow)];
            for (int i = 0; i < this.data.GetLength(ColorRow); i++)
                res[i] = ColorRow == 0 ? this.data[i, pos] : this.data[pos, i];
            return new VectorS(res);

        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < this.data.GetLength(0); i++)
            {
                for (int j = 0; j < this.data.GetLength(1); j++)
                    Console.Write(this.data[i, j] + " ");
                Console.WriteLine("");
            }
        }
    }

    /// <summary>
    /// 字串形式的向量
    /// </summary>
    public class VectorS
    {
        private string[] data;
        public string[] Data { get { return data; } }
        public VectorS(string[] array)
        {
            data = new string[array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
                data[i] = array[i];
        }

        /// <summary>
        /// 找出指定字串於向量中的位置，若不存在返回-1
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public int Find(string res)
        {
            int po = 0;
            foreach (string ff in this.data)
            {
                if (ff == res)
                    return po;
                po++;
            }
            return -1; 
        } 

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {

            for (int i = 0; i < this.data.GetLength(0); i++)
                Console.Write(this.data[i] + " ");
            Console.WriteLine("");
        }
    }


     



    class Program
    {
        static void Main(string[] args)
        {


            MatrixS m1 = new MatrixS(new string[,] { { "1", "2", "3" }, { "4", "5", "6" }, { "1asdasd1", "12", "13" } });
            Matrix m = new Matrix(new double[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } });
            VectorS s1 = m1.GetVector(1, 0);

            string ss = "11111,22222,33333,4,5,6,7,8,9,0";
            string[] sr = ss.Split(',');
            VectorS vs = new VectorS(sr);
            string[] sq = vs.Data;
            Console.WriteLine(vs.Find("9")) ;
            

        }
    }
}





//public Matrix Add(Matrix input)
//{
//    /// 矩陣大小不同 回傳-1
//    if (this.data.Length != input.data.Length)
//        return new Matrix(new double[,] { { -1 } });

//    Matrix res = new Matrix(input.data);
//    for (int i = 0; i < input.data.GetLength(0); i++)
//    {
//        for (int j = 0; j < input.data.GetLength(1); j++)
//        {
//            res.data[i, j] = this.data[i, j] + input.data[i, j];
//        }
//    }
//    return res;
//}

//public Matrix Minus(Matrix input)
//{

//    /// 矩陣大小不同 回傳-1
//    if (this.data.Length != input.data.Length)
//        return new Matrix(new double[,] { { -1 } });

//    Matrix res = new Matrix(input.data);
//    for (int i = 0; i < input.data.GetLength(0); i++)
//    {
//        for (int j = 0; j < input.data.GetLength(1); j++)
//        {
//            res.data[i, j] = this.data[i, j] - input.data[i, j];
//        }
//    }
//    return res;
//}

//public Matrix Mult(Matrix input)
//{

//    /// 矩陣大小不合法無法相乘時候回傳-1
//    if (this.data.GetLength(1) != input.data.GetLength(0))
//        return new Matrix(new double[,] { { -1 } });

//    double[,] res = new double[this.data.GetLength(0), input.data.GetLength(1)];
//    this.Mdata = new Matrix(this.data);
//    for (int i = 0; i < this.data.GetLength(0); i++)
//    {
//        for (int j = 0; j < input.data.GetLength(1); j++)
//        {
//            res[i, j] = this.Mdata.GetVector(i, 1).Dot(input.GetVector(j, 0));
//        }
//    }
//    return new Matrix(res);
//}