using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Pesticide;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    public class PesticideController : BaseController
    {
        private readonly IPesticideService _pesticideService;
        private readonly IMapper _mapper;

        public PesticideController(IPesticideService pesticideService, IMapper mapper)
        {
            _pesticideService = pesticideService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowsePesticides query)
        {
            return Collection(await _pesticideService.BrowseAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _pesticideService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreatePesticideRequest request)
        {
            var pesticideDto = _mapper.Map<PesticideDto>(request);
            await _pesticideService.CreateAsync(pesticideDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdatePesticideRequest request, Guid id)
        {
            var pesticideDto = _mapper.Map<PesticideDto>(request);
            pesticideDto.Id = id;
            await _pesticideService.UpdateAsync(pesticideDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pesticideService.DeleteAsync(id);
            return NoContent();
        }
    }
}