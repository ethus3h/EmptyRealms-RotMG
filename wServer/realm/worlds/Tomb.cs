namespace wServer.realm.worlds
{
    public class TombMap : World
    {
        public TombMap()
        {
            Name = "Tomb of the Ancients";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.tomb.wmap"));
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new TombMap());
        }
    }
}
