using Charge.Activity.Service.Client;
using Charges.Business.Dtos;
using Chargues.Repository.Service.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Charges.Action {
    public class AddChargeAction {
        private ChargeRepositoryServiceClient clientChargeRepository;
        private readonly ChargeActivityServiceClient clientActivityService;

        public AddChargeAction() {
        }

        public AddChargeAction(ChargeRepositoryServiceClient clientChargeRepository, ChargeActivityServiceClient clientActivityService) {
            this.clientChargeRepository = clientChargeRepository;
            this.clientActivityService = clientActivityService;
        }

        public virtual async Task<bool> Execute(Business.Dtos.Charge newCharge) {
            await clientActivityService.NotifyNewCharge ( new ActivityDto { identifier = newCharge.identifier });
            var resultAdd =   await clientChargeRepository.AddCharge(newCharge);
            var identifierDto = new ActivityDto { identifier = newCharge.identifier, AddResult = resultAdd };
            return await clientActivityService.UpdateNotifyCharge(identifierDto);
        }
    }
}