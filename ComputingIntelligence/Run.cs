﻿using System;
using System.Windows.Forms;

namespace ComputingIntelligence
{
    /// <summary>
    /// 主运行类
    /// 运行主程序
    /// </summary>
    class Run
    {

        /// <summary>
        /// 阈值函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>矩阵元素大于0为1否则为0</returns>
        public static Matrix ThresholdFun(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);
            for(int i = 0; i < result.Data.Length; i++)
            {
                result.Data[i] = (matrix.Data[i] > 0) ? 1 : 0;
            }
            return result;
        }

        /// <summary>
        /// 线性函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>矩阵元素乘一个常系数</returns>
        public static Matrix LinearFun(Matrix matrix)
        {
            return matrix * 0.1f;
        }

        /// <summary>
        /// Logsig函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>计算结果矩阵</returns>
        public static Matrix LogSigFun(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);
            for(int i=0; i < result.Data.Length; i++)
            {
                float Fx = 1 / (1 + (float)Math.Pow(Math.E, (double)-matrix.Data[i]));
                result.Data[i] = Fx * (1 - Fx);
            }
            return result;
        }

        /// <summary>
        /// Tansig函数
        /// </summary>
        /// <param name="matrix">输入矩阵</param>
        /// <returns>计算结果矩阵</returns>
        public static Matrix TanSigFun(Matrix matrix)
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
        /// 运行主函数
        /// </summary>
        /// <param name="args">参数</param>
        public static void Main(string[] args)
        {
            //Run.Start();
            //Run.Demo();
            Run.Test();
        }

        /// <summary>
        /// 运行模块
        /// </summary>
        static void Start()
        {
            // 读取输入矩阵
            Matrix input = MatrixFileIO.ReadMatrixOfFile("input.txt");
            // 读取输出矩阵
            Matrix output = MatrixFileIO.ReadMatrixOfFile("output.txt");
            // 创建BP神经网络
            BPNeuralNetwork bp = new BPNeuralNetwork(input, output);
            // 设置神经元函数
            bp.SetFun(LogSigFun, LinearFun);
            bp.Train(200);
            Application.Run(new DrawForm(MatrixFileIO.ReadMatrixOfFile("error.txt")));
        }

        /// <summary>
        /// 演示模块
        /// </summary>
        static void Demo()
        {
            // 读取输入矩阵
            Matrix input = MatrixFileIO.ReadMatrixOfFile("input.txt");
            // 读取输出矩阵
            Matrix output = MatrixFileIO.ReadMatrixOfFile("output.txt");
            // 创建单层神经网络
            SingleNeuralNetwork network = new SingleNeuralNetwork(input, output);
            // 添加函数委托
            network.Fun += LinearFun;
            // 训练
            network.Train(200);
            // 保存权重矩阵 
            MatrixFileIO.WriteMatrixToFile("Weights.txt", network.Weights);
            // 保存阈值矩阵 
            MatrixFileIO.WriteMatrixToFile("Threshold.txt", network.Threshold);
            // 测试结果
            Matrix[] testInput = input.GetColumns();
            // 提示信息
            Console.WriteLine("测试输入：");
            for (int i = 0; i < input.Column; i++)
            {
                Console.WriteLine(network.GetResult(testInput[i]));
            }
            Application.Run(new DrawForm(MatrixFileIO.ReadMatrixOfFile("error.txt")));
        }

        /// <summary>
        /// 测试模块
        /// </summary>
        static void Test()
        {
            // 生成输入矩阵
            float[] data = new float[21];
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = 0.1f * i - 1;
            }
            Matrix input = new Matrix(1, data.Length, data);
            // 读取输入矩阵
            Matrix output = MatrixFileIO.ReadMatrixOfFile("source.txt");
            // 创建BP神经网络
            BPNeuralNetwork bp = new BPNeuralNetwork(input, output);
            // 设置神经元函数
            bp.SetFun(TanSigFun, LinearFun);
            // 训练网络
            bp.Train(200);
            // 显示误差
            Application.Run(new DrawForm(MatrixFileIO.ReadMatrixOfFile("error.txt")));
        }

    }
}