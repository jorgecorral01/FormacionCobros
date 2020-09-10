using Charge.Activity.Service.Bussines.Dtos;
using Charge.Activity.Service.Repository.Entity;
using Charge.Repository.Service.Repository.Entity.Models;
using System;
using System.Linq;

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

        public virtual bool Update(Charge.Activity.Service.Bussines.Dtos.IdentifierDto identifierDto) {
            var activity = ChargesContext.Activities.Where(item => item.Identifier == identifierDto.identifier).FirstOrDefault();
            if(activity != null) {
                UpdateValues(identifierDto, activity);
            }
            return true;
        }

        private void UpdateValues(IdentifierDto identifierDto, Activities activity) {
            activity.DateSend = DateTime.Now;
            activity.AddResult = identifierDto.AddResult;
            ChargesContext.SaveChanges();
        }
    }
}
