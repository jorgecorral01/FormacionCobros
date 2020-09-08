using Charge.Repository.Service.Repository;
using Charge.Repository.Service.Repository.Entity.Models;
using System;

namespace Charge.Repository.Service.Factories {
    public class RepositoriesFactory {
        public IChargeRepository GetRespository() {
            return new ChargeRepositoryEntity(new ChargesContext());
        }
    }
}
