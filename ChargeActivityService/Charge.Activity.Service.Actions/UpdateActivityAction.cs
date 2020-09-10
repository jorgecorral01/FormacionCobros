using Charge.Activity.Service.Bussines.Dtos;
using Charge.Activity.Service.Repository;
using System;

namespace Charge.Activity.Service.Action {
    public class UpdateActivityAction {
        private ChargeActivityServiceRepository chargeActivityServiceRepository;

        public UpdateActivityAction() {
        }

        public UpdateActivityAction(ChargeActivityServiceRepository chargeActivityServiceRepository) {
            this.chargeActivityServiceRepository = chargeActivityServiceRepository;
        }

        public bool Execute(IdentifierDto identifierDto) {            
            return chargeActivityServiceRepository.UpdateActivity(identifierDto); ;
        }
    }
}