using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Culture;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
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
        public async Task<IActionResult> Post(CreateCultureRequest request)
        {
            var cultureDto = _mapper.Map<CultureDto>(request);
            await _cultureService.CreateAsync(cultureDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateCultureRequest request, Guid id)
        {
            var cultureDto = _mapper.Map<CultureDto>(request);
            cultureDto.Id = id;
            await _cultureService.UpdateAsync(cultureDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cultureService.DeleteAsync(id);
            return NoContent();
        }
    }
}