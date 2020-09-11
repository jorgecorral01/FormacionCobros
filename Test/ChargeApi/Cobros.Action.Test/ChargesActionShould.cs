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
            ChargeRepositoryServiceClient clientChargeRepository = GivenAMockRepositoryServiceClient(newCharge, addResult);
            ActivityDto activityDto = GivenAnActivity(newCharge.identifier, addResult);
            ChargeActivityServiceClient clientActivityService = GivenAMockActivityServiceClient(activityDto);
            var addChargeAction = new AddChargeAction(clientChargeRepository, clientActivityService);

            var result = await addChargeAction.Execute(newCharge);

            result.Should().Be(true);
            await clientActivityService.Received(1).NotifyNewCharge(Arg.Is<ActivityDto>(item => item.identifier == activityDto.identifier));
            await clientChargeRepository.Received(1).AddCharge(newCharge);
            await clientActivityService.Received(1).UpdateNotifyCharge(Arg.Is<ActivityDto>(item => item.identifier == activityDto.identifier && item.AddResult == activityDto.AddResult));
        }

        private static ActivityDto GivenAnActivity(string identifier, bool addResult) {
            return new ActivityDto { identifier = identifier, AddResult = addResult };
        }

        private static ChargeActivityServiceClient GivenAMockActivityServiceClient(ActivityDto identifierDto) {
            var clientActivityService = Substitute.For<ChargeActivityServiceClient>();
            clientActivityService.UpdateNotifyCharge(Arg.Is<ActivityDto>(item => item.identifier == identifierDto.identifier && item.AddResult == identifierDto.AddResult)).Returns(true);
            return clientActivityService;
        }

        private static ChargeRepositoryServiceClient GivenAMockRepositoryServiceClient(Business.Dtos.Charge newCharge, bool addResult) {
            var clientChargeRepository = Substitute.For<ChargeRepositoryServiceClient>(new object[] { null });
            clientChargeRepository.AddCharge(newCharge).Returns(addResult);
            return clientChargeRepository;
        }

        private static Business.Dtos.Charge GivenACharge() {
            return new Charges.Business.Dtos.Charge { Description = "Nuevo cobro", Amount = 1000, identifier = "anyIdentifier" };
        }
    }
}