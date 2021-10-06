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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace App.Api.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductsController(
            IProductRepository productRepository,
            IProductService productService,
            INotifier notifier,
            IMapper mapper,
            ILogger<ProductsController> logger) : base(notifier)
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsSuppliers());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetById(Guid id)
        {
            var productViewModel = _mapper.Map<ProductViewModel>(await _productRepository.GetProductSupplier(id));

            if (productViewModel == null)
                return NotFound();

            return productViewModel;
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var imageName = Guid.NewGuid() + "_" + productViewModel.Image;

            if (!UploadImage(productViewModel.ImageUpload, productViewModel.Image))
            {
                return CustomResponse(productViewModel);
            }

            productViewModel.Image = imageName;

            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return CustomResponse(productViewModel);
        }

        //[HttpPost("large-image")]
        //public async Task<ActionResult<ProductViewModel>> AddWithLargeImage(ProductImageViewModel productImageViewModel)
        //{
        //    if (!ModelState.IsValid) 
        //        return CustomResponse(ModelState);

        //    var imagePrefix = $"{Guid.NewGuid()}_";

        //    if (!await UploadImageByFormFile(productImageViewModel.ImageUpload, imagePrefix))
        //    {
        //        return CustomResponse(ModelState);
        //    }

        //    productImageViewModel.Image = imagePrefix + productImageViewModel.ImageUpload.FileName;

        //    await _productService.Add(_mapper.Map<Product>(productImageViewModel));

        //    return CustomResponse(productImageViewModel);
        //}

        [ClaimsAuthorize("Produto", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                NotifyError("O id informado na rota é diferente do id do request");

                return CustomResponse();
            }

            var productViewModelFromDb = _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));

            if (string.IsNullOrEmpty(productViewModel.Image))
                productViewModel.Image = productViewModelFromDb.Image;

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            if (productViewModel.ImageUpload != null)
            {
                var imageName = Guid.NewGuid() + "_" + productViewModel.Image;

                if (!UploadImage(productViewModel.ImageUpload, imageName))
                {
                    return CustomResponse(ModelState);
                }

                productViewModelFromDb.Image = imageName;
            }

            productViewModelFromDb.SupplierId = productViewModel.SupplierId;
            productViewModelFromDb.Name = productViewModel.Name;
            productViewModelFromDb.Description = productViewModel.Description;
            productViewModelFromDb.Value = productViewModel.Value;
            productViewModelFromDb.Active = productViewModel.Active;

            await _productService.Update(_mapper.Map<Product>(productViewModelFromDb));

            return CustomResponse(productViewModel);
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Excluir(Guid id)
        {
            var productViewModel = _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));

            if (productViewModel == null)
                return NotFound();

            await _productService.Delete(id);

            return CustomResponse(productViewModel);
        }

        private bool UploadImage(string imageBase64, string imageName)
        {
            if (string.IsNullOrEmpty(imageBase64))
            {
                NotifyError("Forneça uma imagem para este produto!");

                return false;
            }

            var imageByteArray = Convert.FromBase64String(imageBase64);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageName);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("Já existe um arquivo com este nome!");

                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageByteArray);

            return true;
        }

        //private async Task<bool> UploadImageByFormFile(IFormFile file, string imagePrefix)
        //{
        //    if (file == null || file.Length <= 0)
        //    {
        //        NotifyError("Forneça uma imagem para este produto!");

        //        return false;
        //    }                

        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{imagePrefix}{file.FileName}");

        //    if (System.IO.File.Exists(path)) 
        //    {
        //        NotifyError("Já existe um arquivo com este nome!");

        //        return false;
        //    }

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    return true;
        //}
    }
}
