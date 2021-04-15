using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.NextCropAdvisor
{
    public interface INextCropAdvisor
    {
        Task<List<NextCropRecommendation>> GetRecommendation(Guid fieldId);
    }
}