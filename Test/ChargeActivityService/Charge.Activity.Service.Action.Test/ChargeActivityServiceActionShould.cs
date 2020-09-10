using Charge.Activity.Service.Bussines.Dtos;
using Charge.Activity.Service.Repository;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Charge.Activity.Service.Action.Test {
    public partial class ChargeActivityServiceActionShould {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void when_we_update_activity_with_a_exist_identifier_we_obtein_true_result() {
            var identifierDto = new IdentifierDto { identifier = "any identifier", AddResult = "true" } ;
            var chargeActivityServiceRepository = Substitute.For<ChargeActivityServiceRepository>(new object[] { null });
            chargeActivityServiceRepository.UpdateActivity(identifierDto).Returns(true);
            var action = new UpdateActivityAction(chargeActivityServiceRepository);

            var result = action.Execute(identifierDto);

            result.Should().Be(true);
            chargeActivityServiceRepository.Received(1).UpdateActivity(identifierDto);
        }
    }
}