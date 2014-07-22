using System;
using System.Web;
using OpenRasta.Diagnostics;

namespace OpenRasta.Owin
{
    public class OpenRastaIntegratedHandler : IHttpHandler
    {
        public OpenRastaIntegratedHandler()
        {
            Log = NullLogger.Instance;
        }

        public ILogger Log { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            throw new Exception("owin doesn't support httpcontext what ya doing full!");
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(OwinCommunicationContext context)
        {
            OpenRastaMiddleware.Host.RaiseIncomingRequestReceived(context);
        }
    }
}