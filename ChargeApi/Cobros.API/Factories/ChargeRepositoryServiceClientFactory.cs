using Chargues.Repository.Service.Client;
using HttpApiClient;
using System;

namespace Cobros.API.Factories {
    internal class ChargeRepositoryServiceClientFactory {
        internal static ChargeRepositoryServiceApiClient GetChargeRepositoryServiceClient() {
            return new ChargeRepositoryServiceApiClient(new clsHttpApliClient(new System.Net.Http.HttpClient()));
        }
    }
}