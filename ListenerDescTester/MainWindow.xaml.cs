using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace ListenerDescTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIsValidCheck_Click(object sender, RoutedEventArgs e)
        {
            txbSendTo.Text = "FNNM10";
        }

        private void btnPutToken_Click(object sender, RoutedEventArgs e)
        {
            txbSendTo.Text = "FNNM11INPT0000641234567891234567891234657891234567891234567891234657891234567891";
        }

        private void btnWrongFormat_Click(object sender, RoutedEventArgs e)
        {
            txbSendTo.Text = "asdasdasd";
        }

        private string SendTcpMessage(string input)
        {
            string message = string.Empty;

            try
            {
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5585);

                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(serverAddress);

                Console.WriteLine(input);

                // Sending
                int toSendLen = System.Text.Encoding.ASCII.GetByteCount(input);
                byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(input);
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

                message = rcv;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return message;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            txbReturn.Text = SendTcpMessage(txbSendTo.Text);
        }
    }
}
