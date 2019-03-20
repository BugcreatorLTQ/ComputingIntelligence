﻿using System;
using System.IO;
using static ComputingIntelligence.SingleNeuralNetwork;

namespace ComputingIntelligence
{
    class BPNeuralNetwork
    {
        /// <summary>
        /// 输入-隐含网络
        /// </summary>
        public SingleNeuralNetwork IH { set; get; }

        /// <summary>
        /// 隐含-输出网络
        /// </summary>
        public SingleNeuralNetwork HO { set; get; }

        /// <summary>
        /// 输入矩阵
        /// </summary>
        public Matrix Input { set; get; }

        /// <summary>
        /// 输出矩阵
        /// </summary>
        public Matrix Output { set; get; }

        /// <summary>
        /// 创建一个BP神经网络
        /// </summary>
        /// <param name="Input">输入矩阵</param>
        /// <param name="Output">输出矩阵</param>
        public BPNeuralNetwork(Matrix input, Matrix output)
        {
            Input = input;
            Output = output;
            // 构建隐含层 
            Matrix Hide = new Matrix((int)Math.Sqrt(Input.Row * Output.Row) + 3, 1);
            // 构建输入-隐含网络
            IH = new SingleNeuralNetwork(Input, Hide);
            // 构建隐含-输入网络
            HO = new SingleNeuralNetwork(Hide, Output);
        }

        /// <summary>
        /// 获取计算结果
        /// </summary>
        /// <param name="input">输入矩阵</param>
        /// <returns>计算结果矩阵</returns>
        public Matrix GetResult(Matrix input)
        {
            Matrix result = HO.GetResult(IH.GetResult(input));
            return result;
        }

        /// <summary>
        /// 修正权重矩阵
        /// </summary>
        /// <param name="input">输入矩阵</param>
        /// <param name="error">误差矩阵</param>
        public void FixWeights(Matrix input, Matrix error)
        {
            // 计算隐含层误差
            Matrix errHide = HO.Weights.T * error;
            // 修正HO网络
            HO.FixWeights(IH.GetResult(input), error);
            // 修正IO网络
            IH.FixWeights(input, errHide);
        }

        /// <summary>
        /// 训练网络
        /// </summary>
        /// <param name="times">训练次数</param>
        public void Training(int times)
        {
            // 用于创建误差文件
            FileStream outStream = new FileStream("Error.txt", FileMode.Create);
            outStream.Close();
            // 打开文件
            outStream = new FileStream("Error.txt", FileMode.Append);
            // 包装为writer
            StreamWriter writer = new StreamWriter(outStream);
            // 输入矩阵数组
            Matrix[] input = Input.GetColumns();
            // 输出矩阵数组
            Matrix[] output = Output.GetColumns();
            // 误差矩阵
            Matrix errMat;
            // 结果矩阵
            Matrix result;
            // 训练times次
            while (times-- > 0)
            {
                // 用第i组数据训练
                for(int i = 0; i < Input.Column; i++)
                {
                    // 计算结果
                    result = GetResult(input[i]);
                    // 计算误差
                    errMat = output[i] - result;
                    // 修正权重
                    FixWeights(input[i], errMat);
                    // 输出误差
                    Console.WriteLine("误差：\n" + errMat + "\n");
                    // 向文件写入数据
                    writer.WriteLine(errMat);
                }
            }
            // 关闭流
            writer.Close();
        }

        /// <summary>
        /// 设置神经元函数
        /// </summary>
        /// <param name="funHide">隐含层神经元函数</param>
        /// <param name="funOut">输出层神经元函数</param>
        public void SetFun(Function funHide, Function funOut)
        {
            IH.Fun += funHide;
            HO.Fun += funOut;
        }

    }
}