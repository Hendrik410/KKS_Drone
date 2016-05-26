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
        public readonly float Pitch, Roll, RotationalSpeed;
        public readonly int Thrust;

        public PacketType Type => PacketType.Movement;

        /// <summary>
        /// Erstellt ein neues Packet zur Übermittlung von BewegungsDaten
        /// </summary>
        /// <param name="pitch">Die Neigung an der X-Achse in Grad</param>
        /// <param name="roll">Die Neigung an der Y-Achse in Grad</param>
        /// <param name="yaw">Die Drehung um die Z-Achse in Grad/Sekunde</param>
        /// <param name="thrust">Der Schub</param>
        public PacketSetMovement(float pitch, float roll, float rotationalSpeed, int thrust)
        {
            Pitch = pitch;
            Roll = roll;
            RotationalSpeed = rotationalSpeed;
            Thrust = thrust;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            packet.Write(Pitch);
            packet.Write(Roll);
            packet.Write(RotationalSpeed);
            packet.Write(Thrust);
        }
    }
}
