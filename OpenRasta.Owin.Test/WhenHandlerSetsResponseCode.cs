using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using OpenRastaAPIProject;
using Shouldly;

namespace OpenRasta.Owin.Test
{
    [TestFixture]
    public class WhenHandlerSetsResponseCode
    {
        [Test]
        public async void ResponseIsValid()
        {
            using (var server = TestServer.Create<Startup>())
            {
                using (var client = new HttpClient(server.Handler))
                {
                    var response = await client.GetAsync("http://testserver/Get/Error");

                    //test response exists
                    Assert.IsNotNull(response);
                    //test statuscode
                    Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
                    //test result
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();
                    Assert.IsNotNull(readTask.Result);
                    //test custom header added headers
                    response.Headers.First().Value.ShouldContain("Custom Headers added via pipeline");
                }
            }
            
        }
    }
}
