using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> BrowseAsync();
        public Task<UserDto> GetAsync(Guid id);
        public Task CreateAsync(UserDto userDto);
        public Task UpdateAsync(UserDto userDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string email);
        public Task<bool> ExistsAsync(Guid id);
        Task<UserDto> GetByEmailAsync(string email);
    }
}