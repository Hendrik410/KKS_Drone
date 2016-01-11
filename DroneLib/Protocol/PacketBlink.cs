using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol {
    public struct PacketBlink : IPacket {

        public PacketType Type {
            get {
                return PacketType.Blink;
            }
        }

        public void Write(BinaryWriter writer) {
            
        }
    }
}
