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
            if (string.IsNullOrWhiteSpace(DroneName))
                throw new ArgumentException("DroneName is null or white space", nameof(DroneName));
            if (DroneName.Length > 30)
                throw new ArgumentException("DroneName is longer then 30 chars", nameof(DroneName));

            if (string.IsNullOrWhiteSpace(NetworkSSID))
                throw new ArgumentException("NetworkSSID is null or white space", nameof(NetworkSSID));
            if (NetworkSSID.Length > 30)
                throw new ArgumentException("NetworkSSID is longer then 30 chars", nameof(NetworkSSID));

            if (string.IsNullOrWhiteSpace(NetworkPassword))
                throw new ArgumentException("NetworkPassword is null or white space", nameof(NetworkPassword));
            if (NetworkPassword.Length > 30)
                throw new ArgumentException("NetworkPassword is longer then 30 chars", nameof(NetworkPassword));

            if (Degree2Ratio < 0 || Degree2Ratio > 1)
                throw new ArgumentOutOfRangeException(nameof(Degree2Ratio), Degree2Ratio, "Value must be in range 0 - 1");
            if (RotaryDegree2Ratio < 0 || RotaryDegree2Ratio > 1)
                throw new ArgumentOutOfRangeException(nameof(RotaryDegree2Ratio), RotaryDegree2Ratio, "Value must be in range 0 - 1");

            if (PhysicsCalcDelay < 0 || PhysicsCalcDelay > 100)
                throw new ArgumentOutOfRangeException(nameof(PhysicsCalcDelay), PhysicsCalcDelay, "Value must be in range 0 - 100");

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
                hash = hash * 7 + (PitchPid == null ? 0 : PitchPid.GetHashCode());
                hash = hash * 7 + (RollPid == null ? 0 : RollPid.GetHashCode());
                hash = hash * 7 + (YawPid == null ? 0 : YawPid.GetHashCode());
                return hash;
            }
        }
    }
}
