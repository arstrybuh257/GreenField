using System.Collections.Generic;
using System.Threading.Tasks;
using GreenField.BLL.Dto;
using GreenField.BLL.Services.Interfaces;

namespace GreenField.BLL.NextCropAdvisor
{
    public interface INextCropStrategy
    {
        Task<List<NextCropRecommendation>> GetRecommendation(FieldDto field, List<CultureDto> cultures);

    }
}