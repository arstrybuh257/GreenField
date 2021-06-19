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
    [Authorize(Roles = Roles.OrganisationAdmin)]
    public class OrganisationUserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public OrganisationUserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseUsers query)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            query.OrganisationId = OrganisationId;
            var list = await _userService.BrowseOrgAsync(query);
            
            return Collection(list);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            
            return Single(await _userService.GetAsync(id, OrganisationId));
        }
        
        [HttpPost()]
        public async Task<IActionResult> CreateUser(CreateOrganisationUserRequest request)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            
            request.OrganisationId = OrganisationId;
            var userDto = _mapper.Map<UserDto>(request);
            userDto.Role = Role.User;
            var credentials = await _userService.CreateAsync(userDto);
            return Ok(credentials);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest request, Guid id)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            
            var userDto = _mapper.Map<UserDto>(request);
            userDto.Id = id;
            userDto.OrganisationId = OrganisationId;
            await _userService.UpdateAsync(userDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            await _userService.DeleteAsync(id, OrganisationId);
            return NoContent();
        }
    }
}