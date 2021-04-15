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
    public class DroneService : IDroneService
    {
        private readonly IDroneRepository _repository;
        private readonly IMapper _mapper;
        
        public DroneService(IDroneRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<DroneDto>> BrowseAsync()
        {
            var list = await _repository.BrowseAsync();
            return _mapper.Map<List<DroneDto>>(list);
        }

        public async Task<DroneDto> GetAsync(Guid id)
        {
            return _mapper.Map<DroneDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(DroneDto droneDto)
        {
            var drone = _mapper.Map<Drone>(droneDto);
            drone.Id = Guid.NewGuid();
            await _repository.CreateAsync(drone);
        }

        public async Task UpdateAsync(DroneDto droneDto)
        {
            var drone = await _repository.GetAsync(droneDto.Id);

            if (drone == null)
            {
                throw new GreenFieldNotFoundException();
            }

            drone = _mapper.Map<Drone>(droneDto);
            await _repository.UpdateAsync(drone);
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