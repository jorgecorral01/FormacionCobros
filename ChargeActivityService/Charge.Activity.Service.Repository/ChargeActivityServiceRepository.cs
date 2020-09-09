using Charge.Activity.Service.Repository.Entity;
using Charge.Repository.Service.Repository.Entity.Models;
using System;

namespace Charge.Activity.Service.Repository {
    public class ChargeActivityServiceRepository : IChargeActivityRepository {
        public ChargeActivityServiceRepository() {
        }

        public ChargeActivityServiceRepository(ChargesContext chargesContext) {
            ChargesContext = chargesContext;
        }

        public ChargesContext ChargesContext { get; }

        public bool Add(string identifier) {
            ChargesContext.Activities.Add(new Activities { Identifier = identifier });
            ChargesContext.SaveChanges();
            return true;
        }
    }
}
