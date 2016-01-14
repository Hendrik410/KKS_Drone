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

        private Timer timeoutTimer;
        private Drone drone;

        public ConnectingForm(IPAddress ipAddress)
        {
            if (ipAddress == null)
                throw new ArgumentNullException(nameof(ipAddress));

            InitializeComponent();

            this.ipAddress = ipAddress;

            connectStatus.Text = string.Format(connectStatus.Text, ipAddress);


            timeoutTimer = new Timer();
            timeoutTimer.Interval = 10 * 1000; // Timeout von 10 Sekunden
            timeoutTimer.Tick += (object sender, EventArgs args) =>
            {
                timeoutTimer.Stop();

                if (MessageBox.Show("Error while connecting: timeout.", "Connection Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
                else
                    Connect();
            };

            Connect();
        }

        protected override void OnClosed(EventArgs e)
        {
            timeoutTimer.Stop();
            base.OnClosed(e);
        }

        /// <summary>
        /// Versucht eine Verbindung zur IP-Adresse aufzubauen.
        /// </summary>
        private void Connect()
        {
            drone = new Drone(ipAddress, new Config());
            drone.OnConnected += OnDroneConnected;

            // TODO: drone.Connect() einbauen, damit das Event schon gesetzt ist bevor wir verbinden
            if (drone.Ping >= 0) // schauen ob wir schon verbunden wurden, als wir das Event gesetzt haben
                OnDroneConnected(this, EventArgs.Empty);

            timeoutTimer.Start();
        }

        private void OnDroneConnected(object sender, EventArgs args)
        {
            new MainForm(drone).Show();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            if (drone != null)
                drone.Dispose();

            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
