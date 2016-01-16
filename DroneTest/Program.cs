using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
            Drone drone = new Drone(IPAddress.Parse("192.168.178.26"), config);
            while(drone.Ping == -1) {
                drone.SendPing();
                Thread.Sleep(16);
                Console.WriteLine("ping!");
            }

            Console.WriteLine("Ping: {0}ms", drone.Ping);
            Console.WriteLine("Press Key to start motors");
            Console.ReadKey(false);
            drone.SendPacket(new PacketResetRevision(), false);
            drone.SendPacket(new PacketSetRawValues(1200, 1200, 1200, 1200, true), false);

            Console.WriteLine("Press Key to stop");
            Console.ReadKey(false);
            drone.SendStop();
            

            Console.ReadKey(false);
        }
    }
}
