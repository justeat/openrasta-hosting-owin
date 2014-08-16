using OpenRasta.Web;

namespace OpenRastaAPIProject
{
    public class Handler
    {
        public OperationResult Get()
        {
            return new OperationResult.OK();
        }

        [HttpOperation(HttpMethod.GET, ForUriName = "WithJSON")]
        public OperationResult GetWithJSON()
        {
            return new OperationResult.OK(new SomeResponse {value = "Some text for you here returned in JSON"});
        }

        [HttpOperation(HttpMethod.GET, ForUriName = "Error")]
        public OperationResult ShowError()
        {
            return new OperationResult.BadRequest(){ ResponseResource = new SomeResponse { value = "Some custom error response" }};
        }
    }
}