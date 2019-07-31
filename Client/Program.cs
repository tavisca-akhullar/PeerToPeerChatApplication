using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class Client
{
    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }


    public static void StartClient()
    {
        byte[] bytes = new byte[1024];

        try
        {
            IPAddress ipAddress = IPAddress.Parse("172.16.5.194");
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sender.Connect(remoteEP);
                Console.WriteLine("Socket connected to {0}",
                sender.RemoteEndPoint.ToString());
                Thread threadWrite = new Thread(() => Write(sender));
                Thread threadRead = new Thread(() => Read(sender));
                threadWrite.Start();
                threadRead.Start();
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void Read(Socket sender)
    {
        while (true)
        {
            string data = "";
            byte[] bytes = new byte[1024];
            int bytesRec = sender.Receive(bytes);
            data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            Console.WriteLine("Client 2 {0}", data);
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
