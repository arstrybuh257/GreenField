using System;
using System.Collections.Generic;
using System.Linq;
using GreenField.BLL.Recommendations;
using GreenField.Common.Messaging.Messages;

namespace GreenField.BLL.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly List<WeedDetectedMessageWithRecommendation> _weedMessages;
        private readonly List<PestDetectedMessageWithRecommendation> _pestMessages;

        public MessageService()
        {
            _weedMessages = new List<WeedDetectedMessageWithRecommendation>();
            _pestMessages = new List<PestDetectedMessageWithRecommendation>();
        }

        public List<WeedDetectedMessageWithRecommendation> GetWeedDetectedMessage(Guid organisationId)
        {
            lock (_weedMessages)
            {
                var collection = _weedMessages
                    .Where(x => x.OrganisationId == organisationId).ToList();

                foreach (var item in collection)
                {
                    _weedMessages.Remove(item);
                }
                
                return collection;
            }
        }

        public List<PestDetectedMessageWithRecommendation> GetPestDetectedMessage(Guid organisationId)
        {
            lock (_pestMessages)
            {
                var collection = _pestMessages
                    .Where(x => x.OrganisationId == organisationId).ToList();
                
                foreach (var item in collection)
                {
                    _pestMessages.Remove(item);
                }
                
                return collection;
            }
        }

        public void AddWeedDetectedMessage(WeedDetectedMessageWithRecommendation message)
        {
            lock (_weedMessages)
            {
                _weedMessages.Add(message);
            }
        }

        public void AddPestDetectedMessage(PestDetectedMessageWithRecommendation message)
        {
            lock (_pestMessages)
            {
                _pestMessages.Add(message);
            }
        }
    }
}