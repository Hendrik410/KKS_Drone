using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using DroneLibrary.Protocol;

namespace DroneLibrary
{
    /// <summary>
    /// Bietet Funktionen um Dronen im Netzwerk zu suchen und in einer Liste zu sammeln.
    /// </summary>
    public class DroneList
    {
        private Config config;

        private UdpClient client;
        private List<DroneEntry> foundDrones = new List<DroneEntry>();

        public event EventHandler<DroneListChangedEventArgs> OnDroneFound;

        public DroneList(Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            this.config = config;

            client = new UdpClient(config.ProtocolHelloPort);
            client.EnableBroadcast = true;
            client.BeginReceive(ReceivePacket, null);
        }

        public void SendHello()
        {
            Log.Debug("Sending hello broadcast");

            IPAddress[] addresses = NetworkHelper.GetLocalBroadcastAddresses();
            if (addresses == null)
                return;


            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write((byte)'F');
                writer.Write((byte)'L');
                writer.Write((byte)'Y');
                writer.Write((byte)HelloPacketType.Question);

                byte[] packet = stream.GetBuffer();

                for (int i = 0; i < addresses.Length; i++)
                    client.Send(stream.GetBuffer(), (int)stream.Length, new IPEndPoint(addresses[i], config.ProtocolHelloPort)); 
            }
        }

        private void ReceivePacket(IAsyncResult result)
        {
            IPEndPoint sender = null;
            byte[] packet = client.EndReceive(result, ref sender);

            Log.Debug("Got hello answer from {0}", sender.Address);

            HandlePacket(packet, sender);

            client.BeginReceive(ReceivePacket, null);
        }

        private void HandlePacket(byte[] packet, IPEndPoint sender)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(packet))
                {
                    PacketBuffer buffer = new PacketBuffer(stream);

                    if (packet.Length < 3 || buffer.ReadByte() != 'F' || buffer.ReadByte() != 'L' || buffer.ReadByte() != 'Y')
                    {
                        Log.Debug("Hello: Invalid magic value!");
                        return;
                    }

                    if (buffer.ReadByte() != (byte)HelloPacketType.Answer)
                        return;

                    DroneEntry entry = new DroneEntry();
                    entry.Address = sender.Address;

                    entry.LastFound = DateTime.Now;
                    entry.Name = buffer.ReadString();
                    entry.Model = buffer.ReadString();
                    entry.SerialCode = buffer.ReadString();
                    entry.FirmwareVersion = buffer.ReadByte();

                    AddDrone(entry);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        private void RemoveDrone(IPAddress address)
        {
            for (int i = 0; i < foundDrones.Count; i++)
                if (foundDrones[i].Address.Equals(address))
                    foundDrones.RemoveAt(i--);
        }

        private bool UpdateDrone(DroneEntry entry)
        {
            for (int i = 0; i < foundDrones.Count; i++)
            {
                DroneEntry e = foundDrones[i];
                if (e.Address.Equals(entry.Address))
                {
                    foundDrones[i] = DroneEntry.UpdateEntry(entry);

                    if (!e.Equals(entry) && OnDroneFound != null)
                        OnDroneFound(this, new DroneListChangedEventArgs(GetDrones()));
                    return true;
                }
            }
            return false;
        }

        private void AddDrone(DroneEntry entry)
        {
            lock(foundDrones)
            {
                if (UpdateDrone(entry))
                    return;

                // alte Drone mit gleicher IP-Adresse entfernen
                RemoveDrone(entry.Address);

                foundDrones.Add(entry);
            }

            if (OnDroneFound != null)
                OnDroneFound(this, new DroneListChangedEventArgs(GetDrones()));
        }

        private bool CheckDroneTimeout()
        {
            return foundDrones.Where((e) => (DateTime.Now - e.LastFound).TotalSeconds > 10).Count() > 0;
        }

        public DroneEntry[] GetDrones()
        {
            lock(foundDrones)
            {
                return foundDrones.Where((e) => (DateTime.Now - e.LastFound).TotalSeconds < 10).ToArray();
            }
        }
    }
}
