using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Charge.Activity.Service.Client.Test {
    public class ChargesActivityServiceClientShould {
        [SetUp]
        public void Setup() {
        }

        [Ignore("TODO client.PostAsync i can do return reponse Ok")]
        [Test]
        //async Task
        public void given_data_for_add_new_charge_we_obtein_a_ok_response_with_true_result() {
            HttpClient client = Substitute.For<HttpClient>();
            string requestUri = "http://localhost:10002/api/chargeActivity/add";
            var identifier = "anyIdentifier";
            var addResult = true;
            var identifierDto = new IdentifierDto { identifier = identifier, Result = addResult };
            var content = GivenAHttpContent(identifierDto, requestUri);
            client.PostAsync(requestUri, content).Returns(new HttpResponseMessage(HttpStatusCode.OK));
            var chargeActivityServiceClient = new ChargeActivityServiceClient(client);

            var result = chargeActivityServiceClient.NotifyNewCharge(identifierDto);

            result.Should().Be(true);
            client.Received(1).PostAsync(requestUri, content);
        }

        [Ignore("TODO client.PostAsync i can do return reponse Ok")]
        [Test]
        //async Task
        public void when_we_go_to_update_activity_we_obtein_a_ok_response_with_true_result() {
            HttpClient client = Substitute.For<HttpClient>();
            string requestUri = "http://localhost:10002/api/chargeActivity/update";
            var identifier = "anyIdentifier";
            var addResult = true;
            var identifierDto = new IdentifierDto { identifier = identifier, Result = addResult };
            var content = GivenAHttpContent(identifierDto, requestUri);
            client.PostAsync(requestUri, content).Returns(new HttpResponseMessage(HttpStatusCode.OK));
            var chargeActivityServiceClient = new ChargeActivityServiceClient(client);
            
            var result = chargeActivityServiceClient.UpdateNotifyCharge(identifierDto);

            result.Should().Be(true);
            client.Received(1).PostAsync(requestUri, content);
        }

        private static HttpContent GivenAHttpContent(IdentifierDto identifierDto, string requestUri) {
            string json = JsonConvert.SerializeObject(identifierDto, Formatting.Indented);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content
            };
            return content;
        }
    }
}