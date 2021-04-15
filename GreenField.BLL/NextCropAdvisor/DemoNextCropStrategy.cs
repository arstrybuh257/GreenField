using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenField.BLL.Dto;

namespace GreenField.BLL.NextCropAdvisor
{
    public class DemoNextCropStrategy : INextCropStrategy
    {
        public Task<List<NextCropRecommendation>> GetRecommendation(FieldDto field, List<CultureDto> cultures)
        {
            var lastCrop = field.Crops.OrderByDescending(x => x.DateTo).FirstOrDefault();
            List<NextCropRecommendation> recommendations = new List<NextCropRecommendation>();

            if (lastCrop != null)
            {
                foreach (var culture in cultures)
                {
                    if (culture.FriendCultures.Contains(lastCrop.CultureId))
                    {
                        recommendations.Add(new NextCropRecommendation()
                        {
                            Culture = culture,
                            SuccessPossibility = 100
                        });
                    }
                }
            }


            return Task.FromResult(recommendations);
        }
    }
}