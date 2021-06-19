using System;
using System.Threading.Tasks;
using GreenField.BLL.Recommendations;
using GreenField.BLL.Recommendations.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;


        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }
        
        [HttpGet("nextCrop/{fieldId}")]
        public async Task<IActionResult> NextCrop(Guid fieldId)
        {
            //var recommendation = await _recommendationService.GetRecommendation(fieldId);
            return Ok();
        }
        
        [HttpGet("killWeed/{fieldId}")]
        public async Task<IActionResult> KilWeed(Guid fieldId)
        {
            //var recommendation = await _pesticideAdvisor.GetRecommendation();
            return Ok();
        }
    }
}