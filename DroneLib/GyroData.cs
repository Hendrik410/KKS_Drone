using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct GyroData
    {
        public float Pitch { get; }
        public float Roll { get; }
        public float Yaw { get; }

        public float GyroX { get; }
        public float GyroY { get; }
        public float GyroZ { get; }

        public float AccelerationX { get; }
        public float AccelerationY { get; }
        public float AccelerationZ { get; }

        public float MagnetX { get; }
        public float MagnetY { get; }
        public float MagnetZ { get; }

        public float Temperature { get; }

        public GyroData(PacketBuffer buffer)
        {
            this.Pitch = buffer.ReadFloat();
            this.Roll = buffer.ReadFloat();
            this.Yaw = buffer.ReadFloat();

            this.GyroX = buffer.ReadFloat();
            this.GyroY = buffer.ReadFloat();
            this.GyroZ = buffer.ReadFloat();

            this.AccelerationX = buffer.ReadFloat();
            this.AccelerationY = buffer.ReadFloat();
            this.AccelerationZ = buffer.ReadFloat();

            this.MagnetX = buffer.ReadFloat();
            this.MagnetY = buffer.ReadFloat();
            this.MagnetZ = buffer.ReadFloat();

            this.Temperature = buffer.ReadFloat();
        }

        public static bool operator ==(GyroData a, GyroData b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(GyroData a, GyroData b)
        {
            return !(a == b);
        }
    }
}
