using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace server.picture
{

    class list : IRequestHandler
    {
        byte[] buff = new byte[0x10000];
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

            Pics pics = new Pics()
            {
                SearchOffset = Convert.ToInt32(query["offset"]),
                Pictures = new List<Pic>() {
                    new Pic() {
                        PicName = "Dark Fire",
                        PictureId = "dark_fire",
                        DataType = 2,
                        Tags = new List<string>() {
                            "test",
                            "test2",
                            "dark",
                            "fire"
                        }
                    }
                }
            };

            XmlSerializer serializer = new XmlSerializer(pics.GetType(), new XmlRootAttribute(pics.GetType().Name) { Namespace = "" });

            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Encoding = Encoding.UTF8;
            XmlWriter xtw = XmlWriter.Create(context.Response.OutputStream, xws);
            serializer.Serialize(xtw, pics, pics.Namespaces);
        }
    }
}
