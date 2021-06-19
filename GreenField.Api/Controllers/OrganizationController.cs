using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Organisation;
using GreenField.BLL.Services.OrganizationService;
using GreenField.BLL.Services.OrganizationService.Models;
using GreenField.Common.Constants;
using GreenField.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;

        public OrganizationController(IOrganizationService organizationService, IMapper mapper)
        {
            _organizationService = organizationService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Authorize(Roles=Roles.SystemAdmin)]
        public async Task<IActionResult> Get([FromQuery] BrowseOrganizations query)
        {
            return Collection(await _organizationService.BrowseAsync(query));
        }

        [HttpGet("{id}")]
        [Authorize(Roles=Roles.SystemAdmin + ", " + Roles.OrganisationAdmin)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (OrganisationId != id && OrganisationId != Guid.Parse(UnchangeableEntities.AdminOrganisationGuidString))
            {
                return Forbid();
            }
            
            return Single(await _organizationService.GetAsync(id));
        }

        [HttpPost]
        [Authorize(Roles=Roles.SystemAdmin)]
        public async Task<IActionResult> Post(CreateOrganisationRequest request)
        {
            var organisationDto = _mapper.Map<OrganizationDto>(request);
            await _organizationService.CreateAsync(organisationDto);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles=Roles.SystemAdmin)]
        public async Task<IActionResult> Put([FromBody] UpdateOrganizationRequest request, Guid id)
        {
            var organisationDto = _mapper.Map<OrganizationDto>(request);
            organisationDto.Id = id;
            await _organizationService.UpdateAsync(organisationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles=Roles.SystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _organizationService.DeleteAsync(id);
            return NoContent();
        }
        
        [HttpPut("lock/{id}")]
        [Authorize(Roles=Roles.SystemAdmin)]
        public async Task<IActionResult> Lock(Guid id)
        {
            var org = await _organizationService.GetAsync(id);
            org.Status = OrganisationStatus.Locked;
            await _organizationService.UpdateAsync(org);
            return NoContent();
        }
        
        [HttpPut("unlock/{id}")]
        [Authorize(Roles=Roles.SystemAdmin)]
        public async Task<IActionResult> Unlock(Guid id)
        {
            var org = await _organizationService.GetAsync(id);
            org.Status = OrganisationStatus.Active;
            await _organizationService.UpdateAsync(org);
            return NoContent();
        }
    }
}