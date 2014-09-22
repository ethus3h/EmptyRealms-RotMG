using db;
using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace server.account
{
    class register : IRequestHandler
    {
        public bool IsValidEmail(string strIn)
        {
            var invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            MatchEvaluator DomainMapper = match =>
            {
                IdnMapping idn = new IdnMapping();

                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }
                return match.Groups[1].Value + domainName;
            };

            strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper);
            if (invalid)
                return false;

            return Regex.IsMatch(strIn,
                      @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                      RegexOptions.IgnoreCase);
        }

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
                byte[] status;
                if (0 != 0)
                    status = Encoding.UTF8.GetBytes("<Error>Invalid Username</Error>");
                else
                {
                    if (db.HasUuid(query["guid"]) &&
                        db.Verify(query["guid"], "") != null)
                    {
                        if (db.HasUuid(query["newGUID"]))
                            status = Encoding.UTF8.GetBytes("<Error>Username is already taken!</Error>");
                        else
                        {
                            var cmd = db.CreateQuery();
                            cmd.CommandText = "UPDATE accounts SET uuid=@newUuid, name=@newUuid, password=SHA1(@password), guest=FALSE WHERE uuid=@uuid, name=@name;";
                            cmd.Parameters.AddWithValue("@uuid", query["guid"]);
                            cmd.Parameters.AddWithValue("@newUuid", query["newGUID"]);
                            cmd.Parameters.AddWithValue("@password", query["newPassword"]);
                            if (cmd.ExecuteNonQuery() > 0)
                                status = Encoding.UTF8.GetBytes("<Success />");
                            else
                                status = Encoding.UTF8.GetBytes("<Error>Internal Error</Error>");
                        }
                    }
                    else
                    {
                        if (db.Register(query["newGUID"], query["newPassword"], false) != null)
                            status = Encoding.UTF8.GetBytes("<Success />");
                        else
                            status = Encoding.UTF8.GetBytes("<Error>Internal Error</Error>");
                    }
                }
                context.Response.OutputStream.Write(status, 0, status.Length);
            }
        }
    }
}
