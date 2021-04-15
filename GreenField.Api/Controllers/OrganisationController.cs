using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Organisation;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    public class OrganisationController : BaseController
    {
        private readonly IOrganisationService _organisationService;
        private readonly IMapper _mapper;

        public OrganisationController(IOrganisationService organisationService, IMapper mapper)
        {
            _organisationService = organisationService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseOrganisations query)
        {
            return Collection(await _organisationService.BrowseAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _organisationService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrganisationRequest request)
        {
            var organisationDto = _mapper.Map<OrganisationDto>(request);
            await _organisationService.CreateAsync(organisationDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateOrganisationRequest request, Guid id)
        {
            var organisationDto = _mapper.Map<OrganisationDto>(request);
            organisationDto.Id = id;
            await _organisationService.UpdateAsync(organisationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _organisationService.DeleteAsync(id);
            return NoContent();
        }
    }
}