using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.User;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
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
        public async Task<IActionResult> Get([FromQuery] BrowseUsers query)
        {
            var list = await _userService.BrowseAsync();
            return Collection(_mapper.Map<List<UserResponse>>(list));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(_mapper.Map<UserResponse>(await _userService.GetAsync(id)) );
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserRequest request)
        {
            var userDto = _mapper.Map<UserDto>(request);
            await _userService.CreateAsync(userDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest request, Guid id)
        {
            var userDto = _mapper.Map<UserDto>(request);
            userDto.Id = id;
            await _userService.UpdateAsync(userDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}