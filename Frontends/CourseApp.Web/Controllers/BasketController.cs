using CourseApp.Web.Models.Baskets;
using CourseApp.Web.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Web.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;

        public BasketController(IBasketService basketService, ICatalogService catalogService)
        {
            _basketService = basketService;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetCourseByIdAsync(courseId);

            var basketItem = new BasketItemViewModel { CourseId = course.Id, CourseName = course.Name, Price = course.Price };

            await _basketService.AddBasketItem(basketItem);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {

          await  _basketService.RemoveBasketItem(courseId);

            return RedirectToAction(nameof(Index));

        }
    }
}
