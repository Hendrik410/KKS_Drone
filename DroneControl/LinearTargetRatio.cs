using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneLibrary;
using DroneControl.Input;

namespace DroneControl
{
    public class LinearTargetRatio
    {
        private float[] oldValues = new float[4];
        private float lastYaw;
        private long tickCount;
        private const long tickRange = (1000 / 20);

        public float[] Calculate(DroneSettings settings, TargetMovementData target, DroneData data)
        {
            float[] newValues = new float[] {
                GetTargetRatio(settings, true, true, false, target, data),
                GetTargetRatio(settings, true, false, true, target, data),
                GetTargetRatio(settings, false, true, true, target, data),
                GetTargetRatio(settings, false, false, false, target, data)
            };

            for (int i = 0; i < 4; i++)
                oldValues[i] += (newValues[i] - oldValues[i]) * settings.InterpolationFactor;

            if (tickCount++ % tickRange == 0)
                lastYaw = data.Gyro.Yaw;
            return oldValues;
        }

        private static float AngleDifference(float a, float b)
        {
            return ((((a - b) % 360) + 540) % 360) - 180;
        }

        private float GetTargetRatio(DroneSettings settings, bool isFront, bool isLeft, bool isClockwise, TargetMovementData target, DroneData data)
        {
            float ratio = target.TargetThrust;

            float deltaYaw = AngleDifference(data.Gyro.Yaw, lastYaw); // / ((1000 / settings.PhysicsCalcDelay) * tickRange);
            if (Math.Abs(deltaYaw) > 0.5f)
                deltaYaw = 0.5f;

            ratio += GetTargetRatio(settings, isFront, isLeft, isClockwise, target.TargetPitch, target.TargetRoll, target.TargetRotationalSpeed);
            ratio -= settings.CorrectionFactor * GetTargetRatio(settings, isFront, isLeft, isClockwise, 0, 0, deltaYaw);
            return ratio;
        }

        private static float GetTargetRatio(DroneSettings settings, bool isFront, bool isLeft, bool isClockwise, float pitch, float roll, float yaw)
        {
            float targetMotorRatio = 0;
            if (Math.Abs(pitch) >= 0.02)
            {
                if (isFront)
                    targetMotorRatio += pitch * settings.Degree2Ratio;
                else
                    targetMotorRatio -= pitch * settings.Degree2Ratio;
            }

            if (Math.Abs(roll) >= 0.02)
            {
                if (isLeft)
                    targetMotorRatio -= roll * settings.Degree2Ratio;
                else
                    targetMotorRatio += roll * settings.Degree2Ratio;
            }

            if (Math.Abs(yaw) >= 0.02)
            {
                if (isClockwise)
                    targetMotorRatio -= yaw * settings.RotationalDegree2Ratio;
                else
                    targetMotorRatio += yaw * settings.RotationalDegree2Ratio;
            }

            return targetMotorRatio;
        }
    }
}
