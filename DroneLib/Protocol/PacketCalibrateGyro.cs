using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol {
    public struct PacketCalibrateGyro : IPacket {
        public PacketType Type => PacketType.CalibrateGyro;

        public void Write(PacketBuffer packet) {
            if(packet == null)
                throw new ArgumentNullException(nameof(packet));
        }
    }
}
