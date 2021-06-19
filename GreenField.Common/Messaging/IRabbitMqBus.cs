using MediatR;

namespace GreenField.Common.Messaging
{
    public interface IRabbitMqBus
    {
        void Publish<T>(T message) where T : IGreenFieldMessage;
        void Subscribe<T>(string serviceName) where T : IGreenFieldMessage, INotification;
    }
}