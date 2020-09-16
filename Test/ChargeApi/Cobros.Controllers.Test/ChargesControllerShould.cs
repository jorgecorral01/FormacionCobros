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
        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_an_ok_response_with_startup() {
            Business.Dtos.Charge newCharge = GivenANewCharge();
            HttpClient client = TestFixture.HttpClient;
            var requestUri = "http://localhost:10000/api/charges";
            var content = GivenAHttpContent(newCharge, requestUri);
            AddChargeAction action = Substitute.For<AddChargeAction>(null, null);
            ActionsFactoryMock.CreateAddChargeAction(action);
            action.Execute(Arg.Any<Business.Dtos.Charge>()).Returns(true);

            var result = await client.PostAsync(requestUri, content);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            await action.Received(1).Execute(Arg.Is<Business.Dtos.Charge>(item => item.identifier == newCharge.identifier && item.Amount == newCharge.Amount && item.Description == newCharge.Description));
        }
        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_an_ok_response() {
            Business.Dtos.Charge newCharge = GivenANewCharge();
            HttpClient client = new HttpClient();
            var requestUri = "http://localhost:10000/api/charges";
            var content = GivenAHttpContent(newCharge, requestUri);

            var result = await client.PostAsync(requestUri, content);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }



        [Test]
        public async Task given_a_new_charge_we_obtein_an_ok_response() {
            var actionFactory = Substitute.For<ActionFactory>();
            var controller = new ChargesController(actionFactory);
            Business.Dtos.Charge charge = GivenANewCharge();
            AddChargeAction addChargeAction = Substitute.For<AddChargeAction>(null, null);
            addChargeAction.Execute(charge).Returns(true);
            actionFactory.CreateAddChargeAction().Returns(addChargeAction);

            var result = await controller.Post(charge);

            result.Should().BeOfType<OkResult>();
            actionFactory.Received(1).CreateAddChargeAction();
            await addChargeAction.Received(1).Execute(charge);
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
