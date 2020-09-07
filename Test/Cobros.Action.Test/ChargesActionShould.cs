using Cobros.Business.Dtos;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Cobros.Action.Test {
    public class ChargesActionShould {
        [SetUp]
        public void Setup() {
        }

        [Test]        
        public void given_data_for_add_new_charge_we_obtein_a_true_response() {
            var newCharge = new Charge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
            var addChargeAction = new AddChargeAction();

            var result = addChargeAction.Execute(newCharge);

            result.Should().Be(true);
        }
    }
}