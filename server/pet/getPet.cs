using db;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Server.pet
{
    class getPet : IRequestHandler
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
            byte[] status;
            using (var db = new Database())
            {
                var acc = db.Verify(query["guid"], query["password"]);
                if (acc == null)
                {
                    status = Encoding.UTF8.GetBytes("<Error>Bad login</Error>");
                }
                var queryx = HttpUtility.ParseQueryString(context.Request.Url.Query);
                var cmd = db.CreateQuery();
                cmd.CommandText = "SELECT id FROM accounts WHERE uuid=@uuid";
                cmd.Parameters.AddWithValue("@uuid", query["guid"]);
                object id = cmd.ExecuteScalar();

                if (id != null)
                {
                    //int amount = int.Parse(query["jwt"]);

                    cmd = db.CreateQuery();
                    cmd.CommandText = "INSERT INTO pets SET accId = @accId WHERE accId=@accId";
                    cmd.Parameters.AddWithValue("@accId", (int)id);
                    //cmd.Parameters.AddWithValue("@amount", amount);
                    int result_ = (int)cmd.ExecuteNonQuery();

                    if ((result_ > 0))
                        status = Encoding.UTF8.GetBytes("<Success />");
                    else
                        status = Encoding.UTF8.GetBytes("<Error />");
                }
                else
                    status = Encoding.UTF8.GetBytes("<No Login/>");
            }
            var res = Encoding.UTF8.GetBytes(@"<!DOCTYPE html><html lang='en'><head><title>Empty Realms - Pet Service</title></head><body style='background: #333333'><h1 style='color: #EEEEEE; text-align: center'>" + status + "</h1></body></html>");
            context.Response.OutputStream.Write(res, 0, res.Length);
        }
    }
}
