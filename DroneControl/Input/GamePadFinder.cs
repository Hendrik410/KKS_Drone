using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectInput;

namespace DroneControl.Input
{
    public class GamePadFinder : IDeviceFinder
    {
        private DirectInput directInput;

        public GamePadFinder()
        {
            directInput = new DirectInput();
        }

        public IInputDevice[] FindDevices()
        {
            return directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices)
                .Select(d => new GamePad(directInput, d))
                .ToArray();
        }
    }
}
