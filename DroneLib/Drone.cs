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
                    OnPingChange?.Invoke(this, new PingChangedEventArgs(this));
                }
            }
        }

        public bool IsConnected
        {
            get { return Ping >= 0; }
        }

        /// <summary>
        /// Wird aufgerufen wenn die Drone verbunden ist.
        /// </summary>
        public event EventHandler OnConnected;

        /// <summary>
        /// Wird aufgerufen wenn sich der Ping-Wert ändert.
        /// </summary>
        public event EventHandler<PingChangedEventArgs> OnPingChange;

        /// <summary>
        /// Wird aufgerufen wenn die Drone eine Log Nachricht schickt.
        /// </summary>
        public event Action<String> OnLogMessage;

        /// <summary>
        /// Gibt die aktuelle Revision der Daten an die zu der Drone geschickt wurden.
        /// </summary>
        private int currentRevision = 1;

        /// <summary>
        /// Gibt die letzte Revision der Daten an die von der Drone geschickt wurden.
        /// </summary>
        private int lastDataDroneRevision = 0;

        /// <summary>
        /// Gibt die letzte Revision der Daten an die von der Drone mit Log Daten geschickt wurden.
        /// </summary>
        private int lastDataLogRevision = 0;

        /// <summary>
        /// Gibt die letzte Revision der Daten an die von der Drone mit Debug Daten geschickt wurden.
        /// </summary>
        private int lastDataDebugRevision = 0;

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

        /// <summary>
        /// Wrid aufgerufen, wenn sich die aktuellen Daten der Drone ändern.
        /// </summary>
        public event EventHandler<DataChangedEventArgs> OnDataChange;

        private int lastDataTime;

        private DroneData data;

        /// <summary>
        /// Gibt aktuelle Daten über das Verhalten der Drone zurück.
        /// </summary>
        public DroneData Data
        {
            get
            {
                lock (dataLock)
                {
                    return data;
                }
            }
            set
            {
                bool changed;
                lock (dataLock)
                {
                    changed = value != data;
                    if (changed)
                        data = value;
                }

                if (changed)
                    OnDataChange?.Invoke(this, new DataChangedEventArgs(this));
            }
        }

        /// <summary>
        /// Wird aufgerufen wenn sich der Info-Wert ändert.
        /// </summary>
        public event EventHandler<InfoChangedEventArgs> OnInfoChange;

        private DroneInfo info;

        /// <summary>
        /// Gibt Informationen über die Drone zurück. Null wenn noch keine Informationen empfangen wurden.
        /// </summary>
        public DroneInfo Info
        {
            get
            {
                lock (infoLock)
                {
                    return info;
                }
            }
            set
            {
                bool changed;
                lock (infoLock)
                {
                    changed = value != info;
                    if (changed)
                        info = value;
                }

                if (changed)
                    OnInfoChange?.Invoke(this, new InfoChangedEventArgs(this));
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn sich die Dronen Einstellungen ändern.
        /// </summary>
        public event EventHandler<SettingsChangedEventArgs> OnSettingsChange;

        private DroneSettings settings;

        /// <summary>
        /// Gibt Einstellungen der Drone zurück. Null wenn noch keine Informationen empfangen wurden.
        /// </summary>
        public DroneSettings Settings
        {
            get
            {
                lock (settingsLock)
                {
                    return settings;
                }
            }
            set
            {
                bool changed;
                lock (settingsLock)
                {
                    changed = !value.Equals(settings);
                    if (changed)
                        settings = value;
                }

                if (changed)
                    OnSettingsChange?.Invoke(this, new SettingsChangedEventArgs(this));
            }
        }

        public event EventHandler<DebugDataChangedEventArgs> OnDebugDataChange;

        private DebugData debugData;

        /// <summary>
        /// Gibt aktuelle Daten über das Verhalten der Drone zurück.
        /// </summary>
        public DebugData DebugData
        {
            get
            {
                lock (debugDataLock)
                {
                    return debugData;
                }
            }
            set
            {
                lock (dataLock)
                {
                    debugData = value;
                }

                OnDebugDataChange?.Invoke(this, new DebugDataChangedEventArgs(this));
            }
        }

        /// <summary>
        /// Gibt den Socket an mit dem die Drone mit der Hardware per UDP verbunden ist.
        /// </summary>
        private UdpClient controlSocket;

        /// <summary>
        /// Gibt den Socket an, mit dem die Drone die Daten empfängt.
        /// </summary>
        private UdpClient dataSocket;

        /// <summary>
        /// Gibt den Paket-Buffer an der benutzt wird um die Pakete zu generieren.
        /// </summary>
        private MemoryStream packetStream = new MemoryStream();

        /// <summary>
        /// BinaryWriter der für den Packet-Buffer zum Schreiben benutzt wird.
        /// </summary>
        private PacketBuffer packetBuffer;

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
        public bool AnyPacketsAcknowledgePending => packetsToAcknowledge.Count > 0;

        /// <summary>
        /// Gibt die Anzahl der Pakete zurück die noch vom Drone bestätigt werden müssen.
        /// </summary>
        public int PendingAcknowledgePacketsCount => packetsToAcknowledge.Count;

        private object infoLock = new object();
        private object dataLock = new object();
        private object settingsLock = new object();
        private object debugDataLock = new object();

        public Drone(IPAddress address, Config config)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            this.Config = config;
            this.Address = address;

            controlSocket = new UdpClient();
            controlSocket.Connect(address, Config.ProtocolControlPort);

            dataSocket = new UdpClient(Config.ProtocolDataPort);

            packetBuffer = new PacketBuffer(packetStream);

            controlSocket.BeginReceive(ReceivePacket, null);
            dataSocket.BeginReceive(ReceiveDataPacket, null);

            // Ping senden und ein ResetRevision Paket senden damit die Revision wieder zurück gesetzt wird
            SendPing();

            OnConnected += (sender, args) =>
            {
                Log.Info("Connected to {0}", Address);

                currentRevision = 1;
                lastDataDroneRevision = 0;
                lastDataLogRevision = 0;
                lastDataDebugRevision = 0;

                // alle Pending Packets leeren, damit die Drone nach Reconnect nicht überfordert wird
                packetsToAcknowledge.Clear();

                SendGetInfo();
                SendPacket(new PacketResetRevision(), true);
                SendPacket(new PacketCalibrateGyro(), true);
                SendPacket(new PacketSubscribeDataFeed(), true);
            };
        }

#region Dispose

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
                controlSocket?.Close();
                dataSocket?.Close();
                packetStream?.Dispose();
            }

            IsDisposed = true;
        }

        #endregion

        public void Disconnect()
        {
            SendDisarm();
            SendPacket(new PacketUnsubscribeDataFeed(), false);

            Ping = -1;
            Dispose();
        }

        /// <summary>
        /// Verschickt alle Pakete nochmal die noch vom Drone bestätigt werden.
        /// </summary>
        /// <returns>Gibt true zurück, wenn Pakete gesendet wurden.</returns>
        public bool ResendPendingPackets()
        {
            lock (packetsToAcknowledge)
            {
                bool anyDataSent = false;
                KeyValuePair<int, IPacket>[] packets = packetsToAcknowledge.ToArray();
                foreach (KeyValuePair<int, IPacket> packet in packets)
                    if (stopwatch.ElapsedMilliseconds - packetSendTime[packet.Key] > Math.Max(Ping, Config.AcknowlegdeTime)) // ist das Paket alt genug zum neusenden?
                        anyDataSent |= SendPacket(packet.Value, true, packet.Key);
                return anyDataSent;
            }
        }

        public bool CheckConnection()
        {
            if (!IsConnected)
                return false;

            if (Environment.TickCount - lastPing > 3000)
            {
                Log.Warning("Lost connection, no ping received");
                Ping = -1;
            }
            else if (Environment.TickCount - lastDataTime > 10000)
            {
                Log.Warning("Lost connection, no data receied");
                Ping = -1;
            }
            return IsConnected;
        }

#region SendShortcuts

        /// <summary>
        /// Schickt einen Ping-Befehl an das Drone. 
        /// </summary>
        public void SendPing()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            CheckConnection();

            if (!stopwatch.IsRunning)
                stopwatch.Start();

            SendPacket(new PacketPing(stopwatch.ElapsedMilliseconds), false);
        }

        /// <summary>
        /// Schickt der Drone den Befehl neuzustarten.
        /// </summary>
        public void SendReset() {
            if(IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            if (Data.State != DroneState.Reset && Data.State != DroneState.Stopped && Data.State != DroneState.Idle)
                throw new InvalidOperationException("Drone in invalid state: " + Data.State);

            SendPacket(new PacketReset(), false);
        }

        /// <summary>
        /// Fordert die Statusinformationen der Drone an.
        /// </summary>
        public void SendGetInfo() {
            if(IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketInfo(), false);
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

        public void SendMovementData(float pitch, float roll, float rotationalSpeed, float thrust, bool hover) {
            if(IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketSetMovement(pitch, roll, rotationalSpeed, thrust, hover), false);
        }

        public void SendBlink() {
            if(IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketBlink(), true);
        }
        
        /// <summary>
        /// Schickt einen Settings-Befehl an die Drohne.
        /// </summary>
        public void SendConfig(DroneSettings config)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketSetConfig(config), true);
            Settings = config;
        }

        /// <summary>
        /// Schickt einen Stop-Befehl an die Drohne.
        /// </summary>
        public void SendStop()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketStop(), true);
        }

        /// <summary>
        /// Schickt den ClearStatus-Befehl an die Drohne.
        /// </summary>
        public void SendClearStatus()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);

            SendPacket(new PacketClearStatus(), true);
        }

        /// <summary>
        /// Schickt ein Packet an die Drohne.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="guaranteed">Ob vom Drone eine Antwort gefordert wird.</param>
        public bool SendPacket(IPacket packet, bool guaranteed)
        {
            return SendPacket(packet, guaranteed, currentRevision++);
        }

        #endregion SendShortcuts

        private bool CheckRevision(int oldRev, int newRev)
        {
            if (newRev > oldRev)
                return true;
            if (newRev < 0 && oldRev >= 0) // Overflow
                return true;
            return false;
        }

#region ControlUdp

        private bool SendPacket(IPacket packet, bool guaranteed, int revision)
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));

            // wenn die Drohne nicht erreichbar ist
            if (!IsConnected)
            {
                if (Config.IgnoreGuaranteedWhenOffline)
                    guaranteed = false;

                // alle Pakete (außer Ping) ignorieren wenn die Drohne offline ist
                if (packet.Type != PacketType.Ping && Config.IgnorePacketsWhenOffline)
                    return false;
            }

            lock (controlSocket)
            {
                bool alreadySent;
                lock (packetsToAcknowledge)
                {
                    alreadySent = packetsToAcknowledge.ContainsKey(revision);
                    if (guaranteed)
                    {
                        if (!stopwatch.IsRunning)
                            stopwatch.Start();
                        packetsToAcknowledge[revision] = packet;
                        packetSendTime[revision] = stopwatch.ElapsedMilliseconds;
                    }
                }

                packetBuffer.ResetPosition();

                // Paket-Header schreiben
                packetBuffer.Write((byte)'F');
                packetBuffer.Write((byte)'L');
                packetBuffer.Write((byte)'Y');

                // Alle Daten werden nach dem Netzwerkstandard BIG-Endian übertragen
                packetBuffer.Write(revision);

                // wenn die Drohne eine Antwort schickt dann wird kein Ack-Paket angefordert, sonst kann es passieren, dass das Ack-Paket die eigentliche Antwort verdrängt
                packetBuffer.Write(guaranteed && !packet.Type.DoesClusterAnswer());
                packetBuffer.Write((byte)packet.Type);

                // Paket Inhalt schreiben
                packet.Write(packetBuffer);

                controlSocket.BeginSend(packetStream.GetBuffer(), (int)packetBuffer.Position, SendPacket, null);
                if (Config.VerbosePacketSending && (packet.Type != PacketType.Ping || Config.LogPingPacket))
                    Log.Verbose("[{0}] Packet:   [{1}] {2}, size: {3} bytes {4} {5}", Address.ToString(), revision, packet.Type, packetBuffer.Position, guaranteed ? "(guaranteed)" : "", alreadySent ? "(resend)" : "");
            }
            return true;
        }

        private void SendPacket(IAsyncResult result)
        {
            try
            {
                controlSocket.EndSend(result);
            }
            catch(SocketException)
            {
                // Drohne ist möglicherweiße nicht verfügbar
            }
        }

        private void ReceivePacket(IAsyncResult result)
        {
            if (IsDisposed)
                return;

            try
            {
                IPEndPoint endPoint = new IPEndPoint(Address, Config.ProtocolControlPort);
                byte[] packet = controlSocket.EndReceive(result, ref endPoint);

                // kein Packet empfangen
                if (packet == null || packet.Length == 0)
                {
                    controlSocket.BeginReceive(ReceivePacket, null);
                    return;
                }

                HandlePacket(packet);
            }
            catch (ObjectDisposedException e)
            {
                Log.Error(e);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            finally
            {
                controlSocket.BeginReceive(ReceivePacket, null);
            }
        }

        private const int HeaderSize = 9;

        private void HandlePacket(byte[] packet)
        {
            // jedes Drohnen Paket ist mindestens HeaderSize Bytes lang und fangen mit "FLY" an
            using (MemoryStream stream = new MemoryStream(packet))
            {
                PacketBuffer buffer = new PacketBuffer(stream);

                if (packet.Length < HeaderSize || buffer.ReadByte() != 'F' || buffer.ReadByte() != 'L' || buffer.ReadByte() != 'Y')
                    return;

                int revision = buffer.ReadInt();

                bool isGuaranteed = buffer.ReadByte() > 0;
                PacketType type = (PacketType)buffer.ReadByte();

                if (Config.VerbosePacketReceive
                    && type != PacketType.Ack
                    && (type != PacketType.Ping || Config.LogPingPacket))
                    Log.Verbose("[{0}] Received: [{1}] {2}, size: {3} bytes", Address.ToString(), revision, type, packet.Length);

                switch (type)
                {
                    case PacketType.Ping:
                        if (packet.Length < HeaderSize + sizeof(long))
                            throw new InvalidDataException("Packet is not long enough.");

                        bool wasNotConnected = !CheckConnection();

                        lastPing = Environment.TickCount;

                        long time = buffer.ReadLong(); // time ist der Wert von stopwatch zum Zeitpunkt des Absenden des Pakets
                        Ping = (int)(stopwatch.ElapsedMilliseconds - time);

                        if (wasNotConnected)
                            OnConnected?.Invoke(this, EventArgs.Empty);

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
                        Info = new DroneInfo(buffer);
                        Settings = DroneSettings.Read(buffer);

                        RemovePacketToAcknowlegde(revision);
                        break;
                    default:
                        throw new InvalidDataException("Invalid packet type to get sent by cluster.");
                }
            }
        }

        #endregion

        #region DataFeedReceive

        private void ReceiveDataPacket(IAsyncResult result)
        {
            if (IsDisposed)
                return;

            try
            {
                IPEndPoint endPoint = new IPEndPoint(Address, Config.ProtocolControlPort);
                byte[] packet = dataSocket.EndReceive(result, ref endPoint);

                // kein Packet empfangen
                if (packet == null || packet.Length == 0)
                {
                    dataSocket.BeginReceive(ReceivePacket, null);
                    return;
                }

                HandleDataPacket(packet);
            }
            catch(ObjectDisposedException e)
            {
                Log.Error(e);
            }
            catch (Exception e)
            {
                Log.Error(e);
                if (Debugger.IsAttached)
                    Debugger.Break();
            }
            finally
            {
                dataSocket.BeginReceive(ReceiveDataPacket, null);
            }
        }

        private const int DataDronePacketSize = 25;

        private void HandleDataPacket(byte[] packet) {
            using (MemoryStream stream = new MemoryStream(packet)) {
                PacketBuffer buffer = new PacketBuffer(stream);
                if (buffer.Size < 3 || buffer.ReadByte() != 'F' || buffer.ReadByte() != 'L' || buffer.ReadByte() != 'Y')
                    return;

                int revision = buffer.ReadInt();
                DataPacketType type = (DataPacketType)buffer.ReadByte();

                lastDataTime = Environment.TickCount;

                switch (type)
                {
                    case DataPacketType.Drone:
                        if (!CheckRevision(lastDataDroneRevision, revision))
                            return;

                        DroneState state = (DroneState)buffer.ReadByte();

                        QuadMotorValues motorValues = new QuadMotorValues(buffer.ReadUShort(), buffer.ReadUShort(),
                            buffer.ReadUShort(), buffer.ReadUShort());

                        GyroData gyro = new GyroData(
                            // Pitch, Roll, Yaw
                            buffer.ReadFloat(),
                            buffer.ReadFloat(),
                            buffer.ReadFloat(),

                            // Acceleration X, Y, Z
                            buffer.ReadFloat(),
                            buffer.ReadFloat(),
                            buffer.ReadFloat(),

                            // Temperature
                            buffer.ReadFloat());

                        float batteryVoltage = buffer.ReadFloat();

                        int wifiRssi = buffer.ReadInt();

                        Data = new DroneData(state, motorValues, gyro, batteryVoltage, wifiRssi);

                        lastDataDroneRevision = revision;
                        break;
                    case DataPacketType.Log:
                        if (!CheckRevision(lastDataLogRevision, revision))
                           return;

                        int lines = buffer.ReadInt();

                        for (int i = 0; i < lines; i++)
                        {
                            string msg = buffer.ReadString();

                            if (OnLogMessage == null)
                                Log.Info("[Drone] " + msg);
                            else
                                OnLogMessage(msg + Environment.NewLine);
                        }

                        lastDataLogRevision = revision;
                        break;
                    case DataPacketType.Debug:
                        if (!CheckRevision(lastDataDebugRevision, revision))
                            return;

                        DebugData = new DebugData()
                        {
                            Real = new MotorRatios(buffer),
                            Correction = new MotorRatios(buffer)
                        };

                        lastDataDebugRevision = revision;
                        break;
                }
            }
        }

#endregion

        /// <summary>
        /// Entfernt ein Paket von der Liste der noch zu bestätigen Pakete.
        /// </summary>
        /// <param name="packetID"></param>
        private void RemovePacketToAcknowlegde(int packetID)
        {
            lock(packetsToAcknowledge)
            {
                packetsToAcknowledge.Remove(packetID);
                packetSendTime.Remove(packetID);
            }
        }
    }
}
