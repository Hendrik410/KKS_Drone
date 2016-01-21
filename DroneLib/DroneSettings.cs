using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class DroneSettings : IEquatable<DroneSettings>
    {
        public string DroneName { get; set; }
        public string NetworkSSID { get; set; }
        public string NetworkPassword { get; set; }
        public bool VerboseSerialLog { get; set; }
        
        public DroneSettings(string droneName, string networkSSID, string networkPassword, bool verboseSerialLog)
        {
            this.DroneName = droneName;
            this.NetworkSSID = networkSSID;
            this.NetworkPassword = networkPassword;
            this.VerboseSerialLog = verboseSerialLog;
        }

        public static bool operator ==(DroneSettings a, DroneSettings b)
        {
            if (object.ReferenceEquals(a, b))
                return true;
            if (object.ReferenceEquals(a, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(DroneSettings a, DroneSettings b)
        {
            return !(a == b);
        }


        public override bool Equals(object obj)
        {
            if (obj is DroneSettings)
                return Equals(obj as DroneSettings);
            return false;
        }

        public bool Equals(DroneSettings other)
        {
            if (other == null)
                return false;
            return DroneName == other.DroneName
                && NetworkSSID == other.NetworkSSID
                && NetworkPassword == other.NetworkPassword
                && VerboseSerialLog == other.VerboseSerialLog;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = hash * 7 + DroneName.GetHashCode();
                hash = hash * 7 + NetworkSSID.GetHashCode();
                hash = hash * 7 + NetworkPassword.GetHashCode();
                hash = hash * 7 + VerboseSerialLog.GetHashCode();
                return hash;
            }
        }
    }
}
