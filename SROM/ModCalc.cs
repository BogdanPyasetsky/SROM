using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SROM
{
    class ModCalc
    {

        static UInt64[] GCDInternal(UInt64[] a, UInt64[] b)
        {
            UInt64[] d = new UInt64[Math.Min(a.Length, b.Length)];
            d = Num.Conv("1");
            var aTemp = a;
            var bTemp = b;
            while (((aTemp[0] & 0x1) == 0) && ((bTemp[0] & 0x1) == 0))
            {
                aTemp = Calc.LongShiftBitsToLow(aTemp, 1);
                bTemp = Calc.LongShiftBitsToLow(bTemp, 1);
                d = Calc.LongShiftBitsToHigh(d, 1);
            }
            while ((aTemp[0] & 0x1) == 0)
                aTemp = Calc.LongShiftBitsToLow(aTemp, 1);
            while (Num.ReConv(bTemp) != "0")
            {
                while ((bTemp[0] & 0x1) == 0)
                    bTemp = Calc.LongShiftBitsToLow(bTemp, 1);
                var cmpVar = Calc.LongCmp(Num.ReConv(aTemp), Num.ReConv(bTemp));
                UInt64[] min, max;
                if (cmpVar >= 0)
                {
                    min = bTemp;
                    max = aTemp;
                }
                else
                {
                    min = aTemp;
                    max = bTemp;
                }
                aTemp = min;
                bTemp = Num.Conv(Calc.LongSub(Num.ReConv(max), Num.ReConv(min)));
            }
            d = Calc.LongMulInternal(d, aTemp);
            return d;
        }

        public static string GCD(string hex1, string hex2)
        {
            var a = Num.Conv(hex1);
            var b = Num.Conv(hex2);
            var c = GCDInternal(a, b);
            return Num.ReConv(c);
        }
    }
}
