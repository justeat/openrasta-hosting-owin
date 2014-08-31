using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using OpenRastaAPIProject;
using System.Linq;
using Shouldly;

namespace OpenRasta.Owin.Test
{
    [TestFixture]
    public class WhenSendingAPostRequest : TestServerBase
    {
        string Url = "http://testserver/Post";
        SomeRequest _request = new SomeRequest { Id = 10, Value = "You got post!", Description ="And that post has some descriptions"};


        [Test]
        public async void ResponseIsNotNull()
        {
            var response = await CallPostUrlAsync(Url, _request);
            Assert.IsNotNull(response);
        }

        [Test]
        public async void ResponseStatusCodeIsOK()
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
        }

        [Test]
        public async void ResponseContainsCustomHeaders()
        {
            var response = await CallPostUrlAsync(Url, _request);
            response.Headers.First().Value.ShouldContain("Custom Headers added via pipeline");
        }

    }
}
