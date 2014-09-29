namespace wServer.realm.worlds
{
    public class GuildHall : World
    {
        public string Guild { get; set; }
        
        public GuildHall(string guild)
        {
            Id = GHALL;
            Guild = guild;
            Name = "Guild Hall";
            Background = 0;
            AllowTeleport = true;
            switch (this.Level())
            {
                case 0:
                    base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.ghall0.wmap"));
                    break;
                case 1:
                    base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.ghall1.wmap"));
                    break;
                case 2:
                    base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.ghall2.wmap"));
                    break;
                case 3:
                    base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.ghall3.wmap"));
                    break;
            }
            //base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.wmap.guildhall0old.wmap"));            
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new GuildHall(Guild));
        }
        public int Level()
        {
            using (db.Database dbx = new db.Database())
            {
                int id = dbx.GetGuildId(this.Guild);
                return dbx.GetGuildLevel(id);
            }
        }
    }
}
