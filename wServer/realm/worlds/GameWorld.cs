﻿using System;
using wServer.realm.entities;
using wServer.realm.setpieces;

namespace wServer.realm.worlds
{
    class GameWorld : World
    {
        public static GameWorld AutoName(int mapId, bool oryxPresent)
        {
            string name = RealmManager.realmNames[new Random().Next(RealmManager.realmNames.Count)];
            RealmManager.realmNames.Remove(name);
            return new GameWorld(mapId, name, oryxPresent);
        }
        public GameWorld(int mapId, string name, bool oryxPresent)
        {
            Name = name;
            Background = 0;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.world" + mapId + ".wmap"));
            SetPieces.ApplySetPieces(this);
            if (oryxPresent)
            {
                Overseer = new Oryx(this);
                Overseer.Init();
            }
            else
                Overseer = null;
        }

        public Oryx Overseer { get; private set; }

        public override void Tick(RealmTime time)
        {
            base.Tick(time);
            if (Overseer != null)
                Overseer.Tick(time);
        }

        public void EnemyKilled(Enemy enemy, Character killer)
        {
            if (Overseer != null)
                Overseer.OnEnemyKilled(enemy, killer);
        }

        public override int EnterWorld(Entity entity)
        {
            var ret = base.EnterWorld(entity);
            if (entity is Player)
                Overseer.OnPlayerEntered(entity as Player);
            return ret;
        }
    }
}
