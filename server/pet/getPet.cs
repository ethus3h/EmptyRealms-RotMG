using db;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Server.pet {
	class getPet: IRequestHandler {
		public void HandleRequest(HttpListenerContext context) {
			NameValueCollection query;
			using(StreamReader rdr = new StreamReader(context.Request.InputStream))
			query = HttpUtility.ParseQueryString(rdr.ReadToEnd());

			if (query.AllKeys.Length == 0) {
				string querystring = string.Empty;
				string currurl = context.Request.RawUrl;
				int iqs = currurl.IndexOf('?');
				if (iqs >= 0) {
					query = HttpUtility.ParseQueryString((iqs < currurl.Length - 1) ? currurl.Substring(iqs + 1) : string.Empty);
				}
			}
			byte[] status;

			var res = Encoding.UTF8.GetBytes(@"<!DOCTYPE html><html lang='en'><head><title>Empty Realms - Pet Service</title></head><body style='background: #333333'><h1 style='color: #EEEEEE; text-align: center'>Debug</h1></body></html>");
			context.Response.OutputStream.Write(res, 0, res.Length);
		}
	}
}