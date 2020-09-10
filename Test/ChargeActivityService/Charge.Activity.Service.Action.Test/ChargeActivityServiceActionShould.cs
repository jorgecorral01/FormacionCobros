using Charge.Activity.Service.Bussines.Dtos;
using Charge.Activity.Service.Repository;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Charge.Activity.Service.Action.Test {
    public partial class ChargeActivityServiceActionShould {
        private static IdentifierDto identifierDto;
        [SetUp]
        public void Setup() {
            identifierDto = GivenAnIdentifier();
        }

        [Test]
        public void when_we_update_activity_with_a_exist_identifier_we_obtein_true_result() {
            ChargeActivityServiceRepository chargeActivityServiceRepository = GivenArepositoryMockFor();
            var action = new UpdateActivityAction(chargeActivityServiceRepository);

            var result = action.Execute(identifierDto);

            result.Should().Be(true);
            chargeActivityServiceRepository.Received(1).Update(identifierDto);
        }

        private static ChargeActivityServiceRepository GivenArepositoryMockFor() {
            var chargeActivityServiceRepository = Substitute.For<ChargeActivityServiceRepository>(new object[] { null });
            chargeActivityServiceRepository.Update(identifierDto).Returns(true);
            return chargeActivityServiceRepository;
        }

        private static IdentifierDto GivenAnIdentifier() {
            return new IdentifierDto { identifier = "any identifier", AddResult = true };
        }
    }
}