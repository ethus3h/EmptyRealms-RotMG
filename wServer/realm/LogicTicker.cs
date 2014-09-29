using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace wServer.realm
{
    public class LogicTicker
    {
        public const int TPS = 20;
        public const int MsPT = 1000 / TPS;
        public LogicTicker()
        {
            pendings = new ConcurrentQueue<Action<RealmTime>>[5];
            for (int i = 0; i < 5; i++)
                pendings[i] = new ConcurrentQueue<Action<RealmTime>>();
        }

        public void AddPendingAction(Action<RealmTime> callback)
        {
            AddPendingAction(callback, PendingPriority.Normal);
        }
        public void AddPendingAction(Action<RealmTime> callback, PendingPriority priority)
        {
            pendings[(int)priority].Enqueue(callback);
        }
        readonly ConcurrentQueue<Action<RealmTime>>[] pendings;


        public static RealmTime CurrentTime;
        public void TickLoop()
        {
            Stopwatch watch = new Stopwatch();
            long dt = 0;
            long count = 0;

            watch.Start();
            RealmTime t = new RealmTime();
            long xa = 0;
            do
            {
                long times = dt / MsPT;
                dt -= times * MsPT;
                times++;

                long b = watch.ElapsedMilliseconds;

                count += times;
                if (times > 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[INFO] Game Server lagged!");
                    Console.WriteLine(@"[INFO] Times: " + times + 
                        " \n[INFO] DT: " + dt +
                        " \n[INFO] Count: " + count +
                        " \n[INFO] Time: " + b +
                        " \n[INFO] TPS: " + count / (b / 1000.0) +
                        " \n[INFO] End of INFO\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    var _directory = @"logs";
                    if (!Directory.Exists(_directory))
                    {
                        Directory.CreateDirectory(_directory);
                    }
                    using (var writer = new StreamWriter(@"logs\LAGGED.log", true))
                    {
                        writer.WriteLine("[{0}] Times: {1}, DT: {2}, Count: {3}, Time: {4}, TPS: {5}", System.DateTime.Now, times, dt, count, b , count / (b / 1000.0));
                    }
                }

                t.tickTimes = b;
                t.tickCount = count;
                t.thisTickCounts = (int)times;
                t.thisTickTimes = (int)(times * MsPT);
                xa += t.thisTickTimes;

                foreach (var i in pendings)
                {
                    Action<RealmTime> callback;
                    while (i.TryDequeue(out callback))
                    {
                        try
                        {
                            callback(t);
                        }
                        catch { }
                    }
                }
                TickWorlds1(t);

                Thread.Sleep(MsPT);
                dt += Math.Max(0, watch.ElapsedMilliseconds - b - MsPT);

            } while (true);
        }

        void TickWorlds1(RealmTime t)    //Continous simulation
        {
            CurrentTime = t;
            foreach (var i in RealmManager.Worlds.Values.Distinct())
                i.Tick(t);
        }

        void TickWorlds2(RealmTime t)    //Discrete simulation
        {
            long counter = t.thisTickTimes;
            long c = t.tickCount - t.thisTickCounts;
            long x = t.tickTimes - t.thisTickTimes;
            while (counter >= MsPT)
            {
                c++; x += MsPT;
                TickWorlds1(new RealmTime()
                {
                    thisTickCounts = 1,
                    thisTickTimes = MsPT,
                    tickCount = c,
                    tickTimes = x
                });
                counter -= MsPT;
            }
        }
    }
}
