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
            string ds;
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
            ds = Calc.LongMul(Num.ReConv(d), Num.ReConv(aTemp));
            d = Num.Conv(ds);
            return d;
        }

        public static string GCD(string hex1, string hex2)
        {
            var a = Num.Conv(hex1);
            var b = Num.Conv(hex2);
            var c = GCDInternal(a, b);
            return Num.ReConv(c);
        }

        static UInt64[] ModInternal(UInt64[] a, UInt64[] b)
        {
            var k = Calc.BitLength(b);
            var R = a;
            string RS, QS;
            UInt64[] Q = new UInt64[a.Length];
            UInt64[] T = new UInt64[a.Length];
            T[0] = 0x1;
            UInt64[] C;
            while (Calc.LongCmp(Num.ReConv(R), Num.ReConv(b)) >= 0)
            {
                var t = Calc.BitLength(R);
                C = Calc.LongShiftBitsToHigh(b, t - k);
                if (Calc.LongCmp(Num.ReConv(R), Num.ReConv(C)) == -1)
                {
                    t = t - 1;
                    C = Calc.LongShiftBitsToHigh(b, t - k);
                }
                RS = Calc.LongSub(Num.ReConv(R), Num.ReConv(C));
                R = Num.Conv(RS);
                QS = Calc.LongAdd(Num.ReConv(Q), Num.ReConv(Calc.LongShiftBitsToHigh(T, t - k)));
                Q = Num.Conv(QS);
            }
            return R;
        }

        public static string Mod(string hex1, string hex2)
        {
            if (Calc.LongCmp(hex1, hex2) == 0)
                return "0";
            if (Calc.LongCmp(hex1, hex2) == -1)
                return hex1;
            else 
            {
                var a = Num.Conv(hex1);
                var b = Num.Conv(hex2);
                var c = ModInternal(a, b);
                return Num.ReConv(c);
            }
        }

        public static string LongModAdd(string hex1, string hex2, string hex3)
        {
            var hex1_ = Mod(hex1, hex3);
            var hex2_ = Mod(hex2, hex3);
            var res = Calc.LongAdd(hex1_, hex2_);
            return Mod(res, hex3);
        }

        public static string LongModSub(string hex1, string hex2, string hex3)
        {
            var res = Calc.LongSub(hex1, hex2);
            return Mod(res, hex3);
        }

        public static string LongModMul(string hex1, string hex2, string hex3)
        {
            var hex1_ = Mod(hex1, hex3);
            var hex2_ = Mod(hex2, hex3);
            var res = Calc.LongMul(hex1_, hex2_);
            return Mod(res, hex3);
        }


        static UInt64[] LongWPowInernal(UInt64[] a, UInt64[] b, string hex3)
        {
            var C = Num.Conv("1");
            string dTemp, cTemp;
            UInt64[][] D = new UInt64[16][];
            D[0] = Num.Conv("1");
            D[1] = a;
            for (int i = 2; i < 16; i++)
            {
                dTemp = Calc.LongMul(Num.ReConv(D[i - 1]), Num.ReConv(a));
                //dTemp = Mod(dTemp, hex3);
                D[i] = Num.Conv(dTemp);
                D[i] = Calc.RemoveHighZeros(D[i]);
            }
            string B = Num.ReConv(b);
            for (int i = 0; i < B.Length; i++)
            {
                cTemp = Calc.LongMul(Num.ReConv(C), Num.ReConv(D[Num.SymbToInt(B[i])]));
                cTemp = Mod(cTemp, hex3);
                if (i != (B.Length - 1))
                    for (int k = 1; k <= 4; k++)
                    {
                        cTemp = Calc.LongMul(cTemp, cTemp);
                        cTemp = Mod(cTemp, hex3);
                    }
                C = Num.Conv(cTemp);
            }
            return C;
        }
      
        public static string LongModWPow(string hex1, string hex2, string hex3)
        {
            var hex1_ = Mod(hex1, hex3);
            var a = Num.Conv(hex1_);
            var b = Num.Conv(hex2);
            var res = LongWPowInernal(a, b, hex3);
            var res_ = Num.ReConv(res);
            return Mod(res_, hex3);
        }
    }
}
