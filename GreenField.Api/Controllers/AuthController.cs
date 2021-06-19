using System;
using System.Threading.Tasks;
using GreenField.Api.Models.Auth;
using GreenField.BLL.Services.Interfaces;
using GreenField.BLL.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GreenField.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;

        public AuthController(IIdentityService identityService, IUserService userService)
        {
            _identityService = identityService;
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            Console.WriteLine("Log");
            string token = await _identityService.SignInAsync(loginRequest.Email, loginRequest.Password);

            return token;
        }
        
        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> Current()
        {
            if (User.Identity == null)
            {
                return Unauthorized();
            }
            
            return Single(await _userService.GetByEmailAsync(User.Identity.Name));
        }
    }
}