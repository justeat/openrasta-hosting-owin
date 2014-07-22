using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Microsoft.Owin;
using OpenRasta.Web;

namespace OpenRasta.Owin
{
    public class OpenRastaOwinRequest : IRequest
    {
        public OpenRastaOwinRequest(IOwinRequest ctx)
        {
            Uri = new Uri("http://localhost:900" + ctx.Path);

            //todo split this out into a different class or something
            var headerCollection = new NameValueCollection();
            foreach (var header in ctx.Headers)
            {
                headerCollection.Add(header.Key, header.Value.First());
            }

            Headers = new HttpHeaderDictionary(headerCollection);

            //todo and this
            HttpMethod = ctx.Method;

            //todo IHttpEntity needs implementing
            Entity = new HttpEntity(Headers, ctx.Body);

            CodecParameters = new List<string>();
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