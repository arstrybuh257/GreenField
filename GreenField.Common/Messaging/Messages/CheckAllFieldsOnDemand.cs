using System;
using MediatR;

namespace GreenField.Common.Messaging.Messages
{
    public class CheckAllFieldsOnDemand : INotification, IGreenFieldMessage
    {
        public Guid OrganisationId { get; set; }
    }
}