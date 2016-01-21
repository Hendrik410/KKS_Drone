using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct DroneSettings : IEquatable<DroneSettings>
    {
        public string DroneName { get; set; }
        public string NetworkSSID { get; set; }
        public string NetworkPassword { get; set; }
        public bool VerboseSerialLog { get; set; }
        public float Degree2Ratio { get; set; }
        public float RotaryDegree2Ratio { get; set; }


        public DroneSettings(string droneName, string networkSSID, string networkPassword, bool verboseSerialLog, float degree2Ratio, float rotaryDegree2Ratio)
            : this()
        {
            this.DroneName = droneName;
            this.NetworkSSID = networkSSID;
            this.NetworkPassword = networkPassword;
            this.VerboseSerialLog = verboseSerialLog;
            this.Degree2Ratio = degree2Ratio;
            this.RotaryDegree2Ratio = rotaryDegree2Ratio;
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
                   && RotaryDegree2Ratio == other.RotaryDegree2Ratio;

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
                return hash;
            }
        }
    }
}
