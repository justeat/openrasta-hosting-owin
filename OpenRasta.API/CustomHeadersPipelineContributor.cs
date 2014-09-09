using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace OpenRasta.API
{
    public class CustomHeadersPipelineContributor : IPipelineContributor
    {

        public void Initialize(IPipeline pipelineRunner)
        {
            pipelineRunner.Notify(ApplyCustomHeader).After<KnownStages.IOperationExecution>();
        }

        public PipelineContinuation ApplyCustomHeader(ICommunicationContext context)
        {
            
            context.Response.Headers.Add("Custom-Header", "Custom Headers added via pipeline");
            return PipelineContinuation.Continue;
        }
    }
}