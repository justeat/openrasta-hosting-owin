using OpenRasta.Configuration;

namespace OpenRasta.API
{
    public class Config : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Has.ResourcesOfType<object>()
                                 .WithoutUri
                                 .TranscodedBy<JsonCodec>();

                ResourceSpace.Has.ResourcesOfType<object>()
                    .AtUri("Get")
                    .HandledBy<Handler>();

                ResourceSpace.Has.ResourcesOfType<SomeResponse>()
                .AtUri("Post").Named("post")
                .HandledBy<Handler>()
                .TranscodedBy<JsonCodec>();

                ResourceSpace.Has.ResourcesOfType<SomeResponse>()
                    .AtUri("Get/WithJSON")
                    .Named("WithJSON")
                    .HandledBy<Handler>()
                    .TranscodedBy<JsonCodec>();

                ResourceSpace.Has.ResourcesOfType<SomeResponse>()
               .AtUri("Get/WithParams?value={value}")
               .Named("WithParams")
               .HandledBy<Handler>()
               .TranscodedBy<JsonCodec>();

                ResourceSpace.Has.ResourcesOfType<SomeResponse>()
                    .AtUri("Get/Error")
                    .Named("Error")
                    .HandledBy<Handler>()
                    .TranscodedBy<JsonCodec>();


                ResourceSpace.Uses.PipelineContributor<CustomHeadersPipelineContributor>();
            }
        }
    }
}