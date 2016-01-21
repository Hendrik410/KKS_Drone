using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace DroneLibrary
{
    public class PacketBuffer
    {
        private MemoryStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;

        private byte[] helperBuffer = new byte[sizeof(double)];
        
        public long Position
        {
            get { return stream.Position; }
        }

        public long Size
        {
            get { return stream.Length; }
        }

        public PacketBuffer(MemoryStream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            this.stream = stream;
            this.reader = new BinaryReader(stream);
            this.writer = new BinaryWriter(stream);
        }

        public void ResetPosition()
        {
            stream.Position = 0;
        }

        public void Seek(int offset)
        {
            stream.Seek(offset, SeekOrigin.Current);
        }

        public bool ReadBoolean()
        {
            return ReadByte() > 0;
        }

        public byte ReadByte()
        {
            return reader.ReadByte();
        }

        public short ReadShort()
        {
            unchecked
            {
                return IPAddress.NetworkToHostOrder(reader.ReadInt16());
            }
        }

        public int ReadInt()
        {
            unchecked
            {
                return IPAddress.NetworkToHostOrder(reader.ReadInt32());
            }
        }

        public long ReadLong()
        {
            unchecked
            {
                return IPAddress.NetworkToHostOrder(reader.ReadInt64());
            }
        }

        public ushort ReadUShort()
        {
            return BinaryHelper.ReverseBytes(reader.ReadUInt16());
        }

        public uint ReadUInt()
        {
            return BinaryHelper.ReverseBytes(reader.ReadUInt32());
        }

        public ulong ReadULong()
        {
            return BinaryHelper.ReverseBytes(reader.ReadUInt64());
        }

        public float ReadFloat()
        {
            return ReadInt() / 100000.0f;

            /*stream.Read(helperBuffer, 0, sizeof(float));
            return BitConverter.ToSingle(helperBuffer, 0);*/
        }

        public double ReadDouble()
        {
            return ReadInt() / 100000.0f;
            // FIXME
            //stream.Read(helperBuffer, 0, sizeof(double));
            // return BitConverter.ToSingle(helperBuffer, 0);
        }

        public string ReadString()
        {
            char[] str = new char[ReadUShort()];

            for (int i = 0; i < str.Length; i++)
                str[i] = (char)ReadByte();

            return new string(str);
        }

        public void Write(bool value)
        {
            if (value)
                Write((byte)1);
            else
                Write((byte)0);
        }

        public void Write(byte value)
        {
            writer.Write(value);
        }

        public void Write(short value)
        {
            unchecked
            {
                writer.Write(IPAddress.HostToNetworkOrder(value));
            }
        }

        public void Write(int value)
        {
            unchecked
            {
                writer.Write(IPAddress.HostToNetworkOrder(value));
            }
        }

        public void Write(long value)
        {
            unchecked
            {
                writer.Write(IPAddress.HostToNetworkOrder(value));
            }
        }

        public void Write(ushort value)
        {
            writer.Write(BinaryHelper.ReverseBytes(value));
        }

        public void Write(uint value)
        {
            writer.Write(BinaryHelper.ReverseBytes(value));
        }

        public void Write(ulong value)
        {
            writer.Write(BinaryHelper.ReverseBytes(value));
        }

        public void Write(float value)
        {
            Write((int)(value * 100000));
            // FIXME
            //stream.Write(BitConverter.GetBytes(value), 0, sizeof(float));
        }

        public void Write(double value)
        {
            Write((int)(value * 100000));
            // FIXME
            //stream.Write(BitConverter.GetBytes(value), 0, sizeof(double));
        }

        public void Write(string str)
        {
            Write((ushort)str.Length);

            for (int i = 0; i < str.Length; i++)
                Write((byte)str[i]);
        }
    }
}
