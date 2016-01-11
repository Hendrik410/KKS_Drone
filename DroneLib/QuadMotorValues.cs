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
        public int FrontLeft {
            get;
            set;
        }

        /// <summary>
        /// Gibt den Wert des vorderen rechten Motors zurück.
        /// </summary>
        public int FrontRight {
            get;
            set;
        }

        /// <summary>
        /// Gibt den Wert des hinteren linken Motors zurück.
        /// </summary>
        public int BackLeft {
            get;
            set;
        }

        /// <summary>
        /// Gibt den Wert des hintern rechten Motors zurück.
        /// </summary>
        public int BackRight {
            get;
            set;
        }

        public QuadMotorValues(int fl, int fr, int bl, int br) : this() {
            this.FrontLeft = fl;
            this.FrontRight = fr;
            this.BackLeft = bl;
            this.BackRight = br;
        }


    }
}
