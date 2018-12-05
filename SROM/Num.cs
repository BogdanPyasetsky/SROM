using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SROM
{
    class Num
    { 
        public static void LengthControle(string hex1, string hex2, out string hex1_, out string hex2_)
        {
            hex1_ = hex1; hex2_ = hex2;
            if (hex1.Length > hex2.Length)
            {
                while (hex2.Length != hex1.Length)
                    hex2 = "0" + hex2;
                hex2_ = hex2;
            }
            else
            {
                while (hex1.Length != hex2.Length)
                    hex1 = "0" + hex1;
                hex1_ = hex1;
            }
        }
        
        public static UInt64[] Conv(string a)
        {
            var arr = new UInt64[(a.Length / 8) + 1];
            for (int i = 0; i < (a.Length / 8); i++)
            {
                arr[i] = Convert.ToUInt32(a.Substring(a.Length - 8 - i * 8, 8), 16);  // making substr and convert it into UInt32
            }
            if ((a.Length - (a.Length / 8) * 8) > 0)                                  // check for string multplicity
            {
                arr[a.Length / 8] = Convert.ToUInt32(a.Substring(0, a.Length - (a.Length / 8) * 8), 16);
                return arr;
            }
            else
            {
                return arr;
            }
        }

        public static string ReConv(UInt64[] a)
        {
            string g, G = "";
            for (int i = 0; i < a.Length; i++)
            {
                g = a[i].ToString("X");
                if (g.Length < 8)
                    do g = "0" + g;
                    while (g.Length < 8);
                G = g + G;
            }
            var r = G.TrimStart('0');
            return string.IsNullOrEmpty(r) ? "0" : r;
        }

        public static int SymbToInt(char a)
        {
            if (a == '0')
                return 0;
            if (a == '1')
                return 1;
            if (a == '2')
                return 2;
            if (a == '3')
                return 3;
            if (a == '4')
                return 4;
            if (a == '5')
                return 5;
            if (a == '6')
                return 6;
            if (a == '7')
                return 7;
            if (a == '8')
                return 8;
            if (a == '9')
                return 9;
            if (a == 'A')
                return 10;
            if (a == 'B')
                return 11;
            if (a == 'C')
                return 12;
            if (a == 'D')
                return 13;
            if (a == 'E')
                return 14;
            if (a == 'F')
                return 15;
            else
                return -1;
        }
    }
}
