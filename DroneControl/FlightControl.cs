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
        public InputManager InputManager { get; private set; }

        public FlightControl()
        {
            InitializeComponent();
        }

        public void Init(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.drone = drone;
            this.InputManager = new InputManager(drone);
            this.InputManager.OnDeviceInfoChanged += InputManager_OnDeviceInfoChanged;
            this.InputManager.OnTargetDataChanged += InputManager_OnTargetDataChanged;

            this.drone.OnDebugDataChange += Drone_OnDebugDataChange;

            SearchInputDevices();
            UpdateTargetData();
            UpdateDeviceInfo();
            UpdateInputConfig();
        }

        private void Drone_OnDebugDataChange(object sender, DebugDataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DebugDataChangedEventArgs>(Drone_OnDebugDataChange), sender, e);
                return;
            }
            StringBuilder pidData = new StringBuilder();
            pidData.AppendFormat("Pitch: {0}", e.DebugData.PitchOutput.ToString().PadLeft(5));
            pidData.AppendLine();
            pidData.AppendFormat("Roll:  {0}", e.DebugData.RollOutput.ToString().PadLeft(5));
            pidData.AppendLine();
            pidData.AppendFormat("Yaw:   {0}", e.DebugData.YawOutput.ToString().PadLeft(5));

            pidDataLabel.Text = pidData.ToString();
        }

        private void InputManager_OnTargetDataChanged(object sender, EventArgs e)
        {
            UpdateTargetData();
        }

        private void InputManager_OnDeviceInfoChanged(object sender, EventArgs e)
        {
            UpdateDeviceInfo();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (InputManager != null)
                InputManager.Dispose();
            base.OnHandleDestroyed(e);
        }

        private void searchTimer_Tick(object sender, EventArgs e)
        {
			if (DesignMode)
				return;
            SearchInputDevices();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
			if (DesignMode)
				return;
			
			if (InputManager != null)
				InputManager.Update();
        }

        private void searchDeviceButton_Click(object sender, EventArgs e)
        {
            SearchInputDevices();
        }

        private void inputDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // "None" ist String, daher gibt "as" bei "None" null zurück
            InputManager.CurrentDevice = inputDeviceComboBox.SelectedItem as IInputDevice;
            UpdateDeviceInfo();
        }


        private void OnInputConfigChange(object sender, EventArgs e)
        {
            UpdateInputConfig();
        }

        private void SearchInputDevices()
        {
            bool changed;
            IInputDevice[] devices = InputManager.FindDevices(out changed);

            // schauen ob die Geräte sich nicht verändert haben und die Liste schon erstellt wurde
            if (!changed && inputDeviceComboBox.Items.Count > 0)
                return;

            // Liste erstellen
            inputDeviceComboBox.Items.Clear();
            inputDeviceComboBox.Items.Add("\nNone"); // \n damit der Eintrag ganz am Anfang kommt
            inputDeviceComboBox.Items.AddRange(devices);

            // Gerät auswählen
            if (InputManager.CurrentDevice == null)
                inputDeviceComboBox.SelectedIndex = 0;
            else
                inputDeviceComboBox.SelectedItem = InputManager.CurrentDevice;
        }


        private void UpdateDeviceInfo()
        {
            if(InputManager.CurrentDevice == null)
            {
                deviceConnectionLabel.Text = "No device selected";
                deviceConnectionLabel.ForeColor = SystemColors.ControlText;
                deviceBatteryLabel.Visible = false;
                return;
            }

            if (InputManager.CurrentDevice.IsConnected)
            {
                deviceConnectionLabel.Text = "Device connected";
                deviceConnectionLabel.ForeColor = Color.Green;

                if (InputManager.CurrentDevice.Battery.HasBattery)
                {
                    deviceBatteryLabel.Visible = true;
                    deviceBatteryLabel.Text = string.Format("Battery: {0}", InputManager.CurrentDevice.Battery.Level);

                    switch(InputManager.CurrentDevice.Battery.Level)
                    {
                        case BatteryLevel.Empty:
                            deviceBatteryLabel.ForeColor = Color.DarkRed;
                            break;
                        case BatteryLevel.Low:
                            deviceBatteryLabel.ForeColor = Color.Red;
                            break;
                        case BatteryLevel.Medium:
                            deviceBatteryLabel.ForeColor = Color.Orange;
                            break;
                        case BatteryLevel.Full:
                            deviceBatteryLabel.ForeColor = Color.Green;
                            break;
                    }
                   
                }
                else
                    deviceBatteryLabel.Visible = false;
            }
            else
            {
                deviceConnectionLabel.Text = "Device disconnected";
                deviceConnectionLabel.ForeColor = Color.Red;

                deviceBatteryLabel.Visible = false;
            }
        }

        private void UpdateTargetData()
        {
            bool deviceConnected = InputManager.CurrentDevice != null && InputManager.CurrentDevice.IsConnected;
            pitchLabel.Visible = deviceConnected;
            rollLabel.Visible = deviceConnected;
            rotationalSpeedLabel.Visible = deviceConnected;
            thrustLabel.Visible = deviceConnected;

            if (deviceConnected)
            {
                pitchLabel.Text = string.Format("Pitch: {0}",
                    Formatting.FormatDecimal(InputManager.TargetData.Pitch, 2, 2));
                rollLabel.Text = string.Format("Roll: {0}",
                    Formatting.FormatDecimal(InputManager.TargetData.Roll, 2, 2));
                rotationalSpeedLabel.Text = string.Format("Rotational Speed: {0}",
                    Formatting.FormatDecimal(InputManager.TargetData.RotationalSpeed, 2, 2));
                thrustLabel.Text = string.Format("Thrust: {0}",
                    Formatting.FormatDecimal(InputManager.TargetData.Thurst, 2, 2));
            }
        }

        private void UpdateInputConfig()
        {
            InputManager.MaxPitch = (float)maxPitchNumeric.Value;
            InputManager.MaxRoll = (float)maxRollNumeric.Value;
            InputManager.MaxRotationalSpeed = (float)maxRotationalSpeedNumeric.Value;

            InputManager.PitchOffset = (float)pitchOffsetNumeric.Value;
            InputManager.RollOffset = (float)rollOffsetNumeric.Value;
            InputManager.RotationalOffset = (float)rotationalOffsetNumeric.Value;

            InputManager.MaxThrustPositive = (float)thrustPositiveNumeric.Value;
            InputManager.MaxThrustNegative = (float)thrustNegativeNumeric.Value;

            InputManager.DeadZone = deadZoneCheckBox.Checked;

            if (invertPitchCheckBox.Checked)
                InputManager.MaxPitch *= -1;
            if (invertRollCheckBox.Checked)
                InputManager.MaxRoll *= -1;
            if (invertRotationalCheckBox.Checked)
                InputManager.MaxRotationalSpeed *= -1;
            InputManager.InvertThrust = invertThrustCheckBox.Checked;
        }
    }
}
