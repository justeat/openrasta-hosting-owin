OpenRasta Owin Hosting
======================

OWIN host for OpenRasta, allows OpenRasta to be combined with other MiddleWare opening the possibilities.

Getting started
======================

1. Remove OpenRasta AspNet Hosting (or not you can matain both)
2. Add OpenRasta.Owin
3. Create Startup.cs
4. Add the following code 

````c#

public void Configuration(IAppBuilder appBuilder)
{
   IConfigurationSource configSources = new OpenRastaConfig();
   appBuilder.UseOpenRasta(configSources);
}

````
Now you can start creating your hosting platform using the Microsoft.Owin hosting nugets (console, service, IIS). See code link below for an example https://github.com/justeat/openrasta-hosting-owin/blob/master/src/OpenRasta.Owin.Console/Program.cs


Using a custom dependency resolver

````c#

public void Configuration(IAppBuilder appBuilder)
{
   IConfigurationSource configSources = new OpenRastaConfig();
   IDependencyResolverAccessor customDependencyResolver = new CustomDependencyResolver()
   appBuilder.UseOpenRasta(configSources, customDependencyResolver);
}

````
