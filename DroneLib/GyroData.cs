using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary {
    public struct GyroData : IEquatable<GyroData> {

        public float Pitch {
            get;
        }

        public float Roll {
            get;
        }

        public float Yaw {
            get;
        }

        public GyroData(float pitch, float roll, float yaw) {
            this.Pitch = pitch;
            this.Roll = roll;
            this.Yaw = yaw;
        }

        public static bool operator ==(GyroData a, GyroData b) {
            return a.Equals(b);
        }

        public static bool operator !=(GyroData a, GyroData b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            if(obj is GyroData)
                return Equals((GyroData)obj);
            return false;
        }

        public bool Equals(GyroData other) {
            return Math.Abs(Roll - other.Roll) < 0.1f
                   && Math.Abs(Pitch - other.Pitch) < 0.1f
                   && Math.Abs(Yaw - other.Yaw) < 0.1f;
        }

        public override int GetHashCode() {
            unchecked {
                int hash = 13;
                hash = hash * 7 + Pitch.GetHashCode();
                hash = hash * 7 + Roll.GetHashCode();
                hash = hash * 7 + Yaw.GetHashCode();
                return hash;
            }
        }
    }
}
