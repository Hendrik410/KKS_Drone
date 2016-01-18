using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol
{
    public struct PacketResetRevision : IPacket
    {

        public PacketType Type => PacketType.ResetRevision;

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));
        }
    }
}