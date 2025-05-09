using AutoMapper;
using EComerceApi.InputModel;
using EComerceApi.Models;
using EComerceApi.ViewModels;

namespace EComerceApi.Data.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        
        CreateMap<Order, OrderViewModel>().MaxDepth(1);
        CreateMap<OrderInputModel, Order>();
        
        

      
    }
}