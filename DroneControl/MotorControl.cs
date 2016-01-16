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
    public partial class MotorControl : UserControl
    {
        private Drone drone;

        public MotorControl()
        {
            InitializeComponent();

            leftFrontTextBox.TextChanged += OnValueChanged;
            leftBackTextBox.TextChanged += OnValueChanged;
            rightFrontTextBox.TextChanged += OnValueChanged;
            rightBackTextBox.TextChanged += OnValueChanged;
        }

        public void UpdateDrone(Drone drone)
        {
            if (drone == null)
                throw new ArgumentNullException(nameof(drone));

            this.drone = drone;
        }

        private void OnValueChanged(object sender, EventArgs args)
        {
        }
    }
}
