using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputingIntelligence
{
    class DNAT : DNA
    {

        class BitData : DNAData
        {
            public int Data { get; set; }

            public override void Copy(DNAData src)
            {
                Data = (src as BitData).Data;
            }

            public override string ToCode()
            {
                return Data.ToString();
            }

            public override void Variation()
            {
                Data = random.Next(2);
            }

            protected override DNAData GetRandomData()
            {
                return new BitData { Data = random.Next(2) };
            }
        }

        protected override DNA GetRandomDNA()
        {
            DNAT temp = new DNAT();
            temp.AddRandomDNA(21 + 18, new BitData());
            return temp;
        }

        protected override string GetSolution()
        {

            double A = 0, B = 0;
            for (int i = 0; i < 21; i++)
            {
                A += (Data[i] as BitData).Data << i;
            }
            A = A / (double)2097151 * 1510000 / 100000;
            for (int i = 21; i < 39; i++)
            {
                B += (Data[i] as BitData).Data << (i - 21);
            }
            B = B / (double)262143 * 170000 / 100000;
            A -= 3.0;
            B += 4.1;
            return "A :" + A + " B :" + B + " Result : " + (21.5 + A * Math.Sin(4 * Math.PI * A) + B * Math.Sin(20 * Math.PI * B)) + '\n';
        }

        protected override double GetValue()
        {
            double A = 0, B = 0;
            for(int i = 0; i < 21; i++)
            {
                A += (Data[i] as BitData).Data << i;
            }
            A = A / (double)2097151 * 1510000 / 100000;
            for(int i = 21; i < 39; i++)
            {
                B += (Data[i] as BitData).Data << (i - 21);
            }
            B = B / (double)262143 * 170000 / 100000;
            A -= 3.0;
            B += 4.1;
            return 21.5 + A * Math.Sin(4 * Math.PI * A) + B * Math.Sin(20 * Math.PI * B);
        }

    }
}
