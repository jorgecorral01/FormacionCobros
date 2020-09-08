using Charge.Repository.Service.Business.Dtos;
using Charge.Repository.Service.Repository.Entity.Models;
using System;
using System.Threading.Tasks;

namespace Charge.Repository.Service.Repository {
    public class ChargeRepositoryEntity : IChargeRepository {
        private ChargesContext chargesContext;

        public ChargeRepositoryEntity(ChargesContext chargesContext) {
            this.chargesContext = chargesContext;
        }

        //Task<bool> Add(RepositoryCharge newCharge) 
        public async Task<bool> Add(RepositoryCharge newCharge) {
            await Task.Delay(1);
            Charges entity = new Charges 
                    {
                        Concept = newCharge.Description, 
                        Amount = newCharge.Amount, 
                        Identifier = newCharge.identifier,
                        DateCreated = DateTime.Now
                    };
            chargesContext.Charges.Add(entity);
            chargesContext.SaveChanges();
            return true;
        }
    }
}