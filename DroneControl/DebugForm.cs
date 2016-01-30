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
using DroneLibrary.Protocol;

namespace DroneControl
{
    public partial class DebugForm : Form
    {
        public Drone Drone { get; private set; }

        public DebugForm(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            InitializeComponent();

            this.Drone = drone;
            this.Drone.OnDebugDataChange += Drone_OnDebugDataChange;

            UpdateDebugData(drone.DebugData);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Drone.OnDebugDataChange -= Drone_OnDebugDataChange;
            base.OnFormClosing(e);
        }

        private void UpdateDebugData(DebugData data)
        {
            ratioDataLabel.Text = string.Format("FL: {0}\nFR: {1}\nBL: {2}\nBR: {3}",
                Formatting.FormatRatio(data.FrontLeftRatio),
                Formatting.FormatRatio(data.FrontRightRatio),
                Formatting.FormatRatio(data.BackLeftRatio),
                Formatting.FormatRatio(data.BackRightRatio));

            correctionDataLabel.Text = string.Format("FL: {0:0.00}\nFR: {1:0.00}\nBL: {2:0.00}\nBR: {3:0.00}",
                Formatting.FormatRatio(data.FrontLeftCorrection),
                Formatting.FormatRatio(data.FrontRightCorrection),
                Formatting.FormatRatio(data.BackLeftCorrection),
                Formatting.FormatRatio(data.BackRightCorrection));
        }

        private void Drone_OnDebugDataChange(object sender, DebugDataChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DebugDataChangedEventArgs>(Drone_OnDebugDataChange), sender, e);
                return;
            }

            UpdateDebugData(e.DebugData);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (Drone.Data.State != DroneState.Reset && Drone.Data.State != DroneState.Stopped && Drone.Data.State != DroneState.Idle)
                return;
            Drone.SendReset();
        }

        private void blinkButton_Click(object sender, EventArgs e)
        {
            Drone.SendBlink();
        }
    }
}
