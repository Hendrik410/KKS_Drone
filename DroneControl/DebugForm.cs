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
                Formatting.FormatRatio(data.Real.FrontLeft),
                Formatting.FormatRatio(data.Real.FrontRight),
                Formatting.FormatRatio(data.Real.BackLeft),
                Formatting.FormatRatio(data.Real.BackRight));

            correctionDataLabel.Text = string.Format("FL: {0:0.00}\nFR: {1:0.00}\nBL: {2:0.00}\nBR: {3:0.00}",
                Formatting.FormatRatio(data.Correction.FrontLeft),
                Formatting.FormatRatio(data.Correction.FrontRight),
                Formatting.FormatRatio(data.Correction.BackLeft),
                Formatting.FormatRatio(data.Correction.BackRight));

            StringBuilder profilerString = new StringBuilder();

            for (int i = 0; i < data.Profiler.Entries.Length; i++)
                profilerString.AppendFormat("{0} {1}ms{2}", 
                    data.Profiler.Entries[i].Name.PadLeft(25), 
                    Formatting.FormatDecimal(data.Profiler.Entries[i].Time.TotalMilliseconds, 1, 4),
                    Environment.NewLine);

            profilerData.Text = profilerString.ToString();
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
