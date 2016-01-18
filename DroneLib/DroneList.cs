using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

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

        public event EventHandler OnDroneFound;

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

            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write((byte)'F');
                writer.Write((byte)'L');
                writer.Write((byte)'Y');
                writer.Write((byte)1);

                IPAddress[] addresses = NetworkHelper.GetLocalBroadcastAddresses();
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

                    if (buffer.ReadByte() != 2)
                        return;

                    DroneEntry entry = new DroneEntry();
                    entry.Address = sender.Address;

                    entry.Name = buffer.ReadString();
                    entry.Model = buffer.ReadString();
                    entry.SerialCode = buffer.ReadString();
                    entry.FirmwareVersion = buffer.ReadByte();

                    AddDrone(entry);
                    foundDrones.Add(entry);
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

        private bool ContainsDrone(DroneEntry entry)
        {
            foreach (DroneEntry e in foundDrones)
                if (e.Equals(entry))
                    return true;
            return false;
        }

        private void AddDrone(DroneEntry entry)
        {
            if (ContainsDrone(entry))
                return;

            // alte Drone mit gleicher IP-Adresse entfernen
            RemoveDrone(entry.Address);

            foundDrones.Add(entry);
            if (OnDroneFound != null)
                OnDroneFound(this, EventArgs.Empty);
        }

        public DroneEntry[] GetDrones()
        {
            return foundDrones.ToArray();
        }
    }
}
