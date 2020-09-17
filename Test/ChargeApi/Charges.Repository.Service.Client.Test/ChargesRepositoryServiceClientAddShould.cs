using Charges.Business.Dtos;
using Chargues.Repository.Service.Client;
using FluentAssertions;
using HttpApiClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Repository.Service.Client.Test {
    public class ChargesRepositoryServiceClientAddShould{        
        [SetUp]
        public void Setup() {
        }
               
        
        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_a_ok_response_with_true_result() {
            IHttpApiClient client = Substitute.For<IHttpApiClient>();
            string requestUri = "http://localhost:10001/api/charges/add";
            var newCharge = new Charge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
            var content = GivenAHttpContent(newCharge, requestUri);
            client.PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>()).Returns(new HttpResponseMessage(HttpStatusCode.OK));
            var chargeRepositoryServiceClient = new ChargeRepositoryServiceApiClient(client);

            var result = await chargeRepositoryServiceClient.AddCharge(newCharge);

            result.Should().Be(true);            
            await client.Received(1).PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>());
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