using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class DroneSettings : IEquatable<DroneSettings>
    {
        
        public DroneSettings()
        {
            
        }

        public DroneSettings(Config config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            
        }


        public static bool operator ==(DroneSettings a, DroneSettings b)
        {
            if (object.ReferenceEquals(a, b))
                return true;
            if (object.ReferenceEquals(a, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(DroneSettings a, DroneSettings b)
        {
            return !(a == b);
        }


        public override bool Equals(object obj)
        {
            if (obj is DroneSettings)
                return Equals(obj as DroneSettings);
            return false;
        }

        public bool Equals(DroneSettings other)
        {
            if (other == null)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                return hash;
            }
        }
    }
}
