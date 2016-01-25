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

        public GraphForm(Drone drone)
        {
            InitializeComponent();

            this.Drone = drone;
            this.Drone.OnDebugDataChange += Drone_OnDebugDataChange;
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

            frontLeftRatio.UpdateValue(e.DebugData.FrontLeftRatio);
            frontRightRatio.UpdateValue(e.DebugData.FrontRightRatio);
            backLeftRatio.UpdateValue(e.DebugData.BackLeftRatio);
            backRightRatio.UpdateValue(e.DebugData.BackRightRatio);
        }
    }
}
