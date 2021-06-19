using System;
using System.Collections.Generic;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Recommendations.Types
{
    public class WhereSeedRecommendation
    {
        public RecommendationLevel Recommendation { get; set; }
        public Guid FieldId { get; set; }
        public List<PesticideWithDose> Pesticides { get; set; }
    }
}