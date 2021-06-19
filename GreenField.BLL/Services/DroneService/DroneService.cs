using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Services.Drone.Models;
using GreenField.BLL.Services.DroneService.Models;
using GreenField.Common;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services.DroneService
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
        
        public async Task<List<DroneDto>> BrowseAsync(BrowseDrones browseDrones)
        {
            var list = await _repository.BrowseAsync();
            list = list.Where(x => x.OrganisationId == browseDrones.OrganisationId).ToList();
            return _mapper.Map<List<DroneDto>>(list);
        }

        public async Task<DroneDto> GetAsync(Guid id, Guid organisationId)
        {
            var drone = await _repository.GetAsync(id);
            if (drone.OrganisationId != organisationId)
            {
                return null;
            }
            
            return _mapper.Map<DroneDto>(drone);
        }

        public async Task CreateAsync(DroneDto droneDto)
        {
            var drone = _mapper.Map<DAL.Entities.Drone>(droneDto);
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

            if (drone.OrganisationId != droneDto.OrganisationId)
            {
                throw new GreenFieldException("forbidden", "You are not allowed to modify this.");
            }

            drone = _mapper.Map<DAL.Entities.Drone>(droneDto);
            await _repository.UpdateAsync(drone);
        }

        public async Task DeleteAsync(Guid droneId, Guid organisationId)
        {
            if (await ExistsAsync(droneId, organisationId))
            {
                throw new GreenFieldNotFoundException();
            }
            
            await _repository.DeleteAsync(droneId);
        }
        
        public async Task<bool> ExistsAsync(Guid id, Guid organisationId)
        {
            return await _repository.ExistsAsync(id, organisationId);
        }
    }
}