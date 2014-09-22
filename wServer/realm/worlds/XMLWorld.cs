using System.IO;
using System.Net;
using terrain;

namespace wServer.realm.worlds
{
    public class XMLWorld : World
    {
        DungeonDesc d;

        public XMLWorld(DungeonDesc desc)
        {
            this.d = desc;
            string json = new WebClient().DownloadString(desc.Json);

            Name = desc.Name;
            Background = desc.Background;
            AllowTeleport = desc.AllowTeleport;
            base.FromWorldMap(new MemoryStream(Json2Wmap.Convert(json)));
        }

        public override World GetInstance(ClientProcessor psr)
        {
            return RealmManager.AddWorld(new XMLWorld(d));
        }                 
    }
}
