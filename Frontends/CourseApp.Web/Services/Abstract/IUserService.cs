using CourseApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Web.Services.Abstract
{
   public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
