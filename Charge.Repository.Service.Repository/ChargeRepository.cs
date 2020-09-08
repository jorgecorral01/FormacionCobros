using Charge.Repository.Service.Business.Dtos;
using System;
using System.Threading.Tasks;

namespace Charge.Repository.Service.Repository {
    public class ChargeRepository {
        public ChargeRepository() {
        }

        public async Task<bool> Add(RepositoryCharge newCharge) {
            await Task.Delay(1);
            return true;
        }
    }
}