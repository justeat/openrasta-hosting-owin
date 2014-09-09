using System.Linq;
using System.Net;
using NUnit.Framework;
using Shouldly;

namespace OpenRasta.Owin.Test
{
    [TestFixture]
    public class WhenSendingAUnknowResourceRequest : TestServerBase
    {
        private const string Url = "http://testserver/get/unknown";

        [Test]
        public async void ResponseIsNotNull()
        {
            var response = await CallGetUrlAsync(Url);
            Assert.IsNotNull(response);
        }

        [Test]
        public async void ResponseIsABadRequest()
        {
            var response = await CallGetUrlAsync(Url);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public async void ResponseDoesNotContainsCustomHeaders()
        {
            var response = await CallGetUrlAsync(Url);
            response.Headers.Count().ShouldBe(0);
        }

    }
}
