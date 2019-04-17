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
            Matrix input = Matrix.Space(0,2,0.1f);
            // 读取输入矩阵
            Matrix output = MatrixFileIO.ReadMatrixOfFile("source.txt");
            Matrix In = input.T.GetSubMatrix(0, 15).T;
            Matrix Out = output.T.GetSubMatrix(0, 15).T;
            Matrix C_In = input.T.GetSubMatrix(15, 21).T;
            Matrix C_Out = output.T.GetSubMatrix(15, 21).T;
            // 创建BP神经网络
            BPNeuralNetwork bp = new BPNeuralNetwork(In, Out,new TanSigFun(), new LinearFun());
            // 训练网络
            bp.Train(5);
            // 检验结果
            bp.Comparison(C_In, C_Out);
            // 显示误差
            //Application.Run(new DrawForm(MatrixFileIO.ReadMatrixOfFile("error.txt")));
            // 显示图像
            Application.Run(new DrawForm(bp.GetResult(Matrix.Space(0, 30, 0.1f)).T));
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
            SingleNeuralNetwork network = new SingleNeuralNetwork(input, output, new LinearFun(0.3f));
            // 训练
            network.Train(250);
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
            Console.WriteLine(Matrix.Space(0f, 2f, 0.1f));
            Console.WriteLine(Matrix.Space(0f, 2f, 20));
        }

    }
}