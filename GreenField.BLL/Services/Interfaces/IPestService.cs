using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface IPestService
    {
        public Task<List<PestDto>> BrowseAsync();
        public Task<PestDto> GetAsync(Guid id);
        public Task CreateAsync(PestDto pestDto);
        public Task UpdateAsync(PestDto pestDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string kind);
        public Task<bool> ExistsAsync(Guid id);
    }
}