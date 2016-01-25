﻿using System;
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
    public partial class Graph : UserControl
    {
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
            return (int)(((History[x] - History.Min) / (History.Max - History.Min)) * Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if (DesignMode || History == null)
                return;

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

            base.OnPaint(e);
        }

        public void UpdateValue(double value)
        {
            History.UpdateValue(value);
            Refresh();
        }
    }
}
