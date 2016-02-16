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
        private InputManager inputManager;

        public FlightControl()
        {
            InitializeComponent();
        }

        public void Init(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.drone = drone;
            this.inputManager = new InputManager(drone);
            this.inputManager.OnDeviceInfoChanged += InputManager_OnDeviceInfoChanged;
            this.inputManager.OnTargetDataChanged += InputManager_OnTargetDataChanged;

            SearchInputDevices();
            UpdateTargetData();
            UpdateDeviceInfo();
            UpdateInputConfig();
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
            if (inputManager != null)
                inputManager.Dispose();
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
			
			if (inputManager != null)
				inputManager.Update();
        }

        private void searchDeviceButton_Click(object sender, EventArgs e)
        {
            SearchInputDevices();
        }

        private void inputDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // "None" ist String, daher gibt "as" bei "None" null zurück
            inputManager.CurrentDevice = inputDeviceComboBox.SelectedItem as IInputDevice;
            UpdateDeviceInfo();
        }


        private void OnInputConfigChange(object sender, EventArgs e)
        {
            UpdateInputConfig();
        }

        private void SearchInputDevices()
        {
            bool changed;
            IInputDevice[] devices = inputManager.FindDevices(out changed);

            // schauen ob die Geräte sich nicht verändert haben und die Liste schon erstellt wurde
            if (!changed && inputDeviceComboBox.Items.Count > 0)
                return;

            // Liste erstellen
            inputDeviceComboBox.Items.Clear();
            inputDeviceComboBox.Items.Add("\nNone"); // \n damit der Eintrag ganz am Anfang kommt
            inputDeviceComboBox.Items.AddRange(devices);

            // Gerät auswählen
            if (inputManager.CurrentDevice == null)
                inputDeviceComboBox.SelectedIndex = 0;
            else
                inputDeviceComboBox.SelectedItem = inputManager.CurrentDevice;
        }


        private void UpdateDeviceInfo()
        {
            if(inputManager.CurrentDevice == null)
            {
                deviceConnectionLabel.Text = "No device selected";
                deviceConnectionLabel.ForeColor = SystemColors.ControlText;
                deviceBatteryLabel.Visible = false;
                return;
            }

            if (inputManager.CurrentDevice.IsConnected)
            {
                deviceConnectionLabel.Text = "Device connected";
                deviceConnectionLabel.ForeColor = Color.Green;

                if (inputManager.CurrentDevice.Battery.HasBattery)
                {
                    deviceBatteryLabel.Visible = true;
                    deviceBatteryLabel.Text = string.Format("Battery: {0}", inputManager.CurrentDevice.Battery.Level);

                    switch(inputManager.CurrentDevice.Battery.Level)
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
            bool deviceConnected = inputManager.CurrentDevice != null && inputManager.CurrentDevice.IsConnected;
            pitchLabel.Visible = deviceConnected;
            rollLabel.Visible = deviceConnected;
            rotationalSpeedLabel.Visible = deviceConnected;
            thrustLabel.Visible = deviceConnected;

            if (deviceConnected)
            {
                pitchLabel.Text = string.Format("Pitch: {0}",
                    Formatting.FormatDecimal(inputManager.TargetData.Pitch, 2, 2));
                rollLabel.Text = string.Format("Roll: {0}",
                    Formatting.FormatDecimal(inputManager.TargetData.Roll, 2, 2));
                rotationalSpeedLabel.Text = string.Format("Rotational Speed: {0}",
                    Formatting.FormatDecimal(inputManager.TargetData.RotationalSpeed, 2, 2));
                thrustLabel.Text = string.Format("Thrust: {0}",
                    Formatting.FormatDecimal(inputManager.TargetData.Thurst, 2, 2));
            }
        }

        private void UpdateInputConfig()
        {
            inputManager.MaxPitch = (float)maxPitchNumeric.Value;
            inputManager.MaxRoll = (float)maxRollNumeric.Value;
            inputManager.MaxRotationalSpeed = (float)maxRotationalSpeedNumeric.Value;

            inputManager.PitchOffset = (float)pitchOffsetNumeric.Value;
            inputManager.RollOffset = (float)rollOffsetNumeric.Value;
            inputManager.RotationalOffset = (float)rotationalOffsetNumeric.Value;

            inputManager.MaxThrustPositive = (float)thrustPositiveNumeric.Value;
            inputManager.MaxThrustNegative = (float)thrustNegativeNumeric.Value;

            inputManager.DeadZone = deadZoneCheckBox.Checked;
        }
    }
}
