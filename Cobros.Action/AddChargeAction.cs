using Chargues.Repository.Service.Client;
using Cobros.Business.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cobros.Action.Test {
    public class AddChargeAction {
        private ChargeRepositoryServiceClient clientChargeRepository;

        public AddChargeAction() {
        }

        public AddChargeAction(ChargeRepositoryServiceClient clientChargeRepository) {
            this.clientChargeRepository = clientChargeRepository;
        }

        public async Task<bool> Execute(Charge newCharge) {            
            return await clientChargeRepository.AddCharge(newCharge); 
        }
    }
}