using System;
using System.Threading;
using System.Threading.Tasks;
using GreenField.BLL.Recommendations;
using GreenField.BLL.Recommendations.Services;
using GreenField.BLL.Services.FieldService;
using GreenField.BLL.Services.MessageService;
using GreenField.BLL.Services.WeedService;
using GreenField.Common.Enums;
using GreenField.Common.Messaging;
using GreenField.Common.Messaging.Messages;
using MediatR;

namespace GreenField.Api.Handlers
{
    public class WeedDetectedHandler: INotificationHandler<WeedDetectedMessage>
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IWeedService _weedService;
        private readonly IFieldService _fieldService;
        private readonly IMessageService _messageService;

        public WeedDetectedHandler(IRecommendationService recommendationService, IFieldService fieldService, IMessageService messageService)
        {
            _recommendationService = recommendationService;
            _fieldService = fieldService;
            _messageService = messageService;
        }

        public async Task Handle(WeedDetectedMessage message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Weed detected:{message.FieldId} {message.OrganizationId} {message.Percentage} {message.Severity}");

            var field = await _fieldService.GetAsync(message.FieldId, message.OrganizationId);
            field.Status = message.Severity == DangerLevel.Low || message.Severity == DangerLevel.High
                ? FieldStatus.NeedAttention
                : FieldStatus.Critical;
            await _fieldService.UpdateAsync(field);

            var rec = await _recommendationService.GetWeedDetectedRecommendation(message);

            var weed = await _weedService.GetAsync(message.WeedId);
            
            _messageService.AddWeedDetectedMessage(new WeedDetectedMessageWithRecommendation()
            {
                FieldId = message.FieldId,
                OrganisationId = message.OrganizationId,
                Recommendation = rec,
                Severity = message.Severity,
                WeedName = weed.Kind
            });
        }
    }
}