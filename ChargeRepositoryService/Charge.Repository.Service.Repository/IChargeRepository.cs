using Charge.Repository.Service.Business.Dtos;
using System;
using System.Threading.Tasks;

namespace Charge.Repository.Service.Repository {
    public interface IChargeRepository {
        Task<bool> Add(RepositoryCharge newCharge);
    }
}