using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MySQLite
{
    public static class DateTimeExt
    {
        public static int GetSeconds(this DateTime dt)
        {
            return (int)(dt - new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public static int GetDays(this DateTime dt)
        {
            return (int)(dt - new DateTime(1970, 1, 1)).TotalDays;
        }
        public static DateTime ToDateTime(this long totalSeconds)
        {
            try
            {
                return new DateTime(1970, 1, 1).AddSeconds(totalSeconds);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static DateTime ToDateTime(this int totalDays)
        {
            try
            {
                return new DateTime(1970, 1, 1).AddDays(totalDays);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
