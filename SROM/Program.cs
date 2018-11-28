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
            //var a = "AAAAAA";
            //var b = "DDDDDDDDDD";
            //var c = Calc.LongAdd(a, b);
            //string a1, b1;
            //Num.LengthControle(a, b, out a1, out b1);
            var a = Num.Conv("AAAAAA");
            var b = Num.Conv("0");
            b = a;


            Console.WriteLine(Num.ReConv(b));
            Console.ReadLine();

        }
    }
}
