using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DroneLibrary;

namespace DroneControl
{
    public partial class UpdateOTAForm : Form
    {
        private Drone drone;
        private DroneOTA ota;

        public UpdateOTAForm(Drone drone)
        {
            InitializeComponent();

            this.drone = drone;

            this.ota = new DroneOTA(drone);
            this.ota.OnProgress += (s, e) => UpdateProgress();

            drone.OnDataChange += Drone_OnDataChange;
            drone.OnConnected += Drone_OnConnected;
            drone.OnDisconnect += Drone_OnDisconnect;

            UpdateProgress();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (ota.IsRunning)
                ota.Abort();

            drone.OnConnected -= Drone_OnConnected;
            drone.OnDataChange -= Drone_OnDataChange;
            drone.OnDisconnect -= Drone_OnDisconnect;
            base.OnHandleDestroyed(e);
        }

        private void Drone_OnConnected(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs e)
        {
            UpdateProgress();
        }

        private void Drone_OnDisconnect(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void openFileDialogButton_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
                fileNameTextBox.Text = fileDialog.FileName;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (ota.IsRunning)
            {
                ota.Abort();
                return;
            }


            if (!File.Exists(fileNameTextBox.Text))
                return;

            if (!ota.CanStart)
                return;

            ota.Start(fileNameTextBox.Text);

            progressBar.Minimum = 0;
            progressBar.Maximum = ota.Size;
        }

        private void UpdateProgress()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateProgress));
                return;
            }

            startButton.Enabled = ota.CanStart;
            progressLabel.Visible = ota.IsRunning & ota.Started;

            startButton.Text = ota.IsRunning ? "Abort" : "Start";
            if (ota.IsRunning && !ota.Started)
            {
                stateLabel.Text = "Aborting...";
                stateLabel.ForeColor = Color.DarkRed;
            }
            else if (ota.IsRunning)
            {
                stateLabel.Text = "Updating...";
                stateLabel.ForeColor = Color.DarkOrange;

                progressLabel.Text = string.Format("{0}kbytes / {1}kbytes ({2}%)", ota.Position / 1024, ota.Size / 1024, (int)Math.Round(100 * (float)ota.Position / ota.Size));

                progressBar.Value = ota.Position;
                progressBar.Style = ProgressBarStyle.Continuous;   
            }
            else if (ota.Done)
            {
                stateLabel.Text = "Done";
                stateLabel.ForeColor = Color.DarkGreen;
            }
            else if (ota.CanStart)
            {
                stateLabel.Text = "Ready";
                stateLabel.ForeColor = Color.DarkGreen;
                progressBar.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                stateLabel.Text = "Not ready";
                stateLabel.ForeColor = Color.Red;
                progressBar.Style = ProgressBarStyle.Marquee;
            }
        }
    }
}
