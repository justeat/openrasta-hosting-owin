using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Microsoft.Owin;
using OpenRasta.Web;

namespace OpenRasta.Owin
{
    public class OwinRequest : IRequest
    {
        public OwinRequest(IOwinRequest ctx)
        {
            Uri = ctx.Uri;
            PopulateHeaders(ctx);
            HttpMethod = ctx.Method;
            Entity = new HttpEntity(Headers, ctx.Body);
            CodecParameters = new List<string>();
        }

        private void PopulateHeaders(IOwinRequest ctx)
        {
            var headerCollection = new NameValueCollection();
            foreach (var header in ctx.Headers)
            {
                headerCollection.Add(header.Key, header.Value.First());
            }

            Headers = new HttpHeaderDictionary(headerCollection);

            if(Headers.ContentLength == null)
            {
                Headers.ContentLength = ctx.Body.Length;
            }
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