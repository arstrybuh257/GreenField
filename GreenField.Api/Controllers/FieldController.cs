using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Crop;
using GreenField.Api.Models.Field;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    public class FieldController : BaseController
    {
        private readonly IFieldService _fieldService;
        private readonly IMapper _mapper;

        public FieldController(IFieldService fieldService, IMapper mapper)
        {
            _fieldService = fieldService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseFields query)
        {
            return Collection(await _fieldService.BrowseAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Single(await _fieldService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateFieldRequest request)
        {
            var fieldDto = _mapper.Map<FieldDto>(request);
            await _fieldService.CreateAsync(fieldDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateFieldRequest request, Guid id)
        {
            var fieldDto = _mapper.Map<FieldDto>(request);
            fieldDto.Id = id;
            await _fieldService.UpdateAsync(fieldDto);
            return NoContent();
        }
        
        [HttpPost("addCrop")]
        public async Task<IActionResult> AddCrop([FromBody] AddCropRequest request)
        {
            var cropDto = _mapper.Map<CropDto>(request);
            await _fieldService.AddCropAsync(cropDto, request.FieldId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _fieldService.DeleteAsync(id);
            return NoContent();
        }
    }
}