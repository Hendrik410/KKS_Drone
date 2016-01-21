using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DroneLibrary
{
    public struct DroneEntry : IEquatable<DroneEntry>
    {
        /// <summary>
        /// Gibt den Zeitpunkt zurück, als die Drone zuletzt gefunden wurde.
        /// </summary>
        public DateTime LastFound { get; set; }

        /// <summary>
        /// Gibt die IP-Adresse der Drone an.
        /// </summary>
        public IPAddress Address;

        /// <summary>
        /// Gibt den benutzerfreundlichen Namen der Drone an.
        /// </summary>
        public string Name;

        /// <summary>
        /// Gibt den Model Namen der Drone an.
        /// </summary>
        public string Model;

        /// <summary>
        /// Gibt die Seriennummer der Drone an.
        /// </summary>
        public string SerialCode;

        /// <summary>
        /// Gibt die Version der Firmware auf der Drone an.
        /// </summary>
        public int FirmwareVersion;

        public static DroneEntry UpdateEntry(DroneEntry entry)
        {
            DroneEntry e = new DroneEntry();
            e.LastFound = DateTime.Now;
            e.Address = entry.Address;
            e.Name = entry.Name;
            e.Model = entry.Model;
            e.SerialCode = entry.SerialCode;
            e.FirmwareVersion = entry.FirmwareVersion;
            return e;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DroneEntry)
                return Equals((DroneEntry)obj);
            return false;
        }

        public bool Equals(DroneEntry other)
        {
            return other.Address.Equals(Address)
                && other.Name == Name
                && other.Model == Model
                && other.SerialCode == SerialCode
                && other.FirmwareVersion == FirmwareVersion;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Address.GetHashCode();
            hash = (hash * 7) + Name.GetHashCode();
            hash = (hash * 7) + Model.GetHashCode();
            hash = (hash * 7) + FirmwareVersion.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return string.Format("{0} (v{1}, {2}) - {3}", Name, FirmwareVersion, SerialCode, Address);
        }
    }
}
