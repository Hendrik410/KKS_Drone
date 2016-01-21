using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class DroneListChangedEventArgs : EventArgs
    {
        public DroneEntry[] Entries { get; private set; }

        public DroneListChangedEventArgs(DroneEntry[] entries)
        {
            this.Entries = entries;
        }
    }
}
