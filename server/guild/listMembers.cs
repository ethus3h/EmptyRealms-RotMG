﻿using db;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Server.guild
{
    class listMembers : IRequestHandler
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
                if (acc == null) status = Encoding.UTF8.GetBytes("<Error>Bad login</Error>");
                else
                {
                    try
                    {
                        status = Encoding.UTF8.GetBytes(db.HTTPGetGuildMembers(Convert.ToInt32(query["num"]), Convert.ToInt32(query["offset"]), acc));
                    }
                    catch
                    {
                        status = Encoding.UTF8.GetBytes("<Error>Guild member error</Error>");
                    }
                }
                context.Response.OutputStream.Write(status, 0, status.Length);
                context.Response.Close();
            }
        }
    }
}