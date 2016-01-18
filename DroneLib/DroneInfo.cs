﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    /// <summary>
    /// Stellt verschiedene Informationen über das Drone bereit.
    /// </summary>
    public class DroneInfo : IEquatable<DroneInfo>
    {
        /// <summary>
        /// Gibt die Build-Version der Firmware des Clusters zurück.
        /// </summary>
        public byte BuildVersion { get; private set; }

        /// <summary>
        /// Gibt die größte Revision des Clusters zurück.
        /// </summary>
        public int HighestRevision { get; private set; }

        /// <summary>
        /// Gibt zurück ob die Motoren aktiviert sind.
        /// </summary>
        public bool IsArmed {
            get;
            private set;
        }
        
        /// <summary>
        /// Gibt die einzelnen Motorwerte zurück.
        /// </summary>
        public QuadMotorValues MotorValues {
            get;
            private set;
        }


        public DroneInfo(byte buildVersion, int highestRevision, bool isArmed, QuadMotorValues motorValues)
        {
            this.BuildVersion = buildVersion;
            this.HighestRevision = highestRevision;
            this.IsArmed = isArmed;
            this.MotorValues = motorValues;
        }

        public static bool operator ==(DroneInfo a, DroneInfo b)
        {
            if (object.ReferenceEquals(a, b))
                return true;
            if (object.ReferenceEquals(a, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(DroneInfo a, DroneInfo b)
        {
            return !(a == b);
        }


        public override bool Equals(object obj)
        {
            if (obj is DroneInfo)
                return Equals(obj as DroneInfo);
            return false;
        }

        public bool Equals(DroneInfo other)
        {
            if (other == null)
                return false;
            return BuildVersion == other.BuildVersion 
                && HighestRevision == other.HighestRevision
                && IsArmed == other.IsArmed
                && MotorValues == other.MotorValues;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = hash * 7 + BuildVersion.GetHashCode();
                hash = hash * 7 + HighestRevision.GetHashCode();
                return hash;
            }
        }
    }
}
