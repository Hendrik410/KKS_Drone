using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class PidSettings : IEquatable<PidSettings>
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;

            if (obj is PidSettings)
                return Equals(obj as PidSettings);
            return false;
        }

        public bool Equals(PidSettings other)
        {
            return Kp == other.Kp && Ki == other.Ki && Kd == other.Kd;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = hash * 7 + Kp.GetHashCode();
                hash = hash * 7 + Ki.GetHashCode();
                hash = hash * 7 + Kd.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("Kp: {0:0.00}, Ki: {1:0.00}, Kd: {2:0.00}", Kp, Ki, Kd);
        }
    }
}
