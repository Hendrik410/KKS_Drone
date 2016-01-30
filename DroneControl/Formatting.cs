using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl
{
    public static class Formatting
    {
        public static string FormatDecimal(float value, int places)
        {
            return value.ToString("0." + new string('0', places)).PadLeft(places + 3);
        }

        public static string FormatRatio(float value)
        {
            return FormatDecimal(value, 2);
        }
    }
}
