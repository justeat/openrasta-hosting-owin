using OpenRasta.Configuration;
using OpenRasta.Web;

namespace OpenRastaAPIProject
{
    public class Config : IConfigurationSource
    {
        public void Configure()
        {
            ResourceSpace.Has.ResourcesOfType<object>()
                .AtUri("/").Named("AddServiceableAreas")
                .HandledBy<Handler>();
        }
    }

    public class Handler
    {
        public OperationResult Get()
        {
            return new OperationResult.OK();
        }
    }

    

}
