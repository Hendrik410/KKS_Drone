using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroneLibrary;
using SharpDX.DirectInput;

namespace DroneControl.Input {
    class InputController {

        public Drone Drone {
            get;
        }

        public DeviceInputInterpreter DeviceInterpreter {
            get;
        }
        

        public InputController(Drone drone, DeviceInputInterpreter deviceInterpreter) {
            if(drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.DeviceInterpreter = deviceInterpreter;
            this.Drone = drone;

            

            DeviceInterpreter.TargetMovementChange += OnTargetMovementDataChange;
            DeviceInterpreter.ControlButtonPressed += OnControlButtonPressed;
        }

        private void OnControlButtonPressed(object sender, ControlButtonType type) {
            switch(type) {
                case ControlButtonType.Stop:
                    Drone.SendStop();
                    break;

                case ControlButtonType.ToggleArm:
                    if(Drone.IsConnected) 
                        if(Drone.Data.IsArmed)
                            Drone.SendDisarm();
                        else
                            Drone.SendArm();
                    break;
            }
        }

        private void OnTargetMovementDataChange(object sender, EventArgs eventArgs) {
            TargetMovementData targetMovement = DeviceInterpreter.TargetMovementData;
            
            Drone.SendMovementData(targetMovement.TargetPitch, targetMovement.TargetRoll, targetMovement.TargetYaw, targetMovement.TargetThrust, false);
        }

        public void Start() {
            DeviceInterpreter.StartListen();
        }

        public void Stop() {
            DeviceInterpreter.StopListen();
        }

    }
}
