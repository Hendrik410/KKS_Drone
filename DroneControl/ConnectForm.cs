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

        public ConnectForm()
        {
            InitializeComponent();

            droneList = new DroneList(new Config());

            droneList.OnListChanged += DroneList_OnListChanged;

            searchTimer.Interval = 500; // Millisekunden
            searchTimer.Tick += (object sender, EventArgs args) =>
            {
                droneList.SendHello();
            };
            searchTimer.Start();

            droneList.TimeoutSeconds = 2; // Sekunden
            droneList.SendHello();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (droneList != null)
                droneList.OnListChanged -= DroneList_OnListChanged;

            searchTimer.Stop();
            base.OnFormClosing(e);
        }

        private void TryToConnect()
        {
            IPAddress address;
            if (!IPAddress.TryParse(ipAddressTextBox.Text, out address))
                return;

            Connect(address);
        }

        private void Connect(IPAddress address)
        {
            connectButton.Enabled = false;

            // wenn wir verbunden sind (result == OK), können wir das Fenster schließen
            ConnectingForm form = new ConnectingForm(address);
            if (form.ShowDialog() == DialogResult.OK)
            {
                searchTimer.Stop();

                new MainForm(form.Drone).Show();
                Hide();
            }
            form.Dispose();

            connectButton.Enabled = true;
        }

        private void DroneList_OnListChanged(object sender, DroneListChangedEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler<DroneListChangedEventArgs>(DroneList_OnListChanged), sender, e);
            else
            {
                if (e.Entries.Length == 0)
                    searchStatus.Text = "Search drones...";
                else if (e.Entries.Length == 1)
                    searchStatus.Text = string.Format("Found {0} drone...", e.Entries.Length);
                else
                    searchStatus.Text = string.Format("Found {0} drones...", e.Entries.Length);

                droneListBox.Items.Clear();
                foreach (DroneEntry entry in e.Entries)
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
            TryToConnect();
        }

        private void droneListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (droneListBox.SelectedItem == null)
                return;

            DroneEntry entry = (DroneEntry)droneListBox.SelectedItem;
            Connect(entry.Address);
        }

        private void ipAddressTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            TryToConnect();
        }
    }
}
