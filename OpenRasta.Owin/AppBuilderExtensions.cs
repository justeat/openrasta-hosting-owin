using OpenRasta.Configuration;
using Owin;

namespace OpenRasta.Owin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseOpenRasta(this IAppBuilder builder, IConfigurationSource configurationSource)
        {
            return builder.Use(typeof(OpenRastaMiddleware), configurationSource);
        }
    }
}