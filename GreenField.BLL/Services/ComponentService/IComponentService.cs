using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.ComponentService.Models;

namespace GreenField.BLL.Services.ComponentService
{
    public interface IComponentService
    {
        public Task<List<ComponentDto>> BrowseAsync(BrowseComponents browseComponents);
        public Task<ComponentDto> GetAsync(Guid id);
        public Task CreateAsync(ComponentDto componentDto);
        Task<ImportResult> ImportAsync(List<ComponentDto> listToImport);
        public Task UpdateAsync(ComponentDto componentDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}