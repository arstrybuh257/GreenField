using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Auth;
using GreenField.BLL.Services.Interfaces;
using GreenField.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper, IAuthService authService)
        {
            _userService = userService;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            var userDto = await _userService.GetByEmailAsync(loginRequest.Email);

            if (userDto != null)
            {
                var jwtToken = _authService.CreateToken(userDto);
                return jwtToken;
            }

            return BadRequest("Invalid login attempt.");
        }
    }
}