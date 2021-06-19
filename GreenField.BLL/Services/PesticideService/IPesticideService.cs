using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.PesticideService.Models;

namespace GreenField.BLL.Services.PesticideService
{
    public interface IPesticideService
    {
        public Task<List<PesticideDto>> BrowseAsync(BrowsePesticides query);
        public Task<PesticideDto> GetAsync(Guid id);
        public Task CreateAsync(PesticideDto pesticideDto);
        Task<ImportResult> ImportAsync(List<PesticideDto> listToImport);
        public Task UpdateAsync(PesticideDto pesticideDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string name);
        public Task<bool> ExistsAsync(Guid id);
    }
}