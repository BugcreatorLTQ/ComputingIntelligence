using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 隐含矩阵
        /// </summary>
        public Matrix Hide { set; get; }

        /// <summary>
        /// 输出矩阵
        /// </summary>
        public Matrix Output { set; get; }

        /// <summary>
        /// 创建一个BP神经网络
        /// </summary>
        /// <param name="Input">输入矩阵</param>
        /// <param name="Output">输出矩阵</param>
        public BPNeuralNetwork(Matrix input, Matrix output)
        {
            Input = input;
            Output = output;
            
        }

    }
}
