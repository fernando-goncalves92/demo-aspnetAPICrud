using System;
using System.Threading.Tasks;
using App.Data.Context;
using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DatabaseContext context) : base(context) { }
       
        public async Task<Supplier> GetSupplierAddress(Guid id)
        {
            return await _context
                .Suplliers
                .AsNoTracking()
                .Include(a => a.Address)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> GetSupplierAddressProducts(Guid id)
        {
            return await _context
                .Suplliers
                .AsNoTracking()
                .Include(a => a.Address)
                .Include(a => a.Products)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
