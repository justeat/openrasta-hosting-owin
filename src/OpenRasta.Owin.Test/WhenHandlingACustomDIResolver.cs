using System.Linq;
using System.Net;
using NUnit.Framework;
using Shouldly;

namespace OpenRasta.Owin.Test
{
    [TestFixture]
    public class WhenHandlingACustomDIResolver : TestServerBase
    {
        string Url = "http://testserver/Get/withjson";

        [Test]
        public async void ResponseIsNotNull()
        {
            var response = await CallGetUrlDIAsync(Url);
            Assert.IsNotNull(response);
        }
        
        [Test]
        public async void ResponseStatusCodeIsOk()
        {
            var response = await CallGetUrlDIAsync(Url);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public async void ResponseHasAResult()
        {
            var response = await CallGetUrlDIAsync(Url);
            var readTask = response.Content.ReadAsStringAsync();
            readTask.Wait();
            Assert.IsNotNull(readTask.Result);
        }

        [Test]
        public async void ResponseContainsCustomHeaders()
        {
            var response = await CallGetUrlDIAsync(Url);
            response.Headers.First().Value.ShouldContain("Custom Headers added via pipeline");
        }
    }
}
