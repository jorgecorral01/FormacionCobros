using Charge.Activity.Service.Client;
using Charges.Action;
using Chargues.Repository.Service.Client;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using System;

namespace Cobros.API.Factories {
    public class ActionFactory {
        private ChargeRepositoryServiceClient chargeRepositoryServiceClient;
        private ChargeActivityServiceApiClient chargeActivityServiceClient;
        public ActionFactory() {
            chargeRepositoryServiceClient = ChargeRepositoryServiceClientFactory.GetChargeRepositoryServiceClient();
            chargeActivityServiceClient = ChargeActivityServiceClientFactory.GetChargeActivityServiceClient();
        }
        public virtual  AddChargeAction CreateAddChargeAction() {
            return new AddChargeAction(
                    chargeRepositoryServiceClient,
                    chargeActivityServiceClient);
        }
    }
}