using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public struct DebugData
    {
        public float FrontLeftRatio { get; set; }
        public float FrontRightRatio { get; set; }
        public float BackLeftRatio { get; set; }
        public float BackRightRatio { get; set; }

        public float FrontLeftCorrection { get; set; }
        public float FrontRightCorrection { get; set; }
        public float BackLeftCorrection { get; set; }
        public float BackRightCorrection { get; set; }
    }
}
