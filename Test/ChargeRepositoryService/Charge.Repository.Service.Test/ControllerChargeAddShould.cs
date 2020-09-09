using Charge.Repository.Service.Business.Dtos;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Charge.Repository.Service.Controller.Test {
    public class ControllerChargeAddShould {
        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_an_ok_response() {
            var newCharge = new RepositoryCharge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
            HttpClient client = new HttpClient();
            var requestUri = "http://localhost:10001/api/charges/add";
            var content = GivenAHttpContent(newCharge, requestUri);

            var result = await client.PostAsync(requestUri, content);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static HttpContent GivenAHttpContent(RepositoryCharge repositoryCharge, string requestUri) {
            string jsonVideo = JsonConvert.SerializeObject(repositoryCharge, Formatting.Indented);
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

