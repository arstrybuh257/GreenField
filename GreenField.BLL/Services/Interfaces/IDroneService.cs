using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface IDroneService
    {
        public Task<List<DroneDto>> BrowseAsync();
        public Task<DroneDto> GetAsync(Guid id);
        public Task CreateAsync(DroneDto droneDto);
        public Task UpdateAsync(DroneDto droneDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(Guid id);
    }
}