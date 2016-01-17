using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol {
    public struct PacketArm : IPacket {

        public PacketType Type => PacketType.Arm;

        public readonly bool Arm;

        public PacketArm(bool arm) {
            this.Arm = arm;
        }

        public void Write(BinaryWriter writer) {
            if(writer == null)
                throw new ArgumentNullException(nameof(writer));

            writer.Write((byte)'A');
            writer.Write((byte)'R');
            writer.Write((byte)'M');
            writer.Write((byte)(Arm ? 1 : 0));
        }
    }
}
