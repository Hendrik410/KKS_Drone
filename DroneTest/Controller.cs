using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DroneLibrary;

namespace TestStand {

    [Flags]
    enum MotorPosition {
        Position_Front = 1,
        Position_Back = 2,
        Position_Left = 4,
        Position_Right = 8
    };

    [Flags]
    enum MotorRotation {
        Clockwise = 1,
        Counterclockwise = 2
    };


    class Controller {
        SerialPort serialPort;

        private Timer timer;

        private float pG = 10f;
        private float iG = 0.01f;
        private float dG = 0.1f;

        public PID2 PidPitch {
            get;
            set;
        }
        public PID2 PidRoll {
            get;
            set;
        }
        public PID2 PidYaw {
            get;
            set;
        }

        public float outPitch;
        private float inPitch;

        public Controller() {
            timer = new Timer {
                Interval = 10,
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += TimerOnElapsed;

            PidPitch = new PID2(pG, iG, dG, 40, -40, 40, -40, () => inPitch, () => 0, v => { outPitch = (float)v; });
            //PidRoll = new PID2();
            //PidYaw = new PID2();

            serialPort = new SerialPort("COM7", 115200);

            serialPort.DataReceived += SerialPortOnDataReceived;
            serialPort.ErrorReceived += SerialPortOnErrorReceived;
            serialPort.Open();

            PidPitch.Enable();
            timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs) {
            float frontLeftRatio = mixMotor(outPitch, 0, 0, 0, MotorPosition.Position_Front | MotorPosition.Position_Left, MotorRotation.Counterclockwise);
            float frontRightRatio = mixMotor(outPitch, 0, 0, 0, MotorPosition.Position_Front | MotorPosition.Position_Right, MotorRotation.Clockwise);
            float backLeftRatio = mixMotor(outPitch, 0, 0, 0, MotorPosition.Position_Back | MotorPosition.Position_Left, MotorRotation.Clockwise);
            float backRightRatio = mixMotor(outPitch, 0, 0, 0, MotorPosition.Position_Back | MotorPosition.Position_Right, MotorRotation.Counterclockwise);
            ushort frontLeft = (ushort)mapRatio(frontLeftRatio, 1200, 1400, 1280);
            ushort frontRight = (ushort)mapRatio(frontRightRatio, 1200, 1400, 1280);
            ushort backLeft = (ushort)mapRatio(backLeftRatio, 1200, 1400, 1280);
            ushort backRight = (ushort)mapRatio(backRightRatio, 1200, 1400, 1280);

            SendMotorValues(frontLeft, frontRight, backLeft, backRight);
        }

        public void SendMotorValues(ushort fl, ushort fr, ushort bl, ushort br) {
            byte[] buffer = new byte[9];

            unchecked {
                buffer[0] = (byte)'S';
                buffer[1] = (byte)(fl >> 8);
                buffer[2] = (byte)(fl & 0xFF);
                buffer[3] = (byte)(fr >> 8);
                buffer[4] = (byte)(fr & 0xFF);
                buffer[5] = (byte)(bl >> 8);
                buffer[6] = (byte)(bl & 0xFF);
                buffer[7] = (byte)(br >> 8);
                buffer[8] = (byte)(br & 0xFF);
            }
            if(serialPort.IsOpen)
                serialPort.Write(buffer, 0, 9);
        }

        public void CalibrateGyro() {
            byte[] buffer = new byte[9];
            buffer[0] = (byte)'R';

            if(serialPort.IsOpen)
                serialPort.Write(buffer, 0, 9);
        }

        private void SerialPortOnErrorReceived(object sender, SerialErrorReceivedEventArgs serialErrorReceivedEventArgs) {
            Console.WriteLine("Error recieved");
        }

        public void Stop() {
            SendMotorValues(900, 900, 900, 900);
            PidPitch.Disable();
            serialPort.Close();
        }

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs) {

            string line = serialPort.ReadLine();

            if(!line.StartsWith("G"))
                return;

            string[] data = line.Substring(1).Split(';');
            if(data.Length != 3) {
                Console.WriteLine("Incompatible lentgh: {0}", data.Length);
                return;
            }

            int pitchRaw = int.Parse(data[0]);
            int rollRaw = int.Parse(data[1]);
            int yawRaw = int.Parse(data[2]);

            inPitch = pitchRaw / 1000f;
            //PidRoll.Input = rollRaw / 1000f;
            //PidYaw.Input = yawRaw / 1000f;
        }

        private const float Degree2Ratio = 0.001f;
        private const float RotaryDegree2Ratio = 0.001f;

        float mixMotor(float pitchDelta, float rollDelta, float yawDelta, float verticalRatio, MotorPosition position, MotorRotation rotation) {
            float targetMotorRatio = verticalRatio;

            if(Math.Abs(pitchDelta) >= 0.02) {
                if((position & MotorPosition.Position_Front) != (MotorPosition)0) {
                    targetMotorRatio += pitchDelta * Degree2Ratio;
                } else {
                    targetMotorRatio -= pitchDelta * Degree2Ratio;
                }
            }

            if(Math.Abs(rollDelta) >= 0.02) {
                if((position & MotorPosition.Position_Left) != (MotorPosition)0) {
                    targetMotorRatio -= rollDelta * Degree2Ratio;
                } else {
                    targetMotorRatio += rollDelta * Degree2Ratio;
                }
            }

            if(Math.Abs(yawDelta) >= 0.02) {
                if(rotation == MotorRotation.Clockwise) {
                    targetMotorRatio -= yawDelta * RotaryDegree2Ratio;
                } else {
                    targetMotorRatio += yawDelta * RotaryDegree2Ratio;
                }
            }

            return targetMotorRatio;
        }

        float mapRatio(float ratio, float min, float max, float center) {
            ratio = Math.Min(Math.Max(ratio, -1), 1);

            if(ratio == 0)
                return center;

            if(ratio > 0) {
                return (ratio) * (max - center) + center;
            } else {
                return center + (center - min) * ratio;
            }
        }
    }
}
