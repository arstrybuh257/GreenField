using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using GreenField.Common;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<UserDto>> BrowseAsync()
        {
            var list = await _repository.BrowseAsync();
            return _mapper.Map<List<UserDto>>(list);
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            return _mapper.Map<UserDto>(await _repository.GetAsync(id));
        }
        
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = (await _repository.BrowseAsync()).FirstOrDefault(u => u.Email.Equals(email));
            return _mapper.Map<UserDto>(user);
        }

        public async Task CreateAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();
            await _repository.CreateAsync(user);
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            var user = await _repository.GetAsync(userDto.Id);

            if (user == null)
            {
                throw new GreenFieldNotFoundException();
            }

            user = _mapper.Map<User>(userDto);
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _repository.ExistsAsync(email);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}