using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.FieldService.Models;
using GreenField.BLL.Types;
using GreenField.Common;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Services.FieldService
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _repository;
        private readonly ICultureRepository _cultureRepository;
        private readonly IMapper _mapper;
        
        public FieldService(IFieldRepository repository, IMapper mapper, ICultureRepository cultureRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _cultureRepository = cultureRepository;
        }
        
        public async Task<List<FieldDto>> BrowseAsync(BrowseFields browseFields)
        {
            var list = await _repository.BrowseAsync();
            var listDto = _mapper.Map<List<FieldDto>>(list);

            foreach (var field in listDto)
            {
                foreach (var crop in field.Crops)
                {
                    crop.CultureName = _cultureRepository.GetAsync(crop.CultureId).Result.Name;
                }
            }
            
            return listDto;
        }

        public async Task<FieldDto> GetAsync(Guid id, Guid organisationId)
        {
            return _mapper.Map<FieldDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(FieldDto fieldDto)
        {
            var field = _mapper.Map<Field>(fieldDto);
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
        
        public async Task DeleteAsync(Guid id, Guid organisationId)
        {
            if (!await ExistsAsync(id, organisationId))
            {
                throw new GreenFieldNotFoundException();
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(Guid id, Guid organisationId)
        {
            return await _repository.ExistsAsync(id, organisationId);
        }

        public async Task AddCropAsync(CropDto cropDto)
        {
            var field = await _repository.GetAsync(cropDto.FieldId);

            if (field == null || field.OrganisationId != cropDto.OrganisationId)
            {
                throw new GreenFieldNotFoundException();
            }
            var crop = _mapper.Map<Crop>(cropDto);
            field.Crops.Add(crop);
            await _repository.UpdateAsync(field);
        }
    }
}