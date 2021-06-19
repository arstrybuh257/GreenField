using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Services.Drone.Models;
using GreenField.BLL.Services.DroneService.Models;

namespace GreenField.BLL.Services.DroneService
{
    public interface IDroneService
    {
        public Task<List<DroneDto>> BrowseAsync(BrowseDrones browseDrones);
        public Task<DroneDto> GetAsync(Guid id, Guid organisationId);
        public Task CreateAsync(DroneDto droneDto);
        public Task UpdateAsync(DroneDto droneDto);
        public Task DeleteAsync(Guid id, Guid organisationId);
        public Task<bool> ExistsAsync(Guid id, Guid organisationId);
    }
}