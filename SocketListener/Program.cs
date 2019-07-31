using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
public class SocketListener
{
    public static int Main(String[] args)
    {
        StartServer();
        return 0;
    }
    public static void StartServer()
    {
        IPAddress ipAddress = IPAddress.Parse("172.16.5.194");
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
        try
        {

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            Console.WriteLine("Waiting for a connection...");
            Socket handler = listener.Accept();
            Thread threadWrite = new Thread(() => Write(handler));
            Thread threadRead = new Thread(() => Read(handler));
            threadWrite.Start();
            threadRead.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\n Press any key to continue...");
        Console.ReadKey();
    }
        public static void Read(Socket sender)
        {
            while (true)
            {
                String data = "";
                byte[] bytes = new byte[1024];
                int bytesRec = sender.Receive(bytes);
                data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine("Client 1 {0}", data);
            }
        }

    public static void Write(Socket sender)
    {
        while (true)
        {
            String message = Console.ReadLine();
            byte[] msg = Encoding.ASCII.GetBytes(message);
            sender.Send(msg);
          }
        }
     }
    