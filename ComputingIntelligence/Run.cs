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
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Matrix fun(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);
            for(int i = 0; i < result.Data.Length; i++)
            {
                result.Data[i] = result.Data[i] > 0.5f ? 1 : 0;
            }
            return result;
        }

        public static void Main(string[] args)
        {
            // 读取输入矩阵
            Matrix input = MatrixFileIO.ReadMatrixOfFile("input.txt");
            // 读取输出矩阵
            Matrix output = MatrixFileIO.ReadMatrixOfFile("output.txt");
            // 创建单层神经网络
            SingleNeuralNetwork network = new SingleNeuralNetwork(input, output);
            // 添加函数委托
            network.Fun += fun;
            // 训练
            network.Training(100);
            // 保存权重矩阵
            MatrixFileIO.WriteMatrixToFile("Weights.txt", network.Weights);
        }
    }
}