using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroneLibrary;

namespace DroneControl
{
    public partial class GraphForm : Form
    {
        public Drone Drone { get; private set; }
        public FlightControl FlightControl { get; private set; }

        public GraphForm(Drone drone, FlightControl flightControl)
        {
            InitializeComponent();

            this.Drone = drone;
            this.Drone.OnSettingsChange += Drone_OnSettingsChange;
            this.Drone.OnDataChange += Drone_OnDataChange;
            this.Drone.OnDebugDataChange += Drone_OnDebugDataChange;

            this.FlightControl = flightControl;

            UpdateSettings(Drone.Settings);

            ratiosGraph.ValueMin = -1;
            ratiosGraph.ValueMax = 1;

            orientationGraphList.ValueMinimums = new double[] { -90, -90, 0 };
            orientationGraphList.ValueMaximums = new double[] { 90, 90, 360 };

            rotationGraphList.ValueMinimums = new double[] { -100, -100, -100 };
            rotationGraphList.ValueMaximums = new double[] { 100, 100, 100 };

            accelerationGraphList.ValueMinimums = new double[] { -5, -5, -5 };
            accelerationGraphList.ValueMaximums = new double[] { 5, 5, 5 };
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (this.Drone != null)
            {
                this.Drone.OnSettingsChange -= Drone_OnSettingsChange;
                this.Drone.OnDataChange -= Drone_OnDataChange;
                this.Drone.OnDebugDataChange -= Drone_OnDebugDataChange;
            }
            base.OnFormClosed(e);
        }

        private void Drone_OnSettingsChange(object sender, SettingsChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<SettingsChangedEventArgs>(Drone_OnSettingsChange), sender, e);
                return;
            }

            UpdateSettings(e.Settings);
        }

        private void UpdateSettings(DroneSettings settings)
        {
            servoGraph.BaseLine = settings.ServoHover;
            servoGraph.ValueMin = 0;
            servoGraph.ValueMax = settings.ServoMax;
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DataChangedEventArgs>(Drone_OnDataChange), sender, e);
                return;
            }

            UpdateGraph(servoGraph, e.Data.MotorValues);

            orientationGraphList.UpdateValue(e.Data.Gyro.Roll, e.Data.Gyro.Pitch, e.Data.Gyro.Yaw);
            rotationGraphList.UpdateValue(e.Data.Gyro.GyroX, e.Data.Gyro.GyroY, e.Data.Gyro.GyroZ);
            accelerationGraphList.UpdateValue(e.Data.Gyro.AccelerationX, e.Data.Gyro.AccelerationY, e.Data.Gyro.AccelerationZ);
        }

        private void Drone_OnDebugDataChange(object sender, DebugDataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DebugDataChangedEventArgs>(Drone_OnDebugDataChange), sender, e);
                return;
            }

            if (Drone.Data.State == DroneState.Armed || Drone.Data.State == DroneState.Flying)
            {
                UpdateGraph(ratiosGraph, e.DebugData.Real);
                UpdateGraph(correctionGraph, e.DebugData.Correction);
            }
        }

        private void FlightControl_OnRatioChanged(object sender, MotorRatios e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<MotorRatios>(FlightControl_OnRatioChanged), sender, e);
                return;
            }

            // nur Werte zeigen, wenn die Drohne keinen echten Werte berechnet und ausführt
            if (Drone.Data.State != DroneState.Armed && Drone.Data.State != DroneState.Flying)
            {
                UpdateGraph(ratiosGraph, e);
                UpdateGraph(correctionGraph, new MotorRatios());
            }
        }

        private void UpdateGraph(QuadGraphControl graph, MotorRatios values)
        {
            graph.UpdateValues(values.FrontLeft, values.FrontRight, values.BackLeft, values.BackRight);
        }

        private void UpdateGraph(QuadGraphControl graph, QuadMotorValues values)
        {
            graph.UpdateValues(values.FrontLeft, values.FrontRight, values.BackLeft, values.BackRight);
        }

        private void motorTabPage_Click(object sender, EventArgs e)
        {

        }
    }
}
