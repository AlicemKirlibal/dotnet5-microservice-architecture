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

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        public Task<bool> ApplyDiscount(string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelApplyDiscount()
        {
            throw new NotImplementedException();
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
