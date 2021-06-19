using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Culture;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.CultureService;
using GreenField.BLL.Services.CultureService.Models;
using GreenField.BLL.Services.Interfaces;
using GreenField.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    [Authorize]
    public class CultureController : BaseController
    {
        private readonly ICultureService _cultureService;
        private readonly IMapper _mapper;

        public CultureController(ICultureService cultureService, IMapper mapper)
        {
            _cultureService = cultureService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseCultures query)
        {
            return Collection(await _cultureService.BrowseAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _cultureService.GetAsync(id));
        }

        [HttpPost]
        [Authorize(Roles= Roles.SystemAdmin)]
        public async Task<IActionResult> Post(CreateCultureRequest request)
        {
            var cultureDto = _mapper.Map<CultureDto>(request);
            await _cultureService.CreateAsync(cultureDto);
            return Ok();
        }
        
        [HttpPost("import")]
        [Authorize(Roles= Roles.SystemAdmin)]
        public async Task<IActionResult> Import(List<CreateCultureRequest>request)
        {
            var cultureDto = _mapper.Map<List<CultureDto>>(request); 
            var res = await _cultureService.ImportAsync(cultureDto);
            return Ok(res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles= Roles.SystemAdmin)]
        public async Task<IActionResult> Put([FromBody] UpdateCultureRequest request, Guid id)
        {
            var cultureDto = _mapper.Map<CultureDto>(request);
            cultureDto.Id = id;
            await _cultureService.UpdateAsync(cultureDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles= Roles.SystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cultureService.DeleteAsync(id);
            return NoContent();
        }
    }
}