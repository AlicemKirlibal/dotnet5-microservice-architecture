using CourseApp.Shared.Dtos;
using CourseApp.Web.Helpers;
using CourseApp.Web.Models.Catalogs;
using CourseApp.Web.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CourseApp.Web.Services.Concrete
{
    public class CatalogSeervice : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogSeervice(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> AddCourseAsync(CreateCourseViewModel createCourse)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(createCourse.PhotoFormFile);

            if (resultPhotoService != null)
            {
                createCourse.Picture = resultPhotoService.Url;
            }


            var response = await _httpClient.PostAsJsonAsync<CreateCourseViewModel>("course", createCourse);

            return response.IsSuccessStatusCode;
           
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"course/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("category");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("course");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccess.Data.ForEach(x =>
            {
                x.ShortPictureUrl = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetCourseByIdAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"course/{courseId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            responseSuccess.Data.ShortPictureUrl = _photoHelper.GetPhotoStockUrl(responseSuccess.Data.Picture);


            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"course/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSuccess.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });


            return responseSuccess.Data;

        }

        public async Task<bool> UpdateCourseAsync(UpdateViewModel updateCourse)
        {


            var resultPhotoService = await _photoStockService.UploadPhoto(updateCourse.PhotoFormFile);

            if (resultPhotoService != null)
            {
                await _photoStockService.DeletePhoto(updateCourse.Picture);
                updateCourse.Picture = resultPhotoService.Url;
            }


            var response = await _httpClient.PutAsJsonAsync<UpdateViewModel>("course", updateCourse);

            return response.IsSuccessStatusCode;

        }
    }
}
