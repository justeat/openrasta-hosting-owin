using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Owin;
using OpenRasta.Diagnostics;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace OpenRasta.Owin
{
    public class OwinCommunicationContext : ICommunicationContext
    {
        private readonly IOwinContext _nativeContext;

        public OwinCommunicationContext(ref IOwinContext nativeContext,ILogger logger)
        {
            PipelineData = new PipelineData();
            _nativeContext = nativeContext;
            Request = new OwinRequest(nativeContext.Request);
            Response = new OwinResponse(ref nativeContext);
            ServerErrors = new ServerErrorList { Log = logger };
            User = nativeContext.Request.User;
        }

        public Uri ApplicationBaseUri
        {
            get
            {
                var request = _nativeContext.Request;

                string baseUri = "{0}://{1}{2}/".With(request.Uri.Scheme,
                                                     request.Uri.Host,
                                                      request.Uri.IsDefaultPort ? string.Empty : ":" + request.Uri.Port);
                //todo manage the relative path if needed?
                var appBaseUri = new Uri(baseUri, UriKind.Absolute);//, new Uri(_host.ApplicationVirtualPath, UriKind.Relative));
                return appBaseUri;
            }
        }

        public IRequest Request { get; set; }
        public IResponse Response { get; set; }
        public OperationResult OperationResult { get; set; }
        public PipelineData PipelineData { get; set; }
        public IList<Error> ServerErrors { get; set; }
        public IPrincipal User
        {
            get { return _nativeContext.Request.User; }
            set { _nativeContext.Request.User = value; }
        }
    }
}