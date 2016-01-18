using System;
using System.IO;

namespace DroneLibrary.Protocol
{
    public struct PacketStop : IPacket
    {
        public PacketType Type => PacketType.Stop;

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));
        }
    }
}