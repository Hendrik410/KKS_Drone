using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using DroneLibrary.Protocol;

namespace DroneLibrary
{
    /// <summary>
    /// Stellt eine Drone dar.
    /// </summary>
    public class Drone : IDisposable
    {
        public bool IsDisposed { get; private set; }

        private int lastPing = Environment.TickCount;
        private int ping = -1;
        private Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// Gibt die letzte Paketumlaufzeit an die von der Drone erfasst zurück. 
        /// Der Wert ist -1 wenn noch kein Ping-Wert emfangen wurde.
        /// </summary>
        public int Ping
        { 
            get
            {
                return ping;
            }
            private set
            {
                if (ping != value)
                {
                    ping = value;
                    if (OnPingChange != null)
                        OnPingChange(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen wenn die Drone verbunden ist.
        /// </summary>
        public event EventHandler OnConnected;

        /// <summary>
        /// Wird aufgerufen wenn sich der Ping-Wert ändert.
        /// </summary>
        public event EventHandler OnPingChange;

        /// <summary>
        /// Gibt die aktuelle Revision der Daten an die zu der Drone geschickt wurden.
        /// </summary>
        private int currentRevision = 1;

        /// <summary>
        /// Gibt die IPAdress der Drone zurück.
        /// </summary>
        public IPAddress Address { get; private set; }

        /// <summary>
        /// Gibt die Einstellungen der Drohne zurück
        /// </summary>
        public Config Config {
            get;
            private set;
        }

        private DroneInfo info;

        /// <summary>
        /// Gibt Informationen über die Drone zurück. Null wenn noch keine Informationen empfangen wurden.
        /// </summary>
        public DroneInfo Info
        {
            get
            {
                lock(locker)
                {
                    return info;
                }
            }
            set
            {
                lock (locker)
                {
                    if (value != info)
                    {
                        info = value;
                        if (OnInfoChange != null)
                            OnInfoChange(this, EventArgs.Empty);
                    }
                }
            }
        }


        private DroneSettings settings;

        /// <summary>
        /// Gibt Einstellungen der Drone zurück. Null wenn noch keine Informationen empfangen wurden.
        /// </summary>
        public DroneSettings Settings {
            get {
                lock(locker) {
                    return settings;
                }
            }
            set {
                lock(locker) {
                    if(value != settings) {
                        settings = value;
                        if(OnInfoChange != null)
                            OnInfoChange(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Wird aufgerufen wenn sich der Info-Wert ändert.
        /// </summary>
        public event EventHandler OnInfoChange;

        /// <summary>
        /// Gibt den Socket an mit dem die Drone mit der Hardware per UDP verbunden ist.
        /// </summary>
        private UdpClient socket;

        /// <summary>
        /// Gibt den Paket-Buffer an der benutzt wird um die Pakete zu generieren.
        /// </summary>
        private MemoryStream packetBuffer = new MemoryStream();

        /// <summary>
        /// BinaryWriter der für den Packet-Buffer zum Schreiben benutzt wird.
        /// </summary>
        private BinaryWriter packetWriter;

        /// <summary>
        /// Gibt den Zeitpunkt an als das Paket abgeschickt wurde.
        /// </summary>
        private Dictionary<int, long> packetSendTime = new Dictionary<int, long>();

        /// <summary>
        /// Packete die noch vom Drone bestätigt werden müssen.
        /// </summary>
        private Dictionary<int, IPacket> packetsToAcknowledge = new Dictionary<int, IPacket>();

        /// <summary>
        /// Gibt zurück ob Pakete noch warten vom Drone bestätigt zu werden.
        /// </summary>
        public bool AnyPacketsAcknowledgePending
        {
            get { return packetsToAcknowledge.Count > 0; }
        }

        /// <summary>
        /// Gibt die Anzahl der Pakete zurück die noch vom Drone bestätigt werden müssen.
        /// </summary>
        public int PendingAcknowledgePacketsCount
        {
            get { return packetsToAcknowledge.Count; }
        }


        private object locker = new object();

        public Drone(IPAddress address, Config config)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            this.Config = config;
            this.Address = address;

            socket = new UdpClient();
            socket.Connect(address, Config.ProtocolControlPort);

            packetWriter = new BinaryWriter(packetBuffer);

            socket.BeginReceive(ReceivePacket, null);

            // Ping senden und ein ResetRevision Paket senden damit die Revision wieder zurück gesetzt wird
            SendPing();

            OnConnected += (sender, args) =>
            {
                SendPacket(new PacketResetRevision(), true);
                
            };
        }

        ~Drone()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
            {
                if (socket != null)
                    socket.Close();

                if (packetBuffer != null)
                    packetBuffer.Dispose();

                if (packetWriter != null)
                    packetWriter.Dispose();
            }

            IsDisposed = true;
        }


        /// <summary>
        /// Verschickt alle Pakete nochmal die noch vom Drone bestätigt werden.
        /// </summary>
        /// <returns>Gibt true zurück, wenn Pakete gesendet wurden.</returns>
        public bool ResendPendingPackets()
        {
            lock (locker)
            {
                bool anyDataSent = false;
                KeyValuePair<int, IPacket>[] packets = packetsToAcknowledge.ToArray();
                foreach (KeyValuePair<int, IPacket> packet in packets)
                    if (stopwatch.ElapsedMilliseconds - packetSendTime[packet.Key] > Math.Max(Ping, Config.AcknowlegdeTime)) // ist das Paket alt genug zum neusenden?
                        anyDataSent |= SendPacket(packet.Value, true, packet.Key);
                return anyDataSent;
            }
        }

        /// <summary>
        /// Schickt einen Ping-Befehl an das Drone. 
        /// </summary>
        public void SendPing()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (Environment.TickCount - lastPing > 5000)
                Ping = -1;

            if (!stopwatch.IsRunning)
                stopwatch.Start();

            SendPacket(new PacketPing(stopwatch.ElapsedMilliseconds), false);
        }

        /// <summary>
        /// Schickt einen Arm-Befehl an die Drohne
        /// </summary>
        public void SendArm() {
            if(IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketArm(true), true);
        }

        /// <summary>
        /// Schickt einen Disarm-Befehl an die Drohne
        /// </summary>
        public void SendDisarm() {
            if(IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketArm(false), true);
        }

        public void SendBlink() {
            if(IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketBlink(), true);
        }
        
        /// <summary>
        /// Schickt einen Settings-Befehl an die Drohne.
        /// </summary>
        /*public void SendConfig(DroneSettings config)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketConfig(config), true);
        }*/

        /// <summary>
        /// Schickt einen Stop-Befehl an das Drone.
        /// </summary>
        public void SendStop()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketStop(), true);
        }

        /// <summary>
        /// Schickt ein Packet an das Drone.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="guaranteed">Ob vom Drone eine Antwort gefordert wird.</param>
        public bool SendPacket(IPacket packet, bool guaranteed)
        {
            return SendPacket(packet, guaranteed, currentRevision++);
        }

        private bool SendPacket(IPacket packet, bool guaranteed, int revision)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
            if (packet == null)
                throw new ArgumentNullException("packet");

            // wenn das Drone nicht erreichbar ist
            if (Ping < 0)
            {
                if (Config.IgnoreGuaranteedWhenOffline)
                    guaranteed = false;

                // alle Pakete (außer Ping) ignorieren wenn das Drone offline ist
                if (packet.Type != PacketType.Ping && Config.IgnorePacketsWhenOffline)
                    return false;
            }

            lock (locker)
            {
                bool alreadySent = packetsToAcknowledge.ContainsKey(revision);
                if (guaranteed)
                {
                    if (!stopwatch.IsRunning)
                        stopwatch.Start();
                    packetsToAcknowledge[revision] = packet;
                    packetSendTime[revision] = stopwatch.ElapsedMilliseconds;
                }

                packetBuffer.Position = 0;

                // Paket-Header schreiben
                packetWriter.Write((byte)'F');
                packetWriter.Write((byte)'L');
                packetWriter.Write((byte)'Y');

                // Alle Daten werden nach dem Netzwerkstandard BIG-Endian übertragen!!
                packetWriter.Write(BitConverter.IsLittleEndian? (int)BinaryHelper.ReverseBytes((uint)revision) : revision);

                // wenn die Drone eine Antwort schickt dann wird kein Ack-Paket angefordert, sonst kann es passieren, dass das Ack-Paket die eigentliche Antwort verdrängt
                if (guaranteed && !packet.Type.DoesClusterAnswer())
                    packetWriter.Write((byte)1);
                else
                    packetWriter.Write((byte)0);

                packetWriter.Write((byte)packet.Type);

                // Paket Inhalt schreiben
                packet.Write(packetWriter);


                socket.BeginSend(packetBuffer.GetBuffer(), (int)packetBuffer.Position, SendPacket, null);
                if (Config.VerbosePacketSending && (packet.Type != PacketType.Ping ||Config.LogPingPacket))
                    Log.Verbose("[{0}] Packet:   [{1}] {2}, size: {3} bytes {4} {5}", Address.ToString(), revision, packet.Type, packetBuffer.Position, guaranteed ? "(guaranteed)" : "", alreadySent ? "(resend)" : "");
            }
            return true;
        }

        private void SendPacket(IAsyncResult result)
        {
            try
            {
                socket.EndSend(result);
            }
            catch(SocketException)
            {
                // Drone ist möglicherweiße nicht verfügbar
            }
        }

        private void ReceivePacket(IAsyncResult result)
        {
            if (IsDisposed)
                return;

            try
            {
                IPEndPoint endPoint = new IPEndPoint(Address, Config.ProtocolControlPort);
                byte[] packet = socket.EndReceive(result, ref endPoint);

                // kein Packet empfangen
                if (packet == null || packet.Length == 0)
                {
                    socket.BeginReceive(ReceivePacket, null);
                    return;
                }

                lock (locker)
                {
                    HandlePacket(packet);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            finally
            {
                socket.BeginReceive(ReceivePacket, null);
            }
        }

        private const int HeaderSize = 9;

        private void HandlePacket(byte[] packet)
        {
            // jedes Kugelmatik V3 Paket ist mindestens HeaderSize Bytes lang und fangen mit "KKS" an
            if (packet.Length < HeaderSize || packet[0] != 'F' || packet[1] != 'L' || packet[2] != 'Y')
                return;

            bool isGuaranteed = packet[7] > 0;
            PacketType type = (PacketType)packet[8];

            using (MemoryStream stream = new MemoryStream(packet))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                stream.Position = HeaderSize - 6; // 4 Bytes für die Revision, 2 Bytes für Typ und Ack

                int revision = reader.ReadInt32();

                reader.ReadBytes(2);

                if (Config.VerbosePacketReceive 
                    && type != PacketType.Ack 
                    && (type != PacketType.Ping || Config.LogPingPacket))
                    Log.Verbose("[{0}] Received: [{1}] {2}, size: {3} bytes", Address.ToString(), revision, type, packet.Length);

                switch (type)
                {
                    case PacketType.Ping:
                        if (packet.Length < HeaderSize + sizeof(long))
                            throw new InvalidDataException("Packet is not long enough.");

                        int timeSpan = Environment.TickCount - lastPing;
                        if (timeSpan > 1000 * 10)
                            if (OnConnected != null)
                                OnConnected(this, EventArgs.Empty);

                        lastPing = Environment.TickCount;

                        long time = reader.ReadInt64(); // time ist der Wert von stopwatch zum Zeitpunkt des Absenden des Pakets
                        Ping = (int)(stopwatch.ElapsedMilliseconds - time);

                        RemovePacketToAcknowlegde(revision);
                        break;
                    case PacketType.Ack:
                        IPacket acknowlegdedPacket;
                        if (!packetsToAcknowledge.TryGetValue(revision, out acknowlegdedPacket))
                        {
                            if (Config.VerbosePacketReceive)
                                Log.Verbose("[{0}] Unkown acknowlegde: [{1}]", Address.ToString(), revision);
                            break;
                        }

                        if (Config.VerbosePacketReceive)
                            Log.Verbose("[{0}] Acknowlegde: [{1}] {2}", Address.ToString(), revision, acknowlegdedPacket.Type);

                        RemovePacketToAcknowlegde(revision);
                        break;
                    
                    case PacketType.Info:
                        if (packet.Length < HeaderSize + 22)
                            throw new InvalidDataException("Packet is not long enough.");

                        byte buildVersion = reader.ReadByte();
                        int highestRevision = reader.ReadInt32();

                        bool isArmed = reader.ReadByte() > 0;
                        
                        QuadMotorValues motorValues = new QuadMotorValues(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());

                        Info = new DroneInfo(buildVersion, highestRevision, isArmed, motorValues);
                        RemovePacketToAcknowlegde(revision);
                        break;
                    default:
                        throw new InvalidDataException("Invalid packet type to get sent by cluster.");
                }
            }
        }

        /// <summary>
        /// Entfernt ein Paket von der Liste der noch zu bestätigen Pakete.
        /// </summary>
        /// <param name="packetID"></param>
        private void RemovePacketToAcknowlegde(int packetID)
        {
            packetsToAcknowledge.Remove(packetID);
            packetSendTime.Remove(packetID);
        }
    }
}
