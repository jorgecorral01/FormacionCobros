using Charge.Activity.Service.Bussines.Dtos;
using Charge.Activity.Service.Repository;
using Charge.Activity.Service.Repository.Entity;
using System;

namespace Charge.Activity.Service.Action {
    public class UpdateActivityAction {
        private IChargeActivityRepository chargeActivityServiceRepository;

        //public UpdateActivityAction() {
        //}

        public UpdateActivityAction(IChargeActivityRepository chargeActivityServiceRepository) {
            this.chargeActivityServiceRepository = chargeActivityServiceRepository;
        }

        public bool Execute(IdentifierDto identifierDto) {            
            return chargeActivityServiceRepository.Update(identifierDto); ;
        }
    }
}