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
    public partial class ConnectingForm : Form
    {
        private IPAddress ipAddress;

        public Drone Drone { get; private set; }

        public ConnectingForm(IPAddress ipAddress)
        {
            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));

            InitializeComponent();

            this.ipAddress = ipAddress;

            connectStatus.Text = string.Format(connectStatus.Text, ipAddress);

            timeoutTimer.Interval = 10 * 1000; // Timeout von 10 Sekunden
            timeoutTimer.Tick += (object sender, EventArgs args) =>
            {
                StartTimers();

                if (MessageBox.Show("Error while connecting: timeout.", "Connection Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
                else
                    StopTimers();
            };

            pingTimer.Interval = 200;
            pingTimer.Tick += (object sender, EventArgs args) =>
            {
                Drone.SendPing();
            };

            Connect();
        }

        protected override void OnClosed(EventArgs e)
        {
            StopTimers();
            base.OnClosed(e);
        }

        private void StartTimers()
        {
            timeoutTimer.Start();
            pingTimer.Start();
        }

        private void StopTimers()
        {
            timeoutTimer.Stop();
            pingTimer.Stop();
        }

        /// <summary>
        /// Versucht eine Verbindung zur IP-Adresse aufzubauen.
        /// </summary>
        private void Connect()
        {
            Drone = new Drone(ipAddress, new Config());
            Drone.OnConnected += OnDroneConnected;

            // TODO: drone.Connect() einbauen, damit das Event schon gesetzt ist bevor wir verbinden
            if (Drone.IsConnected) // schauen ob wir schon verbunden wurden, als wir das Event gesetzt haben
                OnDroneConnected(this, EventArgs.Empty);

            StartTimers();
        }

        private void OnDroneConnected(object sender, EventArgs args)
        {
            if (InvokeRequired)
                Invoke(new EventHandler(OnDroneConnected), sender, args);
            else
            {
                DialogResult = DialogResult.OK;
                StopTimers();
                Close();
            }
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            if (Drone != null)
                Drone.Dispose();

            DialogResult = DialogResult.Abort;
            StopTimers();
            Close();
        }
    }
}
