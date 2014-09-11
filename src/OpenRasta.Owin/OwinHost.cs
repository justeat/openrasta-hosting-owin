using System;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.Diagnostics;
using OpenRasta.Hosting;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace OpenRasta.Owin
{
    public class OwinHost : IHost
    {

        public OwinHost(IConfigurationSource configuration)
        {
            ConfigurationSource = configuration;
        }

        public OwinHost(IConfigurationSource configuration,IDependencyResolverAccessor resolverAccesor)
        {
            ConfigurationSource = configuration;
            ResolverAccessor = resolverAccesor;
        }

        public IConfigurationSource ConfigurationSource { get; set; }

        public event EventHandler<IncomingRequestProcessedEventArgs> IncomingRequestProcessed;

        public bool ConfigureRootDependencies(IDependencyResolver resolver)
        {
            resolver.AddDependency<IContextStore, OwinContextStore>(DependencyLifetime.Singleton);
            resolver.AddDependency<ICommunicationContext, OwinCommunicationContext>(DependencyLifetime.PerRequest);
            resolver.AddDependency<ILogger<OwinLogSource>, TraceSourceLogger<OwinLogSource>>(
                DependencyLifetime.Transient);
            return true;
        }

        public bool ConfigureLeafDependencies(IDependencyResolver resolver)
        {
            if (ConfigurationSource != null)
                resolver.AddDependencyInstance<IConfigurationSource>(ConfigurationSource);
            return true;
        }

        public string ApplicationVirtualPath { get; private set; }
        public IDependencyResolverAccessor ResolverAccessor { get; private set; }
        public event EventHandler Start;
        public event EventHandler Stop;
        public event EventHandler<IncomingRequestReceivedEventArgs> IncomingRequestReceived;

        protected internal virtual void RaiseIncomingRequestReceived(ICommunicationContext context)
        {
            IncomingRequestReceived.Raise(this, new IncomingRequestReceivedEventArgs(context));
        }

        protected internal void RaiseIncomingRequestProcessed(ICommunicationContext context)
        {
            IncomingRequestProcessed.Raise(this, new IncomingRequestProcessedEventArgs(context));
        }

        protected internal virtual void RaiseStart()
        {
            Start.Raise(this);
        }
    }

    internal class ActionOnDispose : IDisposable
    {
        private readonly Action _onDispose;

        private bool _disposed;

        public ActionOnDispose(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _onDispose();
            }
        }
    }
}