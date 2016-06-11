using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public enum StopReason
    {
        Unknown,
        None,
        User,
        NoData,
        NoPing,
        InvalidGyro,
        WifiDisconnect
    }
}
