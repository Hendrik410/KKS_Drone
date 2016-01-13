using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneLibrary;
using DroneLibrary.Protocol;

namespace DroneLibrary.Protocol {
    public struct PacketSetRawValues : IPacket {

        public readonly QuadMotorValues Values;
        public readonly bool IgnoreNotArmed;

        public PacketType Type => PacketType.SetRawValues;

        public PacketSetRawValues(ushort fl, ushort fr, ushort bl, ushort br, bool ignoreNotArmed) {
            this.Values = new QuadMotorValues(fl, fr, bl, br);
            this.IgnoreNotArmed = ignoreNotArmed;
        }

        public PacketSetRawValues(QuadMotorValues values, bool ignoreNotArmed) {
            this.Values = values;
            this.IgnoreNotArmed = ignoreNotArmed;
        }

        public void Write(BinaryWriter writer) {
            if(writer == null)
                throw new ArgumentNullException(nameof(writer));

            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Values.FrontLeft) : Values.FrontLeft);
            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Values.FrontRight) : Values.FrontRight);
            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Values.BackLeft) : Values.BackLeft);
            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Values.BackRight) : Values.BackRight);
            writer.Write((byte)(IgnoreNotArmed ? 1 : 0));
        }
    }
}
