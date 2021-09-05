using CourseApp.Services.PhotoStock.Dtos;
using CourseApp.Shared.Controllers;
using CourseApp.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseApp.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : CustomBaseController
    {
        public async Task<IActionResult> PhotoSave(IFormFile photo,CancellationToken cancellationToken)
        {
            if (photo!=null&&photo.Length>0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);


                using var stream = new FileStream(path, FileMode.Create);

                await photo.CopyToAsync(stream, cancellationToken);

                //photostock.api/photos/forexample.jpg
                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new PhotoDto { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto,200));

            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));
        }


        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwwroot/photos", photoUrl);

            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("photo not found", 404));
            }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));


        }
    }
}
