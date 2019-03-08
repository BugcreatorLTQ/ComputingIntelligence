﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{
    /// <summary>
    /// 神经网络
    /// </summary>
    class SingleNeuralNetwork
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
        public Function Fun { get; set; }

        /// <summary>
        /// 创建一个神经网络
        /// </summary>
        /// <param name="input">输入矩阵</param>
        /// <param name="output">输出矩阵</param>
        public SingleNeuralNetwork(Matrix input, Matrix output)
        {
            Input = input;
            Output = output;
            // 生成权重矩阵
            Weights = Matrix.GetRandomMatrix(output.Row, input.Row);
            // 生成阈值矩阵
            Threshold = Matrix.GetRandomMatrix(output.Row, output.Column);
        }

        /// <summary>
        /// 修正权重矩阵
        /// </summary>
        /// <param name="error">计算误差</param>
        private void FixWeights(Matrix error)
        {
            // 修正权重矩阵
            Weights += 0.9f * error * Input.GetT();
            // 修正阈值矩阵
            Threshold += 0.9f * error;
        }

        /// <summary>
        /// 训练网络
        /// </summary>
        /// <param name="times">训练次数</param>
        public void Training(int times)
        {
            // 用于结果矩阵
            Matrix outMat;
            Matrix errMat;
            while (times-- > 0)
            {
                // 判断函数是否为空
                if (Fun == null)
                {
                    throw new Exception("神经元函数为空");
                }
                // 计算输出
                outMat = GetResult(Input);
                // 计算误差
                errMat = (Output - outMat);
                Console.WriteLine(Weights);
                // 修正权重
                FixWeights(errMat);
            }
        }

        /// <summary>
        /// 获取计算结果
        /// </summary>
        /// <param name="input">输入矩阵</param>
        /// <returns>结果矩阵</returns>
        public Matrix GetResult(Matrix input)
        {
            return Fun(Weights * input + Threshold);
        }

    }
}


