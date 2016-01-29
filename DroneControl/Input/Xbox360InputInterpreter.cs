using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SharpDX;
using SharpDX.XInput;

namespace DroneControl.Input
{
    public class Xbox360InputInterpreter : DeviceInputInterpreter
    {
        public override TargetMovementData TargetMovementData
        {
            get { return targetMovementData; }
            protected set
            {
                targetMovementData = value; // immer setzten, um kontinuierlich Daten der Drohne zu senden
                OnTargetMovementChange(EventArgs.Empty);
            }
        }


        private Timer timer;
        private Controller controller;

        private State lastState;

        private TargetMovementData targetMovementData;

        public Xbox360InputInterpreter(int updateIntervall = 50)
        {
            if (updateIntervall < 1)
                throw new ArgumentOutOfRangeException(nameof(updateIntervall));

            timer = new Timer(updateIntervall)
            {
                AutoReset = true,
                Enabled = true
            };
            timer.Stop();
            timer.Elapsed += OnTimerTick;
        }

        public bool SelectDevice()
        {
            for (int i = (int)UserIndex.One; i <= (int)UserIndex.Four; i++)
            {
                Controller controller = new Controller((UserIndex)i);
                if (controller != null && controller.IsConnected)
                {
                    this.controller = controller;
                    return true;
                }
            }
            return false;
        }

        public override bool StartListen()
        {
            if (!SelectDevice())
                return false;
            timer.Start();

            return true;
        }

        public override bool StopListen()
        {
            timer.Stop();
            
            return true;
        }

        private const int controllerMaxValue = UInt16.MaxValue / 2;
        private const int minButtonPressedTime = 30;


        private void OnTimerTick(object sender, EventArgs e)
        {
            if (!controller.IsConnected)
                return;
            var state = controller.GetState();

            float targetPitch = (state.Gamepad.LeftThumbY / (float)short.MaxValue) * MaxPitch;
            float targetRoll = (state.Gamepad.LeftThumbX / (float)short.MaxValue) * MaxRoll;
            float targetRotationalSpeed = (state.Gamepad.RightThumbX / (float)short.MaxValue) * MaxYaw;
            float targetThrust = (state.Gamepad.RightThumbY / (float)short.MaxValue);

            /*if (Math.Abs(targetPitch) < 1)
                targetPitch = 0;
            if (Math.Abs(targetRoll) < 1)
                targetRoll = 0;
            //if (Math.Abs(targetRotationalSpeed) < 10)
            //    targetRotationalSpeed = 0;
            if (Math.Abs(targetThrust) < 0.1)
                targetThrust = 0;*/

            targetPitch += PitchOffset;
            targetRoll += RollOffset;
            targetRotationalSpeed += RotationalSpeedOffset;

            if (targetThrust >= 0)
                targetThrust *= 0.5f;

            TargetMovementData = new TargetMovementData(targetPitch, targetRoll, targetRotationalSpeed, targetThrust);

            CheckButton(lastState, state, GamepadButtonFlags.Start, ControlButtonType.ToggleArm);
            CheckButton(lastState, state, GamepadButtonFlags.Y, ControlButtonType.Stop);

            lastState = state;
        }

        private void CheckButton(State lastState, State state, GamepadButtonFlags button, ControlButtonType type)
        {
            if (lastState.Gamepad.Buttons.HasFlag(button) && !state.Gamepad.Buttons.HasFlag(button))
                OnControlButtonPressed(type);
        }
    }
}
