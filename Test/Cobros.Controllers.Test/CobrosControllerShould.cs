using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cobros.Business.Dtos;

namespace Cobros.Controllers.Test {
    public class CobrosControllerShould {
        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_a_ok_response() {
            var newCharge = new Charge {Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
            HttpClient client = new HttpClient();
            var requestUri = "http://localhost:10000/api/charges";
            var content = GivenAHttpContent(newCharge, requestUri);

            var result = await client.PostAsync(requestUri, content);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static HttpContent GivenAHttpContent(Charge charge2, string requestUri) {
            string jsonVideo = JsonConvert.SerializeObject(charge2, Formatting.Indented);
            HttpContent content = new StringContent(jsonVideo, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content
            };
            return content;
        }
    }
}
