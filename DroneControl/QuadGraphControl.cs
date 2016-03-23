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
    public partial class QuadGraphControl : UserControl
    {
        public Graph LeftTop { get { return leftTop; } }
        public Graph RightTop { get { return rightTop; } }
        public Graph LeftBottom { get { return leftBottom; } }
        public Graph RightBottom { get { return rightBottom; } }

        public string LeftTopName
        {
            get { return leftTop.Titel; }
            set { leftTop.Titel = value; }
        }

        public string RightTopName
        {
            get { return rightTop.Titel; }
            set { rightTop.Titel = value; }
        }

        public string LeftBottomName
        {
            get { return leftBottom.Titel; }
            set { leftBottom.Titel = value; }
        }

        public string RightBottomName
        {
            get { return rightBottom.Titel; }
            set { rightBottom.Titel = value; }
        }

        private bool showBaseLine;

        public bool ShowBaseLine
        {
            get { return showBaseLine; }
            set
            {
                showBaseLine = value;

                leftTop.ShowBaseLine = showBaseLine;
                rightTop.ShowBaseLine = showBaseLine;
                leftBottom.ShowBaseLine = showBaseLine;
                rightBottom.ShowBaseLine = showBaseLine;
            }
        }

        private double baseLine;

        public double BaseLine
        {
            get { return baseLine; }
            set
            {
                baseLine = value;

                leftTop.BaseLine = baseLine;
                rightTop.BaseLine = baseLine;
                leftBottom.BaseLine = baseLine;
                rightBottom.BaseLine = baseLine;
            }
        }

        private double valueMin, valueMax;

        public double ValueMin
        {
            get { return valueMin; }
            set
            {
                valueMin = value;

                leftTop.ValueMin = value;
                leftBottom.ValueMin = value;
                rightTop.ValueMin = value;
                rightBottom.ValueMin = value;
            }
        }

        public double ValueMax
        {
            get { return valueMax; }
            set
            {
                valueMax = value;

                leftTop.ValueMax = value;
                leftBottom.ValueMax = value;
                rightTop.ValueMax = value;
                rightBottom.ValueMax = value;
            }
        }

        public QuadGraphControl()
        {
            InitializeComponent();
        }

        public void UpdateValues(double leftTop, double rightTop, double leftBottom, double rightBottom)
        {
            this.leftTop.UpdateValue(leftTop);
            this.rightTop.UpdateValue(rightTop);
            this.leftBottom.UpdateValue(leftBottom);
            this.rightBottom.UpdateValue(rightBottom);
        }
    }
}
