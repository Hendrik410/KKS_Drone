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
    public partial class ConnectForm : Form
    {
        private DroneList droneList;
        private Timer searchTimer;

        public ConnectForm()
        {
            InitializeComponent();

            droneList = new DroneList(new Config());

            droneList.OnDroneFound += DroneList_OnDroneFound;

            searchTimer = new Timer();
            searchTimer.Interval = 2000; // 2 Sekunden
            searchTimer.Tick += (object sender, EventArgs args) =>
            {
                droneList.SendHello();
            };
            searchTimer.Start();

            droneList.SendHello();
        }

        protected override void OnClosed(EventArgs e)
        {
            searchTimer.Stop();
            base.OnClosed(e);
        }

        private void Connect(IPAddress address)
        {
            // wenn wir verbunden sind (result == OK), können wir das Fenster schließen
            ConnectingForm form = new ConnectingForm(address);
            if (form.ShowDialog() == DialogResult.OK)
            {
                new MainForm(form.Drone).Show();
                Hide();
            }
        }

        private void DroneList_OnDroneFound(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler(DroneList_OnDroneFound), sender, e);
            else
            {
                DroneEntry[] drones = droneList.GetDrones();

                if (drones.Length == 1)
                    searchStatus.Text = string.Format("Found {0} drone...", drones.Length);
                else
                    searchStatus.Text = string.Format("Found {0} drones...", drones.Length);

                droneListBox.Items.Clear();
                foreach (DroneEntry entry in drones)
                    droneListBox.Items.Add(entry);
            }
        }

        private void ipAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            IPAddress address;
            connectButton.Enabled = IPAddress.TryParse(ipAddressTextBox.Text, out address);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            IPAddress address;
            if (!IPAddress.TryParse(ipAddressTextBox.Text, out address))
                return;

            Connect(address);
        }

        private void droneListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (droneListBox.SelectedItem == null)
                return;

            DroneEntry entry = (DroneEntry)droneListBox.SelectedItem;
            Connect(entry.Address);
        }
    }
}
