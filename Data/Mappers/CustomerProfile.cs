using AutoMapper;
using EComerceApi.Models;
using EComerceApi.ViewModels;

namespace EComerceApi.Data.Mapping;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustumerViewModel>().ReverseMap();
    }
}

