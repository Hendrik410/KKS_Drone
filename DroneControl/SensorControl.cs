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

        private void Drone_OnDataChange(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(Drone_OnDataChange), sender, e);
                return;
            }

            artificialHorizon.SetAttitudeIndicatorParameters(Drone.Data.Gyro.Pitch, Drone.Data.Gyro.Roll);
            headingIndicator.SetHeadingIndicatorParameters((int)Drone.Data.Gyro.Yaw);

            accelerationLabel.Text = string.Format("Acceleration x: {0:0.00} y: {1:0.00} z: {2:0.00}",
                Drone.Data.Gyro.AccelerationX,
                Drone.Data.Gyro.AccelerationY,
                Drone.Data.Gyro.AccelerationZ);
            temperatureLabel.Text = string.Format("Temperature: {0:0.0}°C", Drone.Data.Gyro.Temperature);
        }
    }
}
