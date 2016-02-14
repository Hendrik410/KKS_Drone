using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary {

    /// <summary>
    /// Eine Struktur zum Speichern der Motorwerte aller vier Motoren.
    /// </summary>
    public struct QuadMotorValues : IEquatable<QuadMotorValues> {

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

        public QuadMotorValues(ushort all) {
            this.BackLeft = all;
            this.BackRight = all;
            this.FrontLeft = all;
            this.FrontRight = all;
        }

        public QuadMotorValues(PacketBuffer buffer)
        {
            this.FrontLeft = buffer.ReadUShort();
            this.FrontRight = buffer.ReadUShort();
            this.BackLeft = buffer.ReadUShort();
            this.BackRight = buffer.ReadUShort();
        }

        public QuadMotorValues(ushort fl, ushort fr, ushort bl, ushort br) : this() {
            this.FrontLeft = fl;
            this.FrontRight = fr;
            this.BackLeft = bl;
            this.BackRight = br;
        }


        public static bool operator ==(QuadMotorValues a, QuadMotorValues b) {
            return a.Equals(b);
        }

        public static bool operator !=(QuadMotorValues a, QuadMotorValues b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            if(obj is QuadMotorValues)
                return Equals((QuadMotorValues)obj);
            return false;
        }

        public bool Equals(QuadMotorValues other) {
            return FrontLeft == other.FrontLeft
                && FrontRight == other.FrontRight
                && BackLeft == other.BackLeft
                && BackRight == other.BackRight;
        }

        public override int GetHashCode() {
            unchecked {
                int hash = 13;
                hash = hash * 7 + FrontLeft.GetHashCode();
                hash = hash * 7 + FrontRight.GetHashCode();
                hash = hash * 7 + BackLeft.GetHashCode();
                hash = hash * 7 + BackRight.GetHashCode();
                return hash;
            }
        }
    }
}
