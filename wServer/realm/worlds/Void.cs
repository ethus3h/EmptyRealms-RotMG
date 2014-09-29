﻿namespace wServer.realm.worlds
{
    public class VoidWorld : World
    {
        public VoidWorld()
        {
            Name = "The Void";
            Background = 1;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.void.wmap"));            
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new VoidWorld());
        }
    }
}
