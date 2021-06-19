using System;
using GreenField.Common.Enums;
using MediatR;

namespace GreenField.Common.Messaging.Messages
{
    public class WeedDetectedMessage : INotification, IGreenFieldMessage
    {
        public Guid OrganizationId { get; set; }
        public Guid FieldId { get; set; }
        public Guid WeedId { get; set; }
        public int Percentage { get; set; }
        public DangerLevel Severity { get; set; }
    }
}