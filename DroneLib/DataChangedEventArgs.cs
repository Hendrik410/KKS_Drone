using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class DataChangedEventArgs : EventArgs
    {
        public DroneData Data { get; private set; }

        public DataChangedEventArgs(Drone drone)
        {
            this.Data = drone.Data;
        }
    }
}
