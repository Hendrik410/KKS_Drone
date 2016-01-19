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
using DroneLibrary.Protocol;

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
            drone.SendPacket(new PacketCalibrateGyro(), true);
            drone.SendPacket(new PacketSubscribeDataFeed(), true);

            timer.Interval = 2000;
            timer.Tick += Timer_Tick;
            timer.Start();

            drone.OnPingChange += Drone_OnPingChange;
            drone.OnDataChange += Drone_OnDataChange;
            motorControl1.UpdateDrone(drone);

            ipInfoLabel.Text = string.Format(ipInfoLabel.Text, drone.Address);
            statusArmedLabel.Text = string.Format(statusArmedLabel.Text, "diarmed");
        }

        protected override void OnClosed(EventArgs e)
        {
            timer.Stop();

            lock(locker) {
                drone.SendPacket(new PacketUnsubscribeDataFeed(), true);
            }

            base.OnClosed(e);
        }

        private void Drone_OnDataChange(object sender, EventArgs eventArgs) {

            if(InvokeRequired) {
                Invoke(new EventHandler(Drone_OnDataChange), this, eventArgs);
                return;
            }

            statusArmedLabel.Text = $"Status: {(drone.Data.IsArmed ? "armed" : "disarmed")}";
            armToogleButton.Text = drone.Data.IsArmed ? "Disarm" : "Arm";

            artificialHorizon.SetAttitudeIndicatorParameters(drone.Data.Gyro.Pitch, drone.Data.Gyro.Roll);
            headingIndicator.SetHeadingIndicatorParameters((int)drone.Data.Gyro.Yaw);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lock(locker) {
                drone.SendPing();

                drone.ResendPendingPackets();
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

        private void calibrateGyroButton_Click(object sender, EventArgs e) {
            lock(locker) {
                drone.SendPacket(new PacketCalibrateGyro(), true);
            }
        }
    }
}
