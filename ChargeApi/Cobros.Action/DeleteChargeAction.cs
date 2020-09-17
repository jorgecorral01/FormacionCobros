using Charge.Activity.Service.Client;
using Chargues.Repository.Service.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Charges.Action {
    public class DeleteChargeAction {
        private readonly ChargeRepositoryServiceApiClient clientChargeRepository;

        public DeleteChargeAction(ChargeRepositoryServiceApiClient clientChargeRepository) {
            this.clientChargeRepository = clientChargeRepository;            
        }
        public virtual  async Task<bool> Execute(string identifier) {            
            return await clientChargeRepository.DeleteCharge(identifier);
        }
    }
}
