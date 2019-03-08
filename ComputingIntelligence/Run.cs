using System;


namespace ComputingIntelligence
{
    /// <summary>
    /// 主运行类
    /// 运行主程序
    /// </summary>
    class Run
    {
        public static void Main(String[] args)
        {
            Matrix W = Matrix.GetRandomMatrix(4, 5);
            Console.WriteLine(W);
            Console.WriteLine(W.GetT());

        }
    }
}