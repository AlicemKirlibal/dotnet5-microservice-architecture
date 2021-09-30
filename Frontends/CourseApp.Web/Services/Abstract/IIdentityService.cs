using CourseApp.Shared.Dtos;
using CourseApp.Web.Models;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace CourseApp.Web.Services.Abstract
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SingInInput singInInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
