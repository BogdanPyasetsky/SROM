using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SROM
{
    class Num
    {
        public UInt64[] arr = new UInt64[0];
        
        public Num(string a)
        {
            var arr = new UInt64[(a.Length / 8) + 1];
            for (int i = 0; i < (a.Length / 8); i++)
            {
                arr[i] = Convert.ToUInt32(a.Substring(a.Length - 8 - i * 8, 8), 16);  // making substr and convert it into UInt32
            }
            if ((a.Length - (a.Length / 8) * 8) > 0)                                  // check for string multplicity
            {
                arr[a.Length / 8] = Convert.ToUInt32(a.Substring(0, a.Length - (a.Length / 8) * 8), 16);
                
            }
            
        }
            
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
            return G.TrimStart('0');
        }
    }
}
