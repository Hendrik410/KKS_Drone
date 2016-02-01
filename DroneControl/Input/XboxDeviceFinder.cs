using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XInput;

namespace DroneControl.Input
{
    public class XboxDeviceFinder : IDeviceFinder
    {
        public IInputDevice[] FindDevices()
        {
            List<IInputDevice> devices = new List<IInputDevice>();

            for (int i = (int)UserIndex.One; i <= (int)UserIndex.Four; i++)
            {
                Controller controller = new Controller((UserIndex)i);
                if (controller.IsConnected)
                    devices.Add(new XboxController(controller));
            }


            return devices.ToArray();
        }
    }
}
