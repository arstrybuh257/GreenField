using System;
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
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _repository;
        private readonly IMapper _mapper;
        
        public SensorService(ISensorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<SensorDto>> BrowseAsync()
        {
            var list = await _repository.BrowseAsync();
            return _mapper.Map<List<SensorDto>>(list);
        }

        public async Task<SensorDto> GetAsync(Guid id)
        {
            return _mapper.Map<SensorDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(SensorDto sensorDto)
        {
            var sensor = _mapper.Map<Sensor>(sensorDto);
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

            sensor = _mapper.Map<Sensor>(sensorDto);
            await _repository.UpdateAsync(sensor);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
        
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}