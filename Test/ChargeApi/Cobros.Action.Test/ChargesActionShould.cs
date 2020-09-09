using Charge.Activity.Service.Client;
using Chargues.Repository.Service.Client;
using Charges.Business.Dtos;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Charges.Action.Test {
    public class ChargesActionShould {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_a_true_response() {
            var newCharge = new Charges.Business.Dtos.Charge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
            var clientChargeRepository = Substitute.For<ChargeRepositoryServiceClient>(new object[] { null });
            var clientActivityService = Substitute.For<ChargeActivityServiceClient>();
            clientChargeRepository.AddCharge(newCharge).Returns(true);
            var addChargeAction = new AddChargeAction(clientChargeRepository, clientActivityService);
            var addResult = true;
            clientChargeRepository.AddCharge(newCharge).Returns(addResult);
            clientActivityService.UpdateNotifyCharge(newCharge.identifier, addResult).Returns(true);

            var result = await addChargeAction.Execute(newCharge);

            result.Should().Be(true);
            clientActivityService.Received(1).NotifyNewCharge(newCharge.identifier);
            await clientChargeRepository.Received(1).AddCharge(newCharge);
            clientActivityService.Received(1).UpdateNotifyCharge(newCharge.identifier, addResult);
        }
    }
}