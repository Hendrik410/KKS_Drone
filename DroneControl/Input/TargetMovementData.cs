using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input {
    public struct TargetMovementData : IEquatable<TargetMovementData> {

        public float TargetPitch { get; private set; }
        public float TargetRoll { get; private set; }
        public float TargetRotationalSpeed { get; private set; }
        public float TargetThrust { get; private set; }

        public TargetMovementData(float targetPitch, float targetRoll, float targetRotationalSpeed, float targetThrust) {
            TargetPitch = targetPitch;
            TargetRoll = targetRoll;
            TargetRotationalSpeed = targetRotationalSpeed;
            TargetThrust = targetThrust;
        }

        public static bool operator ==(TargetMovementData a, TargetMovementData b) {
            return a.Equals(b);
        }

        public static bool operator !=(TargetMovementData a, TargetMovementData b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            if(obj is TargetMovementData)
                return Equals((TargetMovementData)obj);
            return false;
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = TargetPitch.GetHashCode();
                hashCode = (hashCode * 397) ^ TargetRoll.GetHashCode();
                hashCode = (hashCode * 397) ^ TargetRotationalSpeed.GetHashCode();
                hashCode = (hashCode * 397) ^ TargetThrust.GetHashCode();
                return hashCode;
            }
        }

        public bool Equals(TargetMovementData other) {
            return Math.Abs(TargetPitch - other.TargetPitch) < 0.05
                   && Math.Abs(TargetRoll - other.TargetRoll) < 0.05
                   && Math.Abs(TargetRotationalSpeed - other.TargetRotationalSpeed) < 0.05
                   && Math.Abs(TargetThrust - other.TargetThrust) < 0.01;
        }
    }
}
