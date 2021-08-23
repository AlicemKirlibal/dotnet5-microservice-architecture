using CourseApp.Service.Catalog.Dtos;
using CourseApp.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Service.Catalog.Services
{
   public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto createDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
        Task<Response<NoContent>> UpdateAsync(CategoryDto categoryDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
