using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneControl.Input
{
    public interface IInputDevice : IDisposable, IEquatable<IInputDevice>
    {
        /// <summary>
        /// Gibt zurück, ob das Eingabegerät noch verbunden ist.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gibt den Namen des Eingabegeräts zurück.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gibt die Informationen über die Batterie des Eingabegeräts zurück.
        /// </summary>
        BatteryInfo Battery { get; }

        void Calibrate();

        void Update(InputManager manager);
    }
}
