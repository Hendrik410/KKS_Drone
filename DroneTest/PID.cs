using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TestStand {
    class PID {

        public float Kp {
            get;
            set;
        } = 0.001f;

        public float Ki {
            get;
            set;
        } = 0.001f;

        public float Kd {
            get;
            set;
        } = 0.001f;

        public float Target {
            get;
            set;
        } = 0;

        public float Input {
            get;
            set;
        } = 0;

        public float Output {
            get;
            private set;
        } = 0;


        public float OutputMin {
            get;
            set;
        } = -40;

        public float OutputMax {
            get;
            set;
        } = 40;
        

        private Timer timer;
        private float lastError = 0;
        private float integralValue = 0;

        public PID() {
            timer = new Timer {
                Interval = 10,
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs) {
            Compute();
        }

        public void Compute() {

            float error = Target - Input;

            float p = Kp * error;

            float d = Kd * (lastError - error);
            lastError = error;

            integralValue += Ki * error;

            Output = Math.Max(Math.Min(p + integralValue + d, OutputMax), OutputMin);
            
        }
    }

}
