using CourseApp.Service.Catalog.Dtos;
using CourseApp.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Service.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto createDto);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto updateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
