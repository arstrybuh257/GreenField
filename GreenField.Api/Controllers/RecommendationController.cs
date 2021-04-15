using System;
using System.Threading.Tasks;
using GreenField.BLL.NextCropAdvisor;
using GreenField.BLL.PesticideAdvisor;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IPesticideAdvisor _pesticideAdvisor;
        private readonly INextCropAdvisor _nextCropAdvisor;

        public RecommendationController(IPesticideAdvisor pesticideAdvisor, INextCropAdvisor nextCropAdvisor)
        {
            _pesticideAdvisor = pesticideAdvisor;
            _nextCropAdvisor = nextCropAdvisor;
        }
        
        [HttpGet("nextCrop/{fieldId}")]
        public async Task<IActionResult> NextCrop(Guid fieldId)
        {
            var recommendation = await _nextCropAdvisor.GetRecommendation(fieldId);
            return Ok(recommendation);
        }
        
        [HttpGet("killWeed/{fieldId}")]
        public async Task<IActionResult> KilWeed(Guid fieldId)
        {
            var recommendation = await _pesticideAdvisor.GetRecommendation();
            return Ok(recommendation);
        }
    }
}