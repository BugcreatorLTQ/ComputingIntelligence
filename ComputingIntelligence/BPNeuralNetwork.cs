using System;
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
        /// <param name="funHide">隐含层响应函数</param>
        /// <param name="funOut">输出层响应函数</param>
        public BPNeuralNetwork(Matrix input, Matrix output, NetworkFunction funHide, NetworkFunction funOut)
        {
            Input = input;
            Output = output;
            // 构建隐含层 
            Matrix Hide = new Matrix((int)Math.Sqrt(Input.Row * Output.Row) + 3, 1);
            // 构建输入-隐含网络
            IH = new SingleNeuralNetwork(Input, Hide, funHide);
            // 构建隐含-输入网络
            HO = new SingleNeuralNetwork(Hide, Output, funOut);
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
        /// <param name="output">输出矩阵</param>
        /// <param name="error">误差矩阵</param>
        [Obsolete("该方法已被弃用")]
        public void FixWeights(Matrix input, Matrix output, Matrix error)
        {
            // 计算输入-隐含层输出
            Matrix IHOut = IH.GetResult(input);
            // 计算隐含层误差
            Matrix errHide = HO.Weights.T * error;
            // 修正IH网络
            HO.FixWeights(IHOut, error);
            // 修正IO网络
            IH.FixWeights(input, errHide);
        }

        /// <summary>
        /// 训练网络
        /// </summary>
        /// <param name="times">训练次数</param>
        public void Train(int times)
        {
            // 用于创建误差文件
            FileStream outStream = new FileStream("error.txt", FileMode.Create);
            outStream.Close();
            // 打开文件
            outStream = new FileStream("error.txt", FileMode.Append);
            // 包装为writer
            StreamWriter writer = new StreamWriter(outStream);
            // 输入矩阵数组
            Matrix[] input = Input.GetColumns();
            // 输出矩阵数组
            Matrix[] output = Output.GetColumns();
            // 误差矩阵
            Matrix errMat = null;
            // 训练times次
            while (times-- > 0)
            {
                // 用第i组数据训练
                for (int i = 0; i < Input.Column; i++)
                {
                    // 计算IH层输出
                    Matrix IHout = IH.GetResult(input[i]);
                    // 计算HO层输出
                    Matrix HOout = HO.GetResult(IHout);
                    // 计算输出层误差
                    errMat = output[i] - HOout;
                    // 计算输出层误差效能
                    errMat = errMat.X(HO.Fun.func(HOout));
                    // 计算隐含层误差效能
                    Matrix errHide = HO.Weights.T * errMat;
                    errHide = errHide.X(IH.Fun.func(IHout));
                    // 修正HO网络
                    HO.FixWeights(IHout, errMat);
                    // 修正IH网络
                    IH.FixWeights(input[i], errHide);
                }
                // 输出误差
                Console.WriteLine("误差：\n" + errMat);
                // 向文件写入数据
                writer.Write(errMat.T);
            }
            // 关闭流
            writer.Close();
        }

        /// <summary>
        /// 检验训练数据
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <param name="output">输出数据</param>
        public void Comparison(Matrix input,Matrix output)
        {
            Console.WriteLine("检验开始");
            Console.WriteLine(output - GetResult(input));
            Console.WriteLine("检验结束");
        }
    }
}
