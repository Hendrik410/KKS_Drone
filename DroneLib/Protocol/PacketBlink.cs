using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol {
    public struct PacketBlink : IPacket
    {
        public PacketType Type => PacketType.Blink;

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));
        }
    }
}
