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
        private List<IInputDevice> lastDevices = new List<IInputDevice>();

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
        public float MaxThrustPositive { get; set; } = 0.5f;
        public float MaxThrustNegative { get; set; } = 1f;

        public float PitchOffset { get; set; } = 0;
        public float RollOffset { get; set; } = 0;
        public float RotationalOffset { get; set; } = 0;

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
            foreach (IInputDevice device in lastDevices)
                device.Dispose(); 
        }

        /// <summary>
        /// Sucht nach alle IInputDevices die angeschlossen sind oder einmal angeschlossen waren.
        /// </summary>
        /// <returns>Array mit allen IInputDevices die gefunden wurden.</returns>
        public IInputDevice[] FindDevices(out bool changed)
        {
            // alle IDeviceFinder Types in diesem Code suchen
            var finderTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IDeviceFinder)))
                .Where(t => t.IsClass);

            // IDeviceFinder Instanzen erzeugen
            var finders = finderTypes.Select(t => Activator.CreateInstance(t));

            // Geräte suchen
            foreach (IDeviceFinder finder in finders)
                lastDevices.AddRange(finder.FindDevices());

            // doppelte Geräte entfernen
            var newDevices = lastDevices.Distinct();

            // schauen ob neue Geräte hinzugefügt wurden
            changed = newDevices.Count() > lastDevices.Count;

            lastDevices = newDevices.ToList();
            return lastDevices.ToArray();
        }

        public void Update()
        {
            if (CurrentDevice != null)
            {
                CurrentDevice.Update(this);

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

            if (data.Thurst >= 0)
                data.Thurst *= MaxThrustPositive;
            else
                data.Thurst *= MaxThrustNegative;

            // Offset hinzufügen
            data.Pitch += PitchOffset;
            data.Roll += RollOffset;
            data.RotationalSpeed += RotationalOffset;

            // Daten setzen und senden
            TargetData = data;
            if (OnTargetDataChanged != null)
                OnTargetDataChanged(this, EventArgs.Empty);

            if (drone.Data.State == DroneState.Armed || drone.Data.State == DroneState.Flying)
                drone.SendMovementData(data.Pitch, data.Roll, data.RotationalSpeed, data.Thurst, false);
        }

        public void ToogleArmStatus()
        {
            if (drone.Data.State == DroneState.Idle)
                drone.SendArm();
            else if (drone.Data.State == DroneState.Armed || drone.Data.State == DroneState.Flying)
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
