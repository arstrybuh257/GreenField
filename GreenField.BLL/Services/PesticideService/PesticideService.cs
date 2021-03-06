using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.PesticideService.Models;
using GreenField.Common;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services.PesticideService
{
    public class PesticideService : IPesticideService
    {
        private readonly IPesticideRepository _repository;
        private readonly IMapper _mapper;
        
        public PesticideService(IPesticideRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<PesticideDto>> BrowseAsync(BrowsePesticides query)
        {
            var list = await _repository.BrowseAsync(p=> true);
            return _mapper.Map<List<PesticideDto>>(list);
        }

        public async Task<PesticideDto> GetAsync(Guid id)
        {
            return _mapper.Map<PesticideDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(PesticideDto pesticideDto)
        {
            var pesticide = _mapper.Map<Pesticide>(pesticideDto);
            pesticide.Id = Guid.NewGuid();
            await _repository.CreateAsync(pesticide);
        }
        
        public async Task<ImportResult> ImportAsync(List<PesticideDto> listToImport)
        {
            ImportResult result = new ImportResult();
            foreach (var pestDto in listToImport)
            {
                pestDto.Id = Guid.NewGuid();
                try
                {
                    var pesticide = _mapper.Map<Pesticide>(pestDto);
                    await _repository.CreateAsync(pesticide);
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

        public async Task UpdateAsync(PesticideDto pesticideDto)
        {
            var pesticide = await _repository.GetAsync(pesticideDto.Id);

            if (pesticide == null)
            {
                throw new GreenFieldNotFoundException();
            }

            pesticide = _mapper.Map<Pesticide>(pesticideDto);
            await _repository.UpdateAsync(pesticide);
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