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
        private Timer timer;
        private Drone drone;

        private object locker = new object();

        public MainForm(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            InitializeComponent();

            this.drone = drone;

            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            timer.Start();

            drone.OnPingChange += Drone_OnPingChange;
            drone.OnInfoChange += Drone_OnInfoChange;
            motorControl1.UpdateDrone(drone);

            ipInfoLabel.Text = string.Format(ipInfoLabel.Text, drone.Address);
            statusArmedLabel.Text = string.Format(statusArmedLabel.Text, "diarmed");
        }

        private void Drone_OnInfoChange(object sender, EventArgs eventArgs) {

            if(statusArmedLabel.InvokeRequired)
                statusArmedLabel.Invoke(new EventHandler(Drone_OnInfoChange), sender, eventArgs);
            else
                statusArmedLabel.Text = $"Status: {(drone.Info.IsArmed ? "armed" : "disarmed")}";
            
            if(armToogleButton.InvokeRequired)
                armToogleButton.Invoke(new EventHandler(Drone_OnInfoChange), sender, eventArgs);
            else
                armToogleButton.Text = drone.Info.IsArmed ? "Disarm" : "Arm";

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lock(locker) {
                drone.SendPing();

                drone.SendGetInfo();
            }
        }

        private void Drone_OnPingChange(object sender, EventArgs e)
        {
            if (pingLabel.InvokeRequired)
                pingLabel.Invoke(new EventHandler(Drone_OnPingChange), sender, e);
            else
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
        }

        private void armToogleButton_Click(object sender, EventArgs e) {
            lock(locker) {
                if(drone.Info.IsArmed)
                    drone.SendDisarm();
                else
                    drone.SendArm();
            }
        }
    }
}
