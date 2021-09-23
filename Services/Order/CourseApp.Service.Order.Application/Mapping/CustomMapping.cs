using AutoMapper;
using CourseApp.Service.Order.Application.Dtos;
using CourseApp.Service.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Service.Order.Application.Mapping
{
    public class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Address, AddressDto>();
        }
    }
}
