namespace wServer.realm.worlds
{
    public class SnakePit : World
    {
        public SnakePit()
        {
            Name = "Snake Pit";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.snakepit.wmap"));
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new SnakePit());
        }
    }
}
