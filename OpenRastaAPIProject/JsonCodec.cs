using System;
using System.Text;
using Newtonsoft.Json;
using OpenRasta.Codecs;
using OpenRasta.TypeSystem;
using OpenRasta.Web;

namespace OpenRastaAPIProject
{
    [MediaType("application/json;q=0.5;charset=utf-8", "json")]
    // ReSharper disable ClassNeverInstantiated.Global
    public class JsonCodec : IMediaTypeWriter, IMediaTypeReader
        // ReSharper restore ClassNeverInstantiated.Global
    {
        public object ReadFrom(IHttpEntity request, IType destinationType, string destinationName)
        {
            if (destinationType.StaticType == null)
            {
                throw new InvalidOperationException();
            }

            var streamBytes = new byte[request.Stream.Length];
            var bytesRead = request.Stream.Read(streamBytes, 0, streamBytes.Length);
            var postData = Encoding.UTF8.GetString(streamBytes, 0, bytesRead);

            return JsonConvert.DeserializeObject(postData, destinationType.StaticType);
        }

        public object Configuration { get; set; }

        public void WriteTo(object entity, IHttpEntity response, string[] codecParameters)
        {
            if (entity == null)
            {
                return;
            }

            var output = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity));
            response.Stream.Write(output, 0, output.Length);


            response.ContentType = new MediaType("application/json") {CharSet = "utf-8"};
        }
    }
}