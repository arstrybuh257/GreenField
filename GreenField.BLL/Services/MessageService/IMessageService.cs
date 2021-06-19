using System;
using System.Collections.Generic;
using GreenField.BLL.Recommendations;
using GreenField.Common.Messaging.Messages;

namespace GreenField.BLL.Services.MessageService
{
    public interface IMessageService
    {
        List<WeedDetectedMessageWithRecommendation> GetWeedDetectedMessage(Guid organisationId);
        List<PestDetectedMessageWithRecommendation> GetPestDetectedMessage(Guid organisationId);
        
        void AddWeedDetectedMessage(WeedDetectedMessageWithRecommendation message);
        void AddPestDetectedMessage(PestDetectedMessageWithRecommendation message);
    }
}