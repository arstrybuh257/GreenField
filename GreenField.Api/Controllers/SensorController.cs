using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Sensor;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using GreenField.Common;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    public class SensorController : BaseController
    {
        private readonly ISensorService _sensorService;
        private readonly IMapper _mapper;

        public SensorController(ISensorService sensorService, IMapper mapper)
        {
            _sensorService = sensorService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseSensors query)
        {
            return Collection(await _sensorService.BrowseAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _sensorService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSensorRequest request)
        {
            var sensorDto = _mapper.Map<SensorDto>(request);
            sensorDto.Status = DeviceStatus.Enabled;
            await _sensorService.CreateAsync(sensorDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateSensorRequest request, Guid id)
        {
            var sensorDto = _mapper.Map<SensorDto>(request);
            sensorDto.Id = id;
            await _sensorService.UpdateAsync(sensorDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sensorService.DeleteAsync(id);
            return NoContent();
        }
    }
}