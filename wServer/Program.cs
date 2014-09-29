using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using wServer.realm;

namespace wServer
{
    static class Program
    {
        static Socket svrSkt;

        static void HostPolicyServer()
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 843);
                listener.Start();
                listener.BeginAcceptTcpClient(ServePolicyFile, listener);
            }
            catch { }
        }

        static void Main(string[] args)
        {
            const int gameserverport = 2050;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            svrSkt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            svrSkt.Bind(new IPEndPoint(IPAddress.Any, gameserverport));
            svrSkt.Listen(0xff);
            svrSkt.BeginAccept(Listen, null);
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Saving Please Wait...");
                svrSkt.Close();
                foreach (var i in RealmManager.Clients.Values.ToArray())
                {
                    i.Save();
                    i.Disconnect();
                }
                Console.WriteLine("\nClosing...");
                Thread.Sleep(500);
                Environment.Exit(0);
            };
            
            Console.WriteLine("Starting Game Server on port {0}.", gameserverport);
            Thread.Sleep(5000);
            Console.WriteLine("Successfully started Game Server.");

            HostPolicyServer();

            RealmManager.CoreTickLoop();    //Never returns
        }

        static void ServePolicyFile(IAsyncResult ar)
        {
            TcpClient cli = (ar.AsyncState as TcpListener).EndAcceptTcpClient(ar);
            (ar.AsyncState as TcpListener).BeginAcceptTcpClient(ServePolicyFile, ar.AsyncState);
            try
            {
                var s = cli.GetStream();
                NReader rdr = new NReader(s);
                NWriter wtr = new NWriter(s);
                if (rdr.ReadNullTerminatedString() == "<policy-file-request/>")
                {
                    wtr.WriteNullTerminatedString(@"<cross-domain-policy>
     <allow-access-from domain=""*"" to-ports=""*"" />
</cross-domain-policy>");
                    wtr.Write((byte)'\r');
                    wtr.Write((byte)'\n');
                }
                cli.Close();
            }
            catch { }
        }

        static void Listen(IAsyncResult ar)
        {
            Socket skt = null;
            try
            {
                skt = svrSkt.EndAccept(ar);
            }
            catch (ObjectDisposedException)
            { 
            }
            try
            {
                svrSkt.BeginAccept(Listen, null);
            }
            catch (ObjectDisposedException)
            {
            }
            if (skt != null)
            {
                var psr = new ClientProcessor(skt);
                psr.BeginProcess();
            }
        }
    }
}
