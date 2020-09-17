using Charges.Action;
using Charges.Business.Dtos;
using Charges.Controllers.Test.mocks;
using Cobros.API.Controllers;
using Cobros.API.Factories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Controllers.Test {
    [TestFixture]
    public class ChargesControllerShould {
        Business.Dtos.Charge newCharge;
        HttpClient client;

        [SetUp]
        public void Setup() {
            newCharge = GivenANewCharge();
            client = TestFixture.HttpClient;
        }

        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_an_ok_response() {
            var requestUri = "http://localhost:10000/api/charges";
            var content = GivenAHttpContent(newCharge, requestUri);
            AddChargeAction action = GivenAnAddChargeActionMock(true);

            var result = await client.PostAsync(requestUri, content);
            
            await verifyResult(newCharge, action, result);
        }

        [Test]
        public async Task given_data_for_add_new_charge_and_have_any_problem_we_obtein_bad_request() {         
            var requestUri = "http://localhost:10000/api/charges";
            var content = GivenAHttpContent(newCharge, requestUri);
            AddChargeAction action = GivenAnAddChargeActionMock(false);

            var result = await client.PostAsync(requestUri, content);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);            //TODO add text to badrequest 
            await action.Received(1).Execute(Arg.Is<Business.Dtos.Charge>(item => item.identifier == newCharge.identifier && item.Amount == newCharge.Amount && item.Description == newCharge.Description));
        }

        private static async Task verifyResult(Business.Dtos.Charge newCharge, AddChargeAction action, HttpResponseMessage result) {
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            await action.Received(1).Execute(Arg.Is<Business.Dtos.Charge>(item => item.identifier == newCharge.identifier && item.Amount == newCharge.Amount && item.Description == newCharge.Description));
        }

        private AddChargeAction GivenAnAddChargeActionMock(bool actionResult) {
            AddChargeAction action = Substitute.For<AddChargeAction>(null, null);
            action.Execute(Arg.Any<Business.Dtos.Charge>()).Returns(actionResult);
            ActionsFactoryMock.CreateAddChargeAction(action);
            return action;
        }

        private static Business.Dtos.Charge GivenANewCharge() {
            return new Business.Dtos.Charge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
        }

        private static HttpContent GivenAHttpContent(Business.Dtos.Charge charge2, string requestUri) {
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
