using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.CultureService.Models;
using GreenField.BLL.Services.Interfaces;
using GreenField.Common;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services.CultureService
{
    public class CultureService : ICultureService
    {
        private readonly ICultureRepository _repository;
        private readonly IMapper _mapper;
        
        public CultureService(ICultureRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<CultureDto>> BrowseAsync()
        {
            var list = await _repository.BrowseAsync(x=>true);
            
            var culturesDto = _mapper.Map<List<CultureDto>>(list);
            foreach (var cultureDto in culturesDto)
            {
                var ecn = (await _repository.BrowseAsync(x => cultureDto.EnemyCultures.Contains(x.Id)))
                    .Select(x => x.Name).ToList();
                cultureDto.EnemyCulturesNames = ecn;
                var fcn = (await _repository.BrowseAsync(x => cultureDto.FriendlyCultures.Contains(x.Id)))
                    .Select(x => x.Name).ToList();
                cultureDto.FriendlyCulturesNames = fcn;
            }

            return culturesDto;
        }

        public async Task<CultureDto> GetAsync(Guid id)
        {
            return _mapper.Map<CultureDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(CultureDto cultureDto)
        {
            var culture = _mapper.Map<Culture>(cultureDto);
            culture.Id = Guid.NewGuid();
            await _repository.CreateAsync(culture);
        }
        
        public async Task<ImportResult> ImportAsync(List<CultureDto> listToImport)
        {
            ImportResult result = new ImportResult();
            foreach (var cultureDto in listToImport)
            {
                cultureDto.Id = Guid.NewGuid();
                try
                {
                    var culture = _mapper.Map<Culture>(cultureDto);
                    await _repository.CreateAsync(culture);
                    result.SuccessfullyImported++;
                }
                catch(Exception ex)
                {
                    result.Failed++;
                    result.FailedImports.Add(new FailedImport(cultureDto.Id, ex.Message));
                }
            }

            return result;
        }

        public async Task UpdateAsync(CultureDto cultureDto)
        {
            var culture = await _repository.GetAsync(cultureDto.Id);

            if (culture == null)
            {
                throw new GreenFieldNotFoundException();
            }

            culture = _mapper.Map<Culture>(cultureDto);
            await _repository.UpdateAsync(culture);
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