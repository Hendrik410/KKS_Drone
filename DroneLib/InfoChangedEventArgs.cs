using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class InfoChangedEventArgs : EventArgs
    {
        public DroneInfo Info { get; private set; }

        public InfoChangedEventArgs(Drone drone)
        {
            this.Info = drone.Info;
        }
    }
}
