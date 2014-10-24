using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Server.account
{
    class forgotPassword : IRequestHandler
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

            byte[] status = Encoding.UTF8.GetBytes("<Error>This function has been disabled, please contact a server admin.</Error>");
            context.Response.OutputStream.Write(status, 0, status.Length);
        }
    }
}