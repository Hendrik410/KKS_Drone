using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using DroneLibrary;

namespace DroneControl
{
    public partial class MainForm : Form
    {
        private Drone drone;

        private object locker = new object();

        public MainForm(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            InitializeComponent();

            this.drone = drone;

            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            timer.Start();

            drone.OnPingChange += Drone_OnPingChange;
            drone.OnInfoChange += Drone_OnInfoChange;
            motorControl1.UpdateDrone(drone);

            ipInfoLabel.Text = string.Format(ipInfoLabel.Text, drone.Address);
            UpdatePing();
            UpdateInfo();
        }

        protected override void OnClosed(EventArgs e)
        {
            timer.Stop();

            if (drone != null)
            {
                if (drone.Info != null && drone.Info.IsArmed)
                    drone.SendDisarm();

                drone.Dispose();
            }

            Application.Exit();
            base.OnClosed(e);
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            lock (locker)
            {
                drone.SendPing();
                drone.SendGetInfo();
            }
        }

        private void Drone_OnInfoChange(object sender, EventArgs eventArgs)
        {
            if (InvokeRequired)
                Invoke(new EventHandler(Drone_OnInfoChange), sender, eventArgs);
            else
                UpdateInfo();
        }

        private void UpdateInfo()
        {
            if (drone.Info == null)
            {
                statusArmedLabel.Text = "Status: unkown";
                armToogleButton.Text = "Arm";
                armToogleButton.Enabled = false;
            }
            else if (drone.Info.IsArmed)
            {
                statusArmedLabel.Text = "Status: armed";
                armToogleButton.Text = "Disarm";
                armToogleButton.Enabled = true;
            }
            else
            {
                statusArmedLabel.Text = "Status: disarmed";
                armToogleButton.Text = "Arm";
                armToogleButton.Enabled = true;
            }
        }



        private void Drone_OnPingChange(object sender, EventArgs e)
        {
            if (pingLabel.InvokeRequired)
                pingLabel.Invoke(new EventHandler(Drone_OnPingChange), sender, e);
            else
                UpdatePing();
        }

        private void UpdatePing()
        {
            if (drone.Ping < 0)
                pingLabel.Text = "Not connected";
            else
                pingLabel.Text = string.Format("Ping: {0}ms", drone.Ping);

            if (drone.Ping < 0 || drone.Ping > 50)
                pingLabel.ForeColor = Color.Red;
            else
                pingLabel.ForeColor = Color.Green;
        }

        private void armToogleButton_Click(object sender, EventArgs e)
        {
            lock (locker)
            {
                if (drone.Info == null)
                    return;

                if (drone.Info.IsArmed)
                    drone.SendDisarm();
                else
                    drone.SendArm();
            }
        }
    }
}