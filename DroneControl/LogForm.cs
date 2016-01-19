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
        public LogForm()
        {
            InitializeComponent();

            // Log Klasse vorbereiten 
            Log.OnFlushBuffer += Log_OnFlushBuffer;
            Log.FlushBuffer();
            Log.AutomaticFlush = true;
        }

        private void Log_OnFlushBuffer(string obj)
        {
            if (logTextBox.InvokeRequired)
                logTextBox.BeginInvoke(new Action<string>(Log_OnFlushBuffer), obj);
            else
                logTextBox.AppendText(obj);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Log.OnFlushBuffer -= Log_OnFlushBuffer;
            Log.AutomaticFlush = false;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            logTextBox.Clear();
        }

        private void flushTimer_Tick(object sender, EventArgs e)
        {
            Log.FlushBuffer();
        }
    }
}
