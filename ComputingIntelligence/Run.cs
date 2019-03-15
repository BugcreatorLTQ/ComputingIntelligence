using System;


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
        /// <returns></returns>
        public static Matrix LogSigFun(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);
            for(int i=0; i < result.Data.Length; i++)
            {
                result.Data[i] = 1 / (1 + (float)Math.Pow(Math.E, (double)-matrix.Data[i]));
            }
            return result;
        }


        public static void Main(string[] args)
        {
            //Run.Test();
            Run.Start();
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
            bp.Training(50);
        }

        /// <summary>
        /// 测试模块
        /// </summary>
        static void Test()
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
            network.Training(200);
            // 保存权重矩阵 
            MatrixFileIO.WriteMatrixToFile("Weights.txt", network.Weights);
            // 测试结果
            Matrix[] testInput = input.GetDatas();
            for (int i = 0; i < input.Column; i++)
            {
                Console.Write(network.GetResult(testInput[i]) + "\n\n");
            }
        }


    }
}