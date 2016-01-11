using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol {
    public static class PacketTypeHelper {
        /// <summary>
        /// Gibt zurück, ob das Cluster bei einem Paket-Typ antwortet.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool DoesClusterAnswer(this PacketType type) {
            switch(type) {
                case PacketType.Ping:
                case PacketType.Info:
                    return true;
                default:
                    return false;
            }
        }

        
    }
}
