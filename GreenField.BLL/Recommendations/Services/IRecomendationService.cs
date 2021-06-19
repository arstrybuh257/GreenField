using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Recommendations.Types;
using GreenField.Common.Messaging.Messages;

namespace GreenField.BLL.Recommendations.Services
{
    public interface IRecommendationService
    {
        Task<List<NextCropRecommendation>> GetNextCropRecommendationByFieldId(Guid fieldId, DateTime date);
        Task<PestDetectedRecommendation> GetPestDetectedRecommendation(PestDetectedMessage message);
        Task<WeedDetectedRecommendation> GetWeedDetectedRecommendation(WeedDetectedMessage message);
    }
}