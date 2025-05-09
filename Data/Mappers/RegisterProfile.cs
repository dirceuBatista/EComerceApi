using AutoMapper;
using EComerceApi.Models;
using EComerceApi.ViewModels;

namespace EComerceApi.Data.Mapping;

public class RegisterProfile : Profile
{
    public RegisterProfile()
    {
        CreateMap<User, RegisterViewModel>();
    }
    
}

