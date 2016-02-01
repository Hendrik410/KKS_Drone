﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary {
    public struct DroneData : IEquatable<DroneData> {

        public DroneState State {
            get;
        }

        public QuadMotorValues MotorValues {
            get;
        }

        public GyroData Gyro {
            get;
        }

        public float BatteryVoltage {
            get;
        }

        public int WifiRssi { get; }

        public DroneData(DroneState state, QuadMotorValues motorValues, GyroData gyro, float batteryVoltage, int wifiRssi) {
            this.State = state;
            this.MotorValues = motorValues;
            this.Gyro = gyro;
            this.BatteryVoltage = batteryVoltage;
            this.WifiRssi = wifiRssi;
        }

        public static bool operator ==(DroneData a, DroneData b) {
            return a.Equals(b);
        }

        public static bool operator !=(DroneData a, DroneData b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            if(obj is DroneData)
                return Equals((DroneData)obj);
            return false;
        }

        public bool Equals(DroneData other) {
            return State == other.State
                   && MotorValues.Equals(other.MotorValues)
                   && Gyro.Equals(other.Gyro)
                   && BatteryVoltage == other.BatteryVoltage;
        }

        public override int GetHashCode() {
            unchecked {
                int hash = 13;
                hash = hash * 7 + State.GetHashCode();
                hash = hash * 7 + MotorValues.GetHashCode();
                hash = hash * 7 + Gyro.GetHashCode();
                hash = hash * 7 + (int)BatteryVoltage;
                return hash;
            }
        }
    }
}
