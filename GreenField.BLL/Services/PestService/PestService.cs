using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.PestService.Models;
using GreenField.Common;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services.PestService
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
        
        public async Task<List<PestDto>> BrowseAsync(BrowsePests query)
        {
            var list = await _repository.BrowseAsync();
            if (!string.IsNullOrWhiteSpace(query.Kind))
            {
                list = list.Where(x => x.Kind.ToLower().Contains(query.Kind.ToLower()));
            }

            if (query.Culture != null)
            {
                list = list.Where(x => x.Cultures.Contains(query.Culture.Value));
            }
            return _mapper.Map<List<PestDto>>(list.ToList());
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
        
        public async Task<ImportResult> ImportAsync(List<PestDto> listToImport)
        {
            ImportResult result = new ImportResult();
            foreach (var pestDto in listToImport)
            {
                pestDto.Id = Guid.NewGuid();
                try
                {
                    var pest = _mapper.Map<Pest>(pestDto);
                    await _repository.CreateAsync(pest);
                    result.SuccessfullyImported++;
                }
                catch(Exception ex)
                {
                    result.Failed++;
                    result.FailedImports.Add(new FailedImport(pestDto.Id, ex.Message));
                }
            }

            return result;
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