using Charges.Business.Dtos;
using Chargues.Repository.Service.Client;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Cobros.Action.Test {
    public class ChargesActionShould {
        [SetUp]
        public void Setup() {
        }

        [Test]        
        public async Task given_data_for_add_new_charge_we_obtein_a_true_response() {
            var newCharge = new Charge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
            var clientChargeRepository = Substitute.For<ChargeRepositoryServiceClient>(new object[] { null }); 
            clientChargeRepository.AddCharge(newCharge).Returns(true);
            var addChargeAction = new AddChargeAction(clientChargeRepository);
            

            var result = await  addChargeAction.Execute(newCharge);

            result.Should().Be(true);
            await clientChargeRepository.Received(1).AddCharge(newCharge);
        }
    }
}