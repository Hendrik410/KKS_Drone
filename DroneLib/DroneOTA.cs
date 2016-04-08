using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DroneLibrary.Protocol;

namespace DroneLibrary
{
    public class DroneOTA
    {
        public Drone Drone { get; private set; }

        public bool CanStart { get { return Drone.IsConnected && Drone.Data.State != DroneState.Flying; } }
        public bool IsRunning { get { return Drone.IsConnected && Drone.Data.State == DroneState.OTA; } }

        public bool Started { get; private set; } 
        public bool Done { get; private set; }

        public int Position { get; private set; }
        public int Size { get { return data == null ? 0 : data.Length; } }

        public string MD5 { get; private set; }

        public event EventHandler OnProgress;

        private byte[] data;

        public DroneOTA(Drone drone)
        {
            this.Drone = drone;

            if (IsRunning)
                Abort();
        }

        public void Start(string file)
        {
            if (!CanStart)
                throw new InvalidOperationException("Drone not ready for OTA");

            Started = true;
            Done = false;
            Position = 0;

            data = File.ReadAllBytes(file);
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                MD5 = BitConverter.ToString(md5.ComputeHash(data)).Replace("-", "").ToLower();
            }

            Drone.SendPacket(new PacketBeginOTA(MD5, Size), true, (s, p) =>
            {
                if (IsRunning)
                    SendData();
                else
                    Drone.OnDataChange += Drone_OnDataChange;
            });
        }

        private void Drone_OnDataChange(object sender, DataChangedEventArgs e)
        {
            if (e.Data.State == DroneState.OTA)
            {
                Drone.OnDataChange -= Drone_OnDataChange;
                SendData();
            }
        }

        private void SendData()
        {
            if (!IsRunning)
            {
                Started = false;
                Done = false;
                return;
            }
            OnProgress?.Invoke(this, EventArgs.Empty);

            int chunkSize = Math.Min(256, Size - Position);

            byte[] chunk = new byte[chunkSize];
            Array.Copy(data, Position, chunk, 0, chunkSize);

            byte hash = 0;
            foreach (byte b in chunk)
                hash ^= b;

            Drone.SendPacket(new PacketDataOTA(chunkSize, hash, chunk), true, (s, p) =>
            {
                Position += chunkSize;
                if (Position >= Size)
                {
                    Drone.SendPacket(new PacketEndOTA(false), true);
                    Done = true;
                    return;
                }
                SendData();
            });
        }

        public void Abort()
        {
            if (!IsRunning)
                throw new InvalidOperationException("OTA not running");

            Done = false;
            Started = false;
            Drone.SendPacket(new PacketEndOTA(true), false);
            Drone.OnDataChange -= Drone_OnDataChange;
        }
    }
}
