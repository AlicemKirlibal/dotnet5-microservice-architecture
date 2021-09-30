using CourseApp.Web.Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Web.Services.Abstract
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<List<CategoryViewModel>> GetAllCategoryAsync();

        Task<List<CourseViewModel>> GetCourseByUserIdAsync(string userId);

        Task<CourseViewModel> GetCourseByIdAsync(string courseId);

        Task<bool> DeleteCourseAsync(string courseId);

        Task<bool> AddCourseAsync(CreateCourseViewModel createCourse);

        Task<bool> UpdateCourseAsync(UpdateViewModel updateCourse);
    }
}
