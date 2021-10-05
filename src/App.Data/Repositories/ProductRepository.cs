using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using App.Data.Context;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using App.Domain.Interfaces.Repositories;

namespace App.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext _context) : base (_context) { }

        public async Task<Product> GetProductSupplier(Guid id)
        {
            return await _context.Products.AsNoTracking().Include(s => s.Supplier).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId)
        {
            return await Get(p => p.SupplierId == supplierId);
        }

        public async Task<IEnumerable<Product>> GetProductsSuppliers()
        {
            return await _context.Products.AsNoTracking().Include(s => s.Supplier).OrderBy(p => p.Name).ToListAsync();
        }
    }
}
