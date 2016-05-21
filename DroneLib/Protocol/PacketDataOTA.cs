using System;
using System.IO;

namespace DroneLibrary.Protocol
{
    public struct PacketDataOTA : IPacket
    {
        public PacketType Type => PacketType.DataOTA;

        public int ChunkSize;
        public byte DataHash;
        public byte[] Data;

        public PacketDataOTA(int chunkSize, byte dataHash, byte[] data)
        {
            this.ChunkSize = chunkSize;
            this.DataHash = dataHash;
            this.Data = data;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write(ChunkSize);
            packet.Write(DataHash);
            packet.Write(Data, 0, Data.Length);
        }
    }
}