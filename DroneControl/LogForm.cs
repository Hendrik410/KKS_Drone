using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroneLibrary;

namespace DroneControl
{
    public partial class LogForm : Form
    {
        private Drone drone;

        public LogForm(Drone drone)
        {
            InitializeComponent();

            // Log Klasse vorbereiten 
            Log.OnFlushBuffer += Log_OnFlushBuffer;
            Log.FlushBuffer();
            Log.AutomaticFlush = true;

            this.drone = drone;

            drone.OnLogMessage += Drone_OnLogMessage;
        }

        private void Log_OnFlushBuffer(string obj)
        {
            if (logTextBox.InvokeRequired)
                logTextBox.BeginInvoke(new Action<string>(Log_OnFlushBuffer), obj);
            else
                logTextBox.AppendText(obj);
        }

        private void Drone_OnLogMessage(string msg)
        {
            if (droneLogTextBox.InvokeRequired)
                droneLogTextBox.Invoke(new Action<string>(Drone_OnLogMessage), msg);
            else
                droneLogTextBox.AppendText(msg);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Log.OnFlushBuffer -= Log_OnFlushBuffer;
            Log.AutomaticFlush = false;

            drone.OnLogMessage -= Drone_OnLogMessage;

            base.OnFormClosing(e);
        }

        private void flushTimer_Tick(object sender, EventArgs e)
        {
            Log.FlushBuffer();
        }

        private void logCleanButton_Click(object sender, EventArgs e)
        {
            logTextBox.Clear();
        }

        private void clearDroneButton_Click(object sender, EventArgs e)
        {
            droneLogTextBox.Clear();
        }
    }
}
