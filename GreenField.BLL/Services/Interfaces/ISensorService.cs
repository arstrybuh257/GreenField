using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface ISensorService
    {
        public Task<List<SensorDto>> BrowseAsync();
        public Task<SensorDto> GetAsync(Guid id);
        public Task CreateAsync(SensorDto sensorDto);
        public Task UpdateAsync(SensorDto sensorDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(Guid id);
    }
}