using CourseApp.Shared.Dtos;
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

        public CatalogSeervice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddCourseAsync(CreateCourseViewModel createCourse)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateCourseViewModel>("courses", createCourse);

            return response.IsSuccessStatusCode;
           
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

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

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSuccess.Data;

        }

        public async Task<bool> UpdateCourseAsync(UpdateViewModel updateCourse)
        {
            var response = await _httpClient.PutAsJsonAsync<UpdateViewModel>("courses", updateCourse);

            return response.IsSuccessStatusCode;

        }
    }
}
