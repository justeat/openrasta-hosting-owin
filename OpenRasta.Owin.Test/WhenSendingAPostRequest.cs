using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenRastaAPIProject;

namespace OpenRasta.Owin.Test
{
    [TestFixture]
    public class WhenSendingAPostRequest
    {
        [Test]
        public async void ResponseIsValid()
        {
            using (var server = TestServer.Create<Startup>())
            {
                using (var client = new HttpClient(server.Handler))
                {
                    var request = new SomeRequest{Id = 10, Value = "You got post!"};
                    var content = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8 ,"application/json");

                   var response = await client.PostAsync("http://testserver/Post", content);

                    //test response exists
                    Assert.IsNotNull(response);
                    //test statuscode
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.UnsupportedMediaType);
                    //test result
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();
                    Assert.IsNotNull(readTask.Result);
                }
            }
            
        }
    }
}
