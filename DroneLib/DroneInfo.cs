using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    /// <summary>
    /// Stellt verschiedene Informationen über das Drone bereit.
    /// </summary>
    public struct DroneInfo : IEquatable<DroneInfo>
    {
        /// <summary>
        /// Gibt die Build-Version der Firmware des Clusters zurück.
        /// </summary>
        public byte BuildVersion { get; private set; }

        /// <summary>
        /// Gibt die größte Revision des Clusters zurück.
        /// </summary>
        public int HighestRevision { get; private set; }

        public DroneInfo(byte buildVersion, int highestRevision)
        {
            this.BuildVersion = buildVersion;
            this.HighestRevision = highestRevision;
        }

        public static bool operator ==(DroneInfo a, DroneInfo b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DroneInfo a, DroneInfo b)
        {
            return !(a == b);
        }


        public override bool Equals(object obj)
        {
            if (obj is DroneInfo)
                return Equals((DroneInfo)obj);
            return false;
        }

        public bool Equals(DroneInfo other)
        {
            return BuildVersion == other.BuildVersion 
                && HighestRevision == other.HighestRevision;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = hash * 7 + BuildVersion.GetHashCode();
                hash = hash * 7 + HighestRevision.GetHashCode();
                return hash;
            }
        }
    }
}
