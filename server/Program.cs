using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;

namespace Server
{
    class Program
    {
        static HttpListener listener;
        static readonly Thread[] workers = new Thread[5];
        static readonly Queue<HttpListenerContext> contextQueue = new Queue<HttpListenerContext>();
        static readonly object queueLock = new object();
        static readonly ManualResetEvent queueReady = new ManualResetEvent(false);

        public XmlDatas GameData
        {
            get;
            private set;
        }

        const int dataserverport = 8887;

        static ILog log = LogManager.GetLogger("Server");

        static void Main(string[] args) {
            XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.Name = "Entry";

            listener = new HttpListener();
            listener.Prefixes.Add("http://*:" + dataserverport + "/");
            listener.Start();

            listener.BeginGetContext(ListenerCallback, null);
            for (var i = 0; i < workers.Length; i++) {
                workers[i] = new Thread(Worker) {
                    Name = "Worker " + i
                };
                workers[i].Start();
            }
            Console.CancelKeyPress += (sender, e) => e.Cancel = true;

            Console.WriteLine("Starting Data Server on port {0}.", dataserverport);
            Thread.Sleep(2500);
            Console.WriteLine("Successfully started Data Server.");

            while (Console.ReadKey(true).Key != ConsoleKey.Escape) {
                Console.WriteLine("Terminating...");
                listener.Stop();
                while (contextQueue.Count > 0)
                Thread.Sleep(100);
                Environment.Exit(0);
            };
        }

        static void ListenerCallback(IAsyncResult ar)
        {
            if (!listener.IsListening) return;
            var context = listener.EndGetContext(ar);
            listener.BeginGetContext(ListenerCallback, null);
            lock (queueLock)
            {
                contextQueue.Enqueue(context);
                queueReady.Set();
            }
        }

        static void Worker()
        {
            while (queueReady.WaitOne())
            {
                HttpListenerContext context;
                lock (queueLock)
                {
                    if (contextQueue.Count > 0) context = contextQueue.Dequeue();
                    else
                    {
                        queueReady.Reset();
                        continue;
                    }
                }

                try
                {
                    ProcessRequest(context);
                }
                catch { }
            }
        }

        static void ProcessRequest(HttpListenerContext context)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[REQUEST] Request incoming.");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[TIME] {0}", DateTime.Now);
                Console.WriteLine("[URL] {0}", context.Request.Url.LocalPath);
                Console.WriteLine("[IP] {0}", context.Request.RemoteEndPoint);
                Console.ResetColor();

                IRequestHandler handler;

                if (!RequestHandlers.Handlers.TryGetValue(context.Request.Url.LocalPath, out handler))
                {
                    context.Response.StatusCode = 400;
                    context.Response.StatusDescription = "Bad request";
                    using (StreamWriter wtr = new StreamWriter(context.Response.OutputStream))
                        wtr.Write("<h1>Bad request</h1>");
                }
                else handler.HandleRequest(context);
            }
            catch (Exception e)
            {
                using (StreamWriter wtr = new StreamWriter(context.Response.OutputStream))
                    wtr.Write("<Error>Internal Server Error</Error>");
                log.Error("Error when dispatching request", e);
            }

            context.Response.Close();
        }
    }
}