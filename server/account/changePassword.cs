﻿using db;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Server.account
{
    class changePassword : IRequestHandler
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
                    query = HttpUtility.ParseQueryString((iqs < currurl.Length - 1) ? currurl.Substring(iqs + 1) : string.Empty);
                }
            }

            using (var db = new Database())
            {
                var acc = db.Verify(query["guid"], query["password"]);
                byte[] status;
                if (acc == null)
                {
                    status = Encoding.UTF8.GetBytes("<Error>Bad login</Error>");
                }
                else
                {
                    var cmd = db.CreateQuery();
                    cmd.CommandText = "UPDATE accounts SET password=SHA1(@password) WHERE id=@accId;";
                    cmd.Parameters.AddWithValue("@accId", acc.AccountId);
                    cmd.Parameters.AddWithValue("@password", query["newPassword"]);
                    if (cmd.ExecuteNonQuery() > 0) status = Encoding.UTF8.GetBytes("<Success />");
                    else status = Encoding.UTF8.GetBytes("<Error>Internal error</Error>");
                }
                context.Response.OutputStream.Write(status, 0, status.Length);
            }
        }
    }
}