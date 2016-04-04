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

        public double ValueMin
        {
            get { return History.FullMin; }
            set { History.FullMin = value; }
        }

        public double ValueMax
        {
            get { return History.FullMax; }
            set { History.FullMax = value; }
        }

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
            newHistory.FullMin = ValueMin;
            newHistory.FullMax = ValueMax;

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

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            Font valueFont = new Font(FontFamily.GenericMonospace, 8);
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

                DrawValue(e.Graphics, valueFont, BaseLine, true);
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

                DrawHistoryValue(e.Graphics, valueFont, 0);
                DrawHistoryValue(e.Graphics, valueFont, 0.25);
                DrawHistoryValue(e.Graphics, valueFont, 0.75);
                DrawHistoryValue(e.Graphics, valueFont, 1);
            }

            
            if (!string.IsNullOrWhiteSpace(Titel))
                e.Graphics.DrawString(Titel, font, new SolidBrush(Color.DarkGray), 8, 8);
            base.OnPaint(e);
        }

        private void DrawHistoryValue(Graphics graphics, Font font, double value, bool center = false)
        {
            double realValue = History.FullMin + (History.FullMax - History.FullMin) * value;
            DrawValue(graphics, font, realValue, center);
        }

        private void DrawValue(Graphics graphics, Font font, double value, bool center = false)
        {
            float offsetY = 0;
            if (center)
                offsetY = graphics.MeasureString(value.ToString(), font).Height / 2;
            graphics.DrawString(value.ToString(), font, new SolidBrush(Color.DarkGray), 4, ConvertValue(value) - offsetY);
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
