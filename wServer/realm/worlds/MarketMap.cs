namespace wServer.realm.worlds
{
    public class MarketMap : World
    {
        public MarketMap()
        {
            Id = MARKET;
            Name = "Market";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.MarketMap.wmap"));
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new GauntletMap());
        }
    }
}
