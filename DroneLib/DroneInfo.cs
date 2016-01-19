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
        /// Gibt den benutzerfreundlichen Namen der Drone zurück.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gibt den Model Namen der Drone zurück.
        /// </summary>
        public string ModelName { get; private set; }

        /// <summary>
        /// Gibt die Seriennummer der Drone zurück.
        /// </summary>
        public string SerialCode { get; private set; }
        
        /// <summary>
        /// Gibt den Buildnamen der Drone zurück.
        /// </summary>
        public string BuildName { get; private set; }

        /// <summary>
        /// Gibt die Build-Version der Firmware der Drone zurück.
        /// </summary>
        public byte BuildVersion { get; private set; }

        /// <summary>
        /// Gibt die größte Revision der Drone zurück.
        /// </summary>
        public int HighestRevision { get; private set; }

        public DroneInfo(string name, string modelName, string serialCode, string buildName, byte buildVersion, int highestRevision)
        {
            this.Name = name;
            this.ModelName = modelName;
            this.SerialCode = serialCode;
            this.BuildName = buildName;
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
            return Name == other.Name
                && ModelName == other.ModelName
                && SerialCode == other.SerialCode
                && BuildName == other.BuildName
                && BuildVersion == other.BuildVersion 
                && HighestRevision == other.HighestRevision;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = hash * 7 + (Name == null ? 0 : Name.GetHashCode());
                hash = hash * 7 + (ModelName == null ? 0 : ModelName.GetHashCode());
                hash = hash * 7 + (SerialCode == null ? 0 : SerialCode.GetHashCode());
                hash = hash * 7 + (BuildName == null ? 0 : BuildName.GetHashCode());
                hash = hash * 7 + BuildVersion.GetHashCode();
                hash = hash * 7 + HighestRevision.GetHashCode();
                return hash;
            }
        }
    }
}
