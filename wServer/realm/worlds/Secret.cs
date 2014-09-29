namespace wServer.realm.worlds
{
    public class Secret : World
    {
        public Secret()
        {
            Name = "?????";
            Background = 0;
            AllowTeleport = false;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.secret.wmap"));            
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new Secret());
        }
    }
}
