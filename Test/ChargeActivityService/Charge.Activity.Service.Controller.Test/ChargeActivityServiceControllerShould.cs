using FluentAssertions;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Charge.Activity.Service.Controller.Test {
    public class ChargeActivityServiceControllerShould {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public async Task given_an_identifier_add_new_activity_charge_we_obtein_an_ok_response() {
            HttpClient client = new HttpClient();
            var requestUri = "http://localhost:10002/api/ChargeActivity/add";
            var content = GivenAHttpContent("any identifier", requestUri);

            var result = await client.PostAsync(requestUri, content);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static HttpContent GivenAHttpContent(string identifier, string requestUri) {            
            HttpContent content = new StringContent(identifier, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content
            };
            return content;
        }
    }
}