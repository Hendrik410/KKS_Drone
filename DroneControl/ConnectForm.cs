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

namespace DroneControl
{
    public partial class ConnectForm : Form
    {
        public ConnectForm()
        {
            InitializeComponent();
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

            // wenn wir verbunden sind (result = OK), können wir Fenster schließen
            if (new ConnectingForm(address).ShowDialog() == DialogResult.OK)
                Close();
        }
    }
}
