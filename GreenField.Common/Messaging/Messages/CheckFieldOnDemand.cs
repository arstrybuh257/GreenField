using System;
using MediatR;

namespace GreenField.Common.Messaging.Messages
{
    public class CheckFieldOnDemand : INotification, IGreenFieldMessage
    {
        public Guid FieldId { get; set; }
    }
}