using Charges.Action;
using System;

namespace Cobros.API.Factories {
    public class ActionFactory {
        public virtual  AddChargeAction CreateAddChargeAction() {
            return new AddChargeAction(
                    ChargeRepositoryServiceClientFactory.GetChargeRepositoryServiceClient(),
                    ChargeActivityServiceClientFactory.GetChargeActivityServiceClient()
                    );
        }
    }
}