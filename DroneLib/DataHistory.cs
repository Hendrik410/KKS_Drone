using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class DataHistory
    {
        public int Count { get; private set; }
        private List<double> values;

        private bool dirty;
        private double min, max, average;

        public double Min
        {
            get
            {
                if (dirty)
                    Update();
                return min;
            }
        }

        public double Max
        {
            get
            {
                if (dirty)
                    Update();
                return max;
            }
        }

        public double Average
        {
            get
            {
                if (dirty)
                    Update();
                return average;
            }
        }

        public double Current
        {
            get { return values.Last(); }
        }

        public int ValueCount
        {
            get { return values.Count; }
        }

        public double FullMin { get; private set; } = double.MaxValue;
        public double FullMax { get; private set; } = double.MinValue;

        public double this[int value]
        {
            get
            {
                return values[Math.Min(value, values.Count)];
            }
        }

        public DataHistory(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.Count = count;
            this.values = new List<double>(count);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        private void Update()
        {
            min = values.Min();
            max = values.Max();
            average = values.Average();
            dirty = false;
        }

        public void UpdateValue(double value)
        {
            if (values.Count == Count)
                values.RemoveAt(0);
            values.Add(value);
            dirty = true;

            if (value < FullMin)
                FullMin = value;
            if (value > FullMax)
                FullMax = value;
        }
    }
}
