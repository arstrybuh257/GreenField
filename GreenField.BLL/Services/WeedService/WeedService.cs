using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Weed;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.WeedService.Models;
using GreenField.Common;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services.WeedService
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
        
        public async Task<List<WeedDto>> BrowseAsync(BrowseWeeds query)
        {
            var list = await _repository.BrowseAsync();

            if (!string.IsNullOrWhiteSpace(query.Kind))
            {
                list = list.Where(x => x.Kind.ToLower().Contains(query.Kind.ToLower()));
            }
            
            return _mapper.Map<List<WeedDto>>(list.ToList());
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

        public async Task<ImportResult> ImportAsync(List<WeedDto> listToImport)
        {
            ImportResult result = new ImportResult();
            foreach (var weedDto in listToImport)
            {
                weedDto.Id = Guid.NewGuid();
                try
                {
                    var weed = _mapper.Map<Weed>(weedDto);
                    await _repository.CreateAsync(weed);
                    result.SuccessfullyImported++;
                }
                catch(Exception ex)
                {
                    result.Failed++;
                    result.FailedImports.Add(new FailedImport(weedDto.Id, ex.Message));
                }
            }

            return result;
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