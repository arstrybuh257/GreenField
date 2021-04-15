using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface ICultureService
    {
        public Task<List<CultureDto>> BrowseAsync();
        public Task<CultureDto> GetAsync(Guid id);
        public Task CreateAsync(CultureDto cultureDto);
        public Task UpdateAsync(CultureDto cultureDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string email);
        public Task<bool> ExistsAsync(Guid id);
    }
}