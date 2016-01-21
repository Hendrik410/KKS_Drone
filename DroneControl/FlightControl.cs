using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroneControl.Input;
using DroneLibrary;

namespace DroneControl {
    public partial class FlightControl : UserControl {

        private Drone drone;

        private InputController inputController;

        public FlightControl() {

            InitializeComponent();
            

            foreach(string s in Enum.GetNames(typeof(InputInterpreterType)))
                inputTypeComboBox.Items.Add(s);

            inputTypeComboBox.SelectedIndex = 0;

            maxPitchNumeric.ValueChanged += OnTiltLimitInputChange;
            maxRollNumeric.ValueChanged += OnTiltLimitInputChange;
            maxYawNumeric.ValueChanged += OnTiltLimitInputChange;
        }

        public void Init(Drone drone) {
            if(drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.drone = drone;
            DeviceInputInterpreter interpreter;
            switch((InputInterpreterType)Enum.GetValues(typeof(InputInterpreterType)).GetValue(inputTypeComboBox.SelectedIndex)) {
                case InputInterpreterType.GamePad:
                    interpreter = new GamepadInputInterpreter();
                    break;
                default:
                    return;
            }

            this.inputController = new InputController(drone, interpreter);
            this.inputController.DeviceInterpreter.TargetMovementChange += DeviceInterpreterOnTargetMovementChange;
            OnTiltLimitInputChange(this, EventArgs.Empty);
        }
        

        private void DeviceInterpreterOnTargetMovementChange(object sender, EventArgs eventArgs) {
            if(InvokeRequired) {
                Invoke(new EventHandler(DeviceInterpreterOnTargetMovementChange), this, eventArgs);
                return;
            }
            TargetMovementData target = inputController.DeviceInterpreter.TargetMovementData;

            targetPitchLabel.Text = $"Target Pitch: {target.TargetPitch}";
            targetRollLabel.Text = $"Target Roll: {target.TargetRoll}";
            targetYawLabel.Text = $"Target Yaw: {target.TargetYaw}";
            targetThrustLabel.Text = $"Target Thrust: {target.TargetThrust}";
        }

        private void OnTiltLimitInputChange(object sender, EventArgs e) {
            inputController.DeviceInterpreter.MaxPitch = (float)maxPitchNumeric.Value;
            inputController.DeviceInterpreter.MaxRoll = (float)maxRollNumeric.Value;
            inputController.DeviceInterpreter.MaxYaw = (float)maxYawNumeric.Value;
        }

        private void activeCheckBox_CheckedChanged(object sender, EventArgs e) {
            if(activeCheckBox.Checked) {
                inputTypeComboBox.Enabled = false;
                inputController.Start();
            } else {
                inputTypeComboBox.Enabled = true;
                inputController.Stop();
            }
        }
    }
}
