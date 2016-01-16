using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DroneLibrary
{
    public struct DroneEntry
    {
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

        public override string ToString()
        {
            return string.Format("{0} (v{1}, {2}) - {3}", Name, FirmwareVersion, SerialCode, Address);
        }
    }
}
