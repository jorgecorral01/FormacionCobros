using Chargues.Repository.Service.Client;
using Cobros.Business.Dtos;
using System;

namespace Cobros.Action.Test {
    public class AddChargeAction {
        private ChargeRepositoryServiceClient clientChargeRepository;

        public AddChargeAction() {
        }

        public AddChargeAction(ChargeRepositoryServiceClient clientChargeRepository) {
            this.clientChargeRepository = clientChargeRepository;
        }

        public bool Execute(Charge newCharge) {            
            return clientChargeRepository.AddCharge(newCharge); 
        }
    }
}