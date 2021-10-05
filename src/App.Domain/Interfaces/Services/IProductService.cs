using App.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Services
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}
