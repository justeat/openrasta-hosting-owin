using StructureMap.Configuration.DSL;

namespace OpenRastaAPIProject
{
    public class APIRegistry : Registry
    {
        public APIRegistry()
        {
            Initialise();
        }

        private void Initialise()
        {
            For<IHeresSomeObject>()
                .Singleton()
                .Use<HeresSomeObject>();
        }
    }
}