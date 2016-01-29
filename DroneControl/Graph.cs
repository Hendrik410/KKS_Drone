using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroneLibrary;

namespace DroneControl
{
    public partial class Graph : UserControl
    {
        public string Titel { get; set; }
        public DataHistory History { get; private set; }

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

        private int GetValue(int x)
        {
            int v = (Height - 1) - (int)(((History[x] - History.FullMin) / (History.FullMax - History.FullMin)) * Height);
            return Math.Min(Height, Math.Max(0, v));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if (!DesignMode && History != null)
            {
                Pen pen = new Pen(Color.Black);
                int lastY = 0;
                for (int i = 0; i < History.ValueCount; i++)
                {
                    int y = GetValue(i);
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
            History.UpdateValue(value);
            Refresh();
        }
    }
}
