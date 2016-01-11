using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol {
    public struct PacketInfo : IPacket {

        public PacketType Type {
            get {
                return PacketType.Info;
            }
        }

        public void Write(BinaryWriter writer) {
            
        }
    }
}
