using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{
    /// <summary>
    /// 遗传算法
    /// </summary>
    class Genetic
    {
        /// <summary>
        /// 随机数
        /// </summary>
        private static readonly Random random = new Random();

        /// <summary>
        /// 群落大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 交叉概率
        /// </summary>
        public double Cross_e { get; set; }

        /// <summary>
        /// 变异概率
        /// </summary>
        public double Variation_e { get; set; }

        /// <summary>
        /// DNA集合
        /// </summary>
        public DNA[] DNAS { get; set; }

        /// <summary>
        /// 创建一个生物群落
        /// </summary>
        /// <param name="size">群落大小</param>
        /// <param name="c_e">交叉概率</param>
        /// <param name="v_e">变异概率</param>
        /// <param name="template">DNA模板</param>
        public Genetic(int size, double c_e, double v_e, DNA template)
        {
            Size = size;
            Cross_e = c_e;
            Variation_e = v_e;
            DNAS = new DNA[Size];
            for (int i = 0; i < Size; i++)
            {
                // 产生随机个体
                DNAS[i] = template.RandomDNA;
            }
        }

        /// <summary>
        /// 选择
        /// </summary>
        private void Selection()
        {
            DNA[] temp = new DNA[Size];
            for(int i = 0; i < Size; i++)
            {
                temp[i] = DNAS[i].RandomDNA;
            }
            int max = 0;
            // 采用随机锦标赛选取Size-1个个体
            for (int i = 1; i < Size; i++)
            {
                // 保留最优解
                if (DNAS[max].Value < DNAS[i].Value)
                {
                    max = i;
                }
                DNA A, B;
                A = DNAS[i];
                B = DNAS[random.Next(Size)];
                temp[i].Copy(A.Value > B.Value ? A : B);
            }
            temp[0].Copy(DNAS[max]);
            DNAS = temp;
        }

        /// <summary>
        /// 交叉
        /// </summary>
        private void Cross()
        {
            for (int i = 1; i < Size; i++)
            {
                // Cross_e的概率交叉
                if (random.NextDouble() < Cross_e)
                {
                    // 随机选取种群中另一个DNA
                    int other = random.Next(Size - 1) + 1;
                    // 交叉
                    DNAS[i].Cross(ref DNAS[other]);
                }
            }
        }

        /// <summary>
        /// 变异
        /// </summary>
        private void Variation()
        {
            for (int i = 1; i < Variation_e; i++)
            {
                // Variation_e的概率变异
                if (random.NextDouble() < Variation_e)
                {
                    // 产生变异
                    DNAS[i].Variation();
                }
            }
        }

        /// <summary>
        /// 模拟
        /// </summary>
        /// <param name="times">模拟次数</param>
        public void Train(int times)
        {
            while (times-- > 0)
            {
                // 交叉
                Cross();
                // 变异
                Variation();
                // 选择
                Selection();
                // 输出
                ShowDNA();
            }
            ShowSolution();
        }

        /// <summary>
        /// 显示DNA序列
        /// </summary>
        public void ShowDNA()
        {
            foreach (DNA x in DNAS)
            {
                Console.Write(x.DNACode + " ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 显示解决方案
        /// </summary>
        public void ShowSolution()
        {
            foreach (DNA x in DNAS)
            {
                Console.Write(x.Solution + " ");
            }
            Console.WriteLine();
        }


    }
}
