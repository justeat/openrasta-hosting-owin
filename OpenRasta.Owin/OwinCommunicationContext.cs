using System;
using System.Collections.Generic;
using System.Security.Principal;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace OpenRasta.Owin
{
    public class OwinCommunicationContext : ICommunicationContext
    {
        public OwinCommunicationContext()
        {
            PipelineData = new PipelineData();
        }

        public Uri ApplicationBaseUri { get; set; }
        public IRequest Request { get; set; }
        public IResponse Response { get; set; }
        public OperationResult OperationResult { get; set; }
        public PipelineData PipelineData { get; set; }
        public IList<Error> ServerErrors { get; set; }
        public IPrincipal User { get; set; }
    }
}