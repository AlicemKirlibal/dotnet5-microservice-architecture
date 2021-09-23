using CourseApp.Service.Order.Application.Dtos;
using CourseApp.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Service.Order.Application.Commands
{
   public class CreateOrderCommand:IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

        public AddressDto AddressDto { get; set; }
    }
}
