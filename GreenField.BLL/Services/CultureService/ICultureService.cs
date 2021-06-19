using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.CultureService.Models;

namespace GreenField.BLL.Services.CultureService
{
    public interface ICultureService
    {
        public Task<List<CultureDto>> BrowseAsync();
        public Task<CultureDto> GetAsync(Guid id);
        public Task CreateAsync(CultureDto cultureDto);
        Task<ImportResult> ImportAsync(List<CultureDto> listToImport);
        public Task UpdateAsync(CultureDto cultureDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}