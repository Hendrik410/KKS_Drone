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
            switch ((InputInterpreterType)inputTypeComboBox.SelectedIndex)
            {
                case InputInterpreterType.GamePad:
                    interpreter = new GamepadInputInterpreter();
                    break;
                default:
                    return;
            }

            this.inputController = new InputController(drone, interpreter);
            this.inputController.DeviceInterpreter.TargetMovementChange += DeviceInterpreterOnTargetMovementChange;
            OnTiltLimitInputChange(this, EventArgs.Empty);

            this.drone.OnDataChange += Drone_OnDataChange;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (drone != null)
                drone.OnDataChange -= Drone_OnDataChange;
            base.OnHandleDestroyed(e);
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DataChangedEventArgs>(Drone_OnDataChange), this, e);
                return;
            }

            UpdateTargetRatio(e.Data);
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

            UpdateTargetRatio(drone.Data);
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

        private void UpdateTargetRatio(DroneData data)
        {
            TargetMovementData target = inputController.DeviceInterpreter.TargetMovementData;
            float deltaPitch = target.TargetPitch - data.Gyro.Pitch;
            float deltaRoll = target.TargetRoll - data.Gyro.Roll;
            float deltaYaw = 0; //AngleDifference(data.Gyro.Yaw, target.TargetYaw);

            ratioDataLabel.Text = string.Format("FL: {0:0.00}\nFR: {1:0.00}\nBL: {2:0.00}\nBR: {3:0.00}",
                GetTargetRatio(true, true, false, deltaPitch, deltaRoll, deltaYaw, target.TargetThrust),
                GetTargetRatio(true, false, true, deltaPitch, deltaRoll, deltaYaw, target.TargetThrust),
                GetTargetRatio(false, true, true, deltaPitch, deltaRoll, deltaYaw, target.TargetThrust),
                GetTargetRatio(false, false, false, deltaPitch, deltaRoll, deltaYaw, target.TargetThrust));

        }

        private float AngleDifference(float a, float b)
        {
            return ((((a - b) % 360) + 540) % 360) - 180;
        }

        private float GetTargetRatio(bool isFront, bool isLeft, bool isClockwise, float pitchDelta, float rollDelta, float yawDelta, float verticalRatio)
        {
            float targetMotorRatio = verticalRatio;

            if (Math.Abs(pitchDelta) >= 0.02)
            {
                if (isFront)
                    targetMotorRatio += pitchDelta * drone.Settings.Degree2Ratio;
                else
                    targetMotorRatio -= pitchDelta * drone.Settings.Degree2Ratio;
            }

            if (Math.Abs(rollDelta) >= 0.02)
            {
                if (isLeft)
                    targetMotorRatio -= rollDelta * drone.Settings.Degree2Ratio;
                else
                    targetMotorRatio += rollDelta * drone.Settings.Degree2Ratio;
            }

            if (Math.Abs(yawDelta) >= 0.02)
            {
                if (isClockwise)
                    targetMotorRatio -= yawDelta * drone.Settings.RotaryDegree2Ratio;
                else
                    targetMotorRatio += yawDelta * drone.Settings.RotaryDegree2Ratio;
            }

            return targetMotorRatio;
        }
    }
}
