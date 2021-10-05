using App.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Services
{
    public interface ISupplierService : IDisposable
    {
        Task<bool> Add(Supplier supplier);
        Task<bool> Update(Supplier supplier);
        Task<bool> Delete(Guid id);
        Task<bool> UpdateAddress(Address address);
    }
}
