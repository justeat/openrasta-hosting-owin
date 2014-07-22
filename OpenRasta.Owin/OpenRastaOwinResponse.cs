using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using OpenRasta.DI;
using OpenRasta.Web;

namespace OpenRasta.Owin
{
    public class OpenRastaOwinResponse : IResponse
    {
        public OpenRastaOwinResponse(IOwinResponse response)
        {
            NativeContext = response;
            Headers = new HttpHeaderDictionary();
            Entity = new HttpEntity(Headers, NativeContext.Body);
        }

        private IOwinResponse NativeContext { get; set; }

        public IHttpEntity Entity { get; private set; }
        public HttpHeaderDictionary Headers { get; private set; }
        public bool HeadersSent { get; private set; }
        public int StatusCode { get; set; }

        public void WriteHeaders()
        {
            if (HeadersSent)
                throw new InvalidOperationException("The headers have already been sent.");
            foreach (var header in Headers.Where(h => h.Key != "Content-Type" && h.Key != "Content-Length"))
            {
                try
                {
                    var value = new[] {header.Value};
                    var valuePair = new KeyValuePair<string, string[]>(header.Key, value);
                    NativeContext.Headers.Add(valuePair);
                }
                catch (Exception ex)
                {
                    var commcontext = DependencyManager.GetService<ICommunicationContext>();
                    if (commcontext != null)
                        commcontext.ServerErrors.Add(new Error {Message = ex.ToString()});
                }
            }
            HeadersSent = true;
            if (Headers.ContentType != null)
            {
                NativeContext.Headers.Add(new KeyValuePair<string, string[]>("Content-Type",
                    new[] {Headers.ContentType.MediaType}));
            }
        }
    }
}