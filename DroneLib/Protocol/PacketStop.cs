using System;
using System.IO;

namespace DroneLibrary.Protocol
{
    public struct PacketStop : IPacket
    {
        public PacketType Type => PacketType.Stop;

        public void Write(BinaryWriter writer){
            if(writer == null)
                throw new ArgumentNullException(nameof(writer));
        }
    }
}