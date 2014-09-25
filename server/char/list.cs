using db;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Server.@char
{
    class list : IRequestHandler
    {
        public void HandleRequest(HttpListenerContext context)
        {
            NameValueCollection query;
            using (StreamReader rdr = new StreamReader(context.Request.InputStream))
                query = HttpUtility.ParseQueryString(rdr.ReadToEnd());

            if (query.AllKeys.Length == 0)
            {
                string querystring = string.Empty;
                string currurl = context.Request.RawUrl;
                int iqs = currurl.IndexOf('?');
                if (iqs >= 0)
                {
                    query =
                        HttpUtility.ParseQueryString((iqs < currurl.Length - 1)
                            ? currurl.Substring(iqs + 1)
                            : string.Empty);
                }
            }

            using (var db = new Database())
            {
                List<ServerItem> filteredServers = null;
                Account a = db.Verify(query["guid"], query["password"]);
                if (a != null)
                {
                    if (a.Banned)
                    {
                        filteredServers = YoureBanned();
                    }
                    else
                    {
                        filteredServers = GetServersForRank(a.Rank);
                    }
                }
                else
                {
                    filteredServers = GetServersForRank(0);
                }

                Chars chrs = new Chars()
                {
                    Characters = new List<Char>() { },
                    NextCharId = 2,
                    MaxNumChars = 1,
                    Account = db.Verify(query["guid"], query["password"]),
                    Servers = filteredServers
                };

                Account dvh = null;
                if (chrs.Account != null)
                {
                    db.GetCharData(chrs.Account, chrs);
                    db.LoadCharacters(chrs.Account, chrs);
                    chrs.News = db.GetNews(chrs.Account);
                    dvh = chrs.Account;
                }
                else
                {
                    chrs.Account = Database.CreateGuestAccount(query["guid"]);
                    chrs.News = db.GetNews(null);
                }

                MemoryStream ms = new MemoryStream();
                XmlSerializer serializer = new XmlSerializer(chrs.GetType(), new XmlRootAttribute(chrs.GetType().Name) { Namespace = "" });

                XmlWriterSettings xws = new XmlWriterSettings();
                xws.OmitXmlDeclaration = true;
                xws.Encoding = Encoding.UTF8;
                XmlWriter xtw = XmlWriter.Create(context.Response.OutputStream, xws);
                serializer.Serialize(xtw, chrs, chrs.Namespaces);
            }
        }

        public static List<ServerItem> GetServersForRank(int r)
        {
            List<ServerItem> slist = GetServers();
            List<ServerItem> removedServers = new List<ServerItem>();

            foreach (var i in slist)
                if (i.RankRequired > r)
                    removedServers.Add(i);

            foreach (var i in removedServers)
                slist.Remove(i);

            return slist;
        }

        public static List<ServerItem> GetServers()
        {
            List<ServerItem> Servers = new List<ServerItem>()
                {
                    new ServerItem()
                        {
                            Name = "Empty Realms",
                            Lat = 57.282,
                            Long = 43.1076,
                            DNS = "25.103.23.132",
                            Usage = 0.2,
                            AdminOnly = false,
                            RankRequired = 0
                        },                   
                        new ServerItem()
                        {
                            Name = "Localhost",
                            Lat = 57.282,
                            Long = 43.1076,
                            DNS = "127.0.0.1",
                            Usage = 0.2,
                            AdminOnly = false,
                            RankRequired = 8
                        }
                };
            return Servers;
        }

        public static List<ServerItem> YoureBanned()
        {
            List<ServerItem> Servers = new List<ServerItem>()
                {
                    new ServerItem()
                        {
                            Name = "You're Banned!",
                            Lat = 57.282,
                            Long = 43.1076,
                            DNS = "",
                            Usage = 0.2,
                            AdminOnly = false
                        }
                };
            return Servers;
        }
    }
}
