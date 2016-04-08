﻿namespace DroneLibrary.Protocol
{
    public enum PacketType : byte
    {
        Movement = 1,
        Stop = 2,
        Arm = 3,
        Blink = 4,
        SetRawValues = 5,

        Ack = 6,
        Ping = 7,
        ResetRevision = 8,

        Info = 9,
        SubscribeDataFeed = 10,
        UnsubscribeDataFeed = 11,

        CalibrateGyro = 12,
        Reset = 13,

        SetConfig = 14,
        ClearStatus = 15,

        BeginOTA = 16,
        DataOTA = 17,
        EndOTA = 18
    }
}
