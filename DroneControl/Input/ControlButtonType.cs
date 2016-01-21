using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input {
    enum ControlButtonType {
        ToggleArm,
        Stop,
        SetCurrentAsOffset,

        PitchOffsetIncrement,
        PitchOffsetDecrement,

        RollOffsetIncrement,
        RollOffsetDecrement,

        YawOffsetIncrement,
        YawOffsetDecrement,

        TrustOffsetIncrement,
        TrustOffsetDecrement
    }
}
