using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct DebugProfiler
    {
        public readonly Entry[] Entries;

        public DebugProfiler(PacketBuffer buffer)
        {
            uint count = buffer.ReadUInt();

            Entries = new Entry[count];
            for (int i = 0; i < count; i++)
                Entries[i] = new Entry(buffer);
        }

        public struct Entry
        {
            public readonly string Name;
            public readonly uint TimeMicros;
            public readonly uint TimeMaxMicros;

            public TimeSpan Time
            {
                get { return new TimeSpan(TimeMicros * 10); }
            }

            public TimeSpan TimeMax
            {
                get { return new TimeSpan(TimeMaxMicros * 10); }
            }

            public Entry(PacketBuffer buffer)
            {
                Name = buffer.ReadString();
                TimeMicros = buffer.ReadUInt();
                TimeMaxMicros = buffer.ReadUInt();
            }
        }
    }
}
