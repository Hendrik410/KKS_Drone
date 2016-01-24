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

        private LogForm logForm;
        private DebugForm debugForm;

        private long tickCount;

        public MainForm(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            InitializeComponent();

            this.drone = drone;

            drone.SendPacket(new PacketCalibrateGyro(), true);
            drone.SendPacket(new PacketSubscribeDataFeed(), true);

            timer.Interval = 250;
            timer.Tick += Timer_Tick;
            timer.Start();

            drone.OnInfoChange += Drone_OnInfoChange;
            drone.OnPingChange += Drone_OnPingChange;
            drone.OnDataChange += Drone_OnDataChange;
            drone.OnSettingsChange += Drone_OnSettingsChange;

            motorControl1.Init(drone);
            flightControl1.Init(drone);
            sensorControl1.Init(drone);

            ipInfoLabel.Text = string.Format(ipInfoLabel.Text, drone.Address);
            UpdatePing(drone.IsConnected, drone.Ping);
            UpdateInfo(drone.Info);
            UpdateData(drone.Data);
            UpdateSettings(drone.Settings);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (timer != null)
                timer.Stop();
            
            flightControl1.Close();

            if (drone != null)
            {
                drone.OnInfoChange -= Drone_OnInfoChange;
                drone.OnPingChange -= Drone_OnPingChange;
                drone.OnDataChange -= Drone_OnDataChange;
                drone.OnSettingsChange -= Drone_OnSettingsChange;

                drone.Disconnect();
            }

            base.OnFormClosed(e);
            Application.Exit();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            drone.SendPing();
            if (tickCount % 16 == 0)
                drone.SendGetInfo();

            tickCount++;
        }

        private Form ShowForm(Form form, Func<Form> onClosed)
        {
            if (onClosed == null)
                throw new ArgumentNullException(nameof(onClosed));

            if (form == null || form.IsDisposed)
                form = onClosed();

            if (!form.Visible)
                form.Show(this);
            form.BringToFront();
            return form;
        }

        private void UpdatePing(bool isConnected, int ping)
        {
            if (!isConnected)
                pingLabel.Text = "Not connected";
            else
                pingLabel.Text = string.Format("Ping: {0}ms", drone.Ping);

            if (!isConnected || ping > 50)
                pingLabel.ForeColor = Color.Red;
            else
                pingLabel.ForeColor = Color.Green;
        }

        private void UpdateInfo(DroneInfo info)
        {
            if (string.IsNullOrWhiteSpace(info.Name))
                Text = string.Format("DroneControl - {0}", drone.Address);
            else
                Text = string.Format("DroneControl - {0}", info.Name);

            droneInfoPropertyGrid.SelectedObject = info;
        }


        private void UpdateData(DroneData data)
        {
            if (!drone.IsConnected)
            {
                statusArmedLabel.Text = "Status: not connected";
                statusButton.Enabled = false;
            }
            else
            {
                statusButton.Enabled = true;

                switch(drone.Data.State)
                {
                    case DroneState.Unkown:
                        statusButton.Enabled = false;
                        statusButton.Text = "Unkown";
                        break;
                    case DroneState.Stopped:
                    case DroneState.Reset:
                        statusButton.Text = "Clear";
                        break;
                    case DroneState.Idle:
                        statusButton.Text = "Arm";
                        break;
                    case DroneState.Armed:
                    case DroneState.Flying:
                        statusButton.Text = "Disarm";
                        break;
                }

                statusArmedLabel.Text = $"Status: {data.State}";
            }
        }

        private void UpdateSettings(DroneSettings settings)
        {
            droneSettingsPropertyGrid.SelectedObject = settings;
        }

        private void Drone_OnInfoChange(object sender, InfoChangedEventArgs eventArgs)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<InfoChangedEventArgs>(Drone_OnInfoChange), this, eventArgs);
                return;
            }

            UpdateInfo(eventArgs.Info);
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs eventArgs)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<DataChangedEventArgs>(Drone_OnDataChange), this, eventArgs);
                return;
            }

            UpdateData(eventArgs.Data);
        }

        private void Drone_OnSettingsChange(object sender, SettingsChangedEventArgs eventArgs)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<SettingsChangedEventArgs>(Drone_OnSettingsChange), this, eventArgs);
                return;
            }

            UpdateSettings(eventArgs.Settings);
        }

        private void Drone_OnPingChange(object sender, PingChangedEventArgs e)
        {
            if (pingLabel.InvokeRequired)
                pingLabel.Invoke(new EventHandler<PingChangedEventArgs>(Drone_OnPingChange), sender, e);
            else
                UpdatePing(e.IsConnected, e.Ping);
        }


        private void statusToogleButton_Click(object sender, EventArgs e)
        {
            switch(drone.Data.State)
            {
                case DroneState.Reset:
                case DroneState.Stopped:
                    drone.SendClearStatus();
                    break;
                case DroneState.Idle:
                    drone.SendArm();
                    break;
                case DroneState.Armed:
                case DroneState.Flying:
                    drone.SendDisarm();
                    break;
            }

        }

        private void logButton_Click(object sender, EventArgs e)
        {
            ShowForm(logForm, () => (logForm = new LogForm(drone)));
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            drone.SendStop();
        }

        private void droneSettingsPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            drone.SendConfig((DroneSettings)droneSettingsPropertyGrid.SelectedObject);
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            ShowForm(debugForm, () => (debugForm = new DebugForm(drone)));
        }
    }
}
