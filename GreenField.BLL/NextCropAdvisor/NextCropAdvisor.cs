using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;

namespace GreenField.BLL.NextCropAdvisor
{
    public class NextCropAdvisor : INextCropAdvisor
    {
        private INextCropStrategy _strategy;
        private readonly IFieldService _fieldService;
        private readonly ICultureService _cultureService;

        public NextCropAdvisor(INextCropStrategy strategy, IFieldService fieldService, ICultureService cultureService)
        {
            _strategy = strategy;
            _fieldService = fieldService;
            _cultureService = cultureService;
        }

        public void SetStrategy(INextCropStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task<List<NextCropRecommendation>> GetRecommendation(Guid fieldId)
        {
            var fieldDto = await _fieldService.GetAsync(fieldId);
            var cultures = await _cultureService.BrowseAsync();
            if (fieldDto == null)
            {
                return null;
            }

            return await _strategy.GetRecommendation(fieldDto, cultures);
        }
    }
}