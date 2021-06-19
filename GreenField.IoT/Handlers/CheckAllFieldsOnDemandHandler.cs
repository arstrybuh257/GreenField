using System;
using System.Threading;
using System.Threading.Tasks;
using GreenField.Common.Messaging;
using GreenField.Common.Messaging.Messages;
using GreenField.IoT.Interfaces;
using GreenField.IoT.Utils;
using MediatR;

namespace GreenField.IoT.Handlers
{
    public class CheckAllFieldsOnDemandHandler : INotificationHandler<CheckAllFieldsOnDemand>
    {
        private readonly IRabbitMqBus _bus;
        private readonly IDataProvider _dataProvider;

        public CheckAllFieldsOnDemandHandler(IRabbitMqBus bus, IDataProvider dataProvider)
        {
            _bus = bus;
            _dataProvider = dataProvider;
        }

        public Task Handle(CheckAllFieldsOnDemand notification, CancellationToken cancellationToken)
        {
            var fields = _dataProvider.GetFieldsAsync().Result;
            
            foreach (var field in fields)
            {
                CheckHelper.CheckPets(field, _dataProvider, _bus);
                CheckHelper.CheckWeeds(field, _dataProvider, _bus);
            }

            Console.WriteLine("Message was sent");
            
            return Task.CompletedTask;
        }
    }
}