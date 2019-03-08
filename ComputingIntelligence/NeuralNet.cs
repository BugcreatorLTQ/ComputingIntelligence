using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{
    /// <summary>
    /// 神经网络
    /// </summary>
    class NeuralNet
    {
        /// <summary>
        /// 输入矩阵
        /// </summary>
        public Matrix Input { set; get; }

        /// <summary>
        /// 输出矩阵
        /// </summary>
        public Matrix Output { set; get; }

        /// <summary>
        /// 权重矩阵
        /// </summary>
        public Matrix Weights { set; get; }

        /// <summary>
        /// 阈值矩阵
        /// </summary>
        public Matrix Threshold { set; get; }

        /// <summary>
        /// 定义神经元函数委托
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public delegate Matrix Function(Matrix input);

        /// <summary>
        /// 定义神经元函数
        /// </summary>
        public Function function;

        /// <summary>
        /// 创建一个神经网络
        /// </summary>
        /// <param name="input">输入矩阵</param>
        /// <param name="output">输出矩阵</param>
        public NeuralNet(Matrix input, Matrix output)
        {
            Input = input;
            Output = output;
            // 生成权重矩阵
            Weights = Matrix.GetRandomMatrix(output.Row, input.Row);
        }

        /// <summary>
        /// 修正权重矩阵
        /// </summary>
        /// <param name="error">计算误差</param>
        private void FixWeights(Matrix error)
        {
            Weights += 0.9f * error * Input;
            Threshold += 0.9f * error;
        }

        /// <summary>
        /// 训练网络
        /// </summary>
        public void Training()
        {
            // 训练次数
            int times = 50;
            // 用于结果矩阵
            Matrix outMat = new Matrix(Output.Row, Output.Column);
            Matrix errMat = new Matrix(Output.Row, Output.Column);
            while (times-- > 0)
            {
                // 判断函数是否为空
                if (function == null)
                {
                    throw new Exception("神经元函数为空");
                }
                // 计算输出
                outMat = function(Weights * Input + Threshold);
                // 计算误差
                errMat = (outMat - Output);
                // 修正权重
            }
        }
    }
}



