using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DroneLibrary
{
    [StructLayout(LayoutKind.Sequential, Size = 172, Pack = 0, CharSet = CharSet.Ansi)]
    [TypeConverter(typeof(DroneSettingsTypeConverter))]
    public unsafe struct DroneSettings 
    {
        //A user-friendly name for the drone
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        [Category("Drone")]
        public string DroneName;

        //The name of the WiFi network
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        [Category("Network")]
        public string NetworkSSID;

        //The password of the WiFi network
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        [Category("Network")]
        public string NetworkPassword;

        //The UDP-Port for the hello packets
        [Category("Network")]
        public ushort NetworkHelloPort;

        //The UDP-Port for the control packets
        [Category("Network")]
        public ushort NetworkControlPort;
        //The UDP-Port for the data packets

        [Category("Network")]
        public ushort NetworkDataPort;
        //The size of the buffer for incoming packets

        [Category("Network")]
        public ushort NetworkPacketBufferSize;

        //Toogles the debug output on Serial
        [MarshalAs(UnmanagedType.U1)]
        [Category("Debug")]
        public bool VerboseSerialLog;
        //The temperature, at which the drone starts to decent on turn off

        [Category("Drone")]
        public float MaxTemperature;

        //A offset value for the pitch
        [Category("Flying")]
        public ushort TrimPitch;

        //A offset value for the roll
        [Category("Flying")]
        public ushort TrimRoll;

        //A offset value for the yaw
        [Category("Flying")]
        public ushort TrimYaw;

        //A offset value for the throttle
        [Category("Flying")]
        public ushort TrimThrottle;

        //The minumum output value for the ESC's
        [Category("Motors")]
        public ushort ServoMin;

        //The maximum output value for the ESC's
        [Category("Motors")]
        public ushort ServoMax;

        //The output value for the ESC's, at which they start to turn
        [Category("Motors")]
        public ushort ServoIdle;

        //The output value for the ESC's, at which the drone hovers
        [Category("Motors")]
        public ushort ServoHover;

        //The X gyro offset value for the DMP
        [Category("Gyro")]
        public short DMPOffsetX;

        //The Y gyro offset value for the DMP
        [Category("Gyro")]
        public short DMPOffsetY;

        //The Z gyro offset value for the DMP
        [Category("Gyro")]
        public short DMPOffsetZ;

        //The acceleration offset value for the DMP
        [Category("Gyro")]
        public ushort DMPOffsetAccel;

        //The pin of the front-left motor
        [Category("Pins")]
        public byte PinFrontLeft;

        //The pin of the front-right motor
        [Category("Pins")]
        public byte PinFrontRight;

        //The pin of the back-left motor
        [Category("Pins")]
        public byte PinBackLeft;

        //The pin of the back-right motor
        [Category("Pins")]
        public byte PinBackRight;

        //The pin of the LED
        [Category("Pins")]
        public byte PinLed;

        [Category("Flying")]
        public float Degree2Ratio;

        [Category("Flying")]
        public float RotaryDegree2Ratio;

        [Category("Flying")]
        public ushort PhysicsCalcDelay;

        [Category("Flying")]
        [MarshalAs(UnmanagedType.U1)]
        public EngineType EngineType;

        public PidSettings PitchPidSettings;
        public PidSettings RollPidSettings;
        public PidSettings YawPidSettings;


        public static DroneSettings Read(PacketBuffer packetBuffer)
        {
            int size = Marshal.SizeOf(typeof(DroneSettings));

            byte[] buffer = new byte[size];
            packetBuffer.Read(buffer, 0, buffer.Length);

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            DroneSettings settings = (DroneSettings)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(DroneSettings));  
            handle.Free();

            return settings;
        }

        public void Write(PacketBuffer packetBuffer)
        {
            int size = Marshal.SizeOf(typeof(DroneSettings));

            byte[] buffer = new byte[size];

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(this, handle.AddrOfPinnedObject(), false);
            handle.Free();

            packetBuffer.Write(buffer, 0, buffer.Length);
        }
    }
}
