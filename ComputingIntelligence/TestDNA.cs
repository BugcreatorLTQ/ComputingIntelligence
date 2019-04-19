using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{
    class TestDNA : DNA
    {

        class DataA : DNAData
        {

            public int Data { get; set; }

            public override void Copy(DNAData src)
            {
                Data = (src as DataA).Data;
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
                return new DataA { Data = random.Next(10) };
            }
        }

        class DataB : DNAData
        {

            public bool Data { get; set; }

            public override void Copy(DNAData src)
            {
                Data = (src as DataB).Data;
            }

            public override string ToCode()
            {
                return Data ? "1" : "0";
            }

            public override void Variation()
            {
                Data = random.Next(2) == 1;
            }

            protected override DNAData GetRandomData()
            {
                return new DataB { Data = random.Next(2) == 1 };
            }
        }

        protected override DNA GetRandomDNA()
        {
            TestDNA temp = new TestDNA();
            temp.AddRandomDNA(3, new DataA());
            temp.AddRandomDNA(5, new DataB());
            return temp;
        }

        protected override string GetSolution()
        {
            StringBuilder builder = new StringBuilder();
            foreach(DNAData x in Data)
            {
                builder.Append(x.ToCode() + ".");
            }
            return builder.ToString();
        }

        protected override double GetValue()
        {
            double sum = 0;
            foreach (DNAData x in Data)
            {
                if (x is DataA)
                    sum += (x as DataA).Data;
                if (x is DataB)
                    sum += (x as DataB).Data ? 1 : 0;
            }
            return sum;
        }
    }
}
