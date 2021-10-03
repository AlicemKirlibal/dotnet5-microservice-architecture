using CourseApp.Shared.Dtos;
using CourseApp.Web.Models.Baskets;
using CourseApp.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CourseApp.Web.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;

        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task AddBasketItem(BasketItemViewModel viewModel)
        {
            var basket = await Get();

            if (basket!=null)
            {
                if (!basket.BasketItems.Any(x=>x.CourseId==viewModel.CourseId))
                {
                    basket.BasketItems.Add(viewModel);
                }
            }
            else
            {
                basket = new BasketViewModel();

                basket.BasketItems.Add(viewModel);
            }

            await SaveOrUpdate(basket);

        }

        public async Task<bool> ApplyDiscount(string discountCode)
        {
            await CancelApplyDiscount();
            var basket = await Get();

            if (basket==null)
            {
                return false;
            }

            var hasDiscount = await _discountService.GetDiscount(discountCode);
            if (hasDiscount==null)
            {
                return false;
            }

            basket.DiscountRate = hasDiscount.Rate;
            basket.DiscountCode = hasDiscount.Code;
            await SaveOrUpdate(basket);
            return true;


        }

        public async Task<bool> CancelApplyDiscount()
        {
            var basket = await Get();

            if (basket==null&& basket.DiscountCode==null)
            {
                return false;
            }

            basket.DiscountCode = null;
            await SaveOrUpdate(basket);

            return true;


        }

        public async Task<bool> Delete()
        {
            var response = await _httpClient.DeleteAsync("basket");

            return response.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> Get()
        {
            var response = await _httpClient.GetAsync("basket");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

            return successResponse.Data;

            
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await Get();

            if (basket==null)
            {
                return false;
            }

            var deleteBasketItem =  basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);

            if (deleteBasketItem==null)
            {
                return false;
            }

            var deletedResult =  basket.BasketItems.Remove(deleteBasketItem);

            if (!deletedResult)
            {
                return false;
            }

            if (!basket.BasketItems.Any())
            {
                basket.DiscountCode = null;
            }

           return await SaveOrUpdate(basket);



        }

        public async Task<bool> SaveOrUpdate(BasketViewModel viewModel)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("basket", viewModel);

            return response.IsSuccessStatusCode;
           
        }
    }
}
