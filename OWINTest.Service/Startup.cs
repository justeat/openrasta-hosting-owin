using OpenRasta.Configuration;
using OpenRastaAPIProject;
using Owin;

namespace OpenRasta.Owin.Service
{
    internal class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            IConfigurationSource configSources = new Config();

            appBuilder.UseOpenRasta(configSources);
        }
    }
}