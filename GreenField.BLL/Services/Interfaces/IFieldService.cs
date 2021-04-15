using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.Services.Interfaces
{
    public interface IFieldService
    {
        public Task<List<FieldDto>> BrowseAsync();
        public Task<FieldDto> GetAsync(Guid id);
        public Task CreateAsync(FieldDto fieldDto);
        public Task UpdateAsync(FieldDto fieldDto);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(Guid id);
        Task AddCropAsync(CropDto cropDto, Guid fieldId);
    }
}