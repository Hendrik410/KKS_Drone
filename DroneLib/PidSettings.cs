using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DroneLibrary
{
    [StructLayout(LayoutKind.Sequential, Size = 12, Pack = 0, CharSet = CharSet.Ansi)]
    [TypeConverter(typeof(DroneSettingsTypeConverter))]
    public class PidSettings 
    {
        public float Kp;
        public float Ki;
        public float Kd;

        public override string ToString()
        {
            return string.Format("Kp: {0:0.00}, Ki: {1:0.00}, Kd: {2:0.00}", Kp, Ki, Kd);
        }
    }
}
