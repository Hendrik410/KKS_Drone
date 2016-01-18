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
    public partial class MotorControl : UserControl
    {
        private Drone drone;

        public MotorControl()
        {
            InitializeComponent();
        }

        public void UpdateDrone(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.drone = drone;
            drone.OnInfoChange += OnDroneInfoChange;
        }

        private void OnDroneInfoChange(object sender, EventArgs args) {
            QuadMotorValues motorValues = drone.Info.MotorValues;

            if(leftFrontTextBox.InvokeRequired)
                leftFrontTextBox.Invoke(new EventHandler(OnDroneInfoChange), sender, args);
            else
                leftFrontTextBox.Text = $"{motorValues.FrontLeft}";

            if(rightFrontTextBox.InvokeRequired)
                rightFrontTextBox.Invoke(new EventHandler(OnDroneInfoChange), sender, args);
            else
                rightFrontTextBox.Text = $"{motorValues.FrontRight}";

            if(leftBackTextBox.InvokeRequired)
                leftBackTextBox.Invoke(new EventHandler(OnDroneInfoChange), sender, args);
            else
                leftBackTextBox.Text = $"{motorValues.BackLeft}";

            if(rightBackTextBox.InvokeRequired)
                rightBackTextBox.Invoke(new EventHandler(OnDroneInfoChange), sender, args);
            else
                rightBackTextBox.Text = $"{motorValues.BackRight}";
        }

        private void setValuesButton_Click(object sender, EventArgs e) {
            if(drone.Info.IsArmed)
                drone.SendPacket(new PacketSetRawValues(new QuadMotorValues((ushort)servoValueNumericUpDown.Value)),
                    true);
            else
                MessageBox.Show("The drone has to be armed, before setting the motors!");
        }
    }
}
