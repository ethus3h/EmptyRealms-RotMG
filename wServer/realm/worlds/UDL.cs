namespace wServer.realm.worlds
{
    public class UndeadLair : World
    {
        public UndeadLair()
        {
            Name = "Undead Lair";
            Background = 0;
            AllowTeleport = false;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.UDL.wmap"));            
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new UndeadLair());
        }
    }
}
