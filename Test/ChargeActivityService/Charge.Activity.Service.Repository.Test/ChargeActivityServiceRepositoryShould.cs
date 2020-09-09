using Charge.Repository.Service.Repository.Entity.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Charge.Activity.Service.Repository.Test {
    public class ChargeActivityServiceRepositoryShould {
        private ChargesContext chargesContext;
        [SetUp]
        public void Setup() {
            var optionsBuilder = new DbContextOptionsBuilder<ChargesContext>()
                    .UseInMemoryDatabase(databaseName: "BDInMemory")
                    .Options;

            chargesContext = new ChargesContext(optionsBuilder);
        }

        [Test]
        public void when_we_add_new_activity_we_can_rercover() {
            var identifier = "any identifier";
            var chargeActivityServiceRepository = new ChargeActivityServiceRepository(chargesContext);

            var result = chargeActivityServiceRepository.Add(identifier);

            result.Should().Be(true);
            chargesContext.Activities.Select(item => item.Identifier == identifier).ToList().Should().HaveCount(1);

        }

        private class ChargeActivityServiceRepository {
            public ChargeActivityServiceRepository() {
            }

            public ChargeActivityServiceRepository(ChargesContext chargesContext) {
                ChargesContext = chargesContext;
            }

            public ChargesContext ChargesContext { get; }

            internal bool Add(string identifier) {
                ChargesContext.Activities.Add(new Activities { Identifier = identifier });
                ChargesContext.SaveChanges();
                return true;
            }
        }
    }
}