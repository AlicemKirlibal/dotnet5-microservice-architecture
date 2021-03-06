using CourseApp.IdentityServer.Dtos;
using CourseApp.IdentityServer.Models;
using CourseApp.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace CourseApp.IdentityServer.Controllers
{   [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser
            {
                City = signUpDto.City,
                UserName = signUpDto.UserName,
                Email = signUpDto.Email
            };

            var result = await _userManager.CreateAsync(user,signUpDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x=>x.Description).ToList(),400));
            }

            return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaims = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaims == null) return BadRequest();

            var user = await _userManager.FindByIdAsync(userIdClaims.Value);
            if (user == null) return BadRequest();

            return Ok(new {Id=user.Id,UserName=user.UserName,Email=user.Email,City=user.City});
           
        }



    }
}
