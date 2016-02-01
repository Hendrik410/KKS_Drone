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
            this.Drone.OnDebugDataChange += Drone_OnDebugDataChange;

            this.FlightControl = flightControl;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (this.Drone != null)
                this.Drone.OnDebugDataChange -= Drone_OnDebugDataChange;
            base.OnFormClosed(e);
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
                frontLeftRatio.UpdateValue(e.DebugData.Real.FrontLeft);
                frontRightRatio.UpdateValue(e.DebugData.Real.FrontRight);
                backLeftRatio.UpdateValue(e.DebugData.Real.BackLeft);
                backRightRatio.UpdateValue(e.DebugData.Real.BackRight);
            }
        }

        private void FlightControl_OnRatioChanged(object sender, float[] e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<float[]>(FlightControl_OnRatioChanged), sender, e);
                return;
            }

            if (Drone.Data.State != DroneState.Armed && Drone.Data.State != DroneState.Flying)
            {
                frontLeftRatio.UpdateValue(e[0]);
                frontRightRatio.UpdateValue(e[1]);
                backLeftRatio.UpdateValue(e[2]);
                backRightRatio.UpdateValue(e[3]);
            }
        }
    }
}
