using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Domain.Entities;

namespace App.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId);
        Task<IEnumerable<Product>> GetProductsSuppliers();
        Task<Product> GetProductSupplier(Guid id);
    }
}
