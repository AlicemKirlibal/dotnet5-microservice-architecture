using CourseApp.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var userExist = await _userManager.FindByEmailAsync(context.UserName);

            if (userExist==null)
            {
                var errors = new Dictionary<string, object>();

                errors.Add("errors",new List<string> {"Email or Password error" });

                context.Result.CustomResponse = errors;
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(userExist,context.Password);
            if (passwordCheck == false)
            {
                var errors = new Dictionary<string, object>();

                errors.Add("errors", new List<string> { "Email or Password error" });

                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(userExist.Id.ToString(), OidcConstants.AuthenticationMethods.Password);

        }
    }
}
