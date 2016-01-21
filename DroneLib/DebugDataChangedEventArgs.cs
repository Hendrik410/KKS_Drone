using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class DebugDataChangedEventArgs : EventArgs
    {
        public DebugData DebugData { get; private set; }

        public DebugDataChangedEventArgs(Drone drone)
        {
            this.DebugData = drone.DebugData;
        }
    }
}
