using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct PidSettings
    {
        public float Kp { get; set; }
        public float Ki { get; set; }
        public float Kd { get; set; }

        public PidSettings(PacketBuffer buffer)
        {
            Kp = buffer.ReadFloat();
            Ki = buffer.ReadFloat();
            Kd = buffer.ReadFloat();
        }

        public void Write(PacketBuffer buffer)
        {
            buffer.Write(Kp);
            buffer.Write(Ki);
            buffer.Write(Kd);
        }
    }
}
