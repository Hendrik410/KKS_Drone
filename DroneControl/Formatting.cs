using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl
{
    public static class Formatting
    {
        public static string FormatDecimal(double value, int places)
        {
            return FormatDecimal(value, places, 1);
        }
        public static string FormatDecimal(double value, int places, int integerPlaces)
        {
            return value.ToString("0." + new string('0', places)).PadLeft(places + 3 + (integerPlaces - 1));
        }

        public static string FormatRatio(double value)
        {
            return FormatDecimal(value, 2);
        }
    }
}
