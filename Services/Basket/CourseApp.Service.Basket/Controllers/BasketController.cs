using CourseApp.Service.Basket.Dtos;
using CourseApp.Service.Basket.Services.Abstract;
using CourseApp.Shared.Controllers;
using CourseApp.Shared.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Service.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var basket = await _basketService.GetBasket(_sharedIdentityService.GetUserId);

            return CreateActionResultInstance(basket);
        }

        [HttpPost]

        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {

            basketDto.UserId =  _sharedIdentityService.GetUserId;
            var response = await _basketService.SaveOrUpdate(basketDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return CreateActionResultInstance( await _basketService.Delete(_sharedIdentityService.GetUserId));
        }


    }
}
