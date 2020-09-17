using Charge.Activity.Service.Client;
using HttpApiClient;
using System;

namespace Cobros.API.Factories {
    internal class ChargeActivityServiceClientFactory {
        internal static ChargeActivityServiceApiClient GetChargeActivityServiceClient() {
            return new ChargeActivityServiceApiClient(new clsHttpApliClient( new System.Net.Http.HttpClient()));
        }
    }
}