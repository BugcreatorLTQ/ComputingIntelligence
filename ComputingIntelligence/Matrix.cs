using System;
using System.Text;


namespace ComputingIntelligence
{
    /// <summary>
    /// 矩阵类
    /// 提供矩阵的基本操作
    /// </summary>
    public class Matrix
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
        /// 获取转置矩阵
        /// </summary>
        public Matrix T { get { return GetT(); } }

        /// <summary>
        /// 获取绝对值矩阵
        /// </summary>
        public Matrix Abs { get { return GetAbs(); } }

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
        /// <param name="min">下限</param>
        /// <param name="max">上限</param>
        /// <returns>一个row行column列的随机矩阵</returns>
        public static Matrix GetRandomMatrix(int row, int column, double min = -1, double max = 1)
        {
            // 申请数组
            float[] data = new float[row * column];
            Random random = new Random();
            for(int i = 0; i < data.Length; i++)
            {
                // 获取随机数
                data[i] = (float)(random.NextDouble() * (max - min) + min);
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
        /// 创建一个单调矩阵
        /// </summary>
        /// <param name="start">起始值</param>
        /// <param name="end">结束值</param>
        /// <param name="size">元素个数</param>
        /// <returns>[srart,end],个数为size的单调矩阵</returns>
        public static Matrix Space(float start,float end,int size)
        {
            Matrix matrix = new Matrix(1, size);
            float step = (end - start) / size;
            for(int i = 0; i < size; i++)
            {
                matrix.Data[i] = start + i * step;
            }
            return matrix;
        }

        /// <summary>
        /// 创建一个单调矩阵
        /// </summary>
        /// <param name="start">起始值</param>
        /// <param name="end">结束值</param>
        /// <param name="step">步长</param>
        /// <returns>[srart,end],步长为step的单调矩阵</returns>
        public static Matrix Space(float start,float end, float step)
        {
            int size = (int)((end - start + 1E-6) / step) + 1;
            Matrix matrix = new Matrix(1, size);
            for(int i = 0; i < size; i++)
            {
                matrix.Data[i] = start + i * step;
            }
            return matrix;
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
            Matrix matrix;
            // 检查维度
            if (A.Column == 1 && A.Row == 1)
            {
                matrix = Clone(B);
                for (int i = 0; i < matrix.Data.Length; i++)
                {
                    matrix.Data[i] *= A.Data[0];
                }
                return matrix;
            }
            if (B.Column == 1 && B.Row == 1)
            {
                return B * A;
            }
            if (A.Column != B.Row)
            {
                throw new Exception("矩阵乘法异常：矩阵维度不一致");
            }
            // 创建结果矩阵
            matrix = new Matrix(A.Row, B.Column);
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
        /// 将一个数隐形传换为矩阵
        /// </summary>
        /// <param name="num">一个1*1的矩阵</param>
        public static implicit operator Matrix(float num)
        {
            float[] data = { num };
            return new Matrix(1, 1, data);
        }

        /// <summary>
        /// 矩阵元素依次相乘
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>结果矩阵</returns>
        public Matrix X(Matrix matrix)
        {
            Matrix result = Clone(this);
            for(int i = 0; i < result.Data.Length; i++)
            {
                result.Data[i] *= matrix.Data[i];
            }
            return result;
        }

        /// <summary>
        /// 矩阵转置
        /// </summary>
        /// <returns>转置后的矩阵</returns>
        protected Matrix GetT()
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
        /// 获取每列矩阵
        /// </summary>
        /// <returns>子矩阵数组</returns>
        public Matrix[] GetColumns()
        {
            Matrix[] matrixs = new Matrix[Column];
            for(int i = 0; i< Column; i++)
            {
                float[] data = new float[Row];
                // 第i列
                for(int j = 0; j < Row; j++)
                {
                    // 第j行
                    data[j] = Data[j * Column + i];
                }
                matrixs[i] = new Matrix(Row, 1, data);
            }
            return matrixs;
        }

        /// <summary>
        /// 获取每行矩阵
        /// </summary>
        /// <returns>子矩阵数组</returns>
        public Matrix[] GetRows()
        {
            Matrix[] matrixs = new Matrix[Row];
            for (int i = 0; i < Row; i++)
            {
                float[] data = new float[Column];
                // 第i行
                for (int j = 0; j < Column; j++)
                {
                    // 第j列
                    data[j] = Data[i * Column + j];
                }
                matrixs[i] = new Matrix(1, Column, data);
            }
            return matrixs;

        }

        /// <summary>
        /// 获取绝对值矩阵
        /// </summary>
        /// <returns>绝对值矩阵</returns>
        protected Matrix GetAbs()
        {
            Matrix matrix = Clone(this);
            for(int i = 0; i < Data.Length; i++)
            {
                matrix.Data[i] = Math.Abs(matrix.Data[i]);
            }
            return matrix;
        }

        /// <summary>
        /// 获取逆矩阵
        /// </summary>
        /// <returns>逆矩阵</returns>
        [Obsolete("未完成")]
        protected Matrix GetInv()
        {
            // 增广矩阵数据
            float[] data = new float[Data.Length * 2];
            for(int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    data[i * Column * 2 + j] = Data[i * Column + j];
                    data[i * Column * 2 + j * 2] = j == i ? 1 : 0;
                }
            }
            // 创建增广矩阵
            Matrix matrix = new Matrix(Row, Column * 2, data);
            Console.WriteLine(matrix);
            // 处理下三角矩阵
            for(int i = 0; i < Row; i++)
            {
                // 第i行
                // 处理第i列
                for(int j = i; j < Row; j++)
                {
                    ;
                }
            }
            // 处理上三角矩阵

            // 逆矩阵数据
            data = new float[Data.Length];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    data[i * Column + j] = matrix.Data[i * Column * 2 + j * 2];
                }
            }
            Matrix result = new Matrix(Row, Column, data);
            return result;
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
                    if (j < Column - 1) stringBuilder.Append(' ');
                }
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取子矩阵
        /// </summary>
        /// <param name="row_start">子矩阵开始行数</param>
        /// <param name="row_end">子矩阵结束行数</param>
        /// <returns>子矩阵</returns>
        public Matrix GetSubMatrix(int row_start, int row_end)
        {
            float[] data = new float[(row_end - row_start) * Column];
            Matrix[] datas = GetRows();
            for(int i = row_start; i < row_end; i++)
            {
                datas[i].Data.CopyTo(data, (i - row_start) * Column);
            }
            Matrix matrix = new Matrix(row_end - row_start, Column, data);
            return matrix;
        }
    }
}
