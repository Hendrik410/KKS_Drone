using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DroneLibrary;
using DroneLibrary.Protocol;

namespace DroneTest {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("DroneTest");
            Config config = new Config {
                IgnorePacketsWhenOffline = false,
                VerbosePacketSending = true,
                VerbosePacketReceive = true
            };
            Drone drone = new Drone(IPAddress.Parse("192.168.4.1"), config);

            Console.WriteLine("Press Key to start motors");
            Console.ReadKey(false);
            drone.SendPacket(new PacketResetRevision(), false);
            drone.SendPacket(new PacketSetRawValues(1500, 1500, 1500, 1500, true), false);

            Console.WriteLine("Press Key to stop");
            Console.ReadKey(false);
            drone.SendStop();

            Console.ReadKey(false);
            drone.SendBlink();

            Console.ReadKey(false);
        }
    }
}
