using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input
{
    public struct TargetData
    {
        public float Pitch;
        public float Roll;
        public float RotationalSpeed;
        public float Thrust;

        public TargetData(float pitch, float roll, float rotationalSpeed, float thrust)
        {
            this.Pitch = pitch;
            this.Roll = roll;
            this.RotationalSpeed = rotationalSpeed;
            this.Thrust = thrust;
        }
    }
}
