using System;
using System.Threading.Tasks;
using App.Data.Context;
using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DatabaseContext context) : base(context) { }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await _context.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.SupplierId == supplierId);
        }
    }
}
