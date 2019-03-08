using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ComputingIntelligence
{
    /// <summary>
    /// 矩阵类
    /// 提供矩阵的基本操作
    /// </summary>
    class Matrix
    {
        /// <summary>
        /// 矩阵的行
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// 矩阵的列
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// 矩阵的数据
        /// </summary>
        public float[] Data { get; set; }

        /// <summary>
        /// 创建一个空矩阵
        /// </summary>
        /// <param name="row">矩阵的行</param>
        /// <param name="column">矩阵的列</param>
        public Matrix(int row, int column)
        {
            Row = row;
            Column = column;
            Data = new float[row * column];
        }

        /// <summary>
        /// 构造一个矩阵
        /// </summary>
        /// <param name="row">矩阵的行数</param>
        /// <param name="column">矩阵的列数</param>
        /// <param name="data">矩阵的数据</param>
        public Matrix(int row, int column, float[] data)
        {
            ReSetMatrix(row, column, data);
        }

        /// <summary>
        /// 重新设置矩阵的大小及数据
        /// </summary>
        /// <param name="row">矩阵的行数</param>
        /// <param name="column">矩阵的列数</param>
        /// <param name="data">矩阵的元素数组</param>
        public void ReSetMatrix(int row, int column, float[] data)
        {
            // 检查数据个数是否正确
            if(data.Length != row * column)
            {
                throw new Exception("创建矩阵异常：矩阵元素个数异常");
            }
            // 对属性重新赋值
            Row = row;
            Column = column;
            Data = new float[data.Length];
            data.CopyTo(Data, 0);
        }

        /// <summary>
        /// 获取一个随机矩阵
        /// </summary>
        /// <param name="row">矩阵的行数</param>
        /// <param name="column">矩阵的列数</param>
        /// <returns>一个row行column列的随机矩阵</returns>
        public static Matrix GetRandomMatrix(int row, int column)
        {
            // 申请数组
            float[] data = new float[row * column];
            Random random = new Random();
            for(int i = 0; i < data.Length; i++)
            {
                // 获取随机数
                data[i] = (float)random.NextDouble();
            }
            // 创建随机数组
            Matrix randomMatrix = new Matrix(row, column,data);
            return randomMatrix;
        }

        /// <summary>
        /// 复制一个矩阵
        /// </summary>
        /// <param name="matrix">源矩阵</param>
        /// <returns>复制的矩阵</returns>
        public static Matrix Clone(Matrix matrix)
        {
            // 创建一个同大小的空数组
            Matrix cloneMatrix = new Matrix(matrix.Row, matrix.Column);
            // 复制数据
            matrix.Data.CopyTo(cloneMatrix.Data, 0);
            return cloneMatrix;
        }

        /// <summary>
        /// 矩阵取负
        /// </summary>
        /// <param name="srcMatrix">运算右矩阵</param>
        /// <returns>取负后的矩阵</returns>
        public static Matrix operator-(Matrix srcMatrix)
        {
            Matrix matrix = Clone(srcMatrix);
            // 元素取负
            for (int i = 0; i < matrix.Data.Length; i++)
            {
                matrix.Data[i] = -matrix.Data[i];
            }
            return matrix;
        }

        /// <summary>
        /// 矩阵加法
        /// </summary>
        /// <param name="A">运算左矩阵</param>
        /// <param name="B">运算右矩阵</param>
        /// <returns>运算后的矩阵</returns>
        public static Matrix operator+(Matrix A, Matrix B)
        {
            // 检查维度
            if (A.Row != B.Row || A.Column != B.Column)
            {
                throw new Exception("矩阵加减法异常：矩阵维度不一致");
            }
            // 复制A数组
            Matrix C = Clone(A);
            int size = C.Data.Length;
            // 做矩阵加法
            for(int i = 0;i<size;i++)
            {
                C.Data[i] += B.Data[i];
            }
            return C;
        }

        /// <summary>
        /// 矩阵减法
        /// </summary>
        /// <param name="A">运算左矩阵</param>
        /// <param name="B">运算右矩阵</param>
        /// <returns>运算后的矩阵</returns>
        public static Matrix operator-(Matrix A, Matrix B)
        {
            // 复制A数组
            Matrix matrix = Clone(A);
            // 做矩阵减法
            matrix += -B;
            return matrix;
        }

        /// <summary>
        /// 矩阵乘法
        /// </summary>
        /// <param name="A">运算左矩阵</param>
        /// <param name="B">运算右矩阵</param>
        /// <returns>运算后的矩阵</returns>
        public static Matrix operator*(Matrix A,Matrix B)
        {
            // 检查维度
            if(A.Column != B.Row)
            {
                throw new Exception("矩阵乘法异常：矩阵维度不一致");
            }
            // 创建结果矩阵
            Matrix matrix = new Matrix(A.Row, B.Column);
            // 做矩阵乘法
            for(int i = 0; i < matrix.Row; i++)
            {
                // i行
                for(int j = 0; j < matrix.Column; j++)
                {
                    // j列
                    for(int k = 0; k < A.Column; k++)
                    {
                        // 累加和
                        matrix.Data[i * matrix.Column + j] += A.Data[i * A.Column + k] * B.Data[k * B.Column + j];
                    }

                }
            }
            return matrix;
        }

        /// <summary>
        /// 矩阵乘法
        /// </summary>
        /// <param name="A">运算矩阵</param>
        /// <param name="num">运算常数</param>
        /// <returns>运算后的矩阵</returns>
        public static Matrix operator*(Matrix A, float num)
        {
            Matrix matrix = Clone(A);
            for (int i = 0; i < matrix.Data.Length; i++)
            {
                matrix.Data[i] *= num;
            }
            return matrix;
        }

        /// <summary>
        /// 矩阵乘法
        /// </summary>
        /// <param name="num">运算常数</param>
        /// <param name="A">运算矩阵</param>
        /// <returns>运算后的矩阵</returns>
        public static Matrix operator*(float num, Matrix A)
        {
            return A * num;
        }

        /// <summary>
        /// 获取转置矩阵
        /// </summary>
        /// <returns>转置后的矩阵</returns>
        public Matrix GetT()
        {
            Matrix matrix = new Matrix(Column, Row);
            // 第i行
            for(int i = 0; i < Column; i++)
            {
                // 第j列
                for(int j = 0; j < Row; j++)
                {
                    matrix.Data[i * Row + j] = Data[j * Column + i];
                }
            }
            return matrix;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>转化的字符串</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++) {
                    stringBuilder.Append(Data[i * Column + j]);
                    stringBuilder.Append(' ');
                }
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();
        }

    }
}
