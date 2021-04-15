using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface IWeedService
    {
        public Task<List<WeedDto>> BrowseAsync();
        public Task<WeedDto> GetAsync(Guid id);
        public Task CreateAsync(WeedDto weedDto);
        public Task UpdateAsync(WeedDto weedDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}