using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            float[] data = { 1, 2, 3, 4, 5, 6, 7, 8 };

            Matrix A = new Matrix(2, 4, data);
            Matrix B = new Matrix(4, 2, data);
            Console.WriteLine(-A*B);

        }
    }
}
