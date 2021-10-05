using App.Api.Controllers.Base;
using App.Api.Filters;
using App.Api.ViewModels;
using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;
using App.Domain.Notifications.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Api.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v:{version:apiVersion}/[controller]")]
    public class SupplierController : MainController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public SupplierController(
            ISupplierRepository supplierRepository,
            ISupplierService supplierService,
            IAddressRepository addressRepository,
            INotifier notifier,
            IMapper mapper) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierViewModel>>> GetAllSuppliers()
        {
            return Ok(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> GetSupplierById(Guid id)
        {
            return Ok(_mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddressProducts(id)));
        }

        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<SupplierViewModel>> Add(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _supplierService.Add(_mapper.Map<Supplier>(supplierViewModel));

            return CustomResponse(supplierViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SupplierViewModel>> Update(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id)
            {
                NotifyError("O id informado na rota é diferente do id do request");

                return CustomResponse(supplierViewModel);
            }

            if (!ModelState.IsValid)
            {
                CustomResponse(ModelState);
            }

            await _supplierService.Update(_mapper.Map<Supplier>(supplierViewModel));

            return CustomResponse(supplierViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var supplierViewModel = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddressProducts(id));

            if (supplierViewModel == null)
                return NotFound();

            await _supplierService.Delete(id);

            return CustomResponse(supplierViewModel);
        }

        [HttpGet("address/{id:guid}")]
        public async Task<ActionResult> GetAddressBySupplierId(Guid id)
        {
            return Ok(_mapper.Map<AddressViewModel>(await _addressRepository.GetById(id)));
        }

        [HttpPut("address/{id:guid}")]
        public async Task<ActionResult> GetAddressBySupplierId(Guid id, AddressViewModel addressViewModel)
        {
            if (id != addressViewModel.Id)
            {
                NotifyError("O id informado na rota é diferente do id do request");

                return CustomResponse(addressViewModel);
            }

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _supplierService.UpdateAddress(_mapper.Map<Address>(addressViewModel));

            return CustomResponse(addressViewModel);
        }
    }
}