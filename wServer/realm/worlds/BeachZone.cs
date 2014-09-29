﻿namespace wServer.realm.worlds
{
    public class BeachZone : World
    {
        public BeachZone()
        {
            Name = "Beachzone";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.bz.wmap"));
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new Abyss());
        }
    }
}
