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
            {
                drone.OnDataChange -= OnDroneDataChange;
                drone.OnSettingsChange -= Drone_OnSettingsChange;
            }
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
            UpdateEnabled(drone.Data.State);
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

            SetServoValues(motorValues.FrontLeft, motorValues.FrontRight, motorValues.BackLeft, motorValues.BackRight);
            UpdateServoValue(motorValues.FrontLeft, motorValues.FrontRight, motorValues.BackLeft, motorValues.BackRight);
            UpdateEnabled(args.Data.State);
        }

        private void setValuesButton_Click(object sender, EventArgs e)
        {
            if (!SendValues())
                MessageBox.Show(this, "Setting the motors is only allowed when the drone is armed.", "Not armed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void UpdateServoValue()
        {
            int leftFront = (int)leftFrontTextBox.Value;
            int rightFront = (int)rightFrontTextBox.Value;
            int leftBack = (int)leftBackTextBox.Value;
            int rightBack = (int)rightBackTextBox.Value;
            UpdateServoValue(leftFront, rightFront, leftBack, rightBack);
        }

        private void UpdateServoValue(int leftFront, int rightFront, int leftBack, int rightBack)
        {
            int average = (leftFront + rightFront + leftBack + rightBack) / 4;

            changingValues = true;

            // in bestimmten Fällen kann es passieren, dass die DroneData Werte vor den Einstellungen
            // geschickt werden, dann stimmten die Min und Max Werte nicht mehr
            // wir müssen daher die Werte anpassen
            int max = (int)Math.Min(servoValueNumericUpDown.Maximum, valueTrackBar.Maximum);
            int min = (int)Math.Max(servoValueNumericUpDown.Minimum, valueTrackBar.Minimum);

            if (average > max)
                average = max;
            if (average < min)
                average = min;

            if (!servoValueNumericUpDown.Focused)
                servoValueNumericUpDown.Value = average;
            if (!valueTrackBar.Focused)
                valueTrackBar.Value = average;

            changingValues = false;
        }

        private void OnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !changingValues)
            {
                CheckNumericUpDown((NumericUpDown)sender);
                UpdateServoValue();
                SendValues();
            }
        }

        private void servoValueNumericUpDown_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !changingValues)
            {
                CheckNumericUpDown((NumericUpDown)sender);
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

        private void UpdateEnabled(DroneState state)
        {
            UpdateEnabled(state == DroneState.Armed, state != DroneState.Flying);
        }

        private void UpdateEnabled(bool enabled, bool enabledSet)
        {
            leftFrontTextBox.Enabled = enabled;
            rightFrontTextBox.Enabled = enabled;
            leftBackTextBox.Enabled = enabled;
            rightBackTextBox.Enabled = enabled;

            leftFrontTick.Enabled = enabled;
            rightFrontTick.Enabled = enabled;
            leftBackTick.Enabled = enabled;
            rightBackTick.Enabled = enabled;

            servoValueNumericUpDown.Enabled = enabled;
            valueTrackBar.Enabled = enabled;

            setValuesButton.Enabled = enabledSet;
        }

        private void UpdateValueBounds(DroneSettings settings)
        {
            changingValues = true;
            valueTrackBar.Minimum = settings.ServoMin;
            valueTrackBar.Maximum = settings.ServoMax;
            valueTrackBar.Value = settings.ServoMin;
            servoValueNumericUpDown.Value = settings.ServoMin;
            SetServoValueToAll();
            changingValues = false;
        }


        private bool SendValues()
        {
            if (!drone.IsConnected || drone.Data.State != DroneState.Armed)
                return false;

            if (!leftFrontTick.Checked)
                CheckNumericUpDown(leftFrontTextBox);
            if (!rightFrontTick.Checked)
                CheckNumericUpDown(rightFrontTextBox);
            if (!leftBackTick.Checked)
                CheckNumericUpDown(leftBackTextBox);
            if (!rightBackTick.Checked)
                CheckNumericUpDown(rightBackTextBox);

            ushort leftFront = (ushort)leftFrontTextBox.Value;
            ushort rightFront = (ushort)rightFrontTextBox.Value;
            ushort leftBack = (ushort)leftBackTextBox.Value;
            ushort rightBack = (ushort)rightBackTextBox.Value;

            if (leftFrontTick.Checked)
                leftFront = 1;
            if (rightFrontTick.Checked)
                rightFront = 1;
            if (leftBackTick.Checked)
                leftBack = 1;
            if (rightBackTick.Checked)
                rightBack = 1;


            drone.SendPacket(
                new PacketSetRawValues(new QuadMotorValues(leftFront, rightFront, leftBack, rightBack)), true);
            return true;
        }

        private void SetServoValueToAll()
        {
            int value = (int)servoValueNumericUpDown.Value;
            SetServoValues(value, value, value, value);
        }

        private void SetServoValues(int leftFront, int rightFront, int leftBack, int rightBack)
        {
            changingValues = true;
            if (!leftFrontTextBox.Focused)
                leftFrontTextBox.Value = leftFront;

            if (!rightFrontTextBox.Focused)
                rightFrontTextBox.Value = rightFront;

            if (!leftBackTextBox.Focused)
                leftBackTextBox.Value = leftBack;

            if (!rightBackTextBox.Focused)
                rightBackTextBox.Value = rightBack;

            if (!leftFrontTick.Focused)
                leftFrontTick.Checked = (leftFront == 1);

            if (!rightFrontTick.Focused)
                rightFrontTick.Checked = (rightFront == 1);

            if (!leftBackTick.Focused)
                leftBackTick.Checked = (leftBack == 1);

            if (!rightBackTick.Focused)
                rightBackTick.Checked = (rightBack == 1);

            changingValues = false;
        }

        private void CheckNumericUpDown(NumericUpDown box)
        {
            int max = Math.Min(drone.Settings.ServoMax, drone.Settings.SafeServoValue);
            if (box.Value < drone.Settings.ServoMin)
                box.Value = drone.Settings.ServoMin;
            if (box.Value > max)
                box.Value = max;
        }
    }
}
