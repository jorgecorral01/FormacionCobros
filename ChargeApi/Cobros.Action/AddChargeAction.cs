using Charge.Activity.Service.Client;
using Charges.Business.Dtos;
using Chargues.Repository.Service.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Charges.Action {
    public class AddChargeAction {
        private ChargeRepositoryServiceApiClient clientChargeRepository;
        private readonly ChargeActivityServiceApiClient clientActivityService;

        public AddChargeAction(ChargeRepositoryServiceApiClient clientChargeRepository, ChargeActivityServiceApiClient chargeActivityServiceApiClient) {
            this.clientChargeRepository = clientChargeRepository;
            this.clientActivityService = chargeActivityServiceApiClient;
        }

        public virtual async Task<ChargeResponse> Execute(Business.Dtos.Charge newCharge) {
            if (await clientChargeRepository.Get(newCharge.identifier) is ChargeAlreadyExist) 
                { return new ChargeAlreadyExist(); }

            await clientActivityService.NotifyNewCharge ( new ActivityDto { identifier = newCharge.identifier });
            var resultAddCharge =   await clientChargeRepository.AddCharge(newCharge);
            bool resultAdd = false;
            if (resultAddCharge is ChargeResponseOK) { 
                resultAdd = true;
            }
            var identifierDto = new ActivityDto { identifier = newCharge.identifier, AddResult = resultAdd };
            var result = await clientActivityService.UpdateNotifyCharge(identifierDto);
            if(result) {
                return new ChargeResponseOK(); 
            }
            else {
                return new ChargeResponseKO();
            }            
        }
    }
}