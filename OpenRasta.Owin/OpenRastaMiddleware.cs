using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.Hosting;
using OpenRasta.Pipeline;

namespace OpenRasta.Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    using OwinVariables = IDictionary<string, object>;

    public class OpenRastaMiddleware
    {
        private readonly AppFunc _next;
        private HostManager _hostManager;
        private IConfigurationSource _options;
        private PipelineData _pipelineData;
        private Dictionary<string, AppFunc> _requestDispatcher;
        static readonly object _syncRoot = new object();
        public static OwinHost Host { get; private set; }

        public OpenRastaMiddleware(AppFunc next, IConfigurationSource options)
        {
            _next = next;
            _options = options;
            Host = new OwinHost();
    

          //  todo need to figure out how to initialize the pipelines
            //_pipelineData = new PipelineData();
         //   var stage = _pipelineData.PipelineStage;
         //   if (stage == null)
         //       _pipelineData.PipelineStage = stage = new PipelineStage(_hostManager.Resolver.Resolve<IPipeline>());

          //  stage.SuspendAfter<KnownStages.IUriMatching>();
        }

        public Task Invoke(OwinVariables environment)
        {
            var Request = new Uri("http://localhost:900");
            try
            {
                TryInitializeHosting();

                var context = new OwinCommunicationContext() { ApplicationBaseUri = Request, Request = new OpenRastaOwinRequest(environment), Response = new OpenRastaOwinResponse()};
                _hostManager.Resolver.Resolve<OpenRastaIntegratedHandler>().ProcessRequest(context);
            }
            catch (Exception e)
            {
               
                WriteToResponseStream(environment, e.ToString());
            }
           
            return _next(environment);
        }

        private void WriteToResponseStream(OwinVariables environment, string message)
        {
            var response = environment["owin.ResponseBody"] as Stream;
            var streamWriter = new StreamWriter(response);
            Task.Factory.StartNew(() =>
            {
                streamWriter.Write(message);
                streamWriter.Dispose();
            });
        }

        public void TryInitializeHosting()
        {
            if (_hostManager == null)
            {
                lock (_syncRoot)
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
                            ExecuteConfig();
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

        public void ExecuteConfig()
        {
             using (OpenRastaConfiguration.Manual)
             {
                 var x = true;
             }
        }
        
    }
}