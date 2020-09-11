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
            var identifierDto = new IdentifierDto { identifier = newCharge.identifier, AddResult = addResult };
            clientActivityService.UpdateNotifyCharge(Arg.Is<IdentifierDto>(item => item.identifier == identifierDto.identifier && item.AddResult == identifierDto.AddResult)).Returns(true);
            
            var result = await addChargeAction.Execute(newCharge);

            result.Should().Be(true);
            await clientActivityService.Received(1).NotifyNewCharge(Arg.Is<IdentifierDto>(item => item.identifier == identifierDto.identifier));            
            await clientChargeRepository.Received(1).AddCharge(newCharge);
            await clientActivityService.Received(1).UpdateNotifyCharge(Arg.Is<IdentifierDto>(item => item.identifier == identifierDto.identifier && item.AddResult == identifierDto.AddResult));
        }
    }
}