using System;
using GreenField.Common.Enums;
using MediatR;

namespace GreenField.Common.Messaging.Messages
{
    public class PestDetectedMessage : INotification, IGreenFieldMessage
    {
        public Guid OrganizationId { get; set; }
        public Guid FieldId { get; set; }
        public Guid PestId { get; set; }
        public int CountOnSquareMeter { get; set; }
        public DangerLevel Severity { get; set; }
    }
}