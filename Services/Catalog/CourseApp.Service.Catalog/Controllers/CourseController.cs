using CourseApp.Service.Catalog.Dtos;
using CourseApp.Service.Catalog.Services;
using CourseApp.Shared.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomBaseController
    {
        private readonly ICourseService _couserService;

        public CourseController(ICourseService couserService)
        {
            _couserService = couserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _couserService.GetAllAsync();

            return CreateActionResultInstance(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _couserService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto createDto)
        {
            var response = await _couserService.CreateAsync(createDto);

            return CreateActionResultInstance(response);

        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto updateDto)
        {
            var response = await _couserService.UpdateAsync(updateDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _couserService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }

    }
}
