using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public enum StopReason
    {
        Unkown,
        None,
        User,
        NoData,
        NoPing,
        InvalidGyro
    }
}
