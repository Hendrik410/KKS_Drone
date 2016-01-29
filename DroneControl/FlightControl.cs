﻿using System;
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

            targetPitchLabel.Text = $"Target Pitch: {target.TargetPitch}";
            targetRollLabel.Text = $"Target Roll: {target.TargetRoll}";
            rotationalSpeedLabel.Text = $"Rotational Speed: {target.TargetRotationalSpeed}";
            targetThrustLabel.Text = $"Target Thrust: {target.TargetThrust}";

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
            ratioDataLabel.Text = string.Format("FL: {0:0.00} ({4:0.00})\nFR: {1:0.00} ({5:0.00})\nBL: {2:0.00} ({6:0.00})\nBR: {3:0.00} ({7:0.00})",
                ratios[0], ratios[1], ratios[2], ratios[3],
                actualMotorRatios.FrontLeft, actualMotorRatios.FrontRight, actualMotorRatios.BackLeft, actualMotorRatios.BackRight);

            if (OnRatioChanged != null)
                OnRatioChanged(this, ratios);
        }
    }
}
