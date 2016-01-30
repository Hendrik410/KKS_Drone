using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroneLibrary;
using DroneLibrary.Protocol;

namespace DroneControl
{
    public partial class SensorControl : UserControl
    {
        public Drone Drone { get; private set; }

        public SensorControl()
        {
            InitializeComponent();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (this.Drone != null)
                this.Drone.OnDataChange -= Drone_OnDataChange;
            base.OnHandleDestroyed(e);
        }

        public void Init(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));
            this.Drone = drone;

            this.Drone.OnDataChange += Drone_OnDataChange;
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DataChangedEventArgs>(Drone_OnDataChange), sender, e);
                return;
            }

            if (!float.IsNaN(e.Data.Gyro.Pitch) && !float.IsNaN(e.Data.Gyro.Roll))
                artificialHorizon.SetAttitudeIndicatorParameters(e.Data.Gyro.Pitch, e.Data.Gyro.Roll);
            if (!float.IsNaN(e.Data.Gyro.Yaw))
                headingIndicator.SetHeadingIndicatorParameters((int)e.Data.Gyro.Yaw);

            calibrateGyroButton.Enabled = e.Data.State != DroneState.Armed && e.Data.State != DroneState.Flying;

            gyroDataLabel.Text = string.Format("Roll: {0} Pitch: {1} Yaw: {2}",
                Formatting.FormatDecimal(e.Data.Gyro.Roll, 2),
                Formatting.FormatDecimal(e.Data.Gyro.Pitch, 2),
                Formatting.FormatDecimal(e.Data.Gyro.Yaw, 2));

            accelerationLabel.Text = string.Format("Acceleration x: {0} y: {1} z: {2}",
                Formatting.FormatDecimal(e.Data.Gyro.AccelerationX / 100, 2),
                Formatting.FormatDecimal(e.Data.Gyro.AccelerationY / 100, 2),
                Formatting.FormatDecimal(e.Data.Gyro.AccelerationZ / 100, 2));

            temperatureLabel.Text = string.Format("Temperature: {0}°C",
                Formatting.FormatDecimal(e.Data.Gyro.Temperature, 2));

            batteryVoltageLabel.Text = string.Format("Battery voltage: {0} V",
                Formatting.FormatDecimal(e.Data.BatteryVoltage, 2));
        }

        private void calibrateGyroButton_Click(object sender, EventArgs e)
        {
            Drone.SendPacket(new PacketCalibrateGyro(), true);
        }
    }
}
