using System;
using System.Collections.Generic;
using System.Globalization;
using OpenRasta.Web;

namespace OpenRasta.Owin
{
    public class OpenRastaOwinRequest : IRequest
    {
        public OpenRastaOwinRequest(IDictionary<string, object> environment)
        {
            Uri = new Uri("http://localhost:900" + environment["owin.RequestPath"]);
        }

        public IHttpEntity Entity { get; private set; }
        public HttpHeaderDictionary Headers { get; private set; }
        public Uri Uri { get; set; }
        public string UriName { get; set; }
        public CultureInfo NegotiatedCulture { get; set; }
        public string HttpMethod { get; set; }
        public IList<string> CodecParameters { get; private set; }
    }
}