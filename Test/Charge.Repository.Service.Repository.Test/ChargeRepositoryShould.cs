using Charge.Repository.Service.Business.Dtos;
using Charge.Repository.Service.Repository.Entity.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Charge.Repository.Service.Repository.Test {
    public class ChargeRepositoryShould {
        private ChargesContext chargesContext;

        [SetUp]
        public void Setup() {
            var optionsBuilder = new DbContextOptionsBuilder<ChargesContext>()
                    .UseInMemoryDatabase(databaseName: "BDInMemory")
                    .Options;

            chargesContext = new ChargesContext(optionsBuilder);
        }

        [Test]
        public async Task given_data_for_add_new_charge_we_can_recover_it() {
            var newCharge = new RepositoryCharge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
            var chargeRepository = new ChargeRepositoryEntity(chargesContext);

            var result = await chargeRepository.Add(newCharge);

            result.Should().Be(true);
            chargesContext.Charges.Select(item => item.Concept == newCharge.Description).ToList().Should().HaveCount(1);
        }
    }
}