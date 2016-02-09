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
