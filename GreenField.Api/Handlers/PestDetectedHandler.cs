using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using GreenField.BLL.Recommendations;
using GreenField.BLL.Recommendations.Services;
using GreenField.BLL.Services.FieldService;
using GreenField.BLL.Services.MessageService;
using GreenField.BLL.Services.PestService;
using GreenField.Common.Enums;
using GreenField.Common.Messaging.Messages;
using MediatR;

namespace GreenField.Api.Handlers
{
    public class PestDetectedHandler : INotificationHandler<PestDetectedMessage>
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IPestService _pestService;
        private readonly IFieldService _fieldService;
        private readonly IMessageService _messageService;

        public PestDetectedHandler(IRecommendationService recommendationService, IFieldService fieldService, IMessageService messageService, IPestService pestService)
        {
            _recommendationService = recommendationService;
            _fieldService = fieldService;
            _messageService = messageService;
            _pestService = pestService;
        }

        public async Task Handle(PestDetectedMessage message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Weed detected:{message.FieldId} {message.OrganizationId} {message.CountOnSquareMeter} {message.Severity}");

            var field = await _fieldService.GetAsync(message.FieldId, message.OrganizationId);
            field.Status = message.Severity == DangerLevel.Low || message.Severity == DangerLevel.High
                ? FieldStatus.NeedAttention
                : FieldStatus.Critical;
            await _fieldService.UpdateAsync(field);

            var rec = await _recommendationService.GetPestDetectedRecommendation(message);

            var pest = await _pestService.GetAsync(message.PestId);
            
            _messageService.AddPestDetectedMessage(new PestDetectedMessageWithRecommendation()
            {
                FieldId = message.FieldId,
                OrganisationId = message.OrganizationId,
                Recommendation = rec,
                Severity = message.Severity,
                PestName = pest.Kind
            });
        }
    }
}