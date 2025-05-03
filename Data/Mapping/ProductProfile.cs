using AutoMapper;
using EComerceApi.Models;
using EComerceApi.ViewModels;

namespace EComerceApi.Data.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductViewModel>().ReverseMap();
    }


    
}

 