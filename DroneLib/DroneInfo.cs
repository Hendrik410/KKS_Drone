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
        [Category("Drone")]
        public string Name { get; private set; }

        /// <summary>
        /// Gibt den Model Namen der Drone zurück.
        /// </summary>
        [Category("Drone")]
        public string ModelName { get; private set; }

        /// <summary>
        /// Gibt die Seriennummer der Drone zurück.
        /// </summary>
        [Category("Drone")]
        public string SerialCode { get; private set; }

        /// <summary>
        /// Gibt den Buildnamen der Drone zurück.
        /// </summary>
        [Category("Build")]
        public string BuildName { get; private set; }

        /// <summary>
        /// Gibt die Build-Version der Firmware der Drone zurück.
        /// </summary>
        [Category("Build")]
        public byte BuildVersion { get; private set; }

        /// <summary>
        /// Gibt die größte Revision der Drone zurück.
        /// </summary>
        [Category("Debug")]
        public int HighestRevision { get; private set; }

        [Category("Debug")]
        public ResetReason ResetReason { get; private set; }

        [Category("Debug")]
        public ResetException ResetException { get; private set; }

        [Category("Debug")]
        public StopReason StopReason { get; private set; }

        public DroneInfo(PacketBuffer buffer)
        {
            Name = buffer.ReadString();
            ModelName = buffer.ReadString();
            SerialCode = buffer.ReadString();

            BuildName = buffer.ReadString().Trim().Replace(' ', '_');
            BuildVersion = buffer.ReadByte();

            HighestRevision = buffer.ReadInt();
            ResetReason = (ResetReason)buffer.ReadByte();
            ResetException = (ResetException)buffer.ReadByte();

            if (ResetReason != ResetReason.Exception)
                ResetException = ResetException.None;

            StopReason = (StopReason)buffer.ReadByte();
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
                && HighestRevision == other.HighestRevision
                && ResetReason == other.ResetReason
                && ResetException == other.ResetException
                && StopReason == other.StopReason;
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
                hash = hash * 7 + ResetReason.GetHashCode();
                hash = hash * 7 + ResetException.GetHashCode();
                hash = hash * 7 + StopReason.GetHashCode();
                return hash;
            }
        }
    }
}
