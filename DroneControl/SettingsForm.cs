using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using DroneLibrary;

namespace DroneControl
{
    public partial class SettingsForm : Form
    {
        private Drone drone;
        private DroneInfo info;
        private DroneSettings data;

        private List<Binding> bindings = new List<Binding>();

        public SettingsForm(Drone drone)
        {
            InitializeComponent();

            this.drone = drone;
            this.info = drone.Info;
            this.data = drone.Settings;

            Bind(nameTextBox, data, nameof(data.DroneName));

            Bind(firmwareVersionTextBox, info, nameof(info.BuildVersion));
            Bind(buildDateTextBox, info, nameof(info.BuildName));

            Bind(modelTextBox, info, nameof(info.ModelName));
            Bind(idTextBox, info, nameof(info.SerialCode));
            Bind(gyroSensorTextBox, info, nameof(info.GyroSensor));
            Bind(magnetometerTextBox, info, nameof(info.Magnetometer));

            Bind(minValueTextBox, data, nameof(data.ServoMin));
            Bind(idleValueTextBox, data, nameof(data.ServoIdle));
            Bind(hoverValueTextBox, data, nameof(data.ServoHover));
            Bind(maxValueTextBox, data, nameof(data.ServoMax));

            Bind(safeMotorValueTextBox, data, nameof(data.SafeServoValue));
            Bind(safeTemperatureTextBox, data, nameof(data.MaxTemperature));
            Bind(safePitchTextBox, data, nameof(data.SafePitch));
            Bind(safeRollTextBox, data, nameof(data.SafeRoll));

            Bind(pitchKpTextBox, data.PitchPid, nameof(data.PitchPid.Kp));
            Bind(pitchKiTextBox, data.PitchPid, nameof(data.PitchPid.Ki));
            Bind(pitchKdTextBox, data.PitchPid, nameof(data.PitchPid.Kd));

            Bind(rollKpTextBox, data.RollPid, nameof(data.RollPid.Kp));
            Bind(rollKiTextBox, data.RollPid, nameof(data.RollPid.Ki));
            Bind(rollKdTextBox, data.RollPid, nameof(data.RollPid.Kd));

            Bind(yawKpTextBox, data.YawPid, nameof(data.YawPid.Kp));
            Bind(yawKiTextBox, data.YawPid, nameof(data.YawPid.Ki));
            Bind(yawKdTextBox, data.YawPid, nameof(data.YawPid.Kd));

            Bind(thrustValue, data, nameof(data.ServoThrust));
        }

        private void Bind(Control control, object data, string member)
        {
            bindings.Add(new Binding(control, data, member));
        }

        private class Binding
        {
            public Control Control { get; private set; }
            public object Data { get; private set; }
            public string DataMember { get; private set; }

            private PropertyInfo property;
            private FieldInfo field;

            private PropertyInfo controlProperty;

            private bool ignoreChange;

            public Binding(Control control, object data, string dataMember)
            {
                this.Control = control;
                this.Data = data;
                this.DataMember = dataMember;

                property = data.GetType().GetProperty(dataMember);
                if (property == null)
                    field = data.GetType().GetField(dataMember);

                if (property != null)
                    Control.Enabled = (property.SetMethod?.IsPublic).GetValueOrDefault();

                if (control is TextBox)
                {
                    controlProperty = control.GetType().GetProperty("Text");
                    (control as TextBox).TextChanged += (s, e) => UpdateData();
                }
                else if (control is NumericUpDown)
                {
                    controlProperty = control.GetType().GetProperty("Value");
                    (control as NumericUpDown).ValueChanged += (s, e) => UpdateData();
                }
                else
                    throw new NotSupportedException("Control type not supported");

                NotifyValueChanged();
            }

            public void UpdateData()
            {
                if (ignoreChange)
                    return;


                object value = controlProperty.GetValue(Control);
                if (property != null)
                    property.SetValue(Data, value);
                if (field != null)
                    field.SetValue(Data, value);
            }

            public void NotifyValueChanged()
            {
                ignoreChange = true;

                object value;
                if (property != null)
                    value = property.GetValue(Data);
                else if (field != null)
                    value = field.GetValue(Data);
                else
                    return;


                if (controlProperty.PropertyType == typeof(string))
                    controlProperty.SetValue(Control, value.ToString());
                else if (controlProperty.PropertyType == typeof(decimal))
                    controlProperty.SetValue(Control, Convert.ToDecimal(value));
                else
                    controlProperty.SetValue(Control, value);
                ignoreChange = false;
            }
        }

        private void updateFirmwareButton_Click(object sender, EventArgs e)
        {
            new UpdateOTAForm(drone).Show(this);
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            drone.SendReset();
        }
    }
}
