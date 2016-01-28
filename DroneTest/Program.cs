using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DroneLibrary;
using DroneLibrary.Protocol;
using TestStand;

namespace DroneTest {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Test Stand");
            Controller controller = new Controller();
            Console.WriteLine("Started");

            controller.SendMotorValues(1200, 1200, 1200, 1200);

            bool repeat = true;

            while(repeat) {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch(keyInfo.Key) {
                    case ConsoleKey.Q:
                        repeat = false;
                        break;

                    case ConsoleKey.R:
                        controller.CalibrateGyro();
                        break;

                    case ConsoleKey.V:
                        Console.WriteLine("PID Output: P: {0}, R: {1}, Y: {2}", controller.outPitch, 0, 0);
                        break;
                }
            }
            controller.Stop();
        }
    }
}
