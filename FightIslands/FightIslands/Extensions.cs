using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightIslands
{
    public static class Extensions
    {
        public static byte[] SubArray(this byte[] array, int offset, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }

        public static byte ToByte(this object obj)
        {
            byte wartosc = 0;
            Byte.TryParse(obj.ToString(), out wartosc);
            return wartosc;
        }

        public static int ToInt(this object obj)
        {
            int wartosc = 0;
            if (obj is decimal) return (int)(decimal)obj;
            Int32.TryParse(obj.ToString(), out wartosc);
            return wartosc;
        }

        public static float ToFloat(this object obj)
        {
            float wartosc = 0;
            var valueStr = obj.ToString().Replace('.', ',');
            float.TryParse(valueStr, out wartosc);
            return wartosc;
        }
    }
}
