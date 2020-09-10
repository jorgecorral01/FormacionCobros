using Charge.Activity.Service.Bussines.Dtos;
using Charge.Repository.Service.Repository.Entity.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Charge.Activity.Service.Repository.Test {
    public class ChargeActivityServiceRepositoryShould {
        private ChargesContext chargesContext;
        private IdentifierDto identifier;
        private ChargeActivityServiceRepository chargeActivityServiceRepository;
        [SetUp]
        public void Setup() {
            GivenAContext();
            GivenAActivityRepository();
            GivenAnActivity();
        }
     
        [Test]
        public void when_we_add_new_activity_we_can_rercover() {
            var result = chargeActivityServiceRepository.Add(identifier.identifier);

            VerifyResult(result);
            DeleteActivity();
        }       

        [Test]
        public void when_we_update_activity_we_can_rercover_with_new_data() {                                    
            chargeActivityServiceRepository.Add(identifier.identifier);

            var result = chargeActivityServiceRepository.Update(identifier);

            VerifyResult(result);
            DeleteActivity();
        }
        private void GivenAnActivity() {
            identifier = new IdentifierDto { identifier = "any identifier", AddResult = true };
        }
        private void GivenAContext() {
            var optionsBuilder = new DbContextOptionsBuilder<ChargesContext>()
                                .UseInMemoryDatabase(databaseName: "BDInMemory")
                                .Options;
            chargesContext = new ChargesContext(optionsBuilder);
        }
        private void GivenAActivityRepository() {
            chargeActivityServiceRepository = new ChargeActivityServiceRepository(chargesContext);
        }
        private void VerifyResult(bool result) {
            result.Should().Be(true);
            chargesContext.Activities.Select(item => item.Identifier == identifier.identifier && item.AddResult == identifier.AddResult).ToList().Should().HaveCount(1);
        }

        private void DeleteActivity() {
            chargesContext.Activities.RemoveRange(chargesContext.Activities.Where(item => item.Identifier == identifier.identifier));
            chargesContext.SaveChanges();
        }
    }
}