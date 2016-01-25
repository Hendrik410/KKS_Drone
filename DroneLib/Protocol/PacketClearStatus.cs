using System;
using System.IO;

namespace DroneLibrary.Protocol
{
    public struct PacketClearStatus : IPacket
    {
        public PacketType Type => PacketType.ClearStatus;

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));
        }
    }
}