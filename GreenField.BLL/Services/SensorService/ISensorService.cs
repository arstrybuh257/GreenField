using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Services.SensorService.Models;

namespace GreenField.BLL.Services.SensorService
{
    public interface ISensorService
    {
        public Task<List<SensorDto>> BrowseAsync(BrowseSensors query);
        public Task<SensorDto> GetAsync(Guid id, Guid organisationId);
        public Task CreateAsync(SensorDto sensorDto);
        public Task UpdateAsync(SensorDto sensorDto);
        public Task DeleteAsync(Guid id, Guid organisationId);
        public Task<bool> ExistsAsync(Guid id, Guid organisationId);
    }
}