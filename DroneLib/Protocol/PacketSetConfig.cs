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

            /*packet.Write(Settings.DroneName);
            packet.Write(Settings.NetworkSSID);
            packet.Write(Settings.NetworkPassword);
            packet.Write(Settings.VerboseSerialLog);*/
            packet.Write(Settings.Degree2Ratio);
            packet.Write(Settings.RotaryDegree2Ratio);
        }
    }
}