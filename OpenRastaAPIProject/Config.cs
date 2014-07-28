using OpenRasta.Configuration;

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

                ResourceSpace.Uses.PipelineContributor<CustomHeadersPipelineContributor>();
            }
        }
    }
}