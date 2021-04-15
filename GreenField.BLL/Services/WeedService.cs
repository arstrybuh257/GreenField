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
    public class WeedService : IWeedService
    {
        private readonly IWeedRepository _repository;
        private readonly IMapper _mapper;
        
        public WeedService(IWeedRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<WeedDto>> BrowseAsync()
        {
            var list = await _repository.BrowseAsync();
            return _mapper.Map<List<WeedDto>>(list);
        }

        public async Task<WeedDto> GetAsync(Guid id)
        {
            return _mapper.Map<WeedDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(WeedDto weedDto)
        {
            var weed = _mapper.Map<Weed>(weedDto);
            weed.Id = Guid.NewGuid();
            await _repository.CreateAsync(weed);
        }

        public async Task UpdateAsync(WeedDto weedDto)
        {
            var weed = await _repository.GetAsync(weedDto.Id);

            if (weed == null)
            {
                throw new GreenFieldNotFoundException();
            }

            weed = _mapper.Map<Weed>(weedDto);
            await _repository.UpdateAsync(weed);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _repository.ExistsAsync(name);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}