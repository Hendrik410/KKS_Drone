using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class GraphListControl : UserControl
    {
        private List<Graph> graphs = new List<Graph>();

        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");

                if (count != value)
                {
                    count = value;
                    CreateGraphs();
                }
            }
        }

        private bool showBaseLine;

        public bool ShowBaseLine
        {
            get { return showBaseLine; }
            set
            {
                showBaseLine = value;

                foreach (Graph graph in graphs)
                    graph.ShowBaseLine = showBaseLine;
            }
        }

        private double baseLine;

        public double BaseLine
        {
            get { return baseLine; }
            set
            {
                baseLine = value;

                foreach (Graph graph in graphs)
                    graph.BaseLine = baseLine;
            }
        }

        public string[] Names
        {
            get { return graphs.Select(g => g.Titel).Reverse().ToArray(); }
            set
            {
                CheckArray(value);
                for (int i = 0; i < value.Length; i++)
                    GetGraph(i).Titel = value[i];
            }
        }

        public double[] ValueMinimums
        {
            get { return graphs.Select(g => g.ValueMin).Reverse().ToArray(); }
            set
            {
                CheckArray(value);
                for (int i = 0; i < value.Length; i++)
                    GetGraph(i).ValueMin = value[i];
            }
        }

        public double[] ValueMaximums
        {
            get { return graphs.Select(g => g.ValueMax).Reverse().ToArray(); }
            set
            {
                CheckArray(value);
                for (int i = 0; i < value.Length; i++)
                    GetGraph(i).ValueMax = value[i];
            }
        }

        public GraphListControl()
        {
            InitializeComponent();
        }

        private void RemoveGraph()
        {
            Graph g = graphs.Last();
            Controls.Remove(g);
            graphs.RemoveAt(graphs.Count - 1);
        }

        private void CheckArray<T>(T[] array)
        {
            if (Count == 0)
                Count = array.Length;

            if (array.Length != Count)
                throw new ArgumentException("Names length does not match Count", "value");
        }

        private void AddGraph()
        {
            Graph g = new Graph();

            g.Dock = DockStyle.Top;
            g.ShowBaseLine = ShowBaseLine;
            g.BaseLine = BaseLine;
            g.Height = 256;
            g.Margin = new Padding(16, 0, 0, 16);

            Controls.Add(g);
            graphs.Add(g);
        }

        private void CreateGraphs()
        {
            while (Count < graphs.Count)
                RemoveGraph();
            while (Count > graphs.Count)
                AddGraph();
        }

        private Graph GetGraph(int index)
        {
            return graphs[graphs.Count - index - 1];
        }

        public void UpdateValue(params double[] values)
        {
            if (values.Length != Count)
                throw new ArgumentException("Values length does not match Count", nameof(values));

            for (int i = 0; i < values.Length; i++)
                GetGraph(i).UpdateValue(values[i]);
        }

        public void UpdateValue(int index, double value)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            GetGraph(index).UpdateValue(value);
        }
    }
}
