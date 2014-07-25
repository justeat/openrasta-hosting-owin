using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin;
using OpenRasta.Configuration;
using OpenRasta.Hosting;

namespace OpenRasta.Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    using OwinVariables = IDictionary<string, object>;

    public class OpenRastaMiddleware : OwinMiddleware
    {
        private static readonly object SyncRoot = new object();
        private readonly IConfigurationSource _options;
        private HostManager _hostManager;

        public OpenRastaMiddleware(OwinMiddleware next, IConfigurationSource options)
            : base(next)
        {
            _options = options;
            Host = new OwinHost();
        }

        public static OwinHost Host { get; private set; }

        public override async Task Invoke(IOwinContext owinContext)
        {
            TryInitializeHosting();

            var context = new OwinCommunicationContext(owinContext);

            Host.RaiseIncomingRequestReceived(context);
            Host.RaiseIncomingRequestProcessed(context);

            var ms = new MemoryStream(Encoding.UTF8.GetBytes("Hello from TestMiddleware!"));

            owinContext.Response.StatusCode = context.Response.StatusCode;

            await ms.CopyToAsync(owinContext.Response.Body);
        }


        private void TryInitializeHosting()
        {
            if (_hostManager != null) return;
            lock (SyncRoot)
            {
                Thread.MemoryBarrier();
                if (_hostManager == null)
                {
                    var hostManager = HostManager.RegisterHost(Host);
                    Thread.MemoryBarrier();
                    _hostManager = hostManager;
                    try
                    {
                        Host.ConfigurationSource = _options;
                        Host.RaiseStart();
                    }
                    catch
                    {
                        HostManager.UnregisterHost(Host);
                        _hostManager = null;
                    }
                }
            }
        }
    }
}