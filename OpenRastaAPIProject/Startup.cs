using OpenRasta.Configuration;
using OpenRasta.Owin;
using Owin;

namespace OpenRastaAPIProject
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            IConfigurationSource configSources = new Config();
            appBuilder.UseOpenRasta(configSources);
        }
    }
}