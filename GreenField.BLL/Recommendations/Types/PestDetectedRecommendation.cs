using System.Collections.Generic;
using GreenField.DAL.ValueObjects;

namespace GreenField.BLL.Recommendations.Types
{
    public class PestDetectedRecommendation
    {
        public List<PesticideWithDose> Pesticides { get; set; }
    }
}