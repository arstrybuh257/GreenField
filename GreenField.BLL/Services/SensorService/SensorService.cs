using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Services.Interfaces;
using GreenField.BLL.Services.SensorService;
using GreenField.BLL.Services.SensorService.Models;
using GreenField.Common;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services.Sensor
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _repository;
        private readonly IMapper _mapper;
        
        public SensorService(ISensorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<SensorDto>> BrowseAsync(BrowseSensors query)
        {
            var list = (await _repository.BrowseAsync()).Where(x=>x.OrganisationId == query.OrganisationId);
            return _mapper.Map<List<SensorDto>>(list);
        }

        public async Task<SensorDto> GetAsync(Guid id, Guid organisationId)
        {
            var sensor = await _repository.GetAsync(id);

            if (sensor.OrganisationId != organisationId)
            {
                return null;
            }
            
            return _mapper.Map<SensorDto>(sensor);
        }

        public async Task CreateAsync(SensorDto sensorDto)
        {
            var sensor = _mapper.Map<DAL.Entities.Sensor>(sensorDto);
            sensor.Id = Guid.NewGuid();
            await _repository.CreateAsync(sensor);
        }

        public async Task UpdateAsync(SensorDto sensorDto)
        {
            var sensor = await _repository.GetAsync(sensorDto.Id);

            if (sensor == null)
            {
                throw new GreenFieldNotFoundException();
            }

            sensor = _mapper.Map<DAL.Entities.Sensor>(sensorDto);
            await _repository.UpdateAsync(sensor);
        }

        public async Task DeleteAsync(Guid id, Guid organisationId)
        {
            if (!await ExistsAsync(id, organisationId))
            {
                throw new GreenFieldNotFoundException();
            }
            
            await _repository.DeleteAsync(id);
        }
        
        public async Task<bool> ExistsAsync(Guid id, Guid organisationId)
        {
            return await _repository.ExistsAsync(id, organisationId);
        }
    }
}