using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Drone;
using GreenField.BLL.Services.Drone;
using GreenField.BLL.Services.Drone.Models;
using GreenField.BLL.Services.DroneService;
using GreenField.BLL.Services.DroneService.Models;
using GreenField.Common.Constants;
using GreenField.DAL.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    [Authorize(Roles=Roles.OrganisationAdmin)]
    public class DroneController : BaseController
    {
        private readonly IDroneService _droneService;
        private readonly IMapper _mapper;

        public DroneController(IDroneService droneService, IMapper mapper)
        {
            _droneService = droneService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Collection(await _droneService.BrowseAsync(new BrowseDrones(){OrganisationId = OrganisationId}));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            return Single(await _droneService.GetAsync(id, OrganisationId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDroneRequest request)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            var droneDto = _mapper.Map<DroneDto>(request);
            droneDto.Status = DroneStatus.GetDefaultStatus();
            droneDto.OrganisationId = OrganisationId;
            await _droneService.CreateAsync(droneDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateDroneRequest request, Guid id)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            var droneDto = _mapper.Map<DroneDto>(request);
            droneDto.Id = id;
            droneDto.Id = OrganisationId;
            await _droneService.UpdateAsync(droneDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            await _droneService.DeleteAsync(id, OrganisationId);
            return NoContent();
        }
    }
}