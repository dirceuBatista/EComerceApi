using System.ComponentModel.DataAnnotations;
using EComerceApi.Models;
using EComerceApi.ViewModels;

namespace EComerceApi.InputModel;

    public class OrderInputModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
       
    }
