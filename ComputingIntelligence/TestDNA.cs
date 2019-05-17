using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{
    class TestDNA : DNA
    {

        class DecData : DNAData
        {

            public int Data { get; set; }

            public override void Copy(DNAData src)
            {
                Data = (src as DecData).Data;
            }

            public override string ToCode()
            {
                return Data.ToString();
            }

            public override void Variation()
            {
                Data = random.Next(10);
            }

            protected override DNAData GetRandomData()
            {
                return new DecData { Data = random.Next(10) };
            }
        }
        protected override DNA GetRandomDNA()
        {
            TestDNA temp = new TestDNA();
            temp.AddRandomDNA(7, new DecData());
            return temp;
        }

        protected override string GetSolution()
        {
            double x = 0;
            double step = 1;
            for (int i = 0; i < Data.Length; i++)
            {
                x += (Data[i] as DecData).Data * step;
                step *= 0.1;
            }
            double res = 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
            return "x: " + x + " result: " + res;
        }

        protected override double GetValue()
        {
            double sum = 0;
            double step = 1;
            for (int i = 0; i < Data.Length; i++)
            {
                sum += (Data[i] as DecData).Data * step;
                step *= 0.1;
            }
            sum = 10 * Math.Sin(5 * sum) + 7 * Math.Cos(4 * sum);
            return sum;
        }
    }
}
