using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct DebugData
    {
        public readonly DebugProfiler Profiler;

        public DebugData(PacketBuffer buffer)
        {
            Profiler = new DebugProfiler(buffer);
        }
    }
}
