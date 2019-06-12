using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;


namespace Net
{
    public class SocketClient
    {
        private static byte[] result = new byte[1024];
        private static Socket clientSocket;

        public bool IsConnected = false;

        public SocketClient()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        
        ///<summary>
        /// connect to Server from specified IP and Port
        /// </summary>
        /// <param name="port"</param>
        /// <param name="ip"</param>


        public void ConnectServer(string ip, int port)
        {
            IPAddress mIp = IPAddress.Parse(ip);
            IPEndPoint ip_end_point = new IPEndPoint(mIp, port);

            try
            {
                clientSocket.Connect(ip_end_point);
                IsConnected = true;
                Debug.WriteLine("Connection successful");
            }
            catch
            {
                IsConnected = false;
                Debug.WriteLine("Connection failed");
                return;
            }

            //int receiveLength = clientSocket.Receive(result);
            //ByteBuffer buffer = new ByteBuffer(result);
            //int len = buffer.ReadShort();
            //string data = buffer.ReadString();
            //Debug.WriteLine("Server returned data: " + data);


        }

        /// <summary>
        /// Send data to Server
        /// </summary>
        /// 

        public void Close()
        {
            clientSocket.Close();
            Console.WriteLine("Socket client closed");
           
        }
        public void ReceiveBytes(byte[] dataframe)
        {
            try
            {
                int receiveLength = clientSocket.Receive(result);
                ByteBuffer buff = new ByteBuffer(result);
                dataframe = buff.ReadBytes();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }
        public void ReceiveMessage()
        {
                try
                {
                    int receiveLength = clientSocket.Receive(result);
                    ByteBuffer buff = new ByteBuffer(result);
                    // content of data
                    string data = buff.ReadString(result.Length);
                    Debug.WriteLine("content of data: " + data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            
        }

        public void SendBytes(byte[] data)
        {
            if (IsConnected == false)
                return;
            try
            {
                clientSocket.Send(data);
            }
            catch
            {
                IsConnected = false;
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }
        public void SendMessage(string data)
        {
            if (IsConnected == false)
                return;
            try
            {
                ByteBuffer buffer = new ByteBuffer();
                buffer.WriteString(data);
                clientSocket.Send(buffer.ToBytes());
            }
            catch
            {
                IsConnected = false;
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        /// <summary>
        ///  data transition, Net transmition needs two parts, 1. data length 2. data frame
        /// </summary>
        /// <param name="message"</param>
        ///  <returns></returns>

        private static byte[] WriteMessage(byte[] message)
        {
            MemoryStream ms = null;
            using (ms = new MemoryStream())
            {
                ms.Position = 0;
                BinaryWriter writer = new BinaryWriter(ms);
                ushort msglen = (ushort)message.Length;
                writer.Write(msglen);
                writer.Write(message);
                writer.Flush();
                return ms.ToArray();
            }
        }
    }
}