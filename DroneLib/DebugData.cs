using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct DebugData
    {
        public readonly MotorRatios Real;
        public readonly MotorRatios Correction;
        public readonly DebugProfiler Profiler;

        public DebugData(PacketBuffer buffer)
        {
            Real = new MotorRatios(buffer);
            Correction = new MotorRatios(buffer);
            Profiler = new DebugProfiler(buffer);
        }
    }
}
