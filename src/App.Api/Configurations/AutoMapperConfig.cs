using App.Api.ViewModels;
using App.Domain.Entities;
using AutoMapper;

namespace App.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));            
            CreateMap<ProductViewModel, Product>();
            CreateMap<ProductImageViewModel, Product>().ReverseMap();
        }
    }
}
