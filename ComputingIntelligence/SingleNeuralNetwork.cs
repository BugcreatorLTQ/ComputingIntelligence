using System;
using System.IO;

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
        /// 定义神经元函数
        /// </summary>
        public NetworkFunction Fun { get; set; }

        /// <summary>
        /// 创建一个神经网络
        /// </summary>
        /// <param name="input">输入矩阵</param>
        /// <param name="output">输出矩阵</param>
        /// <param name="fun">响应函数</param>
        public SingleNeuralNetwork(Matrix input, Matrix output, NetworkFunction fun)
        {
            Input = input;
            Output = output;
            // 创建函数
            Fun = fun;
            // 生成权重矩阵
            Weights = Matrix.GetRandomMatrix(output.Row, input.Row);
            // 生成阈值矩阵
            Threshold = Matrix.GetRandomMatrix(output.Row, 1);
        }

        /// <summary>
        /// 修正权重矩阵
        /// </summary>
        /// <param name="input">输入矩阵</param>
        /// <param name="output">输出矩阵</param>
        /// <param name="error">计算误差</param>
        public void FixWeights(Matrix input, Matrix output, Matrix error)
        {
            // 计算误差效能
            error = error.X(Fun.func(output));
            // 修正权重矩阵
            Weights += 0.5f * error * input.T;
            // 修正阈值矩阵
            Threshold += 0.5f * error;
        }

        /// <summary>
        /// 训练网络
        /// </summary>
        /// <param name="times">训练次数</param>
        public void Train(int times)
        {
            // 用于创建误差文件
            FileStream outStream = new FileStream("Error.txt", FileMode.Create);
            outStream.Close();
            // 打开文件
            outStream = new FileStream("Error.txt", FileMode.Append);
            // 包装为writer
            StreamWriter writer = new StreamWriter(outStream);
            // 误差矩阵
            Matrix errMat = null;
            // 结果矩阵
            Matrix result = null;
            // 输入矩阵数组
            Matrix[] input = Input.GetColumns();
            // 输出矩阵数组
            Matrix[] output = Output.GetColumns();
            // 判断函数是否为空
            if (Fun == null)
            {
                throw new Exception("神经元函数为空");
            }
            // 训练times次
            while (times-- > 0)
            {
                // 用第i对输入输出进行训练
                for (int i = 0; i < Input.Column; i++)
                {
                    // 计算输出
                    result = GetResult(input[i]);
                    // 计算误差 (理想减输出)
                    errMat = output[i] - result;
                    // 修正权重
                    FixWeights(input[i], output[i], errMat);
                }
                // 向文件写入数据
                writer.Write(errMat.T);
                // 输出误差
                Console.WriteLine("误差：\n" + errMat + "\n");
            }
            // 关闭流
            writer.Close();
        }

        /// <summary>
        /// 获取计算结果
        /// </summary>
        /// <param name="input">输入矩阵</param>
        /// <returns>结果矩阵</returns>
        public Matrix GetResult(Matrix input)
        {
            // 权重×输入+阈值
            return Fun.function(Weights * input + Threshold);
        }

    }
}



