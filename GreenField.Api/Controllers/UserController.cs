using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.User;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.UserService;
using GreenField.BLL.Services.UserService.Models;
using GreenField.Common.Constants;
using GreenField.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    //[Authorize(Roles = Roles.SystemAdmin)]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Authorize(Roles = Roles.SystemAdmin)]
        public async Task<IActionResult> Get([FromQuery] BrowseUsers query)
        {
            var list = await _userService.BrowseAdminAsync(query);
            return Collection(list);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.SystemAdmin)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _userService.GetAsync(id));
        }
        
        [HttpPost("createSystemAdmin")]
        //[Authorize(Roles = Roles.SystemAdmin)]
        public async Task<IActionResult> CreateSystemAdmin(CreateSystemAdminRequest request)
        {
            if (await _userService.ExistsAsync(request.Email))
            {
                return BadRequest("User with this name already exists.");
            }
            var userDto = _mapper.Map<UserDto>(request);
            userDto.Role = Role.SystemAdmin;
            userDto.OrganisationId = new Guid(UnchangeableEntities.AdminOrganisationGuidString);
            var credentials = await _userService.CreateAsync(userDto);
            return Ok(credentials);
        }
        
        [HttpPost("createOrganisationAdmin")]
        [Authorize(Roles = Roles.SystemAdmin)]
        public async Task<IActionResult> CreateOrganisationAdmin(CreateOrganisationUserRequest request)
        {
            if (await _userService.ExistsAsync(request.Email))
            {
                return BadRequest("User with this name already exists.");
            }
            var userDto = _mapper.Map<UserDto>(request);
            userDto.Role = Role.OrganisationAdmin;
            userDto.OrganisationId = request.OrganisationId;
            var credentials = await _userService.CreateAsync(userDto);
            return Ok(credentials);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.SystemAdmin)]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest request, Guid id)
        {
            var userDto = _mapper.Map<UserDto>(request);
            userDto.Id = id;
            await _userService.UpdateAsync(userDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.SystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}