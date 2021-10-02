using CourseApp.Web.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Web.Services.Abstract
{
   public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketViewModel viewModel);

        Task<BasketViewModel> Get();

        Task<bool> Delete();

        Task AddBasketItem(BasketItemViewModel viewModel);

        Task<bool> RemoveBasketItem(string courseId);

        Task<bool> ApplyDiscount(string discountCode);

        Task<bool> CancelApplyDiscount();
    }
}
