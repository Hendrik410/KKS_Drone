using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectInput;

namespace DroneControl.Input
{
    public class GamePad : IInputDevice
    {
        private DirectInput directInput;
        private Joystick device;

        private JoystickState currentState;
        private JoystickState lastState;

        public bool IsConnected
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return device.Information.InstanceName;
            }
        }

        public BatteryInfo Battery
        {
            get
            {
                return new BatteryInfo(false, BatteryLevel.Empty);
            }
        }

        public GamePad(DirectInput directInput, DeviceInstance deviceInstance)
        {
            this.directInput = directInput;
            this.device = new Joystick(directInput, deviceInstance.InstanceGuid);
            this.device.Acquire();
        }

        public void Update(InputManager manager)
        {
            currentState = device.GetCurrentState();

            if (CheckButtonPressed(3))
                manager.SendClear();

            if (CheckButtonPressed(0))
                manager.StopDrone();

            if (CheckButtonPressed(9))
                manager.ToogleArmStatus();

            float deadZone = 0.075f;
            if (!manager.DeadZone)
                deadZone = 0;

            const int maxValue = UInt16.MaxValue / 2;
            TargetData target = new TargetData();
            target.Roll = DeadZone.Compute(currentState.Y, maxValue, deadZone);
            target.Pitch = DeadZone.Compute(currentState.X, maxValue, deadZone);
            target.RotationalSpeed = DeadZone.Compute(currentState.Z, maxValue, deadZone);
            target.Thurst = DeadZone.Compute(currentState.RotationZ, maxValue, deadZone);

            manager.SendTargetData(target);

            lastState = currentState;
        }

        private bool CheckButtonPressed(int button)
        {
            if (button >= currentState.Buttons.Length)
                return false;

            bool current = currentState.Buttons[button];
            bool last = lastState != null && lastState.Buttons[button];
            return current && !last;
        }

        public void Dispose()
        {
            if (device != null)
            {
                device.Unacquire();
                device.Dispose();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is GamePad)
                return Equals((GamePad)obj);
            return false;
        }

        public bool Equals(IInputDevice other)
        {
            GamePad o = other as GamePad;
            if (o == null)
                return false;
            return o.device.Information.InstanceGuid == device.Information.InstanceGuid;
        }

        public override int GetHashCode()
        {
            return device.Information.InstanceGuid.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
