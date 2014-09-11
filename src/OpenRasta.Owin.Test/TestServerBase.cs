using System.Text;
using System.Net.Http;
using Microsoft.Owin.Testing;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenRasta.API;

namespace OpenRasta.Owin.Test
{
    public class TestServerBase
    {
        protected static async Task<HttpResponseMessage> CallGetUrlAsync(string url)
        {
            var server = TestServer.Create<Startup>();

            var client = new HttpClient(server.Handler);
            
            return await client.GetAsync(url);
        }

        protected static async Task<HttpResponseMessage> CallGetUrlDIAsync(string url)
        {
            var server = TestServer.Create<StartupDI>();

            var client = new HttpClient(server.Handler);

            return await client.GetAsync(url);
        }


        protected static async Task<HttpResponseMessage> CallPostUrlAsync(string url, object data)
        {
            var server = TestServer.Create<Startup>();

            var client = new HttpClient(server.Handler);
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
  
            return await client.PostAsync(url, content);
        }
    }
}
