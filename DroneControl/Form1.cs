using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.DirectInput;

namespace DroneControl {
    public partial class MainWindow : Form {

        DirectInput directInput = new DirectInput();
        Joystick joystick;

        public MainWindow() {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e) {
            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach(var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if(joystickGuid == Guid.Empty)
                foreach(DeviceInstance deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            if(joystickGuid == Guid.Empty) {
                MessageBox.Show("No device found");
                Application.Exit();
            }

            joystick = new Joystick(directInput, joystickGuid);
            joystick.Acquire();
            
            RefreshTimer.Enabled = true;

        }

        private void RefreshTimer_Tick(object sender, EventArgs e) {
            joystick.Poll();
            JoystickState state = joystick.GetCurrentState();

            txtControllerDump.Text = state.ToString();
            //txtControllerDump.Text = String.Format("X1: {0}, Y1: {1}, X2: {2}, Y2: {3}", state.X, state.Y, state.Z, state.RotationZ);
        }


        
    }
}
