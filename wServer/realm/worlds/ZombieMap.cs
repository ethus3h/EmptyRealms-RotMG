using System;
using System.Collections.Generic;
using wServer.svrPackets;

namespace wServer.realm.worlds
{
    public class ZombieMap : World
    {
        public int Wave = 0;
        public bool Waiting = false;

        public string[] Zombies;
        public string[] SpecialZombies;

        public void InitWaveEnemies()
        {
            Zombies = new string[] { "Zombie", "Hell Hound", "Crawler" };
        }

        public bool OutOfBounds(float x, float y)
        {
            return ((x <= 0.5f || x >= 255.5f) || (y <= 0.5f || y >= 255.5f));
        }

        public ZombieMap()
        {
            Name = "Zombies";
            Background = 0;
            AllowTeleport = false;
            Random r = new Random();
            InitWaveEnemies();
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.zombies.wmap"));
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new ZombieMap());
        }

        public void SpawnEnemies()
        {
            List<string> enems = new List<string>();
            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                enems.Add(Zombies[r.Next(0, Zombies.Length - 1)]);
            }
            Random r2 = new Random();
            foreach (string i in enems)
            {
                short id = XmlDatas.IdToType[i];
                float xloc = (float)r2.Next(1, 254) + 0.5f;
                float yloc = (float)r2.Next(1, 254) + 0.5f;
                Entity enemy = Entity.Resolve(id);
                enemy.Move(xloc, yloc);
                EnterWorld(enemy);
            }
        }

        public override void Tick(RealmTime time)
        {
            base.Tick(time);

            if (Players.Count > 0)
            {
                if (Enemies.Count < 1)
                {
                    if (!Waiting)
                    {
                        Wave++;
                        Waiting = true;
                        Timers.Add(new WorldTimer(1500, (w, t) =>
                        {
                            foreach (var i in Players)
                            {
                                i.Value.Client.SendPacket(new NotificationPacket()
                                {
                                    Color = new ARGB(0xffff00ff),
                                    ObjectId = i.Value.Id,
                                    Text = "Wave " + Wave.ToString() + " Starting in 5.."
                                });
                            }
                            Timers.Add(new WorldTimer(1000, (w1, t1) =>
                                {
                                    foreach (var i in Players)
                                    {
                                        i.Value.Client.SendPacket(new NotificationPacket()
                                        {
                                            Color = new ARGB(0xffff00ff),
                                            ObjectId = i.Value.Id,
                                            Text = "Wave " + Wave.ToString() + " Starting in 4.."
                                        });
                                    }
                                    Timers.Add(new WorldTimer(1000, (w2, t2) =>
                                    {
                                        foreach (var i in Players)
                                        {
                                            i.Value.Client.SendPacket(new NotificationPacket()
                                            {
                                                Color = new ARGB(0xffff00ff),
                                                ObjectId = i.Value.Id,
                                                Text = "Wave " + Wave.ToString() + " Starting in 3.."
                                            });
                                        }
                                        Timers.Add(new WorldTimer(1000, (w3, t3) =>
                                        {
                                            foreach (var i in Players)
                                            {
                                                i.Value.Client.SendPacket(new NotificationPacket()
                                                {
                                                    Color = new ARGB(0xffff00ff),
                                                    ObjectId = i.Value.Id,
                                                    Text = "Wave " + Wave.ToString() + " Starting in 2.."
                                                });
                                            }
                                            Timers.Add(new WorldTimer(1000, (w4, t4) =>
                                            {
                                                foreach (var i in Players)
                                                {
                                                    i.Value.Client.SendPacket(new NotificationPacket()
                                                    {
                                                        Color = new ARGB(0xffff00ff),
                                                        ObjectId = i.Value.Id,
                                                        Text = "Wave " + Wave.ToString() + " Starting.."
                                                    });
                                                }
                                                Timers.Add(new WorldTimer(500, (w5, t5) =>
                                                {
                                                    SpawnEnemies();
                                                    Waiting = false;
                                                }));

                                            }));
                                        }));
                                    }));
                                 }));
                        }));
                        
                        
                    }
                }
                else
                {
                    foreach (var i in Enemies)
                    {
                        if (OutOfBounds(i.Value.X, i.Value.Y))
                        {
                            LeaveWorld(i.Value);
                        }
                    }
                }
            }
            else
            {
                foreach (var i in Enemies)
                {
                    LeaveWorld(i.Value);
                    Wave = 0;
                }
            }
        }
    }
}
