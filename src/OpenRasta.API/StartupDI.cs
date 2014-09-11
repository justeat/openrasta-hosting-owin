using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.Owin;
using Owin;

namespace OpenRasta.API
{
    public class StartupDI
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            IDependencyResolverAccessor dependencyResolverAccessor = new CustomDependencyResolver();
            IConfigurationSource configSources = new Config();
            appBuilder.UseOpenRasta(configSources,dependencyResolverAccessor);
        }
    }

    public class CustomDependencyResolver : IDependencyResolverAccessor
    {
        public IDependencyResolver Resolver { get { return new InternalDependencyResolver(); }  }
    }
}
