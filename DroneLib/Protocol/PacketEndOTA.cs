using System;
using System.IO;

namespace DroneLibrary.Protocol
{
    public struct PacketEndOTA : IPacket
    {
        public PacketType Type => PacketType.EndOTA;

        public bool Abort;

        public PacketEndOTA(bool abort)
        {
            this.Abort = abort;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write(Abort);
        }
    }
}