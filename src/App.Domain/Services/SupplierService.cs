using App.Domain.Entities;
using App.Domain.Entities.Validators;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;
using App.Domain.Notifications.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository supplierRepository, IAddressRepository addressRepository, INotifier notifier) : base (notifier)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }

        public async Task<bool> Add(Supplier supplier)
        {
            if (!RunValidator(new SupplierValidator(), supplier) ||
                !RunValidator(new AddressValidator(), supplier.Address))
                return false;

            if (_supplierRepository.Get(s => s.Document == supplier.Document).Result.Any())
            {
                Notify("Já existe um fornecedor com o documento informado!");

                return false;
            }

            await _supplierRepository.Add(supplier);

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            if (_supplierRepository.GetSupplierAddressProducts(id).Result.Products.Any())
            {
                Notify("O fornecedor possui produtos cadastrados!");

                return false;
            }

            var address = await _addressRepository.GetAddressBySupplier(id);

            if (address != null)
            {
                await _addressRepository.Delete(address.Id);
            }

            await _supplierRepository.Delete(id);

            return true;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            if (!RunValidator(new SupplierValidator(), supplier))
                return false;

            if (_supplierRepository.Get(s => s.Document == supplier.Document && s.Id != supplier.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com o documento informado!");

                return false;
            }

            await _supplierRepository.Update(supplier);

            return true;
        }

        public async Task<bool> UpdateAddress(Address address)
        {
            if (!RunValidator(new AddressValidator(), address))
                return false;

            await _addressRepository.Update(address);

            return true;
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
