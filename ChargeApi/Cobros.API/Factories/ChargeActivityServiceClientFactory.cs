using Charge.Activity.Service.Client;
using System;

namespace Cobros.API.Factories {
    internal class ChargeActivityServiceClientFactory {
        internal static ChargeActivityServiceClient GetChargeActivityServiceClient() {
            return new ChargeActivityServiceClient(new System.Net.Http.HttpClient());
        }
    }
}