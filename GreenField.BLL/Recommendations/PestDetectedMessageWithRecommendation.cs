using System;
using GreenField.BLL.Recommendations.Types;
using GreenField.Common.Enums;

namespace GreenField.BLL.Recommendations
{
    public class PestDetectedMessageWithRecommendation
    {
        public string PestName { get; set; }
        public DangerLevel Severity { get; set; }
        public Guid FieldId { get; set; }
        public Guid OrganisationId { get; set; }
        public PestDetectedRecommendation Recommendation { get; set; }
    }
}