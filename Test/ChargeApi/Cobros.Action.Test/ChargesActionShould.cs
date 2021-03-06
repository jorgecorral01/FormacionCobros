using Charge.Activity.Service.Client;
using Chargues.Repository.Service.Client;
using Charges.Business.Dtos;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using NSubstitute.Extensions;

namespace Charges.Action.Test {
    public class ChargesActionShould {
        string identifier;
        ChargeRepositoryServiceApiClient clientChargeRepository;
        [SetUp]
        public void Setup() {
            clientChargeRepository = GivenARepositoryApiClientMock();
            identifier = "any identifier";
        }

        [Test]
        public async Task given_data_for_add_new_charge_we_obtein_a_true_response() {
            Business.Dtos.Charge newCharge = GivenACharge();
            var addResult = true;
            clientChargeRepository.AddCharge(newCharge).Returns(new ChargeResponseOK());
             ActivityDto activityDto = GivenAnActivity(newCharge.identifier, addResult);
            ChargeActivityServiceApiClient clientActivityService = GivenAMockActivityServiceClient(activityDto);
            clientActivityService.NotifyNewCharge(Arg.Any<ActivityDto>()).Returns(true); ;
            clientActivityService.UpdateNotifyCharge(Arg.Any<ActivityDto>()).Returns(true); ;
            var addChargeAction = new AddChargeAction(clientChargeRepository, clientActivityService);

            var result = await addChargeAction.Execute(newCharge);

            await VerifyResult(newCharge, activityDto, clientActivityService, result);
        }

        [Test]
        public async Task given_an_idenfier_for_delete_charge_we_obtein_a_true_response() {
            clientChargeRepository.DeleteCharge(identifier).Returns(true);
            var deleteChargeAction = new DeleteChargeAction(clientChargeRepository);

            var result = await deleteChargeAction.Execute(identifier);

            result.Should().Be(true);
            await clientChargeRepository.Received(1).DeleteCharge(identifier);
        }

        [Test]
        public async Task given_an_idenfier_for_delete_charge_and_dont_exist_we_obtein_a_false_response() {
            clientChargeRepository.DeleteCharge(identifier).Returns(false);
            var deleteChargeAction = new DeleteChargeAction(clientChargeRepository);

            var result = await deleteChargeAction.Execute(identifier);

            result.Should().Be(false);
            await clientChargeRepository.Received(1).DeleteCharge(identifier);
        }

        [Test]
        public async Task should_return_charge_already_exist_when_try_add_charge_with_exist_identifier() {
            var addResult = true;
            Business.Dtos.Charge newCharge = GivenACharge();
            ActivityDto activityDto = GivenAnActivity(newCharge.identifier, addResult);
            ChargeActivityServiceApiClient clientActivityService = GivenAMockActivityServiceClient(activityDto);
            var addChargeAction = new AddChargeAction(clientChargeRepository, clientActivityService);
            clientChargeRepository.Get(newCharge.identifier).Returns(new ChargeAlreadyExist());

            var result = await addChargeAction.Execute(newCharge);

            result.Should().BeOfType<ChargeAlreadyExist>();
            clientChargeRepository.Received(1).Get(newCharge.identifier);
        }

        private static ChargeRepositoryServiceApiClient GivenARepositoryApiClientMock() {
            return Substitute.For<ChargeRepositoryServiceApiClient>(new object[] { null });
        }

        private static ActivityDto GivenAnActivity(string identifier, bool addResult) {
            return new ActivityDto { identifier = identifier, AddResult = addResult };
        }

        private static ChargeActivityServiceApiClient GivenAMockActivityServiceClient(ActivityDto identifierDto) {
            var clientActivityService = Substitute.For<ChargeActivityServiceApiClient>(new object[] { null }); 
            return clientActivityService;
        }

        private Business.Dtos.Charge GivenACharge() {
            return new Charges.Business.Dtos.Charge { Description = "Nuevo cobro", Amount = 1000, identifier = identifier };
        }
        private async Task VerifyResult(Business.Dtos.Charge newCharge, ActivityDto activityDto, ChargeActivityServiceApiClient clientActivityService, ChargeResponse result) {
            result.Should().BeOfType<ChargeResponseOK>();
            await clientActivityService.Received(1).NotifyNewCharge(Arg.Is<ActivityDto>(item => item.identifier == activityDto.identifier));
            await clientChargeRepository.Received(1).AddCharge(newCharge);
            await clientActivityService.Received(1).UpdateNotifyCharge(Arg.Is<ActivityDto>(item => item.identifier == activityDto.identifier && item.AddResult == activityDto.AddResult));
        }
    }
}