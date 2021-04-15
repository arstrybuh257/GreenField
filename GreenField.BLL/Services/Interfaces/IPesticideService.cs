using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface IPesticideService
    {
        public Task<List<PesticideDto>> BrowseAsync();
        public Task<PesticideDto> GetAsync(Guid id);
        public Task CreateAsync(PesticideDto pesticideDto);
        public Task UpdateAsync(PesticideDto pesticideDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(string email);
        public Task<bool> ExistsAsync(Guid id);
    }
}