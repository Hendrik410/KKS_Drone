using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct DebugData
    {
        public float FrontLeftRatio { get; set; }
        public float FrontRightRatio { get; set; }
        public float BackLeftRatio { get; set; }
        public float BackRightRatio { get; set; }

        public static bool operator ==(DebugData a, DebugData b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DebugData a, DebugData b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is DebugData)
                return Equals((DebugData)obj);
            return false;
        }

        public bool Equals(DebugData other)
        {
            return FrontLeftRatio == other.FrontLeftRatio
                && FrontRightRatio == other.FrontRightRatio
                && BackLeftRatio == other.BackLeftRatio
                && BackRightRatio == other.BackRightRatio;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = hash * 7 + FrontLeftRatio.GetHashCode();
                hash = hash * 7 + FrontRightRatio.GetHashCode();
                hash = hash * 7 + BackLeftRatio.GetHashCode();
                hash = hash * 7 + BackRightRatio.GetHashCode();
                return hash;
            }
        }
    }
}
