using System.IO;
using System.Text;
using System;

namespace Net{
    
    public class ByteBuffer
    {
        MemoryStream stream = null;
        BinaryWriter writer = null;
        BinaryReader reader = null;

        public ByteBuffer()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public ByteBuffer(byte[] data)
        {
            if (data != null)
            {
                stream = new MemoryStream(data);
                reader = new BinaryReader(stream);
            }
            else
            {
                stream = new MemoryStream();
                writer = new BinaryWriter(stream);
            }
        }

        public void WriteByte(byte v)
        {
            writer.Write(v);
        }

        public void WriteInt(int v)
        {
            writer.Write((int)v);
        }

        public void WriteShort(ushort v)
        {
            writer.Write((ushort)v);
        }

        public void WriteLong(long v)
        {
            writer.Write((long)v);
        }

        public void WriteFloat(float v)
        {
            byte[] tmp = BitConverter.GetBytes(v);
            Array.Reverse(tmp);
            writer.Write(BitConverter.ToDouble(tmp, 0));
        }

        public void WriteString(string v)
        {
            byte[] tmp = Encoding.ASCII.GetBytes(v);
            //writer.Write((ushort)tmp.Length);
            writer.Write(tmp);
        }
         public void WriteBytes(byte[] v) {
            writer.Write((int)v.Length);
            writer.Write(v);
        }
 
        public byte ReadByte() 
        {
            return reader.ReadByte();
        }
 
        public int ReadInt() 
        {
            return (int)reader.ReadInt32();
        }
 
        public ushort ReadShort()
        {
            return (ushort)reader.ReadInt16();
        }
 
        public long ReadLong() 
        {
            return (long)reader.ReadInt64();
        }
 
        public float ReadFloat() 
        {
            byte[] temp = BitConverter.GetBytes(reader.ReadSingle());
            Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }
 
        public double ReadDouble()
        {
            byte[] temp = BitConverter.GetBytes(reader.ReadDouble());
            Array.Reverse(temp);
            return BitConverter.ToDouble(temp, 0);
        }
 
        public string ReadString(int len) 
        {
            byte[] buffer = new byte[len];
            buffer = reader.ReadBytes(len);
            return Encoding.ASCII.GetString(buffer);
        }
 
        public byte[] ReadBytes()
        {
            int len = ReadInt();
            return reader.ReadBytes(len);
        }
 
        public byte[] ToBytes() 
        {
            writer.Flush();
            return stream.ToArray();
        }
 
        public void Flush() 
        {
            writer.Flush();
        }

    }
}
