using Charge.Activity.Service.Repository;
using Charge.Activity.Service.Repository.Entity;
using Charge.Repository.Service.Repository;
using Charge.Repository.Service.Repository.Entity.Models;
using System;

namespace Charge.Activity.Service.Factories {
    public class RepositoriesFactory {
        public IChargeActivityRepository GetRespository() {
            return new ChargeActivityServiceRepository(new ChargesContext());
        }        
    }
}
