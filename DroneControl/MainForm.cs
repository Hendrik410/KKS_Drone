using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using DroneLibrary;
using DroneLibrary.Protocol;

namespace DroneControl
{
    public partial class MainForm : Form
    {
        private Drone drone;

        private LogForm logForm;

        public MainForm(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            InitializeComponent();

            this.drone = drone;

            drone.SendPacket(new PacketCalibrateGyro(), true);
            drone.SendPacket(new PacketSubscribeDataFeed(), true);

            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            timer.Start();

            drone.OnInfoChange += Drone_OnInfoChange;
            drone.OnPingChange += Drone_OnPingChange;
            drone.OnDataChange += Drone_OnDataChange;
            drone.OnSettingsChange += Drone_OnSettingsChange;
            motorControl1.UpdateDrone(drone);
            flightControl1.UpdateDrone(drone);

            ipInfoLabel.Text = string.Format(ipInfoLabel.Text, drone.Address);
            UpdatePing();
            UpdateInfo();
            UpdateData();
        }


        protected override void OnClosed(EventArgs e)
        {
            timer.Stop();

            if (drone != null)
            {
                drone.Disconnect();
                drone.Dispose();
            }

            Application.Exit();
            base.OnClosed(e);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            drone.SendPing();
            drone.SendGetInfo();
        }

        private Form ShowForm(Form form, Func<Form> onClosed)
        {
            if (onClosed == null)
                throw new ArgumentNullException(nameof(onClosed));

            if (form == null || form.IsDisposed)
                form = onClosed();

            if (!form.Visible)
                form.Show(this);
            form.BringToFront();
            return form;
        }

        private void UpdatePing()
        {
            if (!drone.IsConnected)
                pingLabel.Text = "Not connected";
            else
                pingLabel.Text = string.Format("Ping: {0}ms", drone.Ping);

            if (!drone.IsConnected || drone.Ping > 50)
                pingLabel.ForeColor = Color.Red;
            else
                pingLabel.ForeColor = Color.Green;
        }

        private void UpdateInfo()
        {
            infoPropertyGrid.SelectedObject = drone.Info;
        }

        private void Drone_OnInfoChange(object sender, EventArgs eventArgs)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new EventHandler(Drone_OnInfoChange), this, eventArgs);
                return;
            }

            UpdateInfo();
        }

        private void Drone_OnDataChange(object sender, EventArgs eventArgs)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new EventHandler(Drone_OnDataChange), this, eventArgs);
                return;
            }

            UpdateData();
        }

        private void Drone_OnSettingsChange(object sender, EventArgs eventArgs)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                Invoke(new EventHandler(Drone_OnSettingsChange), this, eventArgs);
                return;
            }

            droneConfigPropertyGrid.SelectedObject = drone.Settings;
        }

        private void UpdateData()
        {
            if (!drone.IsConnected)
            {
                statusArmedLabel.Text = "Status: not connected";
                armToogleButton.Enabled = false;
            }
            else
            {
                armToogleButton.Enabled = true;

                

                if (drone.Data.State == DroneState.Armed)
                {
                    armToogleButton.Text = "Disarm";
                }
                else
                {
                    armToogleButton.Text = "Arm";
                }

                statusArmedLabel.Text = $"Status: {drone.Data.State}";

                artificialHorizon.SetAttitudeIndicatorParameters(drone.Data.Gyro.Pitch, drone.Data.Gyro.Roll);
                headingIndicator.SetHeadingIndicatorParameters((int)drone.Data.Gyro.Yaw);

                accelerationLabel.Text = string.Format("Acceleration x: {0:0.00} y: {1:0.00} z: {2:0.00}",
                    drone.Data.Gyro.AccelerationX,
                    drone.Data.Gyro.AccelerationY,
                    drone.Data.Gyro.AccelerationZ);
                temperatureLabel.Text = string.Format("Temperature: {0:0.0}°C", drone.Data.Gyro.Temperature);
            }
        }

        private void Drone_OnPingChange(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;

            if (pingLabel.InvokeRequired)
                pingLabel.Invoke(new EventHandler(Drone_OnPingChange), sender, e);
            else
                UpdatePing();
        }


        private void armToogleButton_Click(object sender, EventArgs e)
        {
            if (!drone.IsConnected)
                return;

            if (drone.Data.State == DroneState.Armed)
                drone.SendDisarm();
            else
                drone.SendArm();
        }

        private void calibrateGyroButton_Click(object sender, EventArgs e)
        {
            drone.SendPacket(new PacketCalibrateGyro(), true);
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            ShowForm(logForm, () => (logForm = new LogForm(drone)));
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            drone.SendStop();
        }

        private void resetButton_Click(object sender, EventArgs e) {
            drone.SendReset();
        }

        private void droneConfigPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            drone.SendConfig((DroneSettings)droneConfigPropertyGrid.SelectedObject);
        }
    }
}
