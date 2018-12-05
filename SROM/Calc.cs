using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SROM
{
    class Calc
    {
        
        static UInt64[] LongAddInternal(UInt64[] a, UInt64[] b)
        {

            UInt64[] C = new UInt64[Math.Max(a.Length, b.Length) + 1];
            UInt64 temp, carry = 0;
            for (int i = 0; i < Math.Min(a.Length, b.Length); i++)
            {
                temp = a[i] + b[i] + carry;
                carry = temp >> 32;
                C[i] = temp & 0xFFFFFFFF;
            }
            return C;
        }

        public static string LongAdd(string hex1, string hex2)
        {
            string hex1_, hex2_;
            Num.LengthControle(hex1, hex2, out hex1_, out hex2_);
            var a = Num.Conv(hex1_);
            var b = Num.Conv(hex2_);
            var C = LongAddInternal(a, b);
            return Num.ReConv(C);
        }

        public static int LongCmpInternal(UInt64[] a, UInt64[] b)
        {
            int i = a.Length - 1;
            while (a[i] == b[i])
            {
                i--;
                if (i == -1)
                    return 0;
            }
            if (a[i] > b[i])
                return 1;
            else
                return -1;
        }

        public static int LongCmp(string hex1, string hex2)
        {
            string hex1_, hex2_;
            Num.LengthControle(hex1, hex2, out hex1_, out hex2_);
            var a = Num.Conv(hex1_);
            var b = Num.Conv(hex2_);
            var C = LongCmpInternal(a, b);
            return C;
        }

        public static UInt64[] LongSubInternal(UInt64[] a, UInt64[] b)
        {
            UInt64[] C = new UInt64[a.Length];
            UInt64 temp, borrow = 0;
            for (int i = 0; i < a.Length; i++)
            {
                temp = a[i] - b[i] - borrow;
                if (temp <= a[i])
                {
                    C[i] = temp;
                    borrow = 0;
                }
                else
                {
                    C[i] = temp & 0xFFFFFFFF;
                    borrow = 1;
                }
            }
            return C;
        }

        public static string LongSub(string hex1, string hex2)
        {
            string hex1_, hex2_;
            Num.LengthControle(hex1, hex2, out hex1_, out hex2_);
            var a = Num.Conv(hex1_);
            var b = Num.Conv(hex2_);
            var t = LongCmpInternal(a, b);
            if (t > 0)
            {
                var c = LongSubInternal(a, b);
                return Num.ReConv(c);
            }
            else
            {
                if (t == 0)
                    return "0";
                else
                    return "Negative number";
            }
        }

        static UInt64[] LongMulODInternal(UInt64[] a, UInt64 b)
        {
            UInt64 temp, carry = 0;
            UInt64[] C = new UInt64[a.Length + 1];
            for (int i = 0; i < a.Length; i++)
            {
                temp = a[i] * b + carry;
                carry = temp >> 32;
                C[i] = temp & 0xFFFFFFFF;
            }
            C[a.Length] = carry;
            return C;
        }

        static UInt64[] LongShiftDigitsToHigh(UInt64[] a, int j)
        {
            UInt64[] C = new UInt64[a.Length + j];
            for (int i = 0; i < a.Length; i++)
            {
                C[i + j] = a[i];
            }
            return C;
        }

        public static UInt64[] RemoveHighZeros(UInt64[] a)
        {
            int i = a.Length - 1;
            while (a[i] == 0)
                i--;
            UInt64[] res = new UInt64[i + 1];
            Array.Copy(a, res, i + 1);
            return res;
        }

        public static UInt64[] LongMulInternal(UInt64[] a, UInt64[] b)
        {
            UInt64[] C = new UInt64[(a.Length)];
            UInt64[] temp;
            for (int i = 0; i < b.Length; i++)
            {
                temp = LongMulODInternal(a, b[i]);
                temp = LongShiftDigitsToHigh(temp, i);
                C = LongAddInternal(C, temp);
            }
            
            return C;
        }

        public static string LongMul(string hex1, string hex2)
        {
            //string hex1_, hex2_;
            //Num.LengthControle(hex1, hex2, out hex1_, out hex2_);
            var a = Num.Conv(hex1);
            var b = Num.Conv(hex2);
            if ((hex2 == "0") || (hex1 == "0"))
            {
                return "0";
            }
            else
            {
                var c = LongMulInternal(a, b);
                return Num.ReConv(c);
            }
        }

        public static int BitLength(UInt64[] a)
        {
            int t = 0, i = a.Length - 1;
            while (a[i] == 0)
            {
                if (i < 0)
                    return 0;
                i--;
            }
            var n = a[i];
            while (n > 0)
            {
                t++;
                n = n >> 1;
            }
            t = t + 32 * i;
            return t;
        }

        public static UInt64[] LongShiftBitsToHigh(UInt64[] a, int b)
        {
            int t = b / 32;
            int s = b - t * 32;
            UInt64 n, carry = 0;
            UInt64[] C = new UInt64[a.Length + t + 1];
            for (int i = 0; i < a.Length; i++)
            {
                n = a[i];
                n = n << s;
                C[i + t] = (n & 0xFFFFFFFF) | carry;
                carry = (n & 0xFFFFFFFF00000000) >> 32;
            }
            C[a.Length + t] = carry;
            return C;
        }

        public static UInt64[] LongShiftBitsToLow(UInt64[] a, int b)
        {
            int t = b / 32;
            int s = b - t * 32;
            UInt64 n, nn, carry = 0;
            UInt64[] C = new UInt64[a.Length - t];
            for (int i = t; i < a.Length-1; i++)
            {
                n = a[i];
                nn = a[i + 1];
                n = n >> s;
                nn = nn << (64 - s);
                nn = nn >> (64 - s);
                carry = nn;
                C[i - t] = n | (carry << 32 - s);
            }
            C[a.Length - 1 - t] = a[a.Length - 1] >> s;
            return C;
        }

        static UInt64[] LongDivInternal(UInt64[] a, UInt64[] b, out UInt64[] r)
        {
            var k = BitLength(b);
            var R = a;
            UInt64[] Q = new UInt64[a.Length];
            UInt64[] T = new UInt64[a.Length];
            T[0] = 0x1;
            UInt64[] C;
            while (LongCmpInternal(R, b) >= 0)
            {
                var t = BitLength(R);
                C = LongShiftBitsToHigh(b, t - k);
                if (LongCmpInternal(R, C) == -1)
                {
                    t = t - 1;
                    C = LongShiftBitsToHigh(b, t - k);
                }
                R = LongSubInternal(R, C);
                Q = LongAddInternal(Q, LongShiftBitsToHigh(T, t - k));
            }
            r = R;
            return Q;  //whole part from division
        }

        public static string LongDiv(string hex1, string hex2, out string hex3)
        {
            string hex1_, hex2_;
            Num.LengthControle(hex1, hex2, out hex1_, out hex2_);
            var a = Num.Conv(hex1_);
            var b = Num.Conv(hex2_);
            //string hex3;
            if (hex2 == "0")
            {
                hex3 = "0";
                return "Division by 0";
            }
            else
            {
                UInt64[] r;
                var c = LongDivInternal(a, b, out r);
                hex3 = Num.ReConv(r);
                //if (hex3 == "")
                    //hex3 = "0";
                var c1 = Num.ReConv(c);
                //if (c1 == "")
                    //c1 = "0";
                return c1;
            }
        }

        static UInt64[] LongWPowInernal(UInt64[] a, UInt64[] b)
        {
            var C = Num.Conv("1");
            UInt64[][] D = new UInt64[16][];
            D[0] = Num.Conv("1");
            D[1] = a;
            for (int i = 2; i < 16; i++)
            {
                D[i] = LongMulInternal(D[i - 1], a);
                D[i] = RemoveHighZeros(D[i]);
            }
            string B = Num.ReConv(b);
            for(int i = 0; i < B.Length; i++)
            {
                C = LongMulInternal(C, D[Num.SymbToInt(B[i])]);
                if (i != (B.Length-1))
                    for (int k = 1; k <= 4; k++)
                        C = LongMulInternal(C, C);
            }
            return C;
        } 
         public static string LongWPow(string hex1, string hex2)
        {
            //string hex1_, hex2_;
            //Num.LengthControle(hex1, hex2, out hex1_, out hex2_);
            if (hex1 == "1")
                return "1";
            if (hex1 == "0")
                return "0";
            else
            {
                var a = Num.Conv(hex1);
                var b = Num.Conv(hex2);
                var c = LongWPowInernal(a, b);
                return Num.ReConv(c);
            }
            
        }
    }
}
