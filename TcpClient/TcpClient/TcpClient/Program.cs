using System;
using System.Net;
using System.Net.Sockets;

namespace TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {

                    string toSend = string.Empty;

                    Console.WriteLine("Choose Code 1 - IsValid , 2 - Puttoken 3 - Error Protokol 4 - Error Format");

                    var code = Console.ReadLine();

                    if (code.Equals("1"))
                    {
                        toSend = "FNNM10"; //"FNNM11INPT00000255"
                    }
                    else if (code.Equals("2"))
                    {
                        toSend = "FNNM11INPT00000255"; //
                    }
                    else if (code.Equals("3"))
                    {
                        toSend = "FNNM55";
                    }
                    else if (code.Equals("4"))
                    {
                        toSend = "asdasdasd";
                    }
                    else if (code.Equals("0"))
                    {
                        break;
                    }

                    IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5585);

                    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.Connect(serverAddress);

                    Console.WriteLine(toSend);

                    // Sending
                    int toSendLen = System.Text.Encoding.ASCII.GetByteCount(toSend);
                    byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(toSend);
                    byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
                    clientSocket.Send(toSendLenBytes);
                    clientSocket.Send(toSendBytes);

                    // Receiving
                    byte[] rcvLenBytes = new byte[4];
                    clientSocket.Receive(rcvLenBytes);
                    int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
                    byte[] rcvBytes = new byte[rcvLen];
                    clientSocket.Receive(rcvBytes);
                    String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);

                    Console.WriteLine("Client received: " + rcv);

                    clientSocket.Close();

                    //Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                    Console.ReadLine();
                }
            }
        }
    }
}
