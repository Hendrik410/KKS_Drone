using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input {
    public abstract class DeviceInputInterpreter {

        public event EventHandler TargetMovementChange;

        protected virtual void OnTargetMovementChange(EventArgs e) {
            TargetMovementChange?.Invoke(this, e);
        }

        public abstract TargetMovementData TargetMovementData {
            get;
            protected set;
        }

        public float MaxPitch {
            get;
            set;
        } = 45;

        public float MaxRoll {
            get;
            set;
        } = 45;

        public float MaxYaw {
            get;
            set;
        } = 45;

        //TODO use ControlButtonEventArgs
        public delegate void ControlButtenPressedEventHandler(object sender, ControlButtonType type);

        public event ControlButtenPressedEventHandler ControlButtonPressed;

        protected virtual void OnControlButtonPressed(ControlButtonType t) {
            ControlButtonPressed?.Invoke(this, t);
        }

        public abstract bool StartListen();
        public abstract bool StopListen();

    }
}
