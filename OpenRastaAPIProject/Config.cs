using OpenRasta.Configuration;
using OpenRasta.Web;

namespace OpenRastaAPIProject
{
    public class Config : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Has.ResourcesOfType<object>()
                    .AtUri("Get")
                    .HandledBy<Handler>();

                ResourceSpace.Has.ResourcesOfType<SomeResponse>()
                    .AtUri("Get/WithJSON")
                    .Named("WithJSON")
                    .HandledBy<Handler>()
                    .TranscodedBy<JustEatJsonCodec>();
            }
        }
    }

    public class Handler
    {
        public OperationResult Get()
        {
            return new OperationResult.OK();
        }

        [HttpOperation(HttpMethod.GET, ForUriName = "WithJSON")]
        public OperationResult GetWithJSON()
        {
            return new OperationResult.OK(new SomeResponse { value = "Some text for you here returned in JSON" });
        }
    }

    public class SomeResponse
    {
        public string value { get; set; }
    }
}