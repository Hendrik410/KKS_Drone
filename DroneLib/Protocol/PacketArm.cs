using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol
{
    public struct PacketArm : IPacket
    {
        public PacketType Type => PacketType.Arm;

        public readonly bool Arm;

        public PacketArm(bool arm)
        {
            this.Arm = arm;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write((byte)'A');
            packet.Write((byte)'R');
            packet.Write((byte)'M');
            packet.Write(Arm);
        }
    }
}
