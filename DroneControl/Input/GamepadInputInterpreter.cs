using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SharpDX.DirectInput;

namespace DroneControl.Input {
    class GamepadInputInterpreter : DeviceInputInterpreter {


        public override TargetMovementData TargetMovementData {
            get { return targetMovementData; }
            protected set {
                targetMovementData = value; // immer setzten, um kontinuierlich daten der drohne zu senden
                OnTargetMovementChange(EventArgs.Empty);
            }
        }


        private Timer timer;

        private DirectInput directInput;

        private Joystick inputDevice;

        private TargetMovementData targetMovementData;

        private Dictionary<int, ControlButtonType> buttonMappings;

        private Stopwatch buttonPressedStopwatch;
        private Dictionary<int, long> buttonPressedTimes; 

        public GamepadInputInterpreter(int updateIntervall = 50) {
            if(updateIntervall < 1)
                throw new ArgumentOutOfRangeException(nameof(updateIntervall));

            buttonMappings = new Dictionary<int, ControlButtonType>();
            PopulateButtonMappings();
            buttonPressedTimes = new Dictionary<int, long>();
            buttonPressedStopwatch = new Stopwatch();
            buttonPressedStopwatch.Start();

            timer = new Timer(updateIntervall) {
                AutoReset = true,
                Enabled = true
            };
            timer.Stop();
            timer.Elapsed += OnTimerTick;
        }

        private void PopulateButtonMappings() {
            buttonMappings.Add(0, ControlButtonType.Stop);
            buttonMappings.Add(3, ControlButtonType.SetCurrentAsOffset);
            buttonMappings.Add(9, ControlButtonType.ToggleArm);
        }

        public bool SelectDevice() {

            directInput = new DirectInput();

            var joystickGuid = Guid.Empty;

            
            foreach(DeviceInstance deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            if(joystickGuid == Guid.Empty)
                return false;

            inputDevice = new Joystick(directInput, joystickGuid);

            return true;
        }

        public override bool StartListen() {
            if(!SelectDevice())
                return false;
            inputDevice.Acquire();
            timer.Start();

            return true;
        }

        public override bool StopListen() {
            timer.Stop();
            inputDevice?.Unacquire();
            return true;
        }


        private const int controllerMaxValue = UInt16.MaxValue / 2;
        private const int minButtonPressedTime = 30;
        

        private void OnTimerTick(object sender, EventArgs e) {
            inputDevice.Poll();

            JoystickState state = inputDevice.GetCurrentState();

            float targetPitch = ((float)(state.Y - controllerMaxValue) / controllerMaxValue) * MaxPitch;
            float targetRoll = ((float)(state.X - controllerMaxValue) / controllerMaxValue) * MaxRoll;
            float targetRotationalSpeed = ((float)(state.Z - controllerMaxValue) / controllerMaxValue) * MaxYaw;
            float targetThrust = (float)(state.RotationZ - controllerMaxValue) / controllerMaxValue;

            targetThrust *= -1;

            if(Math.Abs(targetPitch) < 0.1)
                targetPitch = 0;
            if(Math.Abs(targetRoll) < 0.1)
                targetRoll = 0;
            if(Math.Abs(targetRotationalSpeed) < 0.1)
                targetRotationalSpeed = 0;
            if(Math.Abs(targetThrust) < 0.05)
                targetThrust = 0;

            TargetMovementData = new TargetMovementData(targetPitch, targetRoll, targetRotationalSpeed, targetThrust);

            for(int i = 0; i < state.Buttons.Length; i++) {
                if(state.Buttons[i] && (!buttonPressedTimes.ContainsKey(i) || buttonPressedTimes[i] == -1))
                    buttonPressedTimes[i] = buttonPressedStopwatch.ElapsedMilliseconds;

                if(!state.Buttons[i] && buttonPressedTimes.ContainsKey(i) && buttonPressedTimes[i] > 0)
                    if(buttonPressedStopwatch.ElapsedMilliseconds - buttonPressedTimes[i] >= minButtonPressedTime)
                        if(buttonMappings.ContainsKey(i)) {
                            buttonPressedTimes[i] = -1;
                            OnControlButtonPressed(buttonMappings[i]);
                        }
            }

        }

    }
}
