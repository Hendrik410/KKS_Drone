using System;
using System.IO;

namespace DroneLibrary.Protocol
{
    public struct PacketStop : IPacket
    {
        public PacketType Type
        {
            get { return PacketType.Stop; }
        }

        public void Write(BinaryWriter writer){
            if(writer == null)
                throw new ArgumentNullException("writer");

            writer.Write((byte)'S');
            writer.Write((byte)'T');
            writer.Write((byte)'O');
            writer.Write((byte)'P');
        }
    }
}