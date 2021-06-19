using System.Collections.Generic;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Recommendations.Types
{
    public class NextCropRecommendation
    {
        public RecommendationLevel Recommendation;  
        public string CultureName { get; set; }
        public List<PesticideWithDose> Pesticides { get; set; }
    }
}