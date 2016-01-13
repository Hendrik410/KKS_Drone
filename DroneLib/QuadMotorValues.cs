using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary {

    /// <summary>
    /// Eine Struktur zum Speichern der Motorwerte aller vier Motoren.
    /// </summary>
    public struct QuadMotorValues {

        /// <summary>
        /// Gibt den Wert des vorderen linken Motors zurück.
        /// </summary>
        public ushort FrontLeft {
            get;
            set;
        }

        /// <summary>
        /// Gibt den Wert des vorderen rechten Motors zurück.
        /// </summary>
        public ushort FrontRight {
            get;
            set;
        }

        /// <summary>
        /// Gibt den Wert des hinteren linken Motors zurück.
        /// </summary>
        public ushort BackLeft {
            get;
            set;
        }

        /// <summary>
        /// Gibt den Wert des hintern rechten Motors zurück.
        /// </summary>
        public ushort BackRight {
            get;
            set;
        }

        public QuadMotorValues(ushort fl, ushort fr, ushort bl, ushort br) : this() {
            this.FrontLeft = fl;
            this.FrontRight = fr;
            this.BackLeft = bl;
            this.BackRight = br;
        }


    }
}
