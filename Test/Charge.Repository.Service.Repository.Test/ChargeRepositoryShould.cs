using Charge.Repository.Service.Business.Dtos;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Charge.Repository.Service.Repository.Test {
    public class ChargeRepositoryShould {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public async Task given_data_for_add_new_charge_we_can_recover_it() {
            var newCharge = new RepositoryCharge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
            var chargeRepository = new ChargeRepository();

            var result = await chargeRepository.Add(newCharge);

            result.Should().Be(true);
        }
    }
}