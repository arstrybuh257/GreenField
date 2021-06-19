using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.ComponentService.Models;
using GreenField.Common;
using GreenField.DAL.Entities;
using GreenField.DAL.Repositories.Interfaces;

namespace GreenField.BLL.Services.ComponentService
{
    public class ComponentService : IComponentService
    {
        private readonly IComponentRepository _repository;
        private readonly IMapper _mapper;
        
        public ComponentService(IComponentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<ComponentDto>> BrowseAsync(BrowseComponents query)
        {
            var list = await _repository.BrowseAsync();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                list = list.Where(x => x.Name.ToLower().Contains(query.Name.ToLower()));
            }
            
            return _mapper.Map<List<ComponentDto>>(list.ToList());
        }

        public async Task<ComponentDto> GetAsync(Guid id)
        {
            return _mapper.Map<ComponentDto>(await _repository.GetAsync(id));
        }

        public async Task CreateAsync(ComponentDto componentDto)
        {
            var component = _mapper.Map<Component>(componentDto);
            component.Id = Guid.NewGuid();
            await _repository.CreateAsync(component);
        }
        
        public async Task<ImportResult> ImportAsync(List<ComponentDto> listToImport)
        {
            ImportResult result = new ImportResult();
            foreach (var componentDto in listToImport)
            {
                componentDto.Id = Guid.NewGuid();
                try
                {
                    var component = _mapper.Map<Component>(componentDto);
                    await _repository.CreateAsync(component);
                    result.SuccessfullyImported++;
                }
                catch(Exception ex)
                {
                    result.Failed++;
                    result.FailedImports.Add(new FailedImport(componentDto.Id, ex.Message));
                }
            }

            return result;
        }

        public async Task UpdateAsync(ComponentDto componentDto)
        {
            var component = await _repository.GetAsync(componentDto.Id);

            if (component == null)
            {
                throw new GreenFieldNotFoundException();
            }

            component = _mapper.Map<Component>(componentDto);
            await _repository.UpdateAsync(component);
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