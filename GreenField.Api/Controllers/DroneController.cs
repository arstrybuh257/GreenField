using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Drone;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using GreenField.Common;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
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
        public async Task<IActionResult> Get([FromQuery] BrowseDrones query)
        {
            return Collection(await _droneService.BrowseAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _droneService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDroneRequest request)
        {
            var droneDto = _mapper.Map<DroneDto>(request);
            droneDto.Battery = 0;
            droneDto.Status = DeviceStatus.Disabled;
            await _droneService.CreateAsync(droneDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateDroneRequest request, Guid id)
        {
            var droneDto = _mapper.Map<DroneDto>(request);
            droneDto.Id = id;
            await _droneService.UpdateAsync(droneDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _droneService.DeleteAsync(id);
            return NoContent();
        }
    }
}