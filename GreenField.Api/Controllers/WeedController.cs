using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Weed;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    public class WeedController : BaseController
    {
        private readonly IWeedService _weedService;
        private readonly IMapper _mapper;

        public WeedController(IWeedService weedService, IMapper mapper)
        {
            _weedService = weedService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseWeeds query)
        {
            return Collection(await _weedService.BrowseAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _weedService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateWeedRequest request)
        {
            var weedDto = _mapper.Map<WeedDto>(request);
            await _weedService.CreateAsync(weedDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateWeedRequest request, Guid id)
        {
            var weedDto = _mapper.Map<WeedDto>(request);
            weedDto.Id = id;
            await _weedService.UpdateAsync(weedDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _weedService.DeleteAsync(id);
            return NoContent();
        }
    }
}