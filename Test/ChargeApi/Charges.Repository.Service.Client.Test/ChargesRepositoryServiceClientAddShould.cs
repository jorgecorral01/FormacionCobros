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
    public class ChargesRepositoryServiceClientAddShould {
        string identifier;
        IHttpApiClient client;

        [SetUp]
        public void Setup() {            
            identifier = "any identifier";
        }

        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_a_ok_response_with_true_result() {
            ReturnOkFor(false);
            string requestUri = "http://localhost:10001/api/charges/add";
            Charge newCharge = GivenACharge();
            var content = GivenAHttpContent(newCharge, requestUri);
            var chargeRepositoryServiceClient = new ChargeRepositoryServiceApiClient(client);

            ChargeResponse result = await chargeRepositoryServiceClient.AddCharge(newCharge);

            result.Should().BeOfType<Charges.Business.Dtos.ChargeResponseOK>();
            result.alreadyExist.Should().Be(false);
            result.Message.Should().BeNull();
            await client.Received(1).PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>());
        }

        [Test]
        public async Task given_an_identifier_try_delete_charge_return_ok_response() {
            ReturnOkForDelete();
            string requestUri = string.Format("http://localhost:10001/api/charges/charge/{0}", identifier);
            var chargeRepositoryServiceClient = new ChargeRepositoryServiceApiClient(client);

            var result = await chargeRepositoryServiceClient.DeleteCharge(identifier);

            result.Should().Be(true);
            await client.Received(1).DeleteAsync(Arg.Any<string>());
        }

        [Test]
        public async Task given_an_identifier_try_delete_charge_return_not_found_if_charge_dont_exist() {            
            ReturnNotFoundFor();
            string requestUri = string.Format("http://localhost:10001/api/charges/charge/{0}", identifier);
            var chargeRepositoryServiceClient = new ChargeRepositoryServiceApiClient(client);

            var result = await chargeRepositoryServiceClient.DeleteCharge(identifier);

            result.Should().Be(false);
            await client.Received(1).DeleteAsync(Arg.Any<string>());
        }

        [Test]
        public async Task should_return_charge_already_exist_when_try_add_charge_with_exist_identifier() {
            ReturnOkFor(true);
            string requestUri = "http://localhost:10001/api/charges/add";
            Charge newCharge = GivenACharge();
            var content = GivenAHttpContent(newCharge, requestUri);
            var chargeRepositoryServiceClient = new ChargeRepositoryServiceApiClient(client);

            var result = await chargeRepositoryServiceClient.AddCharge(newCharge);
            
            result.Should().BeOfType<Charges.Business.Dtos.ChargeAlreadyExist>();
            await client.Received(1).PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>());
        }

        private void ReturnNotFoundFor() {
            client = GivenAHttpClienMock();
            client.DeleteAsync(Arg.Any<string>()).Returns(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
        private void ReturnOkFor(bool alreadyExist) {
            client = GivenAHttpClienMock();
            string jsonVideo = JsonConvert.SerializeObject(new ChargeResponse() { alreadyExist = alreadyExist }, Formatting.Indented);
            HttpContent content = new StringContent(jsonVideo, Encoding.UTF8, "application/json");
            HttpResponseMessage returnThis = new HttpResponseMessage() {StatusCode = HttpStatusCode.OK, Content = content };
            client.PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>()).Returns(returnThis);
            //client.PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>()).Returns(new HttpResponseMessage(HttpStatusCode.OK));
        }
        private void ReturnOkForDelete() {
            client = GivenAHttpClienMock();
            client.DeleteAsync(Arg.Any<string>()).Returns(new HttpResponseMessage(HttpStatusCode.OK));
        }
        private static Charge GivenACharge() {
            return new Charge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
        }

        private static IHttpApiClient GivenAHttpClienMock() {
            var client = Substitute.For<IHttpApiClient>();
            return client;
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