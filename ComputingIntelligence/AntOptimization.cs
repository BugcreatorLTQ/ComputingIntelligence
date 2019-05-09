using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{
    /// <summary>
    /// 蚁群算法
    /// </summary>
    public class AntOptimization
    {
        /// <summary>
        /// 蚂蚁
        /// </summary>
        public class Ant
        {
            /// <summary>
            /// 访问数组
            /// </summary>
            private bool[] visit = null;

            /// <summary>
            /// 长度
            /// </summary>
            public int Length { get { return visit.Length; } }

            /// <summary>
            /// 索引器
            /// </summary>
            /// <param name="index">下标</param>
            /// <returns>值</returns>
            public bool this[int index]
            {
                get
                {
                    return visit[index];
                }
                set
                {
                    visit[index] = value;
                }
            }

            /// <summary>
            /// 设置蚂蚁长度
            /// </summary>
            /// <param name="length">长度</param>
            public void SetAnt(int length)
            {
                visit = new bool[length];
                for(int i = 0; i < length; i++)
                {
                    visit[i] = false;
                }
            }

            /// <summary>
            /// 选择道路
            /// </summary>
            /// <param name="way">道路权重</param>
            /// <param name="mession">信息素</param>
            public void ChanceWay(Matrix way, Matrix mession)
            {
                // 选择道路
                // 更新信息素
            }
        }

        /// <summary>
        /// 蚂蚁个数
        /// </summary>
        public Ant[] Ants { get; set; }

        /// <summary>
        /// 路径权值矩阵
        /// </summary>
        public Matrix Way { get; set; }

        /// <summary>
        /// 信息素矩阵
        /// </summary>
        public Matrix Message { get; set; }

        /// <summary>
        /// 构造一个蚁群
        /// </summary>
        /// <param name="count">蚂蚁个数</param>
        /// <param name="way">路径权重</param>
        public AntOptimization(int count, Matrix way)
        {
            Ants = new Ant[count];
            for(int i = 0; i < count; i++)
            {
                Ants[i].SetAnt(way.Column);
            }
            Way = Matrix.Clone(way);
            Message = new Matrix(way.Row, way.Column);

        }

        /// <summary>
        /// 模拟
        /// </summary>
        /// <param name="times">模拟次数</param>
        public void Train(int times)
        {
            // 初始化信息素
            Message *= 0;
            // 模拟times次
            while (times-- > 0)
            {
                // 第i只蚂蚁选择道路
                foreach (Ant ant in Ants)
                {
                    ant.ChanceWay(Way, Message);

                }
            }
        }

    }
}
