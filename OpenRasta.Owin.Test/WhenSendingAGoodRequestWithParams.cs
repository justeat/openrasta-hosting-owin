using System.Linq;
using System.Net;
using NUnit.Framework;
using Shouldly;

namespace OpenRasta.Owin.Test
{
    [TestFixture]
    public class WhenSendingAGoodRequestWithParams :TestServerBase
    {
        private const string Url = "http://testserver/Get/WithParams?value=100";

        [Test]
        public async void ResponseIsNotNull()
        {
            var response = await CallGetUrlAsync(Url);
            Assert.IsNotNull(response);
        }

        [Test]
        public async void ResponseStatusCodeIsOk()
        {
            var response = await CallGetUrlAsync(Url);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async void ResponseHasAResult()
        {
            var response = await CallGetUrlAsync(Url);
            var readTask = response.Content.ReadAsStringAsync();
            readTask.Wait();
            Assert.IsNotNull(readTask.Result);
        }

        [Test]
        public async void ResponseContainsCustomHeaders()
        {
            var response = await CallGetUrlAsync(Url);
            response.Headers.First().Value.ShouldContain("Custom Headers added via pipeline");
        }
      
    }
}
