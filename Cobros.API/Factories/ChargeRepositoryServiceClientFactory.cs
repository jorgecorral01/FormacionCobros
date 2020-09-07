using Chargues.Repository.Service.Client;
using System;

namespace Cobros.API.Factories {
    internal class ChargeRepositoryServiceClientFactory {
        internal static ChargeRepositoryServiceClient GetChargeRepositoryServiceClient() {
            return new ChargeRepositoryServiceClient(new System.Net.Http.HttpClient());
        }
    }
}