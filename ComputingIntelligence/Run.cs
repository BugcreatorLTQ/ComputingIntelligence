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
            Genetic_Test();
            //Single_Linear();
            //BP_Curve();
            //BP_Iris();
        }

        static void Genetic_Test()
        {
            Genetic genetic = new Genetic(6, 0.5, 0.5, new TestDNA());
            genetic.Train(50);
        }

        /// <summary>
        /// 鸢尾花数据训练
        /// </summary>
        static void BP_Iris()
        {
            Matrix data = MatrixFileIO.ReadMatrixOfFile("irisData.txt", new IrisFilter());
            int rows = data.Row * 5 / 7;
            Matrix data_train = data.GetSubMatrix(0, rows).T;
            Matrix data_comp = data.GetSubMatrix(rows, data.Row).T;
            Matrix input = data_train.GetSubMatrix(0, data_train.Row - 3);
            Matrix output = data_train.GetSubMatrix(data_train.Row - 3, data_train.Row);
            BPNeuralNetwork bp = new BPNeuralNetwork(input, output, new TanSigFun(), new LinearFun(0.5f));
            bp.Train(500);
            input = data_comp.GetSubMatrix(0, data_comp.Row - 3);
            output = data_comp.GetSubMatrix(data_comp.Row - 3, data_comp.Row);
            bp.Comparison(input, output);
            ShowError();
        }

        /// <summary>
        /// BP神经网络-曲线拟合
        /// </summary>
        static void BP_Curve()
        {
            // 生成输入矩阵
            Matrix input = Matrix.Space(0,2,0.1f);
            // 读取输出矩阵
            Matrix output = MatrixFileIO.ReadMatrixOfFile("source.txt");
            Matrix In = input;
            Matrix Out = output;
            // 创建BP神经网络
            BPNeuralNetwork bp = new BPNeuralNetwork(In, Out,new TanSigFun(), new LinearFun(0.4f));
            // 训练网络
            bp.Train(2000);
            // 显示图像
            Matrix data = bp.GetResult(Matrix.Space(0, 2, 0.1f));
            Matrix temp = new Matrix(2, 1, new float[] { 1, 0 });
            data = temp * data;
            temp = new Matrix(2, 1, new float[] { 0, 1 });
            data += temp * output;
            Application.Run(new DrawForm(data));
        }

        /// <summary>
        /// 单层神经网络-线性拟合
        /// </summary>
        static void Single_Linear()
        {
            // 读取输入矩阵
            Matrix input = MatrixFileIO.ReadMatrixOfFile("input.txt");
            // 读取输出矩阵
            Matrix output = MatrixFileIO.ReadMatrixOfFile("output.txt");
            // 创建单层神经网络
            SingleNeuralNetwork network = new SingleNeuralNetwork(input, output, new LinearFun(0.25f));
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
            ShowError();
        }

        /// <summary>
        /// 测试模块
        /// </summary>
        static void Test()
        {
            Matrix matrix = Matrix.Space(-10, 10, 0.1f);
            matrix = new TanSigFun().function(matrix);
            Application.Run(new DrawForm(matrix));
        }

        /// <summary>
        /// 显示误差
        /// </summary>
        static void ShowError()
        {
            Application.Run(new DrawForm(MatrixFileIO.ReadMatrixOfFile("error.txt").T));
        }
    }
}