using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Authentication;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using GreenField.BLL.Services.UserService.Models;
using GreenField.Common;
using GreenField.Common.Constants;
using GreenField.Common.Enums;
using GreenField.Common.Messaging;
using GreenField.Common.Messaging.Messages;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;
using MediatR;

namespace GreenField.BLL.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly PasswordManager _passwordManager;
        private readonly IMapper _mapper;
        private readonly IRabbitMqBus _bus;
        
        public UserService(IUserRepository repository, IMapper mapper, PasswordManager passwordManager, IMediator mediator, IRabbitMqBus bus, IOrganisationRepository organisationRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordManager = passwordManager;
            _bus = bus;
            _organisationRepository = organisationRepository;
        }
        
        public async Task<List<UserDto>> BrowseAdminAsync(BrowseUsers query)
        {
            var list = await _repository.BrowseAsync();

            if (query.Role != null)
            {
                list = list.Where(x => x.Role == query.Role);
            }
            
            if (query.OrganisationId != null)
            {
                list = list.Where(x => x.OrganisationId == query.OrganisationId);
            }
            
            return _mapper.Map<List<UserDto>>(list.ToList());
        }

        public async Task<List<UserDto>> BrowseOrgAsync(BrowseUsers query)
        {
            var list = await _repository.BrowseAsync();
            list = list.Where(x => x.OrganisationId == query.OrganisationId);
            if (!string.IsNullOrWhiteSpace(query.SearchString))
            {
                list = list.Where(x=>x.Email.ToLower().Contains(query.SearchString.ToLower()) || 
                                     x.FirstName.ToLower().Contains(query.SearchString.ToLower()) ||
                                     x.LastName.ToLower().Contains(query.SearchString.ToLower()));
            }
            
            return _mapper.Map<List<UserDto>>(list.ToList());
        }

        public async Task<UserDto> GetAsync(Guid id, Guid? organisationId = null)
        {
            var user = await _repository.GetAsync(id);
            
            if(organisationId != null)
            {
                if (user != null && user.OrganisationId != organisationId)
                {
                    return null;
                }
            }
            
            return _mapper.Map<UserDto>(await _repository.GetAsync(id));
        }
        
        public async Task<UserDto> GetByEmailAsync(string email, Guid? organisationId = null)
        {
            var user = (await _repository.BrowseAsync()).FirstOrDefault(u => u.Email.Equals(email));
            return _mapper.Map<UserDto>(user);
        }

        public async Task<Credentials> CreateAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();
            user.Role = userDto.Role;
            user.Status = UserStatus.Active;
            var generatedPassword = PasswordManager.GeneratePassword(PasswordManager.CharSet.All, 8);
            _passwordManager.SetHashedPassword(user, generatedPassword);
            user.OrganisationName = (await _organisationRepository.GetAsync(userDto.OrganisationId)).Name;
            await _repository.CreateAsync(user);
            try
            {
                _bus.Publish(new UserRegisteredMessage() {Email = userDto.Email, Password = generatedPassword});
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new Credentials()
            {
                Email = userDto.Email,
                Password = generatedPassword
            };
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            if (userDto.Id.ToString() == UnchangeableEntities.SystemAdminGuidString)
            {
                throw new GreenFieldException("unchangeable_entity", "You cannot change this user");
            }
            
            if (!await ExistsAsync(userDto.Id, userDto.OrganisationId))
            {
                throw new GreenFieldException("not_found", "User not found.");
            }
            
            var user = await _repository.GetAsync(userDto.Id);

            if (user == null)
            {
                throw new GreenFieldNotFoundException();
            }

            user = _mapper.Map<User>(userDto);
            await _repository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id, Guid? organisationId = null)
        {
            if (id.ToString() == UnchangeableEntities.SystemAdminGuidString)
            {
                throw new GreenFieldException("unchangeable_entity", "You cannot change this user.");
            }

            if (!await ExistsAsync(id, organisationId))
            {
                throw new GreenFieldException("not_found", "User not found.");
            }
            
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string email, Guid? organisationId = null)
        {
            return await _repository.ExistsAsync(email, organisationId);
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? organisationId = null)
        {
            return await _repository.ExistsAsync(id, organisationId);
        }
    }
}