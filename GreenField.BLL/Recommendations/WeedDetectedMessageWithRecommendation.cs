using System;
using GreenField.BLL.Recommendations.Types;
using GreenField.BLL.Types;
using GreenField.Common.Enums;

namespace GreenField.BLL.Recommendations
{
    public class WeedDetectedMessageWithRecommendation
    {
        public string WeedName { get; set; }
        public DangerLevel Severity { get; set; }
        public Guid FieldId { get; set; }
        public Guid OrganisationId { get; set; }
        public WeedDetectedRecommendation Recommendation { get; set; }
    }
}