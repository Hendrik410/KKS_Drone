using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary.Protocol
{
    public struct PacketSetConfig: IPacket
    {
        public PacketType Type => PacketType.SetConfig;

        public readonly DroneSettings Settings;

        public PacketSetConfig(DroneSettings settings)
        {
            this.Settings = settings;
        }

        public void Write(PacketBuffer packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            Settings.Write(packet);
        }
    }
}