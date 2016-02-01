using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct MotorRatios
    {
        public float FrontLeft { get; }
        public float FrontRight { get; }
        public float BackLeft { get; }
        public float BackRight { get; }

        public MotorRatios(float frontLeft, float frontRight, float backLeft, float backRight)
        {
            FrontLeft = frontLeft;
            FrontRight = frontRight;
            BackLeft = backLeft;
            BackRight = BackLeft;
        }

        public MotorRatios(PacketBuffer buffer)
        {
            FrontLeft = buffer.ReadFloat();
            FrontRight = buffer.ReadFloat();
            BackLeft = buffer.ReadFloat();
            BackRight = buffer.ReadFloat();
        }
    }
}
