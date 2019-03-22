using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{
    /// <summary>
    /// 神经元响应函数
    /// </summary>
    public interface NetworkFunction
    {

        /// <summary>
        /// 导函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>结果矩阵</returns>
        Matrix func(Matrix matrix);

        /// <summary>
        /// 函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>结果矩阵</returns>
        Matrix function(Matrix matrix);
    }

    /// <summary>
    /// 阈值函数
    /// </summary>
    public class ThresholdFun : NetworkFunction
    {
        /// <summary>
        /// 导函数
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>错误</returns>
        public Matrix func(Matrix matrix)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>矩阵元素大于0为1否则为0</returns>
        public Matrix function(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);
            for (int i = 0; i < result.Data.Length; i++)
            {
                result.Data[i] = (matrix.Data[i] > 0) ? 1 : 0;
            }
            return result;
        }
    }

    /// <summary>
    /// 线性函数
    /// </summary>
    public class LinearFun : NetworkFunction
    {
        /// <summary>
        /// 斜率
        /// </summary>
        public float K { set; get; }

        /// <summary>
        /// 创建一个斜率为k的线性函数
        /// </summary>
        /// <param name="k">斜率</param>
        public LinearFun(float k)
        {
            K = k;
        }

        /// <summary>
        /// 创建一个斜率为1的线性函数
        /// </summary>
        public LinearFun():this(1)
        {
            
        }

        /// <summary>
        /// 导函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns导函数</returns>
        public Matrix func(Matrix matrix)
        {
            float[] data = new float[matrix.Data.Length];
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = K;
            }
            return new Matrix(matrix.Row, matrix.Column, data);
        }

        /// <summary>
        /// 函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>结果矩阵</returns>
        public Matrix function(Matrix matrix)
        {
            return matrix * K;
        }
    }

    /// <summary>
    /// Logsig函数
    /// </summary>
    public class LogSigFun : NetworkFunction
    {
        /// <summary>
        /// 导函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>导函数</returns>
        public Matrix func(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);
            for (int i = 0; i < result.Data.Length; i++)
            {
                float Fx = 1 / (1 + (float)Math.Pow(Math.E, (double)-matrix.Data[i]));
                result.Data[i] = Fx * (1 - Fx);
            }
            return result;
        }

        /// <summary>
        /// 函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>结果矩阵</returns>
        public Matrix function(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);
            for (int i = 0; i < result.Data.Length; i++)
            {
                float Fx = 1 / (1 + (float)Math.Pow(Math.E, (double)-matrix.Data[i]));
                result.Data[i] = Fx;
            }
            return result;
        }
    }

    /// <summary>
    /// Tansig函数
    /// </summary>
    public class TanSigFun : NetworkFunction
    {
        /// <summary>
        /// 导函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>导函数</returns>
        public Matrix func(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);
            for (int i = 0; i < result.Data.Length; i++)
            {
                float Fx = (float)Math.Tanh(matrix.Data[i]);
                result.Data[i] = (1 - Fx * Fx) / 2;
            }
            return result;
        }

        /// <summary>
        /// 函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>结果矩阵</returns>
        public Matrix function(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);
            for (int i = 0; i < result.Data.Length; i++)
            {
                float Fx = (float)Math.Tanh(matrix.Data[i]);
                result.Data[i] = Fx;
            }
            return result;
        }
    }

}
