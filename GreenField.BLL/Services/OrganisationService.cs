using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using GreenField.Common;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services
{
    public class OrganisationService : IOrganisationService
    {
        private readonly IOrganisationRepository _repository;
        private readonly IMapper _mapper;
        
        public OrganisationService(IOrganisationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<OrganisationDto>> BrowseAsync()
        {
            var list = await _repository.BrowseAsync();
            return _mapper.Map<List<OrganisationDto>>(list);
        }

        public async Task<OrganisationDto> GetAsync(Guid id)
        {
            return _mapper.Map<OrganisationDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(OrganisationDto organisationDto)
        {
            var organisation = _mapper.Map<Organisation>(organisationDto);
            organisation.Id = Guid.NewGuid();
            await _repository.CreateAsync(organisation);
        }

        public async Task UpdateAsync(OrganisationDto organisationDto)
        {
            var organisation = await _repository.GetAsync(organisationDto.Id);

            if (organisation == null)
            {
                throw new GreenFieldNotFoundException();
            }

            organisation = _mapper.Map<Organisation>(organisationDto);
            await _repository.UpdateAsync(organisation);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _repository.ExistsAsync(name);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}