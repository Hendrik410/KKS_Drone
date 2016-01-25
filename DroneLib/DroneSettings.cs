using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct DroneSettings : IEquatable<DroneSettings>
    {
        [Category("Drone")]
        [Description("Name der Drone")]
        public string DroneName { get; set; }

        [Category("Network")]
        [Description("SSID des WiFi Netzwerks")]
        public string NetworkSSID { get; set; }

        [Category("Network")]
        [Description("Passwort des WiFi Netzwerks")]
        public string NetworkPassword { get; set; }

        [Category("Debug")]
        [Description("Ob Log-Nachrichten auf die serielle Schnittstelle geschrieben werden sollen.")]
        public bool VerboseSerialLog { get; set; }

        [Category("Flying")]
        public float Degree2Ratio { get; set; }

        [Category("Flying")]
        public float RotaryDegree2Ratio { get; set; }

        [Category("Engine")]
        public ushort PhysicsCalcDelay { get; set; }

        [Category("PID")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PidSettings PitchPid { get; set; }

        [Category("PID")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PidSettings RollPid { get; set; }

        [Category("PID")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public PidSettings YawPid { get; set; }


        public DroneSettings(string name, PacketBuffer buffer)
            : this()
        {
            this.DroneName = name;
            this.NetworkSSID = buffer.ReadString();
            this.NetworkPassword = buffer.ReadString();
            this.VerboseSerialLog = buffer.ReadBoolean();
            this.Degree2Ratio = buffer.ReadFloat();
            this.RotaryDegree2Ratio = buffer.ReadFloat();
            this.PhysicsCalcDelay = buffer.ReadUShort();

            this.PitchPid = new PidSettings(buffer);
            this.RollPid = new PidSettings(buffer);
            this.YawPid = new PidSettings(buffer);
        }

        public void Write(PacketBuffer buffer)
        {
            buffer.Write(DroneName);
            buffer.Write(NetworkSSID);
            buffer.Write(NetworkPassword);
            buffer.Write(VerboseSerialLog);
            buffer.Write(Degree2Ratio);
            buffer.Write(RotaryDegree2Ratio);
            buffer.Write(PhysicsCalcDelay);

            PitchPid.Write(buffer);
            RollPid.Write(buffer);
            YawPid.Write(buffer);
        }

        public static bool operator ==(DroneSettings a, DroneSettings b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DroneSettings a, DroneSettings b)
        {
            return !(a == b);
        }


        public override bool Equals(object obj)
        {
            if (obj is DroneSettings)
                return Equals((DroneSettings)obj);
            return false;
        }

        public bool Equals(DroneSettings other)
        {
            return DroneName == other.DroneName
                   && NetworkSSID == other.NetworkSSID
                   && NetworkPassword == other.NetworkPassword
                   && VerboseSerialLog == other.VerboseSerialLog
                   && Degree2Ratio == other.Degree2Ratio
                   && RotaryDegree2Ratio == other.RotaryDegree2Ratio
                   && PhysicsCalcDelay == other.PhysicsCalcDelay
                   && PitchPid.Equals(other.PitchPid)
                   && RollPid.Equals(other.RollPid)
                   && YawPid.Equals(other.YawPid);

        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = hash * 7 + (DroneName == null ? 0 : DroneName.GetHashCode());
                hash = hash * 7 + (NetworkSSID == null ? 0 : NetworkSSID.GetHashCode());
                hash = hash * 7 + (NetworkPassword == null ? 0 : NetworkPassword.GetHashCode());
                hash = hash * 7 + VerboseSerialLog.GetHashCode();
                hash = hash * 7 + Degree2Ratio.GetHashCode();
                hash = hash * 7 + RotaryDegree2Ratio.GetHashCode();
                hash = hash * 7 + PhysicsCalcDelay.GetHashCode();
                hash = hash * 7 + PitchPid.GetHashCode();
                hash = hash * 7 + RollPid.GetHashCode();
                hash = hash * 7 + YawPid.GetHashCode();
                return hash;
            }
        }
    }
}
