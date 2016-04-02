using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DroneLibrary
{
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    [TypeConverter(typeof(DroneSettingsTypeConverter))]
    public unsafe struct DroneSettings 
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        [Category("Drone")]
        [Description("User-friendly name for the drone")]
        public string DroneName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        [Category("Network")]
        [Description("The SIID of the WiFi network")]
        public string NetworkSSID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        [Category("Network")]
        [Description("The password of the WiFi network")]
        public string NetworkPassword;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        [Category("Network")]
        [Description("The password of the WiFi access point")]
        public string AccessPointPassword;

        [Category("Network")]
        [Description("The UDP port for the hello packets")]
        public ushort NetworkHelloPort;

        [Category("Network")]
        [Description("The UDP port for the control packets")]
        public ushort NetworkControlPort;

        [Category("Network")]
        [Description("The UDP port for the data packets")]
        public ushort NetworkDataPort;
        
        [Category("Network")]
        [Description("The size of the buffer for incoming packets")]
        public ushort NetworkPacketBufferSize;

        
        [Category("Network")]
        [Description("Time before the drone stops without any action sent")]
        public uint MaximumNetworkTimeout;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Debug")]
        [Description("Enables or disables the output of log messages to the serial port")]
        public bool VerboseSerialLog;

        [Category("Drone")]
        [Description("The temperature, at which the drone starts to decent on turn off")]
        public float MaxTemperature;

        [Category("Motors")]
        [Description("Minimum value for the servo motors")]
        public ushort ServoMin;

        [Category("Motors")]
        [Description("Maximum value for the servo motors")]
        public ushort ServoMax;

        [Category("Motors")]
        [Description("Value when the servo motors begin to start")]
        public ushort ServoIdle;

        [Category("Motors")]
        [Description("Value when the drone begins to hover")]
        public ushort ServoHover;

        [Category("Pins")]
        [Description("The pin of the front-left motor")]
        public byte PinFrontLeft;

        [Category("Pins")]
        [Description("The pin of the front-right motor")]
        public byte PinFrontRight;

        [Category("Pins")]
        [Description("The pin of the back-left motor")]
        public byte PinBackLeft;

        [Category("Pins")]
        [Description("The pin of the back-right motor")]
        public byte PinBackRight;

        [Category("Pins")]
        [Description("The pin of the LED")]
        public byte PinLed;

        [Category("Flying")]
        [Description("Value for the conversion between degree and ratio")]
        public float Degree2Ratio;

        [Category("Flying")]
        [Description("Value for the conversion between rotational degree and ratio")]
        public float RotationalDegree2Ratio;

        [Category("Flying")]
        [Description("Interval between the physics calculations")]
        public ushort PhysicsCalculationInterval;

        [MarshalAs(UnmanagedType.U1)]
        [Category("Flying")]
        [Description("Type of the drone engine to use")]
        public EngineType EngineType;

        [Category("PID Flying")]
        public PidSettings PitchPidSettings;

        [Category("PID Flying")]
        public PidSettings RollPidSettings;

        [Category("PID Flying")]
        public PidSettings YawPidSettings;

        [Category("Linear Flying")]
        public float InterpolationFactor;

        [Category("Linear Flying")]
        public float CorrectionFactor;

        [Category("Flying")]
        public float SafePitch;

        [Category("Flying")]
        public float SafeRoll;

        [Category("Flying")]
        public int SafeServoValue;

        [Category("Linear Flying")]
        public float RotationalCorrectionMax;

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
