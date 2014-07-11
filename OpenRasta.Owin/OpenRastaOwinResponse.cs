using System;
using OpenRasta.Web;

namespace OpenRasta.Owin
{
    public class OpenRastaOwinResponse : IResponse
    {
        public IHttpEntity Entity { get; private set; }
        public HttpHeaderDictionary Headers { get; private set; }
        public bool HeadersSent { get; private set; }
        public int StatusCode { get; set; }
        public void WriteHeaders()
        {
            throw new NotImplementedException();
        }
    }
}