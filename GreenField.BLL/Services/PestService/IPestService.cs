using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.PestService.Models;

namespace GreenField.BLL.Services.PestService
{
    public interface IPestService
    {
        public Task<List<PestDto>> BrowseAsync(BrowsePests query);
        public Task<PestDto> GetAsync(Guid id);
        public Task CreateAsync(PestDto pestDto);
        Task<ImportResult> ImportAsync(List<PestDto> listToImport);
        public Task UpdateAsync(PestDto pestDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string kind);
        public Task<bool> ExistsAsync(Guid id);
    }
}