using SocketAsync;

namespace AsyncSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketClient client = new SocketClient();

            client.RaiseTextReceivedEvent += HandleTextReceived;

            Console.WriteLine("***Socket Client Example - Type a valid IP address:");
            string strIPAddress = Console.ReadLine();
            Console.WriteLine("Indicate Port (0-65535)");
            string strPort = Console.ReadLine();

            if (!client.SetServerIPAddress(strIPAddress) || !client.SetPortNumber(strPort))
            {
                Console.WriteLine(string.Format("Wrong IP address or port supplied - {0} - {1}", strIPAddress, strPort));
                Console.ReadKey();
                return;
            }

            client.ConnectToServer();

            string strInputUser = null;

            do
            {
                strInputUser = Console.ReadLine();
                if(strInputUser.Trim() != "EXIT")
                {
                    client.SendToServer(strInputUser);
                }
                else if (strInputUser.Equals("EXIT"))
                {
                    client.CloseAndDisconnect();
                }
            } while (strInputUser != "EXIT");
        }

        private static void HandleTextReceived(object sender, TextReceivedEventArgs trea)
        {
            Console.WriteLine(string.Format("{0} - Received: {1}{2}",DateTime.Now,trea.SentText,Environment.NewLine));
        }
    }
}