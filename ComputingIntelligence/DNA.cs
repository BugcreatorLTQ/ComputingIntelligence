using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{
    abstract class DNA
    {

        /// <summary>
        /// 基础数据类
        /// </summary>
        public abstract class DNAData
        {
            /// <summary>
            /// 随机数
            /// </summary>
            protected static Random random = new Random();

            /// <summary>
            /// 获取随机值
            /// </summary>
            public DNAData RandomData { get => GetRandomData(); }

            /// <summary>
            /// 获取随机值
            /// </summary>
            /// <returns>随机值</returns>
            protected abstract DNAData GetRandomData();

            /// <summary>
            /// 变异
            /// </summary>
            public abstract void Variation();

            /// <summary>
            /// 转为DNA码
            /// </summary>
            /// <returns></returns>
            public abstract String ToCode();

            /// <summary>
            /// 复制DNA数据
            /// </summary>
            /// <param name="src">源数据</param>
            public abstract void Copy(DNAData src);

        }

        /// <summary>
        /// 随机数
        /// </summary>
        protected static Random random = new Random();

        /// <summary>
        /// DNA数据
        /// </summary>
        public DNAData[] Data { get; set; }

        /// <summary>
        /// 随机DNA
        /// </summary>
        public DNA RandomDNA { get => GetRandomDNA(); }

        /// <summary>
        /// 获取解决方案
        /// </summary>
        public String Solution { get => GetSolution(); }

        /// <summary>
        /// 获取DNA编码
        /// </summary>
        public String DNACode
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (DNAData x in Data)
                {
                    stringBuilder.Append(x.ToCode() + '-');
                }
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// 适应度值
        /// </summary>
        public double Value { get => GetValue(); }

        /// <summary>
        /// 获取随机DNA
        /// </summary>
        /// <returns>随机合法DNA</returns>
        protected abstract DNA GetRandomDNA();

        /// <summary>
        /// 添加改变DNA
        /// </summary>
        /// <param name="length">DNA长度</param>
        /// <param name="template">DNA数据模板</param>
        protected void AddRandomDNA(int length, DNAData template)
        {
            if (Data == null)
            {
                Data = new DNAData[0];
            }
            DNAData[] temp = new DNAData[Data.Length + length];
            for(int i = 0; i < Data.Length; i++)
            {
                temp[i] = Data[i];
            }
            for (int i = Data.Length; i < Data.Length + length; i++)
            {
                temp[i] = template.RandomData;
            }
            Data = temp;
        }

        /// <summary>
        /// 交叉
        /// </summary>
        public void Cross(ref DNA other)
        {
            int Length = Data.Length;
            // 随机选取交叉位点
            int index = random.Next(Length);
            for (int i = index; i < Length; i++)
            {
                DNAData temp = Data[i];
                Data[i] = other.Data[i];
                other.Data[i] = temp;
            }
        }

        /// <summary>
        /// 变异
        /// </summary>
        public void Variation()
        {
            int index = random.Next(Data.Length);
            Data[index].Variation();
        }

        /// <summary>
        /// 获取适应度值
        /// </summary>
        /// <returns>适应度值</returns>
        protected abstract double GetValue();

        /// <summary>
        /// 获取解决方案
        /// </summary>
        /// <returns></returns>
        protected abstract String GetSolution();

        /// <summary>
        /// 复制DNA
        /// </summary>
        /// <param name="src">源DNA</param>
        public void Copy(DNA src)
        {
            Data = new DNAData[src.Data.Length];
            for(int i = 0; i < Data.Length; i++)
            {
                Data[i] = src.Data[i].RandomData;
                Data[i].Copy(src.Data[i]);
            }
        }
    }

}
