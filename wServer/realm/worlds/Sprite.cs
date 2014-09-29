﻿namespace wServer.realm.worlds
{
    public class SpriteWorld : World
    {
        public SpriteWorld()
        {
            Name = "Sprite World";
            Background = 1;
            AllowTeleport = false;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.sprite.wmap"));
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new SpriteWorld());
        }
    }
}
