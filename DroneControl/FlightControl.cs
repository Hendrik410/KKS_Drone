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

namespace DroneControl
{
    public partial class FlightControl : UserControl
    {

        private Drone drone;

        private InputController inputController;
        private LinearTargetRatio targetRatio;

        public event EventHandler<float[]> OnRatioChanged;

        public FlightControl()
        {
            InitializeComponent();

            foreach (string s in Enum.GetNames(typeof(InputInterpreterType)))
                inputTypeComboBox.Items.Add(s);

            inputTypeComboBox.SelectedIndex = 0;

            maxPitchNumeric.ValueChanged += OnTiltLimitInputChange;
            maxRollNumeric.ValueChanged += OnTiltLimitInputChange;
            maxRotationSpeedNumeric.ValueChanged += OnTiltLimitInputChange;

            targetRatio = new LinearTargetRatio();
        }

        public void Init(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.drone = drone;

            OnTiltLimitInputChange(this, EventArgs.Empty);

            this.drone.OnDataChange += Drone_OnDataChange;

            UpdateTarget();
        }

        public void Close()
        {
            if (inputController != null)
                inputController.Stop();
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

        private void DeviceInterpreterOnTargetMovementChange(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(DeviceInterpreterOnTargetMovementChange), this, e);
                return;
            }

            UpdateTarget();
        }

        private void UpdateTarget()
        {
            TargetMovementData target = new TargetMovementData();
            
            if (inputController != null)
                target = inputController.DeviceInterpreter.TargetMovementData;

            targetPitchLabel.Text = "Target Pitch: " + Formatting.FormatDecimal(target.TargetPitch, 2);
            targetRollLabel.Text = "Target Roll: " + Formatting.FormatDecimal(target.TargetRoll, 2);
            rotationalSpeedLabel.Text = "Rotational Speed: " + Formatting.FormatDecimal(target.TargetRotationalSpeed, 2);
            targetThrustLabel.Text = "Target Thrust: " + Formatting.FormatDecimal(target.TargetThrust, 2);

            UpdateTargetRatio(drone.Data);
        }

        private void OnTiltLimitInputChange(object sender, EventArgs e)
        {
            if (inputController == null) return;

            inputController.DeviceInterpreter.MaxPitch = (float)maxPitchNumeric.Value;
            inputController.DeviceInterpreter.MaxRoll = (float)maxRollNumeric.Value;
            inputController.DeviceInterpreter.MaxYaw = (float)maxRotationSpeedNumeric.Value;
            inputController.DeviceInterpreter.PitchOffset = (float)pitchOffsetTextBox.Value;
            inputController.DeviceInterpreter.RollOffset = (float)rollOffsetTextBox.Value;
            inputController.DeviceInterpreter.RotationalSpeedOffset = (float)rotationalSpeedOffsetTextBox.Value;
        }

        private void activeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (activeCheckBox.Checked)
            {
                inputTypeComboBox.Enabled = false;

                DeviceInputInterpreter interpreter;
                switch ((InputInterpreterType)inputTypeComboBox.SelectedIndex)
                {
                    case InputInterpreterType.GamePad:
                        interpreter = new GamepadInputInterpreter();
                        break;
                    case InputInterpreterType.PrecisionController:
                        interpreter = new PrecisionControllerInputInterpreter();
                        break;
                    case InputInterpreterType.Xbox360:
                        interpreter = new Xbox360InputInterpreter();
                        break;
                    default:
                        return;
                }

                this.inputController = new InputController(drone, interpreter);
                this.inputController.DeviceInterpreter.TargetMovementChange += DeviceInterpreterOnTargetMovementChange;
                OnTiltLimitInputChange(this, EventArgs.Empty);

                inputController.Start();
            }
            else {
                inputTypeComboBox.Enabled = true;
                inputController.Stop();
            }
        }

        private void UpdateTargetRatio(DroneData data)
        {
            if (inputController == null || inputController.DeviceInterpreter == null)
                return;

            TargetMovementData target = inputController.DeviceInterpreter.TargetMovementData;
            MotorRatios actualMotorRatios = data.MotorRatios;

            float[] ratios = targetRatio.Calculate(drone.Settings, target, data);
            ratioDataLabel.Text = string.Format("FL: {0} ({4})\nFR: {1} ({5})\nBL: {2} ({6})\nBR: {3} ({7})",
                Formatting.FormatRatio(ratios[0]),
                Formatting.FormatRatio(ratios[1]),
                Formatting.FormatRatio(ratios[2]),
                Formatting.FormatRatio(ratios[3]),

                Formatting.FormatRatio(actualMotorRatios.FrontLeft),
                Formatting.FormatRatio(actualMotorRatios.FrontRight),
                Formatting.FormatRatio(actualMotorRatios.BackLeft),
                Formatting.FormatRatio(actualMotorRatios.BackRight));

            if (OnRatioChanged != null)
                OnRatioChanged(this, ratios);
        }
    }
}
