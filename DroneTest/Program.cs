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
            while(!drone.IsConnected) {
                drone.SendPing();
                Thread.Sleep(16);
                Console.WriteLine("ping!");
            }

            Console.WriteLine("Ping: {0}ms", drone.Ping);
            drone.SendPacket(new PacketResetRevision(), false);

            Console.WriteLine("Press Key to arm");
            Console.ReadKey(true);
            drone.SendArm();

            Console.WriteLine("Press Key to start motors");
            Console.ReadKey(true);
            drone.SendPacket(new PacketSetRawValues(1900, 1900, 1900, 1900), false);

            Console.WriteLine("Press Key to stop");
            Console.ReadKey(true);
            drone.SendStop();
            

            Console.ReadKey(true);
        }
    }
}
