using CourseApp.Web.Exceptions;
using CourseApp.Web.Models;
using CourseApp.Web.Models.Catalogs;
using CourseApp.Web.Services.Abstract;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var dataList = await _catalogService.GetAllCourseAsync();
            return View(dataList);
        }

        public async Task<IActionResult> Details(string id)
        {
            var response = await _catalogService.GetCourseByIdAsync(id);

            return View(response);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (errorFeature != null && errorFeature.Error is UnAuthorizedException)
            {
                return RedirectToAction(nameof(AuthController.Logout), "Auth");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


      
    }
}
