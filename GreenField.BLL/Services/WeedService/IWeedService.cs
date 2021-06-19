using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.Api.Models.Weed;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.WeedService.Models;

namespace GreenField.BLL.Services.WeedService
{
    public interface IWeedService
    {
        public Task<List<WeedDto>> BrowseAsync(BrowseWeeds query);
        public Task<WeedDto> GetAsync(Guid id);
        public Task CreateAsync(WeedDto weedDto);
        public Task<ImportResult> ImportAsync(List<WeedDto> listToImport);
        public Task UpdateAsync(WeedDto weedDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}