using App.Domain.Entities;
using App.Domain.Entities.Validators;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;
using App.Domain.Notifications.Interfaces;
using System;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if (!RunValidator(new ProductValidator(), product))
                return;

            await _productRepository.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!RunValidator(new ProductValidator(), product))
                return;

            await _productRepository.Add(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Delete(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
