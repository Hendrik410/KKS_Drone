using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input
{
    public struct BatteryInfo
    {
        public readonly bool HasBattery;
        public readonly BatteryLevel Level;

        public BatteryInfo(bool hasBattery, BatteryLevel level)
        {
            this.HasBattery = hasBattery;
            this.Level = level;
        }
    }
}
