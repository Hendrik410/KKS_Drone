using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class SettingsChangedEventArgs : EventArgs
    {
        public DroneSettings Settings { get; private set; }

        public SettingsChangedEventArgs(Drone drone)
        {
            this.Settings = drone.Settings;
        }
    }
}
