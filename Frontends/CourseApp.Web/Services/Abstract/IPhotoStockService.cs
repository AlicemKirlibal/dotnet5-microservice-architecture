using CourseApp.Web.Models.PhotoStocks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Web.Services.Abstract
{
    public interface IPhotoStockService
    {
        Task<PhotoStockViewModel> UploadPhoto(IFormFile formFİle);

        Task<bool> DeletePhoto(string photoUrl);
    }
}
