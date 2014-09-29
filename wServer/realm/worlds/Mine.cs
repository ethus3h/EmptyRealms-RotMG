namespace wServer.realm.worlds
{
    public class Mine : World
    {
        public Mine()
        {
            Name = "Underground Mine";
            Background = 0;
            AllowTeleport = false;
            Mining = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.mine.wmap"));            
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new Mine());
        }
    }
}
