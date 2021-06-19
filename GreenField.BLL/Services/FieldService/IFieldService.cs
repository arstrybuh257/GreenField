using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.FieldService.Models;
using GreenField.BLL.Types;

namespace GreenField.BLL.Services.FieldService
{
    public interface IFieldService
    {
        public Task<List<FieldDto>> BrowseAsync(BrowseFields browseFields);
        public Task<FieldDto> GetAsync(Guid id, Guid organisationId);
        public Task CreateAsync(FieldDto fieldDto);
        public Task UpdateAsync(FieldDto fieldDto);
        public Task DeleteAsync(Guid id, Guid organisationId);
        public Task<bool> ExistsAsync(Guid id, Guid organisationId);
        Task AddCropAsync(CropDto cropDto);
    }
}