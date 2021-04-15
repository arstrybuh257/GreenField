using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Pest;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
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
            return Collection(await _pestService.BrowseAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _pestService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePestRequest request)
        {
            var pestDto = _mapper.Map<PestDto>(request);
            await _pestService.CreateAsync(pestDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdatePestRequest request, Guid id)
        {
            var pestDto = _mapper.Map<PestDto>(request);
            pestDto.Id = id;
            await _pestService.UpdateAsync(pestDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pestService.DeleteAsync(id);
            return NoContent();
        }
    }
}