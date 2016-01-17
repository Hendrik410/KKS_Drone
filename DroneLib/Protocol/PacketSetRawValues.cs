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

        public PacketType Type => PacketType.SetRawValues;

        public PacketSetRawValues(ushort fl, ushort fr, ushort bl, ushort br) {
            this.Values = new QuadMotorValues(fl, fr, bl, br);
        }

        public PacketSetRawValues(QuadMotorValues values) {
            this.Values = values;
        }

        public void Write(BinaryWriter writer) {
            if(writer == null)
                throw new ArgumentNullException(nameof(writer));

            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Values.FrontLeft) : Values.FrontLeft);
            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Values.FrontRight) : Values.FrontRight);
            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Values.BackLeft) : Values.BackLeft);
            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Values.BackRight) : Values.BackRight);
        }
    }
}
