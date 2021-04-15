using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using GreenField.Common;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _repository;
        private readonly IMapper _mapper;
        
        public FieldService(IFieldRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<FieldDto>> BrowseAsync()
        {
            var list = await _repository.BrowseAsync();
            return _mapper.Map<List<FieldDto>>(list);
        }

        public async Task<FieldDto> GetAsync(Guid id)
        {
            return _mapper.Map<FieldDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(FieldDto fieldDto)
        {
            var field = _mapper.Map<Field>(fieldDto);
            field.Id = Guid.NewGuid();
            await _repository.CreateAsync(field);
        }

        public async Task UpdateAsync(FieldDto fieldDto)
        {
            var field = await _repository.GetAsync(fieldDto.Id);

            if (field == null)
            {
                throw new GreenFieldNotFoundException();
            }

            field = _mapper.Map<Field>(fieldDto);
            await _repository.UpdateAsync(field);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task AddCropAsync(CropDto cropDto, Guid fieldId)
        {
            var field = await _repository.GetAsync(fieldId);

            if (field == null)
            {
                throw new GreenFieldNotFoundException();
            }
            var crop = _mapper.Map<Crop>(cropDto);
            field.Crops.Add(crop);
            await _repository.UpdateAsync(field);
        }
    }
}