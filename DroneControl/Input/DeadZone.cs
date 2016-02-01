using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input
{
    public static class DeadZone
    {
        public static float Compute(int value, int maxValue, float deadZone)
        {
            return Compute(value / (float)maxValue, deadZone);
        }

        public static float Compute(float value, float deadZone)
        {
            if (Math.Abs(value) < deadZone)
                return 0;

            float newValue = Math.Abs(value); // Wert ins positive bringen fürs bessere Rechnen
            newValue -= deadZone; // Dead Zone subtrahieren
            newValue /= 1 - deadZone; // Normalisieren
            return newValue * Math.Sign(value); // Wert wieder ins negative bringen, wenn der orginale Wert negativ ist
        }
    }
}
