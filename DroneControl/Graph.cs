using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using DroneLibrary;

namespace DroneControl
{
    public partial class Graph : UserControl
    {
        public string Titel { get; set; }
        public DataHistory History { get; private set; }

        public bool ShowBaseLine { get; set; } = false;
        public double BaseLine { get; set; } = 0;

        private const int gridSize = 30;
        private int offsetX = 15;
        private const int offsetY = 15;

        public Graph()
        {
            InitializeComponent();

            History = new DataHistory(Width);
        }

        protected override void OnResize(EventArgs e)
        {
            if (DesignMode || History == null)
                return;

            DataHistory newHistory = new DataHistory(Width);
            foreach (double value in History)
                newHistory.UpdateValue(value);

            History = newHistory;
            Refresh();
            base.OnResize(e);
        }

        private int ConvertValue(double historyValue)
        {
            double min = History.FullMin;
            double max = History.FullMax;

            if (min == max)
            {
                min--;
                max++;
            }

            double value = (historyValue - min) / (max - min);
            value = 1 - value;

            int y = (int)(value * (Height - 1));
            return Math.Min(Height, Math.Max(0, y));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            Color gridColor = Color.FromArgb(0x50B3B3B3);
            Pen gridPen = new Pen(gridColor);

            for (int x = 0; x < Width; x += gridSize)
                e.Graphics.DrawLine(gridPen, offsetX + x, 0, offsetX + x, Height);
            for (int y = 0; y < Height; y+= gridSize)
                e.Graphics.DrawLine(gridPen, 0, offsetY + y, Width, offsetY + y);

            if (ShowBaseLine)
            {
                Color baseLineColor = Color.FromArgb(0x79, 0x55, 0x48);
                Pen baseLinePen = new Pen(baseLineColor);
                int baseLineValue = ConvertValue(BaseLine);
                e.Graphics.DrawLine(baseLinePen, 0, baseLineValue, Width, baseLineValue);
            }

            if (!DesignMode && History != null)
            {
                Pen pen = new Pen(Color.Black);
                int lastY = 0;
                for (int i = 0; i < History.ValueCount; i++)
                {
                    int y = ConvertValue(History[i]);
                    if (i == 0)
                        e.Graphics.DrawLine(pen, 0, y, 0, y);
                    else
                        e.Graphics.DrawLine(pen, i - 1, lastY, i, y);

                    lastY = y;
                }
            }

            
            if (!string.IsNullOrWhiteSpace(Titel))
                e.Graphics.DrawString(Titel, new Font(FontFamily.GenericSansSerif, 12), new SolidBrush(Color.DarkGray), 8, 8);
            base.OnPaint(e);
        }

        public void UpdateValue(double value)
        {
            // wenn unsere History nicht mehr Daten fassen kann, dann Grid verschieben
            if (History.Count == History.ValueCount)
            {
                offsetX--;
                if (offsetX < 0)
                    offsetX = gridSize - 1;
            }

            History.UpdateValue(value);
            Refresh();
        }
    }
}
