using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary {
    public struct DroneData : IEquatable<DroneData> {

        public bool IsArmed {
            get;
        }

        public QuadMotorValues MotorValues {
            get;
        }

        public GyroData Gyro {
            get;
        }

        public DroneData(bool isArmed, QuadMotorValues motorValues, GyroData gyro) {
            this.IsArmed = isArmed;
            this.MotorValues = motorValues;
            this.Gyro = gyro;
        }

        public static bool operator ==(DroneData a, DroneData b) {
            return a.Equals(b);
        }

        public static bool operator !=(DroneData a, DroneData b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            if(obj is DroneData)
                return Equals((DroneData)obj);
            return false;
        }

        public bool Equals(DroneData other) {
            return IsArmed == other.IsArmed
                   && MotorValues.Equals(other.MotorValues)
                   && Gyro.Equals(other.Gyro);
        }

        public override int GetHashCode() {
            unchecked {
                int hash = 13;
                hash = hash * 7 + IsArmed.GetHashCode();
                hash = hash * 7 + MotorValues.GetHashCode();
                hash = hash * 7 + Gyro.GetHashCode();
                return hash;
            }
        }
    }
}
