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
            drone.OnSettingsChange += Drone_OnSettingsChange;
            
            UpdateValueBounds(drone.Settings);
            UpdateServoValue();
        }

        private void Drone_OnSettingsChange(object sender, SettingsChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<SettingsChangedEventArgs>(Drone_OnSettingsChange), sender, e);
                return;
            }

            UpdateValueBounds(e.Settings);
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

            UpdateServoValue();
        }

        private void UpdateValueBounds(DroneSettings settings)
        {
            leftFrontTextBox.Minimum = settings.ServoMin;
            rightFrontTextBox.Minimum = settings.ServoMin;
            leftBackTextBox.Minimum = settings.ServoMin;
            rightBackTextBox.Minimum = settings.ServoMin;
            servoValueNumericUpDown.Minimum = settings.ServoMin;
            valueTrackBar.Minimum = settings.ServoMin;

            leftFrontTextBox.Maximum = settings.ServoMax;
            rightFrontTextBox.Maximum = settings.ServoMax;
            leftBackTextBox.Maximum = settings.ServoMax;
            rightBackTextBox.Maximum = settings.ServoMax;
            servoValueNumericUpDown.Maximum = settings.ServoMax;
            valueTrackBar.Maximum = settings.ServoMax;
        }

        private void setValuesButton_Click(object sender, EventArgs e)
        {
            if (!SendValues())
                MessageBox.Show("The drone has to be armed, before setting the motors!");
        }

        private void UpdateServoValue()
        {
            ushort leftFront = (ushort)leftFrontTextBox.Value;
            ushort rightFront = (ushort)rightFrontTextBox.Value;
            ushort leftBack = (ushort)leftBackTextBox.Value;
            ushort rightBack = (ushort)rightBackTextBox.Value;

            if (!servoValueNumericUpDown.Focused)
                servoValueNumericUpDown.Value = (leftFront + rightFront + leftBack + rightBack) / 4;

            if (!valueTrackBar.Focused)
                valueTrackBar.Value = (ushort)servoValueNumericUpDown.Value;
        }

        private bool SendValues()
        {
            ushort leftFront = (ushort)leftFrontTextBox.Value;
            ushort rightFront = (ushort)rightFrontTextBox.Value;
            ushort leftBack = (ushort)leftBackTextBox.Value;
            ushort rightBack = (ushort)rightBackTextBox.Value;

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
            {
                UpdateServoValue();
                SendValues();
            }
        }

        private void SetServoValueToAll()
        {
            changingValues = true;
            leftFrontTextBox.Value = servoValueNumericUpDown.Value;
            rightFrontTextBox.Value = servoValueNumericUpDown.Value;
            leftBackTextBox.Value = servoValueNumericUpDown.Value;
            rightBackTextBox.Value = servoValueNumericUpDown.Value;
            changingValues = false;
        }

        private void servoValueNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !changingValues)
            {          
                SetServoValueToAll();
                SendValues();
            }
        }

        private void valueTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (changingValues)
                return;

            changingValues = true;
            servoValueNumericUpDown.Value = valueTrackBar.Value;
            SetServoValueToAll();


            SendValues();
        }
    }
}
