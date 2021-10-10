using CourseApp.Services.FakePayment.Model;
using CourseApp.Shared.Controllers;
using CourseApp.Shared.Dtos;
using CourseApp.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public FakePaymentController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }


        [HttpPost]
        public async  Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {

            var sendEnpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));


            var createOrderMessageCommand = new CreateOrderMessageCommand();

            createOrderMessageCommand.BuyerId = paymentDto.Orders.BuyerId;
            createOrderMessageCommand.District = paymentDto.Orders.Address.District;
            createOrderMessageCommand.Line = paymentDto.Orders.Address.Line;
            createOrderMessageCommand.Province = paymentDto.Orders.Address.Province;
            createOrderMessageCommand.Street = paymentDto.Orders.Address.Street;
            createOrderMessageCommand.ZipCode = paymentDto.Orders.Address.ZipCode;

            paymentDto.Orders.OrderItems.ForEach(x =>
            {
                createOrderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                });
            });


           await sendEnpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResultInstance(Shared.Dtos.Response<NoContent>.Success(200));
        }
    }
}
