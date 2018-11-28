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

            var a = Num.Conv("3333");
            var b = Num.Conv("2");
            var c = Calc.LongWPow("3333", "2");

            Console.WriteLine(c);

            Console.ReadLine();

        }
    }
}
