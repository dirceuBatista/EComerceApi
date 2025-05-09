using AutoMapper;
using EComerceApi.Models;
using EComerceApi.ViewModels;

namespace EComerceApi.Data.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
    }
}