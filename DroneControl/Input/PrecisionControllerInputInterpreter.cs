using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SharpDX.DirectInput;

namespace DroneControl.Input {
    class PrecisionControllerInputInterpreter : DeviceInputInterpreter {
        public override TargetMovementData TargetMovementData
        {
            get { return targetMovementData; }
            protected set
            {
                targetMovementData = value; // immer setzten, um kontinuierlich daten der drohne zu senden
                OnTargetMovementChange(EventArgs.Empty);
            }
        }


        private Timer timer;

        private DirectInput directInput;

        private Joystick inputJoystick;
        private Joystick inputG13Joystick;
        private Keyboard inputKeyboard;

        private TargetMovementData targetMovementData;

        private Dictionary<int, ControlButtonType> joystickButtonMappings;

        private Stopwatch buttonPressedStopwatch;
        private Dictionary<int, long> buttonPressedTimes;

        public PrecisionControllerInputInterpreter(int updateIntervall = 50) {
            if(updateIntervall < 1)
                throw new ArgumentOutOfRangeException(nameof(updateIntervall));

            joystickButtonMappings = new Dictionary<int, ControlButtonType>();
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
            joystickButtonMappings.Add(0, ControlButtonType.Stop);
            joystickButtonMappings.Add(1, ControlButtonType.ToggleArm);
            joystickButtonMappings.Add(2, ControlButtonType.SetCurrentAsOffset);
        }

        public bool SelectDevice() {

            directInput = new DirectInput();

            var joystickGuid = Guid.Empty;
            var g13JoystickGuid = Guid.Empty;

            IList<DeviceInstance> controllerDevices = directInput.GetDevices(DeviceClass.All,
                DeviceEnumerationFlags.AllDevices);
            foreach(DeviceInstance deviceInstance in controllerDevices)
                if(deviceInstance.InstanceName.Contains("G13"))
                    g13JoystickGuid = deviceInstance.InstanceGuid;
                else if(deviceInstance.Type == DeviceType.Joystick)
                    joystickGuid = deviceInstance.InstanceGuid;

            if(joystickGuid == Guid.Empty || g13JoystickGuid == Guid.Empty)
                return false;

            inputJoystick = new Joystick(directInput, joystickGuid);
            inputG13Joystick = new Joystick(directInput, g13JoystickGuid);

            inputKeyboard = new Keyboard(directInput);
            inputKeyboard.Properties.BufferSize = 128;


            return true;
        }

        public override bool StartListen() {
            if(!SelectDevice())
                return false;
            inputJoystick.Acquire();
            inputG13Joystick.Acquire();
            inputKeyboard.Acquire();
            timer.Start();

            return true;
        }

        public override bool StopListen() {
            timer.Stop();
            inputJoystick?.Unacquire();
            inputG13Joystick?.Unacquire();
            inputKeyboard?.Unacquire();
            return true;
        }

        private const int controllerMaxValue = UInt16.MaxValue / 2;
        private const int minButtonPressedTime = 30;


        private void OnTimerTick(object sender, EventArgs e) {
            inputJoystick.Poll();

            JoystickState state = inputJoystick.GetCurrentState();
            JoystickState g13State = inputG13Joystick.GetCurrentState();

            float targetPitch = ((float)(state.Y - controllerMaxValue) / controllerMaxValue) * MaxPitch;
            float targetRoll = ((float)(state.X - controllerMaxValue) / controllerMaxValue) * MaxRoll;
            float targetRotationalSpeed = ((float)(state.RotationZ - controllerMaxValue) / controllerMaxValue) * MaxYaw;
            float targetThrust = (float)(g13State.Y - controllerMaxValue) / controllerMaxValue;

            targetPitch *= -1;
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
                        if(joystickButtonMappings.ContainsKey(i)) {
                            buttonPressedTimes[i] = -1;
                            OnControlButtonPressed(joystickButtonMappings[i]);
                        }
            }

        }
    }
}
