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
        private bool changingValues = false;

        public MotorControl()
        {
            InitializeComponent();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (drone != null)
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

        private void OnDroneDataChange(object sender, DataChangedEventArgs args)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DataChangedEventArgs>(OnDroneDataChange), sender, args);
                return;
            }

            QuadMotorValues motorValues = args.Data.MotorValues;

            if (!leftFrontTextBox.Focused)
                leftFrontTextBox.Text = motorValues.FrontLeft.ToString();

            if (!rightFrontTextBox.Focused)
                rightFrontTextBox.Text = motorValues.FrontRight.ToString();

            if (!leftBackTextBox.Focused)
                leftBackTextBox.Text = motorValues.BackLeft.ToString();

            if (!rightBackTextBox.Focused)
                rightBackTextBox.Text = motorValues.BackRight.ToString();
        }

        private void setValuesButton_Click(object sender, EventArgs e)
        {
            if (!SendValues())
                MessageBox.Show("The drone has to be armed, before setting the motors!");
        }

        private bool SendValues()
        {
            ushort leftFront = ushort.Parse(leftFrontTextBox.Text);
            ushort rightFront = ushort.Parse(rightFrontTextBox.Text);
            ushort leftBack = ushort.Parse(leftBackTextBox.Text);
            ushort rightBack = ushort.Parse(rightBackTextBox.Text);

            servoValueNumericUpDown.Value = (leftFront + rightFront + leftBack + rightBack) / 4;
            if (drone.IsConnected && drone.Data.State == DroneState.Armed)
            {
                drone.SendPacket(
                    new PacketSetRawValues(new QuadMotorValues(leftFront, rightFront, leftBack, rightBack)), true);
                return true;
            }
            return false;
        }

        private void OnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !changingValues)
                SendValues();
        }

        private void servoValueNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                changingValues = true;
                leftFrontTextBox.Text = servoValueNumericUpDown.Value.ToString();
                rightFrontTextBox.Text = servoValueNumericUpDown.Value.ToString();
                leftBackTextBox.Text = servoValueNumericUpDown.Value.ToString();
                rightBackTextBox.Text = servoValueNumericUpDown.Value.ToString();
                changingValues = false;

                SendValues();
            }
        }
    }
}
