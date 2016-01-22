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
        /// Gibt die lokale IP-Adressen des Hosts zurück.
        /// </summary>
        /// <returns>Die IP-Adressen des Hosts</returns>
        public static IPAddress[] GetLocalIPAddresses() {
            if(!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToArray();
        }

        /// <summary>
        /// Gibt die Adressen zum Senden an alle Netzwerkgeräte zurück.
        /// </summary>
        /// <returns>Die Adressen zum Senden an alle Netzwerkgeräte</returns>
        public static IPAddress[] GetLocalBroadcastAddresses()
        {
            IPAddress[] addresses = GetLocalIPAddresses();
            if (addresses == null)
                return null;
            for (int i = 0; i < addresses.Length; i++)
                addresses[i] = GetLocalBroadcastAddress(addresses[i]);
            return addresses;
        }

        private static IPAddress GetLocalBroadcastAddress(IPAddress address)
        {
            byte[] ipBytes = address.GetAddressBytes();
            ipBytes[3] = 255;
            return new IPAddress(ipBytes);
        }
    }
}
