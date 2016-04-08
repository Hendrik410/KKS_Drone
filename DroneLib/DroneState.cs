using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public enum DroneState
    {
        Unkown,
        Reset,
        OTA,
        Stopped,
        Idle,
        Armed,
        Flying
    }
}
