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
            return new OperationResult.OK(new SomeResponse { value = "Some text for you here returned in JSON" });
        }

        [HttpOperation(HttpMethod.GET, ForUriName = "Perf")]
        public OperationResult GetPerf()
        {//todo
            return new OperationResult.OK(new SomeResponse { value = "need to do some database calls a like for like of production openrasta" });
        }
    }
}