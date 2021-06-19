using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Recommendations.Types
{
    public class PesticidesShouldBeUsedRecommendation
    {
        public PesticideWithDose Pesticide { get; set; }
        public RecommendationLevel Recommendation { get; set; }
    }
}