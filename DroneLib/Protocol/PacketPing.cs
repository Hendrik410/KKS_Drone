using System;
using System.IO;

namespace DroneLibrary.Protocol
{
    public struct PacketPing : IPacket
    {
        public PacketType Type => PacketType.Ping;

        /// <summary>
        /// Gibt die Zeit zurück, an dem das Paket abgesendet wurde.
        /// </summary>
        public long Time;

        public PacketPing(long time)
        {
            this.Time = time;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write(Time);
        }
    }
}