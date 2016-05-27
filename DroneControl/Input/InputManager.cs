using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DroneLibrary;

namespace DroneControl.Input
{
    public class InputManager : IDisposable
    {
        private Drone drone;
        private List<IInputDevice> devices = new List<IInputDevice>();

        private bool lastConnected;
        private BatteryInfo lastBattery;

        /// <summary>
        /// Gibt das aktuelle Eingabegerät zurück oder setzt dieses. 
        /// </summary>
        public IInputDevice CurrentDevice { get; set; }

        /// <summary>
        /// Gibt aktuelle Ziel Daten 
        /// </summary>
        public TargetData TargetData { get; set; }

        public float MaxPitch { get; set; } = 10;
        public float MaxRoll { get; set; } = 10;
        public float MaxRotationalSpeed { get; set; } = 30;

        public float PitchOffset { get; set; } = 0;
        public float RollOffset { get; set; } = 0;
        public float RotationalOffset { get; set; } = 0;

        public int MaxThrust { get; set; } = 500;

        public bool DeadZone { get; set; } = true;

        public event EventHandler OnDeviceInfoChanged;
        public event EventHandler OnTargetDataChanged;

        public InputManager(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));
            this.drone = drone;
        }

        public void Dispose()
        {
            foreach (IInputDevice device in devices)
                device.Dispose(); 
        }

        /// <summary>
        /// Sucht nach alle IInputDevices die angeschlossen sind oder einmal angeschlossen waren.
        /// </summary>
        /// <returns>Array mit allen IInputDevices die gefunden wurden.</returns>
        public IInputDevice[] FindDevices(out bool changed)
        {
            int lastDeviceCount = devices.Count;

            // alle IDeviceFinder Types in diesem Code suchen
            var finderTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IDeviceFinder)))
                .Where(t => t.IsClass);

            // IDeviceFinder Instanzen erzeugen
            var finders = finderTypes.Select(t => Activator.CreateInstance(t));

            // Geräte suchen
            foreach (IDeviceFinder finder in finders)
                foreach (IInputDevice device in finder.FindDevices())
                    if (!devices.Contains(device))
                        devices.Add(device);


            // hat sich verändert, wenn Geräte dazugekommen sind
            changed = devices.Count > lastDeviceCount;
            return devices.ToArray();
        }

        public void Update()
        {
            if (CurrentDevice != null)
            {
                CurrentDevice.Update(this);
                if (!CurrentDevice.IsConnected)
                    SendTargetData(new TargetData(0, 0, 0, 0));

                // schauen ob sich Informationen vom Gerät geändert haben
                bool dirty = CurrentDevice.IsConnected != lastConnected || !CurrentDevice.Battery.Equals(lastBattery);
                if (dirty)
                {
                    if (OnDeviceInfoChanged != null)
                        OnDeviceInfoChanged(this, EventArgs.Empty);

                    lastConnected = CurrentDevice.IsConnected;
                    lastBattery = CurrentDevice.Battery;
                }
            }
        }

        /// <summary>
        /// Sendet rohe IInputDevice Daten an die Drohne.
        /// </summary>
        /// <param name="data"></param>
        public void SendTargetData(TargetData data)
        {
            // Rohe Daten umwandeln in richtigen Interval
            data.Pitch *= MaxPitch;
            data.Roll *= MaxRoll;
            data.RotationalSpeed *= MaxRotationalSpeed;

            // Offset hinzufügen
            data.Pitch += PitchOffset;
            data.Roll += RollOffset;
            data.RotationalSpeed += RotationalOffset;

            data.Thrust *= MaxThrust;

            // Daten setzen und senden
            TargetData = data;
            if (OnTargetDataChanged != null)
                OnTargetDataChanged(this, EventArgs.Empty);

            if (drone.Data.State == DroneState.Armed || drone.Data.State == DroneState.Flying)
                drone.SendMovementData(data.Pitch, data.Roll, data.RotationalSpeed, (int)data.Thrust);
        }

        public void ToogleArmStatus()
        {
            if (drone.Data.State == DroneState.Idle)
                ArmDrone();
            else if (drone.Data.State == DroneState.Armed || drone.Data.State == DroneState.Flying)
                drone.SendDisarm();
        }

        public void ArmDrone()
        {
            if (drone.Data.State == DroneState.Idle)
                drone.SendArm();
        }

        public void DisarmDrone()
        {
            if (drone.Data.State == DroneState.Armed || drone.Data.State == DroneState.Flying)
                drone.SendDisarm();
        }

        public void StopDrone()
        {
            drone.SendStop();
        }

        public void SendClear()
        {
            drone.SendClearStatus();
        }
    }
}
