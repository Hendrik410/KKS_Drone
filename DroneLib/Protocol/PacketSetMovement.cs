using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneLibrary;
using DroneLibrary.Protocol;

namespace DroneLibrary.Protocol {
    public struct PacketSetMovement : IPacket {

        public readonly short Pitch, Roll, Yaw, Thrust;

        public PacketType Type {
            get { return PacketType.Movement; }
        }

        /// <summary>
        /// Erstellt ein neues Packet zur Übermittlung von BewegungsDaten
        /// </summary>
        /// <param name="pitch">Die Neigung an der X-Achse (-900 bis 900) in Grad x10</param>
        /// <param name="roll">Die Neigung an der Y-Achse (-900 bis 900) in Grad x10</param>
        /// <param name="yaw">Die Drehung um die Z-Achse (-900 bis 900) in Grad x10</param>
        /// <param name="thrust">Der Schub entlang der Z-Achse (-1000 bis 1000) in mm/s</param>
        public PacketSetMovement(short pitch, short roll, short yaw, short thrust) {
            if(-900 > pitch || pitch > 900)
                throw new ArgumentOutOfRangeException("pitch");
            if(-900 > roll || roll > 900)
                throw new ArgumentOutOfRangeException("roll");
            if(-900 > yaw || yaw > 900)
                throw new ArgumentOutOfRangeException("yaw");
            if(-1000 > thrust || thrust > 1000)
                throw new ArgumentOutOfRangeException("thrust");

            Pitch = pitch;
            Roll = roll;
            Yaw = yaw;
            Thrust = thrust;
        }

        public void Write(BinaryWriter writer) {
            if(writer == null)
                throw new ArgumentNullException("writer");

            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Pitch) : Pitch);
            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Roll) : Roll);
            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Yaw) : Yaw);
            writer.Write(BitConverter.IsLittleEndian ? BinaryHelper.ReverseBytes(Thrust) : Thrust);
        }
    }
}
