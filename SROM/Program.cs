using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SROM
{
    class Program
    {
        static void Main(string[] args)
        {

            var a = Num.Conv("4B");
            var b = Num.Conv("21");
            var c = Calc.LongWPow("B", "21");/*
            int t;
            string C = "D8A3";
            for( int i= C.Length -1;i>= 0;i--)
            {
                t = Num.SymbToInt(C[i]);
                Console.WriteLine(t);
            }*/

            Console.WriteLine(c);

            Console.ReadLine();

        }
    }
}
