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
    public class PestService : IPestService
    {
        private readonly IPestRepository _repository;
        private readonly IMapper _mapper;
        
        public PestService(IPestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<PestDto>> BrowseAsync()
        {
            var list = await _repository.BrowseAsync();
            return _mapper.Map<List<PestDto>>(list);
        }

        public async Task<PestDto> GetAsync(Guid id)
        {
            return _mapper.Map<PestDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(PestDto pestDto)
        {
            var pest = _mapper.Map<Pest>(pestDto);
            pest.Id = Guid.NewGuid();
            await _repository.CreateAsync(pest);
        }

        public async Task UpdateAsync(PestDto pestDto)
        {
            var pest = await _repository.GetAsync(pestDto.Id);

            if (pest == null)
            {
                throw new GreenFieldNotFoundException();
            }

            pest = _mapper.Map<Pest>(pestDto);
            await _repository.UpdateAsync(pest);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(string kind)
        {
            return await _repository.ExistsAsync(kind);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}