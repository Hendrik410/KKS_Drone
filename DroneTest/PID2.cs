using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestStand {

    public delegate double GetDouble();
    public delegate void SetDouble(double value);

    class PID2 {
        #region Fields

        //Gains
        private double kp;
        private double ki;
        private double kd;

        //Running Values
        private DateTime lastUpdate;
        private double lastPV;
        private double errSum;

        //Reading/Writing Values
        private GetDouble readPV;
        private GetDouble readSP;
        private SetDouble writeOV;

        //Max/Min Calculation

        //Threading and Timing
        private double computeHz = 1.0f;
        private Thread runThread;

        #endregion

        #region Properties

        public double PGain
        {
            get { return kp; }
            set { kp = value; }
        }

        public double IGain
        {
            get { return ki; }
            set { ki = value; }
        }

        public double DGain
        {
            get { return kd; }
            set { kd = value; }
        }

        public double PVMin {
            get;
            set;
        }

        public double PVMax {
            get;
            set;
        }

        public double OutMin {
            get;
            set;
        }

        public double OutMax {
            get;
            set;
        }

        public bool PIDOK => runThread != null;

        #endregion

        #region Construction / Deconstruction

        public PID2(double pG, double iG, double dG,
            double pMax, double pMin, double oMax, double oMin,
            GetDouble pvFunc, GetDouble spFunc, SetDouble outFunc) {
            kp = pG;
            ki = iG;
            kd = dG;
            PVMax = pMax;
            PVMin = pMin;
            OutMax = oMax;
            OutMin = oMin;
            readPV = pvFunc;
            readSP = spFunc;
            writeOV = outFunc;
        }

        ~PID2() {
            Disable();
            readPV = null;
            readSP = null;
            writeOV = null;
        }

        #endregion

        #region Public Methods

        public void Enable() {
            if(runThread != null)
                return;

            Reset();

            runThread = new Thread(new ThreadStart(Run));
            runThread.IsBackground = true;
            runThread.Name = "PID Processor";
            runThread.Start();
        }

        public void Disable() {
            if(runThread == null)
                return;

            runThread.Abort();
            runThread = null;
        }

        public void Reset() {
            errSum = 0.0f;
            lastUpdate = DateTime.Now;
        }

        #endregion

        #region Private Methods

        private double ScaleValue(double value, double valuemin,
                double valuemax, double scalemin, double scalemax) {
            double vPerc = (value - valuemin) / (valuemax - valuemin);
            double bigSpan = vPerc * (scalemax - scalemin);

            double retVal = scalemin + bigSpan;

            return retVal;
        }

        private double Clamp(double value, double min, double max) {
            if(value > max)
                return max;
            if(value < min)
                return min;
            return value;
        }

        private void Compute() {
            if(readPV == null || readSP == null || writeOV == null)
                return;

            double pv = readPV();
            double sp = readSP();

            //We need to scale the pv to +/- 100%, but first clamp it
            pv = Clamp(pv, PVMin, PVMax);
            pv = ScaleValue(pv, PVMin, PVMax, -1.0f, 1.0f);

            //We also need to scale the setpoint
            sp = Clamp(sp, PVMin, PVMax);
            sp = ScaleValue(sp, PVMin, PVMax, -1.0f, 1.0f);

            //Now the error is in percent...
            double err = sp - pv;

            double pTerm = err * kp;
            double iTerm = 0.0f;
            double dTerm = 0.0f;

            double partialSum = 0.0f;
            DateTime nowTime = DateTime.Now;

            if(lastUpdate != null) {
                double dT = (nowTime - lastUpdate).TotalSeconds;

                //Compute the integral if we have to...
                if(pv >= PVMin && pv <= PVMax) {
                    partialSum = errSum + dT * err;
                    iTerm = ki * partialSum;
                }

                if(dT != 0.0f)
                    dTerm = kd * (pv - lastPV) / dT;
            }

            lastUpdate = nowTime;
            errSum = partialSum;
            lastPV = pv;

            //Now we have to scale the output value to match the requested scale
            double outReal = pTerm + iTerm + dTerm;

            outReal = Clamp(outReal, -1.0f, 1.0f);
            outReal = ScaleValue(outReal, -1.0f, 1.0f, OutMin, OutMax);

            //Write it out to the world
            writeOV(outReal);
        }

        #endregion

        #region Threading

        private void Run() {
            while(true) {
                try {
                    int sleepTime = (int)(1000 / computeHz);
                    Thread.Sleep(sleepTime);
                    Compute();
                } catch(Exception e) {

                }
            }
        }

        #endregion
    }
}
