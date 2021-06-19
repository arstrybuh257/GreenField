using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Pest;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.PestService;
using GreenField.BLL.Services.PestService.Models;
using GreenField.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    [Authorize]
    public class PestController : BaseController
    {
        private readonly IPestService _pestService;
        private readonly IMapper _mapper;

        public PestController(IPestService pestService, IMapper mapper)
        {
            _pestService = pestService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowsePests query)
        {
            return Collection(await _pestService.BrowseAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _pestService.GetAsync(id));
        }

        [HttpPost]
        [Authorize(Roles= Roles.SystemAdmin)]
        public async Task<IActionResult> Post(CreatePestRequest request)
        {
            var pestDto = _mapper.Map<PestDto>(request);
            await _pestService.CreateAsync(pestDto);
            return Ok();
        }
        
        [HttpPost("import")]
        [Authorize(Roles= Roles.SystemAdmin)]
        public async Task<IActionResult> Import(List<CreatePestRequest>request)
        {
            var pestDto = _mapper.Map<List<PestDto>>(request); 
            var res = await _pestService.ImportAsync(pestDto);
            return Ok(res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles= Roles.SystemAdmin)]
        public async Task<IActionResult> Put([FromBody] UpdatePestRequest request, Guid id)
        {
            var pestDto = _mapper.Map<PestDto>(request);
            pestDto.Id = id;
            await _pestService.UpdateAsync(pestDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles= Roles.SystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pestService.DeleteAsync(id);
            return NoContent();
        }
    }
}