using Charge.Activity.Service.Client;
using Charges.Business.Dtos;
using Chargues.Repository.Service.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cobros.Action.Test {
    public class AddChargeAction {
        private ChargeRepositoryServiceClient clientChargeRepository;
        private readonly ChargeActivityServiceClient clientActivityService;

        public AddChargeAction() {
        }

        public AddChargeAction(ChargeRepositoryServiceClient clientChargeRepository, ChargeActivityServiceClient clientActivityService) {
            this.clientChargeRepository = clientChargeRepository;
            this.clientActivityService = clientActivityService;
        }

        public async Task<bool> Execute(Charges.Business.Dtos.Charge newCharge) {
            await clientActivityService.NotifyNewCharge(newCharge.identifier);
            return await clientChargeRepository.AddCharge(newCharge); 
        }
    }
}