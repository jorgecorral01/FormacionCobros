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
            Business.Dtos.Charge newCharge = GivenACharge();
            var addResult = true;
            ChargeRepositoryServiceApiClient clientChargeRepository = GivenAMockRepositoryServiceClient(newCharge, addResult);
            ActivityDto activityDto = GivenAnActivity(newCharge.identifier, addResult);
            ChargeActivityServiceApiClient clientActivityService = GivenAMockActivityServiceClient(activityDto);
            var addChargeAction = new AddChargeAction(clientChargeRepository, clientActivityService);

            var result = await addChargeAction.Execute(newCharge);

            result.Should().Be(true);
            await clientActivityService.Received(1).NotifyNewCharge(Arg.Is<ActivityDto>(item => item.identifier == activityDto.identifier));
            await clientChargeRepository.Received(1).AddCharge(newCharge);
            await clientActivityService.Received(1).UpdateNotifyCharge(Arg.Is<ActivityDto>(item => item.identifier == activityDto.identifier && item.AddResult == activityDto.AddResult));
        }

        [Test]
        public async Task given_an_idenfier_for_delete_charge_we_obtein_a_true_response() {
            var identifier = "any identifier";
            ChargeRepositoryServiceApiClient clientChargeRepository = GivenARepositoryMock();
            clientChargeRepository.DeleteCharge(identifier).Returns(true);
            var deleteChargeAction = new DeleteChargeAction(clientChargeRepository);

            var result = await deleteChargeAction.Execute(identifier);

            result.Should().Be(true);
            await clientChargeRepository.Received(1).DeleteCharge(identifier);
        }

        [Test]
        public async Task given_an_idenfier_for_delete_charge_and_dont_exist_we_obtein_a_false_response() {
            var identifier = "any identifier";
            ChargeRepositoryServiceApiClient clientChargeRepository = GivenARepositoryMock();
            clientChargeRepository.DeleteCharge(identifier).Returns(false);
            var deleteChargeAction = new DeleteChargeAction(clientChargeRepository);

            var result = await deleteChargeAction.Execute(identifier);

            result.Should().Be(false);
            await clientChargeRepository.Received(1).DeleteCharge(identifier);
        }

        private static ChargeRepositoryServiceApiClient GivenARepositoryMock() {
            return Substitute.For<ChargeRepositoryServiceApiClient>(new object[] { null });
        }

        private static ActivityDto GivenAnActivity(string identifier, bool addResult) {
            return new ActivityDto { identifier = identifier, AddResult = addResult };
        }

        private static ChargeActivityServiceApiClient GivenAMockActivityServiceClient(ActivityDto identifierDto) {
            var clientActivityService = Substitute.For<ChargeActivityServiceApiClient>(new object[] { null });
            clientActivityService.UpdateNotifyCharge(Arg.Is<ActivityDto>(item => item.identifier == identifierDto.identifier && item.AddResult == identifierDto.AddResult)).Returns(true);
            return clientActivityService;
        }

        private static ChargeRepositoryServiceApiClient GivenAMockRepositoryServiceClient(Business.Dtos.Charge newCharge, bool addResult) {
            var clientChargeRepository = Substitute.For<ChargeRepositoryServiceApiClient>(new object[] { null });
            clientChargeRepository.AddCharge(newCharge).Returns(addResult);
            return clientChargeRepository;
        }

        private static Business.Dtos.Charge GivenACharge() {
            return new Charges.Business.Dtos.Charge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
        }
    }
}