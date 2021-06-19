using System;
using System.Threading;
using Autofac;
using EasyNetQ;
using MediatR;

namespace GreenField.Common.Messaging
{
    public class RabbitMqBus : IRabbitMqBus
    {
        private IComponentContext _context;

        public RabbitMqBus(IComponentContext context)
        {
            _context = context;
        }

        public void Publish<T>(T message) where T: IGreenFieldMessage
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest"))
            {
                bus.PubSub.Publish(message);
            }
        }

        public void Subscribe<T>(string serviceName) where T : IGreenFieldMessage, INotification
        {
            var bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest");
            var handler = _context.Resolve<INotificationHandler<T>>();
            bus.PubSub.SubscribeAsync<T>(serviceName, message => handler.Handle(message, CancellationToken.None));
        }
    }
}