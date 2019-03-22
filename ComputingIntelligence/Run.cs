using System;
using System.Windows.Forms;
using static ComputingIntelligence.SingleNeuralNetwork;

namespace ComputingIntelligence
{
    /// <summary>
    /// 主运行类
    /// 运行主程序
    /// </summary>
    class Run
    {
        /// <summary>
        /// 运行主函数
        /// </summary>
        /// <param name="args">参数</param>
        public static void Main(string[] args)
        {
            Run.Start();
            //Run.Demo();
            //Run.Test();
        }

        /// <summary>
        /// 运行模块
        /// </summary>
        static void Start()
        {
            // 生成输入矩阵
            float[] data = new float[21];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 0.1f * i - 1;
            }
            Matrix input = new Matrix(1, data.Length, data);
            // 读取输入矩阵
            Matrix output = MatrixFileIO.ReadMatrixOfFile("source.txt");
            // 创建BP神经网络
            BPNeuralNetwork bp = new BPNeuralNetwork(input, output,new TanSigFun(), new LinearFun());
            // 训练网络
            bp.Train(200);
            // 显示误差
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
            SingleNeuralNetwork network = new SingleNeuralNetwork(input, output,new LinearFun());
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
        }

    }
}