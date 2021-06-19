using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Component;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.ComponentService;
using GreenField.BLL.Services.ComponentService.Models;
using GreenField.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GreenField.Common.Constants.Roles;

namespace GreenField.Api.Controllers
{
    [Authorize(Roles = SystemAdmin)]
    public class ComponentController : BaseController
    {
        private readonly IComponentService _componentService;
        private readonly IMapper _mapper;

        public ComponentController(IComponentService componentService, IMapper mapper)
        {
            _componentService = componentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseComponents query)
        {
            return Collection(await _componentService.BrowseAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _componentService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateComponentRequest request)
        {
            var componentDto = _mapper.Map<ComponentDto>(request);
            await _componentService.CreateAsync(componentDto);
            return Ok();
        }
        
        [HttpPost("import")]
        public async Task<IActionResult> Import(List<CreateComponentRequest>request)
        {
            var componentDto = _mapper.Map<List<ComponentDto>>(request); 
            var res = await _componentService.ImportAsync(componentDto);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateComponentRequest request, Guid id)
        {
            var componentDto = _mapper.Map<ComponentDto>(request);
            componentDto.Id = id;
            await _componentService.UpdateAsync(componentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _componentService.DeleteAsync(id);
            return NoContent();
        }
    }
}