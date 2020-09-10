using Charge.Activity.Service.Bussines.Dtos;
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
            chargesContext.Activities.RemoveRange(chargesContext.Activities.Where(item => item.Identifier == identifier));
            chargesContext.SaveChanges();
        }

        [Test]
        public void when_we_update_activity_we_can_rercover_with_new_data() {            
            var identifier = new IdentifierDto { identifier = "any identifier", AddResult = true };
            var chargeActivityServiceRepository = new ChargeActivityServiceRepository(chargesContext);
            chargeActivityServiceRepository.Add(identifier.identifier);

            var result = chargeActivityServiceRepository.UpdateActivity(identifier);

            result.Should().Be(true);
            chargesContext.Activities.Select(item => item.Identifier == identifier.identifier && item.AddResult == identifier.AddResult).ToList().Should().HaveCount(1);
            chargesContext.Activities.RemoveRange(chargesContext.Activities.Where(item => item.Identifier == identifier.identifier));
            chargesContext.SaveChanges();
        }
    }
}