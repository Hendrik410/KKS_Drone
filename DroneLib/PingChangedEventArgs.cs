using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class PingChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; private set; }
        public int Ping { get; private set; }

        public PingChangedEventArgs(Drone drone)
        {
            this.IsConnected = drone.IsConnected;
            this.Ping = drone.Ping;
        }
    }
}
