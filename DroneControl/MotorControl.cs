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

        protected override void OnHandleDestroyed(EventArgs e)
        {
            drone.OnDataChange -= OnDroneDataChange;
            base.OnHandleDestroyed(e);
        }

        public void Init(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.drone = drone;
            drone.OnDataChange += OnDroneDataChange;
        }

        private void OnDroneDataChange(object sender, EventArgs args)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(OnDroneDataChange), sender, args);
                return;
            }

            QuadMotorValues motorValues = drone.Data.MotorValues;

            leftFrontTextBox.Text = $"{motorValues.FrontLeft}";
            rightFrontTextBox.Text = $"{motorValues.FrontRight}";
            leftBackTextBox.Text = $"{motorValues.BackLeft}";
            rightBackTextBox.Text = $"{motorValues.BackRight}";
        }

        private void setValuesButton_Click(object sender, EventArgs e)
        {
            if (!SendValues())
                MessageBox.Show("The drone has to be armed, before setting the motors!");
        }

        private bool SendValues()
        {
            if (drone.IsConnected && drone.Data.State == DroneState.Armed)
            {
                drone.SendPacket(
                    new PacketSetRawValues(new QuadMotorValues((ushort)servoValueNumericUpDown.Value)), true);
                return true;
            }
            return false;
        }

        private void servoValueNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendValues();
        }
    }
}
