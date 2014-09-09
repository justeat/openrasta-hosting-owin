using OpenRasta.Web;

namespace OpenRasta.API
{
    public class Handler
    {
        public OperationResult Get()
        {
            return new OperationResult.OK();
        }

        [HttpOperation(HttpMethod.GET, ForUriName = "WithParams")]
        public OperationResult GetWithParams(int value)
        {
            SomeResponse response = null;
            if (value == 100)
            {
                response = new SomeResponse
                {
                    Value = "Some text for you here returned in JSON"
                };
            }
            if (response == null)
            {
                return new OperationResult.NotFound();
            }
            return new OperationResult.OK(response);
        }

        [HttpOperation(HttpMethod.GET, ForUriName = "WithJSON")]
        public OperationResult GetWithJSON()
        {
            return new OperationResult.OK(new SomeResponse {Value = "Some text for you here returned in JSON"});
        }

        [HttpOperation(HttpMethod.GET, ForUriName = "Error")]
        public OperationResult ShowError()
        {
            return new OperationResult.BadRequest(){ ResponseResource = new SomeResponse { Value = "Some custom error response" }};
        }
        [HttpOperation(HttpMethod.POST, ForUriName = "post")]
        public OperationResult Post(SomeRequest request)
        {
            return new OperationResult.OK(){ ResponseResource = new SomeResponse { Value = request.Value } };
        }
    }
}