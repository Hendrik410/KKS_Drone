using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary {
    /// <summary>
    /// Stellt Methoden für Netzwerkkommunikation bereit.
    /// </summary>
    class NetworkHelper {

        /// <summary>
        /// Gibt die lokale IP-Adresse des Hosts zurück.
        /// </summary>
        /// <returns>Die IP-Adresse des Hosts</returns>
        public static IPAddress GetLocalIPAddress() {
            if(!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        /// <summary>
        /// Gibt die Adresse zum Senden an alle Netzwerkgeräte zurück.
        /// </summary>
        /// <returns>Die Adresse zum Senden an alle Netzwerkgeräte</returns>
        public static IPAddress GetLocalBroadcastAddress() {
            byte[] ipBytes = GetLocalIPAddress().GetAddressBytes();
            ipBytes[3] = 255;
            return new IPAddress(ipBytes);
        }
    }
}
