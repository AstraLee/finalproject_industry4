using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sck.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1994));
            sck.Listen(0); // ready for connection

            Socket acc = sck.Accept();

            byte[] buffer = Encoding.Default.GetBytes("Hello world!");
            acc.Send(buffer, 0, buffer.Length, 0);

            buffer = new byte[255]; // store data in this buffer flexibly

            int rec = acc.Receive(buffer, 0, buffer.Length, 0); // store actual data

            Array.Resize(ref buffer, rec); //resize size

            Console.WriteLine("Received: {0}", Encoding.Default.GetString(buffer));

            Console.Read();
        }
    }
}
