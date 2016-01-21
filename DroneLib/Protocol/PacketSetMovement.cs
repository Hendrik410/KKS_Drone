using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneLibrary;
using DroneLibrary.Protocol;

namespace DroneLibrary.Protocol
{
    public struct PacketSetMovement : IPacket
    {

        public readonly bool Hover;
        public readonly float Pitch, Roll, Yaw, Thrust;

        public PacketType Type => PacketType.Movement;

        /// <summary>
        /// Erstellt ein neues Packet zur Übermittlung von BewegungsDaten
        /// </summary>
        /// <param name="pitch">Die Neigung an der X-Achse in Grad</param>
        /// <param name="roll">Die Neigung an der Y-Achse in Grad</param>
        /// <param name="yaw">Die Drehung um die Z-Achse in Grad/Sekunde</param>
        /// <param name="thrust">Der Schub entlang der Z-Achse in von -1 bis 1</param>
        /// <param name="hover">Gibt an, ob alle anderen Werte ignoriert werden sollen und die Drohne ihre Position halten soll.</param>
        public PacketSetMovement(float pitch, float roll, float yaw, float thrust, bool hover)
        {
            Pitch = pitch;
            Roll = roll;
            Yaw = yaw;
            Thrust = thrust;
            Hover = hover;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write(Hover);
            packet.Write(Pitch);
            packet.Write(Roll);
            packet.Write(Yaw);
            packet.Write(Thrust);
        }
    }
}
