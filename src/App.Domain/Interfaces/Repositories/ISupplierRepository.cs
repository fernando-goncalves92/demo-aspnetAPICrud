using System;
using System.Threading.Tasks;
using App.Domain.Entities;

namespace App.Domain.Interfaces.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetSupplierAddressProducts(Guid id);
        Task<Supplier> GetSupplierAddress(Guid id);
    }
}
