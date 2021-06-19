using MediatR;

namespace GreenField.Common.Messaging.Messages
{
    public class UserRegisteredMessage : INotification, IGreenFieldMessage
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}