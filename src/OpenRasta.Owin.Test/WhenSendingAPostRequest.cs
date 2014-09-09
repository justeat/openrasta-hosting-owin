using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Linq;
using OpenRasta.API;
using Shouldly;

namespace OpenRasta.Owin.Test
{
    [TestFixture]
    public class WhenSendingAPostRequest : TestServerBase
    {
        private const string Url = "http://testserver/Post";
        readonly SomeRequest _request = new SomeRequest { Id = 10, Value = "You got post!", Description ="And that post has some descriptions"};


        [Test]
        public async void ResponseIsNotNull()
        {
            var response = await CallPostUrlAsync(Url, _request);
            Assert.IsNotNull(response);
        }

        [Test]
        public async void ResponseStatusCodeIsOk()
        {
            var response = await CallPostUrlAsync(Url, _request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async void ResponseHasAResult()
        {
            var response = await CallPostUrlAsync(Url, _request);
            var readTask = response.Content.ReadAsStringAsync();
            readTask.Wait();
            Assert.IsNotNull(readTask.Result);
            var dataResponse = JsonConvert.DeserializeObject<SomeResponse>(readTask.Result);
            Assert.AreEqual(dataResponse.Value ,"You got post!");
            
        }

        [Test]
        public async void ResponseContainsCustomHeaders()
        {
            var response = await CallPostUrlAsync(Url, _request);
            response.Headers.First().Value.ShouldContain("Custom Headers added via pipeline");
        }

    }
}
