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

            Bind(nameTextBox, "data.DroneName");
            Bind(saveConfigCheckBox, "data.SaveConfig");

            Bind(firmwareVersionTextBox, "info.BuildVersion");
            Bind(buildDateTextBox, "info.BuildName");

            Bind(modelTextBox, "info.ModelName");
            Bind(idTextBox, "info.SerialCode");
            Bind(gyroSensorTextBox, "info.GyroSensor");
            Bind(magnetometerTextBox, "info.Magnetometer");

            Bind(minValueTextBox, "data.ServoMin");
            Bind(idleValueTextBox, "data.ServoIdle");
            Bind(hoverValueTextBox, "data.ServoHover");
            Bind(maxValueTextBox, "data.ServoMax");

            Bind(safeMotorValueTextBox, "data.SafeServoValue");
            Bind(safeTemperatureTextBox, "data.MaxTemperature");
            Bind(safePitchTextBox, "data.SafePitch");
            Bind(safeRollTextBox, "data.SafeRoll");

            Bind(pitchKpTextBox, "data.PitchPid.Kp");
            Bind(pitchKiTextBox, "data.PitchPid.Ki");
            Bind(pitchKdTextBox, "data.PitchPid.Kd");

            Bind(rollKpTextBox, "data.RollPid.Kp");
            Bind(rollKiTextBox, "data.RollPid.Ki");
            Bind(rollKdTextBox, "data.RollPid.Kd");

            Bind(yawKpTextBox, "data.YawPid.Kp");
            Bind(yawKiTextBox, "data.YawPid.Ki");
            Bind(yawKdTextBox, "data.YawPid.Kd");

            Bind(enableStabilizationCheckBox, "data.EnableStabilization");
            Bind(negativeMixingCheckBox, "data.NegativeMixing");
            Bind(keepMotorsOnCheckBox, "data.KeepMotorsOn");

            Bind(maxThrustForFlyingTextBox, "data.MaxThrustForFlying");
            Bind(onlyArmWhenStillCheckBox, "data.OnlyArmWhenStill");

            drone.OnSettingsChange += Drone_OnSettingsChange;
            drone.OnInfoChange += Drone_OnInfoChange;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            drone.OnSettingsChange -= Drone_OnSettingsChange;
            drone.OnInfoChange -= Drone_OnInfoChange;
            base.OnHandleDestroyed(e);
        }

        private void Drone_OnSettingsChange(object sender, SettingsChangedEventArgs e)
        {
            data = e.Settings;
            UpdateValues();
        }

        private void Drone_OnInfoChange(object sender, InfoChangedEventArgs e)
        {
            info = e.Info;
            UpdateValues();
        }

        private void UpdateValues()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateValues));
                return;
            }

            foreach (Binding binding in bindings)
                binding.NotifyValueChanged();
        }

        private void Bind(Control control, string member)
        {
            bindings.Add(new Binding(this, control, member));
        }

        private class Binding
        {
            public SettingsForm Settings { get; private set; }
            public Control Control { get; private set; }
            public string DataMember { get; private set; }

            private string[] memberParts;
            private PropertyInfo controlProperty;
            private bool ignoreChange;

            public bool ChangedByUser { get; private set; }

            public Binding(SettingsForm settings, Control control, string dataMember)
            {
                this.Settings = settings;
                this.Control = control;
                this.DataMember = dataMember;
                this.memberParts = dataMember.Split('.');

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
                else if (control is CheckBox)
                {
                    controlProperty = control.GetType().GetProperty("Checked");
                    (control as CheckBox).CheckedChanged += (s, e) => UpdateData();
                }
                else
                    throw new NotSupportedException("Control type not supported");

                NotifyValueChanged();
            }

            public void ClearChangedByUser()
            {
                ChangedByUser = false;
                Control.ForeColor = SystemColors.WindowText;
            }

            private object GetDataValue()
            {
                object current = Settings;
                bool isPublic = false;

                foreach(string member in memberParts)
                    current = GetValue(current, member, out isPublic);

                Control.Enabled = isPublic;
                return current;
            }

            private object GetValue(object data, string member, out bool isPublic)
            {
                Type type = data.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic;
                FieldInfo field = type.GetField(member, flags);
                if (field != null)
                {
                    isPublic = field.IsPublic;
                    return field.GetValue(data);
                }
                PropertyInfo property = type.GetProperty(member, flags);
                if (property != null)
                {
                    isPublic = property.SetMethod != null && property.SetMethod.IsPublic;
                    return property.GetValue(data);
                }
                throw new ArgumentException("Member not found", "member");
            }

            public void UpdateData()
            {
                if (ignoreChange)
                    return;

                try
                {
                    object value = controlProperty.GetValue(Control);

                    Stack<object> objects = new Stack<object>();
                    objects.Push(Settings);
                    foreach (string member in memberParts)
                    {
                        bool isPublic;
                        objects.Push(GetValue(objects.Peek(), member, out isPublic));
                    }

                    objects.Pop();
                    while (objects.Count > 0)
                    {
                        object obj = objects.Pop();
                        SetValue(obj, memberParts[objects.Count], value);
                        value = obj;
                    }

                    ChangedByUser = true;
                    Control.ForeColor = Color.DarkGreen;
                }
                catch(Exception e)
                {
                    Log.Error(e);
                    ForceValue();
                }
            }

            private void SetValue(object data, string member, object value)
            {
                Type type = data.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic;
                FieldInfo field = type.GetField(member, flags);
                if (field != null)
                {
                    field.SetValue(data, Convert.ChangeType(value, field.FieldType));
                    return;
                }
                PropertyInfo property = type.GetProperty(member, flags);
                if (property != null)
                {
                    property.SetValue(data, Convert.ChangeType(value, property.PropertyType));
                    return;
                }
                throw new ArgumentException("Member not found", "member");
            }

            public void NotifyValueChanged()
            {
                if (ChangedByUser)
                {
                    UpdateData();
                    return;
                }
                if (Control.Focused)
                    return;
                ForceValue();
            }

            private void ForceValue()
            { 
                ignoreChange = true;

                object value = GetDataValue();
                controlProperty.SetValue(Control, Convert.ChangeType(value, controlProperty.PropertyType));
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

        private void applyButton_Click(object sender, EventArgs e)
        {
            drone.SendConfig(data);
            foreach (Binding binding in bindings)
                binding.ClearChangedByUser();
        }
    }
}
