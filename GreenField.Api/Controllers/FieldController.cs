using System;
using System.Threading.Tasks;
using AutoMapper;
using GreenField.Api.Models.Crop;
using GreenField.Api.Models.Field;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.FieldService;
using GreenField.BLL.Services.FieldService.Models;
using GreenField.BLL.Services.ImageService;
using GreenField.BLL.Services.Interfaces;
using GreenField.BLL.Types;
using GreenField.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    [Authorize(Roles = Roles.OrganisationAdmin)]
    public class FieldController : BaseController
    {
        private readonly IFieldService _fieldService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;
        private const string ImagesPath = @"C:\ImagesFromGF";

        public FieldController(IFieldService fieldService, IMapper mapper, IImageService imageService)
        {
            _fieldService = fieldService;
            _mapper = mapper;
            _imageService = imageService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BrowseFields query)
        {
            return Collection(await _fieldService.BrowseAsync(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            return Single(await _fieldService.GetAsync(id, OrganisationId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateFieldRequest request)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            var fieldDto = _mapper.Map<FieldDto>(request);
            fieldDto.OrganisationId = OrganisationId;
            var id = Guid.NewGuid();
            fieldDto.Id = id;
            await _fieldService.CreateAsync(fieldDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateFieldRequest request, Guid id)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            var fieldDto = _mapper.Map<FieldDto>(request);
            fieldDto.Id = id;
            fieldDto.OrganisationId = OrganisationId;
            await _fieldService.UpdateAsync(fieldDto);
            return NoContent();
        }
        
        [HttpPost("addCrop")]
        public async Task<IActionResult> AddCrop([FromBody] AddCropRequest request)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            var cropDto = _mapper.Map<CropDto>(request);
            cropDto.OrganisationId = OrganisationId;
            await _fieldService.AddCropAsync(cropDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (OrganisationId == Guid.Empty)
            {
                return Forbid();
            }
            await _fieldService.DeleteAsync(id, OrganisationId);
            return NoContent();
        }
    }
}