using System.Collections.Generic;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Recommendations.Types
{
    public class ShouldCultureBeSeededOnThisFieldRecommendation
    {
        public RecommendationLevel Recommendation { get; set; }
        public string Reason { get; set; }
        public List<PesticideWithDose> RecommendedPesticides { get; set; }
    }
}