using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroneLibrary;
using DroneControl.Input;

namespace DroneControl
{
    public partial class RecordForm : Form
    {
        private Drone drone;
        private InputManager inputManager;

        private bool running;
        private string file;

        public RecordForm(Drone drone, InputManager inputManager)
        {
            this.drone = drone;
            this.inputManager = inputManager;

            InitializeComponent();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (running)
                Stop();
            base.OnFormClosed(e);
        }

        public void Start()
        {
            if (running)
                throw new InvalidOperationException("Already running");

            running = true;
            file = "recording_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".csv";

            progressBar.Style = ProgressBarStyle.Marquee;
            statusLabel.Text = "Running";
            fileLabel.Text = string.Format("Recording to \"{0}\"", file);

            startButton.Text = "Stop";

            System.IO.File.AppendAllText(file, string.Join(", ", columns) + Environment.NewLine);

            timer.Enabled = true;
        }

        public void Stop()
        {
            if (!running)
                throw new InvalidOperationException("Alread stopped");

            running = false;

            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = progressBar.Maximum;
            statusLabel.Text = "Stopped";

            startButton.Text = "Start";

            timer.Enabled = false;
        }

        private static string[] columns = new string[]
        {
            "State", "RSSI", "BatteryV",
            "Temp", "Pitch", "Roll", "Yaw",
            "GyroX", "GyroY", "GyroZ",
            "AccX", "AccY", "AccZ",
            "Target Pitch",
            "Target Roll",
            "Target Yaw",
            "Thrust",
            "PID Pitch",
            "PID Roll",
            "PID Yaw",
            "FL", "FR", "BL", "BR"
        };

        private StringBuilder line = new StringBuilder();

        private void EmitLine()
        {
            try
            {
                object[] values = new object[]
                {
                    drone.Data.State,
                    drone.Data.WifiRssi,
                    drone.Data.BatteryVoltage,

                    drone.Data.Gyro.Temperature,
                    drone.Data.Gyro.Pitch,
                    drone.Data.Gyro.Roll,
                    drone.Data.Gyro.Yaw,
                    drone.Data.Gyro.GyroX,
                    drone.Data.Gyro.GyroY,
                    drone.Data.Gyro.GyroZ,
                    drone.Data.Gyro.AccelerationX,
                    drone.Data.Gyro.AccelerationY,
                    drone.Data.Gyro.AccelerationZ,

                    inputManager.TargetData.Pitch,
                    inputManager.TargetData.Roll,
                    inputManager.TargetData.RotationalSpeed,
                    inputManager.TargetData.Thurst,

                    drone.DebugData.PitchOutput,
                    drone.DebugData.RollOutput,
                    drone.DebugData.YawOutput,

                    drone.Data.MotorValues.FrontLeft,
                    drone.Data.MotorValues.FrontRight,
                    drone.Data.MotorValues.BackLeft,
                    drone.Data.MotorValues.BackRight
                };

                line.Clear();
                for (int i = 0; i < values.Length; i++)
                {
                    if (i > 0)
                        line.Append(", ");

                    object value = values[i];
                    if (value is float)
                        line.Append(((float)value).ToString("F").Replace(',', '.'));
                    else
                        line.Append(values[i].ToString());
                }
                line.AppendLine();

                System.IO.File.AppendAllText(file, line.ToString());

                long fileSize = (new System.IO.FileInfo(file)).Length;
                if (fileSize < 1024 * 1024) 
                    statusLabel.Text = string.Format("Running ({0}kbyte)", fileSize / 1024);
                else
                    statusLabel.Text = string.Format("Running ({0}mkbyte)", fileSize / (1024 * 1024));
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            EmitLine();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (running)
                Stop();
            else
                Start();
        }
    }
}
