using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Services.OrganizationService.Models;
using GreenField.Common;
using GreenField.Common.Constants;
using GreenField.Common.Enums;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services.OrganizationService
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganisationRepository _repository;
        private readonly IMapper _mapper;
        
        public OrganizationService(IOrganisationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<OrganizationDto>> BrowseAsync(BrowseOrganizations query)
        {
            var list = await _repository.BrowseAsync();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                list = list.Where(x => x.Name.ToLower().Contains(query.Name.ToLower()));
            }
            if (query.Country != null)
            {
                list = list.Where(x => x.Country == query.Country);
            }
            
            return _mapper.Map<List<OrganizationDto>>(list.ToList());
        }

        public async Task<OrganizationDto> GetAsync(Guid id)
        {
            return _mapper.Map<OrganizationDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(OrganizationDto organizationDto)
        {
            var organisation = _mapper.Map<Organisation>(organizationDto);
            organisation.Id = Guid.NewGuid();
            organisation.Status = OrganisationStatus.Active;
            await _repository.CreateAsync(organisation);
        }

        public async Task UpdateAsync(OrganizationDto organizationDto)
        {
            if (organizationDto.Id.ToString() == UnchangeableEntities.AdminOrganisationGuidString)
            {
                throw new GreenFieldException("unchangeable_entity", "You cannot change this organisation");
            }
            
            var organisation = await _repository.GetAsync(organizationDto.Id);

            if (organisation == null)
            {
                throw new GreenFieldNotFoundException();
            }

            organisation = _mapper.Map<Organisation>(organizationDto);
            await _repository.UpdateAsync(organisation);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id.ToString() == UnchangeableEntities.AdminOrganisationGuidString)
            {
                throw new GreenFieldException("unchangeable_entity", "You cannot change this organisation");
            }
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