using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            ipInfoLabel.Text = string.Format(ipInfoLabel.Text, drone.Address);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            drone.SendPing();
        }

        private void Drone_OnPingChange(object sender, EventArgs e)
        {
            if (pingLabel.InvokeRequired)
                pingLabel.Invoke(new Action<object, EventArgs>(Drone_OnPingChange), sender, e);
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
    }
}
