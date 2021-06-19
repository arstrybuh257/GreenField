using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.UserService.Models;

namespace GreenField.BLL.Services.UserService
{
    public interface IUserService
    {
        public Task<List<UserDto>> BrowseAdminAsync(BrowseUsers query);
        public Task<List<UserDto>> BrowseOrgAsync(BrowseUsers query);
        Task<UserDto> GetAsync(Guid id, Guid? organisationId = null);
        public Task<Credentials> CreateAsync(UserDto userDto);
        public Task UpdateAsync(UserDto userDto);
        public Task DeleteAsync(Guid id, Guid? organisationId = null);
        public Task<bool> ExistsAsync(string email, Guid? organisationId = null);
        public Task<bool> ExistsAsync(Guid id, Guid? organisationId = null);
        Task<UserDto> GetByEmailAsync(string email, Guid? organisationId = null);
    }
}