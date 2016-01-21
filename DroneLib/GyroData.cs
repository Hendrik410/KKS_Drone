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

        public float AccelerationX { get; }
        public float AccelerationY { get; }
        public float AccelerationZ { get; }

        public float Temperature { get; }

        public GyroData(float pitch, float roll, float yaw, float accelerationX, float accelerationY, float accelerationZ, float temperature) {
            this.Pitch = pitch;
            this.Roll = roll;
            this.Yaw = yaw;
            this.AccelerationX = accelerationX;
            this.AccelerationY = accelerationY;
            this.AccelerationZ = accelerationZ;
            this.Temperature = temperature;
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
                   && Math.Abs(Yaw - other.Yaw) < 0.1f
                   && Math.Abs(AccelerationX - other.AccelerationX) < 0.1f
                   && Math.Abs(AccelerationY - other.AccelerationY) < 0.1f
                   && Math.Abs(AccelerationZ - other.AccelerationZ) < 0.1f
                   && Math.Abs(Temperature - other.Temperature) < 1f;
        }

        public override int GetHashCode() {
            unchecked {
                int hash = 13;
                hash = hash * 7 + Pitch.GetHashCode();
                hash = hash * 7 + Roll.GetHashCode();
                hash = hash * 7 + Yaw.GetHashCode();
                hash = hash * 7 + AccelerationX.GetHashCode();
                hash = hash * 7 + AccelerationY.GetHashCode();
                hash = hash * 7 + AccelerationZ.GetHashCode();
                hash = hash * 7 + Temperature.GetHashCode();
                return hash;
            }
        }
    }
}
