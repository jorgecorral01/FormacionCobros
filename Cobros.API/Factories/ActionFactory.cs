using Cobros.Action.Test;
using System;

namespace Cobros.API.Factories {
    public class ActionFactory {
        public AddChargeAction CreateAddChargeAction() {
            return new AddChargeAction(ChargeRepositoryServiceClientFactory.GetChargeRepositoryServiceClient());
        }
    }
}