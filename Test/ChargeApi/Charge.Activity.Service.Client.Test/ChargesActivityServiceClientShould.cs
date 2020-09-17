using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Charge.Activity.Service.Client.Test {
    public class ChargesActivityServiceClientShould {
        HttpApiClient client;
        ActivityDto identifierDto;
        readonly string server = "http://localhost:10002";
        [SetUp]
        public void Setup() {
            client = GivenAhttpClientMock();
            identifierDto = GivenAnActivityDto();
        }

        [Test]        
        public async Task given_data_for_add_new_charge_we_obtein_a_ok_response_with_true_result() {           
            string requestUri = "/api/chargeActivity/add";                       
            var content = GivenAHttpContent(identifierDto, server + requestUri);
            var chargeActivityServiceClient = new ChargeActivityServiceApiClient(client);

            var result = await chargeActivityServiceClient.NotifyNewCharge(identifierDto);

            result.Should().Be(true);
            await client.Received(1).PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>());
        }
        
        [Test]
        public async Task when_we_go_to_update_activity_we_obtein_a_ok_response_with_true_result() {
            string requestUri = "/api/chargeActivity/update";                 
            var content = GivenAHttpContent(identifierDto, server + requestUri);            
            var chargeActivityServiceClient = new ChargeActivityServiceApiClient(client);
            
            var result = await chargeActivityServiceClient.UpdateNotifyCharge(identifierDto);

            result.Should().Be(true);
            await client.Received(1).PutAsync(Arg.Any<string>(), Arg.Any<HttpContent>());
        }
        private static HttpContent GivenAHttpContent(ActivityDto identifierDto, string requestUri) {
            string json = JsonConvert.SerializeObject(identifierDto, Formatting.Indented);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content
            };
            return content;
        }
        private static ActivityDto GivenAnActivityDto() {
            var identifier = "anyIdentifier";
            var addResult = true;
            var identifierDto = new ActivityDto { identifier = identifier, AddResult = addResult };
            return identifierDto;
        }
        private static HttpApiClient GivenAhttpClientMock() {
            HttpApiClient client = Substitute.For<HttpApiClient>();
            valuesReturnForPost(client);
            valuesReturnForPut(client);
            return client;
        }

        private static void valuesReturnForPut(HttpApiClient client) {
            client.PutAsync(Arg.Any<string>(), Arg.Any<HttpContent>()).Returns(new HttpResponseMessage(HttpStatusCode.OK));
        }

        private static void valuesReturnForPost(HttpApiClient client) {
            client.PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>()).Returns(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}