using System;
using System.IO;

namespace DroneLibrary.Protocol
{
    public struct PacketBeginOTA : IPacket
    {
        public PacketType Type => PacketType.BeginOTA;

        public string MD5;
        public int Size;

        public PacketBeginOTA(string md5, int size)
        {
            this.MD5 = md5;
            this.Size = size;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write(MD5);
            packet.Write(Size);
        }
    }
}