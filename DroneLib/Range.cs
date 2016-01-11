using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Range : Attribute
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public Range(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}
